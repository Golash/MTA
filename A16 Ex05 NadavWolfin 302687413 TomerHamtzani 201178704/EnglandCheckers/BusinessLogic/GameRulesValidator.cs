using System.Linq;
using System.Collections.Generic;
using EnglandCheckers.Components;

namespace EnglandCheckers.BusinessLogic
{
    public class GameRulesValidator
    {
        /// <summary>
        /// C'tor
        /// </summary>
        internal GameRulesValidator(Board i_Board)
        {
            m_Board = i_Board;
        }

        /// <summary>
        /// Check that the player's move is legal according the game rule.   
        /// </summary>
        internal bool IsValidMove(Player i_Player, BoardMove i_MoveToCheck, out string o_InvalidMoveReason)
        {
            bool isValidMove = true;
            o_InvalidMoveReason = string.Empty;
            Coin coinToCheck = m_Board.GetCoin(i_MoveToCheck.From);

            if (m_Board.IsEmptyCell(i_MoveToCheck.From))
            {
                o_InvalidMoveReason = "Please choose a cell with a coin inside it";
                isValidMove = false;
            }

            if (isValidMove && !isCurrentPlayerCoin(i_Player, coinToCheck))
            {
                o_InvalidMoveReason = "Can't move adversary's coin";
                isValidMove = false;
            }

            if (isValidMove && isMoveToOccupiedCell(i_MoveToCheck))
            {
                o_InvalidMoveReason = "Can't move to occupied cell";
                isValidMove = false;
            }

            if (isValidMove && !verifyEatingMoveIfRequired(i_Player, i_MoveToCheck))
            {
                o_InvalidMoveReason = "You must eat the adversary coin when it possible";
                isValidMove = false;
            }

            if (isValidMove && !verifyContinueEatingIfRequired(i_Player, i_MoveToCheck))
            {
                o_InvalidMoveReason = "You must continue eat the adversary coin";
                isValidMove = false;
            }

            // Specific validation for king and regular coin move.
            if (coinToCheck != null)
            {
                if (coinToCheck.IsKing)
                {
                    if (isValidMove && !isValidKingCoinMove(i_Player, i_MoveToCheck, out o_InvalidMoveReason))
                    {
                        isValidMove = false;
                    }
                }
                else
                {
                    if (isValidMove && !isValidCoinMove(i_Player, i_MoveToCheck, out o_InvalidMoveReason))
                    {
                        isValidMove = false;
                    }
                }
            }

            return isValidMove;
        }

        /// <summary>
        /// Validate that is king's legal move
        /// </summary>
        private bool isValidKingCoinMove(Player i_Player, BoardMove i_MoveToCheck, out string o_InvalidMoveReason)
        {
            bool isValidMove = true;
            o_InvalidMoveReason = string.Empty;

            bool eatingMoveDownToUp = isEatingMoveDownToUp(i_MoveToCheck, i_Player.Sign);
            bool isMovingDownToUpInDiagonalLine = eatingMoveDownToUp || isMoveOneStepDownToUpInDiagonalLine(i_MoveToCheck);

            bool eatingMoveUpToDown = isEatingMoveUpToDown(i_MoveToCheck, i_Player.Sign);
            bool isMovingUpToDownInDiagonalLine = eatingMoveUpToDown || isMoveOneStepUpToDownInDiagonalLine(i_MoveToCheck);

            if (!(isMovingDownToUpInDiagonalLine || isMovingUpToDownInDiagonalLine))
            {
                o_InvalidMoveReason = "King Can only move one step backward or forward in diagonal line  (except in eating mode, in this case two steps is allowed)";
                isValidMove = false;
            }

            if (isValidMove)
            {
                // Sign that in last move the player eat adversary's coin
                i_Player.EatInLastMove = eatingMoveDownToUp || eatingMoveUpToDown;
            }

            return isValidMove;
        }

