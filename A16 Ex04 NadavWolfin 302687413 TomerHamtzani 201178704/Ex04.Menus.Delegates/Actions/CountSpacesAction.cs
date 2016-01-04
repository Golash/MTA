using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex04.Menus.Delegates;

namespace Ex04.Menus.Delegates.Actions
{
    public class CountSpacesAction : MenuItemActionBase
    {
        public CountSpacesAction()
            : base(new List<MenuItem>() { new MenuItem("Count Spaces") })
        {
        }

        public override void Execute()
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

        private const char k_Space = ' ';
    }
}
