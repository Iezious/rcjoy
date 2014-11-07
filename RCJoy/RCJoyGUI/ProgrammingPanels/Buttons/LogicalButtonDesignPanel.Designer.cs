namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    partial class LogicalButtonDesignPanel
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
            this.lnkClick = new Tahorg.RCJoyGUI.LinkPoint();
            this.lnkOut = new Tahorg.RCJoyGUI.LinkPoint();
            this.lblAil = new System.Windows.Forms.Label();
            this.lblValue1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lnkMod1 = new Tahorg.RCJoyGUI.LinkPoint();
            this.label2 = new System.Windows.Forms.Label();
            this.lnkMod2 = new Tahorg.RCJoyGUI.LinkPoint();
            this.SuspendLayout();
            // 
            // labelHead
            // 
            this.labelHead.Size = new System.Drawing.Size(118, 13);
            this.labelHead.Text = "Logical button";
            // 
            // lnkClick
            // 
            this.lnkClick.DataMapIdx = ((short)(0));
            this.lnkClick.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkClick.HolderPanel = null;
            this.lnkClick.IsSelected = false;
            this.lnkClick.LinkType = Tahorg.RCJoyGUI.enLinkType.Button;
            this.lnkClick.Location = new System.Drawing.Point(0, 19);
            this.lnkClick.MappedValueName = null;
            this.lnkClick.Name = "lnkClick";
            this.lnkClick.Size = new System.Drawing.Size(12, 12);
            this.lnkClick.TabIndex = 2;
            this.lnkClick.Text = "linkPoint2";
            // 
            // lnkOut
            // 
            this.lnkOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkOut.DataMapIdx = ((short)(0));
            this.lnkOut.Direction = Tahorg.RCJoyGUI.enLinkDirection.Output;
            this.lnkOut.HolderPanel = null;
            this.lnkOut.IsSelected = false;
            this.lnkOut.LinkType = Tahorg.RCJoyGUI.enLinkType.Button;
            this.lnkOut.Location = new System.Drawing.Point(107, 19);
            this.lnkOut.MappedValueName = null;
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
            this.lblAil.Size = new System.Drawing.Size(30, 13);
            this.lblAil.TabIndex = 5;
            this.lblAil.Text = "Click";
            // 
            // lblValue1
            // 
            this.lblValue1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblValue1.AutoSize = true;
            this.lblValue1.BackColor = System.Drawing.Color.Transparent;
            this.lblValue1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValue1.Location = new System.Drawing.Point(73, 18);
            this.lblValue1.Name = "lblValue1";
            this.lblValue1.Size = new System.Drawing.Size(32, 13);
            this.lblValue1.TabIndex = 7;
            this.lblValue1.Text = "BTN";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "And pressed";
            // 
            // lnkMod1
            // 
            this.lnkMod1.DataMapIdx = ((short)(0));
            this.lnkMod1.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkMod1.HolderPanel = null;
            this.lnkMod1.IsSelected = false;
            this.lnkMod1.LinkType = Tahorg.RCJoyGUI.enLinkType.Button;
            this.lnkMod1.Location = new System.Drawing.Point(0, 37);
            this.lnkMod1.MappedValueName = null;
            this.lnkMod1.Name = "lnkMod1";
            this.lnkMod1.Size = new System.Drawing.Size(12, 12);
            this.lnkMod1.TabIndex = 8;
            this.lnkMod1.Text = "linkPoint2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "And not pressed";
            // 
            // lnkMod2
            // 
            this.lnkMod2.DataMapIdx = ((short)(0));
            this.lnkMod2.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkMod2.HolderPanel = null;
            this.lnkMod2.IsSelected = false;
            this.lnkMod2.LinkType = Tahorg.RCJoyGUI.enLinkType.Button;
            this.lnkMod2.Location = new System.Drawing.Point(0, 55);
            this.lnkMod2.MappedValueName = null;
            this.lnkMod2.Name = "lnkMod2";
            this.lnkMod2.Size = new System.Drawing.Size(12, 12);
            this.lnkMod2.TabIndex = 10;
            this.lnkMod2.Text = "linkPoint2";
            // 
            // LogicalButtonDesignPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lnkMod2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lnkMod1);
            this.Controls.Add(this.lnkOut);
            this.Controls.Add(this.lblValue1);
            this.Controls.Add(this.lblAil);
            this.Controls.Add(this.lnkClick);
            this.Name = "LogicalButtonDesignPanel";
            this.Size = new System.Drawing.Size(118, 80);
            this.Title = "Logical button";
            this.Controls.SetChildIndex(this.labelHead, 0);
            this.Controls.SetChildIndex(this.lnkClick, 0);
            this.Controls.SetChildIndex(this.lblAil, 0);
            this.Controls.SetChildIndex(this.lblValue1, 0);
            this.Controls.SetChildIndex(this.lnkOut, 0);
            this.Controls.SetChildIndex(this.lnkMod1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.lnkMod2, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LinkPoint lnkClick;
        private LinkPoint lnkOut;
        private System.Windows.Forms.Label lblAil;
        private System.Windows.Forms.Label lblValue1;
        private System.Windows.Forms.Label label1;
        private LinkPoint lnkMod1;
        private System.Windows.Forms.Label label2;
        private LinkPoint lnkMod2;
    }
}
