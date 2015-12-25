using Ex03.GarageLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Vehicles
{
    internal class RegularMotorcycle : Motorcycle
    {
        public RegularMotorcycle(string i_LicenseNumber, string i_ModelName)
            : base(i_LicenseNumber, i_ModelName, new GasEngine(k_MaxGasAmount, eGasType.Octan96))
        {
        }


        public override bool SetField(string fieldName, string fieldValue)
        {
            switch (fieldName)
            {
                default:
                    throw new VehicleParameterNotExistsException(fieldName);
            }

            return true;
        }

        public void FillGas(eGasType i_GasType, float i_LittersToAdd)
        {
            Engine.FillGas(i_GasType, i_LittersToAdd);
        }

        public GasEngine Engine
        {
            get
            {
                return (GasEngine)m_Engine;
            }
        }

        private const int k_MaxGasAmount = 6;
    }
}
