using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    public class MenuItem
    {
        public MenuItem(string i_MenuItemTitle, MenuItem i_Parent = null)
        {
            m_MenuItemActions = new List<IMenuItemAction>();
            m_MenuItems = new List<MenuItem>();
            m_Title = i_MenuItemTitle;
            m_Parent = i_Parent;
        }
        
        public void AddMenuItemAction(IMenuItemAction i_MenuItemAction)
        {
            m_MenuItemActions.Add(i_MenuItemAction);
        }

        public void AddMenuItem(MenuItem i_MenuItem)
        {
            // Add back to the top of the menu.
            if (m_MenuItems.Count == 0)
            {
                m_MenuItems.Add(new BackMenuItemItem(m_Parent, this));
            }

            m_MenuItems.Add(i_MenuItem);
        }

        internal virtual MenuItem GetSelectedMenuItem()
        {
            bool isValidValue = false;
            MenuItem selectedMenuItem = null;

            while (!isValidValue)
            {
                Console.WriteLine(this.ToString());
                string selectNumberStr = Console.ReadLine();
                isValidValue = tryParseSelectedNumber(selectNumberStr, out selectedMenuItem);
                if (!isValidValue)
                {
                    Console.WriteLine("Invalid input, Please try again");
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

            string parentMenuItemFormat = string.Format("{0}:", m_Parent);
            menuItemsStr.AppendLine(parentMenuItemFormat);
            menuItemsStr.AppendLine();

            int index = 0;
            foreach (MenuItem menuItem in m_MenuItems)
            {
                string menuItemForamt = string.Format("\t{0}. {1}", index, m_Title);
                menuItemsStr.AppendLine(menuItemForamt);
            }

            menuItemsStr.AppendLine();
            menuItemsStr.AppendLine("Please choose option number:   ");

            return menuItemsStr.ToString();
        }

        public void ExecuteActions()
        {
            foreach (var action in m_MenuItemActions)
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

        /// <summary>
        /// When there is no sub menu item, the current menu item is an action.
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
            }
        }

        private List<IMenuItemAction> m_MenuItemActions;
        private List<MenuItem> m_MenuItems;
        private string m_Title;
        private MenuItem m_Parent;
    }
}
