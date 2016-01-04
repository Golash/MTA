﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces.Actions
{
    public class CountSpacesAction : IMenuItemSelectedObserver
    {
        public CountSpacesAction()
        {
            MenuItem menuItem = new MenuItem("Count Spaces");
            menuItem.AttachObserver(this);
            m_MenuItems.Add(menuItem);
        }

        public void ReportSelect(MenuItem i_MenuItem)
        {
            Execute();
        }

        private void Execute()
        {
            Console.WriteLine("Please write a sentance:");
            string sentance = Console.ReadLine();

            int spaceCount = 0;
            foreach (char currentChar in sentance)
            {
                if (currentChar == k_Space)
                {
                    spaceCount++;
                }
            }

            Console.WriteLine("The number of spaces in the given sentance is: {0}", spaceCount);
        }

        public List<MenuItem> MenuItems
        {
            get
            {
                return m_MenuItems;
            }
        }

        private readonly List<MenuItem> m_MenuItems = new List<MenuItem>();

        private const char k_Space = ' ';
    }
}
