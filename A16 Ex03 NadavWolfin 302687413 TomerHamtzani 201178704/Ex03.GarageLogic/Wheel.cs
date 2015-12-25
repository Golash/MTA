using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        public Wheel(float maxWheelAirPressure)
        {
            m_MaxAirPressure = maxWheelAirPressure;
        }

        public void FillAir(float i_AirToFill)
        {

        }

        public string Manufacturer
        {
            get
            {
                return m_Manufacturer;
            }
            set
            {
                m_Manufacturer = value;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }
            set
            {
                m_CurrentAirPressure = value;
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return m_MaxAirPressure;
            }
            set
            {
                m_MaxAirPressure = value;
            }
        }

        private string m_Manufacturer;
        private float m_CurrentAirPressure;
        private float m_MaxAirPressure;
    }
}
