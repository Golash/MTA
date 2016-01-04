using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex04.Menus.Interfaces;
using Ex04.Menus.Interfaces.Actions;

namespace Ex04.Menus.Test
{
    internal class Program
    {
        public static void Main()
        {
            DelegateMenuRunner.Run();

            Console.Clear();
            Console.WriteLine("Exit Successfully, Press Enter to move to the next menu");
            Console.ReadLine();

            InterfaceMenuRunner.Run();
        }
    }
}
