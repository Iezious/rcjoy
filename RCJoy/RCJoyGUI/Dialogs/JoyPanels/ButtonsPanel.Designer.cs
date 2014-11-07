namespace Tahorg.RCJoyGUI.JoyDialog
{
    partial class ButtonsPanel
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tbCount = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lblCName = new System.Windows.Forms.Label();
            this.tbCName = new System.Windows.Forms.TextBox();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.tbSize = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHead
            // 
            this.lblHead.Size = new System.Drawing.Size(645, 15);
            this.lblHead.Text = "Buttons block";
            // 
            // linkRemove
            // 
            this.linkRemove.Location = new System.Drawing.Point(628, 0);
            this.linkRemove.TabStop = false;
            // 
            // linkUp
            // 
            this.linkUp.Location = new System.Drawing.Point(607, 0);
            this.linkUp.TabStop = false;
            // 
            // linkDown
            // 
            this.linkDown.Location = new System.Drawing.Point(586, 0);
            this.linkDown.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tbCount, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblCName, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbCName, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblSize, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblCount, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbSize, 3, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(645, 57);
            this.tableLayoutPanel1.TabIndex = 22;
            // 
            // tbCount
            // 
            this.tbCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCount.Location = new System.Drawing.Point(73, 31);
            this.tbCount.Name = "tbCount";
            this.tbCount.Size = new System.Drawing.Size(246, 20);
            this.tbCount.TabIndex = 27;
            this.tbCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbCount.Validating += new System.ComponentModel.CancelEventHandler(this.tbCount_Validating);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblName.Location = new System.Drawing.Point(3, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(64, 28);
            this.lblName.TabIndex = 22;
            this.lblName.Text = "Name";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.formToolTip.SetToolTip(this.lblName, "Block name in Designer");
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.Location = new System.Drawing.Point(73, 3);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(246, 20);
            this.tbName.TabIndex = 23;
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            this.tbName.Validating += new System.ComponentModel.CancelEventHandler(this.tbName_Validating);
            // 
            // lblCName
            // 
            this.lblCName.AutoSize = true;
            this.lblCName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCName.Location = new System.Drawing.Point(325, 0);
            this.lblCName.Name = "lblCName";
            this.lblCName.Size = new System.Drawing.Size(64, 28);
            this.lblCName.TabIndex = 24;
            this.lblCName.Text = "CName";
            this.lblCName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.formToolTip.SetToolTip(this.lblCName, "Block name in generated code");
            // 
            // tbCName
            // 
            this.tbCName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCName.Location = new System.Drawing.Point(395, 3);
            this.tbCName.Name = "tbCName";
            this.tbCName.Size = new System.Drawing.Size(247, 20);
            this.tbCName.TabIndex = 25;
            this.tbCName.Validating += new System.ComponentModel.CancelEventHandler(this.tbCName_Validating);
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSize.Location = new System.Drawing.Point(325, 28);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(64, 29);
            this.lblSize.TabIndex = 28;
            this.lblSize.Text = "Size";
            this.lblSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.formToolTip.SetToolTip(this.lblSize, "Bits per 1 button");
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCount.Location = new System.Drawing.Point(3, 28);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(64, 29);
            this.lblCount.TabIndex = 26;
            this.lblCount.Text = "Count";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.formToolTip.SetToolTip(this.lblCount, "Number of buttons");
            // 
            // tbSize
            // 
            this.tbSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSize.Location = new System.Drawing.Point(395, 31);
            this.tbSize.Name = "tbSize";
            this.tbSize.Size = new System.Drawing.Size(247, 20);
            this.tbSize.TabIndex = 29;
            this.tbSize.Text = "1";
            this.tbSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbSize.Validating += new System.ComponentModel.CancelEventHandler(this.tbSize_Validating);
            // 
            // ButtonsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ButtonsPanel";
            this.Size = new System.Drawing.Size(645, 74);
            this.Controls.SetChildIndex(this.lblHead, 0);
            this.Controls.SetChildIndex(this.linkRemove, 0);
            this.Controls.SetChildIndex(this.linkUp, 0);
            this.Controls.SetChildIndex(this.linkDown, 0);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox tbCount;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lblCName;
        private System.Windows.Forms.TextBox tbCName;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.TextBox tbSize;

    }
}
