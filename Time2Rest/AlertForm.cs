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

namespace Time2Rest
{
    public partial class AlertForm : Form
    {
        DefaultHook DefaultHook = new DefaultHook();
        public AlertForm()
        {
            InitializeComponent();
            //DefaultHook.OnOperation += OnUserOperation;
            this.Opacity = 0.0;

            this.Left = 0;
            this.Top = 0;

            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;
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
            this.Opacity = 1.0;
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0.0)
            {
                this.Opacity -= 0.01;
            }
        }
    }
}
