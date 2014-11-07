namespace Tahorg.RCJoyGUI
{
    partial class DesignPanel
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.joystickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuActionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuActionsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.modelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pPMOUTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flightModesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelSelectorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.axismodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trimmerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modeTrimmerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invertorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centeredExpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rangeExpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pPMMaperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mixersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deltaMixToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flaperonsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alieronsRudderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.throttleElevatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vTailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logicalButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.switchingButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.switchingStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonHoldSwitchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hatValueSwitcherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hatButtonSwitcherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.variablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.constantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.temporalVariableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eEPROMVariableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.valueAxisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unlinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.trimmerEmulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.valueTrimmerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.linkToolStripMenuItem,
            this.unlinkToolStripMenuItem,
            this.toolStripMenuItem1,
            this.removeToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(151, 98);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.joystickToolStripMenuItem,
            this.menuActionsToolStripMenuItem,
            this.modelToolStripMenuItem,
            this.modelSelectorToolStripMenuItem,
            this.axismodToolStripMenuItem,
            this.mixersToolStripMenuItem,
            this.buttonsToolStripMenuItem,
            this.variablesToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.addToolStripMenuItem.Text = "Add";
            // 
            // joystickToolStripMenuItem
            // 
            this.joystickToolStripMenuItem.Name = "joystickToolStripMenuItem";
            this.joystickToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.joystickToolStripMenuItem.Text = "Joystick";
            this.joystickToolStripMenuItem.Click += new System.EventHandler(this.joystickToolStripMenuItem_Click);
            // 
            // menuActionsToolStripMenuItem
            // 
            this.menuActionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMenuToolStripMenuItem,
            this.menuActionsToolStripMenuItem1});
            this.menuActionsToolStripMenuItem.Name = "menuActionsToolStripMenuItem";
            this.menuActionsToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.menuActionsToolStripMenuItem.Text = "System menu";
            // 
            // openMenuToolStripMenuItem
            // 
            this.openMenuToolStripMenuItem.Name = "openMenuToolStripMenuItem";
            this.openMenuToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openMenuToolStripMenuItem.Text = "Open menu";
            this.openMenuToolStripMenuItem.Click += new System.EventHandler(this.openMenuToolStripMenuItem_Click);
            // 
            // menuActionsToolStripMenuItem1
            // 
            this.menuActionsToolStripMenuItem1.Name = "menuActionsToolStripMenuItem1";
            this.menuActionsToolStripMenuItem1.Size = new System.Drawing.Size(146, 22);
            this.menuActionsToolStripMenuItem1.Text = "Menu actions";
            this.menuActionsToolStripMenuItem1.Click += new System.EventHandler(this.menuActionsToolStripMenuItem1_Click);
            // 
            // modelToolStripMenuItem
            // 
            this.modelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pPMOUTToolStripMenuItem,
            this.flightModesToolStripMenuItem});
            this.modelToolStripMenuItem.Name = "modelToolStripMenuItem";
            this.modelToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.modelToolStripMenuItem.Text = "Model";
            // 
            // pPMOUTToolStripMenuItem
            // 
            this.pPMOUTToolStripMenuItem.Name = "pPMOUTToolStripMenuItem";
            this.pPMOUTToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.pPMOUTToolStripMenuItem.Text = "PPM OUT";
            this.pPMOUTToolStripMenuItem.Click += new System.EventHandler(this.pPMOUTToolStripMenuItem_Click);
            // 
            // flightModesToolStripMenuItem
            // 
            this.flightModesToolStripMenuItem.Name = "flightModesToolStripMenuItem";
            this.flightModesToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.flightModesToolStripMenuItem.Text = "Flight mode PPM OUT";
            this.flightModesToolStripMenuItem.Click += new System.EventHandler(this.flightModesToolStripMenuItem_Click);
            // 
            // modelSelectorToolStripMenuItem
            // 
            this.modelSelectorToolStripMenuItem.Name = "modelSelectorToolStripMenuItem";
            this.modelSelectorToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.modelSelectorToolStripMenuItem.Text = "Model Selector";
            this.modelSelectorToolStripMenuItem.Click += new System.EventHandler(this.modelSelectorToolStripMenuItem_Click);
            // 
            // axismodToolStripMenuItem
            // 
            this.axismodToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trimmerToolStripMenuItem,
            this.modeTrimmerToolStripMenuItem,
            this.trimmerEmulationToolStripMenuItem,
            this.valueTrimmerToolStripMenuItem,
            this.invertorToolStripMenuItem,
            this.centeredExpToolStripMenuItem,
            this.rangeExpToolStripMenuItem,
            this.pPMMaperToolStripMenuItem});
            this.axismodToolStripMenuItem.Name = "axismodToolStripMenuItem";
            this.axismodToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.axismodToolStripMenuItem.Text = "Axis modificators";
            // 
            // trimmerToolStripMenuItem
            // 
            this.trimmerToolStripMenuItem.Name = "trimmerToolStripMenuItem";
            this.trimmerToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.trimmerToolStripMenuItem.Text = "Trimmer";
            this.trimmerToolStripMenuItem.Click += new System.EventHandler(this.trimmerToolStripMenuItem_Click);
            // 
            // modeTrimmerToolStripMenuItem
            // 
            this.modeTrimmerToolStripMenuItem.Name = "modeTrimmerToolStripMenuItem";
            this.modeTrimmerToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.modeTrimmerToolStripMenuItem.Text = "Mode trimmer";
            this.modeTrimmerToolStripMenuItem.Click += new System.EventHandler(this.modeTrimmerToolStripMenuItem_Click);
            // 
            // invertorToolStripMenuItem
            // 
            this.invertorToolStripMenuItem.Name = "invertorToolStripMenuItem";
            this.invertorToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.invertorToolStripMenuItem.Text = "Invertor";
            this.invertorToolStripMenuItem.Click += new System.EventHandler(this.invertorToolStripMenuItem_Click);
            // 
            // centeredExpToolStripMenuItem
            // 
            this.centeredExpToolStripMenuItem.Name = "centeredExpToolStripMenuItem";
            this.centeredExpToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.centeredExpToolStripMenuItem.Text = "Centered Exp";
            this.centeredExpToolStripMenuItem.Click += new System.EventHandler(this.centeredExpToolStripMenuItem_Click);
            // 
            // rangeExpToolStripMenuItem
            // 
            this.rangeExpToolStripMenuItem.Name = "rangeExpToolStripMenuItem";
            this.rangeExpToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.rangeExpToolStripMenuItem.Text = "Range Exp";
            this.rangeExpToolStripMenuItem.Click += new System.EventHandler(this.rangeExpToolStripMenuItem_Click);
            // 
            // pPMMaperToolStripMenuItem
            // 
            this.pPMMaperToolStripMenuItem.Name = "pPMMaperToolStripMenuItem";
            this.pPMMaperToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.pPMMaperToolStripMenuItem.Text = "PPM Maper";
            this.pPMMaperToolStripMenuItem.Click += new System.EventHandler(this.pPMMaperToolStripMenuItem_Click);
            // 
            // mixersToolStripMenuItem
            // 
            this.mixersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deltaMixToolStripMenuItem,
            this.flaperonsToolStripMenuItem,
            this.alieronsRudderToolStripMenuItem,
            this.throttleElevatorToolStripMenuItem,
            this.vTailToolStripMenuItem});
            this.mixersToolStripMenuItem.Name = "mixersToolStripMenuItem";
            this.mixersToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.mixersToolStripMenuItem.Text = "Mixers";
            // 
            // deltaMixToolStripMenuItem
            // 
            this.deltaMixToolStripMenuItem.Name = "deltaMixToolStripMenuItem";
            this.deltaMixToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.deltaMixToolStripMenuItem.Text = "Delta mix";
            this.deltaMixToolStripMenuItem.Click += new System.EventHandler(this.deltaMixToolStripMenuItem_Click);
            // 
            // flaperonsToolStripMenuItem
            // 
            this.flaperonsToolStripMenuItem.Name = "flaperonsToolStripMenuItem";
            this.flaperonsToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.flaperonsToolStripMenuItem.Text = "Flaperons";
            this.flaperonsToolStripMenuItem.Click += new System.EventHandler(this.flaperonsToolStripMenuItem_Click);
            // 
            // alieronsRudderToolStripMenuItem
            // 
            this.alieronsRudderToolStripMenuItem.Name = "alieronsRudderToolStripMenuItem";
            this.alieronsRudderToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.alieronsRudderToolStripMenuItem.Text = "Alierons -> Rudder";
            this.alieronsRudderToolStripMenuItem.Click += new System.EventHandler(this.alieronsRudderToolStripMenuItem_Click);
            // 
            // throttleElevatorToolStripMenuItem
            // 
            this.throttleElevatorToolStripMenuItem.Name = "throttleElevatorToolStripMenuItem";
            this.throttleElevatorToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.throttleElevatorToolStripMenuItem.Text = "Throttle -> Elevator";
            this.throttleElevatorToolStripMenuItem.Click += new System.EventHandler(this.throttleElevatorToolStripMenuItem_Click);
            // 
            // vTailToolStripMenuItem
            // 
            this.vTailToolStripMenuItem.Name = "vTailToolStripMenuItem";
            this.vTailToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.vTailToolStripMenuItem.Text = "V-Tail";
            this.vTailToolStripMenuItem.Click += new System.EventHandler(this.vTailToolStripMenuItem_Click);
            // 
            // buttonsToolStripMenuItem
            // 
            this.buttonsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logicalButtonToolStripMenuItem,
            this.switchingButtonToolStripMenuItem,
            this.switchingStateToolStripMenuItem,
            this.buttonHoldSwitchToolStripMenuItem,
            this.hatValueSwitcherToolStripMenuItem,
            this.hatButtonSwitcherToolStripMenuItem});
            this.buttonsToolStripMenuItem.Name = "buttonsToolStripMenuItem";
            this.buttonsToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.buttonsToolStripMenuItem.Text = "Buttons";
            // 
            // logicalButtonToolStripMenuItem
            // 
            this.logicalButtonToolStripMenuItem.Name = "logicalButtonToolStripMenuItem";
            this.logicalButtonToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.logicalButtonToolStripMenuItem.Text = "Logical button";
            this.logicalButtonToolStripMenuItem.Click += new System.EventHandler(this.logicalButtonToolStripMenuItem_Click);
            // 
            // switchingButtonToolStripMenuItem
            // 
            this.switchingButtonToolStripMenuItem.Enabled = false;
            this.switchingButtonToolStripMenuItem.Name = "switchingButtonToolStripMenuItem";
            this.switchingButtonToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.switchingButtonToolStripMenuItem.Text = "Switch button";
            this.switchingButtonToolStripMenuItem.Click += new System.EventHandler(this.switchingButtonToolStripMenuItem_Click);
            // 
            // switchingStateToolStripMenuItem
            // 
            this.switchingStateToolStripMenuItem.Name = "switchingStateToolStripMenuItem";
            this.switchingStateToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.switchingStateToolStripMenuItem.Text = "Button value switch";
            this.switchingStateToolStripMenuItem.Click += new System.EventHandler(this.switchingStateToolStripMenuItem_Click);
            // 
            // buttonHoldSwitchToolStripMenuItem
            // 
            this.buttonHoldSwitchToolStripMenuItem.Name = "buttonHoldSwitchToolStripMenuItem";
            this.buttonHoldSwitchToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.buttonHoldSwitchToolStripMenuItem.Text = "Button hold switch";
            this.buttonHoldSwitchToolStripMenuItem.Click += new System.EventHandler(this.buttonHoldSwitchToolStripMenuItem_Click);
            // 
            // hatValueSwitcherToolStripMenuItem
            // 
            this.hatValueSwitcherToolStripMenuItem.Name = "hatValueSwitcherToolStripMenuItem";
            this.hatValueSwitcherToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.hatValueSwitcherToolStripMenuItem.Text = "Value -> Value mapper";
            this.hatValueSwitcherToolStripMenuItem.Click += new System.EventHandler(this.hatValueSwitcherToolStripMenuItem_Click);
            // 
            // hatButtonSwitcherToolStripMenuItem
            // 
            this.hatButtonSwitcherToolStripMenuItem.Name = "hatButtonSwitcherToolStripMenuItem";
            this.hatButtonSwitcherToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.hatButtonSwitcherToolStripMenuItem.Text = "Hat -> Button";
            this.hatButtonSwitcherToolStripMenuItem.Click += new System.EventHandler(this.hatButtonSwitcherToolStripMenuItem_Click);
            // 
            // variablesToolStripMenuItem
            // 
            this.variablesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.constantToolStripMenuItem,
            this.temporalVariableToolStripMenuItem,
            this.eEPROMVariableToolStripMenuItem,
            this.valueAxisToolStripMenuItem});
            this.variablesToolStripMenuItem.Name = "variablesToolStripMenuItem";
            this.variablesToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.variablesToolStripMenuItem.Text = "Variables";
            // 
            // constantToolStripMenuItem
            // 
            this.constantToolStripMenuItem.Name = "constantToolStripMenuItem";
            this.constantToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.constantToolStripMenuItem.Text = "Constant";
            this.constantToolStripMenuItem.Click += new System.EventHandler(this.constantToolStripMenuItem_Click);
            // 
            // temporalVariableToolStripMenuItem
            // 
            this.temporalVariableToolStripMenuItem.Name = "temporalVariableToolStripMenuItem";
            this.temporalVariableToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.temporalVariableToolStripMenuItem.Text = "Temporal variable";
            this.temporalVariableToolStripMenuItem.Click += new System.EventHandler(this.temporalVariableToolStripMenuItem_Click);
            // 
            // eEPROMVariableToolStripMenuItem
            // 
            this.eEPROMVariableToolStripMenuItem.Name = "eEPROMVariableToolStripMenuItem";
            this.eEPROMVariableToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.eEPROMVariableToolStripMenuItem.Text = "EEPROM variable";
            this.eEPROMVariableToolStripMenuItem.Click += new System.EventHandler(this.eEPROMVariableToolStripMenuItem_Click);
            // 
            // valueAxisToolStripMenuItem
            // 
            this.valueAxisToolStripMenuItem.Name = "valueAxisToolStripMenuItem";
            this.valueAxisToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.valueAxisToolStripMenuItem.Text = "Value -> Axis";
            this.valueAxisToolStripMenuItem.Click += new System.EventHandler(this.valueAxisToolStripMenuItem_Click);
            // 
            // linkToolStripMenuItem
            // 
            this.linkToolStripMenuItem.Name = "linkToolStripMenuItem";
            this.linkToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.linkToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.linkToolStripMenuItem.Text = "Link";
            this.linkToolStripMenuItem.Click += new System.EventHandler(this.linkToolStripMenuItem_Click);
            // 
            // unlinkToolStripMenuItem
            // 
            this.unlinkToolStripMenuItem.Name = "unlinkToolStripMenuItem";
            this.unlinkToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.unlinkToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.unlinkToolStripMenuItem.Text = "Unlink";
            this.unlinkToolStripMenuItem.Click += new System.EventHandler(this.unlinkToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(147, 6);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.AutoSize = true;
            this.mainPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainPanel.ContextMenuStrip = this.contextMenuStrip;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(0, 0);
            this.mainPanel.TabIndex = 1;
            this.mainPanel.Click += new System.EventHandler(this.Panel_Click);
            this.mainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mainPanel_Paint);
            this.mainPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainPanel_MouseDown);
            this.mainPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mainPanel_MouseMove);
            this.mainPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mainPanel_MouseUp);
            // 
            // trimmerEmulationToolStripMenuItem
            // 
            this.trimmerEmulationToolStripMenuItem.Name = "trimmerEmulationToolStripMenuItem";
            this.trimmerEmulationToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.trimmerEmulationToolStripMenuItem.Text = "Trimmer emulation";
            this.trimmerEmulationToolStripMenuItem.Click += new System.EventHandler(this.trimmerEmulationToolStripMenuItem_Click);
            // 
            // valueTrimmerToolStripMenuItem
            // 
            this.valueTrimmerToolStripMenuItem.Name = "valueTrimmerToolStripMenuItem";
            this.valueTrimmerToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.valueTrimmerToolStripMenuItem.Text = "Value trimmer";
            this.valueTrimmerToolStripMenuItem.Click += new System.EventHandler(this.valueTrimmerToolStripMenuItem_Click);
            // 
            // DesignPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.mainPanel);
            this.DoubleBuffered = true;
            this.Name = "DesignPanel";
            this.Size = new System.Drawing.Size(865, 662);
            this.Click += new System.EventHandler(this.Panel_Click);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainPanel_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mainPanel_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mainPanel_MouseUp);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unlinkToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.ToolStripMenuItem joystickToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem axismodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mixersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buttonsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem variablesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem constantToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem temporalVariableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eEPROMVariableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trimmerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem invertorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem centeredExpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rangeExpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deltaMixToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flaperonsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alieronsRudderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modelSelectorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem throttleElevatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logicalButtonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem switchingButtonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem switchingStateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hatValueSwitcherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hatButtonSwitcherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vTailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem valueAxisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pPMMaperToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buttonHoldSwitchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuActionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuActionsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem modelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pPMOUTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flightModesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modeTrimmerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trimmerEmulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem valueTrimmerToolStripMenuItem;
    }
}
