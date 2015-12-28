using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI.Operations
{
    class VehicleDetailsOperation : UserOperation
    {
        public VehicleDetailsOperation(GarageManager manager) : 
            base(manager, "VehicleDetailsOperation", "Vehicle Details")
        {

        }

        public override void Execute()
        {
            Console.Write("Insert vehicle license number: ");
            string licenseNumber = Console.ReadLine();

            bool isOperationSucceeded = false;
            while (!isOperationSucceeded)
            {
                try
                {
                    Console.WriteLine(); // Empty line for better visualization
                    Console.WriteLine(m_GarageManager.GetVehicleInfo(licenseNumber));
                    isOperationSucceeded = true;
                }
                catch (ArgumentException)
                {
                    Console.Write("The license number {0} doesn't exists, please insert license number again: ", licenseNumber);
                    licenseNumber = Console.ReadLine();
                }
            }
        }
    }
}
