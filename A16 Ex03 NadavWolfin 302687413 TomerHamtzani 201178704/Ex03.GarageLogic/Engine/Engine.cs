using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic.Exceptions;
using Ex03.GarageLogic.Helpers;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        public Engine(float i_MaxEnergyCapacity)
        {
            r_MaxEnergyCapacity = i_MaxEnergyCapacity;
            r_MinEnergyCapacity = 0;
            fillAdditionalParameters();
        }

        public void FillEnergy(float i_energyToAdd)
        {
            float energyFreeSpace = MaxEnergyCapacity - CurrentEnergy;
            Validator.ValidateValueInRange(k_CurrentEnergyFieldName, i_energyToAdd, 0, energyFreeSpace);

            CurrentEnergy += i_energyToAdd;
        }

        private void SetCurrentGasAmount(string i_FieldValue)
        {
            Validator.ValidateNotNullOrWhiteSpace(i_FieldValue, k_CurrentEnergyFieldName);
            
            float currentGasAmount;
            if (!float.TryParse(i_FieldValue, out currentGasAmount))
            {
                throw new FormatException(string.Format("Failed to parse value {0}, for field {1}", i_FieldValue, "CurrentEnergy"));
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
                Validator.ValidateValueInRange(k_CurrentEnergyFieldName, value, r_MinEnergyCapacity, r_MaxEnergyCapacity);
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
                return r_MaxEnergyCapacity;
            }
        }

        public float MinEnergyCapacity
        {
            get
            {
                return r_MinEnergyCapacity;
            }
        }

        public virtual bool SetField(string i_FieldName, string i_FieldValue)
        {
            switch (i_FieldName)
            {
                case k_CurrentEnergyFieldName:
                    SetCurrentGasAmount(i_FieldValue);
                    break;
                default:
                    string errorMessage = string.Format("field Name: '{0}' doesn't exists", i_FieldValue);
                    throw new ArgumentException(errorMessage, i_FieldValue);
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

        protected abstract string CurrentEnergyMsg { get; }

        protected abstract string CurrentEnergyAmountMsg { get; }

        private const string k_CurrentEnergyFieldName = "CurrentEnergy";
        private const string k_CurrentEnergyPercentage = "Current Energy Percentage";
		internal const string k_EngineTypeFieldName = "Engine Type";
        protected IDictionary<string, string> m_AdditionalParameters;
        private float m_CurrentEnergy;
        private readonly float r_MaxEnergyCapacity;
        private readonly float r_MinEnergyCapacity;
    }
}
