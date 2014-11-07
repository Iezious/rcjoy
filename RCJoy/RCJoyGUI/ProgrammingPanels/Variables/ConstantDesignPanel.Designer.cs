namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    partial class ConstantDesignPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lnkOutValue = new Tahorg.RCJoyGUI.LinkPoint();
            this.lnkOutAxis = new Tahorg.RCJoyGUI.LinkPoint();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbEEPRom = new System.Windows.Forms.CheckBox();
            this.lbStep = new System.Windows.Forms.Label();
            this.tbChangeStep = new System.Windows.Forms.TextBox();
            this.lbName = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lblMin = new System.Windows.Forms.Label();
            this.tbMin = new System.Windows.Forms.TextBox();
            this.lbMax = new System.Windows.Forms.Label();
            this.tbMax = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelHead
            // 
            this.labelHead.Text = "Constant";
            // 
            // lnkOutValue
            // 
            this.lnkOutValue.DataMapIdx = ((short)(0));
            this.lnkOutValue.Direction = Tahorg.RCJoyGUI.enLinkDirection.Output;
            this.lnkOutValue.HolderPanel = null;
            this.lnkOutValue.IsSelected = false;
            this.lnkOutValue.LinkType = Tahorg.RCJoyGUI.enLinkType.Value;
            this.lnkOutValue.Location = new System.Drawing.Point(182, 26);
            this.lnkOutValue.MappedValueName = null;
            this.lnkOutValue.Name = "lnkOutValue";
            this.lnkOutValue.Size = new System.Drawing.Size(12, 12);
            this.lnkOutValue.TabIndex = 1;
            this.lnkOutValue.Text = "linkPoint1";
            // 
            // lnkOutAxis
            // 
            this.lnkOutAxis.DataMapIdx = ((short)(0));
            this.lnkOutAxis.Direction = Tahorg.RCJoyGUI.enLinkDirection.Output;
            this.lnkOutAxis.HolderPanel = null;
            this.lnkOutAxis.IsSelected = false;
            this.lnkOutAxis.LinkType = Tahorg.RCJoyGUI.enLinkType.Axle;
            this.lnkOutAxis.Location = new System.Drawing.Point(182, 50);
            this.lnkOutAxis.MappedValueName = null;
            this.lnkOutAxis.Name = "lnkOutAxis";
            this.lnkOutAxis.Size = new System.Drawing.Size(12, 12);
            this.lnkOutAxis.TabIndex = 2;
            this.lnkOutAxis.Text = "linkPoint2";
            // 
            // tbValue
            // 
            this.tbValue.Location = new System.Drawing.Point(89, 22);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(87, 20);
            this.tbValue.TabIndex = 0;
            this.tbValue.Text = "0";
            this.tbValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Value";
            // 
            // cbEEPRom
            // 
            this.cbEEPRom.AutoSize = true;
            this.cbEEPRom.Checked = true;
            this.cbEEPRom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbEEPRom.Location = new System.Drawing.Point(19, 48);
            this.cbEEPRom.Name = "cbEEPRom";
            this.cbEEPRom.Size = new System.Drawing.Size(72, 17);
            this.cbEEPRom.TabIndex = 5;
            this.cbEEPRom.Text = "EEPROM";
            this.cbEEPRom.UseVisualStyleBackColor = true;
            this.cbEEPRom.CheckedChanged += new System.EventHandler(this.cbEEPRom_CheckedChanged);
            // 
            // lbStep
            // 
            this.lbStep.AutoSize = true;
            this.lbStep.Location = new System.Drawing.Point(16, 96);
            this.lbStep.Name = "lbStep";
            this.lbStep.Size = new System.Drawing.Size(29, 13);
            this.lbStep.TabIndex = 9;
            this.lbStep.Text = "Step";
            // 
            // tbChangeStep
            // 
            this.tbChangeStep.Location = new System.Drawing.Point(89, 93);
            this.tbChangeStep.Name = "tbChangeStep";
            this.tbChangeStep.Size = new System.Drawing.Size(87, 20);
            this.tbChangeStep.TabIndex = 8;
            this.tbChangeStep.Text = "10";
            this.tbChangeStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Location = new System.Drawing.Point(16, 70);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(35, 13);
            this.lbName.TabIndex = 11;
            this.lbName.Text = "Name";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(89, 67);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(87, 20);
            this.tbName.TabIndex = 7;
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.Location = new System.Drawing.Point(16, 122);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(24, 13);
            this.lblMin.TabIndex = 13;
            this.lblMin.Text = "Min";
            // 
            // tbMin
            // 
            this.tbMin.Location = new System.Drawing.Point(89, 119);
            this.tbMin.Name = "tbMin";
            this.tbMin.Size = new System.Drawing.Size(87, 20);
            this.tbMin.TabIndex = 9;
            this.tbMin.Text = "-1000";
            this.tbMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbMax
            // 
            this.lbMax.AutoSize = true;
            this.lbMax.Location = new System.Drawing.Point(16, 148);
            this.lbMax.Name = "lbMax";
            this.lbMax.Size = new System.Drawing.Size(27, 13);
            this.lbMax.TabIndex = 15;
            this.lbMax.Text = "Max";
            // 
            // tbMax
            // 
            this.tbMax.Location = new System.Drawing.Point(89, 145);
            this.tbMax.Name = "tbMax";
            this.tbMax.Size = new System.Drawing.Size(87, 20);
            this.tbMax.TabIndex = 10;
            this.tbMax.Text = "1000";
            this.tbMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ConstantDesignPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbMax);
            this.Controls.Add(this.tbMax);
            this.Controls.Add(this.lblMin);
            this.Controls.Add(this.tbMin);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.lbStep);
            this.Controls.Add(this.tbChangeStep);
            this.Controls.Add(this.cbEEPRom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbValue);
            this.Controls.Add(this.lnkOutAxis);
            this.Controls.Add(this.lnkOutValue);
            this.Name = "ConstantDesignPanel";
            this.Size = new System.Drawing.Size(192, 173);
            this.Title = "Constant";
            this.Controls.SetChildIndex(this.labelHead, 0);
            this.Controls.SetChildIndex(this.lnkOutValue, 0);
            this.Controls.SetChildIndex(this.lnkOutAxis, 0);
            this.Controls.SetChildIndex(this.tbValue, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.cbEEPRom, 0);
            this.Controls.SetChildIndex(this.tbChangeStep, 0);
            this.Controls.SetChildIndex(this.lbStep, 0);
            this.Controls.SetChildIndex(this.tbName, 0);
            this.Controls.SetChildIndex(this.lbName, 0);
            this.Controls.SetChildIndex(this.tbMin, 0);
            this.Controls.SetChildIndex(this.lblMin, 0);
            this.Controls.SetChildIndex(this.tbMax, 0);
            this.Controls.SetChildIndex(this.lbMax, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LinkPoint lnkOutValue;
        private LinkPoint lnkOutAxis;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbEEPRom;
        private System.Windows.Forms.Label lbStep;
        private System.Windows.Forms.TextBox tbChangeStep;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.TextBox tbMin;
        private System.Windows.Forms.Label lbMax;
        private System.Windows.Forms.TextBox tbMax;
    }
}
