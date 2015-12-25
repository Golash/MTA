using Ex03.ConsoleUI.Operations;
using Ex03.GarageLogic;
using Ex03.GarageLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    class Program
    {
        static void Main()
        {
            bool userRequestToExit = false;
            while (!userRequestToExit)
            {
                m_GarageManager = new GarageManager();
                UserOperation[] operations = loadUserOperations();
                Menu mainMenu = GetMainMenu(operations);
                int operationNumber = mainMenu.ReadUserSelectedNumber();
                if (operations[operationNumber] is ExitOperation)
                {
                    userRequestToExit = true;
                }
                else
                {
                    operations[operationNumber].Execute();
                }
            }

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }

        public static Menu GetMainMenu(UserOperation[] operations)
        {
            string[] operationNames = operations.Select(x=>x.DisplayName).ToArray();
            return new Menu("Main Menu - Please Choose operation:", operationNames);
        }

        private static  UserOperation[] loadUserOperations()
        {
            List<UserOperation> userOperations = new List<UserOperation>();
            userOperations.Add(new AddNewVehicleOperation(m_GarageManager));

            return userOperations.ToArray();
        }

        static GarageManager m_GarageManager;
    }
}
