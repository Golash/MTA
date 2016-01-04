using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces.Actions
{
    public class ShowTimeAction : IMenuItemSelectedObserver
    {
        public ShowTimeAction()
        {
            MenuItem menuItem = new MenuItem("Show Time");
            menuItem.AttachObserver(this);
            m_MenuItems.Add(menuItem);
        }

        public void ReportSelect(MenuItem i_MenuItem)
        {
            Execute();
        }

        private void Execute()
        {
            Console.WriteLine("The current time is: {0}", DateTime.Now.ToShortTimeString());
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
