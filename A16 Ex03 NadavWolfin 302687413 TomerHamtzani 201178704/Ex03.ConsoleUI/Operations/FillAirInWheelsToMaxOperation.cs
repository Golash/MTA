using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI.Operations
{
    internal class FillAirInWheelsToMaxOperation : UserOperation
    {
        public FillAirInWheelsToMaxOperation(GarageManager i_Manager)
            : base(i_Manager, "FillAirInWheelsToMaxOperation", "Fill Air In Wheels To Max")
        {
        }

        /// <summary>
        /// Execute the operation
        /// </summary>
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
