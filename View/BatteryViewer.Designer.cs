using System.Collections.Generic;

namespace Battery
{
    partial class BatteryViewer
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
            this.batteryListBox = new System.Windows.Forms.ListBox();
            this.exitButton = new System.Windows.Forms.Button();
            this.PowerTimeoutLabel = new System.Windows.Forms.Label();
            this.TimeDisplaComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // batteryListBox
            // 
            this.batteryListBox.FormattingEnabled = true;
            this.batteryListBox.ItemHeight = 20;
            this.batteryListBox.Location = new System.Drawing.Point(62, 18);
            this.batteryListBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.batteryListBox.Name = "batteryListBox";
            this.batteryListBox.Size = new System.Drawing.Size(376, 84);
            this.batteryListBox.TabIndex = 0;
            this.batteryListBox.SelectedIndexChanged += new System.EventHandler(this.batteryListBox_SelectedIndexChanged);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(168, 249);
            this.exitButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(134, 35);
            this.exitButton.TabIndex = 1;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // PowerTimeoutLabel
            // 
            this.PowerTimeoutLabel.AutoSize = true;
            this.PowerTimeoutLabel.Location = new System.Drawing.Point(164, 132);
            this.PowerTimeoutLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PowerTimeoutLabel.Name = "PowerTimeoutLabel";
            this.PowerTimeoutLabel.Size = new System.Drawing.Size(138, 20);
            this.PowerTimeoutLabel.TabIndex = 3;
            this.PowerTimeoutLabel.Text = "Set power timeout";
            // 
            // TimeDisplaComboBox
            // 
            this.TimeDisplaComboBox.FormattingEnabled = true;
            this.TimeDisplaComboBox.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "15",
            "20",
            "30",
            "60"});
            this.TimeDisplaComboBox.Location = new System.Drawing.Point(168, 183);
            this.TimeDisplaComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TimeDisplaComboBox.Name = "TimeDisplaComboBox";
            this.TimeDisplaComboBox.Size = new System.Drawing.Size(132, 28);
            this.TimeDisplaComboBox.TabIndex = 5;
            this.TimeDisplaComboBox.SelectedIndexChanged += new System.EventHandler(this.TimeDisplaComboBox_SelectedIndexChanged);
            // 
            // BatteryViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 309);
            this.Controls.Add(this.TimeDisplaComboBox);
            this.Controls.Add(this.PowerTimeoutLabel);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.batteryListBox);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "BatteryViewer";
            this.Text = "BatteryViewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BatteryViewer_FormClosing);
            this.Load += new System.EventHandler(this.BatteryViewer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox batteryListBox;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label PowerTimeoutLabel;
        private System.Windows.Forms.ComboBox TimeDisplaComboBox;
    }
}

