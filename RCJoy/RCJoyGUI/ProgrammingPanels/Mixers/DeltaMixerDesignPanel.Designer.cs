namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    partial class DeltaMixerDesignPanel
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
            this.lnkElev = new Tahorg.RCJoyGUI.LinkPoint();
            this.lnkAileron = new Tahorg.RCJoyGUI.LinkPoint();
            this.lnkOut1 = new Tahorg.RCJoyGUI.LinkPoint();
            this.lnkOut2 = new Tahorg.RCJoyGUI.LinkPoint();
            this.lblAil = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblValue1 = new System.Windows.Forms.Label();
            this.lblValue2 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbAilQuef = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelHead
            // 
            this.labelHead.Size = new System.Drawing.Size(154, 13);
            this.labelHead.Text = "Delta mix";
            // 
            // lnkElev
            // 
            this.lnkElev.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkElev.HolderPanel = null;
            this.lnkElev.IsSelected = false;
            this.lnkElev.LinkType = Tahorg.RCJoyGUI.enLinkType.Axle;
            this.lnkElev.Location = new System.Drawing.Point(0, 37);
            this.lnkElev.Name = "lnkElev";
            this.lnkElev.Size = new System.Drawing.Size(12, 12);
            this.lnkElev.TabIndex = 1;
            this.lnkElev.TabStop = false;
            this.lnkElev.Text = "linkPoint1";
            // 
            // lnkAileron
            // 
            this.lnkAileron.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkAileron.HolderPanel = null;
            this.lnkAileron.IsSelected = false;
            this.lnkAileron.LinkType = Tahorg.RCJoyGUI.enLinkType.Axle;
            this.lnkAileron.Location = new System.Drawing.Point(0, 19);
            this.lnkAileron.Name = "lnkAileron";
            this.lnkAileron.Size = new System.Drawing.Size(12, 12);
            this.lnkAileron.TabIndex = 2;
            this.lnkAileron.Text = "linkPoint2";
            // 
            // lnkOut1
            // 
            this.lnkOut1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkOut1.Direction = Tahorg.RCJoyGUI.enLinkDirection.Output;
            this.lnkOut1.HolderPanel = null;
            this.lnkOut1.IsSelected = false;
            this.lnkOut1.LinkType = Tahorg.RCJoyGUI.enLinkType.Axle;
            this.lnkOut1.Location = new System.Drawing.Point(143, 19);
            this.lnkOut1.Name = "lnkOut1";
            this.lnkOut1.Size = new System.Drawing.Size(12, 12);
            this.lnkOut1.TabIndex = 3;
            this.lnkOut1.TabStop = false;
            this.lnkOut1.Text = "linkPoint3";
            // 
            // lnkOut2
            // 
            this.lnkOut2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkOut2.Direction = Tahorg.RCJoyGUI.enLinkDirection.Output;
            this.lnkOut2.HolderPanel = null;
            this.lnkOut2.IsSelected = false;
            this.lnkOut2.LinkType = Tahorg.RCJoyGUI.enLinkType.Axle;
            this.lnkOut2.Location = new System.Drawing.Point(143, 37);
            this.lnkOut2.Name = "lnkOut2";
            this.lnkOut2.Size = new System.Drawing.Size(12, 12);
            this.lnkOut2.TabIndex = 4;
            this.lnkOut2.TabStop = false;
            this.lnkOut2.Text = "linkPoint4";
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
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Elevalor";
            // 
            // lblValue1
            // 
            this.lblValue1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblValue1.AutoSize = true;
            this.lblValue1.BackColor = System.Drawing.Color.Transparent;
            this.lblValue1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValue1.Location = new System.Drawing.Point(102, 18);
            this.lblValue1.Name = "lblValue1";
            this.lblValue1.Size = new System.Drawing.Size(39, 13);
            this.lblValue1.TabIndex = 7;
            this.lblValue1.Text = "XXXX";
            // 
            // lblValue2
            // 
            this.lblValue2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblValue2.AutoSize = true;
            this.lblValue2.BackColor = System.Drawing.Color.Transparent;
            this.lblValue2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValue2.Location = new System.Drawing.Point(102, 36);
            this.lblValue2.Name = "lblValue2";
            this.lblValue2.Size = new System.Drawing.Size(39, 13);
            this.lblValue2.TabIndex = 8;
            this.lblValue2.Text = "XXXX";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Out 2:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(66, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Out 1:";
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
            this.tbAilQuef.Location = new System.Drawing.Point(105, 53);
            this.tbAilQuef.Name = "tbAilQuef";
            this.tbAilQuef.Size = new System.Drawing.Size(36, 20);
            this.tbAilQuef.TabIndex = 12;
            this.tbAilQuef.Text = "30";
            this.tbAilQuef.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbAilQuef.Validating += new System.ComponentModel.CancelEventHandler(this.tbAilQuef_Validating);
            // 
            // DeltaMixerDesignPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbAilQuef);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblValue2);
            this.Controls.Add(this.lnkOut1);
            this.Controls.Add(this.lblValue1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblAil);
            this.Controls.Add(this.lnkOut2);
            this.Controls.Add(this.lnkAileron);
            this.Controls.Add(this.lnkElev);
            this.Name = "DeltaMixerDesignPanel";
            this.Size = new System.Drawing.Size(154, 83);
            this.Title = "Delta mix";
            this.Controls.SetChildIndex(this.labelHead, 0);
            this.Controls.SetChildIndex(this.lnkElev, 0);
            this.Controls.SetChildIndex(this.lnkAileron, 0);
            this.Controls.SetChildIndex(this.lnkOut2, 0);
            this.Controls.SetChildIndex(this.lblAil, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.lblValue1, 0);
            this.Controls.SetChildIndex(this.lnkOut1, 0);
            this.Controls.SetChildIndex(this.lblValue2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.tbAilQuef, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LinkPoint lnkElev;
        private LinkPoint lnkAileron;
        private LinkPoint lnkOut1;
        private LinkPoint lnkOut2;
        private System.Windows.Forms.Label lblAil;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblValue1;
        private System.Windows.Forms.Label lblValue2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbAilQuef;
    }
}
