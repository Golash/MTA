using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic.Helpers;

namespace Ex03.GarageLogic
{
    public class VehicleOwnerDetails
    {
        public VehicleOwnerDetails()
        {
            m_OwnerName = string.Empty;
            m_OwnerPhone = string.Empty;

            m_AdditionalFields = new Dictionary<string, string>()
            {
                { k_OwnerNameFieldName, "Vehicle owner name" },
                { k_OwnerPhoneFieldName, "Vehicle owner phone number" }
            };
        }

        public IDictionary<string, string> GetAdditionalParameters()
        {
            return m_AdditionalFields;
        }

        public bool SetField(string i_FieldName, string i_FieldValue)
        {
            switch (i_FieldName)
            {
                case k_OwnerNameFieldName:
                    OwnerName = i_FieldValue;
                    break;
                case k_OwnerPhoneFieldName:
                    OwnerPhone = i_FieldValue;
                    break;
                default:
                    string errorMessage = string.Format("The field: '{0}' not exists", i_FieldName);
                    throw new ArgumentException(errorMessage);
            }

            return true;
        }

        public string OwnerName
        {
            get
            {
                return m_OwnerName;
            }

            private set
            {
                Validator.ValidateNotNullOrWhiteSpace(value, k_OwnerPhoneFieldName);
                m_OwnerName = value;
            }
        }

        public string OwnerPhone
        {
            get
            {
                return m_OwnerPhone;
            }

            private set
            {
                Validator.ValidateNotNullOrWhiteSpace(value, k_OwnerPhoneFieldName);
                m_OwnerPhone = value;
            }
        }

        private const string k_OwnerNameFieldName = "OwnerName";
        private const string k_OwnerPhoneFieldName = "OwnerPhone";
        private IDictionary<string, string> m_AdditionalFields;
        private string m_OwnerName;
        private string m_OwnerPhone;
    }
}
