using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Vehicles
{
    internal class ElectricCar : Car
    {
        public ElectricCar(string i_LicenseNumber, string i_ModelName)
            : base(i_LicenseNumber, i_ModelName, new ElectricEngine(k_MaxBatteryTime))
        {
        }

        public override bool SetField(string fieldName, string fieldValue)
        {
            switch (fieldName)
            {
                case "BattaryLeftTimeInHours":
                    SetBattaryLeftTimeInHours(fieldValue);
                    break;
                default:
                    base.SetField(fieldName, fieldValue);
                    break;
            }

            return true;
        }

        public void SetBattaryLeftTimeInHours(string i_BattaryLeftTimeInHours)
        {
            throw new ValueOutOfRangeException(null, 0, k_MaxBatteryTime);
        }

        protected override void fillAdditionalParameters()
        {
            base.fillAdditionalParameters();
            foreach (string field in Engine.AdditionalParameters.Keys)
            {
                m_AdditionalParameters.Add(field, Engine.AdditionalParameters[field]);
            }
        }

        public ElectricEngine Engine
        {
            get
            {
                return (ElectricEngine)m_Engine;
            }
        }

        private const float k_MaxBatteryTime = 2.8f;
        
    }
}
