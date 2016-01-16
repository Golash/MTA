namespace EnglandCheckers.Components
{
    /// <summary>
    /// This class represent a move in the board (from one cell to another)
    /// </summary>
    public class BoardMove
    {
        /// <summary>
        /// Create a new instace of the BoardMove by the given from cell and to cell
        /// </summary>
        public BoardMove(BoardPoint i_From, BoardPoint i_To)
        {
            r_From = i_From;
            r_To = i_To;
            r_MoveStr = calcMoveStr();
        }

        /// <summary>
        /// Generate the movment string that represent the movment.
        /// </summary>
        /// <returns></returns>
        private string calcMoveStr()
        {
            char fromColumnLetter = (char)(Board.ColumnStartLetter + r_From.Column);
            char fromRowLetter = (char)(Board.RowStartLetter + r_From.Row);
            char toColumnLetter = (char)(Board.ColumnStartLetter + r_To.Column);
            char toRowLetter = (char)(Board.RowStartLetter + r_To.Row);

            return string.Format("{0}{1}>{2}{3}", fromColumnLetter, fromRowLetter, toColumnLetter, toRowLetter);
        }

        /// <summary>
        /// Get the string that represent the move (Template: "COLrow>COLrow")
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return r_MoveStr;
        }

        /// <summary>
        /// Determines whether the specified BoardMove is equal to the current BoardMove.
        /// </summary>
        public override bool Equals(object i_Obj)
        {
            BoardMove bordMove = i_Obj as BoardMove;

            return bordMove != null && r_From.Column == bordMove.From.Column && r_From.Row == bordMove.From.Row &&
                    r_To.Column == bordMove.To.Column && r_To.Row == bordMove.To.Row;
        }

        /// <summary>
        /// Determines the has code function for BoardMove.
        /// </summary>
        /// <remarks>
        /// We need to override GetHashCode() because we override Equals()
        /// </remarks>
        public override int GetHashCode()
        {
            return (r_From.Row * 1000) + (r_From.Column * 100) + (r_To.Row * 10) + r_To.Column;
        }

        /// <summary>
        /// Gets the from cell of the move
        /// </summary>
        public BoardPoint From
        {
            get
            {
                return r_From;
            }
        }

        /// <summary>
        /// Gets the To cell of the move
        /// </summary>
        public BoardPoint To
        {
            get
            {
                return r_To;
            }
        }

        private readonly BoardPoint r_From;
        private readonly BoardPoint r_To;
        private readonly string r_MoveStr;
    }
}
