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

        public void ChargeBattary(float i_NumberOHoursToAdd)
        {
            // TODO: call base.Add
        }

    }
}
