using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        internal const int k_MaxPlayerNameLength = 20;

        /// <summary>
        /// Create a new instance of GameDetails
        /// </summary>
        public GameDetails(Player i_Player1, Player i_Player2, int i_BoardSize)
        {
            Player1 = i_Player1;
            Player2 = i_Player2;
            BoardSize = i_BoardSize;
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

            return i_Name.Length <= k_MaxPlayerNameLength && !isContainsWhiteSpace;
        }

        /// <summary>
        /// Get and Set property for the board size
        /// </summary>
        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }

            set
            {
                m_BoardSize = value;
            }
        }
        
        /// <summary>
        /// Get and Set property for Player1
        /// </summary>
        public Player Player1
        {
            get
            {
                return m_Player1;
            }

            set
            {
                m_Player1 = value;
            }
        }

        /// <summary>
        /// Get and Set property for Player2
        /// </summary>
        public Player Player2
        {
            get
            {
                return m_Player2;
            }

            set
            {
                m_Player2 = value;
            }
        }

        /// <summary>
        /// Get player instance by it's sign
        /// </summary>
        /// <param name="i_PlayerSign"></param>
        /// <returns></returns>
        internal Player GetPlayerBySign(eCoinSign i_PlayerSign)
        {
            return m_Player1.Sign == i_PlayerSign ? m_Player1 : m_Player2;
        }

        private int m_BoardSize;
        private Player m_Player1;
        private Player m_Player2;
    }
}
