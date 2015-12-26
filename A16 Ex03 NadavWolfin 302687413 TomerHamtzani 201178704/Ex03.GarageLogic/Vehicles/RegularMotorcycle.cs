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
        public RegularMotorcycle(string i_LicenseNumber)
            : base(i_LicenseNumber, new GasEngine(k_MaxGasAmount, eGasType.Octan96))
        {
        }

        public void FillGas(eGasType i_GasType, float i_LittersToAdd)
        {
            Engine.FillGas(i_LittersToAdd);
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
