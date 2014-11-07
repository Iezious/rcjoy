namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    partial class FlightModeDesignPanel
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.flightModeOutChannel1 = new Tahorg.RCJoyGUI.ProgrammingPanels.FlightMode.FlightModeOutChannel();
            this.flightModeOutChannel2 = new Tahorg.RCJoyGUI.ProgrammingPanels.FlightMode.FlightModeOutChannel();
            this.SuspendLayout();
            // 
            // labelHead
            // 
            this.labelHead.Size = new System.Drawing.Size(215, 13);
            this.labelHead.Text = "Mode switch";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Modes count";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Channels";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4"});
            this.comboBox1.Location = new System.Drawing.Point(88, 27);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(48, 21);
            this.comboBox1.TabIndex = 3;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.comboBox2.Location = new System.Drawing.Point(88, 54);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(48, 21);
            this.comboBox2.TabIndex = 4;
            // 
            // flightModeOutChannel1
            // 
            this.flightModeOutChannel1.Location = new System.Drawing.Point(163, 25);
            this.flightModeOutChannel1.Name = "flightModeOutChannel1";
            this.flightModeOutChannel1.Number = -1;
            this.flightModeOutChannel1.Size = new System.Drawing.Size(50, 18);
            this.flightModeOutChannel1.TabIndex = 5;
            // 
            // flightModeOutChannel2
            // 
            this.flightModeOutChannel2.Location = new System.Drawing.Point(163, 45);
            this.flightModeOutChannel2.Name = "flightModeOutChannel2";
            this.flightModeOutChannel2.Number = -1;
            this.flightModeOutChannel2.Size = new System.Drawing.Size(50, 18);
            this.flightModeOutChannel2.TabIndex = 6;
            // 
            // FlightModeDesignPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flightModeOutChannel2);
            this.Controls.Add(this.flightModeOutChannel1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FlightModeDesignPanel";
            this.Size = new System.Drawing.Size(215, 241);
            this.Title = "Mode switch";
            this.Controls.SetChildIndex(this.labelHead, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.comboBox1, 0);
            this.Controls.SetChildIndex(this.comboBox2, 0);
            this.Controls.SetChildIndex(this.flightModeOutChannel1, 0);
            this.Controls.SetChildIndex(this.flightModeOutChannel2, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private FlightMode.FlightModeOutChannel flightModeOutChannel1;
        private FlightMode.FlightModeOutChannel flightModeOutChannel2;
    }
}
