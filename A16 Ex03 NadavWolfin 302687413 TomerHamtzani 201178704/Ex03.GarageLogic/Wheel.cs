using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic.Helpers;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        public Wheel(float i_MaxWheelAirPressure)
        {
            m_MaxAirPressure = i_MaxWheelAirPressure;

            // For now the min air pressure is zero, if it will changed one day we can change it in one place
            r_MinAirPressure = 0;

            r_AdditionalParameter = new Dictionary<string, string>();
            r_AdditionalParameter.Add("Manufacture", "Wheel Manufacturer");
            r_AdditionalParameter.Add("CurrentAirPressure", "Wheel current air pressure");
        }

        public bool SetField(string i_FieldName, string i_FieldValue)
        {
            switch (i_FieldName)
            {
                case k_ManufactureFieldName:
                    Manufacturer = i_FieldValue;
                    break;
                case k_CurrentAirPressureFieldName:
                    setCurrentAirPressure(i_FieldValue);
                    break;
                default:
                    throw new ArgumentException(string.Format("The field: '{0}' not exists", i_FieldName), i_FieldName);
            }

            return true;
        }

        public virtual IDictionary<string, string> GetAdditionalParameters()
        {
            return r_AdditionalParameter;
        }

        private void setCurrentAirPressure(string i_CurrentAirPressureStrValue)
        {
            float currentAirPressure;
            if(!float.TryParse(i_CurrentAirPressureStrValue, out currentAirPressure))
            {
                string errorMessage = string.Format("The field '{0}' must be float", k_CurrentAirPressureFieldName);
                throw new ArgumentException(errorMessage);
            }

            CurrentAirPressure = currentAirPressure;
        }
        
        /// <summary>
        /// Fill the wheel air pressure to maximum
        /// </summary>
        public void FillToMax()
        {
            this.CurrentAirPressure = MaxAirPressure;
        }

        public IDictionary<string, string> GetAdditionalParametersForWheel(int i_WheelNumber)
        {
            return r_AdditionalParameter;
        }

        public string Manufacturer
        {
            get
            {
                return m_Manufacturer;
            }

            set
            {
                Validator.ValidateNotNullOrWhiteSpace(value, k_ManufactureFieldName);
                m_Manufacturer = value;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }

            set
            {
                Validator.ValidateValueInRange(k_CurrentAirPressureFieldName, value, MinAirPressure, MaxAirPressure);
                m_CurrentAirPressure = value;
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return m_MaxAirPressure;
            }
        }

        public float MinAirPressure
        {
            get
            {
                return r_MinAirPressure;
            }
        }

        internal const string k_ManufactureFieldName = "Manufacture";
        internal const string k_CurrentAirPressureFieldName = "CurrentAirPressure";

        private string m_Manufacturer;
        private float m_CurrentAirPressure;
        private float m_MaxAirPressure;
        private readonly float r_MinAirPressure;
        private readonly IDictionary<string, string> r_AdditionalParameter;
    }
}
