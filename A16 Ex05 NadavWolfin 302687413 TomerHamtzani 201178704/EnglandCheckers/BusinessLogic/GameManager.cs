using System.Collections.Generic;
using EnglandCheckers.Components;
using EnglandCheckers.GameStrategy;

namespace EnglandCheckers.BusinessLogic
{
    /// <summary>
    /// Manage the game process
    /// </summary>
    public class GameManager
    {   
        /// <summary>
        /// Start a new game and init all the required components
        /// </summary>
        public void StartNewGame(GameDetails i_GameDetails)
        {
            m_GameDetails = i_GameDetails;
            m_Board = new Board(i_GameDetails);
            m_TurnManager = new TurnManager(m_GameDetails.Player1, m_GameDetails.Player2, m_Board);
            m_Winner = null;

            // Set the strategy only if the second player is computer
            if (m_GameDetails.Player2.Mode == ePlayerMode.Computer)
            {
                m_GameStrategy = new AIGameStrategy(m_Board);
            }
        }

        /// <summary>
        /// End the current game and set the winner (if winner is exists)
        /// </summary>
        /// <param name="i_Winner"></param>
        /// <param name="i_IsGameEndWithQuit"></param>
        public void EndGame(Player i_Winner, bool i_IsGameEndWithQuit)
        {
            if (i_Winner == null)
            { 
                // Tie
                addPointsToPlayersInCaseOfTie();
            }
            else
            {
                m_Winner = i_Winner;
                m_Winner.Points += getWinnerPoints(i_Winner, i_IsGameEndWithQuit);
            }
        }

        /// <summary>
        /// Calc and add the points for the better player in case of tie
        /// </summary>
        private void addPointsToPlayersInCaseOfTie()
        {
            int playerOnePoints = getWinnerPoints(m_GameDetails.Player1, false);
            int playerTwoPoints = getWinnerPoints(m_GameDetails.Player2, false);

            // Give the points to the player that have more points.
            // Pay attention - The points are already the diff between the two players
            if (playerOnePoints > playerTwoPoints)
            {
                m_GameDetails.Player1.Points += playerOnePoints - playerTwoPoints;
            }
            else if (playerOnePoints < playerTwoPoints)
            {
                m_GameDetails.Player2.Points += playerTwoPoints - playerOnePoints;
            }
        }

        /// <summary>
        /// Try to make the given <paramref name="i_Move"/>
        /// If the move is a valid move (according to rules in <see cref="GameRulesValidator"/>) it will be applay
        /// and true will return, otherwise <paramref name="o_FailureReason"/> will contain the invalid movce reason
        /// and fale will return.
        /// </summary>
        public bool TryMove(BoardMove i_Move, out string o_FailureReason)
        {
            Player currentPlayer = m_TurnManager.CurrentPlayer;

            bool isValidMove = m_Board.TryMove(currentPlayer, i_Move, out o_FailureReason);

            // Set the coin to be king is needed
            if (isValidMove && m_Board.IsNeedToBeKing(currentPlayer, i_Move))
            {
                m_Board.ChangeCoinToKing(i_Move.To);
            }

            // Remove the eated coin is needed
            if (isValidMove && currentPlayer.EatInLastMove)
            {
                BoardPoint eatedCell = getEatedCell(currentPlayer, i_Move);
                m_Board.RemoveCoin(eatedCell);
            }

            if (isValidMove)
            {
                m_TurnManager.SwitchPlayerIfNeeded();
                m_Board.GetBoardCell(i_Move.From).ApplyChanges();
                m_Board.GetBoardCell(i_Move.To).ApplyChanges();
            }

            return isValidMove;
        }

        /// <summary>
        /// Check if need to end the current game. 
        /// Game is end if one of the folloing is hold:
        /// 1. There is no valid move for both sides (Tie)
        /// 2. There is no valid move for one of the players. In this case, the other player will announced  as the winner 
        /// </summary>
        public bool NeedToEndGame(out Player o_Winner)
        {
            o_Winner = null;

            List<BoardMove> oValidMoves = m_Board.GetValidMoves(eCoinSign.O);
            List<BoardMove> xValidMoves = m_Board.GetValidMoves(eCoinSign.X);

            // Tie - For both sides there is no legal moves
            bool hasValidMoves = oValidMoves.Count != 0 || xValidMoves.Count != 0;

            if (oValidMoves.Count > 0 && m_TurnManager.CurrentPlayer.Sign == eCoinSign.X && xValidMoves.Count == 0)
            {
                o_Winner = GameDetails.GetPlayerBySign(eCoinSign.O);
            }

            if (xValidMoves.Count > 0 && m_TurnManager.CurrentPlayer.Sign == eCoinSign.O && oValidMoves.Count == 0)
            {
                o_Winner = GameDetails.GetPlayerBySign(eCoinSign.X);
            }

            return o_Winner != null || !hasValidMoves;
        }

