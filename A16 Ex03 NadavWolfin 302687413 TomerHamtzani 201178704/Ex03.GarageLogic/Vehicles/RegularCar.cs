using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Vehicles
{
    internal class RegularCar : Car
    {
        public RegularCar(string i_LicenseNumber)
            : base(i_LicenseNumber,new GasEngine(k_MaxGasAmount, eGasType.Octan98))
        {
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

        private const int k_MaxGasAmount = 42;
    }
}
