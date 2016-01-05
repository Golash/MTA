using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces.Actions
{
    public class CountWordsAction : IMenuItemSelectedObserver
    {
        /// <summary>
        /// Create a new instance of <see cref="CountWordsAction"/>
        /// </summary>
        public CountWordsAction()
        {
            MenuItem menuItem = new MenuItem("Count Words");
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
        /// Execute the <see cref="CountWordsAction"/> action.
        /// The actions will:
        /// 1. Read a sentance from the user.
        /// 2. Display the user the number of words in the sentance
        /// </summary>
        private void Execute()
        {
            Console.WriteLine("Please write a sentance:");
            string sentance = Console.ReadLine();
            int wordsCount = sentance.Split(r_SpaceSeparators, StringSplitOptions.RemoveEmptyEntries).Length;

            Console.WriteLine("The number of words in the given sentance is: {0}", wordsCount);
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

        // We consider the following characters as a word separator in a sentence
        private readonly string[] r_SpaceSeparators = new string[] { " ", "\t" };
    }
}
