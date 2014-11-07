namespace Tahorg.RCJoyGUI.Dialogs
{
    partial class SetProjectSettingsDialog
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbTimer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbPPMEnabled = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbLCDSerial = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbLCDEnabled = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbDebugSerial = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbGenerateJoqQuery = new System.Windows.Forms.CheckBox();
            this.cbGenerateDebug = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnSelectDir = new System.Windows.Forms.Button();
            this.tbOutPath = new System.Windows.Forms.TextBox();
            this.fsdOutPath = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cbXBeeSerial = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbXbeeEnabled = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cbTimer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbPPMEnabled);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(262, 81);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PPM Output";
            // 
            // cbTimer
            // 
            this.cbTimer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTimer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbTimer.FormattingEnabled = true;
            this.cbTimer.Items.AddRange(new object[] {
            "Timer 1A (pin 11)",
            "Timer 3A (pin 5)",
            "Timer 4A (pin 6)",
            "Timer 5A (pin 46)"});
            this.cbTimer.Location = new System.Drawing.Point(107, 47);
            this.cbTimer.Name = "cbTimer";
            this.cbTimer.Size = new System.Drawing.Size(145, 21);
            this.cbTimer.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "PPM Timer";
            // 
            // cbPPMEnabled
            // 
            this.cbPPMEnabled.AutoSize = true;
            this.cbPPMEnabled.Checked = true;
            this.cbPPMEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPPMEnabled.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbPPMEnabled.Location = new System.Drawing.Point(18, 24);
            this.cbPPMEnabled.Name = "cbPPMEnabled";
            this.cbPPMEnabled.Size = new System.Drawing.Size(138, 17);
            this.cbPPMEnabled.TabIndex = 0;
            this.cbPPMEnabled.Text = "PPM Generator enabled";
            this.cbPPMEnabled.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cbLCDSerial);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cbLCDEnabled);
            this.groupBox2.Location = new System.Drawing.Point(12, 100);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(262, 85);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "LCD Display";
            // 
            // cbLCDSerial
            // 
            this.cbLCDSerial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLCDSerial.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbLCDSerial.FormattingEnabled = true;
            this.cbLCDSerial.Items.AddRange(new object[] {
            "Serial 0 (pin 0)",
            "Serial 1 (pin 18)",
            "Serial 2 (pin 16)",
            "Serial 3 (pin 14)"});
            this.cbLCDSerial.Location = new System.Drawing.Point(107, 52);
            this.cbLCDSerial.Name = "cbLCDSerial";
            this.cbLCDSerial.Size = new System.Drawing.Size(145, 21);
            this.cbLCDSerial.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "LCD Serial";
            // 
            // cbLCDEnabled
            // 
            this.cbLCDEnabled.AutoSize = true;
            this.cbLCDEnabled.Checked = true;
            this.cbLCDEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLCDEnabled.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbLCDEnabled.Location = new System.Drawing.Point(18, 29);
            this.cbLCDEnabled.Name = "cbLCDEnabled";
            this.cbLCDEnabled.Size = new System.Drawing.Size(123, 17);
            this.cbLCDEnabled.TabIndex = 3;
            this.cbLCDEnabled.Text = "LCD Screen enabled";
            this.cbLCDEnabled.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.cbDebugSerial);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.cbGenerateJoqQuery);
            this.groupBox3.Controls.Add(this.cbGenerateDebug);
            this.groupBox3.Location = new System.Drawing.Point(12, 191);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(262, 111);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Debug output";
            // 
            // cbDebugSerial
            // 
            this.cbDebugSerial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDebugSerial.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbDebugSerial.FormattingEnabled = true;
            this.cbDebugSerial.Items.AddRange(new object[] {
            "Serial 0 (pin 0)",
            "Serial 1 (pin 18)",
            "Serial 2 (pin 16)",
            "Serial 3 (pin 14)"});
            this.cbDebugSerial.Location = new System.Drawing.Point(107, 76);
            this.cbDebugSerial.Name = "cbDebugSerial";
            this.cbDebugSerial.Size = new System.Drawing.Size(145, 21);
            this.cbDebugSerial.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Debugger Serial";
            // 
            // cbGenerateJoqQuery
            // 
            this.cbGenerateJoqQuery.AutoSize = true;
            this.cbGenerateJoqQuery.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbGenerateJoqQuery.Location = new System.Drawing.Point(18, 52);
            this.cbGenerateJoqQuery.Name = "cbGenerateJoqQuery";
            this.cbGenerateJoqQuery.Size = new System.Drawing.Size(195, 17);
            this.cbGenerateJoqQuery.TabIndex = 2;
            this.cbGenerateJoqQuery.Text = "Generate Joy report descriptor query";
            this.cbGenerateJoqQuery.UseVisualStyleBackColor = true;
            // 
            // cbGenerateDebug
            // 
            this.cbGenerateDebug.AutoSize = true;
            this.cbGenerateDebug.Checked = true;
            this.cbGenerateDebug.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGenerateDebug.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbGenerateDebug.Location = new System.Drawing.Point(18, 29);
            this.cbGenerateDebug.Name = "cbGenerateDebug";
            this.cbGenerateDebug.Size = new System.Drawing.Size(143, 17);
            this.cbGenerateDebug.TabIndex = 1;
            this.cbGenerateDebug.Text = "Generate debugger code";
            this.cbGenerateDebug.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(118, 458);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 102;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(199, 458);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 101;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.btnSelectDir);
            this.groupBox4.Controls.Add(this.tbOutPath);
            this.groupBox4.Location = new System.Drawing.Point(12, 399);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(262, 52);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Output path";
            // 
            // btnSelectDir
            // 
            this.btnSelectDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectDir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelectDir.Location = new System.Drawing.Point(228, 16);
            this.btnSelectDir.Name = "btnSelectDir";
            this.btnSelectDir.Size = new System.Drawing.Size(24, 23);
            this.btnSelectDir.TabIndex = 1;
            this.btnSelectDir.Text = "...";
            this.btnSelectDir.UseVisualStyleBackColor = true;
            this.btnSelectDir.Click += new System.EventHandler(this.btnSelectDir_Click);
            // 
            // tbOutPath
            // 
            this.tbOutPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbOutPath.Location = new System.Drawing.Point(18, 19);
            this.tbOutPath.Name = "tbOutPath";
            this.tbOutPath.Size = new System.Drawing.Size(204, 20);
            this.tbOutPath.TabIndex = 0;
            // 
            // fsdOutPath
            // 
            this.fsdOutPath.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.cbXBeeSerial);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.cbXbeeEnabled);
            this.groupBox5.Location = new System.Drawing.Point(12, 308);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(262, 85);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "XBee retranslator";
            // 
            // cbXBeeSerial
            // 
            this.cbXBeeSerial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbXBeeSerial.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbXBeeSerial.FormattingEnabled = true;
            this.cbXBeeSerial.Items.AddRange(new object[] {
            "Serial 0 (pin 0)",
            "Serial 1 (pin 18)",
            "Serial 2 (pin 16)",
            "Serial 3 (pin 14)"});
            this.cbXBeeSerial.Location = new System.Drawing.Point(107, 52);
            this.cbXBeeSerial.Name = "cbXBeeSerial";
            this.cbXBeeSerial.Size = new System.Drawing.Size(145, 21);
            this.cbXBeeSerial.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "XBee Serial";
            // 
            // cbXbeeEnabled
            // 
            this.cbXbeeEnabled.AutoSize = true;
            this.cbXbeeEnabled.Checked = true;
            this.cbXbeeEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbXbeeEnabled.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbXbeeEnabled.Location = new System.Drawing.Point(18, 29);
            this.cbXbeeEnabled.Name = "cbXbeeEnabled";
            this.cbXbeeEnabled.Size = new System.Drawing.Size(91, 17);
            this.cbXbeeEnabled.TabIndex = 3;
            this.cbXbeeEnabled.Text = "XBee enabled";
            this.cbXbeeEnabled.UseVisualStyleBackColor = true;
            // 
            // SetProjectSettingsDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(287, 488);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SetProjectSettingsDialog";
            this.Text = "Configuration settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbPPMEnabled;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cbTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbLCDSerial;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbLCDEnabled;
        private System.Windows.Forms.ComboBox cbDebugSerial;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbGenerateJoqQuery;
        private System.Windows.Forms.CheckBox cbGenerateDebug;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnSelectDir;
        private System.Windows.Forms.TextBox tbOutPath;
        private System.Windows.Forms.FolderBrowserDialog fsdOutPath;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox cbXBeeSerial;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbXbeeEnabled;
    }
}