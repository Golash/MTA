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
        /// <summary>
        /// Create a new instance of the operation: Change Vehicle Status
        /// </summary>
        /// <param name="manager">The instance of the garage manger to make the operation on</param>
        public ChangeVehicleStatusOperation(GarageManager manager)
            : base(manager, "ChangeVehicleStatusOperation", "Change Vehicle Status")
        {
        }

        /// <summary>
        /// Execute the Change Vehicle Status operation.
        /// This operation read form the user vehicle license number and new status
        /// and update the vehicle status to the new status
        /// </summary>
        public override void Execute()
        {
            // Read the license number form the user
            string licenseNumber = ReadLicenseNumber();
            
            // Choose the new status
            Menu menu = new Menu("Status Options", Enum.GetNames(typeof(eVehicleStatus)));
            string userselectedOption = menu.ReadUserselectedValue();
            eVehicleStatus selectedNewdStatus = EnumHelper.ParseByName<eVehicleStatus>(userselectedOption);

            // Call to the server side to make the operation
            m_GarageManager.ChangeVehicleStatus(licenseNumber, selectedNewdStatus);

            // print success message to the user
            Console.WriteLine("The new status of vehicle with license number {0} is {1}", licenseNumber, selectedNewdStatus);
        }
    }
}
