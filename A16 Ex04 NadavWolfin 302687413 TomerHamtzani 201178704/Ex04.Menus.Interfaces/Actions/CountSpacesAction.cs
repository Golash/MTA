using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces.Actions
{
    public class CountSpacesAction : IMenuItemSelectedObserver
    {
        /// <summary>
        /// Create a new instance of <see cref="CountSpacesAction"/>
        /// </summary>
        public CountSpacesAction()
        {
            MenuItem menuItem = new MenuItem("Count Spaces");
            menuItem.AttachObserver(this);
            m_MenuItems.Add(menuItem);
        }

        /// <summary>
        /// Report on a menu that has selected
        /// </summary>
        /// <param name="i_MenuItem">The selected menu to report on</param>
        public void ReportSelect(MenuItem i_MenuItem)
        {
            Execute();
        }

        /// <summary>
        /// Execute the <see cref="CountSpacesAction"/> action.
        /// The actions will:
        /// 1. Read a sentance from the user.
        /// 2. Display the user the number of spaces in the sentance
        /// </summary>
        private void Execute()
        {
            Console.WriteLine("Please write a sentance:");
            string sentance = Console.ReadLine();

            int spaceCount = 0;
            foreach (char currentChar in sentance)
            {
                if (currentChar == k_Space)
                {
                    spaceCount++;
                }
            }

            Console.WriteLine("The number of spaces in the given sentance is: {0}", spaceCount);
        }

        /// <summary>
        /// List of all the menu items that are register for the action
        /// </summary>
        public List<MenuItem> MenuItems
        {
            get
            {
                return m_MenuItems;
            }
        }

        private readonly List<MenuItem> m_MenuItems = new List<MenuItem>();

        // Space sign
        private const char k_Space = ' ';
    }
}
