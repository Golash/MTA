using System;
using System.Collections.Generic;
using EnglandCheckers.Components;

namespace EnglandCheckers.Strategy
{
    /// <summary>
    /// The AIGameStrategy is an AI implementation for O player (the computer side)
    /// </summary>
    public class AIGameStrategy : GameStrategy
    {
        public AIGameStrategy(Board i_Board) : base(i_Board)
        {
            m_RandomGameStrategy = new RandomGameStrategy(i_Board);
        }

        /// <summary>
        /// Get next move by AI logic
        /// </summary>
        protected override BoardMove getNextMove(List<BoardMove> i_ValidMoves)
        {
            BoardMove nextMove = null;
            foreach (BoardMove move in i_ValidMoves)
            {
                // If the next move will create a king do it and break.
                if (isCreateKingMove(move))
                {
                    nextMove = move;
                    break;
                }

                // if there is no step that create a king, move the safe cell (if exists)
                if (nextMove == null && isMoveToSafeCell(move))
                {
                    nextMove = move;
                }
            }

            // if there is no recommended step, tandom from the valid movs
            if (nextMove == null)
            {
                nextMove = m_RandomGameStrategy.GetNextMove(k_ComputerSign);
            }

            return nextMove;
        }

        /// <summary>
        /// CHeck if the move create a king
        /// </summary>
        private bool isCreateKingMove(BoardMove i_MoveToCheck)
        {
            Coin coin = m_Board.GetCoin(i_MoveToCheck.From);

            return i_MoveToCheck.To.Row == m_Board.LastBoardIndex && coin != null && !coin.IsKing;
        }

        /// <summary>
        /// Valid that the move is to safe cell (cell that the adversary can't eat the coin)
        /// </summary>
        private bool isMoveToSafeCell(BoardMove i_MoveToCheck)
        {
            bool isSafeMove = true;

            // if the move is to board borders is a safe move
            if (!isMoveToBoardBorders(i_MoveToCheck))
            {
                Coin coin = m_Board.GetCoin(i_MoveToCheck.From);

                // if is king, check also the back moves
                if (coin.IsKing)
                {
                    isSafeMove &= isSafeMoveFromRightBack(i_MoveToCheck);
                    isSafeMove &= isSafeMoveFromLeftBack(i_MoveToCheck);
                }

                isSafeMove &= isSafeMoveFromRight(i_MoveToCheck);
                isSafeMove &= isSafeMoveFromLeft(i_MoveToCheck);
            }

            return isSafeMove;
        }

        /// <summary>
        /// Check if the move is to board orders
        /// </summary>
        private bool isMoveToBoardBorders(BoardMove i_MoveToCheck)
        {
            return i_MoveToCheck.To.Column == m_Board.LastBoardIndex || i_MoveToCheck.To.Column == 0 || i_MoveToCheck.To.Row == m_Board.LastBoardIndex || i_MoveToCheck.To.Row == 0;
        }

        /// <summary>
        /// Check that if the coin will move in right diagonal, thare is no option to eat to coin (from right left or backe in case of king)
        /// </summary>
        private bool isSafeMoveFromRight(BoardMove i_MoveToCheck)
        {
            BoardPoint cellThreatRight = new BoardPoint(i_MoveToCheck.To.Column + 1, i_MoveToCheck.To.Row + 1);
            BoardPoint cellThreatLeft = new BoardPoint(i_MoveToCheck.To.Column - 1, i_MoveToCheck.To.Row + 1);
            BoardPoint cellThreatBack = new BoardPoint(i_MoveToCheck.To.Column + 1, i_MoveToCheck.To.Row - 1);

            // 1. Stack in the middle move
            //     X
            //   O
            // X
            // ======================================
            // 2. All posible threat cells are empty
            return isSafeMove(cellThreatRight, cellThreatLeft, cellThreatBack);
        }

