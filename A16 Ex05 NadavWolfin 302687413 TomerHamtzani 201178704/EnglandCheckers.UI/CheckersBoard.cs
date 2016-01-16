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
            m_UIBorderCells = new List<UIBoardCell>();
            startNewGame(i_GameDetails);
        }

        private void startNewGame(GameDetails i_GameDetails)
        {
            labelPlayer1Name.Text = string.Format("{0}:", i_GameDetails.Player1.Name);
            labelPlayer2Name.Text = string.Format("{0}:", i_GameDetails.Player2.Name);

            labelPlayer1Score.Text = i_GameDetails.Player1.Points.ToString();
            labelPlayer2Score.Text = i_GameDetails.Player2.Points.ToString();

            labelPlayer1Score.Location = new Point(labelPlayer1Name.Right, labelPlayer1Name.Top);
            labelPlayer2Score.Location = new Point(labelPlayer2Name.Right, labelPlayer1Name.Top);

            m_UIBorderCells.Clear();
            flowLayoutPanelBoardCells.Controls.Clear();
            m_GameManager.StartNewGame(i_GameDetails);
            FillBoard(m_GameManager.Board);
        }

        private void FillBoard(Board board)
        {
            this.ClientSize = calcGameFormSize(board.Size);

            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    BoardCell boardCell = new BoardCell(j, i);
                    Coin currentCoin = board.GetCoin(boardCell);
                    UIBoardCell newCell = new UIBoardCell(true, currentCoin, boardCell);
                    m_UIBorderCells.Add(newCell);
                    newCell.CellSelected += NewCell_CellSelected;
                    flowLayoutPanelBoardCells.Controls.Add(newCell);
                }
            }
        }

        private void NewCell_CellSelected(object obj, EventArgs e)
        {
            UIBoardCell selectedCell = (UIBoardCell)obj;

            if (!selectedCell.IsSelected) 
            {
                // canceled click
                m_FromCell = null;
            }
            else
            {
                if (m_FromCell == null)
                {
                    m_FromCell = selectedCell.IsSelected ? selectedCell : null;
                }
                else
                {
                    m_ToCell = selectedCell;
                    playMove(m_FromCell.BoardCell, m_ToCell.BoardCell);

                    m_FromCell = null;
                    m_ToCell = null;

                    RefreshCells();

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
                RefreshCells();
                isEndGame();
            }
        }

        private void playMove(BoardCell boardCell1, BoardCell boardCell2)
        {
            BoardMove move = new BoardMove(m_FromCell.BoardCell, m_ToCell.BoardCell);

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

        private void RefreshCells()
        {
            foreach (UIBoardCell BoardCell in m_UIBorderCells)
            {
                Coin cellCoin = m_GameManager.Board.GetCoin(BoardCell.BoardCell);

                BoardCell.IsSelected = false;
                BoardCell.Coin = cellCoin;
                BoardCell.UpdateCell();
            }
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
        private List<UIBoardCell> m_UIBorderCells; 
    }
}
