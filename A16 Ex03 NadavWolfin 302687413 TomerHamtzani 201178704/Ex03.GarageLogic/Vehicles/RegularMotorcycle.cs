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

        private const int k_MaxGasAmount = 6;
    }
}
