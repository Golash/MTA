using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex04.Menus.Delegates;

namespace Ex04.Menus.Delegates.Actions
{
    public class CountWordsAction : MenuItemActionBase
    {
        /// <summary>
        /// Create a new instance of <see cref="CountWordsAction"/>
        /// </summary>
        public CountWordsAction() :
            base(new List<MenuItem>() { new MenuItem("Count Words") })
        {
        }

        /// <summary>
        /// Execute the <see cref="CountWordsAction"/> action.
        /// The actions will:
        /// 1. Read a sentance from the user.
        /// 2. Display the user the number of words in the sentance
        /// </summary>
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
