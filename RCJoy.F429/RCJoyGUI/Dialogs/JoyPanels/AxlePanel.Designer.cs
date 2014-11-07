namespace Tahorg.RCJoyGUI.JoyDialog
{
    partial class AxlePanel
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
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbCName = new System.Windows.Forms.TextBox();
            this.cbLength = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblMax = new System.Windows.Forms.Label();
            this.lblBits = new System.Windows.Forms.Label();
            this.tbMax = new System.Windows.Forms.TextBox();
            this.tbMin = new System.Windows.Forms.TextBox();
            this.lblMin = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHead
            // 
            this.lblHead.Size = new System.Drawing.Size(424, 15);
            // 
            // linkRemove
            // 
            this.linkRemove.Location = new System.Drawing.Point(407, 0);
            this.linkRemove.TabStop = false;
            // 
            // linkUp
            // 
            this.linkUp.Location = new System.Drawing.Point(386, 0);
            this.linkUp.TabStop = false;
            // 
            // linkDown
            // 
            this.linkDown.Location = new System.Drawing.Point(365, 0);
            this.linkDown.TabStop = false;
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.Location = new System.Drawing.Point(73, 3);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(137, 20);
            this.tbName.TabIndex = 1;
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            this.tbName.Validating += new System.ComponentModel.CancelEventHandler(this.tbName_Validating);
            // 
            // tbCName
            // 
            this.tbCName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCName.Location = new System.Drawing.Point(286, 3);
            this.tbCName.Name = "tbCName";
            this.tbCName.Size = new System.Drawing.Size(137, 20);
            this.tbCName.TabIndex = 3;
            this.tbCName.Validating += new System.ComponentModel.CancelEventHandler(this.tbCName_Validating);
            // 
            // cbLength
            // 
            this.cbLength.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbLength.FormattingEnabled = true;
            this.cbLength.Items.AddRange(new object[] {
            "1",
            "2",
            "4",
            "6",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"});
            this.cbLength.Location = new System.Drawing.Point(73, 3);
            this.cbLength.Name = "cbLength";
            this.cbLength.Size = new System.Drawing.Size(79, 21);
            this.cbLength.TabIndex = 23;
            this.cbLength.Text = "8";
            this.cbLength.Validating += new System.ComponentModel.CancelEventHandler(this.cbLength_Validating);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbCName, 3, 0);
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(-1, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(426, 25);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.formToolTip.SetToolTip(this.label1, "Axle name for designer");
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(216, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "CName";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.formToolTip.SetToolTip(this.label2, "Variable name for code generator");
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.lblMax, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblBits, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbLength, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tbMax, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.tbMin, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblMin, 2, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(-1, 42);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(425, 24);
            this.tableLayoutPanel2.TabIndex = 15;
            // 
            // lblMax
            // 
            this.lblMax.AutoSize = true;
            this.lblMax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMax.Location = new System.Drawing.Point(293, 0);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(44, 24);
            this.lblMax.TabIndex = 20;
            this.lblMax.Text = "Max";
            this.lblMax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.formToolTip.SetToolTip(this.lblMax, "Maximum logical");
            // 
            // lblBits
            // 
            this.lblBits.AutoSize = true;
            this.lblBits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBits.Location = new System.Drawing.Point(3, 0);
            this.lblBits.Name = "lblBits";
            this.lblBits.Size = new System.Drawing.Size(64, 24);
            this.lblBits.TabIndex = 22;
            this.lblBits.Text = "Bits";
            this.lblBits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.formToolTip.SetToolTip(this.lblBits, "Bits per Axle");
            // 
            // tbMax
            // 
            this.tbMax.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbMax.Location = new System.Drawing.Point(343, 3);
            this.tbMax.Name = "tbMax";
            this.tbMax.Size = new System.Drawing.Size(79, 20);
            this.tbMax.TabIndex = 25;
            this.tbMax.Text = "256";
            this.tbMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbMax.Validating += new System.ComponentModel.CancelEventHandler(this.tbMax_Validating);
            // 
            // tbMin
            // 
            this.tbMin.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbMin.Location = new System.Drawing.Point(208, 3);
            this.tbMin.Name = "tbMin";
            this.tbMin.Size = new System.Drawing.Size(79, 20);
            this.tbMin.TabIndex = 24;
            this.tbMin.Text = "0";
            this.tbMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbMin.Validating += new System.ComponentModel.CancelEventHandler(this.tbMin_Validating);
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMin.Location = new System.Drawing.Point(158, 0);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(44, 24);
            this.lblMin.TabIndex = 18;
            this.lblMin.Text = "Min";
            this.lblMin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.formToolTip.SetToolTip(this.lblMin, "Minimum logical");
            // 
            // AxlePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AxlePanel";
            this.Size = new System.Drawing.Size(424, 69);
            this.Tag = "0";
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.Controls.SetChildIndex(this.tableLayoutPanel2, 0);
            this.Controls.SetChildIndex(this.lblHead, 0);
            this.Controls.SetChildIndex(this.linkRemove, 0);
            this.Controls.SetChildIndex(this.linkUp, 0);
            this.Controls.SetChildIndex(this.linkDown, 0);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbCName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.Label lblBits;
        private System.Windows.Forms.ComboBox cbLength;
        private System.Windows.Forms.TextBox tbMax;
        private System.Windows.Forms.TextBox tbMin;
        private System.Windows.Forms.Label lblMin;
    }
}
