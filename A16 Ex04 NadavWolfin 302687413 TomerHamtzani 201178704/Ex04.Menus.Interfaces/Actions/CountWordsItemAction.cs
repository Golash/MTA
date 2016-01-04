﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces.Actions
{
    public class CountWordsItemAction : IMenuItemSelectedObserver
    {
        public CountWordsItemAction()
        {
            MenuItem menuItem = new MenuItem("Count Words");
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
            int wordsCount = sentance.Split(r_SpaceSeparators, StringSplitOptions.RemoveEmptyEntries).Length;

            Console.WriteLine("The number of words in the given sentance is: {0}", wordsCount);
        }

        public List<MenuItem> MenuItems
        {
            get
            {
                return m_MenuItems;
            }
        }

        private readonly List<MenuItem> m_MenuItems = new List<MenuItem>();

        // We consider the following characters as a word separator in a sentence
        private readonly string[] r_SpaceSeparators = new string[] { " ", "\t" };
    }
}