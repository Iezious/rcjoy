namespace Tahorg.RCJoyGUI
{
    partial class DraggableElement
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
            this.labelHead = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelHead
            // 
            this.labelHead.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.labelHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelHead.Location = new System.Drawing.Point(0, 0);
            this.labelHead.Name = "labelHead";
            this.labelHead.Size = new System.Drawing.Size(192, 13);
            this.labelHead.TabIndex = 0;
            this.labelHead.Click += new System.EventHandler(this.labelHead_Click);
            this.labelHead.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelHead_MouseDown);
            this.labelHead.MouseMove += new System.Windows.Forms.MouseEventHandler(this.labelHead_MouseMove);
            this.labelHead.MouseUp += new System.Windows.Forms.MouseEventHandler(this.labelHead_MouseUp);
            // 
            // DraggableElement
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.Controls.Add(this.labelHead);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "DraggableElement";
            this.Size = new System.Drawing.Size(192, 214);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DraggableElement_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Label labelHead;

    }
}
