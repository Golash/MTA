using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    internal class Menu
    {
        public Menu(IEnumerable<string> options)
        {
            m_Options = options.ToArray();
        }

        private string[] m_Options;

        internal void Display()
        {
            StringBuilder menuStringBuilder = new StringBuilder();
            for (int i=1; i<=m_Options.Count(); i++)
            {
                menuStringBuilder.AppendFormat("{0}. {1}", i, m_Options[i - 1]);
                menuStringBuilder.AppendLine();
            }

            Console.WriteLine(menuStringBuilder);
        }

        internal int ReadUserSelected()
        {
            int selectedNumber;
            bool isValidOption = false;
            do 
            {
                Console.Write("Please choose the option number:");
                string selectedNumberStr = Console.ReadLine();
                isValidOption = int.TryParse(selectedNumberStr, out selectedNumber) && inRange(selectedNumber);
                if (!isValidOption)
                {
                    Console.WriteLine("Invalid option, please try again");
                }

            }
            while (!isValidOption);

            return selectedNumber - 1 ;
        }

        private bool inRange(int option)
        {
            return option - 1 >= 0 && option <= m_Options.Length; 
        }
    }
}
