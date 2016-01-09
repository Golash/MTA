using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglandCheckers.Components
{
    /// <summary>
    /// The BoardCell class represent a cell in the board.
    /// Each cell is represented by Row and Column indexes
    /// </summary>
    public struct BoardCell
    {
        /// <summary>
        /// Create a new instance of BoardCell by the given column and row
        /// </summary>
        /// <param name="i_Column"></param>
        /// <param name="i_Row"></param>
        public BoardCell(int i_Column, int i_Row)
        {
            m_Row = i_Row;
            m_Column = i_Column;
        }

        /// <summary>
        /// Gets the row of the board cell
        /// </summary>
        public int Row 
        {
            get
            {
                return m_Row;
            }
        }

        /// <summary>
        /// Gets the column of the board cell
        /// </summary>
        public int Column
        {
            get
            {
                return m_Column;
            }
        }

        private int m_Column;
        private int m_Row;
    }
}
