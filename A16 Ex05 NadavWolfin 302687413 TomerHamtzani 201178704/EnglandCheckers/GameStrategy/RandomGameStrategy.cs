using System;
using System.Collections.Generic;
using System.Linq;
using EnglandCheckers.Components;

namespace EnglandCheckers.Strategy
{
    /// <summary>
    /// The class RandomGameStrategy implement a game strategy mechanism that get all the valid moves according
    /// to the board state and choose one movment randomlly
    /// </summary>
    public class RandomGameStrategy : GameStrategy
    {
        /// <summary>
        /// Create a new instance of the RandomGameStrategy class
        /// </summary>
        public RandomGameStrategy(Board i_Board) : base(i_Board)
        {
            m_Random = new Random();
        }
        
        /// <summary>
        /// Gets the next move in the board according to the board state
        /// </summary>
        protected override BoardMove getNextMove(List<BoardMove> i_ValidMoves)
        {
            int randomMoveIndex = m_Random.Next(0, i_ValidMoves.Count());
            return i_ValidMoves[randomMoveIndex];
        }

        private readonly Random m_Random;
    }
}
