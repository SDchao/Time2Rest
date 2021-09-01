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
    public partial class Form1 : Form
    {
        KeyboardHook keyboardHook = new KeyboardHook();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            keyboardHook.StartHook();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            keyboardHook.StopHook();
        }
    }
}
