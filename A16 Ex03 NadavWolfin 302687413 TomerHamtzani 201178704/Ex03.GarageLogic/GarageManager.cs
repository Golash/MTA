using Ex03.GarageLogic.Exceptions;
using Ex03.GarageLogic.Factory;
using Ex03.GarageLogic.Helpers;
using Ex03.GarageLogic.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        public GarageManager()
        {
            m_GarageVehicles = new Dictionary<string, GarageVehicle>();
        }

        public void AddNewVehicle(Vehicle i_Vehicle, VehicleOwnerDetails i_VehicleOwnerPhoneDetails)
        {
            string licenseNumber = i_Vehicle.LicenseNumber;
            if (m_GarageVehicles.ContainsKey(licenseNumber))
            {
                string errorMessage = string.Format("Vehicle with the license number: '{0}' already exists", licenseNumber);
                throw new ArgumentException(errorMessage);
            }

            GarageVehicle garageVehicle = new GarageVehicle(i_Vehicle, i_VehicleOwnerPhoneDetails, eVehicleStatus.Repairing);
            m_GarageVehicles.Add(licenseNumber, garageVehicle);
        }

        public List<string> GetLicensesNumbers()
        {
            return m_GarageVehicles.Keys.ToList();
        }

        public List<string> GetLicensesNumbers(eVehicleStatus i_VehicleStatus)
        {
            List<string> licenseNumbers = new List<string>();

            foreach (var licenseNumber in m_GarageVehicles.Keys)
            {
                if (m_GarageVehicles[licenseNumber].Status == i_VehicleStatus)
                {
                    licenseNumbers.Add(licenseNumber);
                }
            }

            return licenseNumbers;
        }

        public void FillAirInWheelsToMax(string i_LicenseNumber)
        {
            Validator.ValidateNotNullOrWhiteSpace(i_LicenseNumber, "LicenseNumber");

            if (!IsExistsVehicle(i_LicenseNumber))
            {
                string errorMessage = string.Format("Vehicle with the license number: '{0}' not exists", i_LicenseNumber);
                throw new ArgumentException(errorMessage);
            }

            // TODO : wrap in exception?
            foreach (Wheel wheel in m_GarageVehicles[i_LicenseNumber].Vehicle.Wheels)
            {
                wheel.FillAir(wheel.MaxAirPressure);
            }
        }

        public void FillGas(string i_LicenseNumber, eGasType i_GasType, string i_LiterToAdd)
        {
            Validator.ValidateNotNullOrWhiteSpace(i_LicenseNumber, "licenseNumber");

            float literToAdd;
            if (!float.TryParse(i_LiterToAdd, out literToAdd))
            {
                string errorMessage = string.Format("Invalid format value '{0}' for gas amount to add", literToAdd);
                throw new FormatException(errorMessage);
            }

            if (!IsExistsVehicle(i_LicenseNumber))
            {
                string errorMessage = string.Format("Vehicle with the license number: '{0}' not exists", i_LicenseNumber);
                throw new ArgumentException(errorMessage);
            }

            Vehicle vehicle = m_GarageVehicles[i_LicenseNumber].Vehicle;
            GasEngine gasEngine = vehicle.Engine as GasEngine;
            if (gasEngine == null)
            {
                string errorMessage = string.Format("The vehicle {0} not support in gas Engine", i_LicenseNumber);
                throw new ArgumentException(errorMessage);
            }

            if (gasEngine.GasType != i_GasType)
            {
                string errorMessage = string.Format("The vehicle {0} not support in gas type {1}, the gas type of the vehicle is {2}",
                    i_LicenseNumber, i_GasType, gasEngine.GasType);

                throw new ArgumentException(errorMessage);
            }

            gasEngine.FillGas(literToAdd);
        }

        public void ChargeBattery(string i_LicenseNumber, string i_MinutesToCharge)
        {
            Validator.ValidateNotNullOrWhiteSpace(i_LicenseNumber, "licenseNumber");

            float MinutesToCharge;
            if (!float.TryParse(i_MinutesToCharge, out MinutesToCharge))
            {
                throw new FormatException(string.Format("Invalid format value '{0}' for minutes to charge", i_MinutesToCharge));
            }

            if (!IsExistsVehicle(i_LicenseNumber))
            {
                string errorMessage = string.Format("Vehicle with the license number: '{0}' not exists", i_LicenseNumber);
                throw new ArgumentException(errorMessage);
            }

            Vehicle vehicle = m_GarageVehicles[i_LicenseNumber].Vehicle;
            ElectricEngine electricEngine = vehicle.Engine as ElectricEngine;
            if (electricEngine == null)
            {
                string errorMessage = string.Format("The vehicle {0} not support in electric engine", i_LicenseNumber);
                throw new ArgumentException(errorMessage);
            }

            float timeToChargeInHour = MinutesToCharge / 60f;
            electricEngine.ChargeBattary(timeToChargeInHour);
        }

        public StringBuilder GetVehicleInfo(string i_LicenseNumber)
        {
            if (!IsExistsVehicle(i_LicenseNumber))
            {
                string errorMessage = string.Format("Vehicle with the license number: '{0}' not exists", i_LicenseNumber);
                throw new ArgumentException(errorMessage);
            }

            
            StringBuilder vehicleInfo = new StringBuilder();

            vehicleInfo.AppendLine("Vehicle Info:");
            m_GarageVehicles[i_LicenseNumber].Vehicle.VehicleDetails(vehicleInfo);

            vehicleInfo.AppendLine(); // Empty line for better visualization
            vehicleInfo.AppendLine("Owner Info:");
            vehicleInfo.AppendLine(string.Format("Owner Name: {0}", m_GarageVehicles[i_LicenseNumber].OwnerDetails.OwnerName));
            vehicleInfo.AppendLine(string.Format("Owner Phone: {0}", m_GarageVehicles[i_LicenseNumber].OwnerDetails.OwnerPhone));
            
            vehicleInfo.AppendLine(); // Empty line for better visualization
            vehicleInfo.AppendLine("Vehicle Status Info:");
            vehicleInfo.AppendLine(string.Format("Vehicle Status: {0}", m_GarageVehicles[i_LicenseNumber].Status));

            return vehicleInfo;
        }


        public bool IsExistsVehicle(string licenseNumber)
        {
            return m_GarageVehicles.ContainsKey(licenseNumber);
        }

        public void ChangeVehicleStatus(string licenseNumber, eVehicleStatus eVehicleStatus)
        {
            Validator.ValidateNotNullOrWhiteSpace(licenseNumber, "licenseNumber");

            if (!IsExistsVehicle(licenseNumber))
            {
                string errorMessage = string.Format("Vehicle with the license number: '{0}' not exists", licenseNumber);
                throw new ArgumentException(errorMessage);
            }

            m_GarageVehicles[licenseNumber].Status = eVehicleStatus;
        }

        private IDictionary<string, GarageVehicle> m_GarageVehicles;

        public IEnumerable<string> GetSupportedVehicle()
        {
            return CarFactory.SupportedVehicle;
        }

        public Vehicle CreateVehicle(string licenseNumber, string vehicleTypeName)
        {
            return CarFactory.CreateVehicle(licenseNumber, vehicleTypeName);
        }
    }
}
