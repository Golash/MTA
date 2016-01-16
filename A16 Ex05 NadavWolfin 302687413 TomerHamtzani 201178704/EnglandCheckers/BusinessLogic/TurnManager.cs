using EnglandCheckers.Components;

namespace EnglandCheckers.BusinessLogic
{
    /// <summary>
    /// The TurnManager class declare which player need to play at each moment
    /// </summary>
    public class TurnManager
    {
        /// <summary>
        /// Create a new instance of the TurnManager
        /// </summary>
        public TurnManager(Player i_Player1, Player i_Player2, Board i_Board)
        {
            m_GameRulesValidator = new GameRulesValidator(i_Board);

            m_Player1 = i_Player1;
            m_Player2 = i_Player2;
            m_Board = i_Board;
            
            m_currentPlayer = m_Player1;
        }

        public void SwitchPlayerIfNeeded()
        {
            if (m_currentPlayer == null)
            {
                // If the current player is null - it's the first turn so set Player1 to play first
                m_currentPlayer = m_Player1;
            }
            else
            {
                m_currentPlayer.ContinuEating = m_currentPlayer.EatInLastMove && m_GameRulesValidator.IsNeedToContinueEating(m_currentPlayer);

                // if the current player, don't need to continue eating - swap the players
                if (!m_currentPlayer.ContinuEating)
                {
                    // swap the players
                    m_currentPlayer = m_currentPlayer == m_Player1 ? m_Player2 : m_Player1;
                }
            }

        }

        /// <summary>
        /// Gets the current player that need to play
        /// </summary>
        public Player CurrentPlayer
        {
            get
            {
                // set the current player
                return m_currentPlayer;
            }
        }

        private Player m_Player1;
        private Player m_Player2;
        private Board m_Board;
        private Player m_currentPlayer;
        private GameRulesValidator m_GameRulesValidator;
    }
}
