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
            m_MinEnergyCapacity = 0;
        }

        public float GetCurrentEnergyPercentage()
        {
            return 0;
        }

        private void SetCurrentGasAmount(string fieldValue)
        {
            float currentGasAmount;
            if (float.TryParse(fieldValue, out currentGasAmount))
            {
                throw new FormatException();
            }
            if (currentGasAmount > MaxEnergyCapacity || currentGasAmount < MinEnergyCapacity)
            {
                throw new ValueOutOfRangeException(null, MinEnergyCapacity, MaxEnergyCapacity);
            }
            
            CurrentEnergy = currentGasAmount;
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

        public float MinEnergyCapacity
        {
            get
            {
                return m_MinEnergyCapacity;
            }
        }

        public virtual bool SetField(string fieldName, string fieldValue)
        {
            switch (fieldName)
            {
                case "CurrentEnergy":
                    SetCurrentGasAmount(fieldValue);
                    break;
                default:
                    string errorMessage = string.Format("field Name: '{0}' doesn't exists", fieldValue);
                    throw new ArgumentException(errorMessage, fieldValue);
            }

            return true;
        }

        protected virtual void fillAdditionalParameters()
        {
            m_AdditionalParameters = new Dictionary<string, string>();
            AdditionalParameters.Add("CurrentEnergy", CurrentEnergyMsg);
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

        protected abstract string CurrentEnergyMsg { get;}

        protected IDictionary<string,string> m_AdditionalParameters;
        private float m_CurrentEnergy;
        private readonly float m_MaxEnergyCapacity;
        private readonly float m_MinEnergyCapacity;
    }
}
