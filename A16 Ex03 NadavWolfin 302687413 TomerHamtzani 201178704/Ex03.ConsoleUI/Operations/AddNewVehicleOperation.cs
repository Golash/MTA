using Ex03.GarageLogic.Helpers;
using Ex03.GarageLogic;
using Ex03.GarageLogic.Factory;
using Ex03.GarageLogic.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic.Exceptions;

namespace Ex03.ConsoleUI.Operations
{
    class AddNewVehicleOperation : UserOperation
    {

        public AddNewVehicleOperation(GarageManager manager)
            : base(manager, "AddNewVehicleOperation", "Add New Vehicle")
        {
        }

        public override void Execute()
        {
            // Get vehicle license number (insure that the value is not empty)
            string licenseNumber = readValidLicenseNumber();

            if (m_GarageManager.IsExistsVehicle(licenseNumber))
            {
                m_GarageManager.ChangeVehicleStatus(licenseNumber, eVehicleStatus.Repairing);
                Console.WriteLine("The vehicle details was found. Vehicle {0} is now in repairing status", licenseNumber);
            }
            else
            {
                // The vehicle not exists in the garage - need to create a new one

                // Get the vehicle type
                Menu vehicleTypesMenu = getSupportedVehicleMenu();
                string vehicleTypeName = vehicleTypesMenu.ReadUserselectedValue();

                // Create a defualt vehicle
                Vehicle vehicle = m_GarageManager.CreateVehicle(licenseNumber, vehicleTypeName);

                // Read additional required fields from user
                IDictionary<string, string> fieldToUserMessage = vehicle.GetAdditionalParameters();
                foreach (string field in fieldToUserMessage.Keys)
                {
                    string fieldMessage = string.Format("{0}: ", fieldToUserMessage[field]);
                    bool isValidField = false;
                    string fieldValue = string.Empty;
                    while(!isValidField)
                    {
                        try
                        {
                            Console.Write(fieldMessage);
                            fieldValue = Console.ReadLine();
                            isValidField = vehicle.SetField(field, fieldValue);
                        }
                        catch (ValueOutOfRangeException ex)
                        {
                            if (ex.MaxValue == int.MaxValue)
                            {
                                // Print nice message to the user in case of unlimit upper
                                Console.WriteLine("The value: '{0}' is out of range. Please insert value greater or equal to {1}", fieldValue, ex.MinValue);
                            }
                            else
                            {
                                Console.WriteLine("The value: '{0}' is out of range. Please insert value between {1} to {2} ", fieldValue, ex.MinValue, ex.MaxValue);
                            }
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("Invalid value '{0}', Please try again.", fieldValue);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("The value: '{0}' format is invlid. Please try again", fieldValue);
                        }
                    }
                }

                // read wheels details from the user
                readWheelsDetails(vehicle.Wheels);

                VehicleOwnerDetails vehicleOwnerDetails = readVehicleOwnerDetails();
                  
                m_GarageManager.AddNewVehicle(vehicle, vehicleOwnerDetails);

                Console.WriteLine(); // Empty line for better visualization
                Console.WriteLine("The vehicle with license number: {0} was added successfully to the garage ", vehicle.LicenseNumber);
            }

        }

            private static VehicleOwnerDetails readVehicleOwnerDetails()
            {
                Console.WriteLine(); // Empty line for better visualization
                Console.WriteLine("Insert vehicle owner details");

                VehicleOwnerDetails vehicleOwnerDetails = new VehicleOwnerDetails();
                IDictionary<string,string> fieldToUserMessage = vehicleOwnerDetails.GetAdditionalParameters();
                foreach(string field in fieldToUserMessage.Keys)
                {
                    string userMessage = string.Format("{0}: ",fieldToUserMessage[field]);
                    string fieldValue = string.Empty;
                    bool isValidValue = false;
                    while (!isValidValue)
                    {
                        try
                        {
                            Console.Write(userMessage);
                            fieldValue = Console.ReadLine();
                            vehicleOwnerDetails.SetField(field, fieldValue);
                            isValidValue = true;
                        }
                        
                        catch (FormatException)
                        {
                            Console.WriteLine("The value: '{0}' format is invlid", fieldValue);
                        }
                        catch (ValueOutOfRangeException ex)
                        {
                            Console.WriteLine("The value: '{0}' is out of range. Please insert value between {1}", field, ex.MinValue);
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("The value: '{0}' is invalid", fieldValue);
                        }
                    }
                }

                return vehicleOwnerDetails;
            }

        private string readValidLicenseNumber()
        {
            string licenseNumber = string.Empty;
            bool isValidLicense = true;
            do
            {
                Console.Write("License number: ");
                licenseNumber = Console.ReadLine();
                isValidLicense = Vehicle.IsValidLicenseNumber(licenseNumber);
                if (!isValidLicense)
                {
                    Console.WriteLine("Invalid License number, please try again.");
                }
            } while (!isValidLicense);

            return licenseNumber;
        }

        private void readWheelsDetails(IEnumerable<Wheel> i_Wheels)
        {
            Console.WriteLine(); // One line space for better visualization
            Console.WriteLine("Please insert the wheels details:");
            int wheelIndex = 1;
            foreach (Wheel wheel in i_Wheels)
            {
                Console.WriteLine("Insert details for wheel number {0}:", wheelIndex);
                IDictionary<string, string> fieldToUserMessage = wheel.GetAdditionalParameters();
                foreach (string field in fieldToUserMessage.Keys)
                {
                    string userMessage = string.Format("{0}: ", fieldToUserMessage[field]);
                    string fieldValue = string.Empty;
                    bool isValidValue = false;
                    while (!isValidValue)
                    {
                        try
                        {
                            Console.Write(userMessage);
                            fieldValue = Console.ReadLine();
                            wheel.SetField(field, fieldValue);
                            isValidValue = true;
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("The value: '{0}' is invalid", fieldValue);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("The value: '{0}' format is invlid", fieldValue);
                        }
                        catch (ValueOutOfRangeException ex)
                        {
                            Console.WriteLine("The value: '{0}' is out of range. Please insert value between {1} to {2} ", fieldValue, ex.MinValue, ex.MaxValue);
                        }
                    }
                }

                wheelIndex++;
            }
        }

        public Menu getSupportedVehicleMenu()
        {
            return new Menu("Please choose vehicle type", m_GarageManager.GetSupportedVehicle());
        }
    }
}
