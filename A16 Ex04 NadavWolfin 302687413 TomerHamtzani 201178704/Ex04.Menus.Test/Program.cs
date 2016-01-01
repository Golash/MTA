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
            MenuItem showDateTimeMenuItem = new MenuItem("Show Date/Time");

            MenuItem showDateMenuItem = new MenuItem("Show Date");
            showDateMenuItem.AddMenuItemAction(new ShowTimeAction());

            MenuItem showTimeMenuItem = new MenuItem("Show Time");
            showTimeMenuItem.AddMenuItemAction(new ShowDateAction());

            showDateTimeMenuItem.AddMenuItem(showDateMenuItem);
            showDateTimeMenuItem.AddMenuItem(showTimeMenuItem);
            
            MenuItem countSpacesMenuItem = new MenuItem("Count Spaces");
            countSpacesMenuItem.AddMenuItemAction(new CountSpacesAction());

            MenuItem countWordsMenuItem = new MenuItem("Count Words");
            countWordsMenuItem.AddMenuItemAction(new CountWordsItemAction());

            MenuItem versionAndActionsMenuItem = new MenuItem("Version and Actions");
            versionAndActionsMenuItem.AddMenuItem(countSpacesMenuItem);
            versionAndActionsMenuItem.AddMenuItem(countWordsMenuItem);

            MainMenu interfacesMainMenu = new MainMenu();
            interfacesMainMenu.AddMenuItems(showDateTimeMenuItem);
            interfacesMainMenu.AddMenuItems(versionAndActionsMenuItem);

            interfacesMainMenu.Show();
        }
    }
}
