using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class ElectricCar : Car
    {
        public ElectricCar(string i_LicenseNumber, string i_ModelName, float i_MaxBatteryTime) 
            : base(i_LicenseNumber, i_ModelName)
        {
            m_Engine = new ElectricEngine(i_MaxBatteryTime);
        }

        public override string VehicleDetails()
        {
            return base.VehicleDetails();
        }

        public ElectricEngine Engine
        {
            get
            {
                return (ElectricEngine)m_Engine;
            }
            set
            {
                m_Engine = value;
            }
        }

    }
}
