namespace Tahorg.RCJoyGUI.Dialogs
{
    partial class STM32ProjectSettings
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
            this.gbPPM = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbPPMGenerator = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbBoard = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnSelectDir = new System.Windows.Forms.Button();
            this.tbOutPath = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbLCD = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbLCDParallel = new System.Windows.Forms.CheckBox();
            this.tbLCDI2CAddr = new System.Windows.Forms.TextBox();
            this.lblI2CAddr = new System.Windows.Forms.Label();
            this.cbLCDBL = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbLCDEnable = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbROMAddrStyle = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbROMAddr = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbROMEnable = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbRetrSPINRF = new System.Windows.Forms.CheckBox();
            this.cbRetrUSART = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbPCUSART = new System.Windows.Forms.CheckBox();
            this.fsDialog = new System.Windows.Forms.SaveFileDialog();
            this.gbPPM.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.gbLCD.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbPPM
            // 
            this.gbPPM.Controls.Add(this.label1);
            this.gbPPM.Controls.Add(this.cbPPMGenerator);
            this.gbPPM.Location = new System.Drawing.Point(345, 12);
            this.gbPPM.Name = "gbPPM";
            this.gbPPM.Size = new System.Drawing.Size(259, 78);
            this.gbPPM.TabIndex = 3;
            this.gbPPM.TabStop = false;
            this.gbPPM.Text = "PPMGenerator";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "PPM Signal will be mapped on pin PB4";
            // 
            // cbPPMGenerator
            // 
            this.cbPPMGenerator.AutoSize = true;
            this.cbPPMGenerator.Location = new System.Drawing.Point(18, 28);
            this.cbPPMGenerator.Name = "cbPPMGenerator";
            this.cbPPMGenerator.Size = new System.Drawing.Size(135, 17);
            this.cbPPMGenerator.TabIndex = 0;
            this.cbPPMGenerator.Text = "Enable PPM Generator";
            this.cbPPMGenerator.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbBoard);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.btnSelectDir);
            this.groupBox4.Controls.Add(this.tbOutPath);
            this.groupBox4.Location = new System.Drawing.Point(12, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(327, 78);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Output file";
            // 
            // cbBoard
            // 
            this.cbBoard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoard.FormattingEnabled = true;
            this.cbBoard.Items.AddRange(new object[] {
            "STMF407VG (STMF4 Discovery)",
            "STMF407VE (Port 407)"});
            this.cbBoard.Location = new System.Drawing.Point(72, 44);
            this.cbBoard.Name = "cbBoard";
            this.cbBoard.Size = new System.Drawing.Size(245, 21);
            this.cbBoard.TabIndex = 4;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(15, 47);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Board type";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = ".bin file";
            // 
            // btnSelectDir
            // 
            this.btnSelectDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectDir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelectDir.Location = new System.Drawing.Point(293, 16);
            this.btnSelectDir.Name = "btnSelectDir";
            this.btnSelectDir.Size = new System.Drawing.Size(24, 23);
            this.btnSelectDir.TabIndex = 1;
            this.btnSelectDir.Text = "...";
            this.btnSelectDir.UseVisualStyleBackColor = true;
            this.btnSelectDir.Click += new System.EventHandler(this.btnSelectDir_Click);
            // 
            // tbOutPath
            // 
            this.tbOutPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbOutPath.Location = new System.Drawing.Point(72, 19);
            this.tbOutPath.Name = "tbOutPath";
            this.tbOutPath.Size = new System.Drawing.Size(215, 20);
            this.tbOutPath.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(529, 430);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 104;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(448, 430);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 105;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // gbLCD
            // 
            this.gbLCD.Controls.Add(this.label3);
            this.gbLCD.Controls.Add(this.cbLCDParallel);
            this.gbLCD.Controls.Add(this.tbLCDI2CAddr);
            this.gbLCD.Controls.Add(this.lblI2CAddr);
            this.gbLCD.Controls.Add(this.cbLCDBL);
            this.gbLCD.Controls.Add(this.label2);
            this.gbLCD.Controls.Add(this.cbLCDEnable);
            this.gbLCD.Location = new System.Drawing.Point(12, 96);
            this.gbLCD.Name = "gbLCD";
            this.gbLCD.Size = new System.Drawing.Size(327, 164);
            this.gbLCD.TabIndex = 1;
            this.gbLCD.TabStop = false;
            this.gbLCD.Text = "LCD";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(35, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(230, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Check documentation for parallel LCD interface";
            // 
            // cbLCDParallel
            // 
            this.cbLCDParallel.AutoSize = true;
            this.cbLCDParallel.Location = new System.Drawing.Point(18, 115);
            this.cbLCDParallel.Name = "cbLCDParallel";
            this.cbLCDParallel.Size = new System.Drawing.Size(182, 17);
            this.cbLCDParallel.TabIndex = 6;
            this.cbLCDParallel.Text = "LCD on I8080 interface (non I2C)";
            this.cbLCDParallel.UseVisualStyleBackColor = true;
            // 
            // tbLCDI2CAddr
            // 
            this.tbLCDI2CAddr.Location = new System.Drawing.Point(155, 44);
            this.tbLCDI2CAddr.Name = "tbLCDI2CAddr";
            this.tbLCDI2CAddr.Size = new System.Drawing.Size(70, 20);
            this.tbLCDI2CAddr.TabIndex = 5;
            this.tbLCDI2CAddr.Text = "27";
            this.tbLCDI2CAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbLCDI2CAddr.Leave += new System.EventHandler(this.tbLCDI2CAddr_Leave);
            // 
            // lblI2CAddr
            // 
            this.lblI2CAddr.AutoSize = true;
            this.lblI2CAddr.Location = new System.Drawing.Point(15, 47);
            this.lblI2CAddr.Name = "lblI2CAddr";
            this.lblI2CAddr.Size = new System.Drawing.Size(119, 13);
            this.lblI2CAddr.TabIndex = 4;
            this.lblI2CAddr.Text = "I2C LCD Address (HEX)";
            // 
            // cbLCDBL
            // 
            this.cbLCDBL.AutoSize = true;
            this.cbLCDBL.Location = new System.Drawing.Point(155, 21);
            this.cbLCDBL.Name = "cbLCDBL";
            this.cbLCDBL.Size = new System.Drawing.Size(105, 17);
            this.cbLCDBL.TabIndex = 2;
            this.cbLCDBL.Text = "Enable backlight";
            this.cbLCDBL.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(35, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 39);
            this.label2.TabIndex = 1;
            this.label2.Text = "I2C LCD must be connected to \r\n      PB6 -> SCL\r\n      PB7 -> SDA";
            // 
            // cbLCDEnable
            // 
            this.cbLCDEnable.AutoSize = true;
            this.cbLCDEnable.Location = new System.Drawing.Point(18, 21);
            this.cbLCDEnable.Name = "cbLCDEnable";
            this.cbLCDEnable.Size = new System.Drawing.Size(103, 17);
            this.cbLCDEnable.TabIndex = 0;
            this.cbLCDEnable.Text = "Enable text LCD";
            this.cbLCDEnable.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbROMAddrStyle);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.tbROMAddr);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbROMEnable);
            this.groupBox1.Location = new System.Drawing.Point(345, 96);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 210);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "EEPROM / FRAM";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(35, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(220, 39);
            this.label4.TabIndex = 10;
            this.label4.Text = "Check documentation for ROM addressing. \r\nFor 8KBit and less ROM use 8bit address" +
    "ing, \r\nand 16 bit for lager ROM.\r\n";
            // 
            // cbROMAddrStyle
            // 
            this.cbROMAddrStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbROMAddrStyle.FormattingEnabled = true;
            this.cbROMAddrStyle.Items.AddRange(new object[] {
            "8 bit",
            "16 bit "});
            this.cbROMAddrStyle.Location = new System.Drawing.Point(155, 49);
            this.cbROMAddrStyle.Name = "cbROMAddrStyle";
            this.cbROMAddrStyle.Size = new System.Drawing.Size(70, 21);
            this.cbROMAddrStyle.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "ROM Addressing";
            // 
            // tbROMAddr
            // 
            this.tbROMAddr.Location = new System.Drawing.Point(155, 127);
            this.tbROMAddr.Name = "tbROMAddr";
            this.tbROMAddr.Size = new System.Drawing.Size(70, 20);
            this.tbROMAddr.TabIndex = 5;
            this.tbROMAddr.Text = "50";
            this.tbROMAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbROMAddr.Leave += new System.EventHandler(this.tbROMAddr_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "I2C ROM Address (HEX)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(35, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(141, 39);
            this.label6.TabIndex = 1;
            this.label6.Text = "ROM must be connected to \r\n      PB10 -> SCL\r\n      PB11 -> SDA";
            // 
            // cbROMEnable
            // 
            this.cbROMEnable.AutoSize = true;
            this.cbROMEnable.Location = new System.Drawing.Point(18, 28);
            this.cbROMEnable.Name = "cbROMEnable";
            this.cbROMEnable.Size = new System.Drawing.Size(143, 17);
            this.cbROMEnable.TabIndex = 0;
            this.cbROMEnable.Text = "Enable EEPROM/FRAM";
            this.cbROMEnable.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.cbRetrSPINRF);
            this.groupBox2.Controls.Add(this.cbRetrUSART);
            this.groupBox2.Location = new System.Drawing.Point(12, 266);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(327, 159);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Retranslator";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(35, 109);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(267, 39);
            this.label9.TabIndex = 4;
            this.label9.Text = "SPI connected NRF retranslator, check documentation\r\nfor connection details\r\n\r\n";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(35, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 39);
            this.label8.TabIndex = 3;
            this.label8.Text = "XBEE or BT retranslator\r\n      PC10 -> RX\r\n      PC11 -> TX";
            // 
            // cbRetrSPINRF
            // 
            this.cbRetrSPINRF.AutoSize = true;
            this.cbRetrSPINRF.Location = new System.Drawing.Point(18, 91);
            this.cbRetrSPINRF.Name = "cbRetrSPINRF";
            this.cbRetrSPINRF.Size = new System.Drawing.Size(139, 17);
            this.cbRetrSPINRF.TabIndex = 2;
            this.cbRetrSPINRF.Text = "Enable NRF retranslator";
            this.cbRetrSPINRF.UseVisualStyleBackColor = true;
            this.cbRetrSPINRF.CheckedChanged += new System.EventHandler(this.cbRetrSPINRF_CheckedChanged);
            // 
            // cbRetrUSART
            // 
            this.cbRetrUSART.AutoSize = true;
            this.cbRetrUSART.Location = new System.Drawing.Point(18, 25);
            this.cbRetrUSART.Name = "cbRetrUSART";
            this.cbRetrUSART.Size = new System.Drawing.Size(154, 17);
            this.cbRetrUSART.TabIndex = 1;
            this.cbRetrUSART.Text = "Enable USART retranslator";
            this.cbRetrUSART.UseVisualStyleBackColor = true;
            this.cbRetrUSART.CheckedChanged += new System.EventHandler(this.cbRetrUSART_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.cbPCUSART);
            this.groupBox3.Location = new System.Drawing.Point(345, 312);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(259, 113);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "PC communication";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(35, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 39);
            this.label10.TabIndex = 4;
            this.label10.Text = "PC Communication\r\n      PC11 -> RX\r\n      PC10 -> TX";
            // 
            // cbPCUSART
            // 
            this.cbPCUSART.AutoSize = true;
            this.cbPCUSART.Location = new System.Drawing.Point(18, 26);
            this.cbPCUSART.Name = "cbPCUSART";
            this.cbPCUSART.Size = new System.Drawing.Size(147, 17);
            this.cbPCUSART.TabIndex = 2;
            this.cbPCUSART.Text = "Enable PC comm USART";
            this.cbPCUSART.UseVisualStyleBackColor = true;
            // 
            // fsDialog
            // 
            this.fsDialog.DefaultExt = "bin";
            this.fsDialog.Filter = "BIN files|*.bin|HEX files|*.hex|All files|*.*";
            this.fsDialog.RestoreDirectory = true;
            // 
            // STM32ProjectSettings
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(616, 462);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbLCD);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gbPPM);
            this.Name = "STM32ProjectSettings";
            this.Text = "STM32F4 Project settings";
            this.gbPPM.ResumeLayout(false);
            this.gbPPM.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.gbLCD.ResumeLayout(false);
            this.gbLCD.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbPPM;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnSelectDir;
        private System.Windows.Forms.TextBox tbOutPath;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbPPMGenerator;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbLCD;
        private System.Windows.Forms.CheckBox cbLCDBL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbLCDEnable;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbLCDParallel;
        private System.Windows.Forms.TextBox tbLCDI2CAddr;
        private System.Windows.Forms.Label lblI2CAddr;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbROMAddrStyle;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbROMAddr;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbROMEnable;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cbRetrSPINRF;
        private System.Windows.Forms.CheckBox cbRetrUSART;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox cbPCUSART;
        private System.Windows.Forms.SaveFileDialog fsDialog;
        private System.Windows.Forms.ComboBox cbBoard;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
    }
}