using Ex03.GarageLogic.Helpers;
using Ex03.GarageLogic;
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

        public AddNewVehicleOperation(GarageManager  manager)
            : base(manager, "AddNewVehicleOperation", "Add new vehicle")
        {
        }

        public override void Execute()
        {
            // Get vehicle license number
            Console.Write("License Number:");
            string licenseNumber = Console.ReadLine();

            if (m_GarageManager.IsExists(licenseNumber))
            {
                m_GarageManager.ChangeVehicleStatus(licenseNumber, eVehicleStatus.Repairing);
                Console.WriteLine("The vehicle details was found. Vehicle {0} is now in repairing status", licenseNumber);
            }
            else
            {
                // The vehicle not exists in the garage - need to create a new one

                // Get the vehicle type
                Menu vehicleTypesMenu = getSupportedVehicleMenu();
                string vehicleTypeName = vehicleTypesMenu.ReadUserSelectedValue();

                // Get model name
                Console.Write("Model Name:");
                string modelName = Console.ReadLine();

                // Create a defualt vehicle
                Vehicle vehicle = m_GarageManager.CreateVehicle(licenseNumber, modelName, vehicleTypeName);

               

                // read wheels details from the user
                readWheelsDetails(vehicle.Wheels);
                
                // Read additional required fields from user
                IDictionary<string,string> fieldToUserMessage = vehicle.GetAdditionalParameters();
                foreach (string field in fieldToUserMessage.Keys)
                {
                    string fieldMessage = fieldToUserMessage[field];
                    Console.Write("{0}: ", fieldMessage);
                    string fieldValue = Console.ReadLine();
                    bool isValidField = false;
                    while(!isValidField)
                    {
                        try
                        {
                            isValidField = vehicle.SetField(field, fieldValue);
                        }
                        catch (ArgumentException)
                       { 
                            string meesage = string.Format("Invalid value '{0}' for field: '{1}', Please try again.'", fieldValue, field);
                            Console.WriteLine(meesage);
                            Console.Write(fieldMessage);
                            Console.ReadLine();
                        }
                    }
                }

                Console.WriteLine("Insert vehicle owner details:");
                Console.Write("Name: ");
                string vehicleOwnerName = Console.ReadLine();
                Console.Write("Phone number: ");
                string vehicleOwnerPhoneNumber = Console.ReadLine();

                m_GarageManager.AddNewVehicle(vehicle, vehicleOwnerName, vehicleOwnerPhoneNumber);
                Console.WriteLine("The vehicle with license number: {0} was added successfully to the garage ", vehicle.LicenseNumber);
            }
        }

            //string[] requiredParameters = newVehicle.GetRequiredParameters();
            //IDictionary<string, string> fieldToValue = readDetailesFromUser(requiredParameters);

           

        private int getWheelsCountByVehicleType(Type vehicleType)
        {
            return 0;
        }

        private void readWheelsDetails(IEnumerable<Wheel> i_Wheels)
        {
            Console.WriteLine("Please insert the wheels details:");
            int wheelIndex = 1;
            foreach(Wheel wheel in i_Wheels)
            {
                Console.WriteLine("Insert Details for wheel number: {0}:", wheelIndex);
                Console.Write("Wheel Manufacturer:");
                string manufacturerName = Console.ReadLine();
                Console.Write("Wheel current air pressure:");
                string currentAirPressureStr = Console.ReadLine();
                float currentAirPressure;
                while (!float.TryParse(currentAirPressureStr, out currentAirPressure))
                {
                    Console.WriteLine("Invalid air pressure, Please try again");
                    Console.Write("Wheel current air pressure:");
                    currentAirPressureStr = Console.ReadLine();
                }

                wheel.CurrentAirPressure = currentAirPressure;
                wheel.Manufacturer = manufacturerName;

                wheelIndex++;
            }
        }

        public Menu getSupportedVehicleMenu()
        {
            return new Menu("Please choose vehicle type", m_GarageManager.GetSupportedVehicle());
        }
    }
}
