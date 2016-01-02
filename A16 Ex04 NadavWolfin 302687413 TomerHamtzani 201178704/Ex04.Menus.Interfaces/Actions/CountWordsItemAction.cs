using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces.Actions
{
    public class CountWordsItemAction : IMenuItemAction
    {
        public void Execute()
        {
            Console.WriteLine("Please write a sentance:");
            string sentance = Console.ReadLine();

            int wordsCount = sentance.Split(r_SpaceSeparator, StringSplitOptions.RemoveEmptyEntries).Length;

            Console.WriteLine("The number of words in the given sentance is: {0}", wordsCount);

            Console.WriteLine();
            Console.WriteLine("Press enter to return to menu");
            Console.ReadLine();
        }

        private readonly string[] r_SpaceSeparator = new string[] { " " };
    }
}
