using System;
using System.Collections.Generic;
using System.Text;
using EnglandCheckers.BusinessLogic;

namespace EnglandCheckers.Components
{
    /// <summary>
    /// The Board class represent the checkers board.
    /// The board manage all the movments in the board.
    /// </summary>
    public class Board
    {
        /// <summary>
        /// Create a new instance of the Board class.
        /// </summary>
        public Board(GameDetails i_GameDetailes)
        {
            // Create the board matrix according to the requested board size
            m_BordSize = i_GameDetailes.BoardSize;
            m_GameBoard = new BoardCell[m_BordSize, m_BordSize];

            m_GameDetailes = i_GameDetailes;
            r_Player1 = i_GameDetailes.Player1;
            r_Player2 = i_GameDetailes.Player2;
            m_GameRulesValidator = new GameRulesValidator(this);

            // Set each player a different side
            r_Player1.Sign = eCoinSign.X;
            r_Player2.Sign = eCoinSign.O;

            // Set the last column index
            r_LastBoardIndex = m_BordSize - 1;

            // Set the start and end letters
            m_ColumnEndLetter = (char)(sr_ColumnStartLetter + r_LastBoardIndex);
            m_RowEndLetter = (char)(sr_RowStartLetter + r_LastBoardIndex);

            fillBoard();
        }

        /// <summary>
        /// Fill the board with coin soldiers
        /// </summary>
        private void fillBoard()
        {
            createBoard();
            fillCoinsOnBoard();
        }

        private void fillCoinsOnBoard()
        {
            eCoinSign coinSign = eCoinSign.O;
            int topRowIndexForPlayer1 = (m_BordSize - 1) / 2;

            // Run all overt the rows
            for (int i = 0; i < m_BordSize; i++)
            {
                // keep on two rows space between player1 and player2
                if (i == topRowIndexForPlayer1 || i == (topRowIndexForPlayer1 + 1))
                {
                    coinSign = eCoinSign.X;
                    continue;
                }

                // Fill the current row (run all over the columns)
                for (int j = 0; j < m_BordSize; j++)
                {
                    BoardCell boardCell = GetBoardCell(new BoardPoint(j, i));
                    if (boardCell.Enabled)
                    {
                        boardCell.Coin = new Coin(coinSign);
                    }
                }
            }
        }

