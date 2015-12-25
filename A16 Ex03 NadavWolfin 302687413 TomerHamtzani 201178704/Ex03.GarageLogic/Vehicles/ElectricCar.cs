using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Vehicles
{
    internal class ElectricCar : Car
    {
        public ElectricCar(string i_LicenseNumber)
            : base(i_LicenseNumber, new ElectricEngine(k_MaxBatteryTime))
        {
        }

        public ElectricEngine Engine
        {
            get
            {
                return (ElectricEngine)m_Engine;
            }
        }

        private const float k_MaxBatteryTime = 2.8f;
        
    }
}
