using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class ElectricEngine : Engine
    {
        public ElectricEngine(float i_MaxBatteryTime): base (i_MaxBatteryTime)
        {
        }

        protected override string CurrentEnergyMsg
        {
            get
            {
                return "Please Insert Remaining Battery Time (in hours)";
            }
        }
        
        protected override string CurrentEnergyAmountMsg
        {
            get
            {
                return "Battery Remaining Time (in hours)";
            }
        }

    }
}