        private BoardPoint getEatedCell(Player i_Player, BoardMove i_Move)
        {
            int eatedCellColumn = getEatedCoinColumn(i_Move);
            int eatedCellRow = getEatedCoinRow(i_Player, i_Move);

            return new BoardPoint(eatedCellColumn, eatedCellRow);
        }

        /// <summary>
        /// Get the column of the coin that was eated in the <paramref name="i_EatingMove"/> eating move.
        /// </summary>
        private int getEatedCoinColumn(BoardMove i_EatingMove)
        {
            int column;

            if (i_EatingMove.To.Column > i_EatingMove.From.Column)
            {
                // Eat right (in X case) Eat left (in O case)
                column = i_EatingMove.From.Column + 1;
            }
            else
            {
                // Eat left (in X case) Eat right (in O case)
                column = i_EatingMove.From.Column - 1;
            }
  
            return column;
        }

        /// <summary>
        /// Get the row of the coin that was eated in the <paramref name="i_Move"/> eating move.
        /// </summary>
        private int getEatedCoinRow(Player i_Player, BoardMove i_Move)
        {
            int row;
            if (i_Player.Sign == eCoinSign.X)
            {
                if (i_Move.From.Row > i_Move.To.Row)
                { 
                    row = i_Move.From.Row - 1;
                }
                else
                { 
                    // King can eat back - In this path code, the GameRulesValidator already check if this move is liegle, so it must be a King move
                    row = i_Move.From.Row + 1;
                }
            }
            else
            {
                if (i_Move.From.Row > i_Move.To.Row)
                { 
                    // King can eat back - In this path code, the GameRulesValidator already check if this move is liegle, so it must be a King move
                    row = i_Move.From.Row - 1;
                }
                else
                { 
                    row = i_Move.From.Row + 1;
                }
            }

            return row;
        }

        /// <summary>
        /// Calc the game points.
        /// The winner will get the point diff between the winner and the loser
        /// </summary>
        private int getWinnerPoints(Player i_Winner, bool i_IsGameEndWithQuit)
        {
            int winnerPoints = 0;
            int loserPoints = 0;

            for (int i = 0; i < m_Board.Size; i++)
            {
                for (int j = 0; j < m_Board.Size; j++)
                {
                    Coin currentCoin = m_Board.GetCoin(new BoardPoint(j, i));
                    if (currentCoin != null)
                    {
                        int coinPoints = currentCoin.IsKing ? k_PointsForKing : k_PointsForCoin;
                        if (currentCoin.Sign == i_Winner.Sign)
                        {
                            winnerPoints += coinPoints;
                        }
                        else if (!i_IsGameEndWithQuit)
                        { // If the loser choosed to leave the game (by prees "Q") - we don't count for him any points.
                          // (it's like he have no coins left)
                            loserPoints += coinPoints;
                        }
                    }
                }
            }

            return winnerPoints - loserPoints;
        }

        /// <summary>
        /// Gets the computer next move according to the computer game strategy.
        /// </summary>
        public void ComputerNextMove(eCoinSign i_Sign)
        {
            BoardMove move = m_GameStrategy.GetNextMove(i_Sign);
            string errorMessage;
            TryMove(move, out errorMessage);
        }

        /// <summary>
        /// Gets the turn of the current player
        /// </summary>
        public Player CurrentPlayer
        {
            get
            {
                return m_TurnManager.CurrentPlayer;
            }
        }

        public Player AdversaryPlayer
        {
            get
            {
                return m_TurnManager.AdversaryPlayer;
            }
        }

        /// <summary>
        /// Gets the game board
        /// </summary>
        public Board Board
        {
            get
            {
                return m_Board;
            }
        }

        /// <summary>
        /// Gets the game details has they recived from the user
        /// </summary>
        public GameDetails GameDetails
        {
            get
            {
                return m_GameDetails;
            }
        }

        // Calculate the game points acording to those points
        private const int k_PointsForKing = 4;
        private const int k_PointsForCoin = 1;

        private TurnManager m_TurnManager;
        private Board m_Board;
        private GameDetails m_GameDetails;
        private Player m_Winner;
        private AIGameStrategy m_GameStrategy;
    }
}
