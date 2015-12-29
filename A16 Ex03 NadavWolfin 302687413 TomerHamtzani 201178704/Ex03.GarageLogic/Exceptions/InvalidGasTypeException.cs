using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Exceptions
{
    public class InvalidGasTypeException : ArgumentException
    {
        public InvalidGasTypeException(eGasType i_InvalidGasType)
            : base(string.Format("The gas type: '{0}' is invalid", i_InvalidGasType))
        {
        }

        public eGasType InvalidGasType
        {
            get
            {
                return m_InvalidGasType;
            }
        }

        private eGasType m_InvalidGasType;
        private eGasType m_ValidGasType;
    }
}