        private void createBoard()
        {
            bool isCellEnabled = false;

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    m_GameBoard[i, j] = new BoardCell(new BoardPoint(j, i), isCellEnabled);
                    if (j != r_LastBoardIndex)
                    {
                        isCellEnabled = !isCellEnabled;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the string that represent the Board
        /// </summary>
        public override string ToString()
        {
            StringBuilder boardStringBuilder = new StringBuilder();

            // print the first letters row
            const string k_SpaceBetweenLetters = "   ";
            boardStringBuilder.Append(k_SpaceBetweenLetters);
            for (char letter = sr_ColumnStartLetter; letter <= ColumnEndLetter; letter++)
            {
                boardStringBuilder.AppendFormat("{0}{1}", letter, k_SpaceBetweenLetters);
            }

            // build the separatpr
            string rowsSeparator = getBoardRowsSeperator();
            boardStringBuilder.AppendFormat("{0}{1}{0}", Environment.NewLine, rowsSeparator);

            // start to build the rows
            char openRowLetter = sr_RowStartLetter;
            for (int i = 0; i < m_BordSize; i++)
            {
                // add the open letter to the row
                boardStringBuilder.AppendFormat("{0}", openRowLetter);
                openRowLetter++;

                // Fill all the row cells
                for (int j = 0; j < m_BordSize; j++)
                {
                    Coin currentCoin = m_GameBoard[i, j].Coin;
                    string currentCellValue = k_EmptyCellSign;
                    if (currentCoin != null)
                    {
                        currentCellValue = currentCoin.ToString();
                    }

                    boardStringBuilder.AppendFormat("| {0} ", currentCellValue);
                }

                boardStringBuilder.AppendFormat("|{0}{1}{0}", Environment.NewLine, rowsSeparator);
            }

            return boardStringBuilder.ToString();
        }

        /// <summary>
        /// Gets the separator line between each row in the board
        /// (e.g. ======================)
        /// </summary>
        private string getBoardRowsSeperator()
        {
            // (3 '=' for each cell + 1 '=' between cells + 2 '=' for the start + 2 '=' for the end
            int separatorLength = (3 * m_BordSize) + r_LastBoardIndex - 1 + 2 + 2;
            StringBuilder lineSeparator = new StringBuilder();
            for (int i = 0; i < separatorLength; i++)
            {
                lineSeparator.Append("=");
            }

            return lineSeparator.ToString();
        }

        /// <summary>
        /// Try to make the given <paramref name="i_Move"/> by the given <paramref name="i_Player"/>.
        /// If the move is a valid move (according to rules in <see cref="GameRulesValidator"/>) it will be applay
        /// and true will return, otherwise <paramref name="o_FailureReason"/> will contain the invalid movce reason
        /// and fale will return.
        /// </summary>
        internal bool TryMove(Player i_Player, BoardMove i_Move, out string o_FailureReason)
        {
            bool isValidMove = m_GameRulesValidator.IsValidMove(i_Player, i_Move, out o_FailureReason);

            // Check if the move is a valid move by the given player
            if (isValidMove)
            {
                i_Player.LastMove = i_Move;
                makeMove(i_Move);
            }

            return isValidMove;
        }

        /// <summary>
        /// Applay the given <see cref="i_Move"/> move in the board
        /// </summary>
        private void makeMove(BoardMove i_Move)
        {
            m_GameBoard[i_Move.To.Row, i_Move.To.Column].Coin = m_GameBoard[i_Move.From.Row, i_Move.From.Column].Coin;
            m_GameBoard[i_Move.From.Row, i_Move.From.Column].Coin = null;
        }

        /// <summary>
        /// Get the coin that locate at the given cell.
        /// If the cell not exists or the cell is empty - null will return
        /// </summary>
        public Coin GetCoin(BoardPoint i_Cell)
        {
            Coin coin = null;
            if (IsCellExists(i_Cell))
            {
                coin = m_GameBoard[i_Cell.Row, i_Cell.Column].Coin;
            }

            return coin;
        }

        /// <summary>
        /// Get the board cell that locate at the given point.
        /// If the board cell not exists - null will return
        /// </summary>
        public BoardCell GetBoardCell(BoardPoint i_BoardPoint)
        {
            BoardCell boardCell = null;
            if (IsCellExists(i_BoardPoint))
            {
                boardCell = m_GameBoard[i_BoardPoint.Row, i_BoardPoint.Column];
            }

            return boardCell;
        }

        /// <summary>
        /// Check if the given cell is empty.
        /// If the cell not exists - false will return
        /// </summary>
        public bool IsEmptyCell(BoardPoint i_Cell)
        {
            return IsCellExists(i_Cell) && m_GameBoard[i_Cell.Row, i_Cell.Column].IsEmptyCell();
        }

        /// <summary>
        /// Check if the given cell is exists in the board.
        /// </summary>
        public bool IsCellExists(BoardPoint i_BoardPoint)
        {
            return i_BoardPoint.Row >= 0 && i_BoardPoint.Row < m_BordSize && 
                i_BoardPoint.Column >= 0 && i_BoardPoint.Column < m_BordSize;
        }

        /// <summary>
        /// Gets the optionsl eating moves for the given cell
        /// </summary>
        /// <returns></returns>
        public List<BoardMove> GetEatingMoves(BoardPoint i_BoardPoint)
        {
            List<BoardMove> eatingMoves = new List<BoardMove>();
            Coin coin = GetCoin(i_BoardPoint);
            if (coin != null)
            {
                if (coin.IsKing)
                {
                    addEatingMovesUpToDown(coin, i_BoardPoint, eatingMoves);
                    addEatingMovesDownToUp(coin, i_BoardPoint, eatingMoves);
                }
                else if (coin.Sign == eCoinSign.X)
                {
                    addEatingMovesDownToUp(coin, i_BoardPoint, eatingMoves);
                }
                else if (coin.Sign == eCoinSign.O)
                {
                    addEatingMovesUpToDown(coin, i_BoardPoint, eatingMoves);
                }
            }

            return eatingMoves;
        }

        /// <summary>
        /// Add potential eating moves for the given coin (for the player that start the game in the bottom of the board)
        /// </summary>
        private void addEatingMovesDownToUp(Coin i_Coin, BoardPoint i_BoardPoint, List<BoardMove> i_EatingMoves)
        {
            // Chcek if has eating movement for to coin in right side
            BoardPoint rightSideCell = new BoardPoint(i_BoardPoint.Column + 2, i_BoardPoint.Row - 2);
            if (IsCellExists(rightSideCell) && IsEmptyCell(rightSideCell))
            {
                Coin potentailCoinToEat = GetCoin(new BoardPoint(i_BoardPoint.Column + 1, i_BoardPoint.Row - 1));
                bool hasPotentioalCoinToEat = IsDifferentSign(potentailCoinToEat, i_Coin.Sign);
                if (hasPotentioalCoinToEat)
                {
                    i_EatingMoves.Add(new BoardMove(i_BoardPoint, rightSideCell));
                }
            }

            // Chcek if has eating movement for to coin in left side
            BoardPoint leftSideCell = new BoardPoint(i_BoardPoint.Column - 2, i_BoardPoint.Row - 2);
            if (IsCellExists(leftSideCell) && IsEmptyCell(leftSideCell))
            {
                Coin potentailCoinToEat = GetCoin(new BoardPoint(i_BoardPoint.Column - 1, i_BoardPoint.Row - 1));
                bool hasPotentioalCoinToEat = IsDifferentSign(potentailCoinToEat, i_Coin.Sign);
                if (hasPotentioalCoinToEat)
                {
                    i_EatingMoves.Add(new BoardMove(i_BoardPoint, leftSideCell));
                }
            }
        }

        /// <summary>
        /// Add potential eating moves for the given coin (for the player that start the game in the top of the board)
        /// </summary>
        private void addEatingMovesUpToDown(Coin i_Coin, BoardPoint i_Cell, List<BoardMove> i_EatingMoves)
        {
            // Chcek if has eating movement for coin in right side
            BoardPoint righSideCell = new BoardPoint(i_Cell.Column + 2, i_Cell.Row + 2);
            if (IsCellExists(righSideCell) && IsEmptyCell(righSideCell))
            {
                Coin potentailCoinToEat = GetCoin(new BoardPoint(i_Cell.Column + 1, i_Cell.Row + 1));
                bool hasPotentioalCoinToEat = IsDifferentSign(potentailCoinToEat, i_Coin.Sign);
                if (hasPotentioalCoinToEat)
                {
                    i_EatingMoves.Add(new BoardMove(i_Cell, righSideCell));
                }
            }

            // Chcek if has eating movement for to coin in left side
            BoardPoint leftSideCell = new BoardPoint(i_Cell.Column - 2, i_Cell.Row + 2);
            if (IsCellExists(leftSideCell) && IsEmptyCell(leftSideCell))
            {
                Coin potentailCoinToEat = GetCoin(new BoardPoint(i_Cell.Column - 1, i_Cell.Row + 1));
                bool hasPotentioalCoinToEat = IsDifferentSign(potentailCoinToEat, i_Coin.Sign);
                if (hasPotentioalCoinToEat)
                {
                    i_EatingMoves.Add(new BoardMove(i_Cell, leftSideCell));
                }
            }
        }

        /// <summary>
        /// Check if the given coin has different sign than the given sign
        /// </summary>
        public bool IsDifferentSign(Coin i_Coin, eCoinSign i_Sign)
        {
            return i_Coin != null && i_Coin.Sign != i_Sign;
        }

        /// <summary>
        /// Check if a moving coin need to be king after the move.
        /// </summary>
        public bool IsNeedToBeKing(Player i_Player, BoardMove i_Move)
        {
            bool isNeedToBeKing = false;
            if (i_Player.Sign == eCoinSign.X && i_Move.To.Row == 0)
            {
                isNeedToBeKing = true;
            }
            else if (i_Player.Sign == eCoinSign.O && i_Move.To.Row == r_LastBoardIndex)
            {
                isNeedToBeKing = true;
            }

            return isNeedToBeKing;
        }

        /// <summary>
        /// Get all the valid move in the board for the given player sign
        /// </summary>
        public List<BoardMove> GetValidMoves(eCoinSign i_Sign)
        {
            List<BoardMove> validMoves = getValidEatingMoves(i_Sign);

            // Only if we don't have eating moves - search for reqular moves
            if (validMoves.Count == 0)
            {
                for (int i = 0; i < Size; i++)
                {
                    for (int j = 0; j < Size; j++)
                    {
                        BoardPoint cell = new BoardPoint(i, j);
                        Coin currentCoin = GetCoin(cell);
                        if (currentCoin != null && currentCoin.Sign == i_Sign)
                        {
                            validMoves.AddRange(getRegularMoves(currentCoin, cell));
                        }
                    }
                }
            }

            return validMoves;
        }

        /// <summary>
        /// Remove the coin from the given board cell
        /// </summary>
        /// <param name="i_Row"></param>
        /// <param name="i_Column"></param>
        public void RemoveCoin(BoardPoint i_BoardPoint)
        {
            if (IsCellExists(i_BoardPoint))
            {
                m_GameBoard[i_BoardPoint.Row, i_BoardPoint.Column].RemoveCoin();
            }
        }

        /// <summary>
        /// Get all the valid eating moves in the board for the given player sign
        /// </summary>
        private List<BoardMove> getValidEatingMoves(eCoinSign i_Sign)
        {
            List<BoardMove> eatingMoves = getValidDoubleEatingMoves(i_Sign);

            // Search for eating moves only when we don't have double eating moves
            if (eatingMoves.Count == 0)
            {
                for (int i = 0; i < Size; i++)
                {
                    for (int j = 0; j < Size; j++)
                    {
                        BoardPoint boardPoint = new BoardPoint(i, j);
                        Coin coin = GetCoin(boardPoint);
                        if (coin != null && coin.Sign == i_Sign)
                        {
                            eatingMoves.AddRange(GetEatingMoves(boardPoint));
                        }
                    }
                }
            }

            return eatingMoves;
        }

        /// <summary>
        /// Get all the double eating moves in the board for the given player sign.
        /// </summary>
        /// <param name="i_Sign"></param>
        /// <returns></returns>
        private List<BoardMove> getValidDoubleEatingMoves(eCoinSign i_Sign)
        {
            List<BoardMove> doubleEatingMoves = new List<BoardMove>();
            Player currentPlayer = m_GameDetailes.GetPlayerBySign(i_Sign);

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    BoardPoint boardPoint = new BoardPoint(i, j);
                    Coin coin = GetCoin(boardPoint);
                    if (coin != null && coin.Sign == i_Sign && currentPlayer.ContinuEating)
                    {
                        doubleEatingMoves.AddRange(GetEatingMoves(boardPoint));
                    }
                }
            }

            return doubleEatingMoves;
        }

