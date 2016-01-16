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
            this(new BoardPoint(i_FromColumn, i_FromRow), new BoardPoint(i_ToColumn, i_ToRow))
        {
        }

        /// <summary>
        /// Create a new instace of the BoardMove by the given from cell and to cell
        /// </summary>
        public BoardMove(BoardPoint i_From, BoardPoint i_To)
        {
            m_From = i_From;
            m_To = i_To;
            m_MoveStr = calcMoveStr();
        }

        /// <summary>
        /// Try to convert the column character into column index
        /// </summary>
        private static bool tryParseColumn(char i_Column, out int o_ColumnIndex)
        {
            return tryGetIndex(i_Column, Board.ColumnStartLetter, Board.ColumnEndLetter, out o_ColumnIndex);
        }

        /// <summary>
        /// Try to create a BoardMove by the given <paramref name="i_MoveStr"/> string
        /// </summary>
        public static bool TryParse(string i_MoveStr, Board i_Board, out BoardMove o_Move)
        {
            o_Move = null;
            string[] parts = i_MoveStr.Split('>');
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
            isValidMove = isValidMove && tryParseColumn(fromPart[0], out fromColumn);

            // from row
            int fromRow = -1;
            isValidMove = isValidMove && tryParseRow(fromPart[1], out fromRow);

            // to column
            int toColumn = -1;
            isValidMove = isValidMove && tryParseColumn(toPart[0], out toColumn);

            // to row
            int toRow = -1;
            isValidMove = isValidMove && tryParseRow(toPart[1], out toRow);

            if (isValidMove)
            {
                o_Move = new BoardMove(new BoardPoint(fromColumn, fromRow), new BoardPoint(toColumn, toRow));
            }
            
            return isValidMove;
        }

        /// <summary>
        /// Try to convert the row character into row index
        /// </summary>
        private static bool tryParseRow(char i_Row, out int o_RowIndex)
        {
            return tryGetIndex(i_Row, Board.RowStartLetter, Board.RowEndLetter, out o_RowIndex);
        }

        /// <summary>
        /// Helper function that convert between the given character <paramref name="i_Character"/> to the correlate index
        /// according to the start (<paramref name="i_StartLetter"/>) and end (<paramref name="i_EndLetter"/>) boarders.
        /// </summary>
        private static bool tryGetIndex(char i_Character, char i_StartLetter, char i_EndLetter, out int o_Index)
        {
            o_Index = -1;
            if (i_StartLetter <= i_Character && i_Character <= i_EndLetter)
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
        public override bool Equals(object i_Obj)
        {
            BoardMove bordMove = i_Obj as BoardMove;

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
        public BoardPoint From
        {
            get
            {
                return m_From;
            }
        }

        /// <summary>
        /// Gets the To cell of the move
        /// </summary>
        public BoardPoint To
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

        private BoardPoint m_From;
        private BoardPoint m_To;
        private string m_MoveStr;
    }
}
