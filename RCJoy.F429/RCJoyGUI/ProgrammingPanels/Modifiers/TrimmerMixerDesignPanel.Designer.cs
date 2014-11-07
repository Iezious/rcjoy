namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    partial class TrimmerDesignPanel
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
            this.lnkValue = new Tahorg.RCJoyGUI.LinkPoint();
            this.lnkAxis = new Tahorg.RCJoyGUI.LinkPoint();
            this.lnkOut = new Tahorg.RCJoyGUI.LinkPoint();
            this.lblAxle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblValue1 = new System.Windows.Forms.Label();
            this.cbTrimmerType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelHead
            // 
            this.labelHead.Size = new System.Drawing.Size(144, 13);
            this.labelHead.Text = "Trimmer";
            // 
            // lnkValue
            // 
            this.lnkValue.DataMapIdx = ((short)(0));
            this.lnkValue.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkValue.HolderPanel = null;
            this.lnkValue.IsSelected = false;
            this.lnkValue.LinkType = Tahorg.RCJoyGUI.enLinkType.Value;
            this.lnkValue.Location = new System.Drawing.Point(0, 37);
            this.lnkValue.MappedValueName = null;
            this.lnkValue.Name = "lnkValue";
            this.lnkValue.Size = new System.Drawing.Size(12, 12);
            this.lnkValue.TabIndex = 1;
            this.lnkValue.TabStop = false;
            this.lnkValue.Text = "linkPoint1";
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
            this.lnkOut.Location = new System.Drawing.Point(133, 19);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Trim value";
            // 
            // lblValue1
            // 
            this.lblValue1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblValue1.AutoSize = true;
            this.lblValue1.BackColor = System.Drawing.Color.Transparent;
            this.lblValue1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValue1.Location = new System.Drawing.Point(92, 18);
            this.lblValue1.Name = "lblValue1";
            this.lblValue1.Size = new System.Drawing.Size(39, 13);
            this.lblValue1.TabIndex = 7;
            this.lblValue1.Text = "XXXX";
            // 
            // cbTrimmerType
            // 
            this.cbTrimmerType.Cursor = System.Windows.Forms.Cursors.Default;
            this.cbTrimmerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTrimmerType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbTrimmerType.FormattingEnabled = true;
            this.cbTrimmerType.Items.AddRange(new object[] {
            "None",
            "Aileron",
            "Elevator",
            "Rudder",
            "Throttle"});
            this.cbTrimmerType.Location = new System.Drawing.Point(74, 53);
            this.cbTrimmerType.Name = "cbTrimmerType";
            this.cbTrimmerType.Size = new System.Drawing.Size(61, 21);
            this.cbTrimmerType.TabIndex = 40;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 39;
            this.label8.Text = "Show as";
            // 
            // TrimmerDesignPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbTrimmerType);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lnkOut);
            this.Controls.Add(this.lblValue1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblAxle);
            this.Controls.Add(this.lnkAxis);
            this.Controls.Add(this.lnkValue);
            this.Name = "TrimmerDesignPanel";
            this.Size = new System.Drawing.Size(144, 85);
            this.Title = "Trimmer";
            this.Controls.SetChildIndex(this.labelHead, 0);
            this.Controls.SetChildIndex(this.lnkValue, 0);
            this.Controls.SetChildIndex(this.lnkAxis, 0);
            this.Controls.SetChildIndex(this.lblAxle, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.lblValue1, 0);
            this.Controls.SetChildIndex(this.lnkOut, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.cbTrimmerType, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LinkPoint lnkValue;
        private LinkPoint lnkAxis;
        private LinkPoint lnkOut;
        private System.Windows.Forms.Label lblAxle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblValue1;
        private System.Windows.Forms.ComboBox cbTrimmerType;
        private System.Windows.Forms.Label label8;
    }
}