        /// <summary>
        /// Change the coin that locate in the given cell to king
        /// </summary>
        public void ChangeCoinToKing(BoardPoint i_BoardPoint)
        {
            Coin coin = GetCoin(i_BoardPoint);
            if (coin != null)
            {
                coin.IsKing = true;
            }
        }

        /// <summary>
        /// Get all the regular moves (not eating moves) for the given coin
        /// </summary>
        private IEnumerable<BoardMove> getRegularMoves(Coin i_Coin, BoardPoint i_BoardPoint)
        {
            List<BoardMove> validMoves = new List<BoardMove>();
            if (i_Coin.Sign == eCoinSign.X)
            {
                addRegularValidMovesDownToUp(i_Coin, i_BoardPoint, validMoves);
            }
            else
            {
                addRegularValidMovesUpToDown(i_Coin, i_BoardPoint, validMoves);
            }

            validMoves.AddRange(GetEatingMoves(i_BoardPoint));
            return validMoves;
        }

        /// <summary>
        /// Add all the regual moves for the given coin - for player that move up to down
        /// </summary>
        private void addRegularValidMovesUpToDown(Coin i_Coin, BoardPoint i_BoardPoint, List<BoardMove> i_ValidMoves)
        {
            if (i_Coin.IsKing)
            {   // King can also move back
                BoardPoint backRightCell = new BoardPoint(i_BoardPoint.Column + 1, i_BoardPoint.Row - 1);
                if (IsEmptyCell(backRightCell))
                {
                    i_ValidMoves.Add(new BoardMove(i_BoardPoint, backRightCell));
                }

                BoardPoint backLeftCell = new BoardPoint(i_BoardPoint.Column - 1, i_BoardPoint.Row - 1);
                if (IsEmptyCell(backLeftCell))
                {
                    i_ValidMoves.Add(new BoardMove(i_BoardPoint, backLeftCell));
                }
            }

            BoardPoint rightCell = new BoardPoint(i_BoardPoint.Column + 1, i_BoardPoint.Row + 1);
            if (IsEmptyCell(rightCell))
            {
                i_ValidMoves.Add(new BoardMove(i_BoardPoint, rightCell));
            }

            BoardPoint leftCell = new BoardPoint(i_BoardPoint.Column - 1, i_BoardPoint.Row + 1);
            if (IsEmptyCell(leftCell))
            {
                i_ValidMoves.Add(new BoardMove(i_BoardPoint, leftCell));
            }
        }

