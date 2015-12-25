using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class GasEngine : Engine
    {

        public GasEngine(float i_MaxGasAmount) 
            : base(i_MaxGasAmount)
        {
            // TODO - change GasType to const
        }

        protected override void fillAdditionalParameters()
        {
            base.fillAdditionalParameters();
            m_AdditionalParameters.Add("GasType", "Please insert the car gas type");
        }

        public void FillGas(eGasType i_GasType, float i_LittersToAdd)
        {

        }

        public eGasType GasType { get; set; }

        protected override string CurrentEnergyMsg
        {
            get
            {
                return "Please Insert Current Gas Amount";
            }
        }
    }
}
