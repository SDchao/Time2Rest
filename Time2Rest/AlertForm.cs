using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Time2Rest.Hooks;
using NLog;

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
        int alertInterval = 10;
        int minimumRestTime = 3;
        int alertAgainInterval = 3;
        double maxOpacity = 0.6;

        public AlertForm()
        {
            InitializeComponent();
            DefaultHook.OnOperation += OnUserOperation;

            // Init
            this.Opacity = 0.0;

            this.Left = 0;
            this.Top = 0;

            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;

            this.UpdateTimer.Enabled = false;

            // TODO: READ CONFIG

            // Init after config reading
            remainingSeconds = alertInterval;
            CountdownTimer.Enabled = true;

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
                        alertAgainCount += 1;
                        remainingSeconds = alertAgainInterval;

                        // TODO text modify here
                    }
                    else
                    {
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
                this.Opacity += 0.01;
                if (this.Opacity >= maxOpacity)
                {
                    showingTime = 0;
                    status = SHOWING;
                    UpdateTimer.Enabled = false;
                    CountdownTimer.Enabled = true;
                }
            }
            else if (status == FADE_OUT)
            {
                this.Opacity -= 0.05;
                if (this.Opacity <= 0)
                {
                    this.Opacity = 0.0;
                    status = HIDING;
                    UpdateTimer.Enabled = false;
                    CountdownTimer.Enabled = true;
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
                    remainingSeconds = alertInterval;
                    CountdownTimer.Enabled = false;
                }

                logger.Debug("Time remain: {0}", remainingSeconds);
                logger.Debug("Time before stop the timer: {0}", remainingSpareSeconds);
                if (remainingSeconds <= 0)
                {
                    status = FADE_IN;
                    CountdownTimer.Enabled = false;
                    UpdateTimer.Enabled = true;

                    // TODO text modify here
                }
            }
            else if (status == SHOWING)
            {
                showingTime += 1;
            }
        }
    }
}
