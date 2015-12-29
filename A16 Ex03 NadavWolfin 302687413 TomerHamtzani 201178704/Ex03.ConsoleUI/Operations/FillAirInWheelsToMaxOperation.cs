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
            : base(manager, "FillAirInWheelsToMaxOperation", "Fill Air In Wheels To Max")
        {
        }

        public override void Execute()
        {
            // Read the license number form the user
            string licenseNumber = ReadLicenseNumber();

            // Use the garage manage to fill air in all the vehicle wheels
            m_GarageManager.FillAirInWheelsToMax(licenseNumber);

            // Print successfull message to the user
            Console.WriteLine("The wheels of vehicle with license number {0} are full", licenseNumber);
        }
    }
}
