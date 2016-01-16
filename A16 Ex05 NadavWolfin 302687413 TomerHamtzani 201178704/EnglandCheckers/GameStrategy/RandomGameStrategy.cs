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
            r_Random = new Random();
        }
        
        /// <summary>
        /// Gets the next move in the board according to the board state
        /// </summary>
        protected override BoardMove GetNextMove(List<BoardMove> i_ValidMoves)
        {
            int randomMoveIndex = r_Random.Next(0, i_ValidMoves.Count());
            return i_ValidMoves[randomMoveIndex];
        }

        private readonly Random r_Random;
    }
}
