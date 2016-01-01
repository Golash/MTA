using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    public class MainMenu
    {
        public MainMenu(List<MenuItem> i_MenuItems = null, List<IMenuItemAction> i_MenuItemActions = null)
        {
            i_MenuItems.Insert(0, new ExitMenuItem());
            m_RootMenuItems = new MenuItem("Main Menu", i_MenuItems, i_MenuItemActions);
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
