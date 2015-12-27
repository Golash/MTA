using Ex03.GarageLogic.Exceptions;
using Ex03.GarageLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Vehicles
{
    internal abstract class Car : Vehicle
    {
        public Car(string i_LicenseNumber, Engine i_Engine)
            : base(i_LicenseNumber, i_Engine, k_WheelsCount, k_MaxWheelsAirPressure)
        {
        }

        public override bool SetField(string fieldName, string fieldValue)
        {
            switch (fieldName)
            {
                case k_ColorFieldName:
                    SetColor(fieldValue);
                    break;
                case k_DoorsFieldName:
                    SetDoorsNumber(fieldValue);
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
            i_VehicleDetailsStr.AppendLine(string.Format("Color: {0}", m_Color));
            i_VehicleDetailsStr.AppendLine(string.Format("Doors: {0}", m_Doors));
        }

        private void SetDoorsNumber(string fieldValue)
        {
            Validator.ValidateNotNullOrWhiteSpace(fieldValue, k_DoorsFieldName);

            IEnumerable<int> enumValues = Enum.GetValues(typeof(eDoors)).Cast<int>();
            
            int doorsNumber;
            if(!(int.TryParse(fieldValue, out doorsNumber) && enumValues.Contains(doorsNumber)))
            {
                string doorsOptionString = getEnumsOptionsString(Enum.GetValues(typeof(eDoors)));
                string errorMessage = string.Format("Doors number value: '{0}' is invalid. optional values are: {1}",fieldValue, doorsOptionString);
                throw new ArgumentException(errorMessage, k_DoorsFieldName);
            }
            
            Doors = (eDoors)Enum.Parse(typeof(eDoors), fieldValue);
        }

        private string getEnumsOptionsString(Array enumOptions)
        {
            return string.Join(",", enumOptions);
        }

        private void SetColor(string fieldValue)
        {
            Color = EnumHelper.ParseByName<eColor>(fieldValue);
        }

        protected override void fillAdditionalParameters()
        {
            base.fillAdditionalParameters();
            m_AdditionalParameters.Add(k_ColorFieldName, "Please insert the car color");
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

        private const string k_DoorsFieldName = "DoorsNumber";
        private const string k_ColorFieldName = "Color";
        private eDoors m_Doors;
        private eColor m_Color;
        private const int k_WheelsCount = 4;
        private const int k_MaxWheelsAirPressure = 29;
    }
}
