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
            MenuItem showDateMenuItem = new MenuItem("Show Date", i_MenuItemActions: new List<IMenuItemAction>() { new ShowDateAction() });
            MenuItem showTimeMenuItem = new MenuItem("Show Time", i_MenuItemActions: new List<IMenuItemAction>() { new ShowTimeAction() });
            MenuItem showDateTimeMenuItem = new MenuItem("Show Date/Time", i_MenuItems: new List<MenuItem>(){ showDateMenuItem, showTimeMenuItem });

            MenuItem countSpacesMenuItem = new MenuItem("Count Spaces", i_MenuItemActions: new List<IMenuItemAction>(){new CountSpacesAction()});
            MenuItem countWordsMenuItem = new MenuItem("Count Words", i_MenuItemActions: new List<IMenuItemAction>(){new CountWordsItemAction()});
            MenuItem versionAndActionsMenuItem = new MenuItem("Version and Actions", i_MenuItems: new List<MenuItem>() { countSpacesMenuItem , countWordsMenuItem});

            MainMenu interfacesMainMenu = new MainMenu(i_MenuItems: new List<MenuItem>() { showDateTimeMenuItem, versionAndActionsMenuItem });
            interfacesMainMenu.Show();
        }
    }
}
