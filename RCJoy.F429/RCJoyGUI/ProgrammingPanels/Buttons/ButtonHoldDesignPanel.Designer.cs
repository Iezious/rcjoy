namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    partial class ButtonHoldDesignPanel
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
            this.tbPressed = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbReleased = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelHead
            // 
            this.labelHead.Size = new System.Drawing.Size(144, 13);
            this.labelHead.Text = "Hold button switch";
            // 
            // lnkIn
            // 
            this.lnkIn.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkIn.HolderPanel = null;
            this.lnkIn.IsSelected = false;
            this.lnkIn.LinkType = Tahorg.RCJoyGUI.enLinkType.Button;
            this.lnkIn.Location = new System.Drawing.Point(1, 20);
            this.lnkIn.MappedValueName = null;
            this.lnkIn.Name = "lnkIn";
            this.lnkIn.Size = new System.Drawing.Size(12, 12);
            this.lnkIn.TabIndex = 1;
            this.lnkIn.Text = "linkPoint1";
            // 
            // lnkOut
            // 
            this.lnkOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkOut.Direction = Tahorg.RCJoyGUI.enLinkDirection.Output;
            this.lnkOut.HolderPanel = null;
            this.lnkOut.IsSelected = false;
            this.lnkOut.LinkType = Tahorg.RCJoyGUI.enLinkType.Value;
            this.lnkOut.Location = new System.Drawing.Point(133, 20);
            this.lnkOut.MappedValueName = null;
            this.lnkOut.Name = "lnkOut";
            this.lnkOut.Size = new System.Drawing.Size(12, 12);
            this.lnkOut.TabIndex = 2;
            this.lnkOut.Text = "linkPoint2";
            // 
            // tbPressed
            // 
            this.tbPressed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPressed.Location = new System.Drawing.Point(69, 41);
            this.tbPressed.Name = "tbPressed";
            this.tbPressed.Size = new System.Drawing.Size(64, 20);
            this.tbPressed.TabIndex = 3;
            this.tbPressed.Text = "0";
            this.tbPressed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbPressed.Validating += new System.ComponentModel.CancelEventHandler(this.tbPressed_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Presssed";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Released";
            // 
            // tbReleased
            // 
            this.tbReleased.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbReleased.Location = new System.Drawing.Point(69, 67);
            this.tbReleased.Name = "tbReleased";
            this.tbReleased.Size = new System.Drawing.Size(64, 20);
            this.tbReleased.TabIndex = 5;
            this.tbReleased.Text = "0";
            this.tbReleased.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbReleased.Validating += new System.ComponentModel.CancelEventHandler(this.tbReleased_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Button";
            // 
            // lblValue
            // 
            this.lblValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValue.Location = new System.Drawing.Point(80, 20);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(50, 13);
            this.lblValue.TabIndex = 8;
            this.lblValue.Text = "XXXX";
            this.lblValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ButtonHoldDesignPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbReleased);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbPressed);
            this.Controls.Add(this.lnkOut);
            this.Controls.Add(this.lnkIn);
            this.Name = "ButtonHoldDesignPanel";
            this.Size = new System.Drawing.Size(144, 101);
            this.Title = "Hold button switch";
            this.Controls.SetChildIndex(this.labelHead, 0);
            this.Controls.SetChildIndex(this.lnkIn, 0);
            this.Controls.SetChildIndex(this.lnkOut, 0);
            this.Controls.SetChildIndex(this.tbPressed, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.tbReleased, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.lblValue, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LinkPoint lnkIn;
        private LinkPoint lnkOut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbReleased;
        private System.Windows.Forms.TextBox tbPressed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblValue;
    }
}
