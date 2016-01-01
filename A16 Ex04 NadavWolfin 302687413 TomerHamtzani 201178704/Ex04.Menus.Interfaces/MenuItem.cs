using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    public class MenuItem
    {
        public MenuItem(string i_MenuItemTitle)
        {
            m_MenuItems = new List<MenuItem>();
            m_MenuItemActions = new List<IMenuItemAction>();
            m_Title = i_MenuItemTitle;

            // Add current menu item as parent for all sub menus
            foreach (MenuItem menuItem in m_MenuItems)
            {
                menuItem.Parent = this;
            }

            addBackMenuItemIfNeeded();
        }

        public void AddMenuItemAction(IMenuItemAction i_MenuItemAction)
        {
            m_MenuItemActions.Add(i_MenuItemAction);
        }

        public void AddMenuItem(MenuItem i_MenuItem)
        {
            i_MenuItem.Parent = this;
            m_MenuItems.Add(i_MenuItem);
        }

        private void addBackMenuItemIfNeeded()
        {
            // Add back to the top of the menu.
            if (m_MenuItems.Count != 0 && m_Parent != null && !(m_MenuItems[0] is BackMenuItemItem))
            {
                m_MenuItems.Insert(0, new BackMenuItemItem(m_Parent, this));
            }
        }

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

        private bool tryParseSelectedNumber(string i_SelectNumberStr, out MenuItem o_SelectedMenuItem)
        {
            int selectedNumber;
            o_SelectedMenuItem = null;

            bool isValidValue = int.TryParse(i_SelectNumberStr, out selectedNumber) &&
                selectedNumber >= 0 && selectedNumber < m_MenuItems.Count;

            if (isValidValue)
            {
                o_SelectedMenuItem = m_MenuItems[selectedNumber];
            }

            return isValidValue;
        }

        public override string ToString()
        {
            StringBuilder menuItemsStr = new StringBuilder();

            menuItemsStr.AppendLine(string.Format("{0}:", m_Title));

            int index = 0;
            foreach (MenuItem menuItem in m_MenuItems)
            {
                string menuItemForamt = string.Format("{0}. {1}", index, menuItem.m_Title);
                menuItemsStr.AppendLine(menuItemForamt);
                index++;
            }

            menuItemsStr.AppendLine();
            menuItemsStr.Append("Please choose option number: ");

            return menuItemsStr.ToString();
        }

        internal void ExecuteActions()
        {
            foreach (IMenuItemAction action in m_MenuItemActions)
            {
                action.Execute();
            }
        }

        /// <summary>
        /// When there is no sub menu item, the current menu item is an action.
        /// </summary>
        public virtual bool IsAction
        {
            get
            {
                return m_MenuItems.Count == 0;
            }
        }

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

        private List<IMenuItemAction> m_MenuItemActions;
        private List<MenuItem> m_MenuItems;
        private string m_Title;
        private MenuItem m_Parent;
    }
}
