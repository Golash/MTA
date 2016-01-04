using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex04.Menus.Delegates;

namespace Ex04.Menus.Delegates.Actions
{
    public class CountWordsItemAction : MenuItemActionBase
    {
        public CountWordsItemAction() :
            base(new List<MenuItem>() { new MenuItem("Count Words") })
        {
        }

        public override void Execute()
        {
            Console.WriteLine("Please write a sentance:");
            string sentance = Console.ReadLine();
            int wordsCount = sentance.Split(r_SpaceSeparators, StringSplitOptions.RemoveEmptyEntries).Length;

            Console.WriteLine("The number of words in the given sentance is: {0}", wordsCount);
        }

        // We consider the following characters as a word separator in a sentence
        private readonly string[] r_SpaceSeparators = new string[] { " ", "\t" };
    }
}
