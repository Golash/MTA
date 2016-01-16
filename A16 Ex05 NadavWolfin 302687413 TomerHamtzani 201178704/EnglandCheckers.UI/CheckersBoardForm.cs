using EnglandCheckers.BusinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EnglandCheckers.Components;

namespace EnglandCheckers.UI
{
    public delegate void UpadteBoardCell();

    public partial class checkersBoardForm : Form
    {
        public checkersBoardForm(GameDetails i_GameDetails)
        {
            InitializeComponent();
            m_GameManager = new GameManager();
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

            m_GameManager.StartNewGame(i_GameDetails);
            FillBoard(m_GameManager.Board);
        }

        private void FillBoard(Board board)
        {
            flowLayoutPanelBoardCells.Controls.Clear();
            this.ClientSize = calcGameFormSize(board.Size);

            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    BoardCell boardCell = board.GetBoardCell(new BoardPoint(j, i));
                    UIBoardCell cell = new UIBoardCell(boardCell);
                    cell.Click += Cell_Click;
                    flowLayoutPanelBoardCells.Controls.Add(cell);
                }
            }
        }

        private void Cell_Click(object sender, EventArgs e)
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
                        if (m_GameManager.GameDetails.Player2.Mode == ePlayerMode.Computer)
                        {
                            playComputerMoves();
                        }
                    }
                }
            }
        }

        private void playComputerMoves()
        {
            while (m_GameManager.CurrentPlayer == m_GameManager.GameDetails.Player2)
            {
                m_GameManager.ComputerNextMove(m_GameManager.GameDetails.Player2.Sign);
                isEndGame();
            }
        }

        private void playMove(BoardPoint i_FromCell, BoardPoint i_ToCell)
        {
            BoardMove move = new BoardMove(i_FromCell, i_ToCell);

            string errorMsg;
            if (!m_GameManager.TryMove(move, out errorMsg))
            {
                MessageBox.Show(errorMsg, "Invalid Move", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private bool isEndGame()
        {
            Player winner;
            bool endGame = m_GameManager.NeedToEndGame(out winner);
            if (endGame)
            {
                m_GameManager.EndGame(winner, false);

                string endGameMessage = winner == null ? "Tie" : string.Format("{0} Won", winner.Name);

                DialogResult messageResult = MessageBox.Show(string.Format(@"{0}! {1} Another Round ?", endGameMessage, Environment.NewLine),
                                       "Damka", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (messageResult == DialogResult.Yes)
                {
                    startNewGame(m_GameManager.GameDetails);
                }
                else
                {
                    this.Close();
                }
            }

            return endGame;
        }

        private Size calcGameFormSize(int i_BoardSize)
        {
            int cellsPanelSize = i_BoardSize * UIBoardCell.k_CellSideSize;

            // todo check the buffer
            int width = cellsPanelSize + 5;
            int height = cellsPanelSize + panelHeaderPlayersInfo.Height;
            
            return new Size(width, height);
        }

        private readonly GameManager m_GameManager;
        private UIBoardCell m_FromCell;
        private UIBoardCell m_ToCell;

        private const int k_LabelsBuffer = 10;
    }
}
