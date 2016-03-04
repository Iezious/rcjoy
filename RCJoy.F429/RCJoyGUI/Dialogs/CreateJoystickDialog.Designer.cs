namespace Tahorg.RCJoyGUI.Dialogs
{
    partial class CreateJoystickDialog
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
            this.components = new System.ComponentModel.Container();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.btnAdd = new WindowsFormsToolkit.Controls.SplitButton();
            this.addContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.axisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hatswitchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dummyBitsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCancek = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.pnlScroll = new System.Windows.Forms.Panel();
            this.pnlName = new System.Windows.Forms.Panel();
            this.tbVendorProduct = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.toolTipContainer = new System.Windows.Forms.ToolTip(this.components);
            this.btnExport = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.sfdExport = new System.Windows.Forms.SaveFileDialog();
            this.ofdImport = new System.Windows.Forms.OpenFileDialog();
            this.bottomPanel.SuspendLayout();
            this.addContextMenu.SuspendLayout();
            this.pnlName.SuspendLayout();
            this.SuspendLayout();
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.btnImport);
            this.bottomPanel.Controls.Add(this.btnExport);
            this.bottomPanel.Controls.Add(this.btnAdd);
            this.bottomPanel.Controls.Add(this.btnCancek);
            this.bottomPanel.Controls.Add(this.btnOK);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 519);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(487, 46);
            this.bottomPanel.TabIndex = 3;
            // 
            // btnAdd
            // 
            this.btnAdd.AutoSize = true;
            this.btnAdd.ContextMenuStrip = this.addContextMenu;
            this.btnAdd.Location = new System.Drawing.Point(12, 11);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(67, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // addContextMenu
            // 
            this.addContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.axisToolStripMenuItem,
            this.buttonToolStripMenuItem,
            this.hatswitchToolStripMenuItem,
            this.dummyBitsToolStripMenuItem});
            this.addContextMenu.Name = "addContextMenu";
            this.addContextMenu.Size = new System.Drawing.Size(140, 92);
            // 
            // axisToolStripMenuItem
            // 
            this.axisToolStripMenuItem.Name = "axisToolStripMenuItem";
            this.axisToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.axisToolStripMenuItem.Text = "Axis";
            this.axisToolStripMenuItem.Click += new System.EventHandler(this.axisToolStripMenuItem_Click);
            // 
            // buttonToolStripMenuItem
            // 
            this.buttonToolStripMenuItem.Name = "buttonToolStripMenuItem";
            this.buttonToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.buttonToolStripMenuItem.Text = "Button";
            this.buttonToolStripMenuItem.Click += new System.EventHandler(this.buttonToolStripMenuItem_Click);
            // 
            // hatswitchToolStripMenuItem
            // 
            this.hatswitchToolStripMenuItem.Name = "hatswitchToolStripMenuItem";
            this.hatswitchToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.hatswitchToolStripMenuItem.Text = "Hatswitch";
            this.hatswitchToolStripMenuItem.Click += new System.EventHandler(this.hatswitchToolStripMenuItem_Click);
            // 
            // dummyBitsToolStripMenuItem
            // 
            this.dummyBitsToolStripMenuItem.Name = "dummyBitsToolStripMenuItem";
            this.dummyBitsToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.dummyBitsToolStripMenuItem.Text = "Dummy bits";
            this.dummyBitsToolStripMenuItem.Click += new System.EventHandler(this.dummyBitsToolStripMenuItem_Click);
            // 
            // btnCancek
            // 
            this.btnCancek.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancek.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancek.Location = new System.Drawing.Point(319, 11);
            this.btnCancek.Name = "btnCancek";
            this.btnCancek.Size = new System.Drawing.Size(75, 23);
            this.btnCancek.TabIndex = 2;
            this.btnCancek.Text = "Cancel";
            this.btnCancek.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(400, 11);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pnlScroll
            // 
            this.pnlScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlScroll.AutoScroll = true;
            this.pnlScroll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlScroll.Location = new System.Drawing.Point(0, 34);
            this.pnlScroll.Name = "pnlScroll";
            this.pnlScroll.Padding = new System.Windows.Forms.Padding(3);
            this.pnlScroll.Size = new System.Drawing.Size(487, 484);
            this.pnlScroll.TabIndex = 2;
            // 
            // pnlName
            // 
            this.pnlName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlName.Controls.Add(this.tbVendorProduct);
            this.pnlName.Controls.Add(this.tbName);
            this.pnlName.Controls.Add(this.label1);
            this.pnlName.Controls.Add(this.lblName);
            this.pnlName.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlName.Location = new System.Drawing.Point(0, 0);
            this.pnlName.Name = "pnlName";
            this.pnlName.Size = new System.Drawing.Size(487, 33);
            this.pnlName.TabIndex = 1;
            // 
            // tbVendorProduct
            // 
            this.tbVendorProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbVendorProduct.Location = new System.Drawing.Point(349, 5);
            this.tbVendorProduct.Name = "tbVendorProduct";
            this.tbVendorProduct.Size = new System.Drawing.Size(125, 20);
            this.tbVendorProduct.TabIndex = 3;
            this.tbVendorProduct.Validating += new System.ComponentModel.CancelEventHandler(this.tbCName_Validating);
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.Location = new System.Drawing.Point(88, 5);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(207, 20);
            this.tbName.TabIndex = 1;
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            this.tbName.Validating += new System.ComponentModel.CancelEventHandler(this.tbName_Validating);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(301, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "VID:PID";
            this.toolTipContainer.SetToolTip(this.label1, "Name for generate code");
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(8, 8);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(74, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Joystick name";
            this.toolTipContainer.SetToolTip(this.lblName, "Short name for display");
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(113, 11);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 4;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.Location = new System.Drawing.Point(194, 11);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 5;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // sfdExport
            // 
            this.sfdExport.DefaultExt = "xml";
            this.sfdExport.Filter = "*.xml|XML files|*.*|All files";
            this.sfdExport.RestoreDirectory = true;
            // 
            // ofdImport
            // 
            this.ofdImport.DefaultExt = "xml";
            this.ofdImport.Filter = "*.xml|XML files|*.*|All files";
            this.ofdImport.RestoreDirectory = true;
            this.ofdImport.ShowReadOnly = true;
            // 
            // CreateJoystickDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancek;
            this.ClientSize = new System.Drawing.Size(487, 565);
            this.Controls.Add(this.pnlName);
            this.Controls.Add(this.pnlScroll);
            this.Controls.Add(this.bottomPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(380, 350);
            this.Name = "CreateJoystickDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CreateJoystickDialog";
            this.bottomPanel.ResumeLayout(false);
            this.bottomPanel.PerformLayout();
            this.addContextMenu.ResumeLayout(false);
            this.pnlName.ResumeLayout(false);
            this.pnlName.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Panel pnlScroll;
        private System.Windows.Forms.ContextMenuStrip addContextMenu;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ToolStripMenuItem axisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buttonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hatswitchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dummyBitsToolStripMenuItem;
        private System.Windows.Forms.Button btnCancek;
        private WindowsFormsToolkit.Controls.SplitButton btnAdd;
        private System.Windows.Forms.Panel pnlName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTipContainer;
        private System.Windows.Forms.TextBox tbVendorProduct;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.SaveFileDialog sfdExport;
        private System.Windows.Forms.OpenFileDialog ofdImport;
    }
}