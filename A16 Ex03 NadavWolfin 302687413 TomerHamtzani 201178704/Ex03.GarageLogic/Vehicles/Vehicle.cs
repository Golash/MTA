using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic.Helpers;

namespace Ex03.GarageLogic.Vehicles
{
    public abstract class Vehicle
    {
        public Vehicle(string i_LicenseNumber, Engine i_Engine, int i_WheelsCount, float i_MaxWheelAirPressure)
        {
            r_LicenseNumber = i_LicenseNumber;
            m_Engine = i_Engine;
            List<Wheel> wheels = new List<Wheel>(i_WheelsCount);
            for (int i = 0; i < i_WheelsCount; i++)
            {
                wheels.Add(new Wheel(i_MaxWheelAirPressure));
            }

            m_Wheels = wheels;
            fillAdditionalParameters();
        }

        public static bool IsValidLicenseNumber(string i_LicenseNumber)
        {
            return !string.IsNullOrWhiteSpace(i_LicenseNumber);
        }

        protected virtual void fillAdditionalParameters()
        {
            m_AdditionalParameters = new Dictionary<string, string>();
            m_AdditionalParameters.Add(k_ModelNameFieldName, "Please insert the vehicle Model");

            foreach (string parameter in m_Engine.AdditionalParameters.Keys)
            {
                m_AdditionalParameters.Add(parameter, m_Engine.AdditionalParameters[parameter]);
            }
        }

        public virtual bool SetField(string i_FieldName, string i_FieldValue)
        {
            switch (i_FieldName)
            {
                case k_ModelNameFieldName:
                    ModelName = i_FieldValue;
                    break;
                default:
                    m_Engine.SetField(i_FieldName, i_FieldValue);
                    break;
            }

            return true;
        }

        public virtual IDictionary<string, string> GetAdditionalParameters()
        {
            return m_AdditionalParameters;
        }

        public virtual void VehicleDetails(StringBuilder i_VehicleDetailsStr)
        {
            i_VehicleDetailsStr.AppendLine(string.Format("License Number: {0}", LicenseNumber));
            i_VehicleDetailsStr.AppendLine(string.Format("Model Name: {0}", ModelName));

            i_VehicleDetailsStr.AppendLine("Wheels Info:");

            // Start the print the number from 1 (and not zero base)
            int index = 1;
            foreach (var wheel in Wheels)
            {
                string msg = string.Format(
                    "wheel number: {0}, manufacturer: {1}, current air pressure: {2}",
                    index,
                    wheel.Manufacturer,
                    wheel.CurrentAirPressure);

                i_VehicleDetailsStr.AppendLine(msg);
                index++;
            }

            Engine.EngineDetails(i_VehicleDetailsStr);
        }

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }

            private set
            { 
                Validator.ValidateNotNullOrWhiteSpace(value, k_ModelNameFieldName);
                m_ModelName = value;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return r_LicenseNumber;
            }
        }

        public IEnumerable<Wheel> Wheels
        {
            get
            {
                return m_Wheels;
            }
        }

        public Engine Engine
        {
            get
            {
                return m_Engine;
            }
        }
        
        internal const string k_LicenseNumberFieldName = "LicenseNumber";
        private const string k_WheelsFieldName = "Wheels";
        private const string k_ModelNameFieldName = "ModelName";
        protected string m_ModelName;
        protected readonly string r_LicenseNumber;
        private List<Wheel> m_Wheels;
        protected Engine m_Engine;
        protected IDictionary<string, string> m_AdditionalParameters;
    }
}
