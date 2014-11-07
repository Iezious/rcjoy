namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    partial class HatToButtonMapperDesignPanelValuePanel
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
            this.tbCase = new System.Windows.Forms.TextBox();
            this.lnkOut = new Tahorg.RCJoyGUI.LinkPoint();
            this.lblName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbCase
            // 
            this.tbCase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbCase.Location = new System.Drawing.Point(6, 4);
            this.tbCase.Name = "tbCase";
            this.tbCase.Size = new System.Drawing.Size(68, 20);
            this.tbCase.TabIndex = 1;
            this.tbCase.Text = "0";
            this.tbCase.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbCase.Validating += new System.ComponentModel.CancelEventHandler(this.tbCase_Validating);
            // 
            // lnkOut
            // 
            this.lnkOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkOut.DataMapIdx = ((short)(0));
            this.lnkOut.Direction = Tahorg.RCJoyGUI.enLinkDirection.Output;
            this.lnkOut.HolderPanel = null;
            this.lnkOut.IsSelected = false;
            this.lnkOut.LinkType = Tahorg.RCJoyGUI.enLinkType.Button;
            this.lnkOut.Location = new System.Drawing.Point(112, 8);
            this.lnkOut.MappedValueName = null;
            this.lnkOut.Name = "lnkOut";
            this.lnkOut.Size = new System.Drawing.Size(12, 12);
            this.lnkOut.TabIndex = 2;
            this.lnkOut.Text = "linkPoint1";
            // 
            // lblName
            // 
            this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(86, 7);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(20, 13);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "B9";
            // 
            // HatToButtonMapperDesignPanelValuePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lnkOut);
            this.Controls.Add(this.tbCase);
            this.Name = "HatToButtonMapperDesignPanelValuePanel";
            this.Size = new System.Drawing.Size(121, 27);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbCase;
        private LinkPoint lnkOut;
        private System.Windows.Forms.Label lblName;

    }
}
