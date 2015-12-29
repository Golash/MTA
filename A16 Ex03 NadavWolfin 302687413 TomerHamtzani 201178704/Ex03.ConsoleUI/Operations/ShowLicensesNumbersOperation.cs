using Ex03.GarageLogic;
using Ex03.GarageLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI.Operations
{
    class ShowLicensesNumbersOperation : UserOperation
    {
        public ShowLicensesNumbersOperation(GarageManager manager)
            : base(manager, "ShowLicensesNumbersOperation", "Show Licenses Numbers")
        {

        }

        public override void Execute() 
        {
            List<string> licenseNumbers;
            IList<string> menuOptions = Enum.GetNames(typeof(eVehicleStatus)).ToList();
            menuOptions.Add(k_AllVehicleKey);
            Menu menu = new Menu("Vehicle Status", menuOptions);
            string userselectedOption = menu.ReadUserselectedValue();

            if (userselectedOption == k_AllVehicleKey)
            {
                licenseNumbers = m_GarageManager.GetLicensesNumbers();
            }
            else
            {
                eVehicleStatus selectedStatus = EnumHelper.ParseByName<eVehicleStatus>(userselectedOption);
                licenseNumbers = m_GarageManager.GetLicensesNumbers(selectedStatus);
            }
            
            Console.WriteLine(); // Empty line for better visualization
            if (licenseNumbers.Count > 0)
            {
                Console.WriteLine("The licenses numbers are:");

                int lineNumber = 1;
                foreach (var licenseNumber in licenseNumbers)
                {
                    Console.WriteLine("{0}: {1}", lineNumber, licenseNumber);
                    lineNumber++;
                }
            }
            else
            {
                Console.WriteLine("No licenses numbers founds.");
            }
        }

        private const string k_AllVehicleKey = "All";
    }
}

