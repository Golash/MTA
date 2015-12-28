using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI.Operations
{
    class FillElectricVehicleOperation : FillVehicleOperation
    {
        public FillElectricVehicleOperation(GarageManager i_GarageManager) : base (i_GarageManager, "Minutes", "Fill electric car")
        {

        }

        private float convertMinuteToHours(float minute)
        {
            return minute / 60;
        }

        private float convertHoursToMinute(float hours)
        {
            return hours * 60;
        }

        protected override void FillEnergy(string i_LicenseNumber, string i_EnergyAmountToAddStrVal)
        {
            float minutes;
            if (!float.TryParse(i_EnergyAmountToAddStrVal, out minutes))
            {
                throw new FormatException("Minutes must be a float number, the value '{0}' is invalid");
            }

            try
            {
                m_GarageManager.ChargeBattery(i_LicenseNumber, convertMinuteToHours(minutes).ToString());
            }
            catch (ValueOutOfRangeException ex)
            {
                throw new ValueOutOfRangeException(ex, convertMinuteToHours(ex.MinValue), convertHoursToMinute(ex.MaxValue));
            }
        }
    }
}
