using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic.Exceptions;
using Ex03.GarageLogic.Helpers;
using Ex03.GarageLogic;
using Ex03.GarageLogic.Factory;
using Ex03.GarageLogic.Vehicles;

namespace Ex03.ConsoleUI.Operations
{
    /// <summary>
    /// This operation add a new vehicle to the garage
    /// </summary>
    internal class AddNewVehicleOperation : UserOperation
    {
        /// <summary>
        /// Create a new instance of <see cref="AddNewVehicleOperation"/>
        /// </summary>
        public AddNewVehicleOperation(GarageManager i_Manager)
            : base(i_Manager, "AddNewVehicleOperation", "Add New Vehicle")
        {
        }

        /// <summary>
        /// Execute the operation
        /// </summary>
        public override void Execute()
        {
            // Get vehicle license number (insure that the value is not empty)
            string licenseNumber = readValidLicenseNumber();

            if (m_GarageManager.IsExistsVehicle(licenseNumber))
            {
                // Vehicle already exists - change the status to repairing and print a messsage
                m_GarageManager.ChangeVehicleStatus(licenseNumber, eVehicleStatus.Repairing);
                Console.WriteLine("The vehicle details was found. Vehicle {0} is now in repairing status", licenseNumber);
            }
            else
            {
                // The vehicle not exists in the garage - need to create a new one

                // Get the vehicle types
                Menu vehicleTypesMenu = GetSupportedVehicleMenu();
                string vehicleTypeName = vehicleTypesMenu.ReadUserselectedValue();

                // Read the vehicle details
                Vehicle vehicle = m_GarageManager.CreateVehicle(licenseNumber, vehicleTypeName);
                fillAditionalParameter(vehicle, "vehicle");
                readWheelsDetails(vehicle.Wheels);

                // Read the vehicle owner details
                VehicleOwnerDetails vehicleOwnerDetails = new VehicleOwnerDetails();
                fillAditionalParameter(vehicleOwnerDetails, "vehicle owner");

                // Add the vehicle to the garage
                m_GarageManager.AddNewVehicle(vehicle, vehicleOwnerDetails);

                // Print a success message to the usr
                Console.WriteLine(); // Empty line for better visualization
                Console.WriteLine("The vehicle with license number: {0} was added successfully to the garage ", vehicle.LicenseNumber);
            }
        }

        /// <summary>
        /// Fill the additional required parameters for the given <paramref name="obj"/>
        /// </summary>
        /// <param name="obj">The given object to fill for</param>
        /// <param name="inputContextName">A message to display before starting to fill the fields</param>
        private static void fillAditionalParameter(object obj, string inputContextName)
        {
            Console.WriteLine(); // add new line for better visualization
            Console.WriteLine("Insert {0} details:", inputContextName);

            IDictionary<string, string> fieldToUserMessage = getAdditionalFields(obj);
            readAdditionalParameters(obj, fieldToUserMessage);
        }

        /// <summary>
        /// Read the required fields from the user and fill them into the <see cref="obj"/> object
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fieldToUserMessage"></param>
        private static void readAdditionalParameters(object obj, IDictionary<string, string> fieldToUserMessage)
        {
            foreach (string field in fieldToUserMessage.Keys)
            {
                string fieldMessage = string.Format("{0}: ", fieldToUserMessage[field]);
                bool isValidField = false;
                string fieldValue = string.Empty;
                while (!isValidField)
                {
                    // In the add operation we catch exceptions because the user 
                    // insert a lot of arguments and we want to check each field and not failed all operations
                    try
                    {
                        Console.Write(fieldMessage);
                        fieldValue = Console.ReadLine();
                        isValidField = setFiledForObject(obj, field, fieldValue);
                    }
                    catch (ValueOutOfRangeException ex)
                    {
                        handleValueOutOfRangeException(field, ex);
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
        }

        /// <summary>
        /// In case of <see cref="ValueOutOfRangeException"/> print the user message that related to the exception information
        /// </summary>
        /// <param name="ex"></param>
        private static void handleValueOutOfRangeException(string i_FieldValiue, ValueOutOfRangeException i_Ex)
        {
            if (i_Ex.MaxValue >= float.MaxValue)
            {
                // Print nice message to the user in case of no upper limit
                Console.WriteLine("The value: '{0}' is out of range. Please insert value greater or equal to {1}", i_FieldValiue, i_Ex.MinValue);
            }
            else
            {
                Console.WriteLine("The value: '{0}' is out of range. Please insert value between {1} to {2} ", i_FieldValiue, i_Ex.MinValue, i_Ex.MaxValue);
            }
        }

        private static void readWheelsDetails(IEnumerable<Wheel> i_Wheels)
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
                            Console.WriteLine("Invalid value '{0}', Please try again.", fieldValue);
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

        /// <summary>
        /// Extract the additional required fields of the given <see cref="obj"/> 
        /// </summary>
        /// <param name="obj">The object to get the additionl parameters for</param>
        /// <returns>A mapping between field and the related user input message</returns>
        private static IDictionary<string, string> getAdditionalFields(object obj)
        {
            IDictionary<string, string> fieldToUserMessage = null;

            VehicleOwnerDetails vehicleOwnerDetails = obj as VehicleOwnerDetails;
            if (vehicleOwnerDetails != null)
            {
                fieldToUserMessage = vehicleOwnerDetails.GetAdditionalParameters();
            }

            Vehicle vehicle = obj as Vehicle;
            if (vehicle != null)
            {
                fieldToUserMessage = vehicle.GetAdditionalParameters();
            }

            return fieldToUserMessage;
        }

        /// <summary>
        /// Set the given <see cref="i_FieldName"/> and <see cref="i_FieldValue"/> to the object <see cref="obj"/>
        /// </summary>
        /// <param name="i_Obj"></param>
        /// <param name="i_FieldName"></param>
        /// <param name="i_FieldValue"></param>
        /// <returns></returns>
        private static bool setFiledForObject(object i_Obj, string i_FieldName, string i_FieldValue)
        {
            bool isValidField = false;
            VehicleOwnerDetails vehicleOwnerDetails = i_Obj as VehicleOwnerDetails;
            if (vehicleOwnerDetails != null)
            {
                isValidField = vehicleOwnerDetails.SetField(i_FieldName, i_FieldValue);
            }

            Vehicle vehicle = i_Obj as Vehicle;
            if (vehicle != null)
            {
                isValidField = vehicle.SetField(i_FieldName, i_FieldValue);
            }

            return isValidField;
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
            } 
            while (!isValidLicense);

            return licenseNumber;
        }

        /// <summary>
        /// Get a menu with all the supported vehicles
        /// </summary>
        /// <returns></returns>
        public Menu GetSupportedVehicleMenu()
        {
            return new Menu("Please choose vehicle type", m_GarageManager.GetSupportedVehicle());
        }
    }
}
