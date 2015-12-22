using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Vehicles
{
    public abstract class Vehicle
    {
        public Vehicle(string i_LicenseNumber, string i_ModelName)
        {
            m_Wheels = new List<Wheel>();
            m_LicenseNumber = i_LicenseNumber;
            m_ModelName = i_ModelName;
        }

        public virtual string VehicleDetails()
        {
            return string.Empty;
        }

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }
        }

        public List<Wheel> Wheels
        {
            get
            {
                return m_Wheels;
            }
            set
            {
                m_Wheels = value;
            }
        }

        private readonly string m_ModelName;
        private readonly string m_LicenseNumber;        
        private List<Wheel> m_Wheels;
        protected Engine m_Engine;
    }
}
