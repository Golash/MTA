using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class GasEngine : Engine
    {

        public GasEngine(float i_MaxGasAmount, eGasType i_GasType): base(i_MaxGasAmount)
        {
            GasType = i_GasType;
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
