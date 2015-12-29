using Ex03.ConsoleUI.Operations;
using Ex03.GarageLogic;
using Ex03.GarageLogic.Exceptions;
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
                int operationNumber = mainMenu.ReadUserSelectedNumber();
                UserOperation operation = operations[operationNumber];
                if (operation is ExitOperation)
                {
                    userRequestToExit = true;
                }
                else
                {
                    
                    try
                    {
                        operation.Execute();
                    }
                    catch (InvalidEngineTypeException ex)
                    {
                        Console.WriteLine("Invalid input, Vehicle engine type: '{0}' is not supported for operation: '{1}', engine type: '{2}' is required ",ex.VehicleEngineType, ex.RequiredEngineType);
                    }
                    catch (VehicleNotExistsException ex)
                    {
                        Console.WriteLine("Invalid input, vehicle with license number: '{0}' not exists in the garage", ex.LisenceNumber);
                    }
                    catch (ValueOutOfRangeException ex)
                    {
                        Console.WriteLine("Invalid input, the field: '{0}' must be between {1} to {2} ", ex.FieldName, ex.MinValue, ex.MaxValue);
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine("One of the operation parameter has invalid format [Server Details: {0}]", ex.Message);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine("The parameter '{0}' is invalid, [Server Details: {0}]", ex.ParamName, ex.Message);
                    }
                    catch (Exception ex)
                    {
                        // In case of unknown exception - catch it and print the internal server message
                        Console.WriteLine("Internal Server Error, [Server Details: {0}]", ex.Message);
                    }
                    
                    Console.WriteLine(); // Empty line for better visualization
                    Console.WriteLine("Press Enter to back to the Main Menu");
                    Console.ReadLine();
                }
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
            truck.SetField("ModelName", "Model1");
            //truck.

            Vehicle electricMotorcycle = m_GarageManager.CreateVehicle("2", "Electric Motorcycle");
            electricMotorcycle.Wheels.ToList().ForEach(wheel =>
            {
                wheel.Manufacturer = "WheelsTel-Aviv";
                wheel.CurrentAirPressure = 12.4f;
            });
            electricMotorcycle.Engine.CurrentEnergy = 1.2f;
            electricMotorcycle.SetField("ModelName", "Model1");

            Vehicle electricCar = m_GarageManager.CreateVehicle("3", "Electric Car");
            electricCar.Wheels.ToList().ForEach(wheel =>
            {
                wheel.Manufacturer = "Wheels-Car";
                wheel.CurrentAirPressure = 24.4f;
            });
            electricCar.Engine.CurrentEnergy = 1.2f;
            electricCar.SetField("ModelName", "Model1");

            Vehicle regularCar = m_GarageManager.CreateVehicle("4", "Regular Car");
            regularCar.Wheels.ToList().ForEach(wheel =>
            {
                wheel.Manufacturer = "Wheels-Car";
                wheel.CurrentAirPressure = 21.4f;
            });
            regularCar.Engine.CurrentEnergy = 11.2f;
            regularCar.SetField("ModelName", "Model1");

            Vehicle regularMotorcycle = m_GarageManager.CreateVehicle("5", "Regular Motorcycle");
            regularMotorcycle.Wheels.ToList().ForEach(wheel =>
            {
                wheel.Manufacturer = "Wheels-Motor";
                wheel.CurrentAirPressure = 14.4f;
            });
            regularMotorcycle.Engine.CurrentEnergy = 1.2f;
            regularMotorcycle.SetField("ModelName", "Model1");

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
