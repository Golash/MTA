using Ex03.GarageLogic;
using Ex03.GarageLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI.Operations
{
    /// <summary>
    /// This class in extended the abstract class <see cref="FillVehicleOperation"/>
    /// by convert the energy amount from minutes to hours.
    /// </summary>
    class FillElectricVehicleOperation : FillVehicleOperation
    {

        public FillElectricVehicleOperation(GarageManager i_GarageManager)
            : base(i_GarageManager, "Minutes", "Chareg Battery For Elctric Vehicle")
        {
        }

        /// <summary>
        /// Convert the given <paramref name="minutes"/> from minutes to hours
        /// </summary>
        /// <param name="minute"></param>
        /// <returns></returns>
        private float convertMinuteToHours(float minutes)
        {
            return minutes / 60;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hours"></param>
        /// <returns></returns>
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
                // Conver the Min and Max limitations from hours to minutes
                throw new ValueOutOfRangeException(convertHoursToMinute(ex.MinValue), convertHoursToMinute(ex.MaxValue), ex.FieldName);
            }
        }
    }
}
