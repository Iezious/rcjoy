namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    partial class HatToButtonMapperDesignPanel
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
            this.label3 = new System.Windows.Forms.Label();
            this.cbCasesCount = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelHead
            // 
            this.labelHead.Size = new System.Drawing.Size(142, 13);
            this.labelHead.Text = "Hat -> Buttons";
            // 
            // lnkIn
            // 
            this.lnkIn.DataMapIdx = ((short)(0));
            this.lnkIn.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkIn.HolderPanel = null;
            this.lnkIn.IsSelected = false;
            this.lnkIn.LinkType = Tahorg.RCJoyGUI.enLinkType.Value;
            this.lnkIn.Location = new System.Drawing.Point(1, 22);
            this.lnkIn.MappedValueName = null;
            this.lnkIn.Name = "lnkIn";
            this.lnkIn.Size = new System.Drawing.Size(12, 12);
            this.lnkIn.TabIndex = 3;
            this.lnkIn.Text = "linkPoint1";
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
            "9"});
            this.cbCasesCount.Location = new System.Drawing.Point(71, 44);
            this.cbCasesCount.Name = "cbCasesCount";
            this.cbCasesCount.Size = new System.Drawing.Size(56, 21);
            this.cbCasesCount.TabIndex = 8;
            this.cbCasesCount.SelectedIndexChanged += new System.EventHandler(this.cbCasesCount_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Hat switch";
            // 
            // HatToButtonMapperDesignPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbCasesCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lnkIn);
            this.Name = "HatToButtonMapperDesignPanel";
            this.Size = new System.Drawing.Size(142, 76);
            this.Title = "Hat -> Buttons";
            this.Controls.SetChildIndex(this.labelHead, 0);
            this.Controls.SetChildIndex(this.lnkIn, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.cbCasesCount, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LinkPoint lnkIn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbCasesCount;
        private System.Windows.Forms.Label label1;
    }
}
