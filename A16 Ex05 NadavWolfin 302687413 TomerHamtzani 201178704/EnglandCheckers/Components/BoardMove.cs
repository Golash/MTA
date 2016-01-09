using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglandCheckers.Components
{
    /// <summary>
    /// This class represent a move in the board (from one cell to another)
    /// </summary>
    public class BoardMove
    {
        /// <summary>
        /// Create a new instance of  the BoardMove by the spesific column and row
        /// </summary>
        public BoardMove(int i_FromColumn, int i_FromRow, int i_ToColumn, int i_ToRow) : 
            this(new BoardCell(i_FromColumn, i_FromRow), new BoardCell(i_ToColumn, i_ToRow))
        {
        }

        /// <summary>
        /// Create a new instace of the BoardMove by the given from cell and to cell
        /// </summary>
        public BoardMove(BoardCell i_From, BoardCell i_To)
        {
            m_From = i_From;
            m_To = i_To;
            m_MoveStr = calcMoveStr();
        }

        /// <summary>
        /// Try to convert the column character into column index
        /// </summary>
        private static bool TryParseColumn(char i_Column, out int o_ColumnIndex)
        {
            return TryGetIndex(i_Column, Board.ColumnStartLetter, Board.ColumnEndLetter, out o_ColumnIndex);
        }

        /// <summary>
        /// Try to create a BoardMove by the given <paramref name="moveStr"/> string
        /// </summary>
        public static bool TryParse(string moveStr, Board board, out BoardMove o_move)
        {
            o_move = null;
            string[] parts = moveStr.Split('>');
            bool isValidMove = parts.Length == 2;

            string fromPart = string.Empty;
            string toPart = string.Empty;
            if (isValidMove)
            {
                fromPart = parts[0];
                toPart = parts[1];
            }

            isValidMove = isValidMove && fromPart.Length == 2 && toPart.Length == 2;

            // from column
            int fromColumn = -1;
            isValidMove = isValidMove && TryParseColumn(fromPart[0], out fromColumn);

            // from row
            int fromRow = -1;
            isValidMove = isValidMove && TryParseRow(fromPart[1], out fromRow);

            // to column
            int toColumn = -1;
            isValidMove = isValidMove && TryParseColumn(toPart[0], out toColumn);

            // to row
            int toRow = -1;
            isValidMove = isValidMove && TryParseRow(toPart[1], out toRow);

            if (isValidMove)
            {
                o_move = new BoardMove(new BoardCell(fromColumn, fromRow), new BoardCell(toColumn, toRow));
            }
            
            return isValidMove;
        }

        /// <summary>
        /// Try to convert the row character into row index
        /// </summary>
        private static bool TryParseRow(char i_Row, out int o_RowIndex)
        {
            return TryGetIndex(i_Row, Board.RowStartLetter, Board.RowEndLetter, out o_RowIndex);
        }

        /// <summary>
        /// Helper function that convert between the given character <paramref name="i_Character"/> to the correlate index
        /// according to the start (<paramref name="i_StartLetter"/>) and end (<paramref name="i_endLetter"/>) boarders.
        /// </summary>
        private static bool TryGetIndex(char i_Character, char i_StartLetter, char i_endLetter, out int o_Index)
        {
            o_Index = -1;
            if (i_StartLetter <= i_Character && i_Character <= i_endLetter)
            {
                o_Index = (int)(i_Character - i_StartLetter);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Generate the movment string that represent the movment.
        /// </summary>
        /// <returns></returns>
        private string calcMoveStr()
        {
            char FromColumnLetter = (char)(Board.ColumnStartLetter + m_From.Column);
            char fromRowLetter = (char)(Board.RowStartLetter + m_From.Row);
            char ToColumnLetter = (char)(Board.ColumnStartLetter + m_To.Column);
            char toRowLetter = (char)(Board.RowStartLetter + m_To.Row);

            return string.Format("{0}{1}>{2}{3}", FromColumnLetter, fromRowLetter, ToColumnLetter, toRowLetter);
        }

        /// <summary>
        /// Get the string that represent the move (Template: "COLrow>COLrow")
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return MoveStr;
        }

        /// <summary>
        /// Determines whether the specified BoardMove is equal to the current BoardMove.
        /// </summary>
        public override bool Equals(object obj)
        {
            BoardMove bordMove = obj as BoardMove;
            return bordMove != null && m_From.Column == bordMove.From.Column && m_From.Row == bordMove.From.Row &&
                    m_To.Column == bordMove.To.Column && m_To.Row == bordMove.To.Row;
        }

        /// <summary>
        /// Determines the has code function for BoardMove.
        /// </summary>
        /// <remarks>
        /// We need to override GetHashCode() because we override Equals()
        /// </remarks>
        public override int GetHashCode()
        {
            return (m_From.Row * 1000) + (m_From.Column * 100) + (m_To.Row * 10) + m_To.Column;
        }

        /// <summary>
        /// Gets the from cell of the move
        /// </summary>
        public BoardCell From
        {
            get
            {
                return m_From;
            }
        }

        /// <summary>
        /// Gets the To cell of the move
        /// </summary>
        public BoardCell To
        {
            get
            {
                return m_To;
            }
        }

        /// <summary>
        /// Gets the move str
        /// </summary>
        public string MoveStr
        {
            get
            {
                return m_MoveStr;
            }
        }

        private BoardCell m_From;
        private BoardCell m_To;
        private string m_MoveStr;
    }
}
