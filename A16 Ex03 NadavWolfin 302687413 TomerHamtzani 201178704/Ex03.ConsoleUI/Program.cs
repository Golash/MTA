using Ex03.ConsoleUI.Helpers;
using Ex03.ConsoleUI.Operations;
using Ex03.GarageLogic;
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
            m_GarageManager = new GarageManager();
            UserOperation[] operations = loadUserOperations();
            Menu mainMenu = GetMainMenu(operations);
            mainMenu.Display();
            int operationNumber = mainMenu.ReadUserSelected();
            operations[operationNumber].Execute();
        }

        public static Menu GetMainMenu(UserOperation[] operations)
        {
            string[] operationNames = operations.Select(x=>x.DisplayName).ToArray();
            return new Menu(operationNames);
        }

        private static  UserOperation[] loadUserOperations()
        {
            List<UserOperation> userOperations = new List<UserOperation>();
            Type[] operationTypes = ReflactionHelper.GetSubClasses<UserOperation>().ToArray();
            foreach(Type operationType in operationTypes)
            {
                userOperations.Add((UserOperation)Activator.CreateInstance(operationType));
            }

            return userOperations.ToArray();
        }

        static GarageManager m_GarageManager;
    }
}
