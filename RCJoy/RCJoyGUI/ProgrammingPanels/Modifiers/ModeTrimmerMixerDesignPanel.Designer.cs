namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    partial class ModeTrimmerMixerDesignPanel
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
            this.lnkInc = new Tahorg.RCJoyGUI.LinkPoint();
            this.lnkAxis = new Tahorg.RCJoyGUI.LinkPoint();
            this.lnkOut = new Tahorg.RCJoyGUI.LinkPoint();
            this.lblAxle = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.lblValue1 = new System.Windows.Forms.Label();
            this.lnkDec = new Tahorg.RCJoyGUI.LinkPoint();
            this.lnkReset = new Tahorg.RCJoyGUI.LinkPoint();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbCName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbStep = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbInitVal = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbFramEnabled = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.cbMode = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // labelHead
            // 
            this.labelHead.Size = new System.Drawing.Size(147, 13);
            this.labelHead.Text = "Mode trimmer";
            // 
            // lnkInc
            // 
            this.lnkInc.DataMapIdx = ((short)(0));
            this.lnkInc.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkInc.HolderPanel = null;
            this.lnkInc.IsSelected = false;
            this.lnkInc.LinkType = Tahorg.RCJoyGUI.enLinkType.Button;
            this.lnkInc.Location = new System.Drawing.Point(0, 37);
            this.lnkInc.MappedValueName = null;
            this.lnkInc.Name = "lnkInc";
            this.lnkInc.Size = new System.Drawing.Size(12, 12);
            this.lnkInc.TabIndex = 1;
            this.lnkInc.TabStop = false;
            this.lnkInc.Text = "linkPoint1";
            // 
            // lnkAxis
            // 
            this.lnkAxis.DataMapIdx = ((short)(0));
            this.lnkAxis.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkAxis.HolderPanel = null;
            this.lnkAxis.IsSelected = false;
            this.lnkAxis.LinkType = Tahorg.RCJoyGUI.enLinkType.Axle;
            this.lnkAxis.Location = new System.Drawing.Point(0, 19);
            this.lnkAxis.MappedValueName = null;
            this.lnkAxis.Name = "lnkAxis";
            this.lnkAxis.Size = new System.Drawing.Size(12, 12);
            this.lnkAxis.TabIndex = 2;
            this.lnkAxis.Text = "linkPoint2";
            // 
            // lnkOut
            // 
            this.lnkOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkOut.DataMapIdx = ((short)(0));
            this.lnkOut.Direction = Tahorg.RCJoyGUI.enLinkDirection.Output;
            this.lnkOut.HolderPanel = null;
            this.lnkOut.IsSelected = false;
            this.lnkOut.LinkType = Tahorg.RCJoyGUI.enLinkType.Axle;
            this.lnkOut.Location = new System.Drawing.Point(136, 19);
            this.lnkOut.MappedValueName = null;
            this.lnkOut.Name = "lnkOut";
            this.lnkOut.Size = new System.Drawing.Size(12, 12);
            this.lnkOut.TabIndex = 3;
            this.lnkOut.TabStop = false;
            this.lnkOut.Text = "linkPoint3";
            // 
            // lblAxle
            // 
            this.lblAxle.AutoSize = true;
            this.lblAxle.Location = new System.Drawing.Point(12, 18);
            this.lblAxle.Name = "lblAxle";
            this.lblAxle.Size = new System.Drawing.Size(26, 13);
            this.lblAxle.TabIndex = 5;
            this.lblAxle.Text = "Axis";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 36);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(48, 13);
            this.Label1.TabIndex = 6;
            this.Label1.Text = "Increase";
            // 
            // lblValue1
            // 
            this.lblValue1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblValue1.AutoSize = true;
            this.lblValue1.BackColor = System.Drawing.Color.Transparent;
            this.lblValue1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValue1.Location = new System.Drawing.Point(95, 18);
            this.lblValue1.Name = "lblValue1";
            this.lblValue1.Size = new System.Drawing.Size(39, 13);
            this.lblValue1.TabIndex = 7;
            this.lblValue1.Text = "XXXX";
            // 
            // lnkDec
            // 
            this.lnkDec.DataMapIdx = ((short)(0));
            this.lnkDec.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkDec.HolderPanel = null;
            this.lnkDec.IsSelected = false;
            this.lnkDec.LinkType = Tahorg.RCJoyGUI.enLinkType.Button;
            this.lnkDec.Location = new System.Drawing.Point(0, 55);
            this.lnkDec.MappedValueName = null;
            this.lnkDec.Name = "lnkDec";
            this.lnkDec.Size = new System.Drawing.Size(12, 12);
            this.lnkDec.TabIndex = 8;
            this.lnkDec.TabStop = false;
            this.lnkDec.Text = "linkPoint1";
            // 
            // lnkReset
            // 
            this.lnkReset.DataMapIdx = ((short)(0));
            this.lnkReset.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkReset.HolderPanel = null;
            this.lnkReset.IsSelected = false;
            this.lnkReset.LinkType = Tahorg.RCJoyGUI.enLinkType.Button;
            this.lnkReset.Location = new System.Drawing.Point(0, 73);
            this.lnkReset.MappedValueName = null;
            this.lnkReset.Name = "lnkReset";
            this.lnkReset.Size = new System.Drawing.Size(12, 12);
            this.lnkReset.TabIndex = 9;
            this.lnkReset.TabStop = false;
            this.lnkReset.Text = "linkPoint1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Decrease";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Clear";
            // 
            // tbCName
            // 
            this.tbCName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbCName.Location = new System.Drawing.Point(73, 187);
            this.tbCName.Name = "tbCName";
            this.tbCName.Size = new System.Drawing.Size(60, 20);
            this.tbCName.TabIndex = 33;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 189);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "LCD Name";
            // 
            // tbStep
            // 
            this.tbStep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbStep.Location = new System.Drawing.Point(81, 143);
            this.tbStep.Name = "tbStep";
            this.tbStep.Size = new System.Drawing.Size(52, 20);
            this.tbStep.TabIndex = 25;
            this.tbStep.Text = "5";
            this.tbStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Step";
            // 
            // tbInitVal
            // 
            this.tbInitVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbInitVal.Location = new System.Drawing.Point(81, 118);
            this.tbInitVal.Name = "tbInitVal";
            this.tbInitVal.Size = new System.Drawing.Size(52, 20);
            this.tbInitVal.TabIndex = 21;
            this.tbInitVal.Text = "0";
            this.tbInitVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 121);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Init value";
            // 
            // cbFramEnabled
            // 
            this.cbFramEnabled.AutoSize = true;
            this.cbFramEnabled.Location = new System.Drawing.Point(15, 166);
            this.cbFramEnabled.Name = "cbFramEnabled";
            this.cbFramEnabled.Size = new System.Drawing.Size(47, 17);
            this.cbFramEnabled.TabIndex = 28;
            this.cbFramEnabled.Text = "EEP";
            this.cbFramEnabled.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "Mode";
            // 
            // cbMode
            // 
            this.cbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMode.FormattingEnabled = true;
            this.cbMode.Location = new System.Drawing.Point(52, 91);
            this.cbMode.Name = "cbMode";
            this.cbMode.Size = new System.Drawing.Size(81, 21);
            this.cbMode.TabIndex = 36;
            this.cbMode.SelectedIndexChanged += new System.EventHandler(this.cbMode_SelectedIndexChanged);
            // 
            // ModeTrimmerMixerDesignPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbMode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbFramEnabled);
            this.Controls.Add(this.tbCName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbStep);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbInitVal);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lnkReset);
            this.Controls.Add(this.lnkDec);
            this.Controls.Add(this.lnkOut);
            this.Controls.Add(this.lblValue1);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.lblAxle);
            this.Controls.Add(this.lnkAxis);
            this.Controls.Add(this.lnkInc);
            this.Name = "ModeTrimmerMixerDesignPanel";
            this.Size = new System.Drawing.Size(147, 218);
            this.Title = "Mode trimmer";
            this.Controls.SetChildIndex(this.labelHead, 0);
            this.Controls.SetChildIndex(this.lnkInc, 0);
            this.Controls.SetChildIndex(this.lnkAxis, 0);
            this.Controls.SetChildIndex(this.lblAxle, 0);
            this.Controls.SetChildIndex(this.Label1, 0);
            this.Controls.SetChildIndex(this.lblValue1, 0);
            this.Controls.SetChildIndex(this.lnkOut, 0);
            this.Controls.SetChildIndex(this.lnkDec, 0);
            this.Controls.SetChildIndex(this.lnkReset, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.tbInitVal, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.tbStep, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.tbCName, 0);
            this.Controls.SetChildIndex(this.cbFramEnabled, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.cbMode, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LinkPoint lnkInc;
        private LinkPoint lnkAxis;
        private LinkPoint lnkOut;
        private System.Windows.Forms.Label lblAxle;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label lblValue1;
        private LinkPoint lnkDec;
        private LinkPoint lnkReset;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbCName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbStep;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbInitVal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cbFramEnabled;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbMode;
    }
}
