using Ex03.ConsoleUI.Helpers;
using Ex03.GarageLogic.Factory;
using Ex03.GarageLogic.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI.Operations
{
    class AddNewVehicleOperation : UserOperation
    {
        public AddNewVehicleOperation()
            : base("AddNewVehicleOperation", "Add new vehicle")
        {

        }

        public override void Execute()
        {
            Menu menu = GetSupportedVehicleMenu();
            menu.Display();
            int selectedNumber = menu.ReadUserSelected();
            Console.WriteLine("Add new vehicle operation");

        }

        public Menu GetSupportedVehicleMenu()
        {
            IList<string> vehicleOptions = new List<string>();
            foreach (Type vehicle in CarFactory.SupportedVehicle)
            {
                vehicleOptions.Add(GetVehicleDisplayName(vehicle));
            }

            return new Menu(vehicleOptions);
        }

        private string GetVehicleDisplayName(Type vehicleType)
        {
            return vehicleType.Name;
            //return ReflactionHelper.GetConstValue(vehicleType, "DisplayName");
        }
    }
}
