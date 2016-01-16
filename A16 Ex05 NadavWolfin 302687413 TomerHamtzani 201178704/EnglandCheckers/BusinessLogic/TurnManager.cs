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

            m_CurrentPlayer = m_Player1;
        }

        public void SwitchPlayerIfNeeded()
        {
            m_CurrentPlayer.ContinuEating = m_CurrentPlayer.EatInLastMove && m_GameRulesValidator.IsNeedToContinueEating(m_CurrentPlayer);

            // if the current player, don't need to continue eating - swap the players
            if (!m_CurrentPlayer.ContinuEating)
            {
                // swap the players
                m_CurrentPlayer = AdversaryPlayer;
            }
        }

        internal Player AdversaryPlayer
        {
            get
            {
                return m_CurrentPlayer == m_Player1 ? m_Player2 : m_Player1;
            }
        }

        /// <summary>
        /// Gets the current player that need to play
        /// </summary>
        internal Player CurrentPlayer
        {
            get
            {
                // set the current player
                return m_CurrentPlayer;
            }
        }

        private readonly Player m_Player1;
        private readonly Player m_Player2;
        private Player m_CurrentPlayer;
        private readonly GameRulesValidator m_GameRulesValidator;
    }
}
