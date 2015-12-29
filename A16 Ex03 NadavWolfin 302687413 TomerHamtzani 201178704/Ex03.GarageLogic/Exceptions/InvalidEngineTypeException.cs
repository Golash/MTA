using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Exceptions
{
    public class InvalidEngineTypeException : ArgumentException
    {
        public InvalidEngineTypeException(string i_VehicleEngineType, string i_SupportedEngineType)
            : base(string.Format("The engine type: '{0}' is not supported for this operation, engine type: '{1}' is required",i_VehicleEngineType, i_SupportedEngineType), ArgumentName)
        {
            m_RequiredEngineType = i_VehicleEngineType;
            m_SupportedEngineType = i_SupportedEngineType;
        }
        /// <summary>
        /// Indicate the needed engine for the operation
        /// </summary>
        public string RequiredEngineType
        {
            get
            {
                return m_RequiredEngineType;
            }
        }

        /// <summary>
        /// Indicate the engine type of the vehicle that was used for the operation
        /// </summary>
        public string VehicleEngineType
        {
            get
            {
                return m_RequiredEngineType;
            }
        }

        private string m_RequiredEngineType;
        private string m_SupportedEngineType;
        
        private const string ArgumentName = "EngineType";
    }
}
