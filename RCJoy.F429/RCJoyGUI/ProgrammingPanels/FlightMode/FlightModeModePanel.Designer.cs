namespace Tahorg.RCJoyGUI.ProgrammingPanels.FlightMode
{
    partial class FlightModeModePanel
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.linkPoint1 = new Tahorg.RCJoyGUI.LinkPoint();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.linkPoint2 = new Tahorg.RCJoyGUI.LinkPoint();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.Location = new System.Drawing.Point(44, 6);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(80, 20);
            this.tbName.TabIndex = 1;
            // 
            // linkPoint1
            // 
            this.linkPoint1.DataMapIdx = ((short)(0));
            this.linkPoint1.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.linkPoint1.HolderPanel = null;
            this.linkPoint1.IsSelected = false;
            this.linkPoint1.LinkType = Tahorg.RCJoyGUI.enLinkType.Axle;
            this.linkPoint1.Location = new System.Drawing.Point(-3, 36);
            this.linkPoint1.MappedValueName = null;
            this.linkPoint1.Name = "linkPoint1";
            this.linkPoint1.Size = new System.Drawing.Size(12, 12);
            this.linkPoint1.TabIndex = 2;
            this.linkPoint1.Text = "linkPoint1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "label3";
            // 
            // linkPoint2
            // 
            this.linkPoint2.DataMapIdx = ((short)(0));
            this.linkPoint2.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.linkPoint2.HolderPanel = null;
            this.linkPoint2.IsSelected = false;
            this.linkPoint2.LinkType = Tahorg.RCJoyGUI.enLinkType.Axle;
            this.linkPoint2.Location = new System.Drawing.Point(-3, 55);
            this.linkPoint2.MappedValueName = null;
            this.linkPoint2.Name = "linkPoint2";
            this.linkPoint2.Size = new System.Drawing.Size(12, 12);
            this.linkPoint2.TabIndex = 4;
            this.linkPoint2.Text = "linkPoint2";
            // 
            // FlightModeModePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.linkPoint2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.linkPoint1);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label1);
            this.Name = "FlightModeModePanel";
            this.Size = new System.Drawing.Size(132, 150);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbName;
        private LinkPoint linkPoint1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private LinkPoint linkPoint2;
    }
}
