using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Delegates.Actions
{
    public abstract class MenuItemActionBase
    {
        public MenuItemActionBase(List<MenuItem> i_MenuItems)
        {
            foreach (MenuItem menuItem in i_MenuItems)
            {
                menuItem.Selected += m_MenuItem_Selected;
                m_MenuItems.Add(menuItem);
            }     
        }

        public List<MenuItem> MenuItems
        {
            get
            {
                return m_MenuItems;
            }
        }

        private void m_MenuItem_Selected(object sender, EventArgs e)
        {
            Execute();
        }

        public abstract void Execute();

        private readonly List<MenuItem> m_MenuItems = new List<MenuItem>();
    }
}
