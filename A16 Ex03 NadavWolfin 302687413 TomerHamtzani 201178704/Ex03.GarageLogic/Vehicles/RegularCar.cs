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
            : base(i_LicenseNumber, new GasEngine(k_MaxGasAmount, eGasType.Octan98))
        {
        }

        private const int k_MaxGasAmount = 42;
    }
}
