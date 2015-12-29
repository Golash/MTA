using Ex03.GarageLogic;
using Ex03.GarageLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI.Operations
{
    class FillGasVehicleOperation : FillVehicleOperation
    {
        public FillGasVehicleOperation(GarageManager i_GarageManager)
            : base(i_GarageManager, "Gas Litters", "Fill regular car")
        {
        }

        protected override void FillEnergy(string i_LicenseNumber, string i_EnergyAmountToAddStrVal)
        {
            Menu menu = new Menu("Please choose gas type:", Enum.GetNames(typeof(eGasType)));
            eGasType gasType = (eGasType)Enum.Parse(typeof(eGasType), menu.ReadUserselectedValue());
            m_GarageManager.FillGas(i_LicenseNumber, gasType, i_EnergyAmountToAddStrVal);
        } 
    }
}
