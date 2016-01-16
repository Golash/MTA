using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglandCheckers.UI
{
    class Program
    {
        static void Main()
        {

            GameSettings gameSettingsForm = new GameSettings();
            if (gameSettingsForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                checkersBoardForm checkersBoard = new checkersBoardForm(gameSettingsForm.GameDetails);
                checkersBoard.ShowDialog();
            }
        }
    }
}
