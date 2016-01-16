using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglandCheckers.Components
{
    public delegate void BoardCellChangedEventHandler(object obj, EventArgs e);

    /// <summary>
    /// The BoardPoint class represent a cell in the board.
    /// Each cell is represented by Row and Column indexes
    /// </summary>
    public class BoardCell
    {
        /// <summary>
        /// Create a new instance of BoardPoint by the given column and row
        /// </summary>
        /// <param name="i_Column"></param>
        /// <param name="i_Row"></param>
        public BoardCell(BoardPoint i_BoardPoint, bool i_Enabeld)
        {
            m_BoardPoint = i_BoardPoint;
            m_Enabled = i_Enabeld;
        }

        public void RemoveCoin()
        {
            m_Coin = null;
            ApplyChanges();
        }

        public bool IsEmptyCell()
        {
            return m_Coin == null;
        }

        protected virtual void OnBoardCellChanged()
        {
            if (BoardCellChanged != null)
            {
                BoardCellChanged.Invoke(this, EventArgs.Empty);
            }
        }

        internal void ApplyChanges()
        {
            OnBoardCellChanged();
        }

        public bool Enabled
        {
            get
            {
                return m_Enabled;
            }
        }

        public Coin Coin
        {
            get
            {
                return m_Coin;
            }
            set
            {
                m_Coin = value;
            }
        }

        public BoardPoint BoardPoint
        {
            get
            {
                return m_BoardPoint;
            }
        }

        public event BoardCellChangedEventHandler BoardCellChanged;
        private readonly bool m_Enabled;
        private readonly BoardPoint m_BoardPoint;
        private Coin m_Coin;
    }
}
