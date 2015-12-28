using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Exceptions
{
    public class VehicleNotExistsException : Exception
    {
        public VehicleNotExistsException(string i_VehicleLisenceNumber)
        {
            m_VehicleLisenceNumber = i_VehicleLisenceNumber;
        }

        public string VehicleLisenceNumber
        {
            get
            {
                return m_VehicleLisenceNumber;
            }
        }

        private string m_VehicleLisenceNumber;

    }
}
