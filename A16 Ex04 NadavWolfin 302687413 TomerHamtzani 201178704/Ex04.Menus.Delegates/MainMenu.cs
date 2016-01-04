using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Delegates
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
            MenuItem currenMenuItem = m_RootMenuItems;

            while (k_DisplyMenu)
            {
                currenMenuItem = currenMenuItem.GetSelectedMenuItem();

                if (currenMenuItem is ExitMenuItem)
                {
                    break;
                }

                if (currenMenuItem.IsAction)
                {
                    Console.Clear();
                    currenMenuItem.Execute();
                    Console.WriteLine();
                    Console.WriteLine("Press enter to return to menu");
                    Console.ReadLine();
                    currenMenuItem = currenMenuItem.Parent;
                }
            }
        }

        private const bool k_DisplyMenu = true;
        private MenuItem m_RootMenuItems;
    }
}
