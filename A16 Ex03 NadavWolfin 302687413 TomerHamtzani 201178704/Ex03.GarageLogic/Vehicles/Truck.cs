using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic.Exceptions;
using Ex03.GarageLogic.Helpers;

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
            m_AdditionalParameters.Add(k_IsCarryDangerousMaterialsFieldName, "Does the truck carry dangerous materials (Y=Yes, N=No)");
            m_AdditionalParameters.Add(k_MaxCarryWeightFieldName, "Please insert truck max carry weight");
        }

        public override bool SetField(string i_FieldName, string i_FieldValue)
        {
            switch (i_FieldName)
            {
                case k_IsCarryDangerousMaterialsFieldName:
                    SetIsCarryDangerousMaterials(i_FieldValue);
                    break;
                case k_MaxCarryWeightFieldName:
                    SetMaxCarryWeight(i_FieldValue);
                    break;
                default:
                    base.SetField(i_FieldName, i_FieldValue);
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

        private void SetIsCarryDangerousMaterials(string i_FieldValue)
        {
            Validator.ValidateNotNullOrWhiteSpace(i_FieldValue, k_IsCarryDangerousMaterialsFieldName);

            // fieldValue represent an answer for boolean question - It can be YES = "Y" or NOT = "N"
            if (i_FieldValue != k_No && i_FieldValue != k_Yes)
            {
                throw new FormatException(string.Format("Failed to parse value {0}, for field {1}", i_FieldValue, k_IsCarryDangerousMaterialsFieldName));
            }

            IsCarryDangerousMaterials = i_FieldValue == k_Yes;
        }

        private void SetMaxCarryWeight(string i_FieldValue)
        {
            Validator.ValidateNotNullOrWhiteSpace(i_FieldValue, k_MaxCarryWeightFieldName);

            float maxCarryWeight;
            if (!float.TryParse(i_FieldValue, out maxCarryWeight))
            {
                throw new FormatException(string.Format("Failed to parse value {0}, for field {1}", i_FieldValue, k_MaxCarryWeightFieldName));
            }

            MaxCarryWeight = maxCarryWeight;
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
                Validator.ValidateValueInRange(k_MaxCarryWeightFieldName, value, 0, float.MaxValue);
                m_MaxCarryWeight = value;
            }
        }

        private const string k_Yes = "Y";
        private const string k_No = "N";
        private const string k_IsCarryDangerousMaterialsFieldName = "IsCarryDangerousMaterials";
        private const string k_MaxCarryWeightFieldName = "MaxCarryWeight";
        private bool m_IsCarryDangerousMaterials;
        private float m_MaxCarryWeight;
        private const int k_WheelsCount = 12;
        private const int k_MaxGasAmount = 160;
        private const int k_MaxWheelsAirPressure = 34;
    }
}
