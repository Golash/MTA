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
            AdditionalParameters.Add("Gas Type", "Please insert the gas type");
        }

        public void FillGas(eGasType i_GasType, float i_LittersToAdd)
        {

        }

        public eGasType GasType { get; set; }
    }
}
