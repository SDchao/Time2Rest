using NAudio.Wave;
using NLog;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Resources;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Time2Rest.Config;
using Time2Rest.Languages;
using Time2Rest.Updater;
using Time2Rest.WinInteractors;

namespace Time2Rest
{
    public partial class AlertForm : Form
    {
        #region variables

        private const int HIDING = -1;
        private const int FADE_IN = 1;
        private const int SHOWING = 2;
        private const int FADE_OUT = 3;

        private readonly Logger logger;

        // WinAPI
        private DefaultHook DefaultHook = new DefaultHook();

        // Display
        private int status = HIDING;
        private Screen screen;
        private int scWidth = 0;

        // Time counter
        private int remainingSeconds = -1;

        private readonly object countdownLock = new object();
        private readonly int maxSpareSeconds = 5 * 60;
        private int remainingSpareSeconds;
        private int showingTime = 0;
        private int userOperatingTime = 0;

        // Config
        // Function Config
        private int alertInterval;

        private int minimumRestTime;
        private int alertAgainInterval;
        private bool hideWhenFullScreen;
        private bool hasRingtonePath;
        private string ringtonePath;

        // UI Config
        private double maxOpacity;

        private Color userBackColor;
        private Color userForeColor;
        private string backGroundImgPath;

        // Lang
        private ResXResourceSet lang;

        // Ringtone
        private IWavePlayer waveOutDevice = new WaveOut();

        private AudioFileReader audioFileReader;

        private bool manuallyRest;

        // Focus
        private IntPtr preForeHwnd;

        // Screenshot Check
        ScreenshotChecker sc = new ScreenshotChecker();

        #endregion variables

        #region DLL Import
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_LAYERED = 0x80000;
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int WS_EX_NOACTIVATE = 0x08000000;
        #endregion

        public AlertForm()
        {
            // Lang
            lang = LanguageManager.GetLangRes();

            if (CheckUniqueProcess.CheckUnique())
            {
                MessageBox.Show(lang.GetString("TIP_STARTED"));
                Environment.Exit(0);
            }

            // Logger
            logger = LogManager.GetCurrentClassLogger();

            // Update Check
            Task.Run(() =>
            {
                UpdateChecker.CheckUpdate();
            });

            preForeHwnd = GetForegroundWindow();

            InitializeComponent();

            DefaultHook.OnOperation += OnUserOperation;
            sc.onChanged += OnUserOperation;

            // Menu Lang
            NotifyMenu.Items.Add(lang.GetString("MENU_REST"), Resource.PNG_REST);
            NotifyMenu.Items.Add(lang.GetString("MENU_SETTING"), Resource.PNG_SETTING);
            NotifyMenu.Items.Add(lang.GetString("MENU_ABOUT"), Resource.PNG_ABOUT);
            NotifyMenu.Items.Add(lang.GetString("MENU_EXIT"), Resource.PNG_EXIT);

            NotifyMenu.ItemClicked += NotifyMenu_ItemClicked;

            // Init
            this.Opacity = 0.0;

            this.UpdateTimer.Enabled = false;

            this.waveOutDevice.PlaybackStopped += OnWaveOutDeviceStopped;

            logger.Info("Alert Form Init Completed");
        }

        #region Config Reading

        private void UpdateConfig()
        {
            T2rConfig config = T2rConfigManager.ReadConfig();
            UpdateConfig(config);
        }

        private void UpdateConfig(T2rConfig config)
        {
            alertInterval = config.alertInterval;
            minimumRestTime = config.minimumRestTime;
            alertAgainInterval = config.alertAgainInterval;
            hideWhenFullScreen = config.hideWhenFullscreen;
            this.screen = config.screen;
            hasRingtonePath = !String.IsNullOrEmpty(config.ringtonePath);
            ringtonePath = config.ringtonePath;

            maxOpacity = config.maxOpacity;
            userBackColor = config.GetBackColor();
            userForeColor = config.GetForeColor();
            backGroundImgPath = config.backGroundImgPath;

            logger.Info("Config reading completed");
            this.BackColor = userBackColor;
            if (!String.IsNullOrEmpty(backGroundImgPath))
                this.BackgroundImage = Image.FromFile(backGroundImgPath);
            else if (backGroundImgPath == "")
                this.BackgroundImage = null;

            ClockLabel.ForeColor = userForeColor;
            TipLabel.ForeColor = userForeColor;

            if (hasRingtonePath)
                audioFileReader = new AudioFileReader(ringtonePath);

            // Init after config reading
            remainingSeconds = alertInterval;
            remainingSpareSeconds = maxSpareSeconds;
            CountdownTimer.Enabled = true;

            // Modify Form Size
            this.Left = screen.Bounds.Left;
            this.Top = screen.Bounds.Top;

            // this.Width = screen.Bounds.Width;
            scWidth = screen.Bounds.Width;
            this.Width = 0;
            this.Height = screen.Bounds.Height;

            // Components Adjust
            float clockHeight = this.Height * 0.2f;
            ClockLabel.Font = new Font(ClockLabel.Font.Name, clockHeight * 0.75f);

            float tipHeight = clockHeight * 0.15f;
            TipLabel.Font = new Font(TipLabel.Font.Name, tipHeight * 0.75f);

            // Startup
            StartupManager.SetStartup(config.startup);

            // Final step
            logger.Info("Config applied");
        }
        private void AlertForm_Load(object sender, EventArgs e)
        {
            UpdateConfig();
            UpdateLayout();
            DefaultHook.StartHook();
            sc.Start();
            this.TopMost = false;
        }

