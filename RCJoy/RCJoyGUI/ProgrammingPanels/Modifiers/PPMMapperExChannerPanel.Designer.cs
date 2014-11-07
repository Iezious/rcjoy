namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    partial class PPMMapperExChannerPanel
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cbFRAM = new System.Windows.Forms.CheckBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbMin = new System.Windows.Forms.TextBox();
            this.tbMiddle = new System.Windows.Forms.TextBox();
            this.tbMax = new System.Windows.Forms.TextBox();
            this.lblValue = new System.Windows.Forms.Label();
            this.lnkIn = new Tahorg.RCJoyGUI.LinkPoint();
            this.lnkOut = new Tahorg.RCJoyGUI.LinkPoint();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(14, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(365, 25);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.cbFRAM, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbMin, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbMiddle, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbMax, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblValue, 5, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(365, 25);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // cbFRAM
            // 
            this.cbFRAM.AutoSize = true;
            this.cbFRAM.Location = new System.Drawing.Point(3, 6);
            this.cbFRAM.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.cbFRAM.Name = "cbFRAM";
            this.cbFRAM.Size = new System.Drawing.Size(14, 14);
            this.cbFRAM.TabIndex = 0;
            this.cbFRAM.UseVisualStyleBackColor = true;
            this.cbFRAM.CheckedChanged += new System.EventHandler(this.cbFRAM_CheckedChanged);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(23, 3);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(114, 20);
            this.tbName.TabIndex = 1;
            // 
            // tbMin
            // 
            this.tbMin.Location = new System.Drawing.Point(143, 3);
            this.tbMin.Name = "tbMin";
            this.tbMin.Size = new System.Drawing.Size(54, 20);
            this.tbMin.TabIndex = 2;
            this.tbMin.Text = "0";
            this.tbMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbMiddle
            // 
            this.tbMiddle.Location = new System.Drawing.Point(203, 3);
            this.tbMiddle.Name = "tbMiddle";
            this.tbMiddle.Size = new System.Drawing.Size(54, 20);
            this.tbMiddle.TabIndex = 3;
            this.tbMiddle.Text = "500";
            this.tbMiddle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbMax
            // 
            this.tbMax.Location = new System.Drawing.Point(263, 3);
            this.tbMax.Name = "tbMax";
            this.tbMax.Size = new System.Drawing.Size(54, 20);
            this.tbMax.TabIndex = 4;
            this.tbMax.Text = "1000";
            this.tbMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValue.Location = new System.Drawing.Point(323, 0);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(39, 25);
            this.lblValue.TabIndex = 5;
            this.lblValue.Text = "XXXX";
            this.lblValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lnkIn
            // 
            this.lnkIn.DataMapIdx = ((short)(0));
            this.lnkIn.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkIn.HolderPanel = null;
            this.lnkIn.IsSelected = false;
            this.lnkIn.LinkType = Tahorg.RCJoyGUI.enLinkType.Axle;
            this.lnkIn.Location = new System.Drawing.Point(-3, 6);
            this.lnkIn.MappedValueName = null;
            this.lnkIn.Name = "lnkIn";
            this.lnkIn.Size = new System.Drawing.Size(12, 12);
            this.lnkIn.TabIndex = 1;
            this.lnkIn.Text = "linkPoint1";
            // 
            // lnkOut
            // 
            this.lnkOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkOut.DataMapIdx = ((short)(0));
            this.lnkOut.Direction = Tahorg.RCJoyGUI.enLinkDirection.Output;
            this.lnkOut.HolderPanel = null;
            this.lnkOut.IsSelected = false;
            this.lnkOut.LinkType = Tahorg.RCJoyGUI.enLinkType.Axle;
            this.lnkOut.Location = new System.Drawing.Point(386, 6);
            this.lnkOut.MappedValueName = null;
            this.lnkOut.Name = "lnkOut";
            this.lnkOut.Size = new System.Drawing.Size(12, 12);
            this.lnkOut.TabIndex = 2;
            this.lnkOut.Text = "linkPoint2";
            // 
            // PPMMapperExChannerPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lnkOut);
            this.Controls.Add(this.lnkIn);
            this.Controls.Add(this.panel1);
            this.Name = "PPMMapperExChannerPanel";
            this.Size = new System.Drawing.Size(395, 27);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox cbFRAM;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbMin;
        private System.Windows.Forms.TextBox tbMiddle;
        private System.Windows.Forms.TextBox tbMax;
        private LinkPoint lnkIn;
        private LinkPoint lnkOut;
        private System.Windows.Forms.Label lblValue;
    }
}
