namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    partial class TrimmerEmulatorDesignPanel
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
            this.lnkSet = new Tahorg.RCJoyGUI.LinkPoint();
            this.lnkAxis = new Tahorg.RCJoyGUI.LinkPoint();
            this.lnkOut = new Tahorg.RCJoyGUI.LinkPoint();
            this.lblAxle = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.lblValue1 = new System.Windows.Forms.Label();
            this.lnkReset = new Tahorg.RCJoyGUI.LinkPoint();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelHead
            // 
            this.labelHead.Size = new System.Drawing.Size(147, 13);
            this.labelHead.Text = "Trimmer emulation";
            // 
            // lnkSet
            // 
            this.lnkSet.DataMapIdx = ((short)(0));
            this.lnkSet.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkSet.HolderPanel = null;
            this.lnkSet.IsSelected = false;
            this.lnkSet.LinkType = Tahorg.RCJoyGUI.enLinkType.Button;
            this.lnkSet.Location = new System.Drawing.Point(0, 37);
            this.lnkSet.MappedValueName = null;
            this.lnkSet.Name = "lnkSet";
            this.lnkSet.Size = new System.Drawing.Size(12, 12);
            this.lnkSet.TabIndex = 1;
            this.lnkSet.TabStop = false;
            this.lnkSet.Text = "linkPoint1";
            // 
            // lnkAxis
            // 
            this.lnkAxis.DataMapIdx = ((short)(0));
            this.lnkAxis.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkAxis.HolderPanel = null;
            this.lnkAxis.IsSelected = false;
            this.lnkAxis.LinkType = Tahorg.RCJoyGUI.enLinkType.Axle;
            this.lnkAxis.Location = new System.Drawing.Point(0, 19);
            this.lnkAxis.MappedValueName = null;
            this.lnkAxis.Name = "lnkAxis";
            this.lnkAxis.Size = new System.Drawing.Size(12, 12);
            this.lnkAxis.TabIndex = 2;
            this.lnkAxis.Text = "linkPoint2";
            // 
            // lnkOut
            // 
            this.lnkOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkOut.DataMapIdx = ((short)(0));
            this.lnkOut.Direction = Tahorg.RCJoyGUI.enLinkDirection.Output;
            this.lnkOut.HolderPanel = null;
            this.lnkOut.IsSelected = false;
            this.lnkOut.LinkType = Tahorg.RCJoyGUI.enLinkType.Axle;
            this.lnkOut.Location = new System.Drawing.Point(136, 19);
            this.lnkOut.MappedValueName = null;
            this.lnkOut.Name = "lnkOut";
            this.lnkOut.Size = new System.Drawing.Size(12, 12);
            this.lnkOut.TabIndex = 3;
            this.lnkOut.TabStop = false;
            this.lnkOut.Text = "linkPoint3";
            // 
            // lblAxle
            // 
            this.lblAxle.AutoSize = true;
            this.lblAxle.Location = new System.Drawing.Point(12, 18);
            this.lblAxle.Name = "lblAxle";
            this.lblAxle.Size = new System.Drawing.Size(26, 13);
            this.lblAxle.TabIndex = 5;
            this.lblAxle.Text = "Axis";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 36);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(56, 13);
            this.Label1.TabIndex = 6;
            this.Label1.Text = "Set center";
            // 
            // lblValue1
            // 
            this.lblValue1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblValue1.AutoSize = true;
            this.lblValue1.BackColor = System.Drawing.Color.Transparent;
            this.lblValue1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValue1.Location = new System.Drawing.Point(95, 18);
            this.lblValue1.Name = "lblValue1";
            this.lblValue1.Size = new System.Drawing.Size(39, 13);
            this.lblValue1.TabIndex = 7;
            this.lblValue1.Text = "XXXX";
            // 
            // lnkReset
            // 
            this.lnkReset.DataMapIdx = ((short)(0));
            this.lnkReset.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkReset.HolderPanel = null;
            this.lnkReset.IsSelected = false;
            this.lnkReset.LinkType = Tahorg.RCJoyGUI.enLinkType.Button;
            this.lnkReset.Location = new System.Drawing.Point(0, 56);
            this.lnkReset.MappedValueName = null;
            this.lnkReset.Name = "lnkReset";
            this.lnkReset.Size = new System.Drawing.Size(12, 12);
            this.lnkReset.TabIndex = 9;
            this.lnkReset.TabStop = false;
            this.lnkReset.Text = "linkPoint1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Clear";
            // 
            // TrimmerEmulatorDesignPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lnkReset);
            this.Controls.Add(this.lnkOut);
            this.Controls.Add(this.lblValue1);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.lblAxle);
            this.Controls.Add(this.lnkAxis);
            this.Controls.Add(this.lnkSet);
            this.Name = "TrimmerEmulatorDesignPanel";
            this.Size = new System.Drawing.Size(147, 81);
            this.Title = "Trimmer emulation";
            this.Controls.SetChildIndex(this.labelHead, 0);
            this.Controls.SetChildIndex(this.lnkSet, 0);
            this.Controls.SetChildIndex(this.lnkAxis, 0);
            this.Controls.SetChildIndex(this.lblAxle, 0);
            this.Controls.SetChildIndex(this.Label1, 0);
            this.Controls.SetChildIndex(this.lblValue1, 0);
            this.Controls.SetChildIndex(this.lnkOut, 0);
            this.Controls.SetChildIndex(this.lnkReset, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LinkPoint lnkSet;
        private LinkPoint lnkAxis;
        private LinkPoint lnkOut;
        private System.Windows.Forms.Label lblAxle;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label lblValue1;
        private LinkPoint lnkReset;
        private System.Windows.Forms.Label label3;
    }
}
