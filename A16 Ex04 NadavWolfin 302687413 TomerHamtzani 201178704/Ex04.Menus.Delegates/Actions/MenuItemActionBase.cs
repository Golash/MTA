using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Delegates.Actions
{
    /// <summary>
    /// Base class for all the actions.
    /// This class handle the event registration on all the given menus
    /// </summary>
    public abstract class MenuItemActionBase
    {
        /// <summary>
        /// Register the given <paramref name="i_MenuItems"/> on the current action
        /// </summary>
        /// <param name="i_MenuItems">The menu items for register on</param>
        public MenuItemActionBase(List<MenuItem> i_MenuItems)
        {
            foreach (MenuItem menuItem in i_MenuItems)
            {
                menuItem.Selected += m_MenuItem_Selected;
                m_MenuItems.Add(menuItem);
            }     
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

        /// <summary>
        /// Event Handler that will be raised when a menu is selected
        /// </summary>
        /// <param name="sender">The menu that was selected</param>
        /// <param name="e">arguments</param>
        private void m_MenuItem_Selected(object sender, EventArgs e)
        {
            Execute();
        }

        public abstract void Execute();

        private readonly List<MenuItem> m_MenuItems = new List<MenuItem>();
    }
}
