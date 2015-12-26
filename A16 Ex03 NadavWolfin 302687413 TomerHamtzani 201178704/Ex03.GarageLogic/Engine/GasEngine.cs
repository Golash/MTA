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
            m_GasType = i_GasType;
        }

        public void FillGas(float i_LittersToAdd)
        {
            base.FillEnergy(i_LittersToAdd);
        }
        
        public override void EngineDetails(StringBuilder i_EngineDetails)
        {
            base.EngineDetails(i_EngineDetails);

            i_EngineDetails.AppendLine(string.Format("Gas Type: {0}", m_GasType));
        }

        protected override string CurrentEnergyMsg
        {
            get
            {
                return "Please Insert Current Gas Amount";
            }
        }

        protected override string CurrentEnergyAmountMsg
        {
            get
            {
                return "Current Gas Amount";
            }
        }

        public eGasType GasType
        {
            get
            {
                return m_GasType;
            }
            set
            {
                m_GasType = value;
            }
        }

        private eGasType m_GasType;
    }
}
