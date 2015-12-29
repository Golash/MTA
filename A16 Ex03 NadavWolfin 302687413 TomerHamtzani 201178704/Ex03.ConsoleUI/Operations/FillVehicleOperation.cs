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
            Console.Write("Insert license number: ");
            string licenseNumber = Console.ReadLine();

            Console.Write("Insert the amount to fill (in {0}): ", m_EnergyType);
            string energyToAdd = Console.ReadLine();

            try
            {
                FillEnergy(licenseNumber, energyToAdd);
            }
            catch (ValueOutOfRangeException ex)
            {
                // Catch the server exception and throw more indicative message to the user
                throw new ValueOutOfRangeException(ex.MinValue, ex.MaxValue, m_EnergyType);
            }

            Console.WriteLine("{0} {1} was added successfully to vehicle with license number: {2}.", energyToAdd, m_EnergyType, licenseNumber);
        }


        protected abstract void FillEnergy(string i_LicenseNumber, string i_EnergyAmountToAddStrVal);


        private string m_EnergyType;
        private string m_OperationDisplayName;
    }
}
