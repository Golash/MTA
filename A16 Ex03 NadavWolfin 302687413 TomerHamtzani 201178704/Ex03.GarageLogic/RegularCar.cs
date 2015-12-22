using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class RegularCar : Car
    {
        public RegularCar(string i_LicenseNumber, string i_ModelName, float i_MaxGasAmount) 
            : base(i_LicenseNumber, i_ModelName)
        {
            m_Engine = new GasEngine(i_MaxGasAmount);
        }

        public override string VehicleDetails()
        {
            return base.VehicleDetails();
        }

        public GasEngine Engine
        {
            get
            {
                return (GasEngine)m_Engine;
            }
            set
            {
                m_Engine = value;
            }
        }
    }
}
