
namespace Time2Rest
{
    partial class SettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.Button_Confirm = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.TabPage_Timer = new System.Windows.Forms.TabPage();
            this.TableLayout_Timer = new System.Windows.Forms.TableLayoutPanel();
            this.TextBox_MinRest = new System.Windows.Forms.NumericUpDown();
            this.TextBox_LaterInterval = new System.Windows.Forms.NumericUpDown();
            this.Label_Sec = new System.Windows.Forms.Label();
            this.Label_Min2 = new System.Windows.Forms.Label();
            this.Label_Min1 = new System.Windows.Forms.Label();
            this.Label_ReminderInterval = new System.Windows.Forms.Label();
            this.Label_RemindLater = new System.Windows.Forms.Label();
            this.Label_MinRestTime = new System.Windows.Forms.Label();
            this.TextBox_Interval = new System.Windows.Forms.NumericUpDown();
            this.TabPage_Visual = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_MaxOpacity = new System.Windows.Forms.Label();
            this.TextBox_Opacity = new System.Windows.Forms.NumericUpDown();
            this.Label_Percentage = new System.Windows.Forms.Label();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_Screen = new System.Windows.Forms.Label();
            this.ComboBox_Screen = new System.Windows.Forms.ComboBox();
            this.CheckBox_Hide = new System.Windows.Forms.CheckBox();
            this.TableLayout_Visual = new System.Windows.Forms.TableLayoutPanel();
            this.Label_ForeColor = new System.Windows.Forms.Label();
            this.Label_BackColor = new System.Windows.Forms.Label();
            this.Label_BackImg = new System.Windows.Forms.Label();
            this.TextBox_Img = new System.Windows.Forms.TextBox();
            this.PictureBox_Fore = new System.Windows.Forms.PictureBox();
            this.PictureBox_Back = new System.Windows.Forms.PictureBox();
            this.Button_Select = new System.Windows.Forms.Button();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.TabPage_Others = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_Sound = new System.Windows.Forms.Label();
            this.TextBox_Sound = new System.Windows.Forms.TextBox();
            this.Button_SelectSound = new System.Windows.Forms.Button();
            this.CheckBox_StartUp = new System.Windows.Forms.CheckBox();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog_Ringtone = new System.Windows.Forms.OpenFileDialog();
            this.Button_Reset = new System.Windows.Forms.Button();
            this.TabPage_Timer.SuspendLayout();
            this.TableLayout_Timer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox_MinRest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox_LaterInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox_Interval)).BeginInit();
            this.TabPage_Visual.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox_Opacity)).BeginInit();
            this.flowLayoutPanel3.SuspendLayout();
            this.TableLayout_Visual.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Fore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Back)).BeginInit();
            this.TabControl.SuspendLayout();
            this.TabPage_Others.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button_Confirm
            // 
            this.Button_Confirm.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button_Confirm.Location = new System.Drawing.Point(318, 231);
            this.Button_Confirm.Name = "Button_Confirm";
            this.Button_Confirm.Size = new System.Drawing.Size(77, 27);
            this.Button_Confirm.TabIndex = 0;
            this.Button_Confirm.Text = "BTN_CONFIRM";
            this.Button_Confirm.UseVisualStyleBackColor = true;
            this.Button_Confirm.Click += new System.EventHandler(this.Button_Confirm_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button_Cancel.Location = new System.Drawing.Point(401, 231);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(77, 27);
            this.Button_Cancel.TabIndex = 2;
            this.Button_Cancel.Text = "BTN_CANCEL";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // TabPage_Timer
            // 
            this.TabPage_Timer.Controls.Add(this.TableLayout_Timer);
            this.TabPage_Timer.Location = new System.Drawing.Point(4, 26);
            this.TabPage_Timer.Name = "TabPage_Timer";
            this.TabPage_Timer.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_Timer.Size = new System.Drawing.Size(469, 190);
            this.TabPage_Timer.TabIndex = 1;
            this.TabPage_Timer.Text = "TAB_TIMER";
            this.TabPage_Timer.UseVisualStyleBackColor = true;
            // 
            // TableLayout_Timer
            // 
            this.TableLayout_Timer.ColumnCount = 3;
            this.TableLayout_Timer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TableLayout_Timer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.TableLayout_Timer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TableLayout_Timer.Controls.Add(this.TextBox_MinRest, 1, 2);
            this.TableLayout_Timer.Controls.Add(this.TextBox_LaterInterval, 1, 1);
            this.TableLayout_Timer.Controls.Add(this.Label_Sec, 2, 2);
            this.TableLayout_Timer.Controls.Add(this.Label_Min2, 2, 1);
            this.TableLayout_Timer.Controls.Add(this.Label_Min1, 2, 0);
            this.TableLayout_Timer.Controls.Add(this.Label_ReminderInterval, 0, 0);
            this.TableLayout_Timer.Controls.Add(this.Label_RemindLater, 0, 1);
            this.TableLayout_Timer.Controls.Add(this.Label_MinRestTime, 0, 2);
            this.TableLayout_Timer.Controls.Add(this.TextBox_Interval, 1, 0);
            this.TableLayout_Timer.Location = new System.Drawing.Point(6, 6);
            this.TableLayout_Timer.Name = "TableLayout_Timer";
            this.TableLayout_Timer.RowCount = 4;
            this.TableLayout_Timer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableLayout_Timer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableLayout_Timer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableLayout_Timer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayout_Timer.Size = new System.Drawing.Size(466, 178);
            this.TableLayout_Timer.TabIndex = 1;
            // 
            // TextBox_MinRest
            // 
            this.TextBox_MinRest.Location = new System.Drawing.Point(144, 61);
            this.TextBox_MinRest.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.TextBox_MinRest.Name = "TextBox_MinRest";
            this.TextBox_MinRest.Size = new System.Drawing.Size(74, 23);
            this.TextBox_MinRest.TabIndex = 12;
            // 
            // TextBox_LaterInterval
            // 
            this.TextBox_LaterInterval.Location = new System.Drawing.Point(144, 32);
            this.TextBox_LaterInterval.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.TextBox_LaterInterval.Name = "TextBox_LaterInterval";
            this.TextBox_LaterInterval.Size = new System.Drawing.Size(74, 23);
            this.TextBox_LaterInterval.TabIndex = 11;
            // 
            // Label_Sec
            // 
            this.Label_Sec.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Sec.AutoSize = true;
            this.Label_Sec.Location = new System.Drawing.Point(224, 58);
            this.Label_Sec.Name = "Label_Sec";
            this.Label_Sec.Size = new System.Drawing.Size(239, 29);
            this.Label_Sec.TabIndex = 9;
            this.Label_Sec.Text = "seconds";
            this.Label_Sec.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_Min2
            // 
            this.Label_Min2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Min2.AutoSize = true;
            this.Label_Min2.Location = new System.Drawing.Point(224, 29);
            this.Label_Min2.Name = "Label_Min2";
            this.Label_Min2.Size = new System.Drawing.Size(239, 29);
            this.Label_Min2.TabIndex = 8;
            this.Label_Min2.Text = "minutes";
            this.Label_Min2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_Min1
            // 
            this.Label_Min1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Min1.AutoSize = true;
            this.Label_Min1.Location = new System.Drawing.Point(224, 0);
            this.Label_Min1.Name = "Label_Min1";
            this.Label_Min1.Size = new System.Drawing.Size(239, 29);
            this.Label_Min1.TabIndex = 7;
            this.Label_Min1.Text = "minutes";
            this.Label_Min1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_ReminderInterval
            // 
            this.Label_ReminderInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_ReminderInterval.AutoSize = true;
            this.Label_ReminderInterval.Location = new System.Drawing.Point(3, 0);
            this.Label_ReminderInterval.Name = "Label_ReminderInterval";
            this.Label_ReminderInterval.Size = new System.Drawing.Size(135, 29);
            this.Label_ReminderInterval.TabIndex = 1;
            this.Label_ReminderInterval.Text = "Reminder Interval:";
            this.Label_ReminderInterval.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_RemindLater
            // 
            this.Label_RemindLater.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_RemindLater.AutoSize = true;
            this.Label_RemindLater.Location = new System.Drawing.Point(3, 29);
            this.Label_RemindLater.Name = "Label_RemindLater";
            this.Label_RemindLater.Size = new System.Drawing.Size(135, 29);
            this.Label_RemindLater.TabIndex = 2;
            this.Label_RemindLater.Text = "Remind Later Interval:";
            this.Label_RemindLater.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_MinRestTime
            // 
            this.Label_MinRestTime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_MinRestTime.AutoSize = true;
            this.Label_MinRestTime.Location = new System.Drawing.Point(3, 58);
            this.Label_MinRestTime.Name = "Label_MinRestTime";
            this.Label_MinRestTime.Size = new System.Drawing.Size(135, 29);
            this.Label_MinRestTime.TabIndex = 3;
            this.Label_MinRestTime.Text = "Minimum Rest Time: ";
            this.Label_MinRestTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TextBox_Interval
            // 
            this.TextBox_Interval.Location = new System.Drawing.Point(144, 3);
            this.TextBox_Interval.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.TextBox_Interval.Name = "TextBox_Interval";
            this.TextBox_Interval.Size = new System.Drawing.Size(74, 23);
            this.TextBox_Interval.TabIndex = 10;
            // 
            // TabPage_Visual
            // 
            this.TabPage_Visual.Controls.Add(this.flowLayoutPanel1);
            this.TabPage_Visual.Controls.Add(this.flowLayoutPanel3);
            this.TabPage_Visual.Controls.Add(this.CheckBox_Hide);
            this.TabPage_Visual.Controls.Add(this.TableLayout_Visual);
            this.TabPage_Visual.Location = new System.Drawing.Point(4, 26);
            this.TabPage_Visual.Name = "TabPage_Visual";
            this.TabPage_Visual.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_Visual.Size = new System.Drawing.Size(469, 190);
            this.TabPage_Visual.TabIndex = 0;
            this.TabPage_Visual.Text = "TAB_VISUAL";
            this.TabPage_Visual.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.Label_MaxOpacity);
            this.flowLayoutPanel1.Controls.Add(this.TextBox_Opacity);
            this.flowLayoutPanel1.Controls.Add(this.Label_Percentage);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(6, 88);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(451, 33);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // Label_MaxOpacity
            // 
            this.Label_MaxOpacity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_MaxOpacity.AutoSize = true;
            this.Label_MaxOpacity.Location = new System.Drawing.Point(3, 0);
            this.Label_MaxOpacity.Name = "Label_MaxOpacity";
            this.Label_MaxOpacity.Size = new System.Drawing.Size(120, 29);
            this.Label_MaxOpacity.TabIndex = 7;
            this.Label_MaxOpacity.Text = "Maximum Opacity: ";
            this.Label_MaxOpacity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TextBox_Opacity
            // 
            this.TextBox_Opacity.Location = new System.Drawing.Point(129, 3);
            this.TextBox_Opacity.Name = "TextBox_Opacity";
            this.TextBox_Opacity.Size = new System.Drawing.Size(53, 23);
            this.TextBox_Opacity.TabIndex = 11;
            // 
            // Label_Percentage
            // 
            this.Label_Percentage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Percentage.AutoSize = true;
            this.Label_Percentage.Location = new System.Drawing.Point(188, 0);
            this.Label_Percentage.Name = "Label_Percentage";
            this.Label_Percentage.Size = new System.Drawing.Size(74, 29);
            this.Label_Percentage.TabIndex = 10;
            this.Label_Percentage.Text = "percentage";
            this.Label_Percentage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.Label_Screen);
            this.flowLayoutPanel3.Controls.Add(this.ComboBox_Screen);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(5, 120);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(452, 33);
            this.flowLayoutPanel3.TabIndex = 12;
            // 
            // Label_Screen
            // 
            this.Label_Screen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Screen.AutoSize = true;
            this.Label_Screen.Location = new System.Drawing.Point(3, 0);
            this.Label_Screen.Name = "Label_Screen";
            this.Label_Screen.Size = new System.Drawing.Size(54, 31);
            this.Label_Screen.TabIndex = 7;
            this.Label_Screen.Text = "Screen: ";
            this.Label_Screen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ComboBox_Screen
            // 
            this.ComboBox_Screen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Screen.FormattingEnabled = true;
            this.ComboBox_Screen.Location = new System.Drawing.Point(63, 3);
            this.ComboBox_Screen.Name = "ComboBox_Screen";
            this.ComboBox_Screen.Size = new System.Drawing.Size(306, 25);
            this.ComboBox_Screen.TabIndex = 8;
            // 
            // CheckBox_Hide
            // 
            this.CheckBox_Hide.AutoSize = true;
            this.CheckBox_Hide.Location = new System.Drawing.Point(6, 159);
            this.CheckBox_Hide.Name = "CheckBox_Hide";
            this.CheckBox_Hide.Size = new System.Drawing.Size(63, 21);
            this.CheckBox_Hide.TabIndex = 1;
            this.CheckBox_Hide.Text = "Hide...";
            this.CheckBox_Hide.UseVisualStyleBackColor = true;
            // 
            // TableLayout_Visual
            // 
            this.TableLayout_Visual.ColumnCount = 3;
            this.TableLayout_Visual.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TableLayout_Visual.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TableLayout_Visual.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TableLayout_Visual.Controls.Add(this.Label_ForeColor, 0, 0);
            this.TableLayout_Visual.Controls.Add(this.Label_BackColor, 0, 1);
            this.TableLayout_Visual.Controls.Add(this.Label_BackImg, 0, 2);
            this.TableLayout_Visual.Controls.Add(this.TextBox_Img, 1, 2);
            this.TableLayout_Visual.Controls.Add(this.PictureBox_Fore, 1, 0);
            this.TableLayout_Visual.Controls.Add(this.PictureBox_Back, 1, 1);
            this.TableLayout_Visual.Controls.Add(this.Button_Select, 2, 2);
            this.TableLayout_Visual.Location = new System.Drawing.Point(5, 6);
            this.TableLayout_Visual.Margin = new System.Windows.Forms.Padding(5);
            this.TableLayout_Visual.Name = "TableLayout_Visual";
            this.TableLayout_Visual.RowCount = 3;
            this.TableLayout_Visual.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableLayout_Visual.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableLayout_Visual.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.TableLayout_Visual.Size = new System.Drawing.Size(456, 79);
            this.TableLayout_Visual.TabIndex = 0;
            // 
            // Label_ForeColor
            // 
            this.Label_ForeColor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_ForeColor.AutoSize = true;
            this.Label_ForeColor.Location = new System.Drawing.Point(3, 0);
            this.Label_ForeColor.Name = "Label_ForeColor";
            this.Label_ForeColor.Size = new System.Drawing.Size(123, 24);
            this.Label_ForeColor.TabIndex = 0;
            this.Label_ForeColor.Text = "Foreground Color: ";
            this.Label_ForeColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_BackColor
            // 
            this.Label_BackColor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_BackColor.AutoSize = true;
            this.Label_BackColor.Location = new System.Drawing.Point(3, 24);
            this.Label_BackColor.Name = "Label_BackColor";
            this.Label_BackColor.Size = new System.Drawing.Size(123, 24);
            this.Label_BackColor.TabIndex = 1;
            this.Label_BackColor.Text = "Background Color:";
            this.Label_BackColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_BackImg
            // 
            this.Label_BackImg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_BackImg.AutoSize = true;
            this.Label_BackImg.Location = new System.Drawing.Point(3, 48);
            this.Label_BackImg.Name = "Label_BackImg";
            this.Label_BackImg.Size = new System.Drawing.Size(123, 31);
            this.Label_BackImg.TabIndex = 2;
            this.Label_BackImg.Text = "Background Image:";
            this.Label_BackImg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TextBox_Img
            // 
            this.TextBox_Img.AllowDrop = true;
            this.TextBox_Img.Location = new System.Drawing.Point(132, 51);
            this.TextBox_Img.Name = "TextBox_Img";
            this.TextBox_Img.Size = new System.Drawing.Size(237, 23);
            this.TextBox_Img.TabIndex = 3;
            this.TextBox_Img.Leave += new System.EventHandler(this.TextBox_Img_LostFocus);
            // 
            // PictureBox_Fore
            // 
            this.PictureBox_Fore.BackColor = System.Drawing.Color.White;
            this.PictureBox_Fore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureBox_Fore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureBox_Fore.Location = new System.Drawing.Point(132, 3);
            this.PictureBox_Fore.Name = "PictureBox_Fore";
            this.PictureBox_Fore.Size = new System.Drawing.Size(35, 18);
            this.PictureBox_Fore.TabIndex = 5;
            this.PictureBox_Fore.TabStop = false;
            this.PictureBox_Fore.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseClick);
            // 
            // PictureBox_Back
            // 
            this.PictureBox_Back.BackColor = System.Drawing.Color.Black;
            this.PictureBox_Back.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureBox_Back.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureBox_Back.Location = new System.Drawing.Point(132, 27);
            this.PictureBox_Back.Name = "PictureBox_Back";
            this.PictureBox_Back.Size = new System.Drawing.Size(35, 18);
            this.PictureBox_Back.TabIndex = 6;
            this.PictureBox_Back.TabStop = false;
            this.PictureBox_Back.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseClick);
            // 
            // Button_Select
            // 
            this.Button_Select.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Button_Select.Location = new System.Drawing.Point(375, 51);
            this.Button_Select.Name = "Button_Select";
            this.Button_Select.Size = new System.Drawing.Size(69, 25);
            this.Button_Select.TabIndex = 4;
            this.Button_Select.Text = "Select...";
            this.Button_Select.UseVisualStyleBackColor = true;
            this.Button_Select.Click += new System.EventHandler(this.Button_Select_Click);
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.TabPage_Visual);
            this.TabControl.Controls.Add(this.TabPage_Timer);
            this.TabControl.Controls.Add(this.TabPage_Others);
            this.TabControl.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TabControl.Location = new System.Drawing.Point(5, 5);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(477, 220);
            this.TabControl.TabIndex = 0;
            // 
            // TabPage_Others
            // 
            this.TabPage_Others.Controls.Add(this.flowLayoutPanel2);
            this.TabPage_Others.Controls.Add(this.CheckBox_StartUp);
            this.TabPage_Others.Location = new System.Drawing.Point(4, 26);
            this.TabPage_Others.Name = "TabPage_Others";
            this.TabPage_Others.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_Others.Size = new System.Drawing.Size(469, 190);
            this.TabPage_Others.TabIndex = 2;
            this.TabPage_Others.Text = "TAB_OTHERS";
            this.TabPage_Others.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.Label_Sound);
            this.flowLayoutPanel2.Controls.Add(this.TextBox_Sound);
            this.flowLayoutPanel2.Controls.Add(this.Button_SelectSound);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(6, 6);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(457, 32);
            this.flowLayoutPanel2.TabIndex = 8;
            // 
            // Label_Sound
            // 
            this.Label_Sound.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Sound.AutoSize = true;
            this.Label_Sound.Location = new System.Drawing.Point(3, 0);
            this.Label_Sound.Name = "Label_Sound";
            this.Label_Sound.Size = new System.Drawing.Size(67, 29);
            this.Label_Sound.TabIndex = 5;
            this.Label_Sound.Text = "Ringtone: ";
            this.Label_Sound.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TextBox_Sound
            // 
            this.TextBox_Sound.AllowDrop = true;
            this.TextBox_Sound.Location = new System.Drawing.Point(76, 3);
            this.TextBox_Sound.Name = "TextBox_Sound";
            this.TextBox_Sound.Size = new System.Drawing.Size(293, 23);
            this.TextBox_Sound.TabIndex = 6;
            this.TextBox_Sound.Leave += new System.EventHandler(this.TextBox_Sound_Leave);
            // 
            // Button_SelectSound
            // 
            this.Button_SelectSound.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Button_SelectSound.Location = new System.Drawing.Point(375, 3);
            this.Button_SelectSound.Name = "Button_SelectSound";
            this.Button_SelectSound.Size = new System.Drawing.Size(69, 23);
            this.Button_SelectSound.TabIndex = 7;
            this.Button_SelectSound.Text = "Select...";
            this.Button_SelectSound.UseVisualStyleBackColor = true;
            this.Button_SelectSound.Click += new System.EventHandler(this.Button_SelectSound_Click);
            // 
            // CheckBox_StartUp
            // 
            this.CheckBox_StartUp.AutoSize = true;
            this.CheckBox_StartUp.Location = new System.Drawing.Point(6, 44);
            this.CheckBox_StartUp.Name = "CheckBox_StartUp";
            this.CheckBox_StartUp.Size = new System.Drawing.Size(77, 21);
            this.CheckBox_StartUp.TabIndex = 0;
            this.CheckBox_StartUp.Text = "startup...";
            this.CheckBox_StartUp.UseVisualStyleBackColor = true;
            // 
            // colorDialog
            // 
            this.colorDialog.AnyColor = true;
            this.colorDialog.FullOpen = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Image files (*.jpg, *.png) | *.jpg; *.png";
            // 
            // openFileDialog_Ringtone
            // 
            this.openFileDialog_Ringtone.Filter = "Sound Files (*.mp3, *.wav)|*.mp3; *.wav";
            // 
            // Button_Reset
            // 
            this.Button_Reset.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button_Reset.Location = new System.Drawing.Point(9, 231);
            this.Button_Reset.Name = "Button_Reset";
            this.Button_Reset.Size = new System.Drawing.Size(123, 27);
            this.Button_Reset.TabIndex = 3;
            this.Button_Reset.Text = "BTN_RESET";
            this.Button_Reset.UseVisualStyleBackColor = true;
            this.Button_Reset.Click += new System.EventHandler(this.Button_Reset_Click);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 266);
            this.Controls.Add(this.Button_Reset);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_Confirm);
            this.Controls.Add(this.TabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SettingForm";
            this.TopMost = true;
            this.TabPage_Timer.ResumeLayout(false);
            this.TableLayout_Timer.ResumeLayout(false);
            this.TableLayout_Timer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox_MinRest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox_LaterInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox_Interval)).EndInit();
            this.TabPage_Visual.ResumeLayout(false);
            this.TabPage_Visual.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox_Opacity)).EndInit();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.TableLayout_Visual.ResumeLayout(false);
            this.TableLayout_Visual.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Fore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Back)).EndInit();
            this.TabControl.ResumeLayout(false);
            this.TabPage_Others.ResumeLayout(false);
            this.TabPage_Others.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button Button_Confirm;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.TabPage TabPage_Timer;
        private System.Windows.Forms.TableLayoutPanel TableLayout_Timer;
        private System.Windows.Forms.TabPage TabPage_Visual;
        private System.Windows.Forms.TableLayoutPanel TableLayout_Visual;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.Label Label_ForeColor;
        private System.Windows.Forms.Label Label_BackColor;
        private System.Windows.Forms.Label Label_ReminderInterval;
        private System.Windows.Forms.Label Label_RemindLater;
        private System.Windows.Forms.Label Label_MinRestTime;
        private System.Windows.Forms.Label Label_Min1;
        private System.Windows.Forms.Label Label_Sec;
        private System.Windows.Forms.Label Label_Min2;
        private System.Windows.Forms.PictureBox PictureBox_Fore;
        private System.Windows.Forms.PictureBox PictureBox_Back;
        private System.Windows.Forms.Label Label_MaxOpacity;
        private System.Windows.Forms.Label Label_Percentage;
        private System.Windows.Forms.Label Label_BackImg;
        private System.Windows.Forms.TextBox TextBox_Img;
        private System.Windows.Forms.Button Button_Select;
        private System.Windows.Forms.CheckBox CheckBox_Hide;
        private System.Windows.Forms.NumericUpDown TextBox_Opacity;
        private System.Windows.Forms.NumericUpDown TextBox_MinRest;
        private System.Windows.Forms.NumericUpDown TextBox_LaterInterval;
        private System.Windows.Forms.NumericUpDown TextBox_Interval;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TabPage TabPage_Others;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox CheckBox_StartUp;
        private System.Windows.Forms.OpenFileDialog openFileDialog_Ringtone;
        private System.Windows.Forms.Label Label_Sound;
        private System.Windows.Forms.TextBox TextBox_Sound;
        private System.Windows.Forms.Button Button_SelectSound;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button Button_Reset;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Label Label_Screen;
        private System.Windows.Forms.ComboBox ComboBox_Screen;
    }
}