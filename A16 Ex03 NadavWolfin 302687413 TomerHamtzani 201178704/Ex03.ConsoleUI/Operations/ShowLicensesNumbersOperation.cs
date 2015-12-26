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
            Console.WriteLine("Press 'S' to sort by Status, any other key will show all license numbers");
            string userOption = Console.ReadLine();

            if (userOption == "S")
            {
                Menu menu = new Menu("Vehicle Status", Enum.GetNames(typeof(eVehicleStatus)));
                string userselectedOption = menu.ReadUserselectedValue();
                eVehicleStatus selectedStatus = EnumHelper.ParseByName<eVehicleStatus>(userselectedOption);

                licenseNumbers = m_GarageManager.GetLicensesNumbers(selectedStatus);
            }
            else
            {
                licenseNumbers = m_GarageManager.GetLicensesNumbers();
            }
            
            Console.WriteLine(); // Empty line for better visualization
            Console.WriteLine("The Licenses Numbers are:");

            int lineNumber = 1;
            foreach (var licenseNumber in licenseNumbers)
            {
                Console.WriteLine("{0}: {1}", lineNumber, licenseNumber);
                lineNumber++;
            }

            Console.WriteLine(); // Empty line for better visualization
            Console.WriteLine("Press Enter to back to main menu");
            Console.ReadLine();
            Console.WriteLine(); // Empty line for better visualization
        }
    }
}

