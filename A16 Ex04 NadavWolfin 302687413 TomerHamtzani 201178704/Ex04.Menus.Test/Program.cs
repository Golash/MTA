using Ex04.Menus.Interfaces;
using Ex04.Menus.Interfaces.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMenu interfacesMainMenu = new MainMenu();

            MenuItem showDateTimeMenuItem = new MenuItem("Show Date/Time");

            MenuItem showDateMenuItem = new MenuItem("Show Date", showDateTimeMenuItem);
            showDateMenuItem.AddMenuItemAction(new ShowDateAction());

            MenuItem showTimeMenuItem = new MenuItem("Show Time", showDateTimeMenuItem);
            showTimeMenuItem.AddMenuItemAction(new ShowTimeAction());

            showDateTimeMenuItem.AddMenuItem(showDateMenuItem);
            showDateTimeMenuItem.AddMenuItem(showTimeMenuItem);


            //interfacesMainMenu.AddMenuItem()

        }
    }
}
