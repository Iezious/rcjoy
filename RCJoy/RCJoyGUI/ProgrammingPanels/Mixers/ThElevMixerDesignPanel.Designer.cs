namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    partial class ThrottleToElevMixerDesignPanel
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
            this.lnkThrottle = new Tahorg.RCJoyGUI.LinkPoint();
            this.lnkElevator = new Tahorg.RCJoyGUI.LinkPoint();
            this.lnkOut = new Tahorg.RCJoyGUI.LinkPoint();
            this.lblAil = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblValue1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbThQuef = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelHead
            // 
            this.labelHead.Size = new System.Drawing.Size(127, 13);
            this.labelHead.Text = "Throttle -> Elevalor";
            // 
            // lnkThrottle
            // 
            this.lnkThrottle.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkThrottle.HolderPanel = null;
            this.lnkThrottle.IsSelected = false;
            this.lnkThrottle.LinkType = Tahorg.RCJoyGUI.enLinkType.Axle;
            this.lnkThrottle.Location = new System.Drawing.Point(0, 37);
            this.lnkThrottle.Name = "lnkThrottle";
            this.lnkThrottle.Size = new System.Drawing.Size(12, 12);
            this.lnkThrottle.TabIndex = 1;
            this.lnkThrottle.TabStop = false;
            this.lnkThrottle.Text = "linkPoint1";
            // 
            // lnkElevator
            // 
            this.lnkElevator.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkElevator.HolderPanel = null;
            this.lnkElevator.IsSelected = false;
            this.lnkElevator.LinkType = Tahorg.RCJoyGUI.enLinkType.Axle;
            this.lnkElevator.Location = new System.Drawing.Point(0, 19);
            this.lnkElevator.Name = "lnkElevator";
            this.lnkElevator.Size = new System.Drawing.Size(12, 12);
            this.lnkElevator.TabIndex = 2;
            this.lnkElevator.Text = "linkPoint2";
            // 
            // lnkOut
            // 
            this.lnkOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkOut.Direction = Tahorg.RCJoyGUI.enLinkDirection.Output;
            this.lnkOut.HolderPanel = null;
            this.lnkOut.IsSelected = false;
            this.lnkOut.LinkType = Tahorg.RCJoyGUI.enLinkType.Axle;
            this.lnkOut.Location = new System.Drawing.Point(116, 19);
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
            this.lblAil.Size = new System.Drawing.Size(46, 13);
            this.lblAil.TabIndex = 5;
            this.lblAil.Text = "Elevator";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Throttle";
            // 
            // lblValue1
            // 
            this.lblValue1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblValue1.AutoSize = true;
            this.lblValue1.BackColor = System.Drawing.Color.Transparent;
            this.lblValue1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValue1.Location = new System.Drawing.Point(75, 18);
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
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Throttle %";
            // 
            // tbThQuef
            // 
            this.tbThQuef.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbThQuef.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbThQuef.Location = new System.Drawing.Point(78, 53);
            this.tbThQuef.Name = "tbThQuef";
            this.tbThQuef.Size = new System.Drawing.Size(36, 20);
            this.tbThQuef.TabIndex = 12;
            this.tbThQuef.Text = "20";
            this.tbThQuef.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbThQuef.Validating += new System.ComponentModel.CancelEventHandler(this.tbAilQuef_Validating);
            // 
            // ThrottleToElevMixerDesignPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbThQuef);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lnkOut);
            this.Controls.Add(this.lblValue1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblAil);
            this.Controls.Add(this.lnkElevator);
            this.Controls.Add(this.lnkThrottle);
            this.Name = "ThrottleToElevMixerDesignPanel";
            this.Size = new System.Drawing.Size(127, 83);
            this.Title = "Throttle -> Elevalor";
            this.Controls.SetChildIndex(this.labelHead, 0);
            this.Controls.SetChildIndex(this.lnkThrottle, 0);
            this.Controls.SetChildIndex(this.lnkElevator, 0);
            this.Controls.SetChildIndex(this.lblAil, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.lblValue1, 0);
            this.Controls.SetChildIndex(this.lnkOut, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.tbThQuef, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LinkPoint lnkThrottle;
        private LinkPoint lnkElevator;
        private LinkPoint lnkOut;
        private System.Windows.Forms.Label lblAil;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblValue1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbThQuef;
    }
}
