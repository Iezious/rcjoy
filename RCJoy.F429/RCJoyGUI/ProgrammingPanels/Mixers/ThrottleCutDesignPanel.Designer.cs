namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    partial class ThrottleCutDesignPanel
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
            this.lnkThrottleIn = new Tahorg.RCJoyGUI.LinkPoint();
            this.lnkEnableSwitch = new Tahorg.RCJoyGUI.LinkPoint();
            this.lnkDisable = new Tahorg.RCJoyGUI.LinkPoint();
            this.lnkOut = new Tahorg.RCJoyGUI.LinkPoint();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblOut = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelHead
            // 
            this.labelHead.Size = new System.Drawing.Size(137, 13);
            this.labelHead.Text = "Throttle cut";
            // 
            // lnkThrottleIn
            // 
            this.lnkThrottleIn.DataMapIdx = ((short)(0));
            this.lnkThrottleIn.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkThrottleIn.HolderPanel = null;
            this.lnkThrottleIn.IsSelected = false;
            this.lnkThrottleIn.LinkType = Tahorg.RCJoyGUI.enLinkType.Axle;
            this.lnkThrottleIn.Location = new System.Drawing.Point(1, 21);
            this.lnkThrottleIn.MappedValueName = null;
            this.lnkThrottleIn.Name = "lnkThrottleIn";
            this.lnkThrottleIn.Size = new System.Drawing.Size(12, 12);
            this.lnkThrottleIn.TabIndex = 2;
            this.lnkThrottleIn.Text = "linkPoint1";
            // 
            // lnkEnableSwitch
            // 
            this.lnkEnableSwitch.DataMapIdx = ((short)(0));
            this.lnkEnableSwitch.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkEnableSwitch.HolderPanel = null;
            this.lnkEnableSwitch.IsSelected = false;
            this.lnkEnableSwitch.LinkType = Tahorg.RCJoyGUI.enLinkType.Button;
            this.lnkEnableSwitch.Location = new System.Drawing.Point(1, 41);
            this.lnkEnableSwitch.MappedValueName = null;
            this.lnkEnableSwitch.Name = "lnkEnableSwitch";
            this.lnkEnableSwitch.Size = new System.Drawing.Size(12, 12);
            this.lnkEnableSwitch.TabIndex = 3;
            this.lnkEnableSwitch.Text = "linkPoint2";
            // 
            // lnkDisable
            // 
            this.lnkDisable.DataMapIdx = ((short)(0));
            this.lnkDisable.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkDisable.HolderPanel = null;
            this.lnkDisable.IsSelected = false;
            this.lnkDisable.LinkType = Tahorg.RCJoyGUI.enLinkType.Button;
            this.lnkDisable.Location = new System.Drawing.Point(1, 60);
            this.lnkDisable.MappedValueName = null;
            this.lnkDisable.Name = "lnkDisable";
            this.lnkDisable.Size = new System.Drawing.Size(12, 12);
            this.lnkDisable.TabIndex = 4;
            this.lnkDisable.Text = "linkPoint3";
            // 
            // lnkOut
            // 
            this.lnkOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkOut.DataMapIdx = ((short)(0));
            this.lnkOut.Direction = Tahorg.RCJoyGUI.enLinkDirection.Output;
            this.lnkOut.HolderPanel = null;
            this.lnkOut.IsSelected = false;
            this.lnkOut.LinkType = Tahorg.RCJoyGUI.enLinkType.Axle;
            this.lnkOut.Location = new System.Drawing.Point(126, 21);
            this.lnkOut.MappedValueName = null;
            this.lnkOut.Name = "lnkOut";
            this.lnkOut.Size = new System.Drawing.Size(12, 12);
            this.lnkOut.TabIndex = 5;
            this.lnkOut.Text = "linkPoint4";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Throttle";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Enable or switch T/C ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Disable T/C ";
            // 
            // lblOut
            // 
            this.lblOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOut.Location = new System.Drawing.Point(73, 20);
            this.lblOut.Name = "lblOut";
            this.lblOut.Size = new System.Drawing.Size(52, 13);
            this.lblOut.TabIndex = 9;
            this.lblOut.Text = "XXXX";
            this.lblOut.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ThrottleCutDesignPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblOut);
            this.Controls.Add(this.lnkOut);
            this.Controls.Add(this.lnkDisable);
            this.Controls.Add(this.lnkEnableSwitch);
            this.Controls.Add(this.lnkThrottleIn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Name = "ThrottleCutDesignPanel";
            this.Size = new System.Drawing.Size(137, 81);
            this.Title = "Throttle cut";
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.labelHead, 0);
            this.Controls.SetChildIndex(this.lnkThrottleIn, 0);
            this.Controls.SetChildIndex(this.lnkEnableSwitch, 0);
            this.Controls.SetChildIndex(this.lnkDisable, 0);
            this.Controls.SetChildIndex(this.lnkOut, 0);
            this.Controls.SetChildIndex(this.lblOut, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LinkPoint lnkThrottleIn;
        private LinkPoint lnkEnableSwitch;
        private LinkPoint lnkDisable;
        private LinkPoint lnkOut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblOut;
    }
}
