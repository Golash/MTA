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
        /// <summary>
        /// Create a new instance of <see cref="CountSpacesAction"/>
        /// </summary>
        public CountSpacesAction()
            : base(new List<MenuItem>() { new MenuItem("Count Spaces") })
        {
        }

        /// <summary>
        /// Execute the <see cref="CountSpacesAction"/> action.
        /// The actions will:
        /// 1. Read a sentance from the user.
        /// 2. Display the user the number of spaces in the sentance
        /// </summary>
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

        // Space Sign
        private const char k_Space = ' ';
    }
}
