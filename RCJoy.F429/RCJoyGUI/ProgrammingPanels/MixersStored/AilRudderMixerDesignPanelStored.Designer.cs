namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    partial class AilronToRudderMixerDesignPanelStored
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
            this.lnkRudder = new Tahorg.RCJoyGUI.LinkPoint();
            this.lnkAileron = new Tahorg.RCJoyGUI.LinkPoint();
            this.lnkOut = new Tahorg.RCJoyGUI.LinkPoint();
            this.lblAil = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblValue1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbAilQuef = new System.Windows.Forms.TextBox();
            this.cbEEPROM = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // labelHead
            // 
            this.labelHead.Size = new System.Drawing.Size(122, 13);
            this.labelHead.Text = "AIlerons To Rudder";
            // 
            // lnkRudder
            // 
            this.lnkRudder.DataMapIdx = ((short)(0));
            this.lnkRudder.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkRudder.HolderPanel = null;
            this.lnkRudder.IsSelected = false;
            this.lnkRudder.LinkType = Tahorg.RCJoyGUI.enLinkType.Axle;
            this.lnkRudder.Location = new System.Drawing.Point(0, 37);
            this.lnkRudder.MappedValueName = null;
            this.lnkRudder.Name = "lnkRudder";
            this.lnkRudder.Size = new System.Drawing.Size(12, 12);
            this.lnkRudder.TabIndex = 1;
            this.lnkRudder.TabStop = false;
            this.lnkRudder.Text = "linkPoint1";
            // 
            // lnkAileron
            // 
            this.lnkAileron.DataMapIdx = ((short)(0));
            this.lnkAileron.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkAileron.HolderPanel = null;
            this.lnkAileron.IsSelected = false;
            this.lnkAileron.LinkType = Tahorg.RCJoyGUI.enLinkType.Axle;
            this.lnkAileron.Location = new System.Drawing.Point(0, 19);
            this.lnkAileron.MappedValueName = null;
            this.lnkAileron.Name = "lnkAileron";
            this.lnkAileron.Size = new System.Drawing.Size(12, 12);
            this.lnkAileron.TabIndex = 2;
            this.lnkAileron.Text = "linkPoint2";
            // 
            // lnkOut
            // 
            this.lnkOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkOut.DataMapIdx = ((short)(0));
            this.lnkOut.Direction = Tahorg.RCJoyGUI.enLinkDirection.Output;
            this.lnkOut.HolderPanel = null;
            this.lnkOut.IsSelected = false;
            this.lnkOut.LinkType = Tahorg.RCJoyGUI.enLinkType.Axle;
            this.lnkOut.Location = new System.Drawing.Point(111, 19);
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
            this.lblAil.Size = new System.Drawing.Size(44, 13);
            this.lblAil.TabIndex = 5;
            this.lblAil.Text = "Ailerons";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Rudder";
            // 
            // lblValue1
            // 
            this.lblValue1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblValue1.AutoSize = true;
            this.lblValue1.BackColor = System.Drawing.Color.Transparent;
            this.lblValue1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValue1.Location = new System.Drawing.Point(70, 18);
            this.lblValue1.Name = "lblValue1";
            this.lblValue1.Size = new System.Drawing.Size(39, 13);
            this.lblValue1.TabIndex = 7;
            this.lblValue1.Text = "XXXX";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Aileron %";
            // 
            // tbAilQuef
            // 
            this.tbAilQuef.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAilQuef.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbAilQuef.Location = new System.Drawing.Point(73, 53);
            this.tbAilQuef.Name = "tbAilQuef";
            this.tbAilQuef.Size = new System.Drawing.Size(36, 20);
            this.tbAilQuef.TabIndex = 12;
            this.tbAilQuef.Text = "20";
            this.tbAilQuef.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbAilQuef.Validating += new System.ComponentModel.CancelEventHandler(this.tbAilQuef_Validating);
            // 
            // cbEEPROM
            // 
            this.cbEEPROM.AutoSize = true;
            this.cbEEPROM.Location = new System.Drawing.Point(15, 79);
            this.cbEEPROM.Name = "cbEEPROM";
            this.cbEEPROM.Size = new System.Drawing.Size(72, 17);
            this.cbEEPROM.TabIndex = 13;
            this.cbEEPROM.Text = "EEPROM";
            this.cbEEPROM.UseVisualStyleBackColor = true;
            this.cbEEPROM.CheckedChanged += new System.EventHandler(this.cbEEPROM_CheckedChanged);
            // 
            // AilronToRudderMixerDesignPanelStored
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbEEPROM);
            this.Controls.Add(this.tbAilQuef);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lnkOut);
            this.Controls.Add(this.lblValue1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblAil);
            this.Controls.Add(this.lnkAileron);
            this.Controls.Add(this.lnkRudder);
            this.Name = "AilronToRudderMixerDesignPanelStored";
            this.Size = new System.Drawing.Size(122, 103);
            this.Title = "AIlerons To Rudder";
            this.Controls.SetChildIndex(this.labelHead, 0);
            this.Controls.SetChildIndex(this.lnkRudder, 0);
            this.Controls.SetChildIndex(this.lnkAileron, 0);
            this.Controls.SetChildIndex(this.lblAil, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.lblValue1, 0);
            this.Controls.SetChildIndex(this.lnkOut, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.tbAilQuef, 0);
            this.Controls.SetChildIndex(this.cbEEPROM, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LinkPoint lnkRudder;
        private LinkPoint lnkAileron;
        private LinkPoint lnkOut;
        private System.Windows.Forms.Label lblAil;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblValue1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbAilQuef;
        private System.Windows.Forms.CheckBox cbEEPROM;
    }
}
