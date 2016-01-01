using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces.Actions
{
    public class CountSpacesAction : IMenuItemAction
    {
        public void Execute()
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

            Console.WriteLine("Press enter to return to menu");
            Console.ReadLine();
        }

        private const char k_Space = ' ';
    }
}
