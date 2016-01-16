namespace EnglandCheckers.Components
{
    /// <summary>
    /// Represent the user selected options for the game.
    /// The selected options are:
    /// 1. First player (<seealso cref="Player"/>)
    /// 2. Board size
    /// 3. Second player (<seealso cref="Player"/>)
    /// </summary>
    public class GameDetails
    {
        // The max size that allowed for player name
        private const int k_MaxPlayerNameLength = 20;

        /// <summary>
        /// Create a new instance of GameDetails
        /// </summary>
        public GameDetails(Player i_Player1, Player i_Player2, int i_BoardSize)
        {
            r_Player1 = i_Player1;
            r_Player2 = i_Player2;
            Player1.Sign = eCoinSign.X;
            Player2.Sign = eCoinSign.O;
            r_BoardSize = i_BoardSize;
        }

        /// <summary>
        /// Check if the given name <paramref name="i_Name"/> is a valid name.
        /// Valid name must holds:
        /// 1. It don't contains any whitespace.
        /// 2. It length is up to 20 characters.
        /// </summary>
        public static bool IsValidName(string i_Name)
        {
            bool isContainsWhiteSpace = false;
            foreach (char tav in i_Name)
            {
                if (char.IsWhiteSpace(tav))
                {
                    isContainsWhiteSpace = true;
                    break;
                }
            }

            return !string.IsNullOrEmpty(i_Name) && i_Name.Length <= k_MaxPlayerNameLength && !isContainsWhiteSpace;
        }

        /// <summary>
        /// Get and Set property for the board size
        /// </summary>
        public int BoardSize
        {
            get
            {
                return r_BoardSize;
            }
        }
        
        /// <summary>
        /// Get and Set property for Player1
        /// </summary>
        public Player Player1
        {
            get
            {
                return r_Player1;
            }
        }

        /// <summary>
        /// Get and Set property for Player2
        /// </summary>
        public Player Player2
        {
            get
            {
                return r_Player2;
            }
        }

        /// <summary>
        /// Get player instance by it's sign
        /// </summary>
        /// <param name="i_PlayerSign"></param>
        /// <returns></returns>
        internal Player GetPlayerBySign(eCoinSign i_PlayerSign)
        {
            return r_Player1.Sign == i_PlayerSign ? r_Player1 : r_Player2;
        }

        private readonly int r_BoardSize;
        private readonly Player r_Player1;
        private readonly Player r_Player2;
    }
}
