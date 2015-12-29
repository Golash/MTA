using Ex03.GarageLogic.Exceptions;
using Ex03.GarageLogic.Helpers;
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
            m_MinEnergyCapacity = 0;
            fillAdditionalParameters();
        }

        public void FillEnergy(float i_energyToAdd)
        {
            float energyFreeSpace = MaxEnergyCapacity - CurrentEnergy;
            Validator.ValidateValueInRange(k_CurrentEnergyFieldName, i_energyToAdd, 0, energyFreeSpace);

            CurrentEnergy += i_energyToAdd;
        }

        private void SetCurrentGasAmount(string fieldValue)
        {
            Validator.ValidateNotNullOrWhiteSpace(fieldValue, k_CurrentEnergyFieldName);
            
            float currentGasAmount;
            if (!float.TryParse(fieldValue, out currentGasAmount))
            {
                throw new FormatException(string.Format("Failed to parse value {0}, for field {1}", fieldValue, "CurrentEnergy"));
            }

            Validator.ValidateValueInRange(k_CurrentEnergyFieldName, currentGasAmount, MinEnergyCapacity, MaxEnergyCapacity);
            
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
                Validator.ValidateValueInRange(k_CurrentEnergyFieldName, value, m_MinEnergyCapacity, m_MaxEnergyCapacity);
                m_CurrentEnergy = value;
            }
        }

        public float CurrentEnergyPercentage
        {
            get
            {
                return (CurrentEnergy / MaxEnergyCapacity) * 100;
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
                case k_CurrentEnergyFieldName:
                    SetCurrentGasAmount(fieldValue);
                    break;
                default:
                    string errorMessage = string.Format("field Name: '{0}' doesn't exists", fieldValue);
                    throw new ArgumentException(errorMessage, fieldValue);
            }

            return true;
        }

        public virtual void EngineDetails(StringBuilder i_EngineDetails)
        {
            i_EngineDetails.AppendLine(string.Format("{0}: {1}", CurrentEnergyAmountMsg, CurrentEnergy));
            i_EngineDetails.AppendLine(string.Format("{0}: {1} %", k_CurrentEnergyPercentage, CurrentEnergyPercentage));
        }

        protected virtual void fillAdditionalParameters()
        {
            m_AdditionalParameters = new Dictionary<string, string>();
            AdditionalParameters.Add(k_CurrentEnergyFieldName, CurrentEnergyMsg);
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
        protected abstract string CurrentEnergyAmountMsg { get; }

        private const string k_CurrentEnergyFieldName = "CurrentEnergy";
        private const string k_CurrentEnergyPercentage = "Current Energy Percentage";
		internal const string k_EngineTypeFieldName = "Engine Type";
        protected IDictionary<string,string> m_AdditionalParameters;
        private float m_CurrentEnergy;
        private readonly float m_MaxEnergyCapacity;
        private readonly float m_MinEnergyCapacity;
    }
}
