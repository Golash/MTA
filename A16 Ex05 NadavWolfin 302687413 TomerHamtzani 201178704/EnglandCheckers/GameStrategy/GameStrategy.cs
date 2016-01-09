using System.Collections.Generic;
using EnglandCheckers.Components;

namespace EnglandCheckers.Strategy
{
    /// <summary>
    /// The GameStrategy class is an abstract class that represent a computer game strategy.
    /// You can inherit from this class and implement the <see cref="getNextMove"/> function that enable you to declare your next move
    /// </summary>
    public abstract class GameStrategy
    {
        public GameStrategy(Board i_Board)
        {
            m_Board = i_Board;
        }

        /// <summary>
        /// Get the next game move
        /// </summary>
        public BoardMove GetNextMove(eCoinSign i_Sign)
        {
            List<BoardMove> validMoves = m_Board.GetValidMoves(i_Sign);
            return getNextMove(validMoves);
        }

        /// <summary>
        /// Logic Implementation that return the next move in the game according to the strategy
        /// </summary>
        protected abstract BoardMove getNextMove(List<BoardMove> i_ValidMoves);

        /// <summary>
        /// The game board - you can use the board while define your strategy
        /// </summary>
        protected Board m_Board;
    }
}
