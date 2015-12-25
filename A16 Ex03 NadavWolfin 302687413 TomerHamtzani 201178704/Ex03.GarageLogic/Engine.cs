using Ex03.GarageLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        public Engine(float i_MaxEnergyCapacity)
        {
            m_MaxEnergyCapacity = i_MaxEnergyCapacity;
            m_CurrentEnergy = 0;
            m_AdditionalParameters  = new Dictionary<string,string>();
        }

        public float GetCurrentEnergyPercentage()
        {
            return 0;
        }

        public float CurrentEnergy
        {
            get
            {
                return m_CurrentEnergy;
            }
            set
            {
                m_CurrentEnergy = value;
            }
        }

        public float MaxEnergyCapacity
        {
            get
            {
                return m_MaxEnergyCapacity;
            }
        }

        public virtual bool SetField(string fieldName, string fieldValue)
        {
            throw new VehicleParameterNotExistsException(fieldName);
        }

        public IDictionary<string, string> AdditionalParameters
        {
            get
            {
                return m_AdditionalParameters;
            }

            protected set
            {
                m_AdditionalParameters = value;
            }
        }

        private IDictionary<string,string> m_AdditionalParameters;
        private float m_CurrentEnergy;
        private readonly float m_MaxEnergyCapacity;
    }
}
