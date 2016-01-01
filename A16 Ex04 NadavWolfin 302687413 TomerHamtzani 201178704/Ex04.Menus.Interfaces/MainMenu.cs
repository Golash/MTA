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
            m_RootMenuItems = new MenuItem("Main Menu", null);
            addExitOperation();
        }

        private void addExitOperation()
        {
            m_RootMenuItems.AddMenuItem(new ExitMenuItem());
        }

        public void Show()
        {
            MenuItem currenMenuItem = m_RootMenuItems;
            bool isUserRequestToExit = false;

            while (!isUserRequestToExit)
            {
                Console.Clear();
                MenuItem menuItem = m_RootMenuItems.GetSelectedMenuItem();

                if (menuItem.IsAction)
                {
                    menuItem.ExecuteActions();
                }
                else
                {
                    menuItem = menuItem.GetSelectedMenuItem();
                }

                isUserRequestToExit = menuItem is ExitMenuItem;
                currenMenuItem = menuItem;
            }
        }

        public void AddMenuItem(MenuItem i_MenuItem)
        {
            i_MenuItem.Parent = m_RootMenuItems;
            m_RootMenuItems.AddMenuItem(i_MenuItem);
        }

        private MenuItem m_RootMenuItems;
    }
}
