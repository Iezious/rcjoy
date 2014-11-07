namespace Tahorg.RCJoyGUI.ProgrammingPanels.Buttons
{
    partial class MultiButtonPanel
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
            this.lnkButton.TabStop = false;
            this.lnkButton.Text = "linkPoint1";
            // 
            // tbCase
            // 
            this.tbCase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbCase.Location = new System.Drawing.Point(51, 1);
            this.tbCase.Name = "tbCase";
            this.tbCase.Size = new System.Drawing.Size(86, 20);
            this.tbCase.TabIndex = 2;
            this.tbCase.Text = "0";
            this.tbCase.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // MultiButtonPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbCase);
            this.Controls.Add(this.lnkButton);
            this.Name = "MultiButtonPanel";
            this.Size = new System.Drawing.Size(147, 23);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LinkPoint lnkButton;
        private System.Windows.Forms.TextBox tbCase;
    }
}
