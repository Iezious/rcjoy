namespace Tahorg.RCJoyGUI
{
    partial class fmMain
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
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.recentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.boardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sketchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateJoyreaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateAndUploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eEPManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.joystickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createFromBoardCaptureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateModelDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelsTabSwitch = new System.Windows.Forms.TabControl();
            this.statusLine = new System.Windows.Forms.StatusStrip();
            this.statusFileStateLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripFileLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripCompileLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.fbdSelectArduino = new System.Windows.Forms.FolderBrowserDialog();
            this.fsdSave = new System.Windows.Forms.SaveFileDialog();
            this.ofdMain = new System.Windows.Forms.OpenFileDialog();
            this.liveDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip.SuspendLayout();
            this.statusLine.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.boardToolStripMenuItem,
            this.sketchToolStripMenuItem,
            this.joystickToolStripMenuItem,
            this.modelsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(1144, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.recentToolStripMenuItem,
            this.toolStripMenuItem3,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.newToolStripMenuItem.Text = "&New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveAsToolStripMenuItem.Text = "Save as ...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(143, 6);
            // 
            // recentToolStripMenuItem
            // 
            this.recentToolStripMenuItem.Name = "recentToolStripMenuItem";
            this.recentToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.recentToolStripMenuItem.Text = "Open recent";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(143, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // boardToolStripMenuItem
            // 
            this.boardToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.liveDataToolStripMenuItem,
            this.toolStripMenuItem1,
            this.disconnectToolStripMenuItem});
            this.boardToolStripMenuItem.Name = "boardToolStripMenuItem";
            this.boardToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.boardToolStripMenuItem.Text = "&Board";
            this.boardToolStripMenuItem.DropDownOpening += new System.EventHandler(this.boardToolStripMenuItem_DropDownOpening);
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.connectToolStripMenuItem.Text = "Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.disconnectToolStripMenuItem.Text = "Disconnect";
            this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.disconnectToolStripMenuItem_Click);
            // 
            // sketchToolStripMenuItem
            // 
            this.sketchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateJoyreaderToolStripMenuItem,
            this.generateProjectToolStripMenuItem,
            this.generateAndUploadToolStripMenuItem,
            this.eEPManagerToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.sketchToolStripMenuItem.Name = "sketchToolStripMenuItem";
            this.sketchToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.sketchToolStripMenuItem.Text = "Sketch";
            // 
            // generateJoyreaderToolStripMenuItem
            // 
            this.generateJoyreaderToolStripMenuItem.Name = "generateJoyreaderToolStripMenuItem";
            this.generateJoyreaderToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
            this.generateJoyreaderToolStripMenuItem.Text = "Generate Joyreader";
            this.generateJoyreaderToolStripMenuItem.Visible = false;
            // 
            // generateProjectToolStripMenuItem
            // 
            this.generateProjectToolStripMenuItem.Name = "generateProjectToolStripMenuItem";
            this.generateProjectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.X)));
            this.generateProjectToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
            this.generateProjectToolStripMenuItem.Text = "Generate project";
            this.generateProjectToolStripMenuItem.Click += new System.EventHandler(this.generateProjectToolStripMenuItem_Click);
            // 
            // generateAndUploadToolStripMenuItem
            // 
            this.generateAndUploadToolStripMenuItem.Name = "generateAndUploadToolStripMenuItem";
            this.generateAndUploadToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
            this.generateAndUploadToolStripMenuItem.Text = "Generate and upload";
            this.generateAndUploadToolStripMenuItem.Click += new System.EventHandler(this.generateAndUploadToolStripMenuItem_Click);
            // 
            // eEPManagerToolStripMenuItem
            // 
            this.eEPManagerToolStripMenuItem.Name = "eEPManagerToolStripMenuItem";
            this.eEPManagerToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
            this.eEPManagerToolStripMenuItem.Text = "EEP Manager";
            this.eEPManagerToolStripMenuItem.Click += new System.EventHandler(this.eEPManagerToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // joystickToolStripMenuItem
            // 
            this.joystickToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNewToolStripMenuItem,
            this.createFromBoardCaptureToolStripMenuItem,
            this.editConfigToolStripMenuItem});
            this.joystickToolStripMenuItem.Name = "joystickToolStripMenuItem";
            this.joystickToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.joystickToolStripMenuItem.Text = "&Joystick";
            // 
            // createNewToolStripMenuItem
            // 
            this.createNewToolStripMenuItem.Name = "createNewToolStripMenuItem";
            this.createNewToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.createNewToolStripMenuItem.Text = "Create new";
            this.createNewToolStripMenuItem.Click += new System.EventHandler(this.createNewToolStripMenuItem_Click);
            // 
            // createFromBoardCaptureToolStripMenuItem
            // 
            this.createFromBoardCaptureToolStripMenuItem.Name = "createFromBoardCaptureToolStripMenuItem";
            this.createFromBoardCaptureToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.createFromBoardCaptureToolStripMenuItem.Text = "Capture from board";
            this.createFromBoardCaptureToolStripMenuItem.Click += new System.EventHandler(this.createFromBoardCaptureToolStripMenuItem_Click);
            // 
            // editConfigToolStripMenuItem
            // 
            this.editConfigToolStripMenuItem.Name = "editConfigToolStripMenuItem";
            this.editConfigToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.editConfigToolStripMenuItem.Text = "Edit config";
            this.editConfigToolStripMenuItem.Click += new System.EventHandler(this.editConfigToolStripMenuItem_Click);
            // 
            // modelsToolStripMenuItem
            // 
            this.modelsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createModelToolStripMenuItem,
            this.updateModelDataToolStripMenuItem,
            this.toolStripMenuItem4,
            this.deleteModelToolStripMenuItem});
            this.modelsToolStripMenuItem.Name = "modelsToolStripMenuItem";
            this.modelsToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.modelsToolStripMenuItem.Text = "Mode&ls";
            // 
            // createModelToolStripMenuItem
            // 
            this.createModelToolStripMenuItem.Name = "createModelToolStripMenuItem";
            this.createModelToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.createModelToolStripMenuItem.Text = "&Create RC model";
            this.createModelToolStripMenuItem.Click += new System.EventHandler(this.createModelToolStripMenuItem_Click);
            // 
            // updateModelDataToolStripMenuItem
            // 
            this.updateModelDataToolStripMenuItem.Name = "updateModelDataToolStripMenuItem";
            this.updateModelDataToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.updateModelDataToolStripMenuItem.Text = "Update model data";
            this.updateModelDataToolStripMenuItem.Click += new System.EventHandler(this.updateModelDataToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(172, 6);
            // 
            // deleteModelToolStripMenuItem
            // 
            this.deleteModelToolStripMenuItem.Name = "deleteModelToolStripMenuItem";
            this.deleteModelToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.deleteModelToolStripMenuItem.Text = "&Delete model";
            this.deleteModelToolStripMenuItem.Click += new System.EventHandler(this.deleteModelToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // modelsTabSwitch
            // 
            this.modelsTabSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modelsTabSwitch.Location = new System.Drawing.Point(0, 26);
            this.modelsTabSwitch.Multiline = true;
            this.modelsTabSwitch.Name = "modelsTabSwitch";
            this.modelsTabSwitch.SelectedIndex = 0;
            this.modelsTabSwitch.Size = new System.Drawing.Size(1144, 796);
            this.modelsTabSwitch.TabIndex = 1;
            // 
            // statusLine
            // 
            this.statusLine.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusFileStateLabel,
            this.toolStripFileLabel,
            this.toolStripCompileLabel});
            this.statusLine.Location = new System.Drawing.Point(0, 826);
            this.statusLine.Name = "statusLine";
            this.statusLine.Size = new System.Drawing.Size(1144, 22);
            this.statusLine.TabIndex = 2;
            this.statusLine.Text = "statusStrip1";
            this.statusLine.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusLine_ItemClicked);
            // 
            // statusFileStateLabel
            // 
            this.statusFileStateLabel.AutoSize = false;
            this.statusFileStateLabel.Name = "statusFileStateLabel";
            this.statusFileStateLabel.Size = new System.Drawing.Size(160, 17);
            this.statusFileStateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripFileLabel
            // 
            this.toolStripFileLabel.Name = "toolStripFileLabel";
            this.toolStripFileLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripCompileLabel
            // 
            this.toolStripCompileLabel.Name = "toolStripCompileLabel";
            this.toolStripCompileLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // fbdSelectArduino
            // 
            this.fbdSelectArduino.RootFolder = System.Environment.SpecialFolder.CommonProgramFilesX86;
            // 
            // fsdSave
            // 
            this.fsdSave.DefaultExt = "xml";
            this.fsdSave.RestoreDirectory = true;
            this.fsdSave.SupportMultiDottedExtensions = true;
            this.fsdSave.Title = "Save project";
            // 
            // ofdMain
            // 
            this.ofdMain.DefaultExt = "xml";
            this.ofdMain.Filter = "XML files|*.xml|All files|*.*";
            this.ofdMain.RestoreDirectory = true;
            this.ofdMain.ShowReadOnly = true;
            this.ofdMain.SupportMultiDottedExtensions = true;
            // 
            // liveDataToolStripMenuItem
            // 
            this.liveDataToolStripMenuItem.Name = "liveDataToolStripMenuItem";
            this.liveDataToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.liveDataToolStripMenuItem.Text = "Live data";
            this.liveDataToolStripMenuItem.Click += new System.EventHandler(this.liveDataToolStripMenuItem_Click);
            // 
            // fmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 848);
            this.Controls.Add(this.statusLine);
            this.Controls.Add(this.modelsTabSwitch);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "fmMain";
            this.Text = "RC Joystick configurator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fmMain_FormClosing);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.statusLine.ResumeLayout(false);
            this.statusLine.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem recentToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem boardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem joystickToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createModelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteModelToolStripMenuItem;
        private System.Windows.Forms.TabControl modelsTabSwitch;
        private System.Windows.Forms.StatusStrip statusLine;
        private System.Windows.Forms.ToolStripMenuItem createFromBoardCaptureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog fbdSelectArduino;
        private System.Windows.Forms.SaveFileDialog fsdSave;
        private System.Windows.Forms.OpenFileDialog ofdMain;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sketchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateJoyreaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel statusFileStateLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripFileLabel;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateModelDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripCompileLabel;
        private System.Windows.Forms.ToolStripMenuItem generateAndUploadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eEPManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem liveDataToolStripMenuItem;

    }
}

