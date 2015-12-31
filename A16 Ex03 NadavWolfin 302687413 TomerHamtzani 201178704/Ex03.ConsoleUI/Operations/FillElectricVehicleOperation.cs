using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;
using Ex03.GarageLogic.Exceptions;

namespace Ex03.ConsoleUI.Operations
{
    /// <summary>
    /// This class in extended the abstract class <see cref="FillVehicleOperation"/>
    /// by convert the energy amount from minutes to hours.
    /// </summary>
    internal class FillElectricVehicleOperation : FillVehicleOperation
    {
        /// <summary>
        /// Create a new instance of the <see cref="FillElectricVehicleOperation"/>
        /// </summary>
        /// <param name="i_GarageManager"></param>
        public FillElectricVehicleOperation(GarageManager i_GarageManager)
            : base(i_GarageManager, "Minutes", "Chareg Battery For Elctric Vehicle")
        {
        }

        /// <summary>
        /// Convert the given <paramref name="i_Minutes"/> from minutes to hours
        /// </summary>
        /// <param name="minute"></param>
        /// <returns></returns>
        private float convertMinuteToHours(float i_Minutes)
        {
            return i_Minutes / 60;
        }

        /// <summary>
        /// Convert the given <paramref name="i_Hours"/> from hours to minutes
        /// </summary>
        /// <param name="i_Hours"></param>
        /// <returns></returns>
        private float convertHoursToMinute(float i_Hours)
        {
            return i_Hours * 60;
        }

        /// <summary>
        /// Fill energy into the vehicle engine
        /// </summary>
        /// <param name="i_LicenseNumber"></param>
        /// <param name="i_EnergyAmountToAddStrVal"></param>
        protected override void FillEnergy(string i_LicenseNumber, string i_EnergyAmountToAddStrVal)
        {
            float minutes;
            if (!float.TryParse(i_EnergyAmountToAddStrVal, out minutes))
            {
                throw new FormatException("Minutes must be a float number, the value '{0}' is invalid");
            }

            try
            {
                m_GarageManager.ChargeBattery(i_LicenseNumber, convertMinuteToHours(minutes));
            }
            catch (ValueOutOfRangeException ex)
            {
                // Conver the Min and Max limitations from hours to minutes
                throw new ValueOutOfRangeException(convertHoursToMinute(ex.MinValue), convertHoursToMinute(ex.MaxValue), ex.FieldName);
            }
        }
    }
}
