using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Exceptions
{
    public class VehicleNotExistsException : ArgumentException
    {
        public VehicleNotExistsException(string i_VehicleLisenceNumber) :
            base(string.Format("A vehicle with license number: '{0}' is not exists in the garage", i_VehicleLisenceNumber), ArgumentName)
        {
            m_VehicleLisenceNumber = i_VehicleLisenceNumber;
        }

        public string LisenceNumber
        {
            get
            {
                return m_VehicleLisenceNumber;
            }
        }

        private const string ArgumentName = "LicenseNumber";
        private string m_VehicleLisenceNumber;

    }
}
