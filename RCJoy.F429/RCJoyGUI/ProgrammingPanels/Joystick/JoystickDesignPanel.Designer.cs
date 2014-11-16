namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    partial class JoystickDesignPanel
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
            this.rbRaw = new System.Windows.Forms.RadioButton();
            this.rbParsed = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // labelHead
            // 
            this.labelHead.Size = new System.Drawing.Size(148, 13);
            // 
            // rbRaw
            // 
            this.rbRaw.AutoSize = true;
            this.rbRaw.Checked = true;
            this.rbRaw.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbRaw.Location = new System.Drawing.Point(13, 19);
            this.rbRaw.Name = "rbRaw";
            this.rbRaw.Size = new System.Drawing.Size(81, 18);
            this.rbRaw.TabIndex = 2;
            this.rbRaw.TabStop = true;
            this.rbRaw.Text = "RAW data";
            this.rbRaw.UseVisualStyleBackColor = true;
            // 
            // rbParsed
            // 
            this.rbParsed.AutoSize = true;
            this.rbParsed.Cursor = System.Windows.Forms.Cursors.Default;
            this.rbParsed.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbParsed.Location = new System.Drawing.Point(13, 42);
            this.rbParsed.Name = "rbParsed";
            this.rbParsed.Size = new System.Drawing.Size(88, 18);
            this.rbParsed.TabIndex = 3;
            this.rbParsed.Text = "Parsed data";
            this.rbParsed.UseVisualStyleBackColor = true;
            // 
            // JoystickDesignPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.rbParsed);
            this.Controls.Add(this.rbRaw);
            this.Name = "JoystickDesignPanel";
            this.Size = new System.Drawing.Size(148, 234);
            this.Controls.SetChildIndex(this.labelHead, 0);
            this.Controls.SetChildIndex(this.rbRaw, 0);
            this.Controls.SetChildIndex(this.rbParsed, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbRaw;
        private System.Windows.Forms.RadioButton rbParsed;
    }
}