        #endregion Config Reading

        #region Core Function


        private void AlertForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DefaultHook.StopHook();
            if (hasRingtonePath)
                audioFileReader.Dispose();
            waveOutDevice.Dispose();
            sc.Stop();
        }

        private void OnUserOperation()
        {
            if (status != HIDING)
            {
                this.TopMost = true; // 在StartRest()中不生效
                this.Width = scWidth;
            }
            if (status == HIDING)
            {
                // User came back or just using the computer
                lock (countdownLock)
                {
                    remainingSpareSeconds = maxSpareSeconds;
                    if (!CountdownTimer.Enabled)
                    {
                        logger.Info("User came back, enabling count down timer");
                        CountdownTimer.Enabled = true;
                        sc.Start();
                    }
                }
            }
            else if (status == SHOWING)
            {
                CountdownTimer.Enabled = false;

                lock (countdownLock)
                {
                    if (manuallyRest && showingTime < minimumRestTime)
                    {
                        // User didnt rest enough
                        logger.Info("Manually rest break, continue countdown");
                        remainingSeconds -= showingTime;

                        if (remainingSeconds <= 0)
                        {
                            remainingSeconds = alertAgainInterval;
                        }

                        // text modify
                        TipLabel.Text = String.Format(lang.GetString("REST_INCOMPLETE"), Math.Round(remainingSeconds / 60.0));
                    }
                    // User operated after showing the alert
                    else if (showingTime < minimumRestTime)
                    {
                        // User didnt rest enough
                        logger.Info("Insisting computer usage, alert later");
                        remainingSeconds = alertAgainInterval;

                        // text modify
                        TipLabel.Text = String.Format(lang.GetString("REST_INCOMPLETE"), Math.Round(alertAgainInterval / 60.0));
                    }
                    else
                    {
                        logger.Info("Rest done, reseting alert");
                        userOperatingTime = 0;
                        remainingSeconds = alertInterval;
                    }
                }

                status = FADE_OUT;
                UpdateTimer.Enabled = true;

                manuallyRest = false;
                this.TopMost = false;

                StopRingtone();
                sc.Start();
            }
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (status == FADE_IN)
            {
                UpdateClock();
                this.Opacity += maxOpacity / 3000 * UpdateTimer.Interval;
                if (this.Opacity >= maxOpacity)
                {
                    showingTime = 0;
                    status = SHOWING;
                    CountdownTimer.Enabled = true;
                }
            }
            else if (status == SHOWING)
            {
                UpdateClock();
            }
            else if (status == FADE_OUT)
            {
                UpdateClock();
                this.Opacity -= maxOpacity / 1000 * UpdateTimer.Interval;
                if (this.Opacity <= 0)
                {
                    this.Opacity = 0.0;
                    status = HIDING;
                    UpdateTimer.Enabled = false;
                    CountdownTimer.Enabled = true;
                    this.Width = 0;
                }
            }
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            if (status == HIDING)
            {
                lock (countdownLock)
                {
                    remainingSeconds -= CountdownTimer.Interval / 1000;
                    remainingSpareSeconds -= 1;
                    userOperatingTime += 1;
                }

                // User leave the computer
                if (remainingSpareSeconds <= 0)
                {
                    logger.Info("User has left, pausing");
                    remainingSeconds = alertInterval;
                    userOperatingTime = 0;
                    CountdownTimer.Enabled = false;
                    sc.Stop();
                }

                logger.Debug("Time remain: {0}", remainingSeconds);
                logger.Debug("Time before stop the timer: {0}", remainingSpareSeconds);
                logger.Debug("User operating time: {0}", userOperatingTime);

                // Notify Icon Update
                string userOperatingTimeStr = String.Format("({0}:{1:D2})", userOperatingTime / 60, userOperatingTime % 60);
                NotifyIcon.Text = "Time2Rest " + userOperatingTimeStr;
                NotifyMenu.Items[0].Text = lang.GetString("MENU_REST") + " " + userOperatingTimeStr;

                if (remainingSeconds <= 0)
                {
                    if (FullscreenDetector.IsForegroundFullScreen(this.screen) && hideWhenFullScreen)
                    {
                        logger.Info("Fullscreen Application detected");
                        PlayRingtone();

                        Task.Run(() =>
                        {
                            Thread.Sleep(5000);
                            StopRingtone();
                        });
                        remainingSeconds = alertAgainInterval;
                    }
                    else
                    {
                        logger.Info("Alert interval time up!");
                        StartRest();
                    }
                }
            }
            else if (status == SHOWING)
            {
                showingTime += 1;

                if (showingTime == minimumRestTime)
                {
                    logger.Info("User has rested enough, waiting for operation");
                    // text modify
                    TipLabel.Text = lang.GetString("REST_COMPLETE");
                }
            }
        }

