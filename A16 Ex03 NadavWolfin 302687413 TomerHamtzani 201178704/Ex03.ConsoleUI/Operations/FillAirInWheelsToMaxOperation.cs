using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI.Operations
{
    class FillAirInWheelsToMaxOperation : UserOperation
    {
        public FillAirInWheelsToMaxOperation(GarageManager manager)
            : base(manager, "FillAirInWheelsToMaxOperation", "Fill Air In Wheels ToMax")
        {
        }

        public override void Execute()
        {
            Console.Write("Insert license number: ");
            string licenseNumber = Console.ReadLine();

            bool isOperationSucceeded = false;
            while (!isOperationSucceeded)
            {
                try
                {
                    m_GarageManager.FillAirInWheelsToMax(licenseNumber);
                    Console.WriteLine(); // Empty line for better visualization
                    Console.WriteLine("The wheels of vehicle with license number {0} are full", licenseNumber);
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
