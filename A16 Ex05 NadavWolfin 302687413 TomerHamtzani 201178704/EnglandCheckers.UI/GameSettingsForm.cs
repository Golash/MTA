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
            DialogResult = DialogResult.OK;
            m_GameDetails = fillGameDetails();
            Close();
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

            Player player1 = new Player(this.textBoxPlayerOne.Text, ePlayerMode.Human);

            ePlayerMode player2Mode = checkBoxPlayerTwo.Checked ? ePlayerMode.Human : ePlayerMode.Computer;
            Player player2 = new Player(this.textBoxPlayerTwo.Text, player2Mode);

            return new GameDetails(player1, player2, borderSize);
        }

        private GameDetails m_GameDetails;
    }
}
