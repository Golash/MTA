using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic.Exceptions;
using Ex03.GarageLogic.Helpers;

namespace Ex03.GarageLogic.Vehicles
{
    internal abstract class Car : Vehicle
    {
        public Car(string i_LicenseNumber, Engine i_Engine)
            : base(i_LicenseNumber, i_Engine, k_WheelsCount, k_MaxWheelsAirPressure)
        {
        }

        public override bool SetField(string i_FieldName, string i_FieldValue)
        {
            switch (i_FieldName)
            {
                case k_ColorFieldName:
                    SetColor(i_FieldValue);
                    break;
                case k_DoorsFieldName:
                    SetDoorsNumber(i_FieldValue);
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
            i_VehicleDetailsStr.AppendLine(string.Format("Color: {0}", m_Color));
            i_VehicleDetailsStr.AppendLine(string.Format("Doors: {0}", m_Doors));
        }

        private void SetDoorsNumber(string i_FieldValue)
        {
            Validator.ValidateNotNullOrWhiteSpace(i_FieldValue, k_DoorsFieldName);

            IEnumerable<int> enumValues = Enum.GetValues(typeof(eDoors)).Cast<int>();
            
            int doorsNumber;
            if(!(int.TryParse(i_FieldValue, out doorsNumber) && enumValues.Contains(doorsNumber)))
            {
                string doorsOptionString = string.Join(",", (Enum.GetValues(typeof(eDoors))));
                string errorMessage = string.Format("Doors number value: '{0}' is invalid. optional values are: {1}", i_FieldValue, doorsOptionString);
                throw new ArgumentException(errorMessage, k_DoorsFieldName);
            }
            
            Doors = (eDoors)Enum.Parse(typeof(eDoors), i_FieldValue);
        }

        private void SetColor(string i_FieldValue)
        {
            Color = EnumHelper.ParseByName<eColor>(i_FieldValue);
        }

        protected override void fillAdditionalParameters()
        {
            base.fillAdditionalParameters();
            string colorInputMessage = string.Format("Please insert the car color ({0})", r_OptionalColores);
            m_AdditionalParameters.Add(k_ColorFieldName, colorInputMessage);
            m_AdditionalParameters.Add(k_DoorsFieldName, "Please insert the doors number");
        }

        public eColor Color
        {
            get
            {
                return m_Color;
            }

            private set
            {
                m_Color = value;
            }
        }

        public eDoors Doors
        {
            get
            {
                return m_Doors;
            }

            private set
            {
                m_Doors = value;
            }
        }

        private readonly string r_OptionalColores = string.Join(",", Enum.GetNames(typeof(eColor)));
        private const string k_DoorsFieldName = "DoorsNumber";
        private const string k_ColorFieldName = "Color";
        private eDoors m_Doors;
        private eColor m_Color;
        private const int k_WheelsCount = 4;
        private const int k_MaxWheelsAirPressure = 29;
    }
}
