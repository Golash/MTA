using Ex03.GarageLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Vehicles
{
    internal class RegularMotorcycle : Motorcycle
    {
        public RegularMotorcycle(string i_LicenseNumber)
            : base(i_LicenseNumber, new GasEngine(k_MaxGasAmount))
        {
        }
        
        protected override void fillAdditionalParameters()
        {
            base.fillAdditionalParameters();
            foreach (string field in Engine.AdditionalParameters.Keys)
            {
                m_AdditionalParameters.Add(field, Engine.AdditionalParameters[field]);
            }
        }

        public void FillGas(eGasType i_GasType, float i_LittersToAdd)
        {
            Engine.FillGas(i_GasType, i_LittersToAdd);
        }

        public GasEngine Engine
        {
            get
            {
                return (GasEngine)m_Engine;
            }
        }

        // TODO why 6?
        private const int k_MaxGasAmount = 6;
    }
}
