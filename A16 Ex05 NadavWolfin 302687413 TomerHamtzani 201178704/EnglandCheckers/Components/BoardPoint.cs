using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglandCheckers.Components
{
    public struct BoardPoint
    {
        public BoardPoint(int i_Column, int i_Row)
        {
            r_Row = i_Row;
            r_Column = i_Column;
        }

        /// <summary>
        /// Gets the row of the board cell
        /// </summary>
        public int Row
        {
            get
            {
                return r_Row;
            }
        }

        /// <summary>
        /// Gets the column of the board cell
        /// </summary>
        public int Column
        {
            get
            {
                return r_Column;
            }
        }

        private readonly int r_Column;
        private readonly int r_Row;
    }
}
