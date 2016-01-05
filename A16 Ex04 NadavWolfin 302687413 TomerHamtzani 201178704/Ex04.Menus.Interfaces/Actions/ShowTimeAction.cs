using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces.Actions
{
    public class ShowTimeAction : IMenuItemSelectedObserver
    {
        /// <summary>
        /// Create a new instance of <see cref="ShowTimeAction"/>
        /// </summary>
        public ShowTimeAction()
        {
            MenuItem menuItem = new MenuItem("Show Time");
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
        /// Execute the <see cref="ShowTimeAction"/> action.
        /// The actions will display the current time
        /// </summary>
        private void Execute()
        {
            Console.WriteLine("The current time is: {0}", DateTime.Now.ToShortTimeString());
        }

        /// <summary>
        /// List of all the menu items that are register on the action
        /// </summary>
        public List<MenuItem> MenuItems 
        {
            get
            {
                return m_MenuItems;
            }
        }

        private readonly List<MenuItem> m_MenuItems = new List<MenuItem>();
    }
}
