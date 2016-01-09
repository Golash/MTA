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

        protected override BoardMove getNextMove(List<BoardMove> i_ValidMoves)
        {
            BoardMove nextMove = null;
            foreach (BoardMove move in i_ValidMoves)
            {
                if (isKingMove(move))
                {
                    nextMove = move;
                    break;
                }

                if (nextMove == null && isMoveToSafeCell(move))
                {
                    nextMove = move;
                }
            }

            if (nextMove == null)
            {
                nextMove = m_RandomGameStrategy.GetNextMove(m_Sign);
            }

            return nextMove;
        }

        private bool isKingMove(BoardMove move)
        {
            Coin coin = m_Board.GetCoin(move.From);
            return move.To.Row == m_Board.LastBoardIndex && coin != null && !coin.IsKing;
        }

        private bool isMoveToSafeCell(BoardMove move)
        {
            bool isSafeMove = true;
            if (!isMoveToBoardBorders(move))
            {
                Coin coin = m_Board.GetCoin(move.From);
                if (coin.IsKing)
                {
                    isSafeMove &= isSafeMoveFromRightBack(move);
                    isSafeMove &= isSafeMoveFromLeftBack(move);
                }

                isSafeMove &= isSafeMoveFromRight(move);
                isSafeMove &= isSafeMoveFromLeft(move);
            }

            return isSafeMove;
        }

        private bool isMoveToBoardBorders(BoardMove move)
        {
            return move.To.Column == m_Board.LastBoardIndex || move.To.Column == 0 || move.To.Row == m_Board.LastBoardIndex || move.To.Row == 0;
        }

        private bool isSafeMoveFromRight(BoardMove move)
        {
            BoardCell cellThreatRight = new BoardCell(move.To.Column + 1, move.To.Row + 1);
            BoardCell cellThreatLeft = new BoardCell(move.To.Column - 1, move.To.Row + 1);
            BoardCell cellThreatBack = new BoardCell(move.To.Column + 1, move.To.Row - 1);

            // 1. Stack in the middle move
            //    X
            //  O
            //X
            // ======================================
            // 2. All posible threat cells are empty
            return isSafeMove(cellThreatRight, cellThreatLeft, cellThreatBack);
        }

        private bool isSafeMoveFromLeft(BoardMove move)
        {
            BoardCell cellThreatRight = new BoardCell(move.To.Column + 1, move.To.Row + 1);
            BoardCell cellThreatLeft = new BoardCell(move.To.Column - 1, move.To.Row + 1);
            BoardCell cellThreatBack = new BoardCell(move.To.Column - 1, move.To.Row + 1);

            // 1. Stack in the middle move
            // X
            //   O
            //     X
            // =========================================
            // 2. All posible threat cells are empty
            return isSafeMove(cellThreatRight, cellThreatLeft, cellThreatBack);
        }

        private bool isSafeMove(BoardCell cellThreatRight, BoardCell cellThreatLeft, BoardCell cellThreatBack)
        {
            bool isMoveToSafeCell = false;
            bool isStackInTheMiddleState = isSafeCoin(cellThreatRight) && (!isSafeCoin(cellThreatLeft) && (!isSafeCoin(cellThreatBack, true)) || !m_Board.IsCellExists(cellThreatBack));

            // All posible threat cells are empty
            bool isAllThreatCellsAreEmpty = isSafeCoin(cellThreatRight) && isSafeCoin(cellThreatLeft) && isSafeCoin(cellThreatBack, true);

            if (isStackInTheMiddleState || isAllThreatCellsAreEmpty)
            {
                isMoveToSafeCell = true;
            }
            return isMoveToSafeCell;
        }

        private bool isSafeMoveFromRightBack(BoardMove move)
        {
            BoardCell cellThreatRight = new BoardCell(move.To.Column + 1, move.To.Row - 1);
            BoardCell cellThreatLeft = new BoardCell(move.To.Column - 1, move.To.Row - 1);
            BoardCell cellThreatBack = new BoardCell(move.To.Column + 1, move.To.Row + 1);

            bool isStackInTheMiddle = isSafeCoin(cellThreatRight) && !isSafeCoin(cellThreatLeft, true) && !isSafeCoin(cellThreatBack);

            return isSafeMoveBackCoin(cellThreatRight, cellThreatLeft, cellThreatBack) || isStackInTheMiddle;
        }

        private bool isSafeMoveFromLeftBack(BoardMove move)
        {
            BoardCell cellThreatRight = new BoardCell(move.To.Column + 1, move.To.Row + 1);
            BoardCell cellThreatLeft = new BoardCell(move.To.Column - 1, move.To.Row + 1);
            BoardCell cellThreatBack = new BoardCell(move.To.Column - 1, move.To.Row - 1);

            bool isStackInTheMiddle = isSafeCoin(cellThreatLeft) && !isSafeCoin(cellThreatRight, true) && !isSafeCoin(cellThreatBack);

            return isSafeMoveBackCoin(cellThreatRight, cellThreatLeft, cellThreatBack) || isStackInTheMiddle;
        }

        private bool isSafeMoveBackCoin(BoardCell cellThreatRight, BoardCell cellThreatLeft, BoardCell cellThreatBack)
        {
            bool isSafeFromRight = isSafeCoin(cellThreatRight, true);
            bool isAllThreatCellsAreEmpty = isSafeCoin(cellThreatRight) && isSafeCoin(cellThreatLeft) && isSafeCoin(cellThreatBack);

            return isSafeFromRight && isAllThreatCellsAreEmpty;
        }

        private bool isSafeCoin(BoardCell cell, bool i_IsKingCell = false)
        {
            Coin coin = m_Board.GetCoin(cell);
            bool isSageCoin = true;
            if (m_Board.IsCellExists(cell))
            {
                isSageCoin =  m_Board.IsEmptyCell(cell) || coin.Sign == m_Sign || (i_IsKingCell && !coin.IsKing);
            }
            return isSageCoin;
        }

        private RandomGameStrategy m_RandomGameStrategy;
        private const eCoinSign m_Sign = eCoinSign.O; 
    }
}