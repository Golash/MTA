using Ex03.ConsoleUI.Operations;
using Ex03.GarageLogic;
using Ex03.GarageLogic.Exceptions;
using Ex03.GarageLogic.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    /// <summary>
    /// This class init the Main Menu for the user and manage all the user operations
    /// </summary>
    public static class UIManager
    {
        /// <summary>
        /// Start the UI - Display the main menu and manage the user operations
        /// </summary>
        public static void Start()
        {
            // Create the garage manager instance
            GarageManager garageManager = new GarageManager();
            
            // Don't stop the running 
            const bool k_Running = true;
            while (k_Running)
            {
                // Get all the available user operations
                UserOperation[] operations = loadUserOperations(garageManager);
                Menu mainMenu = GetMainMenu(operations);

                // Get the selected user operation
                int operationNumber = mainMenu.ReadUserSelectedNumber();
                UserOperation operation = operations[operationNumber];

                try
                {
                    // Exeucte user selected operation
                    operation.Execute();
                }
                catch (RequiredValueException ex)
                {
                    Console.WriteLine("Invalid input, operation parameter {0} required value is: '{1}', but '{2}' was given ", ex.ParamName, ex.RequiredValue, ex.InvalidValue);
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
                    Console.WriteLine("The parameter '{0}' is invalid, [Server Details: {1}]", ex.ParamName, ex.Message);
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

        /// <summary>
        /// Get the user main menu
        /// </summary>
        private static Menu GetMainMenu(UserOperation[] operations)
        {
            IList<string> list = new List<string>();
            foreach (UserOperation operation in operations)
            {
                list.Add(operation.DisplayName);
            }
            return new Menu("Main Menu - Please Choose operation", list);
        }

        /// <summary>
        /// Get all the user available operations
        /// </summary>
        private static  UserOperation[] loadUserOperations(GarageManager i_GarageManager)
        {
            List<UserOperation> userOperations = new List<UserOperation>();
            userOperations.Add(new AddNewVehicleOperation(i_GarageManager));
            userOperations.Add(new ShowLicensesNumbersOperation(i_GarageManager));
            userOperations.Add(new ChangeVehicleStatusOperation(i_GarageManager));
            userOperations.Add(new FillAirInWheelsToMaxOperation(i_GarageManager));
            userOperations.Add(new FillRegularVehicleOperation(i_GarageManager));
            userOperations.Add(new FillElectricVehicleOperation(i_GarageManager));
            userOperations.Add(new VehicleDetailsOperation(i_GarageManager));

            return userOperations.ToArray();
        }
    }
}
