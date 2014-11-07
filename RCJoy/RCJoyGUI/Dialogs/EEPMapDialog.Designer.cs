namespace Tahorg.RCJoyGUI.Dialogs
{
    partial class EEPMapDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnReadSelected = new System.Windows.Forms.Button();
            this.btnWriteAll = new System.Windows.Forms.Button();
            this.btnResetSelected = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnWriteSelected = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.pnlVariables = new System.Windows.Forms.Panel();
            this.tblVariables = new System.Windows.Forms.TableLayoutPanel();
            this.tblHeader = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.pnlButtons.SuspendLayout();
            this.pnlVariables.SuspendLayout();
            this.tblVariables.SuspendLayout();
            this.tblHeader.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlButtons
            // 
            this.pnlButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlButtons.Controls.Add(this.btnReadSelected);
            this.pnlButtons.Controls.Add(this.btnWriteAll);
            this.pnlButtons.Controls.Add(this.btnResetSelected);
            this.pnlButtons.Controls.Add(this.btnRead);
            this.pnlButtons.Controls.Add(this.btnWriteSelected);
            this.pnlButtons.Controls.Add(this.btnReset);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlButtons.Location = new System.Drawing.Point(0, 0);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(811, 33);
            this.pnlButtons.TabIndex = 0;
            // 
            // btnReadSelected
            // 
            this.btnReadSelected.AutoSize = true;
            this.btnReadSelected.Location = new System.Drawing.Point(90, 3);
            this.btnReadSelected.Name = "btnReadSelected";
            this.btnReadSelected.Size = new System.Drawing.Size(86, 23);
            this.btnReadSelected.TabIndex = 5;
            this.btnReadSelected.Text = "Read selected";
            this.btnReadSelected.UseVisualStyleBackColor = true;
            this.btnReadSelected.Click += new System.EventHandler(this.btnReadSelected_Click);
            // 
            // btnWriteAll
            // 
            this.btnWriteAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWriteAll.AutoSize = true;
            this.btnWriteAll.Location = new System.Drawing.Point(401, 3);
            this.btnWriteAll.Name = "btnWriteAll";
            this.btnWriteAll.Size = new System.Drawing.Size(89, 23);
            this.btnWriteAll.TabIndex = 4;
            this.btnWriteAll.Text = "Write all values";
            this.btnWriteAll.UseVisualStyleBackColor = true;
            this.btnWriteAll.Click += new System.EventHandler(this.btnWriteAll_Click);
            // 
            // btnResetSelected
            // 
            this.btnResetSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetSelected.AutoSize = true;
            this.btnResetSelected.Location = new System.Drawing.Point(573, 3);
            this.btnResetSelected.Name = "btnResetSelected";
            this.btnResetSelected.Size = new System.Drawing.Size(88, 23);
            this.btnResetSelected.TabIndex = 3;
            this.btnResetSelected.Text = "Reset selected";
            this.btnResetSelected.UseVisualStyleBackColor = true;
            this.btnResetSelected.Click += new System.EventHandler(this.btnResetSelected_Click);
            // 
            // btnRead
            // 
            this.btnRead.AutoSize = true;
            this.btnRead.Location = new System.Drawing.Point(7, 3);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(77, 23);
            this.btnRead.TabIndex = 2;
            this.btnRead.Text = "Read values";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnWriteSelected
            // 
            this.btnWriteSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWriteSelected.AutoSize = true;
            this.btnWriteSelected.Location = new System.Drawing.Point(276, 3);
            this.btnWriteSelected.Name = "btnWriteSelected";
            this.btnWriteSelected.Size = new System.Drawing.Size(119, 23);
            this.btnWriteSelected.TabIndex = 1;
            this.btnWriteSelected.Text = "Write selected values";
            this.btnWriteSelected.UseVisualStyleBackColor = true;
            this.btnWriteSelected.Click += new System.EventHandler(this.btnWriteSelected_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.AutoSize = true;
            this.btnReset.Location = new System.Drawing.Point(667, 3);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(139, 23);
            this.btnReset.TabIndex = 0;
            this.btnReset.Text = "Reset all to default values";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // pnlVariables
            // 
            this.pnlVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlVariables.AutoScroll = true;
            this.pnlVariables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlVariables.Controls.Add(this.tblVariables);
            this.pnlVariables.Location = new System.Drawing.Point(0, 33);
            this.pnlVariables.Name = "pnlVariables";
            this.pnlVariables.Size = new System.Drawing.Size(811, 368);
            this.pnlVariables.TabIndex = 1;
            // 
            // tblVariables
            // 
            this.tblVariables.AutoSize = true;
            this.tblVariables.ColumnCount = 1;
            this.tblVariables.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblVariables.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblVariables.Controls.Add(this.tblHeader, 0, 0);
            this.tblVariables.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblVariables.Location = new System.Drawing.Point(0, 0);
            this.tblVariables.Name = "tblVariables";
            this.tblVariables.RowCount = 1;
            this.tblVariables.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblVariables.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblVariables.Size = new System.Drawing.Size(809, 24);
            this.tblVariables.TabIndex = 1;
            // 
            // tblHeader
            // 
            this.tblHeader.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tblHeader.ColumnCount = 5;
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblHeader.Controls.Add(this.label1, 0, 0);
            this.tblHeader.Controls.Add(this.label2, 1, 0);
            this.tblHeader.Controls.Add(this.label3, 2, 0);
            this.tblHeader.Controls.Add(this.label4, 3, 0);
            this.tblHeader.Controls.Add(this.label5, 4, 0);
            this.tblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblHeader.Location = new System.Drawing.Point(3, 3);
            this.tblHeader.Name = "tblHeader";
            this.tblHeader.RowCount = 1;
            this.tblHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblHeader.Size = new System.Drawing.Size(803, 18);
            this.tblHeader.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Model";
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(164, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Variable";
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(324, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Address";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(484, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(153, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Default value";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(644, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(155, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Current value";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.sbProgress});
            this.statusStrip1.Location = new System.Drawing.Point(0, 402);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(811, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(82, 17);
            this.statusLabel.Text = "No connected";
            // 
            // sbProgress
            // 
            this.sbProgress.Name = "sbProgress";
            this.sbProgress.Size = new System.Drawing.Size(200, 16);
            // 
            // EEPMapDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 424);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pnlVariables);
            this.Controls.Add(this.pnlButtons);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EEPMapDialog";
            this.Text = "EEP/FRAM Manager";
            this.pnlButtons.ResumeLayout(false);
            this.pnlButtons.PerformLayout();
            this.pnlVariables.ResumeLayout(false);
            this.pnlVariables.PerformLayout();
            this.tblVariables.ResumeLayout(false);
            this.tblHeader.ResumeLayout(false);
            this.tblHeader.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Panel pnlVariables;
        private System.Windows.Forms.TableLayoutPanel tblVariables;
        private System.Windows.Forms.TableLayoutPanel tblHeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnWriteSelected;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripProgressBar sbProgress;
        private System.Windows.Forms.Button btnResetSelected;
        private System.Windows.Forms.Button btnWriteAll;
        private System.Windows.Forms.Button btnReadSelected;

    }
}