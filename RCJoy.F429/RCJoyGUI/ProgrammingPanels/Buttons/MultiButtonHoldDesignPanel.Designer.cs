namespace Tahorg.RCJoyGUI.ProgrammingPanels.Buttons
{
    partial class MultiButtonHoldDesignPanel
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
            this.lnkOut = new Tahorg.RCJoyGUI.LinkPoint();
            this.lblValue = new System.Windows.Forms.Label();
            this.cbCasesCount = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbDefault = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelHead
            // 
            this.labelHead.Size = new System.Drawing.Size(144, 13);
            this.labelHead.Text = "Hod buttons to Value";
            // 
            // lnkOut
            // 
            this.lnkOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkOut.DataMapIdx = ((short)(0));
            this.lnkOut.Direction = Tahorg.RCJoyGUI.enLinkDirection.Output;
            this.lnkOut.HolderPanel = null;
            this.lnkOut.IsSelected = false;
            this.lnkOut.LinkType = Tahorg.RCJoyGUI.enLinkType.Value;
            this.lnkOut.Location = new System.Drawing.Point(133, 20);
            this.lnkOut.MappedValueName = null;
            this.lnkOut.Name = "lnkOut";
            this.lnkOut.Size = new System.Drawing.Size(12, 12);
            this.lnkOut.TabIndex = 2;
            this.lnkOut.TabStop = false;
            this.lnkOut.Text = "linkPoint1";
            // 
            // lblValue
            // 
            this.lblValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblValue.AutoSize = true;
            this.lblValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValue.Location = new System.Drawing.Point(88, 20);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(39, 13);
            this.lblValue.TabIndex = 3;
            this.lblValue.Text = "XXXX";
            // 
            // cbCasesCount
            // 
            this.cbCasesCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCasesCount.FormattingEnabled = true;
            this.cbCasesCount.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.cbCasesCount.Location = new System.Drawing.Point(70, 61);
            this.cbCasesCount.Name = "cbCasesCount";
            this.cbCasesCount.Size = new System.Drawing.Size(57, 21);
            this.cbCasesCount.TabIndex = 2;
            this.cbCasesCount.SelectedIndexChanged += new System.EventHandler(this.cbCasesCount_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Buttons";
            // 
            // tbDefault
            // 
            this.tbDefault.Location = new System.Drawing.Point(70, 37);
            this.tbDefault.Name = "tbDefault";
            this.tbDefault.Size = new System.Drawing.Size(55, 20);
            this.tbDefault.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "No button";
            // 
            // MultiButtonHoldDesignPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbDefault);
            this.Controls.Add(this.cbCasesCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.lnkOut);
            this.Name = "MultiButtonHoldDesignPanel";
            this.Size = new System.Drawing.Size(144, 89);
            this.Title = "Hod buttons to Value";
            this.Controls.SetChildIndex(this.labelHead, 0);
            this.Controls.SetChildIndex(this.lnkOut, 0);
            this.Controls.SetChildIndex(this.lblValue, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.cbCasesCount, 0);
            this.Controls.SetChildIndex(this.tbDefault, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LinkPoint lnkOut;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.ComboBox cbCasesCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbDefault;
        private System.Windows.Forms.Label label1;
    }
}
