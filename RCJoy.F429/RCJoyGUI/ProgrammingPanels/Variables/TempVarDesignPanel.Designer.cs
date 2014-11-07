namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    partial class VariableDesignPanel
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
            this.lnkInc = new Tahorg.RCJoyGUI.LinkPoint();
            this.lnkOut = new Tahorg.RCJoyGUI.LinkPoint();
            this.lblAil = new System.Windows.Forms.Label();
            this.lblValue1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lnkDec = new Tahorg.RCJoyGUI.LinkPoint();
            this.label2 = new System.Windows.Forms.Label();
            this.lnkReset = new Tahorg.RCJoyGUI.LinkPoint();
            this.label3 = new System.Windows.Forms.Label();
            this.tbInitVal = new System.Windows.Forms.TextBox();
            this.tbStep = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelHead
            // 
            this.labelHead.Size = new System.Drawing.Size(146, 13);
            this.labelHead.Text = "Variable";
            // 
            // lnkInc
            // 
            this.lnkInc.DataMapIdx = ((short)(0));
            this.lnkInc.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkInc.HolderPanel = null;
            this.lnkInc.IsSelected = false;
            this.lnkInc.LinkType = Tahorg.RCJoyGUI.enLinkType.Button;
            this.lnkInc.Location = new System.Drawing.Point(0, 19);
            this.lnkInc.MappedValueName = null;
            this.lnkInc.Name = "lnkInc";
            this.lnkInc.Size = new System.Drawing.Size(12, 12);
            this.lnkInc.TabIndex = 2;
            this.lnkInc.Text = "linkPoint2";
            // 
            // lnkOut
            // 
            this.lnkOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkOut.DataMapIdx = ((short)(0));
            this.lnkOut.Direction = Tahorg.RCJoyGUI.enLinkDirection.Output;
            this.lnkOut.HolderPanel = null;
            this.lnkOut.IsSelected = false;
            this.lnkOut.LinkType = Tahorg.RCJoyGUI.enLinkType.Value;
            this.lnkOut.Location = new System.Drawing.Point(135, 19);
            this.lnkOut.MappedValueName = null;
            this.lnkOut.Name = "lnkOut";
            this.lnkOut.Size = new System.Drawing.Size(12, 12);
            this.lnkOut.TabIndex = 3;
            this.lnkOut.TabStop = false;
            this.lnkOut.Text = "linkPoint3";
            // 
            // lblAil
            // 
            this.lblAil.AutoSize = true;
            this.lblAil.Location = new System.Drawing.Point(12, 18);
            this.lblAil.Name = "lblAil";
            this.lblAil.Size = new System.Drawing.Size(54, 13);
            this.lblAil.TabIndex = 5;
            this.lblAil.Text = "Increment";
            // 
            // lblValue1
            // 
            this.lblValue1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblValue1.AutoSize = true;
            this.lblValue1.BackColor = System.Drawing.Color.Transparent;
            this.lblValue1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValue1.Location = new System.Drawing.Point(94, 18);
            this.lblValue1.Name = "lblValue1";
            this.lblValue1.Size = new System.Drawing.Size(39, 13);
            this.lblValue1.TabIndex = 7;
            this.lblValue1.Text = "XXXX";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Decrement";
            // 
            // lnkDec
            // 
            this.lnkDec.DataMapIdx = ((short)(0));
            this.lnkDec.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkDec.HolderPanel = null;
            this.lnkDec.IsSelected = false;
            this.lnkDec.LinkType = Tahorg.RCJoyGUI.enLinkType.Button;
            this.lnkDec.Location = new System.Drawing.Point(0, 37);
            this.lnkDec.MappedValueName = null;
            this.lnkDec.Name = "lnkDec";
            this.lnkDec.Size = new System.Drawing.Size(12, 12);
            this.lnkDec.TabIndex = 8;
            this.lnkDec.Text = "linkPoint2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Reset";
            // 
            // lnkReset
            // 
            this.lnkReset.DataMapIdx = ((short)(0));
            this.lnkReset.Direction = Tahorg.RCJoyGUI.enLinkDirection.Input;
            this.lnkReset.HolderPanel = null;
            this.lnkReset.IsSelected = false;
            this.lnkReset.LinkType = Tahorg.RCJoyGUI.enLinkType.Button;
            this.lnkReset.Location = new System.Drawing.Point(0, 55);
            this.lnkReset.MappedValueName = null;
            this.lnkReset.Name = "lnkReset";
            this.lnkReset.Size = new System.Drawing.Size(12, 12);
            this.lnkReset.TabIndex = 10;
            this.lnkReset.Text = "linkPoint2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Init value";
            // 
            // tbInitVal
            // 
            this.tbInitVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbInitVal.Location = new System.Drawing.Point(81, 71);
            this.tbInitVal.Name = "tbInitVal";
            this.tbInitVal.Size = new System.Drawing.Size(52, 20);
            this.tbInitVal.TabIndex = 13;
            this.tbInitVal.Text = "0";
            this.tbInitVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbInitVal.Validating += new System.ComponentModel.CancelEventHandler(this.tbInitVal_Validating);
            // 
            // tbStep
            // 
            this.tbStep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbStep.Location = new System.Drawing.Point(81, 94);
            this.tbStep.Name = "tbStep";
            this.tbStep.Size = new System.Drawing.Size(52, 20);
            this.tbStep.TabIndex = 15;
            this.tbStep.Text = "5";
            this.tbStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbStep.Validating += new System.ComponentModel.CancelEventHandler(this.tbStep_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Step";
            // 
            // VariableDesignPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbStep);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbInitVal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lnkReset);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lnkDec);
            this.Controls.Add(this.lnkOut);
            this.Controls.Add(this.lblValue1);
            this.Controls.Add(this.lblAil);
            this.Controls.Add(this.lnkInc);
            this.Name = "VariableDesignPanel";
            this.Size = new System.Drawing.Size(146, 121);
            this.Title = "Variable";
            this.Controls.SetChildIndex(this.labelHead, 0);
            this.Controls.SetChildIndex(this.lnkInc, 0);
            this.Controls.SetChildIndex(this.lblAil, 0);
            this.Controls.SetChildIndex(this.lblValue1, 0);
            this.Controls.SetChildIndex(this.lnkOut, 0);
            this.Controls.SetChildIndex(this.lnkDec, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.lnkReset, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.tbInitVal, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.tbStep, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LinkPoint lnkInc;
        private LinkPoint lnkOut;
        private System.Windows.Forms.Label lblAil;
        private System.Windows.Forms.Label lblValue1;
        private System.Windows.Forms.Label label1;
        private LinkPoint lnkDec;
        private System.Windows.Forms.Label label2;
        private LinkPoint lnkReset;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbInitVal;
        private System.Windows.Forms.TextBox tbStep;
        private System.Windows.Forms.Label label4;
    }
}
