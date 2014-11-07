namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    partial class ExponentDesignPanelStored
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
            this.lnkIn = new Tahorg.RCJoyGUI.LinkPoint();
            this.lnkOut = new Tahorg.RCJoyGUI.LinkPoint();
            this.lblAil = new System.Windows.Forms.Label();
            this.lblValue1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbExp = new System.Windows.Forms.TextBox();
            this.cbEEPROM = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // labelHead
            // 
            this.labelHead.Size = new System.Drawing.Size(141, 13);
            this.labelHead.Text = "";
            // 
            // lnkIn
            // 
            this.lnkIn.DataMapIdx = ((short)(0));
            this.lnkIn.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkIn.HolderPanel = null;
            this.lnkIn.IsSelected = false;
            this.lnkIn.LinkType = Tahorg.RCJoyGUI.enLinkType.Axle;
            this.lnkIn.Location = new System.Drawing.Point(0, 19);
            this.lnkIn.MappedValueName = null;
            this.lnkIn.Name = "lnkIn";
            this.lnkIn.Size = new System.Drawing.Size(12, 12);
            this.lnkIn.TabIndex = 2;
            this.lnkIn.Text = "linkPoint2";
            // 
            // lnkOut
            // 
            this.lnkOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkOut.DataMapIdx = ((short)(0));
            this.lnkOut.Direction = Tahorg.RCJoyGUI.enLinkDirection.Output;
            this.lnkOut.HolderPanel = null;
            this.lnkOut.IsSelected = false;
            this.lnkOut.LinkType = Tahorg.RCJoyGUI.enLinkType.Axle;
            this.lnkOut.Location = new System.Drawing.Point(130, 19);
            this.lnkOut.MappedValueName = null;
            this.lnkOut.Name = "lnkOut";
            this.lnkOut.Size = new System.Drawing.Size(12, 12);
            this.lnkOut.TabIndex = 3;
            this.lnkOut.TabStop = false;
            this.lnkOut.Text = "linkPoint3";
            // 
            // lblAil
            // 
            this.lblAil.AutoSize = true;
            this.lblAil.Location = new System.Drawing.Point(12, 18);
            this.lblAil.Name = "lblAil";
            this.lblAil.Size = new System.Drawing.Size(52, 13);
            this.lblAil.TabIndex = 5;
            this.lblAil.Text = "Input axis";
            // 
            // lblValue1
            // 
            this.lblValue1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblValue1.AutoSize = true;
            this.lblValue1.BackColor = System.Drawing.Color.Transparent;
            this.lblValue1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValue1.Location = new System.Drawing.Point(89, 18);
            this.lblValue1.Name = "lblValue1";
            this.lblValue1.Size = new System.Drawing.Size(39, 13);
            this.lblValue1.TabIndex = 7;
            this.lblValue1.Text = "XXXX";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Exp (-10 ... 10)";
            // 
            // tbExp
            // 
            this.tbExp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbExp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbExp.Location = new System.Drawing.Point(92, 37);
            this.tbExp.Name = "tbExp";
            this.tbExp.Size = new System.Drawing.Size(36, 20);
            this.tbExp.TabIndex = 12;
            this.tbExp.Text = "4";
            this.tbExp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbExp.Validating += new System.ComponentModel.CancelEventHandler(this.tbAilQuef_Validating);
            // 
            // cbEEPROM
            // 
            this.cbEEPROM.AutoSize = true;
            this.cbEEPROM.Location = new System.Drawing.Point(15, 66);
            this.cbEEPROM.Name = "cbEEPROM";
            this.cbEEPROM.Size = new System.Drawing.Size(72, 17);
            this.cbEEPROM.TabIndex = 14;
            this.cbEEPROM.Text = "EEPROM";
            this.cbEEPROM.UseVisualStyleBackColor = true;
            this.cbEEPROM.CheckedChanged += new System.EventHandler(this.cbEEPROM_CheckedChanged);
            // 
            // ExponentDesignPanelStored
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbEEPROM);
            this.Controls.Add(this.tbExp);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lnkOut);
            this.Controls.Add(this.lblValue1);
            this.Controls.Add(this.lblAil);
            this.Controls.Add(this.lnkIn);
            this.Name = "ExponentDesignPanelStored";
            this.Size = new System.Drawing.Size(141, 88);
            this.Title = "";
            this.Controls.SetChildIndex(this.labelHead, 0);
            this.Controls.SetChildIndex(this.lnkIn, 0);
            this.Controls.SetChildIndex(this.lblAil, 0);
            this.Controls.SetChildIndex(this.lblValue1, 0);
            this.Controls.SetChildIndex(this.lnkOut, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.tbExp, 0);
            this.Controls.SetChildIndex(this.cbEEPROM, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LinkPoint lnkIn;
        private LinkPoint lnkOut;
        private System.Windows.Forms.Label lblAil;
        private System.Windows.Forms.Label lblValue1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbExp;
        private System.Windows.Forms.CheckBox cbEEPROM;
    }
}
