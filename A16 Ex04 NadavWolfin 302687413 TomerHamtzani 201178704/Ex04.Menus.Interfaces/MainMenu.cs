using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    public class MainMenu
    {
        public MainMenu()
        {
            m_RootMenuItems = new MenuItem("Main Menu");
            m_RootMenuItems.AddMenuItem(new ExitMenuItem());
        }

        public void AddMenuItems(MenuItem i_MenuItem)
        {
            m_RootMenuItems.AddMenuItem(i_MenuItem);
        }

        public void Show()
        {
            MenuItem currenMenuItem = m_RootMenuItems.GetSelectedMenuItem();

            while (k_DisplyMenu)
            {
                if (currenMenuItem is ExitMenuItem)
                {
                    break;
                }

                if (currenMenuItem.IsAction)
                {
                    currenMenuItem.ExecuteActions();
                }
                else
                {
                    currenMenuItem = currenMenuItem.GetSelectedMenuItem();
                }
            }
        }

        private const bool k_DisplyMenu = true;
        private MenuItem m_RootMenuItems;
    }
}
