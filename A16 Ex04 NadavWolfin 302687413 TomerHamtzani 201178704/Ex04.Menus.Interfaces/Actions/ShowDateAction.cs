using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces.Actions
{
    public class ShowDateAction : IMenuItemSelectedObserver
    {
        public ShowDateAction()
        {
            MenuItem menuItem = new MenuItem("Show Date");
            menuItem.AttachObserver(this);
            m_MenuItems.Add(menuItem);
        }

        public void ReportSelect(MenuItem i_MenuItem)
        {
            Execute();
        }

        private void Execute()
        {
            Console.WriteLine("The current data is: {0}", DateTime.Now.ToShortDateString());
        }

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
