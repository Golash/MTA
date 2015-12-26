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

        public float GetCurrentEnergyPercentage()
        {
            // TODO imliment and add to details
            return 0;
        }

        protected void FillEnergy(float i_LittersToAdd)
        {
            if ((CurrentEnergy + i_LittersToAdd) > MaxEnergyCapacity)
            {
                throw new ValueOutOfRangeException(null, MinEnergyCapacity, MaxEnergyCapacity);
            }

            CurrentEnergy += i_LittersToAdd;
        }

        private void SetCurrentGasAmount(string fieldValue)
        {
            Validator.ValidateNotNullOrWhiteSpace(fieldValue, "CurrentEnergy");
            
            float currentGasAmount;
            if (!float.TryParse(fieldValue, out currentGasAmount))
            {
                throw new FormatException(string.Format("Failed to parse value {0}, for field {1}", fieldValue, "CurrentEnergy"));
            }

            Validator.ValidateValueInRange(currentGasAmount, MinEnergyCapacity, MaxEnergyCapacity);
            
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

        public virtual void EngineDetails(StringBuilder i_EngineDetails)
        {
            i_EngineDetails.AppendLine(string.Format("{0}: {1}", CurrentEnergyAmountMsg, CurrentEnergy));
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
        protected abstract string CurrentEnergyAmountMsg { get; }

        protected IDictionary<string,string> m_AdditionalParameters;
        private float m_CurrentEnergy;
        private readonly float m_MaxEnergyCapacity;
        private readonly float m_MinEnergyCapacity;
    }
}