        /// <summary>
        /// Validate that is regular coin legal move
        /// </summary>
        private bool isValidCoinMove(Player i_Player, BoardMove i_MoveToCheck, out string o_InvalidMoveReason)
        {
            bool isValidMove = true;
            o_InvalidMoveReason = string.Empty;

            // Check if legal move according the sign 
            if (i_Player.Sign == eCoinSign.X)
            {
                bool eatingMoveDownToUp = isEatingMoveDownToUp(i_MoveToCheck, i_Player.Sign);
                if (!eatingMoveDownToUp && !isMoveOneStepDownToUpInDiagonalLine(i_MoveToCheck))
                {
                    o_InvalidMoveReason = "Can only move one step forward in diagonal line (except in eating mode, in this case two steps is allowed)";
                    isValidMove = false;
                }

                i_Player.EatInLastMove = eatingMoveDownToUp;
            }
            else
            {
                bool eatingMoveUpToDown = isEatingMoveUpToDown(i_MoveToCheck, i_Player.Sign);
                if (!eatingMoveUpToDown && !isMoveOneStepUpToDownInDiagonalLine(i_MoveToCheck))
                {
                    o_InvalidMoveReason = "Can only move one step forward in diagonal line (except in eating mode, in this case two steps is allowed)";
                    isValidMove = false;
                }

                i_Player.EatInLastMove = eatingMoveUpToDown;
            }

            return isValidMove;
        }

        /// <summary>
        /// Check if the move is eating move up to down.
        /// </summary>
        private bool isEatingMoveUpToDown(BoardMove i_MoveToCheck, eCoinSign i_CoinSignToCheck)
        {
            bool isEatingMove = false;

            // Check if eating up to down in lefe side, by check if the move is two step left in digonal line.
            if (((i_MoveToCheck.From.Column - 2) == i_MoveToCheck.To.Column) && ((i_MoveToCheck.From.Row + 2) == i_MoveToCheck.To.Row))
            {
                // Check that in one step left in diagonal line the coin sign is different than the current sign.
                Coin coin = m_Board.GetCoin(new BoardPoint(i_MoveToCheck.From.Column - 1, i_MoveToCheck.From.Row + 1));
                isEatingMove = m_Board.IsDifferentSign(coin, i_CoinSignToCheck);
            }
            else if (((i_MoveToCheck.From.Column + 2) == i_MoveToCheck.To.Column) && ((i_MoveToCheck.From.Row + 2) == i_MoveToCheck.To.Row))
            {
                // Check if eating up to down in right side, by check if the move is two step right in digonal line.
                // Check that in one step right in diagonal line the coin sign is different than the current sign
                Coin coin = m_Board.GetCoin(new BoardPoint(i_MoveToCheck.From.Column + 1, i_MoveToCheck.From.Row + 1));
                isEatingMove = m_Board.IsDifferentSign(coin, i_CoinSignToCheck);
            }

            return isEatingMove;
        }

        /// <summary>
        /// Check if the move is eating move down to up.
        /// </summary>
        private bool isEatingMoveDownToUp(BoardMove i_MoveToCheck, eCoinSign i_CoinSignToCheck)
        {
            bool isEatingMove = false;

            // Check if eating down to up in left, by check if the move is two step left in digonal line.
            if (((i_MoveToCheck.From.Column - 2) == i_MoveToCheck.To.Column) && ((i_MoveToCheck.From.Row - 2) == i_MoveToCheck.To.Row))
            {
                // Check that in one step left in diagonal line the coin sign is different than the current sign.
                Coin coin = m_Board.GetCoin(new BoardPoint(i_MoveToCheck.From.Column - 1, i_MoveToCheck.From.Row - 1));
                isEatingMove = m_Board.IsDifferentSign(coin, i_CoinSignToCheck);
            }
            else if (((i_MoveToCheck.From.Column + 2) == i_MoveToCheck.To.Column) && ((i_MoveToCheck.From.Row - 2) == i_MoveToCheck.To.Row))
            {
                // Check if eating down to up in right, by check if the move is two step right in digonal line.A
                // Check that in one step right in diagonal line the coin sign is different than the current sign.
                Coin coin = m_Board.GetCoin(new BoardPoint(i_MoveToCheck.From.Column + 1, i_MoveToCheck.From.Row - 1));
                isEatingMove = m_Board.IsDifferentSign(coin, i_CoinSignToCheck);
            }

            return isEatingMove;
        }

