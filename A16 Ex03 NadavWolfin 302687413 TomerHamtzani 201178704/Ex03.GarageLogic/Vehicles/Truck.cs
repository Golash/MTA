using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Vehicles
{
    internal class Truck : Vehicle
    {
        public Truck(string i_LicenseNumber, string i_ModelName, float i_MaxGasAmount) 
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

        public bool CarryDangerousMaterials
        {
            get
            {
                return m_CarryDangerousMaterials;
            }
            set
            {
                m_CarryDangerousMaterials = value;
            }
        }

        public float MaxCarryWeight
        {
            get
            {
                return m_MaxCarryWeight;
            }
            set
            {
                m_MaxCarryWeight = value;
            }
        }

        private bool m_CarryDangerousMaterials;
        private float m_MaxCarryWeight;
    }
}
