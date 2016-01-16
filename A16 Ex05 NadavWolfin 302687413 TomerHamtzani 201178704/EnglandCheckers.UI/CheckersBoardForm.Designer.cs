namespace EnglandCheckers.UI
{
    internal partial class checkersBoardForm
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
            this.labelPlayer1Name = new System.Windows.Forms.Label();
            this.labelPlayer1Score = new System.Windows.Forms.Label();
            this.flowLayoutPanelBoardCells = new System.Windows.Forms.FlowLayoutPanel();
            this.panelHeaderPlayersInfo = new System.Windows.Forms.Panel();
            this.labelPlayer2Score = new System.Windows.Forms.Label();
            this.labelPlayer2Name = new System.Windows.Forms.Label();
            this.panelHeaderPlayersInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelPlayer1Name
            // 
            this.labelPlayer1Name.AutoSize = true;
            this.labelPlayer1Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelPlayer1Name.Location = new System.Drawing.Point(92, 21);
            this.labelPlayer1Name.Name = "labelPlayer1Name";
            this.labelPlayer1Name.Size = new System.Drawing.Size(57, 13);
            this.labelPlayer1Name.TabIndex = 0;
            this.labelPlayer1Name.Text = "Player 1:";
            // 
            // labelPlayer1Score
            // 
            this.labelPlayer1Score.AutoSize = true;
            this.labelPlayer1Score.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelPlayer1Score.Location = new System.Drawing.Point(150, 21);
            this.labelPlayer1Score.Name = "labelPlayer1Score";
            this.labelPlayer1Score.Size = new System.Drawing.Size(14, 13);
            this.labelPlayer1Score.TabIndex = 2;
            this.labelPlayer1Score.Text = "0";
            // 
            // flowLayoutPanelBoardCells
            // 
            this.flowLayoutPanelBoardCells.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanelBoardCells.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanelBoardCells.Location = new System.Drawing.Point(2, 63);
            this.flowLayoutPanelBoardCells.Name = "flowLayoutPanelBoardCells";
            this.flowLayoutPanelBoardCells.Size = new System.Drawing.Size(504, 271);
            this.flowLayoutPanelBoardCells.TabIndex = 4;
            // 
            // panelHeaderPlayersInfo
            // 
            this.panelHeaderPlayersInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelHeaderPlayersInfo.Controls.Add(this.labelPlayer2Score);
            this.panelHeaderPlayersInfo.Controls.Add(this.labelPlayer2Name);
            this.panelHeaderPlayersInfo.Controls.Add(this.labelPlayer1Score);
            this.panelHeaderPlayersInfo.Controls.Add(this.labelPlayer1Name);
            this.panelHeaderPlayersInfo.Location = new System.Drawing.Point(1, 2);
            this.panelHeaderPlayersInfo.Name = "panelHeaderPlayersInfo";
            this.panelHeaderPlayersInfo.Size = new System.Drawing.Size(504, 59);
            this.panelHeaderPlayersInfo.TabIndex = 5;
            // 
            // labelPlayer2Score
            // 
            this.labelPlayer2Score.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPlayer2Score.AutoSize = true;
            this.labelPlayer2Score.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelPlayer2Score.Location = new System.Drawing.Point(378, 21);
            this.labelPlayer2Score.Name = "labelPlayer2Score";
            this.labelPlayer2Score.Size = new System.Drawing.Size(14, 13);
            this.labelPlayer2Score.TabIndex = 4;
            this.labelPlayer2Score.Text = "0";
            // 
            // labelPlayer2Name
            // 
            this.labelPlayer2Name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPlayer2Name.AutoSize = true;
            this.labelPlayer2Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelPlayer2Name.Location = new System.Drawing.Point(320, 21);
            this.labelPlayer2Name.Name = "labelPlayer2Name";
            this.labelPlayer2Name.Size = new System.Drawing.Size(57, 13);
            this.labelPlayer2Name.TabIndex = 3;
            this.labelPlayer2Name.Text = "Player 2:";
            // 
            // checkersBoardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(506, 334);
            this.Controls.Add(this.panelHeaderPlayersInfo);
            this.Controls.Add(this.flowLayoutPanelBoardCells);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "checkersBoardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Damka";
            this.panelHeaderPlayersInfo.ResumeLayout(false);
            this.panelHeaderPlayersInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelPlayer1Name;
        private System.Windows.Forms.Label labelPlayer1Score;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelBoardCells;
        private System.Windows.Forms.Panel panelHeaderPlayersInfo;
        private System.Windows.Forms.Label labelPlayer2Score;
        private System.Windows.Forms.Label labelPlayer2Name;
    }
}