        /// <summary>
        /// Check that if the coin will move in left diagonal, thare is no option to eat to coin (from right left or backe in case of king)
        /// </summary>
        private bool isSafeMoveFromLeft(BoardMove i_MoveToCheck)
        {
            BoardPoint cellThreatRight = new BoardPoint(i_MoveToCheck.To.Column + 1, i_MoveToCheck.To.Row + 1);
            BoardPoint cellThreatLeft = new BoardPoint(i_MoveToCheck.To.Column - 1, i_MoveToCheck.To.Row + 1);
            BoardPoint cellThreatBack = new BoardPoint(i_MoveToCheck.To.Column - 1, i_MoveToCheck.To.Row + 1);

            // 1. Stack in the middle move
            // X
            //   O
            //     X
            // =========================================
            // 2. All posible threat cells are empty
            return isSafeMove(cellThreatRight, cellThreatLeft, cellThreatBack);
        }

        /// <summary>
        /// Check if the move is to safe cell
        /// </summary>
        private bool isSafeMove(BoardPoint i_CellThreatRight, BoardPoint i_CellThreatLeft, BoardPoint i_CellThreatBack)
        {
            bool isMoveToSafeCell = false;
            bool isCellThreatBack = (!isSafeCoin(i_CellThreatBack, true)) || !m_Board.IsCellExists(i_CellThreatBack);
            bool isStackInTheMiddleState = isSafeCoin(i_CellThreatRight) && (!isSafeCoin(i_CellThreatLeft) && isCellThreatBack);

            // All posible threat cells are empty
            bool isAllThreatCellsAreEmpty = isSafeCoin(i_CellThreatRight) && isSafeCoin(i_CellThreatLeft) && isSafeCoin(i_CellThreatBack, true);

            if (isStackInTheMiddleState || isAllThreatCellsAreEmpty)
            {
                isMoveToSafeCell = true;
            }

            return isMoveToSafeCell;
        }

        /// <summary>
        /// Check that if the coin will move in right back diagonal, thare is no option to eat to coin (from right left or backe in case of king)
        /// </summary>
        private bool isSafeMoveFromRightBack(BoardMove i_MoveToCheck)
        {
            BoardPoint cellThreatRight = new BoardPoint(i_MoveToCheck.To.Column + 1, i_MoveToCheck.To.Row - 1);
            BoardPoint cellThreatLeft = new BoardPoint(i_MoveToCheck.To.Column - 1, i_MoveToCheck.To.Row - 1);
            BoardPoint cellThreatBack = new BoardPoint(i_MoveToCheck.To.Column + 1, i_MoveToCheck.To.Row + 1);

            return isSafeMove(cellThreatRight, cellThreatLeft, cellThreatBack);
        }

        /// <summary>
        /// Check that if the coin will move in left back diagonal, thare is no option to eat to coin (from right left or backe in case of king)
        /// </summary>
        private bool isSafeMoveFromLeftBack(BoardMove i_MoveToCheck)
        {
            BoardPoint cellThreatRight = new BoardPoint(i_MoveToCheck.To.Column + 1, i_MoveToCheck.To.Row + 1);
            BoardPoint cellThreatLeft = new BoardPoint(i_MoveToCheck.To.Column - 1, i_MoveToCheck.To.Row + 1);
            BoardPoint cellThreatBack = new BoardPoint(i_MoveToCheck.To.Column - 1, i_MoveToCheck.To.Row - 1);

            return isSafeMove(cellThreatRight, cellThreatLeft, cellThreatBack);
        }

        /// <summary>
        /// Valid that the cell not exists adversary coin that can eat the coin
        /// </summary>
        private bool isSafeCoin(BoardPoint i_CellToCheck, bool i_OnlyKingCanEat = false)
        {
            Coin coin = m_Board.GetCoin(i_CellToCheck);
            bool isSageCoin = true;
            if (m_Board.IsCellExists(i_CellToCheck))
            {
                isSageCoin = m_Board.IsEmptyCell(i_CellToCheck) || coin.Sign == k_ComputerSign || (i_OnlyKingCanEat && !coin.IsKing);
            }

            return isSageCoin;
        }

        private RandomGameStrategy m_RandomGameStrategy;
        private const eCoinSign k_ComputerSign = eCoinSign.O; 
    }
}