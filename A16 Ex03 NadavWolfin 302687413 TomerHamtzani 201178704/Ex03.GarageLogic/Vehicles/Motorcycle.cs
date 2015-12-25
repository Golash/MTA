using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Vehicles
{
    internal abstract class Motorcycle : Vehicle
    {
        public Motorcycle(string i_LicenseNumber, string i_ModelName, Engine i_Engine)
            : base(i_LicenseNumber, i_ModelName, i_Engine, k_WheelsCount, k_MaxWheelsAirPressure)
        {

        }

        protected override void fillAdditionalParameters()
        {
            base.fillAdditionalParameters();
            m_AdditionalParameters.Add("LicenceType","Please insert licence type");
            m_AdditionalParameters.Add("EngineVolume", "Please insert the engine volume");
        }

        public eLicenceType LicenceType
        {
            get
            {
                return m_LicenceType;
            }
            set
            {
                m_LicenceType = value;
            }
        }

        public int EngineVolume
        {
            get
            {
                return m_EngineVolume;
            }
            set
            {
                m_EngineVolume = value;
            }
        }

        private eLicenceType m_LicenceType;
        private int m_EngineVolume;
        private const int k_MaxWheelsAirPressure = 32;
        private const int k_WheelsCount = 2;
    }
}
