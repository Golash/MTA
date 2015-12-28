using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Exceptions
{
    public class InvalidGasTypeException : Exception
    {
        public InvalidGasTypeException(eGasType i_InvalidGasType, eGasType i_ValidGasType)
        {
            m_InvalidGasType = i_InvalidGasType;
            m_ValidGasType = i_ValidGasType;
        }

        public eGasType InvalidGasType
        {
            get
            {
                return m_InvalidGasType;
            }
        }

        public eGasType ValidGasType
        {
            get
            {
                return m_ValidGasType;
            }
        }
        private eGasType m_InvalidGasType;
        private eGasType m_ValidGasType;
    }
}
