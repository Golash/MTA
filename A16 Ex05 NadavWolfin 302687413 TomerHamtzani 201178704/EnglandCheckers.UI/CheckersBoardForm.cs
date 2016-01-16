using System;
using System.Drawing;
using System.Windows.Forms;
using EnglandCheckers.BusinessLogic;
using EnglandCheckers.Components;

namespace EnglandCheckers.UI
{
    public delegate void UpadteBoardCell();

    public partial class CheckersBoardForm : Form
    {
        public CheckersBoardForm(GameDetails i_GameDetails)
        {
            InitializeComponent();
            r_GameManager = new GameManager();
            updateLabelsPlayersName(i_GameDetails.Player1.Name, i_GameDetails.Player2.Name);
            updateLabelsLocation();
            startNewGame(i_GameDetails);
        }

        private void updateLabelsPlayersName(string i_Player1Name, string i_Player2Name)
        {
            labelPlayer1Name.Text = string.Format("{0}: ", i_Player1Name);
            labelPlayer2Name.Text = string.Format("{0}: ", i_Player2Name);
        }

        private void updateLabelsLocation()
        {
            labelPlayer1Name.Left = panelHeaderPlayersInfo.Left + k_LabelsBuffer;
            labelPlayer1Score.Left = labelPlayer1Name.Left + labelPlayer1Name.Width;

            int bufferFromSide = labelPlayer1Name.Left - panelHeaderPlayersInfo.Left;

            labelPlayer2Score.Left = panelHeaderPlayersInfo.Right - labelPlayer2Score.Width - bufferFromSide;
            labelPlayer2Name.Left = labelPlayer2Score.Left - labelPlayer2Name.Width;
        }

        private void updateLabelsScore(int i_Player1Score, int i_Player2Score)
        {
            labelPlayer1Score.Text = i_Player1Score.ToString();
            labelPlayer2Score.Text = i_Player2Score.ToString();
        }

        private void startNewGame(GameDetails i_GameDetails)
        {
            updateLabelsScore(i_GameDetails.Player1.Points, i_GameDetails.Player2.Points);

            r_GameManager.StartNewGame(i_GameDetails);
            fillBoard(r_GameManager.Board);
        }

        private void fillBoard(Board i_Board)
        {
            flowLayoutPanelBoardGame.Controls.Clear();
            this.ClientSize = calcGameFormSize(i_Board.Size);

            for (int i = 0; i < i_Board.Size; i++)
            {
                for (int j = 0; j < i_Board.Size; j++)
                {
                    BoardCell boardCell = i_Board.GetBoardCell(new BoardPoint(j, i));
                    UIBoardCell cell = new UIBoardCell(boardCell);
                    cell.Click += cell_Click;
                    flowLayoutPanelBoardGame.Controls.Add(cell);
                }
            }
        }

        private void cell_Click(object sender, EventArgs e)
        {
            UIBoardCell selectedCell = (UIBoardCell)sender;

            if (m_FromCell == selectedCell)
            {
                // canceled click
                m_FromCell = null;
            }
            else
            {
                if (m_FromCell == null)
                {
                    m_FromCell = selectedCell;
                }
                else
                {
                    m_ToCell = selectedCell;

                    playMove(m_FromCell.BoardPoint, m_ToCell.BoardPoint);

                    m_FromCell.IsSelected = false;
                    m_ToCell.IsSelected = false;

                    m_FromCell = null;
                    m_ToCell = null;

                    if (!isEndGame())
                    {
                        if (r_GameManager.GameDetails.Player2.Mode == ePlayerMode.Computer)
                        {
                            playComputerMoves();
                        }
                    }
                }
            }
        }

        private void playComputerMoves()
        {
            while (r_GameManager.CurrentPlayer == r_GameManager.GameDetails.Player2)
            {
                r_GameManager.ComputerNextMove(r_GameManager.GameDetails.Player2.Sign);
                isEndGame();
            }
        }

        private void playMove(BoardPoint i_FromCell, BoardPoint i_ToCell)
        {
            BoardMove move = new BoardMove(i_FromCell, i_ToCell);
            string errorMsg;

            if (!r_GameManager.TryMove(move, out errorMsg))
            {
                MessageBox.Show(errorMsg, "Invalid Move", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool isEndGame()
        {
            Player winner;
            bool endGameIsNedded = r_GameManager.NeedToEndGame(out winner);

            if (endGameIsNedded)
            {
                endGame(winner, false);
            }

            return endGameIsNedded;
        }

        private void endGame(Player winner, bool i_IsGameEndWithQuit)
        {
            r_GameManager.EndGame(winner, i_IsGameEndWithQuit);

            string endGameMessage = winner == null ? "Tie" : string.Format("{0} Won", winner.Name);
            string message = string.Format(@"{0}!{1} Another Round ?", endGameMessage, Environment.NewLine);
            DialogResult messageResult = MessageBox.Show(message, k_GameMessageCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (messageResult == DialogResult.Yes)
            {
                startNewGame(r_GameManager.GameDetails);
            }
            else
            {
                Close();
            }
        }

        private Size calcGameFormSize(int i_BoardSize)
        {
            int cellsPanelSize = i_BoardSize * UIBoardCell.k_CellSideSize;

            // Added buffer for better visualization
            int width = cellsPanelSize + k_GamePanelWidthBuffer;
            int height = cellsPanelSize + panelHeaderPlayersInfo.Height + k_GamePanelHeightBuffer;

            return new Size(width, height);
        }

        private void CheckersBoardFor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString().ToUpper() == k_QuitLetter)
            {
                string retireMessage = 
                    string.Format("{0},{1}Are you shure you want to quit?", r_GameManager.CurrentPlayer.Name, Environment.NewLine);

                DialogResult isPlayerQuit = MessageBox.Show(retireMessage, k_GameMessageCaption,  MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (isPlayerQuit == DialogResult.Yes)
                {
                    Player winner = r_GameManager.AdversaryPlayer;
                    endGame(winner, true);
                }
            }
        }

        private readonly GameManager r_GameManager;
        private UIBoardCell m_FromCell;
        private UIBoardCell m_ToCell;
        private const int k_LabelsBuffer = 15;
        private const int k_GamePanelWidthBuffer = 26;
        private const int k_GamePanelHeightBuffer = 30;
        private const string k_QuitLetter = "Q";
        private const string k_GameMessageCaption = "Damka";
    }
}