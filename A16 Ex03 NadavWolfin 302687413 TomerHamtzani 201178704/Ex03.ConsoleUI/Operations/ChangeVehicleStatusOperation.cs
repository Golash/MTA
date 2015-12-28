using Ex03.GarageLogic;
using Ex03.GarageLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI.Operations
{
    class ChangeVehicleStatusOperation : UserOperation
    {
        public ChangeVehicleStatusOperation(GarageManager manager)
            : base(manager, "ChangeVehicleStatusOperation", "Change Vehicle Status")
        {
        }

        public override void Execute()
        {
            Console.Write("Insert license number: ");
            string licenseNumber = Console.ReadLine();
            
            Menu menu = new Menu("Status Options", Enum.GetNames(typeof(eVehicleStatus)));
            string userselectedOption = menu.ReadUserselectedValue();
            eVehicleStatus selectedNewdStatus = EnumHelper.ParseByName<eVehicleStatus>(userselectedOption);

            bool isOperationSucceeded = false;
            while (!isOperationSucceeded)
            {
                try
                {
                    m_GarageManager.ChangeVehicleStatus(licenseNumber, selectedNewdStatus);
                    Console.WriteLine(); // Empty line for better visualization
                    Console.WriteLine("The new status of vehicle with license number {0} is {1}", licenseNumber, selectedNewdStatus);
                    isOperationSucceeded = true;
                }
                catch (ArgumentException)
                {
                    Console.Write("The license number {0} doesn't exists, please insert license number again: ", licenseNumber);
                    licenseNumber = Console.ReadLine(); 
                }
                catch (FormatException)
                {
                    Console.Write("The license number {0} doesn't exists, please insert license number again: ", licenseNumber);
                    licenseNumber = Console.ReadLine(); 
                }
            }
        }
    }
}
