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
            m_GameBoard = new Coin[m_BordSize, m_BordSize];

            m_GameDetailes = i_GameDetailes;
            m_Player1 = i_GameDetailes.Player1;
            m_Player2 = i_GameDetailes.Player2;
            m_GameRulesValidator = new GameRulesValidator(this);

            // Set each player a different side
            m_Player1.Sign = eCoinSign.X;
            m_Player2.Sign = eCoinSign.O;

            // Set the last column index
            m_LastBoardIndex = m_BordSize - 1;

            // Set the start and end letters
            m_ColumnEndLetter = (char)((int)sr_ColumnStartLetter + m_LastBoardIndex);
            m_RowEndLetter = (char)((int)sr_RowStartLetter + m_LastBoardIndex);

            FillBoard();
        }

        /// <summary>
        /// Fill the board with coin soldiers
        /// </summary>
        private void FillBoard()
        {
            bool isCellWithCoin = false;
            int topRowIndexForPlayer1 = (m_BordSize - 1) / 2;

            // Run all overt the rows
            for (int i = 0; i < m_BordSize; i++)
            {
                // keep on two rows space between player1 and player2
                if (i == topRowIndexForPlayer1 || i == (topRowIndexForPlayer1 + 1))
                {
                    continue;
                }

                // Fill the current row (run all over the columns)
                for (int j = 0; j < m_BordSize; j++)
                {
                    // We fill coins alternately
                    if (isCellWithCoin)
                    {
                        eCoinSign coinSign = i > topRowIndexForPlayer1 ? eCoinSign.X : eCoinSign.O;
                        m_GameBoard[i, j] = new Coin(coinSign);
                    }

                    // Toggle the coin state only if it's not the end of the line
                    if (j != m_LastBoardIndex)
                    {
                        isCellWithCoin = !isCellWithCoin;
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
                    Coin currentCoin = m_GameBoard[i, j];
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
            int separatorLength = (3 * m_BordSize) + m_LastBoardIndex - 1 + 2 + 2;
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
        internal bool TryMove(Player i_Player, BoardMove i_Move, out string i_FailureReason)
        {
            i_FailureReason = string.Empty;
            bool isValidMove = m_GameRulesValidator.IsValidMove(i_Player, i_Move, out i_FailureReason);

            // Check if the move is a valid move by the given player
            if (isValidMove)
            {
                i_Player.LastMove = i_Move;
                MakeMove(i_Move);
                isValidMove = true;
            }

            return isValidMove;
        }

        /// <summary>
        /// Applay the given <see cref="i_Move"/> move in the board
        /// </summary>
        private void MakeMove(BoardMove i_Move)
        {
            m_GameBoard[i_Move.To.Row, i_Move.To.Column] = m_GameBoard[i_Move.From.Row, i_Move.From.Column];
            m_GameBoard[i_Move.From.Row, i_Move.From.Column] = null;
        }

        /// <summary>
        /// Get the coin that locate at the given cell.
        /// If the cell not exists or the cell is empty - null will return
        /// </summary>
        public Coin GetCoin(BoardCell cell)
        {
            Coin coin = null;
            if (IsCellExists(cell))
            {
                coin = m_GameBoard[cell.Row, cell.Column];
            }

            return coin;
        }

        /// <summary>
        /// Check if the given cell is empty.
        /// If the cell not exists - false will return
        /// </summary>
        public bool IsEmptyCell(BoardCell cell)
        {
            return IsCellExists(cell) && (m_GameBoard[cell.Row, cell.Column] == null);
        }

        /// <summary>
        /// Check if the given cell is exists in the board.
        /// </summary>
        public bool IsCellExists(BoardCell cell)
        {
            return cell.Row >= 0 && cell.Row < m_BordSize && cell.Column >= 0 && cell.Column < m_BordSize;
        }

        /// <summary>
        /// Gets the optionsl eating moves for the given cell
        /// </summary>
        /// <returns></returns>
        public List<BoardMove> GetEatingMoves(BoardCell cell)
        {
            List<BoardMove> eatingMoves = new List<BoardMove>();
            Coin coin = GetCoin(cell);
            if (coin != null)
            {
                if (coin.IsKing)
                {
                    AddEatingMovesUpToDown(coin, cell, eatingMoves);
                    AddEatingMovesDownToUp(coin, cell, eatingMoves);
                }
                else if (coin.Sign == eCoinSign.X)
                {
                    AddEatingMovesDownToUp(coin, cell, eatingMoves);
                }
                else if (coin.Sign == eCoinSign.O)
                {
                    AddEatingMovesUpToDown(coin, cell, eatingMoves);
                }
            }

            return eatingMoves;
        }

        /// <summary>
        /// Add potential eating moves for the given coin (for the player that start the game in the bottom of the board)
        /// </summary>
        private void AddEatingMovesDownToUp(Coin i_Coin, BoardCell i_Cell, List<BoardMove> i_EatingMoves)
        {
            // Chcek if has eating movement for to coin in right side
            BoardCell rightSideCell = new BoardCell(i_Cell.Column + 2, i_Cell.Row - 2);
            if (IsCellExists(rightSideCell) && IsEmptyCell(rightSideCell))
            {
                Coin potentailCoinToEat = GetCoin(new BoardCell(i_Cell.Column + 1, i_Cell.Row - 1));
                bool hasPotentioalCoinToEat = IsDifferentSign(potentailCoinToEat, i_Coin.Sign);
                if (hasPotentioalCoinToEat)
                {
                    i_EatingMoves.Add(new BoardMove(i_Cell, rightSideCell));
                }
            }

            // Chcek if has eating movement for to coin in left side
            BoardCell leftSideCell = new BoardCell(i_Cell.Column - 2, i_Cell.Row - 2);
            if (IsCellExists(leftSideCell) && IsEmptyCell(leftSideCell))
            {
                Coin potentailCoinToEat = GetCoin(new BoardCell(i_Cell.Column - 1, i_Cell.Row - 1));
                bool hasPotentioalCoinToEat = IsDifferentSign(potentailCoinToEat, i_Coin.Sign);
                if (hasPotentioalCoinToEat)
                {
                    i_EatingMoves.Add(new BoardMove(i_Cell, leftSideCell));
                }
            }
        }

        /// <summary>
        /// Add potential eating moves for the given coin (for the player that start the game in the top of the board)
        /// </summary>
        private void AddEatingMovesUpToDown(Coin i_Coin, BoardCell i_Cell, List<BoardMove> i_EatingMoves)
        {
            // Chcek if has eating movement for coin in right side
            BoardCell righSideCell = new BoardCell(i_Cell.Column + 2, i_Cell.Row + 2);
            if (IsCellExists(righSideCell) && IsEmptyCell(righSideCell))
            {
                Coin potentailCoinToEat = GetCoin(new BoardCell(i_Cell.Column + 1, i_Cell.Row + 1));
                bool hasPotentioalCoinToEat = IsDifferentSign(potentailCoinToEat, i_Coin.Sign);
                if (hasPotentioalCoinToEat)
                {
                    i_EatingMoves.Add(new BoardMove(i_Cell, righSideCell));
                }
            }

            // Chcek if has eating movement for to coin in left side
            BoardCell leftSideCell = new BoardCell(i_Cell.Column - 2, i_Cell.Row + 2);
            if (IsCellExists(leftSideCell) && IsEmptyCell(leftSideCell))
            {
                Coin potentailCoinToEat = GetCoin(new BoardCell(i_Cell.Column - 1, i_Cell.Row + 1));
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
        /// <param name="coin"></param>
        public bool IsDifferentSign(Coin coin, eCoinSign sign)
        {
            return coin != null && coin.Sign != sign;
        }

        /// <summary>
        /// Check if a moving coin need to be king after the move.
        /// </summary>
        public bool IsNeedToBeKing(Player player, BoardMove move)
        {
            bool isNeedToBeKing = false;
            if (player.Sign == eCoinSign.X && move.To.Row == 0)
            {
                isNeedToBeKing = true;
            }
            else if (player.Sign == eCoinSign.O && move.To.Row == m_LastBoardIndex)
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
                        BoardCell cell = new BoardCell(i, j);
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
        public void RemoveCoin(BoardCell i_Cell)
        {
            if (GetCoin(i_Cell) != null)
            {
                m_GameBoard[i_Cell.Row, i_Cell.Column] = null;
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
                        BoardCell currentCell = new BoardCell(i, j);
                        Coin coin = GetCoin(currentCell);
                        if (coin != null && coin.Sign == i_Sign)
                        {
                            eatingMoves.AddRange(GetEatingMoves(currentCell));
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
                    BoardCell cell = new BoardCell(i, j);
                    Coin coin = GetCoin(cell);
                    if (coin != null && coin.Sign == i_Sign && currentPlayer.ContinuEating)
                    {
                        doubleEatingMoves.AddRange(GetEatingMoves(cell));
                    }
                }
            }

            return doubleEatingMoves;
        }

        /// <summary>
        /// Change the coin that locate in the given cell to king
        /// </summary>
        public void ChangeCoinToKing(BoardCell i_Cell)
        {
            Coin coin = GetCoin(i_Cell);
            if (coin != null)
            {
                coin.IsKing = true;
            }
        }

        /// <summary>
        /// Get all the regular moves (not eating moves) for the given coin
        /// </summary>
        private IEnumerable<BoardMove> getRegularMoves(Coin i_Coin, BoardCell cell)
        {
            List<BoardMove> validMoves = new List<BoardMove>();
            if (i_Coin.Sign == eCoinSign.X)
            {
                AddRegularValidMovesDownToUp(i_Coin, cell, validMoves);
            }
            else
            {
                addRegularValidMovesUpToDown(i_Coin, cell, validMoves);
            }

            validMoves.AddRange(GetEatingMoves(cell));
            return validMoves;
        }

        /// <summary>
        /// Add all the regual moves for the given coin - for player that move up to down
        /// </summary>
        private void addRegularValidMovesUpToDown(Coin i_Coin, BoardCell i_Cell, List<BoardMove> i_ValidMoves)
        {
            if (i_Coin.IsKing)
            {   // King can also move back
                BoardCell backRightCell = new BoardCell(i_Cell.Column + 1, i_Cell.Row - 1);
                if (IsEmptyCell(backRightCell))
                {
                    i_ValidMoves.Add(new BoardMove(i_Cell, backRightCell));
                }
                
                BoardCell backLeftCell = new BoardCell(i_Cell.Column - 1, i_Cell.Row - 1);
                if (IsEmptyCell(backLeftCell))
                {
                    i_ValidMoves.Add(new BoardMove(i_Cell, backLeftCell));
                }
            }

            BoardCell rightCell = new BoardCell(i_Cell.Column + 1, i_Cell.Row + 1);
            if (IsEmptyCell(rightCell))
            {
                i_ValidMoves.Add(new BoardMove(i_Cell, rightCell));
            }

            BoardCell leftCell = new BoardCell(i_Cell.Column - 1, i_Cell.Row + 1);
            if (IsEmptyCell(leftCell))
            {
                i_ValidMoves.Add(new BoardMove(i_Cell, leftCell));
            }
        }

        /// <summary>
        /// Add all the regular moves for the given coin - for player that move down to up
        /// </summary>
        private void AddRegularValidMovesDownToUp(Coin i_Coin, BoardCell i_Cell, List<BoardMove> i_ValidMoves)
        {
            if (i_Coin.IsKing)
            {   // King can also move back
                BoardCell backCellRight = new BoardCell(i_Cell.Column + 1, i_Cell.Row + 1);
                if (IsEmptyCell(backCellRight))
                {
                    i_ValidMoves.Add(new BoardMove(i_Cell, backCellRight));
                }

                BoardCell backCellLeft = new BoardCell(i_Cell.Column - 1, i_Cell.Row + 1);
                if (IsEmptyCell(backCellLeft))
                {
                    i_ValidMoves.Add(new BoardMove(i_Cell, backCellLeft));
                }
            }

            BoardCell rightCell = new BoardCell(i_Cell.Column + 1, i_Cell.Row - 1);
            if (IsEmptyCell(rightCell))
            {
                i_ValidMoves.Add(new BoardMove(i_Cell, rightCell));
            }
            
            BoardCell leftCell = new BoardCell(i_Cell.Column - 1, i_Cell.Row - 1);
            if (IsEmptyCell(leftCell))
            {
                i_ValidMoves.Add(new BoardMove(i_Cell, leftCell));
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
                return m_LastBoardIndex;
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
        private readonly Player m_Player1;
        private readonly Player m_Player2;
        private int m_LastBoardIndex;
        private Coin[,] m_GameBoard;
        private int m_BordSize;
        private GameRulesValidator m_GameRulesValidator;
    }
}