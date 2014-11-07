namespace Tahorg.RCJoyGUI.JoyDialog
{
    partial class HatPanel
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
            this.tbMax = new System.Windows.Forms.TextBox();
            this.tbMin = new System.Windows.Forms.TextBox();
            this.lblMax = new System.Windows.Forms.Label();
            this.lblMin = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbLength = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbCName = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblName = new System.Windows.Forms.Label();
            this.lblCName = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHead
            // 
            this.lblHead.Size = new System.Drawing.Size(767, 15);
            this.lblHead.Text = "Hatwitch";
            // 
            // linkRemove
            // 
            this.linkRemove.Location = new System.Drawing.Point(752, 0);
            // 
            // linkUp
            // 
            this.linkUp.Location = new System.Drawing.Point(731, 0);
            // 
            // linkDown
            // 
            this.linkDown.Location = new System.Drawing.Point(710, 0);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.tbMax, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbMin, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblMax, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblMin, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbLength, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 49);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(767, 28);
            this.tableLayoutPanel1.TabIndex = 30;
            // 
            // tbMax
            // 
            this.tbMax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMax.Location = new System.Drawing.Point(569, 3);
            this.tbMax.Name = "tbMax";
            this.tbMax.Size = new System.Drawing.Size(195, 20);
            this.tbMax.TabIndex = 45;
            this.tbMax.Text = "7";
            this.tbMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbMax.Validating += new System.ComponentModel.CancelEventHandler(this.tbMax_Validating);
            // 
            // tbMin
            // 
            this.tbMin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMin.Location = new System.Drawing.Point(321, 3);
            this.tbMin.Name = "tbMin";
            this.tbMin.Size = new System.Drawing.Size(192, 20);
            this.tbMin.TabIndex = 41;
            this.tbMin.Text = "0";
            this.tbMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbMin.Validating += new System.ComponentModel.CancelEventHandler(this.tbMin_Validating);
            // 
            // lblMax
            // 
            this.lblMax.AutoSize = true;
            this.lblMax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMax.Location = new System.Drawing.Point(519, 0);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(44, 28);
            this.lblMax.TabIndex = 36;
            this.lblMax.Text = "Max";
            this.lblMax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.formToolTip.SetToolTip(this.lblMax, "Maximum value");
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMin.Location = new System.Drawing.Point(271, 0);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(44, 28);
            this.lblMin.TabIndex = 34;
            this.lblMin.Text = "Min";
            this.lblMin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.formToolTip.SetToolTip(this.lblMin, "Minimum value");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 28);
            this.label1.TabIndex = 38;
            this.label1.Text = "Bits";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbLength
            // 
            this.tbLength.Location = new System.Drawing.Point(73, 3);
            this.tbLength.Name = "tbLength";
            this.tbLength.Size = new System.Drawing.Size(192, 20);
            this.tbLength.TabIndex = 39;
            this.tbLength.Text = "4";
            this.tbLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbLength.Validating += new System.ComponentModel.CancelEventHandler(this.tbLength_Validating);
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.Location = new System.Drawing.Point(73, 3);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(307, 20);
            this.tbName.TabIndex = 31;
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            this.tbName.Validating += new System.ComponentModel.CancelEventHandler(this.tbName_Validating);
            // 
            // tbCName
            // 
            this.tbCName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCName.Location = new System.Drawing.Point(456, 3);
            this.tbCName.Name = "tbCName";
            this.tbCName.Size = new System.Drawing.Size(308, 20);
            this.tbCName.TabIndex = 33;
            this.tbCName.Validating += new System.ComponentModel.CancelEventHandler(this.tbCName_Validating);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.lblName, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tbCName, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.tbName, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblCName, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(767, 33);
            this.tableLayoutPanel2.TabIndex = 31;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblName.Location = new System.Drawing.Point(3, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(64, 33);
            this.lblName.TabIndex = 34;
            this.lblName.Text = "Name";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.formToolTip.SetToolTip(this.lblName, "Hat name");
            // 
            // lblCName
            // 
            this.lblCName.AutoSize = true;
            this.lblCName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCName.Location = new System.Drawing.Point(386, 0);
            this.lblCName.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblCName.Name = "lblCName";
            this.lblCName.Size = new System.Drawing.Size(64, 30);
            this.lblCName.TabIndex = 33;
            this.lblCName.Text = "CName";
            this.lblCName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.formToolTip.SetToolTip(this.lblCName, "Constants name in generated code");
            // 
            // HatPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "HatPanel";
            this.Size = new System.Drawing.Size(767, 77);
            this.Controls.SetChildIndex(this.lblHead, 0);
            this.Controls.SetChildIndex(this.linkRemove, 0);
            this.Controls.SetChildIndex(this.linkUp, 0);
            this.Controls.SetChildIndex(this.linkDown, 0);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.Controls.SetChildIndex(this.tableLayoutPanel2, 0);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox tbMin;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbCName;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.TextBox tbMax;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblCName;
        private System.Windows.Forms.TextBox tbLength;

    }
}
