namespace EnglandCheckers.Components
{
    /// <summary>
    /// The Player class represent a player in the game.
    /// Player can be Human or Computer
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Create a new instance of the Player class with the given name and mode
        /// </summary>
        public Player(string i_Name, ePlayerMode i_PlayrMode)
        {
            rm_Name = i_Name;
            rm_Mode = i_PlayrMode;
        }

        /// <summary>
        /// Gets the name of the player
        /// </summary>
        public string Name
        {
            get
            {
                return rm_Name;
            }
        }

        /// <summary>
        /// Gets the mode of the player
        /// </summary>
        public ePlayerMode Mode
        {
            get
            {
                return rm_Mode;
            }
        }

        /// <summary>
        /// Get the sign of the player
        /// </summary>
        public eCoinSign Sign
        {
            get
            {
                return m_CoinSing;
            }
            
            set
            {
                m_CoinSing = value;
            }
        }

        /// <summary>
        /// Gets the last move of the player
        /// </summary>
        public BoardMove LastMove
        {
            get
            {
                return m_LastMove;
            }
            
            set
            {
                m_LastMove = value;
            }
        }

        /// <summary>
        /// Indicate if the player last move was an eating move
        /// </summary>
        public bool EatInLastMove
        {
            get
            {
                return m_EtaInLastMove;
            }
            
            set
            {
                m_EtaInLastMove = value;
            }
        }

        /// <summary>
        /// Represent the total points of the player in the overall games
        /// </summary>
        public int Points
        {
            get
            {
                return m_Points;
            }
            
            set
            {
                m_Points = value;
            }
        }

        /// <summary>
        /// Indicate if the player has continuation eating moves
        /// </summary>
        public bool ContinuEating
        {
            get
            {
                return m_ContinuEating;
            }
            
            set
            {
                m_ContinuEating = value;
            }
        }

        private readonly string rm_Name;
        private readonly ePlayerMode rm_Mode;
        private eCoinSign m_CoinSing;
        private int m_Points;
        private BoardMove m_LastMove;
        private bool m_EtaInLastMove;
        private bool m_ContinuEating;
    }
}
