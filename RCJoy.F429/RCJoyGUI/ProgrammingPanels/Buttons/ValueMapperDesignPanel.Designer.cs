namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    partial class ValueMapperDesignPanel
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
            this.lnkIn = new Tahorg.RCJoyGUI.LinkPoint();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbDefault = new System.Windows.Forms.TextBox();
            this.cbCasesCount = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // labelHead
            // 
            this.labelHead.Size = new System.Drawing.Size(177, 13);
            this.labelHead.Text = "Value mapper";
            // 
            // lnkOut
            // 
            this.lnkOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkOut.Direction = Tahorg.RCJoyGUI.enLinkDirection.Output;
            this.lnkOut.HolderPanel = null;
            this.lnkOut.IsSelected = false;
            this.lnkOut.LinkType = Tahorg.RCJoyGUI.enLinkType.Value;
            this.lnkOut.Location = new System.Drawing.Point(166, 22);
            this.lnkOut.Name = "lnkOut";
            this.lnkOut.Size = new System.Drawing.Size(12, 12);
            this.lnkOut.TabIndex = 1;
            this.lnkOut.Text = "linkPoint1";
            // 
            // lblValue
            // 
            this.lblValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblValue.AutoSize = true;
            this.lblValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValue.Location = new System.Drawing.Point(127, 21);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(39, 13);
            this.lblValue.TabIndex = 2;
            this.lblValue.Text = "XXXX";
            // 
            // lnkIn
            // 
            this.lnkIn.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkIn.HolderPanel = null;
            this.lnkIn.IsSelected = false;
            this.lnkIn.LinkType = Tahorg.RCJoyGUI.enLinkType.Value;
            this.lnkIn.Location = new System.Drawing.Point(1, 22);
            this.lnkIn.Name = "lnkIn";
            this.lnkIn.Size = new System.Drawing.Size(12, 12);
            this.lnkIn.TabIndex = 3;
            this.lnkIn.Text = "linkPoint1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Default";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Cases";
            // 
            // tbDefault
            // 
            this.tbDefault.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDefault.Location = new System.Drawing.Point(71, 18);
            this.tbDefault.Name = "tbDefault";
            this.tbDefault.Size = new System.Drawing.Size(49, 20);
            this.tbDefault.TabIndex = 7;
            this.tbDefault.Text = "0";
            this.tbDefault.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbDefault.Validating += new System.ComponentModel.CancelEventHandler(this.tbDefault_Validating);
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
            this.cbCasesCount.Location = new System.Drawing.Point(71, 44);
            this.cbCasesCount.Name = "cbCasesCount";
            this.cbCasesCount.Size = new System.Drawing.Size(92, 21);
            this.cbCasesCount.TabIndex = 8;
            this.cbCasesCount.SelectedIndexChanged += new System.EventHandler(this.cbCasesCount_SelectedValueChanged);
            // 
            // ValueMapperDesignPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbCasesCount);
            this.Controls.Add(this.tbDefault);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lnkIn);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.lnkOut);
            this.Name = "ValueMapperDesignPanel";
            this.Size = new System.Drawing.Size(177, 76);
            this.Title = "Value mapper";
            this.Controls.SetChildIndex(this.labelHead, 0);
            this.Controls.SetChildIndex(this.lnkOut, 0);
            this.Controls.SetChildIndex(this.lblValue, 0);
            this.Controls.SetChildIndex(this.lnkIn, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.tbDefault, 0);
            this.Controls.SetChildIndex(this.cbCasesCount, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LinkPoint lnkOut;
        private System.Windows.Forms.Label lblValue;
        private LinkPoint lnkIn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbDefault;
        private System.Windows.Forms.ComboBox cbCasesCount;
    }
}
