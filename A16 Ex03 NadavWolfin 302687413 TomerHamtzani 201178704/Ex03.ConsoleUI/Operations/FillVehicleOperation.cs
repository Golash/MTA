using Ex03.GarageLogic;
using Ex03.GarageLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI.Operations
{
    internal abstract class FillVehicleOperation : UserOperation
    {
        public FillVehicleOperation(GarageManager manager, string energyTypeName, string fillOperationDisplayName)
            : base(manager, "FillVehicleOperation", fillOperationDisplayName)
        {
            m_EnergyType = energyTypeName;
            m_OperationDisplayName = fillOperationDisplayName;
        }

        public override void Execute()
        {
            bool isOperationSucceeded = false;
            while (!isOperationSucceeded)
            {
                Console.Write("Insert license number: ");
                string licenseNumber = Console.ReadLine();

                Console.Write("Insert the amount to fill (in {0}): ", m_EnergyType);
                string energyToAdd = Console.ReadLine();

                try
                {
                    FillEnergy(licenseNumber, energyToAdd);
                    Console.WriteLine(); // Empty line for better visualization
                    Console.WriteLine("{0} {1} was added successfully to vehicle with license number: {2}.", energyToAdd, m_EnergyType, licenseNumber);
                    isOperationSucceeded = true;
                }
                catch (FormatException)
                {
                    string errorMessage = string.Format("The license number or the {0} amout have invalid format.");
                }
                catch (VehicleNotExistsException)
                {
                    Console.WriteLine("Invalid input, vehicle with license number: {0} not exists in the garage", licenseNumber);
                }
                catch (InvalidEngineTypeException)
                {
                    Console.WriteLine("Invalid input, the vehicle with license number: '{0}' is not supported for operation: '{1}'", licenseNumber, m_OperationDisplayName);
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine("Invalid input, the {0} amount must be between 0 to {1} ", m_EnergyType, ex.MaxValue);
                }
            }
        }


        protected abstract void FillEnergy(string i_LicenseNumber, string i_EnergyAmountToAddStrVal);


        private string m_EnergyType;
        private string m_OperationDisplayName;
    }
}
