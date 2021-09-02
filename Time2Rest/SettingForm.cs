using System.Windows.Forms;

using Time2Rest.Languages;
namespace Time2Rest
{
    public partial class SettingForm : Form
    {
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


            // TODO Timer tab
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

        }
    }
}