        private void PlayRingtone()
        {
            // Ringtone
            if (hasRingtonePath)
            {
                try
                {
                    logger.Info("Playing ringtone");
                    waveOutDevice.Init(audioFileReader);
                    waveOutDevice.Play();
                }
                catch (Exception e)
                {
                    logger.Error("Unable to start ringtone");
                    logger.Error(e);
                }
            }
        }

        private void StopRingtone()
        {
            if (hasRingtonePath)
            {
                logger.Info("Stopping ringtone");
                try
                {
                    if (waveOutDevice.PlaybackState == PlaybackState.Playing)
                    {
                        waveOutDevice.Stop();
                    }
                }
                catch (Exception e)
                {
                    logger.Error("Exception when stopping ringtone");
                    logger.Error(e);
                }
            }
        }

        private void OnWaveOutDeviceStopped(object sender, EventArgs e)
        {
            logger.Info("Reseting audio reader");
            try
            {
                audioFileReader.Seek(0, System.IO.SeekOrigin.Begin);
            }
            catch (Exception ex)
            {
                logger.Error("Unable to reset audio reader");
                logger.Error(ex);
            }
        }

        private void StartRest()
        {
            if (status != HIDING)
                return;

            sc.Stop();

            // this.TopMost = false;
            // this.TopMost = true;
            CountdownTimer.Enabled = false;
            logger.Info("Starting alert");
            status = FADE_IN;

            // text modify
            TipLabel.Text = String.Format(lang.GetString("REST_TIP"), userOperatingTime / 60);
            //UpdateClock();
            UpdateTimer.Enabled = true;
            Task.Run(() =>
            {

                PlayRingtone();
            });
             //SetForegroundWindow(this.Handle);
        }

        #endregion Core Function

        // Mouse penetrate and no focus

        #region Window Advanced Settings

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var style = GetWindowLong(this.Handle, GWL_EXSTYLE);
            SetWindowLong(this.Handle, GWL_EXSTYLE, style | WS_EX_LAYERED | WS_EX_TRANSPARENT | WS_EX_NOACTIVATE);
        }

        private void AlertForm_Shown(object sender, EventArgs e)
        {
            SetForegroundWindow(preForeHwnd);
        }

        #endregion Window Advanced Settings

        private void UpdateLayout()
        {
            ClockLabel.Top = (int)((this.Height - ClockLabel.Height) / 2 - ClockLabel.Height * 0.25);
            ClockLabel.Left = (this.Width - ClockLabel.Width) / 2;

            TipLabel.Top = (int)(ClockLabel.Bottom + TipLabel.Height * 0.05);
            TipLabel.Left = (this.Width - TipLabel.Width) / 2;
        }

        private void UpdateClock()
        {
            DateTime time = DateTime.Now;
            string timeText = time.ToString("HH:mm:ss");

            ClockLabel.Text = timeText;

            UpdateLayout();
        }

        #region Notify Icon

        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (sender == NotifyIcon)   // prevent menu click event
            {
                if (e.Button == MouseButtons.Left)
                {
                    logger.Info("User start to rest manually");
                    manuallyRest = true;
                    StartRest();
                }
            }
        }

        #endregion Notify Icon

        #region Notify Menu Methods

        private void NotifyMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int i = 0;
            for (; i < NotifyMenu.Items.Count; i++)
            {
                if (NotifyMenu.Items[i] == e.ClickedItem)
                    break;
            }

            switch (i)
            {
                // TODO
                case 0:     // Rest now
                    manuallyRest = true;
                    StartRest();
                    break;

                case 1:     // Setting
                    SettingForm settingForm = new SettingForm();
                    var result = settingForm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        UpdateConfig(settingForm.NewConfig);
                    }
                    break;

                case 2:     // About
                    Process.Start("https://github.com/SDchao/Time2Rest/blob/main/README.md");
                    break;

                case 3:     // Exit
                    Application.Exit();
                    break;
            }
            NotifyMenu.Close();
        }

        #endregion Notify Menu Methods
    }
}
