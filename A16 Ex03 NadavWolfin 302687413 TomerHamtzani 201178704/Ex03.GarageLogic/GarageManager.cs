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
            ValidateIsVehicleExists(i_LicenseNumber);
            
            // Run all over the vehicle wheels and fill the air to max
            foreach (Wheel wheel in m_GarageVehicles[i_LicenseNumber].Vehicle.Wheels)
            {
                wheel.FillToMax();
            }
        }

        public void FillGas(string i_LicenseNumber, eGasType i_GasType, string i_LiterToAdd)
        {
            ValidateIsGasVehicle(i_LicenseNumber);

            GasEngine gasEngine = (GasEngine)m_GarageVehicles[i_LicenseNumber].Vehicle.Engine;
            if (gasEngine.GasType != i_GasType)
            {
                string errorMessage = string.Format("The vehicle {0} not support gas type {1}, the gas type of the vehicle is {2}",
                    i_LicenseNumber, i_GasType, gasEngine.GasType);

                throw new RequiredValueException(gasEngine.GasType.ToString(), i_GasType.ToString(), GasEngine.k_GasTypeFieldName);
            }

            fillEnergy(i_LicenseNumber, i_LiterToAdd);
        }

        public void ChargeBattery(string i_LicenseNumber, string i_HoursToAdd)
        {
            ValidateVehicleIsElectric(i_LicenseNumber);
            fillEnergy(i_LicenseNumber, i_HoursToAdd);
        }

        public void fillEnergy(string i_LicenseNumber, string i_EnergyToTadd)
        {
            ValidateIsVehicleExists(i_LicenseNumber);
            
            float energyToAdd;
            if (!float.TryParse(i_EnergyToTadd, out energyToAdd))
            {
                throw new FormatException(string.Format("Invalid format. the value to add must be a float", energyToAdd));
            }

            Vehicle vehicle = m_GarageVehicles[i_LicenseNumber].Vehicle;
            GasEngine engine = vehicle.Engine as GasEngine;
            vehicle.Engine.FillEnergy(energyToAdd);
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
            vehicleInfo.AppendLine("Status Info:");
            vehicleInfo.AppendLine(string.Format("Vehicle Status: {0}", m_GarageVehicles[i_LicenseNumber].Status));

            return vehicleInfo;
        }


        public bool IsExistsVehicle(string i_LicenseNumber)
        {
            return m_GarageVehicles.ContainsKey(i_LicenseNumber);
        }

        public void ChangeVehicleStatus(string i_LicenseNumber, eVehicleStatus i_VehicleStatus)
        {
            Validator.ValidateNotNullOrWhiteSpace(i_LicenseNumber, Vehicle.k_LicenseNumberFieldName);
            ValidateIsVehicleExists(i_LicenseNumber);

            m_GarageVehicles[i_LicenseNumber].Status = i_VehicleStatus;
        }

        public void ValidateIsVehicleExists(string i_LicenseNumber)
        {
            Validator.ValidateNotNullOrWhiteSpace(i_LicenseNumber, Vehicle.k_LicenseNumberFieldName);

            if (!IsExistsVehicle(i_LicenseNumber))
            {
                throw new VehicleNotExistsException(i_LicenseNumber);
            }
        }
        public void ValidateVehicleIsElectric(string i_LicenseNumber)
        {
            validateEngineType(i_LicenseNumber, typeof(ElectricEngine));
        }

        internal void ValidateIsGasVehicle(string i_LicenseNumber)
        {
            validateEngineType(i_LicenseNumber, typeof(GasEngine));
        }

        private void validateEngineType(string i_LicenseNumber, Type i_EngineType)
        {
            ValidateIsVehicleExists(i_LicenseNumber);
            Engine engine = m_GarageVehicles[i_LicenseNumber].Vehicle.Engine;

            if (engine.GetType() != i_EngineType)
            {
                throw new RequiredValueException(engine.GetType().Name, i_EngineType.Name, Engine.k_EngineTypeFieldName);
            }
        }

        private IDictionary<string, GarageVehicle> m_GarageVehicles;

        public IEnumerable<string> GetSupportedVehicle()
        {
            return CarFactory.SupportedVehicle;
        }

        public Vehicle CreateVehicle(string i_LicenseNumber, string i_VehicleTypeName)
        {
            return CarFactory.CreateVehicle(i_LicenseNumber, i_VehicleTypeName);
        }
    }
}
