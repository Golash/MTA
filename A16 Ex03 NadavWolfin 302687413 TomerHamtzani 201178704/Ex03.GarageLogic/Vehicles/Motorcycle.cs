using Ex03.GarageLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Vehicles
{
    internal abstract class Motorcycle : Vehicle
    {
        public Motorcycle(string i_LicenseNumber, Engine i_Engine)
            : base(i_LicenseNumber, i_Engine, k_WheelsCount, k_MaxWheelsAirPressure)
        {

        }

        public override bool SetField(string fieldName, string fieldValue)
        {
            switch (fieldName)
            {
                case k_LicenseTypeFieldName:
                    SetLicenseType(fieldValue);
                    break;
                case k_EngineVolumeFieldName:
                    SetEngineVolume(fieldValue);
                    break;
                default:
                    base.SetField(fieldName, fieldValue);
                    break;
            }

            return true;
        }

        private void SetEngineVolume(string fieldValue)
        {
            int engineVolume;
            if (!int.TryParse(fieldValue, out engineVolume))
            {
                throw new FormatException(string.Format("Failed to parse value {0}, for field {1}", fieldValue, "EngineVolume"));
            }

            EngineVolume = engineVolume;
        }

        private void SetLicenseType(string fieldValue)
        {
            
            eLicenseType licenseType;
            if (!Enum.TryParse<eLicenseType>(fieldValue, out licenseType)  || !Enum.GetNames(typeof(eLicenseType)).Contains(fieldValue))
            {
                string licenseTypeOptions = string.Join(",", Enum.GetNames(typeof(eLicenseType)));
                string errorMessage = string.Format("license type value: '{0}' is invalid, optional license Type are: {1}", fieldValue, licenseTypeOptions);
                throw new ArgumentException(errorMessage, k_LicenseTypeFieldName);
            }

            LicenseType = licenseType;
        }

        public override void VehicleDetails(StringBuilder i_VehicleDetailsStr)
        {
            base.VehicleDetails(i_VehicleDetailsStr);
            i_VehicleDetailsStr.AppendLine(string.Format("License Type: {0}", LicenseType));
            i_VehicleDetailsStr.AppendLine(string.Format("Engine Volume: {0}", EngineVolume));
        }

        protected override void fillAdditionalParameters()
        {
            base.fillAdditionalParameters();
            m_AdditionalParameters.Add(k_LicenseTypeFieldName, "Please insert license type");
            m_AdditionalParameters.Add(k_EngineVolumeFieldName, "Please insert the engine volume");
        }

        public eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }
            set
            {
                m_LicenseType = value;
            }
        }

        public int EngineVolume
        {
            get
            {
                return m_EngineVolume;
            }
            set
            {
                Validator.ValidateValueInRange(k_EngineVolumeFieldName, value, 0, int.MaxValue);
                m_EngineVolume = value;
            }
        }

        private const string k_LicenseTypeFieldName = "LicenseType";
        private const string k_EngineVolumeFieldName = "EngineVolume";
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;
        private const int k_MaxWheelsAirPressure = 32;
        private const int k_WheelsCount = 2;
    }
}
