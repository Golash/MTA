using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Vehicles
{
    internal class RegularCar : Car
    {
        public RegularCar(string i_LicenseNumber, string i_ModelName)
            : base(i_LicenseNumber, i_ModelName, new GasEngine(k_MaxGasAmount, eGasType.Octan98))
        {
        }
        public void FillGas(eGasType i_GasType, float i_LittersToAdd)
        {
            Engine.FillGas(i_GasType, i_LittersToAdd);
        }

        protected override void fillAdditionalParameters()
        {
            base.fillAdditionalParameters();
            IDictionary<string, string> engineAdditionalParams = Engine.AdditionalParameters;
            foreach (string field in engineAdditionalParams.Keys)
            {
                m_AdditionalParameters.Add(field, engineAdditionalParams[field]);
            }
        }
        public GasEngine Engine
        {
            get
            {
                return (GasEngine)m_Engine;
            }
        }

        private const int k_MaxGasAmount = 6;
    }
}
