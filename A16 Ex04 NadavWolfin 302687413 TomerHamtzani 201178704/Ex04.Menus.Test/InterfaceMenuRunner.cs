using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex04.Menus.Interfaces;
using Ex04.Menus.Interfaces.Actions;

namespace Ex04.Menus.Test
{
    internal static class InterfaceMenuRunner
    {
        internal static void Run()
        {
            ShowTimeAction showTimeMenuItem = new ShowTimeAction();
            ShowDateAction showDateMenuItem = new ShowDateAction();

            MenuItem showDateTimeMenuItem = new MenuItem("Show Date/Time");
            showDateTimeMenuItem.AddMenuItems(showTimeMenuItem.MenuItems);
            showDateTimeMenuItem.AddMenuItems(showDateMenuItem.MenuItems);

            CountSpacesAction countSpacesAction = new CountSpacesAction();
            CountWordsAction countWordsAction = new CountWordsAction();

            MenuItem versionAndActionsMenuItem = new MenuItem("Version and Actions");
            versionAndActionsMenuItem.AddMenuItems(countSpacesAction.MenuItems);
            versionAndActionsMenuItem.AddMenuItems(countWordsAction.MenuItems);

            MainMenu interfacesMainMenu = new MainMenu();
            interfacesMainMenu.AddMenuItem(showDateTimeMenuItem);
            interfacesMainMenu.AddMenuItem(versionAndActionsMenuItem);

            interfacesMainMenu.Show();
        }
    }
}
