using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglandCheckers.UI
{
    public class Program
    {
        public static void Main()
        {
            GameSettingsForm gameSettingsForm = new GameSettingsForm();
            if (gameSettingsForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CheckersBoardForm checkersBoard = new CheckersBoardForm(gameSettingsForm.GameDetails);
                checkersBoard.ShowDialog();
            }
        }
    }
}
