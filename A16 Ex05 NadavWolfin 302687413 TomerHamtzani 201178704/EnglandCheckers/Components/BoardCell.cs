using System;

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
        public BoardCell(BoardPoint i_BoardPoint, bool i_Enabeld)
        {
            r_BoardPoint = i_BoardPoint;
            r_Enabled = i_Enabeld;
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
                return r_Enabled;
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
                return r_BoardPoint;
            }
        }

        public event BoardCellChangedEventHandler BoardCellChanged;

        private readonly bool r_Enabled;
        private readonly BoardPoint r_BoardPoint;
        private Coin m_Coin;
    }
}
