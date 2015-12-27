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
                case k_LicenceTypeFieldName:
                    SetLicenceTyper(fieldValue);
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
            Validator.ValidateNotNullOrWhiteSpace(fieldValue, k_EngineVolumeFieldName);

            int engineVolume;
            if (!int.TryParse(fieldValue, out engineVolume))
            {
                throw new FormatException(string.Format("Failed to parse value {0}, for field {1}", fieldValue, "EngineVolume"));
            }

            EngineVolume = engineVolume;
        }

        private void SetLicenceTyper(string fieldValue)
        {
            
            eLicenceType licenceType;
            if (!Enum.TryParse<eLicenceType>(fieldValue, out licenceType)  || !Enum.GetNames(typeof(eLicenceType)).Contains(fieldValue))
            {
                string licenceTypeOptions = string.Join(",", Enum.GetNames(typeof(eLicenceType)));
                string errorMessage = string.Format("licence type value: '{0}' is invalid, optional licence Type are: {1}", fieldValue, licenceTypeOptions);
                throw new ArgumentException(errorMessage, k_LicenceTypeFieldName);
            }

            LicenceType = licenceType;
        }

        public override void VehicleDetails(StringBuilder i_VehicleDetailsStr)
        {
            base.VehicleDetails(i_VehicleDetailsStr);
            i_VehicleDetailsStr.AppendLine(string.Format("Model: {0}", m_ModelName));
            i_VehicleDetailsStr.AppendLine(string.Format("Engine Volume: {0}", m_EngineVolume));
        }

        protected override void fillAdditionalParameters()
        {
            base.fillAdditionalParameters();
            m_AdditionalParameters.Add(k_LicenceTypeFieldName, "Please insert licence type");
            m_AdditionalParameters.Add(k_EngineVolumeFieldName, "Please insert the engine volume");
        }

        public eLicenceType LicenceType
        {
            get
            {
                return m_LicenceType;
            }
            set
            {
                m_LicenceType = value;
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
                m_EngineVolume = value;
            }
        }

        private const string k_LicenceTypeFieldName = "LicenceType";
        private const string k_EngineVolumeFieldName = "EngineVolume";
        private eLicenceType m_LicenceType;
        private int m_EngineVolume;
        private const int k_MaxWheelsAirPressure = 32;
        private const int k_WheelsCount = 2;
    }
}
