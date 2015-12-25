using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Exceptions
{
    class VehicleNotExistsException : Exception
    {
        public VehicleNotExistsException(string i_LicenseNumber) : 
            base(string.Format("Vehicle with license number: {0} not exists in the garage",i_LicenseNumber))
        {
        }
    }
}
