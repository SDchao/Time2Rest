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

using Time2Rest.Hooks;
using Time2Rest.Config;
using Time2Rest.Languages;
using NLog;
using System.Runtime.InteropServices;

namespace Time2Rest
{
    public partial class AlertForm : Form
    {
        const int HIDING = -1;
        const int FADE_IN = 1;
        const int SHOWING = 2;
        const int FADE_OUT = 3;

        private readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        // Hook
        DefaultHook DefaultHook = new DefaultHook();

        // Display
        int status = HIDING;
        int remainingSeconds = -1;
        object countdownLock = new object();
        int maxSpareSeconds = 5;
        int remainingSpareSeconds = 5;
        int showingTime = 0;
        int alertAgainCount = 0;

        // Config
        // Function Config
        int alertInterval = 1;
        int minimumRestTime = 3;
        int alertAgainInterval = 3;

        // UI Config
        double maxOpacity = 0.8;
        Color userBackColor = Color.Black;
        Color userForeColor = Color.White;
        String backGroundImgPath = "";

        // Lang
        ResXResourceSet lang;

        public AlertForm()
        {
            InitializeComponent();
            DefaultHook.OnOperation += OnUserOperation;

            // Lang
            lang = LanguageManager.GetLangRes();

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
            logger.Debug("Start config reading");
            T2rConfig config = T2rConfigManager.ReadConfig();
            alertAgainCount = config.alertAgainInterval;
            minimumRestTime = config.minimumRestTime;
            alertAgainInterval = config.alertAgainInterval;

            maxOpacity = config.maxOpacity;
            userBackColor = config.GetBackColor();
            userForeColor = config.GetForeColor();
            backGroundImgPath = config.backGroundImgPath;

            logger.Info("Config reading completed");
            this.BackColor = userBackColor;
            if (!String.IsNullOrEmpty(backGroundImgPath))
                this.BackgroundImage = Image.FromFile(backGroundImgPath);

            ClockLabel.ForeColor = userForeColor;

            // Init after config reading
            remainingSeconds = alertInterval;
            CountdownTimer.Enabled = true;

            // Final step
            logger.Info("Init completed");
        }

        private void AlertForm_Load(object sender, EventArgs e)
        {
            DefaultHook.StartHook();
        }

        private void AlertForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DefaultHook.StopHook();
        }

        private void OnUserOperation()
        {
            // TODO
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
                        alertAgainCount += 1;
                        remainingSeconds = alertAgainInterval;

                        // text modify
                        TipLabel.Text = String.Format(lang.GetString("REST_INCOMPLETE"), Math.Floor(alertAgainInterval / 60.0) + 1);
                    }
                    else
                    {
                        logger.Info("Rest done, reseting alert");
                        alertAgainCount = 0;
                        remainingSeconds = alertInterval;
                    }
                }

                status = FADE_OUT;
                UpdateTimer.Enabled = true;
            }
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (status == FADE_IN)
            {
                UpdateClock();
                this.Opacity += 0.005;
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
                this.Opacity -= 0.02;
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
                if (remainingSeconds <= 0)
                {
                    logger.Info("Time up! Starting alert");
                    status = FADE_IN;
                    CountdownTimer.Enabled = false;
                    UpdateTimer.Enabled = true;

                    // text modify
                    TipLabel.Text = String.Format(lang.GetString("REST_TIP"), Math.Floor(alertInterval / 60.0 + alertAgainInterval * alertAgainCount / 60.0) + 1);
                    UpdateClock();

                    this.Show();
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

        // Mouse penetrate and no focus
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
    }
}
