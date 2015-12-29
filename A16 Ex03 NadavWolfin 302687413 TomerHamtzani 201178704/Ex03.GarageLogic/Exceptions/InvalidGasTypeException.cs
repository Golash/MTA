using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Exceptions
{
    public class InvalidGasTypeException : ArgumentException
    {
        public InvalidGasTypeException(eGasType i_InvalidGasType, eGasType i_RequiredGasType)
            : base(string.Format("The gas type: '{0}' is invalid, this operation required gas type: '{1}'",i_InvalidGasType, i_RequiredGasType), k_GasTypeFieldName)
        {
            m_InvalidGasType = i_InvalidGasType;
            m_RequiredGasType = i_RequiredGasType;
        }

        public eGasType InvalidGasType
        {
            get
            {
                return m_InvalidGasType;
            }
        }

        public eGasType RequiredGasType
        {
            get
            {
                return m_RequiredGasType;
            }
        }

        private eGasType m_InvalidGasType;
        private eGasType m_RequiredGasType;
        private const string k_GasTypeFieldName = "GasType";
    }
}
