using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Exceptions
{
    public class InvalidEngineTypeException : ArgumentException
    {
        public InvalidEngineTypeException(string i_InvalidEngineType, string i_RequiredEngineType)
            : base(string.Format("The engine type: '{0}' is not supported for this operation, engine type: '{1}' is required",i_InvalidEngineType, i_RequiredEngineType), ArgumentName)
        {
            m_InvalidEngineType = i_InvalidEngineType;
            m_RequiredEngineType = i_RequiredEngineType;
        }
        /// <summary>
        /// Indicate the needed engine for the operation
        /// </summary>
        public string InvalidEngineType
        {
            get
            {
                return m_InvalidEngineType;
            }
        }

        /// <summary>
        /// Indicate the engine type of the vehicle that was used for the operation
        /// </summary>
        public string RequiredEngineType
        {
            get
            {
                return m_RequiredEngineType;
            }
        }

        private string m_InvalidEngineType;
        private string m_RequiredEngineType;
        
        private const string ArgumentName = "EngineType";
    }
}
