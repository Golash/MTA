using Ex03.GarageLogic;
using Ex03.GarageLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI.Operations
{
    class FillVehicleInGasOperation : UserOperation
    {
        public FillVehicleInGasOperation(GarageManager manager)
            : base(manager, "FillVehicleInGasOperation", "Fill Vehicle In Gas")
        {
        }

        public override void Execute()
        {
            bool isOperationSucceeded = false;
            while (!isOperationSucceeded)
            {
                Console.Write("Insert license number: ");
                string licenseNumber = Console.ReadLine();

                Menu menu = new Menu("Gas Type Options", Enum.GetNames(typeof(eGasType)));
                string userselectedOption = menu.ReadUserselectedValue();
                eGasType selectedGasType = EnumHelper.ParseByName<eGasType>(userselectedOption);

                Console.Write("Insert amount of gas to add: ");
                string gasAmountToAdd = Console.ReadLine();

                try
                {
                    m_GarageManager.FillGas(licenseNumber, selectedGasType, gasAmountToAdd);
                    Console.WriteLine(); // Empty line for better visualization
                    Console.WriteLine("The Vehicle with license number {0} has now more {1} liter of gas", licenseNumber, gasAmountToAdd);
                    isOperationSucceeded = true;
                }
                catch (FormatException ex)
                {
                    string errorMessage = string.Format("{0}, Plesat try again", ex.Message);
                    Console.WriteLine(errorMessage);
                }
                catch (ArgumentException ex)
                {
                    string errorMessage = string.Format("{0}, Plesat try again", ex.Message);
                    Console.WriteLine(errorMessage);
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.Write("The gas amount you wand to add, will cause to pass the gas maximum capacity for this vehicle ({0}), please insert the amount again: ", ex.MaxValue);
                    gasAmountToAdd = Console.ReadLine();
                }
            }

            Console.WriteLine("Press Enter to back to main menu");
            Console.ReadLine();
            Console.WriteLine(); // Empty line for better visualization
        }
    }
}
