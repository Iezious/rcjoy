namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    partial class PPMMaperDesignPanel
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
            this.lnkMin = new Tahorg.RCJoyGUI.LinkPoint();
            this.lnkMax = new Tahorg.RCJoyGUI.LinkPoint();
            this.lnkCenter = new Tahorg.RCJoyGUI.LinkPoint();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbMin = new System.Windows.Forms.TextBox();
            this.tbCenter = new System.Windows.Forms.TextBox();
            this.tbMax = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbChannels = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // labelHead
            // 
            this.labelHead.Size = new System.Drawing.Size(137, 13);
            this.labelHead.Text = "Axis travel maper";
            // 
            // lnkMin
            // 
            this.lnkMin.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkMin.HolderPanel = null;
            this.lnkMin.IsSelected = false;
            this.lnkMin.LinkType = Tahorg.RCJoyGUI.enLinkType.Value;
            this.lnkMin.Location = new System.Drawing.Point(1, 19);
            this.lnkMin.Name = "lnkMin";
            this.lnkMin.Size = new System.Drawing.Size(12, 12);
            this.lnkMin.TabIndex = 1;
            this.lnkMin.Text = "linkPoint1";
            this.lnkMin.LinkedToChanged += new System.EventHandler(this.lnkMin_LinkedToChanged);
            // 
            // lnkMax
            // 
            this.lnkMax.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkMax.HolderPanel = null;
            this.lnkMax.IsSelected = false;
            this.lnkMax.LinkType = Tahorg.RCJoyGUI.enLinkType.Value;
            this.lnkMax.Location = new System.Drawing.Point(1, 63);
            this.lnkMax.Name = "lnkMax";
            this.lnkMax.Size = new System.Drawing.Size(12, 12);
            this.lnkMax.TabIndex = 2;
            this.lnkMax.Text = "linkPoint2";
            this.lnkMax.LinkedToChanged += new System.EventHandler(this.lnkMax_LinkedToChanged);
            // 
            // lnkCenter
            // 
            this.lnkCenter.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkCenter.HolderPanel = null;
            this.lnkCenter.IsSelected = false;
            this.lnkCenter.LinkType = Tahorg.RCJoyGUI.enLinkType.Value;
            this.lnkCenter.Location = new System.Drawing.Point(1, 40);
            this.lnkCenter.Name = "lnkCenter";
            this.lnkCenter.Size = new System.Drawing.Size(12, 12);
            this.lnkCenter.TabIndex = 3;
            this.lnkCenter.Text = "linkPoint3";
            this.lnkCenter.LinkedToChanged += new System.EventHandler(this.lnkCenter_LinkedToChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Min";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Center";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Max";
            // 
            // tbMin
            // 
            this.tbMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbMin.Location = new System.Drawing.Point(76, 16);
            this.tbMin.Name = "tbMin";
            this.tbMin.Size = new System.Drawing.Size(53, 20);
            this.tbMin.TabIndex = 7;
            this.tbMin.Text = "0";
            this.tbMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbMin.Validating += new System.ComponentModel.CancelEventHandler(this.tbMin_Validating);
            // 
            // tbCenter
            // 
            this.tbCenter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCenter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbCenter.Location = new System.Drawing.Point(76, 38);
            this.tbCenter.Name = "tbCenter";
            this.tbCenter.Size = new System.Drawing.Size(53, 20);
            this.tbCenter.TabIndex = 8;
            this.tbCenter.Text = "0";
            this.tbCenter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbCenter.Validating += new System.ComponentModel.CancelEventHandler(this.tbCenter_Validating);
            // 
            // tbMax
            // 
            this.tbMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbMax.Location = new System.Drawing.Point(76, 60);
            this.tbMax.Name = "tbMax";
            this.tbMax.Size = new System.Drawing.Size(53, 20);
            this.tbMax.TabIndex = 9;
            this.tbMax.Text = "0";
            this.tbMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbMax.Validating += new System.ComponentModel.CancelEventHandler(this.tbMax_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Channels";
            // 
            // cbChannels
            // 
            this.cbChannels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChannels.FormattingEnabled = true;
            this.cbChannels.Items.AddRange(new object[] {
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
            this.cbChannels.Location = new System.Drawing.Point(76, 82);
            this.cbChannels.Name = "cbChannels";
            this.cbChannels.Size = new System.Drawing.Size(53, 21);
            this.cbChannels.TabIndex = 11;
            this.cbChannels.SelectedIndexChanged += new System.EventHandler(this.lbChannels_SelectedIndexChanged);
            // 
            // PPMMaperDesignPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbChannels);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbMax);
            this.Controls.Add(this.tbCenter);
            this.Controls.Add(this.tbMin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lnkCenter);
            this.Controls.Add(this.lnkMax);
            this.Controls.Add(this.lnkMin);
            this.Name = "PPMMaperDesignPanel";
            this.Size = new System.Drawing.Size(137, 109);
            this.Title = "Axis travel maper";
            this.Controls.SetChildIndex(this.labelHead, 0);
            this.Controls.SetChildIndex(this.lnkMin, 0);
            this.Controls.SetChildIndex(this.lnkMax, 0);
            this.Controls.SetChildIndex(this.lnkCenter, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.tbMin, 0);
            this.Controls.SetChildIndex(this.tbCenter, 0);
            this.Controls.SetChildIndex(this.tbMax, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.cbChannels, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LinkPoint lnkMin;
        private LinkPoint lnkMax;
        private LinkPoint lnkCenter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbMin;
        private System.Windows.Forms.TextBox tbCenter;
        private System.Windows.Forms.TextBox tbMax;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbChannels;
    }
}
