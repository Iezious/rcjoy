namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    partial class ValueSwitcherDesignPanel
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbCName = new System.Windows.Forms.TextBox();
            this.cbValuesCunt = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // labelHead
            // 
            this.labelHead.Size = new System.Drawing.Size(143, 13);
            this.labelHead.Text = "Cycle switch";
            // 
            // lnkIn
            // 
            this.lnkIn.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkIn.HolderPanel = null;
            this.lnkIn.IsSelected = false;
            this.lnkIn.LinkType = Tahorg.RCJoyGUI.enLinkType.Button;
            this.lnkIn.Location = new System.Drawing.Point(1, 18);
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
            this.lnkOut.Location = new System.Drawing.Point(132, 18);
            this.lnkOut.Name = "lnkOut";
            this.lnkOut.Size = new System.Drawing.Size(12, 12);
            this.lnkOut.TabIndex = 2;
            this.lnkOut.Text = "linkPoint2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Click";
            // 
            // lblValue
            // 
            this.lblValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblValue.AutoSize = true;
            this.lblValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValue.Location = new System.Drawing.Point(91, 17);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(39, 13);
            this.lblValue.TabIndex = 4;
            this.lblValue.Text = "XXXX";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "CName";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Values";
            // 
            // tbCName
            // 
            this.tbCName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbCName.Location = new System.Drawing.Point(65, 34);
            this.tbCName.Name = "tbCName";
            this.tbCName.Size = new System.Drawing.Size(65, 20);
            this.tbCName.TabIndex = 7;
            this.tbCName.Validating += new System.ComponentModel.CancelEventHandler(this.tbCName_Validating);
            // 
            // cbValuesCunt
            // 
            this.cbValuesCunt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbValuesCunt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbValuesCunt.FormattingEnabled = true;
            this.cbValuesCunt.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4"});
            this.cbValuesCunt.Location = new System.Drawing.Point(65, 58);
            this.cbValuesCunt.Name = "cbValuesCunt";
            this.cbValuesCunt.Size = new System.Drawing.Size(65, 21);
            this.cbValuesCunt.TabIndex = 8;
            this.cbValuesCunt.SelectedIndexChanged += new System.EventHandler(this.cbValuesCunt_SelectedIndexChanged);
            // 
            // ValueSwitcherDesignPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbValuesCunt);
            this.Controls.Add(this.tbCName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lnkOut);
            this.Controls.Add(this.lnkIn);
            this.Name = "ValueSwitcherDesignPanel";
            this.Size = new System.Drawing.Size(143, 89);
            this.Title = "Cycle switch";
            this.Controls.SetChildIndex(this.labelHead, 0);
            this.Controls.SetChildIndex(this.lnkIn, 0);
            this.Controls.SetChildIndex(this.lnkOut, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.lblValue, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.tbCName, 0);
            this.Controls.SetChildIndex(this.cbValuesCunt, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LinkPoint lnkIn;
        private LinkPoint lnkOut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbCName;
        private System.Windows.Forms.ComboBox cbValuesCunt;
    }
}
