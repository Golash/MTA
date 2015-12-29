using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    /// <summary>
    /// The menu class enable you to ask the user a question with spesfic answers and get the user selected value
    /// </summary>
    internal class Menu
    {
        /// <summary>
        /// Create a new instance of the menu class
        /// </summary>
        /// <param name="title">The menu title to display</param>
        /// <param name="options">The menu options to display</param>
        public Menu(string title, IEnumerable<string> options)
        {
            m_Options = options.ToArray();
            m_Title = title;
        }

        /// <summary>
        /// Display the user the menu
        /// </summary>
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
            return m_Options[ReadUserSelectedNumber()];
        }

        /// <summary>
        /// Display the menu and read from the user
        /// </summary>
        /// <returns></returns>
        internal int ReadUserSelectedNumber()
        {
            Display();
            int selectedNumber;
            bool isValidOption = false;
            do 
            {
                Console.Write("Please choose the option number: ");
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
