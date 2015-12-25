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
                case "Color":
                    SetColor(fieldValue);
                    break;
                case "DoorsNumber":
                    SetDoorsNumber(fieldValue);
                    break;
                default:
                    base.SetField(fieldName, fieldValue);
                    break;
            }

            return true;
        }

        private void SetDoorsNumber(string fieldValue)
        {
            Validator.ValidateNotNullOrWhiteSpace(fieldValue, "DoorsNumber");

            IEnumerable<int> enumValues = Enum.GetValues(typeof(eDoors)).Cast<int>();
            
            int doorsNumber;
            if(!(int.TryParse(fieldValue, out doorsNumber) && enumValues.Contains(doorsNumber)))
            {
                string doorsOptionString = getEnumsOptionsString(Enum.GetValues(typeof(eDoors)));
                string errorMessage = string.Format("Doors number value: '{0}' is invalid. optional values are: {1}",fieldValue, doorsOptionString);
                throw new ArgumentException(errorMessage,"DoorsNumber");
            }
            
            Doors = (eDoors)Enum.Parse(typeof(eDoors), fieldValue);
        }

        private string getEnumsOptionsString(Array enumOptions)
        {
            return string.Join(",", enumOptions);
        }

        private void SetColor(string fieldValue)
        {
<<<<<<< HEAD
            Color = EnumHelper.ParseByName<eColor>(fieldValue);
=======
            Validator.ValidateNotNullOrWhiteSpace(fieldValue, "Color");

            eColor color;
            if (!Enum.TryParse<eColor>(fieldValue, out color))
            {
                string colorOptions = string.Join(",", Enum.GetNames(typeof(eColor)));
                string errorMessage = string.Format("Color value: '{0}' is invalid, optional colors are: {1}", fieldValue, colorOptions);
                throw new ArgumentException(errorMessage, "Color");
            }

            Color = color;
>>>>>>> 8b571e8... Added
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
            set
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
            set
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
