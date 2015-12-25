﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Vehicles
{
    public abstract class Vehicle
    {
        private const string k_LicenseNumberFieldName = "LicenseNumber";
        private const string k_WheelsFieldName = "Wheels";
        private const string k_ModelNameFieldName = "ModelName";

        public Vehicle(string i_LicenseNumber, Engine i_Engine, int i_WheelsCount, float i_MaxWheelAirPressure)
        {
            m_LicenseNumber = i_LicenseNumber;
            m_Engine = i_Engine;
            List<Wheel> wheels = new List<Wheel>(i_WheelsCount);
            for (int i=0; i<i_WheelsCount; i++)
            {
                wheels.Add(new Wheel(i_MaxWheelAirPressure));
            }

            m_Wheels = wheels;
            fillAdditionalParameters();
            
        }

        protected virtual void fillAdditionalParameters()
        {
            m_AdditionalParameters = new Dictionary<string, string>();
        }

        public virtual bool SetField(string fieldName, string fieldValue)
        {
            switch (fieldName)
            {
                case "ModelName":
                    SetModelName(fieldValue);
                    break;
                default:
                    m_Engine.SetField(fieldName, fieldValue);
                    break;
            }

            return true;
        }

        private void SetModelName(string fieldValue)
        {
            // TODO add validation
            m_ModelName = fieldValue;
        }

        public virtual IDictionary<string,string> GetAdditionalParameters()
        {
            m_AdditionalParameters.Add("ModelName", "Please insert the car Model");
            return m_AdditionalParameters;
        }

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }
        }

        public IEnumerable<Wheel> Wheels
        {
            get
            {
                return m_Wheels;
            }
        }

        protected string m_ModelName;
        protected readonly string m_LicenseNumber;        
        private IEnumerable<Wheel> m_Wheels;
        protected Engine m_Engine;
        protected IDictionary<string,string> m_AdditionalParameters;
    }
}