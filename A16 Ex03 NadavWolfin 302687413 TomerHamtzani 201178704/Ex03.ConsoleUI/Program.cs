using Ex03.ConsoleUI.Operations;
using Ex03.GarageLogic;
using Ex03.GarageLogic.Helpers;
using Ex03.GarageLogic.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    class Program
    {
        static void Main()
        {
            bool userRequestToExit = false;
            m_GarageManager = new GarageManager();
            fillGarageManagerForTests(); // TODO: Remove this - it's only for tests
            
            while (!userRequestToExit)
            {
                UserOperation[] operations = loadUserOperations();
                Menu mainMenu = GetMainMenu(operations);
                int operationNumber = mainMenu.ReadUserselectedNumber();
                if (operations[operationNumber] is ExitOperation)
                {
                    userRequestToExit = true;
                }
                else
                {
                    operations[operationNumber].Execute();
                }

                Console.WriteLine(); // Empty line for better visualization
                Console.WriteLine("Press Enter to back to the Main Menu");
                Console.ReadLine();
            }

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }

        private static void fillGarageManagerForTests()
        {
            VehicleOwnerDetails nadav = new VehicleOwnerDetails();
            nadav.SetField("OwnerName", "Nadav");
            nadav.SetField("OwnerPhone", "052-432-5169");

            VehicleOwnerDetails tomer = new VehicleOwnerDetails();
            tomer.SetField("OwnerName", "Tomer");
            tomer.SetField("OwnerPhone", "052-000000");

            Vehicle truck = m_GarageManager.CreateVehicle("1", "Truck");
            truck.Wheels.ToList().ForEach(wheel => 
            {
                wheel.Manufacturer = "NadavWheels";
                wheel.CurrentAirPressure = 12.4f;
            });
            truck.Engine.CurrentEnergy = 14.2f;

            Vehicle electricMotorcycle = m_GarageManager.CreateVehicle("2", "Electric Motorcycle");
            electricMotorcycle.Wheels.ToList().ForEach(wheel =>
            {
                wheel.Manufacturer = "WheelsTel-Aviv";
                wheel.CurrentAirPressure = 12.4f;
            });
            electricMotorcycle.Engine.CurrentEnergy = 1.2f;

            Vehicle electricCar = m_GarageManager.CreateVehicle("3", "Electric Car");
            electricCar.Wheels.ToList().ForEach(wheel =>
            {
                wheel.Manufacturer = "Wheels-Car";
                wheel.CurrentAirPressure = 24.4f;
            });
            electricCar.Engine.CurrentEnergy = 1.2f;

            Vehicle regularCar = m_GarageManager.CreateVehicle("4", "Regular Car");
            regularCar.Wheels.ToList().ForEach(wheel =>
            {
                wheel.Manufacturer = "Wheels-Car";
                wheel.CurrentAirPressure = 21.4f;
            });
            regularCar.Engine.CurrentEnergy = 11.2f;

            Vehicle regularMotorcycle = m_GarageManager.CreateVehicle("5", "Regular Motorcycle");
            regularMotorcycle.Wheels.ToList().ForEach(wheel =>
            {
                wheel.Manufacturer = "Wheels-Motor";
                wheel.CurrentAirPressure = 14.4f;
            });
            regularMotorcycle.Engine.CurrentEnergy = 1.2f;

            m_GarageManager.AddNewVehicle(truck, nadav);
            m_GarageManager.AddNewVehicle(regularCar, nadav);
            m_GarageManager.AddNewVehicle(electricMotorcycle, nadav);

            m_GarageManager.AddNewVehicle(regularMotorcycle, tomer);
            m_GarageManager.AddNewVehicle(electricCar, tomer);
        }

        public static Menu GetMainMenu(UserOperation[] operations)
        {
            string[] operationNames = operations.Select(x=>x.DisplayName).ToArray();
            return new Menu("Main Menu - Please Choose operation:", operationNames);
        }

      

        private static  UserOperation[] loadUserOperations()
        {
            List<UserOperation> userOperations = new List<UserOperation>();
            userOperations.Add(new AddNewVehicleOperation(m_GarageManager));
            userOperations.Add(new ShowLicensesNumbersOperation(m_GarageManager));
            userOperations.Add(new ChangeVehicleStatusOperation(m_GarageManager));
            userOperations.Add(new FillAirInWheelsToMaxOperation(m_GarageManager));
            userOperations.Add(new FillGasVehicleOperation(m_GarageManager));
            userOperations.Add(new FillElectricVehicleOperation(m_GarageManager));
            userOperations.Add(new VehicleDetailsOperation(m_GarageManager));

            // Always add the Exit operation at the end of the list
            userOperations.Add(new ExitOperation());

            return userOperations.ToArray();
        }

        static GarageManager m_GarageManager;
    }
}
