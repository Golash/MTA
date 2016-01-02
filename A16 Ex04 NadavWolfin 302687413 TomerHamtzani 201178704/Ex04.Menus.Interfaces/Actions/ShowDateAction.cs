using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces.Actions
{
    public class ShowDateAction : IMenuItemAction
    {
        public void Execute()
        {
            Console.WriteLine("The current data is: {0}", DateTime.Now.ToShortDateString());

            Console.WriteLine();
            Console.WriteLine("Press enter to return to menu");
            Console.ReadLine();
        }
    }
}
