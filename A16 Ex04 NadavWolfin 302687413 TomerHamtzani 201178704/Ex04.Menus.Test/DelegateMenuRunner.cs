using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex04.Menus.Delegates;
using Ex04.Menus.Delegates.Actions;

namespace Ex04.Menus.Test
{
    internal static class DelegateMenuRunner
    {
        internal static void Run()
        {
            ShowDateAction showDateMenuItemAction = new ShowDateAction();
            ShowTimeAction showTimeMenuItemAction = new ShowTimeAction();

            MenuItem showDateTimeMenuItem = new MenuItem("Show Date/Time");
            showDateTimeMenuItem.AddMenuItems(showDateMenuItemAction.MenuItems);
            showDateTimeMenuItem.AddMenuItems(showTimeMenuItemAction.MenuItems);

            CountSpacesAction countSpacesMenuItemAction = new CountSpacesAction();
            CountWordsAction countWordsMenuItemAction = new CountWordsAction();

            MenuItem versionAndActionsMenuItem = new MenuItem("Version and Actions");
            versionAndActionsMenuItem.AddMenuItems(countSpacesMenuItemAction.MenuItems);
            versionAndActionsMenuItem.AddMenuItems(countWordsMenuItemAction.MenuItems);

            MainMenu mainMenu = new MainMenu();
            mainMenu.AddMenuItem(showDateTimeMenuItem);
            mainMenu.AddMenuItem(versionAndActionsMenuItem);

            mainMenu.Show();
        }
    }
}
