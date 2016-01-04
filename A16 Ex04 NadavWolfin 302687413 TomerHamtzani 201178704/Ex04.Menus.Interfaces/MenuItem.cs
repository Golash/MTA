﻿using System;
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
            m_SubMenuItems = new List<MenuItem>();
            m_MenuItemSelectedObservers = new List<IMenuItemSelectedObserver>();
            m_Title = i_MenuItemTitle;
        }

        public void AttachObserver(IMenuItemSelectedObserver i_MenuItemSelectedObserver)
        {
            m_MenuItemSelectedObservers.Add(i_MenuItemSelectedObserver);
        }

        public void AddMenuItems(List<MenuItem> i_MenuItems)
        {
            foreach (MenuItem menuItem in i_MenuItems)
            {
                AddMenuItem(menuItem);
            }
        }

        public void AddMenuItem(MenuItem i_MenuItems)
        {
            i_MenuItems.Parent = this;
            m_SubMenuItems.Add(i_MenuItems);
        }

        private void addBackMenuItemIfNeeded()
        {
            // Add back to the top of the menu.
            if (m_SubMenuItems.Count > 0 && Parent != null && !(m_SubMenuItems[0] is BackMenuItem))
            {
                m_SubMenuItems.Insert(0, new BackMenuItem(Parent, this));
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
                selectedNumber >= 0 && selectedNumber < m_SubMenuItems.Count;

            if (isValidValue)
            {
                o_SelectedMenuItem = m_SubMenuItems[selectedNumber];
            }

            return isValidValue;
        }

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

        public void Select()
        {
            foreach (IMenuItemSelectedObserver action in m_MenuItemSelectedObservers)
            {
                action.ReportSelect(this);
            }
        }

        /// <summary>
        /// When there is no sub menu item, the current menu item is an action.
        /// </summary>
        public virtual bool IsAction
        {
            get
            {
                return m_SubMenuItems.Count == 0;
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

        private List<IMenuItemSelectedObserver> m_MenuItemSelectedObservers;
        private List<MenuItem> m_SubMenuItems;
        private string m_Title;
        private MenuItem m_Parent;
    }
}