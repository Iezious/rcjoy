namespace Tahorg.RCJoyGUI.Dialogs
{
    partial class CreateModelDialog
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
            this.gPPMData = new System.Windows.Forms.GroupBox();
            this.tbMax = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbCenter = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbMin = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gModelData = new System.Windows.Forms.GroupBox();
            this.tbCName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbChannels = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.gbModelType = new System.Windows.Forms.GroupBox();
            this.rbSysMenu = new System.Windows.Forms.RadioButton();
            this.rbRCModel = new System.Windows.Forms.RadioButton();
            this.gPPMData.SuspendLayout();
            this.gModelData.SuspendLayout();
            this.gbModelType.SuspendLayout();
            this.SuspendLayout();
            // 
            // gPPMData
            // 
            this.gPPMData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gPPMData.Controls.Add(this.tbMax);
            this.gPPMData.Controls.Add(this.label6);
            this.gPPMData.Controls.Add(this.tbCenter);
            this.gPPMData.Controls.Add(this.label5);
            this.gPPMData.Controls.Add(this.tbMin);
            this.gPPMData.Controls.Add(this.label4);
            this.gPPMData.Location = new System.Drawing.Point(12, 190);
            this.gPPMData.Name = "gPPMData";
            this.gPPMData.Size = new System.Drawing.Size(279, 100);
            this.gPPMData.TabIndex = 2;
            this.gPPMData.TabStop = false;
            this.gPPMData.Text = "PPM Data";
            // 
            // tbMax
            // 
            this.tbMax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMax.Location = new System.Drawing.Point(76, 71);
            this.tbMax.Name = "tbMax";
            this.tbMax.Size = new System.Drawing.Size(191, 20);
            this.tbMax.TabIndex = 3;
            this.tbMax.Text = "2000";
            this.tbMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbMax.Validating += new System.ComponentModel.CancelEventHandler(this.tbMax_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Max";
            // 
            // tbCenter
            // 
            this.tbCenter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCenter.Location = new System.Drawing.Point(76, 45);
            this.tbCenter.Name = "tbCenter";
            this.tbCenter.Size = new System.Drawing.Size(191, 20);
            this.tbCenter.TabIndex = 2;
            this.tbCenter.Text = "1500";
            this.tbCenter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbCenter.Validating += new System.ComponentModel.CancelEventHandler(this.tbCenter_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Center";
            // 
            // tbMin
            // 
            this.tbMin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMin.Location = new System.Drawing.Point(76, 19);
            this.tbMin.Name = "tbMin";
            this.tbMin.Size = new System.Drawing.Size(191, 20);
            this.tbMin.TabIndex = 1;
            this.tbMin.Text = "1000";
            this.tbMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbMin.Validating += new System.ComponentModel.CancelEventHandler(this.tbMin_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Min";
            // 
            // gModelData
            // 
            this.gModelData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gModelData.Controls.Add(this.tbCName);
            this.gModelData.Controls.Add(this.label3);
            this.gModelData.Controls.Add(this.cbChannels);
            this.gModelData.Controls.Add(this.label2);
            this.gModelData.Controls.Add(this.tbName);
            this.gModelData.Controls.Add(this.label1);
            this.gModelData.Location = new System.Drawing.Point(12, 68);
            this.gModelData.Name = "gModelData";
            this.gModelData.Size = new System.Drawing.Size(279, 119);
            this.gModelData.TabIndex = 1;
            this.gModelData.TabStop = false;
            this.gModelData.Text = "Model data";
            // 
            // tbCName
            // 
            this.tbCName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCName.Location = new System.Drawing.Point(76, 52);
            this.tbCName.Name = "tbCName";
            this.tbCName.Size = new System.Drawing.Size(191, 20);
            this.tbCName.TabIndex = 2;
            this.tbCName.Validating += new System.ComponentModel.CancelEventHandler(this.tbCName_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Code prefix";
            // 
            // cbChannels
            // 
            this.cbChannels.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbChannels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChannels.FormattingEnabled = true;
            this.cbChannels.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.cbChannels.Location = new System.Drawing.Point(76, 78);
            this.cbChannels.Name = "cbChannels";
            this.cbChannels.Size = new System.Drawing.Size(191, 21);
            this.cbChannels.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Channels";
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.Location = new System.Drawing.Point(76, 26);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(191, 20);
            this.tbName.TabIndex = 1;
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            this.tbName.Validating += new System.ComponentModel.CancelEventHandler(this.tbName_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Name";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(135, 294);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(216, 294);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // gbModelType
            // 
            this.gbModelType.Controls.Add(this.rbSysMenu);
            this.gbModelType.Controls.Add(this.rbRCModel);
            this.gbModelType.Location = new System.Drawing.Point(12, 13);
            this.gbModelType.Name = "gbModelType";
            this.gbModelType.Size = new System.Drawing.Size(279, 49);
            this.gbModelType.TabIndex = 0;
            this.gbModelType.TabStop = false;
            this.gbModelType.Text = "Model type";
            // 
            // rbSysMenu
            // 
            this.rbSysMenu.AutoSize = true;
            this.rbSysMenu.Location = new System.Drawing.Point(123, 19);
            this.rbSysMenu.Name = "rbSysMenu";
            this.rbSysMenu.Size = new System.Drawing.Size(88, 17);
            this.rbSysMenu.TabIndex = 1;
            this.rbSysMenu.Text = "System menu";
            this.rbSysMenu.UseVisualStyleBackColor = true;
            this.rbSysMenu.CheckedChanged += new System.EventHandler(this.rbRCModel_CheckedChanged);
            // 
            // rbRCModel
            // 
            this.rbRCModel.AutoSize = true;
            this.rbRCModel.Checked = true;
            this.rbRCModel.Location = new System.Drawing.Point(9, 20);
            this.rbRCModel.Name = "rbRCModel";
            this.rbRCModel.Size = new System.Drawing.Size(54, 17);
            this.rbRCModel.TabIndex = 0;
            this.rbRCModel.TabStop = true;
            this.rbRCModel.Text = "Model";
            this.rbRCModel.UseVisualStyleBackColor = true;
            this.rbRCModel.CheckedChanged += new System.EventHandler(this.rbRCModel_CheckedChanged);
            // 
            // CreateModelDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(301, 330);
            this.Controls.Add(this.gbModelType);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gModelData);
            this.Controls.Add(this.gPPMData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CreateModelDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create model";
            this.gPPMData.ResumeLayout(false);
            this.gPPMData.PerformLayout();
            this.gModelData.ResumeLayout(false);
            this.gModelData.PerformLayout();
            this.gbModelType.ResumeLayout(false);
            this.gbModelType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gPPMData;
        private System.Windows.Forms.TextBox tbMin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gModelData;
        private System.Windows.Forms.TextBox tbCName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbChannels;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbMax;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbCenter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox gbModelType;
        private System.Windows.Forms.RadioButton rbSysMenu;
        private System.Windows.Forms.RadioButton rbRCModel;

    }
}