namespace Tahorg.RCJoyGUI.Dialogs
{
    partial class EEPROMMapVariablePanel
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
            this.lblModel = new System.Windows.Forms.Label();
            this.lblVar = new System.Windows.Forms.Label();
            this.tbAddr = new System.Windows.Forms.TextBox();
            this.tbDefValue = new System.Windows.Forms.TextBox();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.lblModel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblVar, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbAddr, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbDefValue, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbValue, 4, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(446, 27);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Click += new System.EventHandler(this.pnl_Click);
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblModel.Location = new System.Drawing.Point(4, 1);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(82, 25);
            this.lblModel.TabIndex = 0;
            this.lblModel.Text = "label1";
            this.lblModel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblModel.Click += new System.EventHandler(this.pnl_Click);
            // 
            // lblVar
            // 
            this.lblVar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVar.Location = new System.Drawing.Point(93, 1);
            this.lblVar.Name = "lblVar";
            this.lblVar.Size = new System.Drawing.Size(82, 25);
            this.lblVar.TabIndex = 1;
            this.lblVar.Text = "label2";
            this.lblVar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblVar.Click += new System.EventHandler(this.pnl_Click);
            // 
            // tbAddr
            // 
            this.tbAddr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbAddr.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbAddr.Location = new System.Drawing.Point(182, 4);
            this.tbAddr.Name = "tbAddr";
            this.tbAddr.ReadOnly = true;
            this.tbAddr.Size = new System.Drawing.Size(82, 20);
            this.tbAddr.TabIndex = 2;
            this.tbAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbDefValue
            // 
            this.tbDefValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDefValue.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbDefValue.Location = new System.Drawing.Point(271, 4);
            this.tbDefValue.Name = "tbDefValue";
            this.tbDefValue.ReadOnly = true;
            this.tbDefValue.Size = new System.Drawing.Size(82, 20);
            this.tbDefValue.TabIndex = 3;
            this.tbDefValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbValue
            // 
            this.tbValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbValue.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbValue.Location = new System.Drawing.Point(360, 4);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(82, 20);
            this.tbValue.TabIndex = 4;
            this.tbValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // EEPROMMapVariablePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "EEPROMMapVariablePanel";
            this.Size = new System.Drawing.Size(446, 27);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.Label lblVar;
        private System.Windows.Forms.TextBox tbAddr;
        private System.Windows.Forms.TextBox tbDefValue;
        private System.Windows.Forms.TextBox tbValue;
    }
}
