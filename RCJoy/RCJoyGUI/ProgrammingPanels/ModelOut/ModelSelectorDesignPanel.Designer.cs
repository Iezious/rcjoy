namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    partial class ModelSelectorDesignPanel
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
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelHead
            // 
            this.labelHead.Size = new System.Drawing.Size(110, 13);
            this.labelHead.Text = "Switch to model";
            // 
            // lnkClick
            // 
            this.lnkClick.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkClick.HolderPanel = null;
            this.lnkClick.IsSelected = false;
            this.lnkClick.LinkType = Tahorg.RCJoyGUI.enLinkType.Button;
            this.lnkClick.Location = new System.Drawing.Point(1, 18);
            this.lnkClick.Name = "lnkClick";
            this.lnkClick.Size = new System.Drawing.Size(12, 12);
            this.lnkClick.TabIndex = 1;
            this.lnkClick.Text = "linkPoint1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Click";
            // 
            // ModelSelectorDesignPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lnkClick);
            this.Name = "ModelSelectorDesignPanel";
            this.Size = new System.Drawing.Size(110, 39);
            this.Title = "Switch to model";
            this.Controls.SetChildIndex(this.labelHead, 0);
            this.Controls.SetChildIndex(this.lnkClick, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LinkPoint lnkClick;
        private System.Windows.Forms.Label label1;
    }
}
