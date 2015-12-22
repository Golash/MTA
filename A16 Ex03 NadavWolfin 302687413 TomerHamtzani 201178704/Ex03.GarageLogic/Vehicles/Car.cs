using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Vehicles
{
    internal abstract class Car : Vehicle
    {
        public Car(string i_LicenseNumber, string i_ModelName) : base(i_LicenseNumber, i_ModelName)
        {

        }

        
        public eColor Color
        {
            get
            {
                return m_Color;
            }
            set
            {
                m_Color = value;
            }
        }

        public eDoors Doors
        {
            get
            {
                return m_Doors;
            }
            set
            {
                m_Doors = value;
            }
        }

        private eDoors m_Doors;
        private eColor m_Color;
    }
}
