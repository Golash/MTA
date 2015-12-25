using Ex03.GarageLogic.Exceptions;
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
            m_AdditionalParameters.Add("IsCarryDangerousMaterials","Does the truck carry dangerous materials (Y=Yes, N=No)");
            m_AdditionalParameters.Add("MaxCarryWeight", "Please insert truck max carry weight");
        }

        public override bool SetField(string fieldName, string fieldValue)
        {
            switch (fieldName)
            {
                default:
                    throw new VehicleParameterNotExistsException(fieldName);
            }

            return true;
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

        private bool m_IsCarryDangerousMaterials;
        private float m_MaxCarryWeight;
        private const int k_WheelsCount = 12;
        private const int k_MaxGasAmount = 160;
        private const int k_MaxWheelsAirPressure = 34;
    }
}
