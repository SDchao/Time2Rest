using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Time2Rest.WinInteractors;
using Time2Rest.Config;
using Time2Rest.Languages;
using NLog;
using System.Runtime.InteropServices;
using NAudio;
using NAudio.Wave;

namespace Time2Rest
{
    public partial class AlertForm : Form
    {
        #region variables
        const int HIDING = -1;
        const int FADE_IN = 1;
        const int SHOWING = 2;
        const int FADE_OUT = 3;

        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        // WinAPI
        DefaultHook DefaultHook = new DefaultHook();

        // Display
        int status = HIDING;

        // Time counter
        int remainingSeconds = -1;
        readonly object countdownLock = new object();
        readonly int maxSpareSeconds = 5 * 60;
        int remainingSpareSeconds;
        int showingTime = 0;
        int userOperatingTime = 0;

        // Config
        // Function Config
        int alertInterval;
        int minimumRestTime;
        int alertAgainInterval;
        bool hideWhenFullScreen;
        bool hasRingtonePath;
        string ringtonePath;

        // UI Config
        double maxOpacity;
        Color userBackColor;
        Color userForeColor;
        string backGroundImgPath;

        // Lang
        ResXResourceSet lang;

        // Ringtone
        IWavePlayer waveOutDevice = new WaveOut();
        AudioFileReader audioFileReader;

        #endregion

        public AlertForm()
        {
            InitializeComponent();
            DefaultHook.OnOperation += OnUserOperation;

            // Lang
            lang = LanguageManager.GetLangRes();

            // Menu Lang
            NotifyMenu.Items.Add(lang.GetString("MENU_REST"), Resource.PNG_REST);
            NotifyMenu.Items.Add(lang.GetString("MENU_SETTING"), Resource.PNG_SETTING);
            NotifyMenu.Items.Add(lang.GetString("MENU_ABOUT"), Resource.PNG_ABOUT);
            NotifyMenu.Items.Add(lang.GetString("MENU_EXIT"), Resource.PNG_EXIT);

            NotifyMenu.ItemClicked += NotifyMenu_ItemClicked;

            // Init
            this.TopMost = true;
            this.Opacity = 0.0;

            this.Left = 0;
            this.Top = 0;

            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;

            this.UpdateTimer.Enabled = false;

            // Components Adjust
            float clockHeight = this.Height * 0.2f;
            ClockLabel.Font = new Font(ClockLabel.Font.Name, clockHeight * 0.75f);

            float tipHeight = clockHeight * 0.15f;
            TipLabel.Font = new Font(TipLabel.Font.Name, tipHeight * 0.75f);

            UpdateLayout();

            // READ CONFIG
            UpdateConfig();

        }

        #region Config Reading
        private void UpdateConfig()
        {
            T2rConfig config = T2rConfigManager.ReadConfig();
            UpdateConfig(config);
        }

        private void UpdateConfig(T2rConfig config)
        {
            logger.Debug("Applying config");
            alertInterval = config.alertInterval;
            minimumRestTime = config.minimumRestTime;
            alertAgainInterval = config.alertAgainInterval;
            hideWhenFullScreen = config.hideWhenFullscreen;
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

            ClockLabel.ForeColor = userForeColor;
            TipLabel.ForeColor = userForeColor;

            // Init after config reading
            remainingSeconds = alertInterval;
            remainingSpareSeconds = maxSpareSeconds;
            CountdownTimer.Enabled = true;

            // Final step
            logger.Info("Init completed");
        }
        #endregion

        #region Core Function
        private void AlertForm_Load(object sender, EventArgs e)
        {
            DefaultHook.StartHook();
        }

        private void AlertForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DefaultHook.StopHook();
            waveOutDevice.Dispose();
        }

        private void OnUserOperation()
        {
            if (status == HIDING)
            {
                // User came back or just using the computer
                lock (countdownLock)
                {
                    remainingSpareSeconds = maxSpareSeconds;
                    if (!CountdownTimer.Enabled)
                        CountdownTimer.Enabled = true;
                }
            }
            else if (status == SHOWING)
            {
                CountdownTimer.Enabled = false;

                lock (countdownLock)
                {
                    // User operated after showing the alert
                    if (showingTime < minimumRestTime)
                    {
                        // User didnt rest enough
                        logger.Info("Inisting computer usage, alert later");
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

                // Stop Ringtone
                if (waveOutDevice.PlaybackState == PlaybackState.Playing)
                {
                    waveOutDevice.Stop();
                    audioFileReader.Dispose();
                }
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
                    this.Hide();
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
                    CountdownTimer.Enabled = false;
                }

                logger.Debug("Time remain: {0}", remainingSeconds);
                logger.Debug("Time before stop the timer: {0}", remainingSpareSeconds);
                logger.Debug("User operating time: {0}", userOperatingTime);
                if (remainingSeconds <= 0)
                {
                    if (FullscreenDetector.IsForegroundFullScreen() && hideWhenFullScreen)
                    {
                        if (remainingSeconds == 0)
                            logger.Info("Fullscreen Application detected");
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

        private void StartRest()
        {
            if (status != HIDING)
                return;

            CountdownTimer.Enabled = false;
            logger.Info("Starting alert");
            status = FADE_IN;
            UpdateTimer.Enabled = true;

            // text modify
            TipLabel.Text = String.Format(lang.GetString("REST_TIP"), userOperatingTime / 60);
            UpdateClock();

            // Ringtone
            if (hasRingtonePath)
            {
                audioFileReader = new AudioFileReader(ringtonePath);
                waveOutDevice.Init(audioFileReader);
                waveOutDevice.Play();
            }

            this.Show();
        }

        #endregion

        // Mouse penetrate and no focus
        #region Mouse passthrough
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        const int GWL_EXSTYLE = -20;
        const int WS_EX_LAYERED = 0x80000;
        const int WS_EX_TRANSPARENT = 0x20;
        const int WS_EX_NOACTIVATE = 0x08000000;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var style = GetWindowLong(this.Handle, GWL_EXSTYLE);
            SetWindowLong(this.Handle, GWL_EXSTYLE, style | WS_EX_LAYERED | WS_EX_TRANSPARENT | WS_EX_NOACTIVATE);
        }

        #endregion
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

        private void AlertForm_Shown(object sender, EventArgs e)
        {
            this.Hide();
        }



        #region Notify Icon

        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (sender == NotifyIcon)   // prevent menu click event
            {
                if (e.Button == MouseButtons.Left)
                {
                    StartRest();
                }
            }
        }

        #endregion

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
                case 2:     // About TODO

                    break;
                case 3:     // Exit
                    Application.Exit();
                    break;
            }
            NotifyMenu.Close();
        }

        #endregion

    }
}
