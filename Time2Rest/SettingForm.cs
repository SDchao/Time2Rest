using System.Windows.Forms;

using Time2Rest.Languages;
using Time2Rest.Config;
using System;
using System.Drawing;

namespace Time2Rest
{
    public partial class SettingForm : Form
    {
        public T2rConfig NewConfig { get; set; }
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public SettingForm()
        {
            InitializeComponent();

            
            // lang
            var lang = LanguageManager.GetLangRes();

            // Visual tab
            TabPage_Visual.Text = lang.GetString("ST_TAB_VISUAL");
            Label_BackColor.Text = lang.GetString("ST_LB_BACKCOLOR");
            Label_ForeColor.Text = lang.GetString("ST_LB_FORECOLOR");
            Label_BackImg.Text = lang.GetString("ST_LB_BACK_IMG");
            Button_Select.Text = lang.GetString("ST_BTN_SELECT");
            Label_MaxOpacity.Text = lang.GetString("ST_LB_MAX_OPACITY");
            Label_Percentage.Text = lang.GetString("ST_LB_PERCENTAGE");
            CheckBox_Hide.Text = lang.GetString("ST_CB_HIDE_WHEN_FULLSCREEN");

            // Timer tab
            TabPage_Timer.Text = lang.GetString("ST_TAB_TIMER");
            Label_ReminderInterval.Text = lang.GetString("ST_LB_REMIND_INTERVAL");
            Label_RemindLater.Text = lang.GetString("ST_LB_REMIND_LATER");
            Label_MinRestTime.Text = lang.GetString("ST_LB_MIN_REST_TIME");
            Label_Min1.Text = lang.GetString("ST_LB_MIN");
            Label_Min2.Text = lang.GetString("ST_LB_MIN");
            Label_Sec.Text = lang.GetString("ST_LB_SEC");

            // Buttons
            Button_Confirm.Text = lang.GetString("ST_BTN_CONFIRM");
            Button_Cancel.Text = lang.GetString("ST_BTN_CANCEL");


            // Config loading
            var config = T2rConfigManager.ReadConfig();

            PictureBox_Back.BackColor = config.GetBackColor();
            PictureBox_Fore.BackColor = config.GetForeColor();
            TextBox_Img.Text = config.backGroundImgPath;
            TextBox_Opacity.Value = (int)Math.Round(config.maxOpacity * 100);
            CheckBox_Hide.Checked = config.hideWhenFullscreen;

            TextBox_Interval.Value = (config.alertInterval / 60);
            TextBox_LaterInterval.Value = (config.alertAgainInterval / 60);
            TextBox_MinRest.Text = config.minimumRestTime.ToString();

            // Final
            openFileDialog.FileName = "";
        }
        #region click events
        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Button_Confirm_Click(object sender, EventArgs e)
        {
            var newConfig = new T2rConfig();
            logger.Info("Collecting config in settings form");
            newConfig.backColorString = ColorTranslator.ToHtml(PictureBox_Back.BackColor);
            newConfig.foreColorString = ColorTranslator.ToHtml(PictureBox_Fore.BackColor);
            newConfig.backGroundImgPath = TextBox_Img.Text;
            newConfig.maxOpacity = (double)(TextBox_Opacity.Value / 100);
            newConfig.hideWhenFullscreen = CheckBox_Hide.Checked;
            newConfig.alertInterval = (int)TextBox_Interval.Value * 60;
            newConfig.alertAgainInterval = (int)TextBox_LaterInterval.Value * 60;
            newConfig.minimumRestTime = (int)TextBox_MinRest.Value;

            logger.Info("Writing config");
            T2rConfigManager.WriteConfig(newConfig);
            this.NewConfig = newConfig;
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            var result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                var color = colorDialog.Color;
                var PictureBox = (PictureBox)sender;
                PictureBox.BackColor = color;
            }
        }
        private void Button_Select_Click(object sender, EventArgs e)
        {
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                TextBox_Img.Text = openFileDialog.FileName;
            }
        }

        private void TextBox_Img_LostFocus(object sender, EventArgs e)
        {
            var path = TextBox_Img.Text;
            if (String.IsNullOrEmpty(path))
                return;
            if (System.IO.File.Exists(path))
            {
                foreach (string ext in new string[] {".jpg", ".png"})
                {
                    if (path.EndsWith(ext))
                        return;
                }
            }

            TextBox_Img.Text = "";
            var lang = LanguageManager.GetLangRes();
            MessageBox.Show(lang.GetString("ERR_NOT_IMG_FILE"), lang.GetString("ERR"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion
    }
}