        /// <summary>
        /// Add all the regular moves for the given coin - for player that move down to up
        /// </summary>
        private void addRegularValidMovesDownToUp(Coin i_Coin, BoardPoint i_BoardPoint, List<BoardMove> i_ValidMoves)
        {
            if (i_Coin.IsKing)
            {   // King can also move back
                BoardPoint backCellRight = new BoardPoint(i_BoardPoint.Column + 1, i_BoardPoint.Row + 1);
                if (IsEmptyCell(backCellRight))
                {
                    i_ValidMoves.Add(new BoardMove(i_BoardPoint, backCellRight));
                }

                BoardPoint backCellLeft = new BoardPoint(i_BoardPoint.Column - 1, i_BoardPoint.Row + 1);
                if (IsEmptyCell(backCellLeft))
                {
                    i_ValidMoves.Add(new BoardMove(i_BoardPoint, backCellLeft));
                }
            }

            BoardPoint rightCell = new BoardPoint(i_BoardPoint.Column + 1, i_BoardPoint.Row - 1);
            if (IsEmptyCell(rightCell))
            {
                i_ValidMoves.Add(new BoardMove(i_BoardPoint, rightCell));
            }

            BoardPoint leftCell = new BoardPoint(i_BoardPoint.Column - 1, i_BoardPoint.Row - 1);
            if (IsEmptyCell(leftCell))
            {
                i_ValidMoves.Add(new BoardMove(i_BoardPoint, leftCell));
            }
        }

