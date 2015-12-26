using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI.Operations
{
    class FillElectricVehicleOperation : UserOperation
    {
        public FillElectricVehicleOperation(GarageManager manager)
            : base(manager, "FillElectricVehicleOperation", "Fill Electric Vehicle")
        {
        }

        public override void Execute()
        {
            bool isOperationSucceeded = false;
            while (!isOperationSucceeded)
            {
                Console.Write("Insert license number: ");
                string licenseNumber = Console.ReadLine();

                Console.Write("Insert minutes amount to Charge: ");
                string minutesToCharge = Console.ReadLine();

                try
                {
                    m_GarageManager.ChargeBattery(licenseNumber, minutesToCharge);
                    Console.WriteLine(); // Empty line for better visualization
                    Console.WriteLine("The Vehicle with license number {0} has now more {1} mintuts of battery", licenseNumber, minutesToCharge);
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
                    Console.WriteLine("The minitus to charge that you wand to add, will cause to pass the battary maximum capacity for this vehicle ({0}), please try again: ", ex.MaxValue);
                }
            }

            Console.WriteLine("Press Enter to back to main menu");
            Console.ReadLine();
            Console.WriteLine(); // Empty line for better visualization
        }
    }
}
