namespace Tahorg.RCJoyGUI.ProgrammingPanels.ModelOut
{
    partial class PPMMaperChannelPanel
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
            this.lnkChName = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lnkIn
            // 
            this.lnkIn.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkIn.HolderPanel = null;
            this.lnkIn.IsSelected = false;
            this.lnkIn.LinkType = Tahorg.RCJoyGUI.enLinkType.Axle;
            this.lnkIn.Location = new System.Drawing.Point(-2, 5);
            this.lnkIn.Name = "lnkIn";
            this.lnkIn.Size = new System.Drawing.Size(12, 12);
            this.lnkIn.TabIndex = 0;
            this.lnkIn.Text = "linkPoint1";
            // 
            // lnkOut
            // 
            this.lnkOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkOut.Direction = Tahorg.RCJoyGUI.enLinkDirection.Output;
            this.lnkOut.HolderPanel = null;
            this.lnkOut.IsSelected = false;
            this.lnkOut.LinkType = Tahorg.RCJoyGUI.enLinkType.Axle;
            this.lnkOut.Location = new System.Drawing.Point(104, 5);
            this.lnkOut.Name = "lnkOut";
            this.lnkOut.Size = new System.Drawing.Size(12, 12);
            this.lnkOut.TabIndex = 1;
            this.lnkOut.Text = "linkPoint2";
            // 
            // lnkChName
            // 
            this.lnkChName.AutoSize = true;
            this.lnkChName.Location = new System.Drawing.Point(16, 4);
            this.lnkChName.Name = "lnkChName";
            this.lnkChName.Size = new System.Drawing.Size(32, 13);
            this.lnkChName.TabIndex = 2;
            this.lnkChName.Text = "Chan";
            // 
            // lblValue
            // 
            this.lblValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblValue.AutoSize = true;
            this.lblValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValue.Location = new System.Drawing.Point(63, 4);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(39, 13);
            this.lblValue.TabIndex = 3;
            this.lblValue.Text = "XXXX";
            // 
            // PPMMaperChannelPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.lnkChName);
            this.Controls.Add(this.lnkOut);
            this.Controls.Add(this.lnkIn);
            this.Name = "PPMMaperChannelPanel";
            this.Size = new System.Drawing.Size(114, 21);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LinkPoint lnkIn;
        private LinkPoint lnkOut;
        private System.Windows.Forms.Label lnkChName;
        private System.Windows.Forms.Label lblValue;
    }
}
