using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Delegates
{
    /// <summary>
    /// Delegate for function that need to be called when menu is selected
    /// </summary>
    /// <param name="sender">The <see cref="MenuItem"/> that was selected</param>
    /// <param name="e">arguments</param>
    public delegate void MenuSelectedEventHandler(object sender, EventArgs e);

    /// <summary>
    /// Represent a menu item under the main menu
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// Create a new instance of <see cref="MenuItem"/>
        /// </summary>
        /// <param name="i_MenuItemTitle">The title of the menu</param>
        public MenuItem(string i_MenuItemTitle)
        {
            m_SubMenuItems = new List<MenuItem>();
            m_Title = i_MenuItemTitle;
        }

        /// <summary>
        /// Add menu items as a sub menus of the current menu
        /// </summary>
        /// <param name="i_MenuItems">The menus to add</param>
        public void AddMenuItems(List<MenuItem> i_MenuItems)
        {
            foreach (MenuItem menuItem in i_MenuItems)
            {
                AddMenuItem(menuItem);
            }
        }

        /// <summary>
        /// Add a <see cref="MenuItem"/> as a sub menu.
        /// </summary>
        /// <param name="i_MenuItem">The menu to add</param>
        public void AddMenuItem(MenuItem i_MenuItems)
        {
            i_MenuItems.Parent = this;
            m_SubMenuItems.Add(i_MenuItems);
        }

        /// <summary>
        /// Check if need to add back menu, and if so, add it
        /// Back menu need to be added when all following cases holds:
        /// 1. There is at least one sum menu
        /// 2. There is a parent to back to
        /// 3. There is no a <see cref="BackMenuItem"/> that already exists
        /// </summary>
        private void addBackMenuItemIfNeeded()
        {
            // Add back to the top of the menu.
            if (m_SubMenuItems.Count > 0 && Parent != null && !(m_SubMenuItems[0] is BackMenuItem))
            {
                m_SubMenuItems.Insert(0, new BackMenuItem(Parent));
            }
        }

        /// <summary>
        /// Let the user to select a menu from the sub menus and return the user selected
        /// </summary>
        /// <returns>The menu item that was selected by the user</returns>
        internal virtual MenuItem GetSelectedMenuItem()
        {
            bool isValidValue = false;
            MenuItem selectedMenuItem = null;
            Console.Clear();
            Console.Write(this.ToString());

            while (!isValidValue)
            {
                string selectNumberStr = Console.ReadLine();
                isValidValue = tryParseSelectedNumber(selectNumberStr, out selectedMenuItem);
                if (!isValidValue)
                {
                    Console.Write("Invalid input, Please try again: ");
                }
            }

            return selectedMenuItem;
        }

        /// <summary>
        /// Helper function that convert the user selected number into a <see cref="MenuItem"/>
        /// </summary>
        /// <param name="i_SelectNumberStr">The number that represent the selected menu</param>
        /// <param name="o_SelectedMenuItem">The menu that related to the selected number</param>
        /// <returns>true is the given <paramref name="i_SelectNumberStr"/> is a number that represent a sub menu, otherwise false</returns>
        private bool tryParseSelectedNumber(string i_SelectNumberStr, out MenuItem o_SelectedMenuItem)
        {
            int selectedNumber;
            o_SelectedMenuItem = null;

            bool isValidValue = int.TryParse(i_SelectNumberStr, out selectedNumber) &&
                selectedNumber >= 0 && selectedNumber < m_SubMenuItems.Count;

            if (isValidValue)
            {
                o_SelectedMenuItem = m_SubMenuItems[selectedNumber];
            }

            return isValidValue;
        }

        /// <summary>
        /// Publish the selected event to all the subscribers
        /// </summary>
        public void Execute()
        {
            if (Selected != null)
            {
                Selected.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// Create a string that represent the current menu
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder menuItemsStr = new StringBuilder();

            menuItemsStr.AppendLine(string.Format("{0}:", m_Title));

            int index = 0;
            foreach (MenuItem menuItem in m_SubMenuItems)
            {
                string menuItemForamt = string.Format("{0}. {1}", index, menuItem.m_Title);
                menuItemsStr.AppendLine(menuItemForamt);
                index++;
            }

            menuItemsStr.AppendLine();
            menuItemsStr.Append("Please choose option number: ");

            return menuItemsStr.ToString();
        }

        /// <summary>
        /// Indicate if the current menu is an action menu, Action menu is an action that response to action and dosen't contain sub menus
        /// </summary>
        public virtual bool IsAction
        {
            get
            {
                return m_SubMenuItems.Count == 0;
            }
        }

        /// <summary>
        /// Represent the parent of the current <see cref="MenuItem"/>
        /// </summary>
        internal virtual MenuItem Parent
        {
            get
            {
                return m_Parent;
            }

            set
            {
                m_Parent = value;
                addBackMenuItemIfNeeded();
            }
        }

        public event MenuSelectedEventHandler Selected; // The menu item was selcted
        private List<MenuItem> m_SubMenuItems;
        private string m_Title;
        private MenuItem m_Parent;
    }
}
