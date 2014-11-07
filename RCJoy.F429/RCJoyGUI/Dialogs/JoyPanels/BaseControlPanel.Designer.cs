namespace Tahorg.RCJoyGUI.JoyDialog
{
    partial class BaseControlPanel
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
            this.components = new System.ComponentModel.Container();
            this.lblHead = new System.Windows.Forms.Label();
            this.linkRemove = new System.Windows.Forms.LinkLabel();
            this.linkUp = new System.Windows.Forms.LinkLabel();
            this.linkDown = new System.Windows.Forms.LinkLabel();
            this.formToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // lblHead
            // 
            this.lblHead.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHead.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lblHead.Location = new System.Drawing.Point(0, 0);
            this.lblHead.Name = "lblHead";
            this.lblHead.Size = new System.Drawing.Size(255, 15);
            this.lblHead.TabIndex = 0;
            this.lblHead.Text = "Axle";
            // 
            // linkRemove
            // 
            this.linkRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkRemove.AutoSize = true;
            this.linkRemove.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.linkRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkRemove.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkRemove.LinkColor = System.Drawing.SystemColors.HighlightText;
            this.linkRemove.Location = new System.Drawing.Point(238, 0);
            this.linkRemove.Name = "linkRemove";
            this.linkRemove.Size = new System.Drawing.Size(15, 13);
            this.linkRemove.TabIndex = 1;
            this.linkRemove.Text = "X";
            this.linkRemove.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkRemove_LinkClicked);
            // 
            // linkUp
            // 
            this.linkUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkUp.AutoSize = true;
            this.linkUp.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.linkUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkUp.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkUp.LinkColor = System.Drawing.SystemColors.HighlightText;
            this.linkUp.Location = new System.Drawing.Point(217, 0);
            this.linkUp.Name = "linkUp";
            this.linkUp.Size = new System.Drawing.Size(14, 13);
            this.linkUp.TabIndex = 2;
            this.linkUp.Text = "˄";
            this.linkUp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkUp_LinkClicked);
            // 
            // linkDown
            // 
            this.linkDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkDown.AutoSize = true;
            this.linkDown.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.linkDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkDown.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkDown.LinkColor = System.Drawing.SystemColors.HighlightText;
            this.linkDown.Location = new System.Drawing.Point(196, 0);
            this.linkDown.Name = "linkDown";
            this.linkDown.Size = new System.Drawing.Size(14, 13);
            this.linkDown.TabIndex = 3;
            this.linkDown.Text = "˅";
            this.linkDown.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkDown_LinkClicked);
            // 
            // BaseControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.linkDown);
            this.Controls.Add(this.linkUp);
            this.Controls.Add(this.linkRemove);
            this.Controls.Add(this.lblHead);
            this.Name = "BaseControlPanel";
            this.Size = new System.Drawing.Size(255, 131);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.ToolTip formToolTip;
        protected System.Windows.Forms.Label lblHead;
        protected System.Windows.Forms.LinkLabel linkRemove;
        protected System.Windows.Forms.LinkLabel linkUp;
        protected System.Windows.Forms.LinkLabel linkDown;
    }
}
