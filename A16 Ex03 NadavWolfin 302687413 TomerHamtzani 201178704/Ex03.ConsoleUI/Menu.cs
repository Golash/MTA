using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    internal class Menu
    {
        public Menu(string title, IEnumerable<string> options)
        {
            m_Options = options.ToArray();
            m_Title = title;
        }

        internal void Display()
        {
            StringBuilder menuStringBuilder = new StringBuilder();
            menuStringBuilder.AppendFormat("{0}:",m_Title);
            menuStringBuilder.AppendLine();
            for (int i=1; i<=m_Options.Count(); i++)
            {
                menuStringBuilder.AppendFormat("{0}. {1}", i, m_Options[i - 1]);
                menuStringBuilder.AppendLine();
            }

            Console.WriteLine(menuStringBuilder);
        }

        internal string ReadUserselectedValue()
        {
            return m_Options[ReadUserselectedNumber()];
        }
        internal int ReadUserselectedNumber()
        {
            Display();
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

        public string[] Options
        {
            get
            {
                return m_Options;
            }
        }

        public string Title
        {
            get
            {
                return m_Title;
            }
        }

        private string[] m_Options;
        private string m_Title;
    }
}
