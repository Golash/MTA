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
        public ElectricMotorcycle(string i_LicenseNumber, string i_ModelName)
            : base(i_LicenseNumber, i_ModelName, new ElectricEngine(k_MaxBatteryTime))
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

        public ElectricEngine Engine
        {
            get
            {
                return (ElectricEngine)m_Engine;
            }
        }

        private const float k_MaxBatteryTime = 2.4f;
    }
}
