namespace EnglandCheckers.UI
{
    partial class GameSettings
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
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonSixOnSix = new System.Windows.Forms.RadioButton();
            this.radioButtonTenOnTen = new System.Windows.Forms.RadioButton();
            this.radioButtonEightOnEight = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPlayerOne = new System.Windows.Forms.TextBox();
            this.textBoxPlayerTwo = new System.Windows.Forms.TextBox();
            this.checkBoxPlayerTwo = new System.Windows.Forms.CheckBox();
            this.buttonDone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bord Size:";
            // 
            // radioButtonSixOnSix
            // 
            this.radioButtonSixOnSix.AutoSize = true;
            this.radioButtonSixOnSix.Location = new System.Drawing.Point(30, 46);
            this.radioButtonSixOnSix.Name = "radioButtonSixOnSix";
            this.radioButtonSixOnSix.Size = new System.Drawing.Size(48, 17);
            this.radioButtonSixOnSix.TabIndex = 1;
            this.radioButtonSixOnSix.TabStop = true;
            this.radioButtonSixOnSix.Text = "6 x 6";
            this.radioButtonSixOnSix.UseVisualStyleBackColor = true;
            // 
            // radioButtonTenOnTen
            // 
            this.radioButtonTenOnTen.AutoSize = true;
            this.radioButtonTenOnTen.Location = new System.Drawing.Point(158, 46);
            this.radioButtonTenOnTen.Name = "radioButtonTenOnTen";
            this.radioButtonTenOnTen.Size = new System.Drawing.Size(60, 17);
            this.radioButtonTenOnTen.TabIndex = 2;
            this.radioButtonTenOnTen.TabStop = true;
            this.radioButtonTenOnTen.Text = "10 x 10";
            this.radioButtonTenOnTen.UseVisualStyleBackColor = true;
            // 
            // radioButtonEightOnEight
            // 
            this.radioButtonEightOnEight.AutoSize = true;
            this.radioButtonEightOnEight.Location = new System.Drawing.Point(93, 46);
            this.radioButtonEightOnEight.Name = "radioButtonEightOnEight";
            this.radioButtonEightOnEight.Size = new System.Drawing.Size(48, 17);
            this.radioButtonEightOnEight.TabIndex = 3;
            this.radioButtonEightOnEight.TabStop = true;
            this.radioButtonEightOnEight.Text = "8 x 8";
            this.radioButtonEightOnEight.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Players:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Player 1:";
            // 
            // textBoxPlayerOne
            // 
            this.textBoxPlayerOne.Location = new System.Drawing.Point(93, 100);
            this.textBoxPlayerOne.Name = "textBoxPlayerOne";
            this.textBoxPlayerOne.Size = new System.Drawing.Size(141, 20);
            this.textBoxPlayerOne.TabIndex = 7;
            // 
            // textBoxPlayerTwo
            // 
            this.textBoxPlayerTwo.Enabled = false;
            this.textBoxPlayerTwo.Location = new System.Drawing.Point(93, 131);
            this.textBoxPlayerTwo.Name = "textBoxPlayerTwo";
            this.textBoxPlayerTwo.Size = new System.Drawing.Size(141, 20);
            this.textBoxPlayerTwo.TabIndex = 8;
            this.textBoxPlayerTwo.Text = "[Computer]";
            // 
            // checkBoxPlayerTwo
            // 
            this.checkBoxPlayerTwo.AutoSize = true;
            this.checkBoxPlayerTwo.Location = new System.Drawing.Point(26, 133);
            this.checkBoxPlayerTwo.Name = "checkBoxPlayerTwo";
            this.checkBoxPlayerTwo.Size = new System.Drawing.Size(67, 17);
            this.checkBoxPlayerTwo.TabIndex = 9;
            this.checkBoxPlayerTwo.Text = "Player 2:";
            this.checkBoxPlayerTwo.UseVisualStyleBackColor = true;
            this.checkBoxPlayerTwo.CheckedChanged += new System.EventHandler(this.checkBoxPlayerTwo_CheckedChanged);
            // 
            // buttonDone
            // 
            this.buttonDone.Location = new System.Drawing.Point(159, 168);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(75, 23);
            this.buttonDone.TabIndex = 10;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // GameSettings
            // 
            this.AcceptButton = this.buttonDone;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 200);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.checkBoxPlayerTwo);
            this.Controls.Add(this.textBoxPlayerTwo);
            this.Controls.Add(this.textBoxPlayerOne);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radioButtonEightOnEight);
            this.Controls.Add(this.radioButtonTenOnTen);
            this.Controls.Add(this.radioButtonSixOnSix);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "GameSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButtonSixOnSix;
        private System.Windows.Forms.RadioButton radioButtonTenOnTen;
        private System.Windows.Forms.RadioButton radioButtonEightOnEight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPlayerOne;
        private System.Windows.Forms.TextBox textBoxPlayerTwo;
        private System.Windows.Forms.CheckBox checkBoxPlayerTwo;
        private System.Windows.Forms.Button buttonDone;
    }
}