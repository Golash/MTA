using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Exceptions
{
    class VehicleParameterNotExistsException : Exception
    {
        public VehicleParameterNotExistsException(string i_ParameterName) :
            base(string.Format("Parameter name: '{0}' not exists", i_ParameterName))
        {

        }
    }
}
