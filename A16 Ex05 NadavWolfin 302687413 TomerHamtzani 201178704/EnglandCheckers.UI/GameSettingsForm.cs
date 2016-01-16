using System;
using System.Windows.Forms;
using EnglandCheckers.Components;

namespace EnglandCheckers.UI
{
    internal partial class GameSettingsForm : Form
    {
        public GameSettingsForm()
        {
            InitializeComponent();
        }

        private void checkBoxPlayerTwo_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPlayerTwo.Enabled = checkBoxPlayerTwo.Checked;
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            m_GameDetails = fillGameDetails();
            if (m_GameDetails != null)
            {
                DialogResult = DialogResult.OK;
            }
        }

        private bool validateNames(TextBox i_UserNameTextBox)
        {
            bool isValidName = GameDetails.IsValidName(i_UserNameTextBox.Text);
            if (!isValidName)
            {
                nameErrorProvider.SetError(i_UserNameTextBox, "Player name must be between 1 to 20 characters without spaces");
            }

            return isValidName;
        }

        public GameDetails GameDetails
        {
            get
            {
                return m_GameDetails;
            }
        }

        private GameDetails fillGameDetails()
        {
            GameDetails gameDetails = null;

            int borderSize;
            if (radioButtonSixOnSix.Checked)
            {
                borderSize = 6;
            }
            else if (radioButtonEightOnEight.Checked)
            {
                borderSize = 8;
            }
            else
            {
                borderSize = 10;
            }

            bool isValidName = validateNames(textBoxPlayerOne) && validateNames(textBoxPlayerTwo);

            if (isValidName)
            {
                Player player1 = new Player(textBoxPlayerOne.Text, ePlayerMode.Human);

                ePlayerMode player2Mode = checkBoxPlayerTwo.Checked ? ePlayerMode.Human : ePlayerMode.Computer;
                Player player2 = new Player(textBoxPlayerTwo.Text, player2Mode);

                gameDetails = new GameDetails(player1, player2, borderSize);
            }

            return gameDetails;
        }

        private GameDetails m_GameDetails;
    }
}