        /// <summary>
        /// Gets board size
        /// </summary>
        public int Size
        {
            get
            {
                return m_BordSize;
            }
        }

        /// <summary>
        /// Gets the column end letter
        /// </summary>
        public static char ColumnEndLetter
        {
            get
            {
                return m_ColumnEndLetter;
            }
        }

        /// <summary>
        /// Gets the row end latter
        /// </summary>
        public static char RowEndLetter
        {
            get
            {
                return m_RowEndLetter;
            }
        }

        /// <summary>
        /// Gets the column start letter
        /// </summary>
        public static char ColumnStartLetter
        {
            get
            {
                return sr_ColumnStartLetter;
            }
        }

        /// <summary>
        /// Gets the row end latter
        /// </summary>
        public static char RowStartLetter
        {
            get
            {
                return sr_RowStartLetter;
            }
        }

        /// <summary>
        /// Gets the row end latter
        /// </summary>
        public static List<int> BoardAllowedSizes
        {
            get
            {
                return new List<int>(sr_BoardAllowedSizes);
            }
        }

        /// <summary>
        /// Gets the last index of the board boarders
        /// </summary>
        public int LastBoardIndex
        {
            get
            {
                return r_LastBoardIndex;
            }
        }

        private const char k_StartLetter = 'A';
        private const string k_EmptyCellSign = " ";
        private static readonly char sr_ColumnStartLetter = k_StartLetter;
        private static readonly char sr_RowStartLetter = char.ToLower(k_StartLetter);
        private static readonly List<int> sr_BoardAllowedSizes = new List<int>() { 6, 8, 10 };
        private GameDetails m_GameDetailes;
        private static char m_ColumnEndLetter;
        private static char m_RowEndLetter;
        private readonly Player r_Player1;
        private readonly Player r_Player2;
        private readonly int r_LastBoardIndex;
        private BoardCell[,] m_GameBoard;
        private int m_BordSize;
        private GameRulesValidator m_GameRulesValidator;
    }
}