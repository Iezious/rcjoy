namespace Tahorg.RCJoyGUI.Dialogs
{
    partial class SendDebugJoyDialog
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnComSend = new System.Windows.Forms.Button();
            this.lblPortState = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbLines = new System.Windows.Forms.ListBox();
            this.ofdLog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.pnlButtons);
            this.groupBox1.Controls.Add(this.lblPortState);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(911, 51);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Send board debug data";
            // 
            // pnlButtons
            // 
            this.pnlButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlButtons.BackColor = System.Drawing.SystemColors.Control;
            this.pnlButtons.Controls.Add(this.btnRead);
            this.pnlButtons.Controls.Add(this.btnComSend);
            this.pnlButtons.Location = new System.Drawing.Point(691, 12);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(215, 33);
            this.pnlButtons.TabIndex = 4;
            // 
            // btnRead
            // 
            this.btnRead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRead.Location = new System.Drawing.Point(122, 4);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(86, 23);
            this.btnRead.TabIndex = 7;
            this.btnRead.Text = "Read file";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnComSend
            // 
            this.btnComSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnComSend.Location = new System.Drawing.Point(15, 4);
            this.btnComSend.Name = "btnComSend";
            this.btnComSend.Size = new System.Drawing.Size(101, 23);
            this.btnComSend.TabIndex = 6;
            this.btnComSend.Text = "Send selected";
            this.btnComSend.UseVisualStyleBackColor = true;
            this.btnComSend.Click += new System.EventHandler(this.btnComSend_Click);
            // 
            // lblPortState
            // 
            this.lblPortState.AutoSize = true;
            this.lblPortState.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPortState.ForeColor = System.Drawing.Color.Red;
            this.lblPortState.Location = new System.Drawing.Point(64, 20);
            this.lblPortState.Name = "lblPortState";
            this.lblPortState.Size = new System.Drawing.Size(108, 16);
            this.lblPortState.TabIndex = 3;
            this.lblPortState.Text = "Not connected";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Com port:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lbLines);
            this.panel1.Location = new System.Drawing.Point(12, 69);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(911, 584);
            this.panel1.TabIndex = 3;
            // 
            // lbLines
            // 
            this.lbLines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLines.Font = new System.Drawing.Font("Consolas", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLines.FormattingEnabled = true;
            this.lbLines.ItemHeight = 17;
            this.lbLines.Items.AddRange(new object[] {
            "Line1",
            "line 2",
            "line 3"});
            this.lbLines.Location = new System.Drawing.Point(0, 0);
            this.lbLines.Name = "lbLines";
            this.lbLines.Size = new System.Drawing.Size(911, 584);
            this.lbLines.TabIndex = 0;
            this.lbLines.SelectedValueChanged += new System.EventHandler(this.lbLines_SelectedValueChanged);
            // 
            // ofdLog
            // 
            this.ofdLog.DefaultExt = "txt";
            this.ofdLog.Filter = "Text files (*.txt)|*.txt|All files|*.*";
            this.ofdLog.RestoreDirectory = true;
            this.ofdLog.ShowReadOnly = true;
            // 
            // SendDebugJoyDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 665);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SendDebugJoyDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Debug joystick";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnComSend;
        private System.Windows.Forms.Label lblPortState;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox lbLines;
        private System.Windows.Forms.OpenFileDialog ofdLog;
    }
}