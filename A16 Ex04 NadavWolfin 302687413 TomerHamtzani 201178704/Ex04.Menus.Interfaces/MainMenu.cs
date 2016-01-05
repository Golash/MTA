using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    /// <summary>
    /// Represent the main menu of the program
    /// </summary>
    public class MainMenu
    {
        /// <summary>
        /// Create a new instance of  <see cref="MainMenu"/>
        /// </summary>
        public MainMenu()
        {
            m_RootMenuItems = new MenuItem("Main Menu");
            m_RootMenuItems.AddMenuItem(new ExitMenuItem());
        }

        /// <summary>
        /// Add a <see cref="MenuItem"/> as a sub menu.
        /// </summary>
        /// <param name="i_MenuItem">The menu to add</param>
        public void AddMenuItem(MenuItem i_MenuItem)
        {
            m_RootMenuItems.AddMenuItem(i_MenuItem);
        }

        /// <summary>
        /// Display the main menu and handle the navigation between the menus
        /// </summary>
        public void Show()
        {
            // Start from the main menu
            MenuItem currenMenuItem = m_RootMenuItems;

            // Display the menu all the time (Exit will break the loop)
            while (k_DisplyMenu)
            {
                // Let the user to select a menu / action form the sub menus 
                currenMenuItem = currenMenuItem.GetSelectedMenuItem();

                // In case of exit menu selected - close the main menu
                if (currenMenuItem is ExitMenuItem)
                {
                    break;
                }

                // If action was selected - make the action
                if (currenMenuItem.IsAction)
                {
                    Console.Clear();
                    currenMenuItem.Select();
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
