using Ex03.GarageLogic.Exceptions;
using Ex03.GarageLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Vehicles
{
    internal class Truck : Vehicle
    {
        public Truck(string i_LicenseNumber)
            : base(i_LicenseNumber, new GasEngine(k_MaxGasAmount, eGasType.Soler), k_WheelsCount, k_MaxWheelsAirPressure)
        {
        }

        protected override void fillAdditionalParameters()
        {
            base.fillAdditionalParameters();
            m_AdditionalParameters.Add(k_IsCarryDangerousMaterialsFieldName,"Does the truck carry dangerous materials (Y=Yes, N=No)");
            m_AdditionalParameters.Add(k_MaxCarryWeightFieldName, "Please insert truck max carry weight");
        }

        public override bool SetField(string fieldName, string fieldValue)
        {
            switch (fieldName)
            {
                case k_IsCarryDangerousMaterialsFieldName:
                    SetIsCarryDangerousMaterials(fieldValue);
                    break;
                case k_MaxCarryWeightFieldName:
                    SetMaxCarryWeight(fieldValue);
                    break;
                default:
                    base.SetField(fieldName, fieldValue);
                    break;
            }

            return true;
        }

        public override void VehicleDetails(StringBuilder i_VehicleDetailsStr)
        {
            base.VehicleDetails(i_VehicleDetailsStr);
            i_VehicleDetailsStr.AppendLine(string.Format("Max Carry Weight: {0}", m_MaxCarryWeight));
            i_VehicleDetailsStr.AppendLine(string.Format("Carry Dangerous Materials: {0}", m_IsCarryDangerousMaterials));
        }

        private void SetIsCarryDangerousMaterials(string fieldValue)
        {
            Validator.ValidateNotNullOrWhiteSpace(fieldValue, k_IsCarryDangerousMaterialsFieldName);

            if (fieldValue == "Y")
            {
                IsCarryDangerousMaterials = true;
            }
            else if (fieldValue == "N")
            {
                IsCarryDangerousMaterials = false;
            }
            else
            {
                throw new FormatException(string.Format("Failed to parse value {0}, for field {1}", fieldValue, k_IsCarryDangerousMaterialsFieldName));
            }
        }

        private void SetMaxCarryWeight(string fieldValue)
        {
            Validator.ValidateNotNullOrWhiteSpace(fieldValue, k_MaxCarryWeightFieldName);

            float maxCarryWeight;
            if (!float.TryParse(fieldValue, out maxCarryWeight))
            {
                throw new FormatException(string.Format("Failed to parse value {0}, for field {1}", fieldValue, k_MaxCarryWeightFieldName));
            }

            MaxCarryWeight = maxCarryWeight;
        }

        public ElectricEngine Engine
        {
            get
            {
                return (ElectricEngine)m_Engine;
            }
        }

        public bool IsCarryDangerousMaterials
        {
            get
            {
                return m_IsCarryDangerousMaterials;
            }
            set
            {
                m_IsCarryDangerousMaterials = value;
            }
        }

        public float MaxCarryWeight
        {
            get
            {
                return m_MaxCarryWeight;
            }
            set
            {
                m_MaxCarryWeight = value;
            }
        }

        private const string k_IsCarryDangerousMaterialsFieldName = "IsCarryDangerousMaterials";
        private const string k_MaxCarryWeightFieldName = "MaxCarryWeight";
        private bool m_IsCarryDangerousMaterials;
        private float m_MaxCarryWeight;
        private const int k_WheelsCount = 12;
        private const int k_MaxGasAmount = 160;
        private const int k_MaxWheelsAirPressure = 34;
    }
}
