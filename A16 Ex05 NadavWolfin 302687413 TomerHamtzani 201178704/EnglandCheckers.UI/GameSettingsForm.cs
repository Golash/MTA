using EnglandCheckers.BusinessLogic;
using EnglandCheckers.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnglandCheckers.UI
{
    public partial class GameSettingsForm : Form
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
            this.DialogResult = DialogResult.OK;
            m_GameDetails = fillGameDetails();
            this.Close();
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