        /// <summary>
        /// Check if the player should continue eating.
        /// </summary>
        internal bool IsNeedToContinueEating(Player i_Player)
        {
            return m_Board.GetEatingMoves(i_Player.LastMove.To).Count > 0;
        }

        /// <summary>
        /// Compare between input coin and palyer coin
        /// </summary>
        private bool isCurrentPlayerCoin(Player i_Player, Coin i_Coin)
        {
            return i_Player.Sign == i_Coin.Sign;
        }

        /// <summary>
        /// Check if move to occupied cell in the board
        /// </summary>
        private bool isMoveToOccupiedCell(BoardMove i_MoveToCheck)
        {
            return !m_Board.IsEmptyCell(i_MoveToCheck.To);
        }

        /// <summary>
        /// Check if the move in one step up to down is in diagonal line
        /// </summary>
        private bool isMoveOneStepUpToDownInDiagonalLine(BoardMove i_MoveToCheck)
        {
            bool isOneStepInDiagonalLine = (((i_MoveToCheck.From.Column - 1) == i_MoveToCheck.To.Column) && ((i_MoveToCheck.From.Row + 1) == i_MoveToCheck.To.Row)) ||
                       (((i_MoveToCheck.From.Column + 1) == i_MoveToCheck.To.Column) && ((i_MoveToCheck.From.Row + 1) == i_MoveToCheck.To.Row));

            return isOneStepInDiagonalLine;
        }

        /// <summary>
        /// Check if the move in one step down to up is in diagonal line
        /// </summary>
        private bool isMoveOneStepDownToUpInDiagonalLine(BoardMove i_MoveToCheck)
        {
            bool isValidMove = ((i_MoveToCheck.From.Column - 1 == i_MoveToCheck.To.Column) && (i_MoveToCheck.From.Row - 1 == i_MoveToCheck.To.Row)) ||
                ((i_MoveToCheck.From.Column + 1 == i_MoveToCheck.To.Column) && (i_MoveToCheck.From.Row - 1 == i_MoveToCheck.To.Row));

            return isValidMove;
        }

        /// <summary>
        /// If the player ate in last move, and he should continue eating his next move should be continue eating
        /// </summary>
        private bool verifyContinueEatingIfRequired(Player i_Player, BoardMove i_MoveToCheck)
        {
            bool continueEatingIfRequired = true;

            if (i_Player.ContinuEating)
            {
                List<BoardMove> eatingMoves = m_Board.GetEatingMoves(i_Player.LastMove.To);

                // Check if player move contains in the moves that continue eating
                if (eatingMoves.Count > 0)
                {
                    if (!eatingMoves.Contains(i_MoveToCheck))
                    {
                        continueEatingIfRequired = false;
                    }
                }
            }

            return continueEatingIfRequired;
        }

        /// <summary>
        /// If the player has option to eat, he must to eat in his move.
        /// </summary>
        private bool verifyEatingMoveIfRequired(Player i_Player, BoardMove i_MoveToCheck)
        {
            List<BoardMove> eatingMovments = getEatingMovments(i_Player.Sign);

            return eatingMovments.Count() == 0 || eatingMovments.Contains(i_MoveToCheck);
        }

        /// <summary>
        /// Get all eating movment on board accordnig player sign
        /// </summary>
        private List<BoardMove> getEatingMovments(eCoinSign i_CoinSignToCheck)
        {
            List<BoardMove> eatingMovments = new List<BoardMove>();

            // Loop for all cells board.
            for (int i = 0; i < m_Board.Size; i++)
            {
                for (int j = 0; j < m_Board.Size; j++)
                {
                    // Check if the cell contains the relevant coin sign.
                    BoardPoint boardPoint = new BoardPoint(i, j);
                    Coin currentCoin = m_Board.GetCoin(boardPoint);
                    if (currentCoin == null || i_CoinSignToCheck != currentCoin.Sign)
                    {
                        continue;
                    }

                    // if has eating moves from the cell, add them to the list.
                    List<BoardMove> eatingMoves = m_Board.GetEatingMoves(boardPoint);
                    eatingMovments.AddRange(eatingMoves);
                }
            }

            return eatingMovments;
        }

        private readonly Board m_Board;
    }
}
