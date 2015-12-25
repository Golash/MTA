using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Exceptions
{
    class NotSupportedVehicleType : Exception
    {
        public NotSupportedVehicleType(string typeName)
            : base(string.Format("The vehicle type: {0} is not supported", typeName))
        {

        }
    }
}
