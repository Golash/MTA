using Ex03.GarageLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Vehicles
{
    internal class ElectricMotorcycle : Motorcycle
    {
        public ElectricMotorcycle(string i_LicenseNumber)
            : base(i_LicenseNumber, new ElectricEngine(k_MaxBatteryTime))
        {
        }

        private const float k_MaxBatteryTime = 2.4f;
    }
}
