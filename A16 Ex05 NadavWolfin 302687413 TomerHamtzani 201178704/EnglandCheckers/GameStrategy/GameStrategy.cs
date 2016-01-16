using System.Collections.Generic;
using EnglandCheckers.Components;

namespace EnglandCheckers.GameStrategy
{
    /// <summary>
    /// The GameStrategy class is an abstract class that represent a computer game strategy.
    /// You can inherit from this class and implement the <see cref="GetNextMove"/> function that enable you to declare your next move
    /// </summary>
    public abstract class GameStrategy
    {
        protected GameStrategy(Board i_Board)
        {
            Board = i_Board;
        }

        /// <summary>
        /// Get the next game move
        /// </summary>
        public BoardMove GetNextMove(eCoinSign i_Sign)
        {
            List<BoardMove> validMoves = Board.GetValidMoves(i_Sign);
            return GetNextMove(validMoves);
        }

        /// <summary>
        /// Logic Implementation that return the next move in the game according to the strategy
        /// </summary>
        protected abstract BoardMove GetNextMove(List<BoardMove> i_ValidMoves);

        /// <summary>
        /// The game board - you can use the board while define your strategy
        /// </summary>
        protected readonly Board Board;
    }
}
