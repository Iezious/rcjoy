namespace Tahorg.RCJoyGUI.ProgrammingPanels.Buttons
{
    partial class ButtonToValuePanel
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
            this.lnkButton = new Tahorg.RCJoyGUI.LinkPoint();
            this.tbCase = new System.Windows.Forms.TextBox();
            this.tbText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lnkButton
            // 
            this.lnkButton.DataMapIdx = ((short)(0));
            this.lnkButton.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkButton.HolderPanel = null;
            this.lnkButton.IsSelected = false;
            this.lnkButton.LinkType = Tahorg.RCJoyGUI.enLinkType.Button;
            this.lnkButton.Location = new System.Drawing.Point(-3, 5);
            this.lnkButton.MappedValueName = null;
            this.lnkButton.Name = "lnkButton";
            this.lnkButton.Size = new System.Drawing.Size(12, 12);
            this.lnkButton.TabIndex = 0;
            this.lnkButton.Text = "linkPoint1";
            // 
            // tbCase
            // 
            this.tbCase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbCase.Location = new System.Drawing.Point(89, 1);
            this.tbCase.Name = "tbCase";
            this.tbCase.Size = new System.Drawing.Size(48, 20);
            this.tbCase.TabIndex = 2;
            this.tbCase.Text = "0";
            this.tbCase.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbText
            // 
            this.tbText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbText.Location = new System.Drawing.Point(17, 1);
            this.tbText.Name = "tbText";
            this.tbText.Size = new System.Drawing.Size(66, 20);
            this.tbText.TabIndex = 3;
            // 
            // ButtonToValuePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbText);
            this.Controls.Add(this.tbCase);
            this.Controls.Add(this.lnkButton);
            this.Name = "ButtonToValuePanel";
            this.Size = new System.Drawing.Size(147, 23);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LinkPoint lnkButton;
        private System.Windows.Forms.TextBox tbCase;
        private System.Windows.Forms.TextBox tbText;
    }
}
