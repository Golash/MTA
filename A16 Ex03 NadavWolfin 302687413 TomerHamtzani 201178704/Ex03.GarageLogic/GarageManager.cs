using Ex03.GarageLogic.Exceptions;
using Ex03.GarageLogic.Factory;
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
                throw new ArgumentException("Vehicle with the license number: '{0}' already exists", licenseNumber);
            }

            GarageVehicle garageVehicle = new GarageVehicle(i_Vehicle, i_VehicleOwnerPhoneDetails, eVehicleStatus.Repairing);
            m_GarageVehicles.Add(licenseNumber, garageVehicle);
        }

        public List<string> GetLicenseNumber()
        {
            return m_GarageVehicles.Keys.ToList();
        }

        public List<string> GetLicenseNumber(eVehicleStatus i_VehicleStatus)
        {
            return null;
        }

        public void FillAirInWheelsToMax(string i_LicenseNumber)
        {

        }

        public void FillGas(string i_LicenseNumber, eGasType i_GasType, float i_LiterToAdd)
        {

        }

        public void ChargeBattery(string i_LicenseNumber, float i_MinutesToCharge)
        {

        }

        public string GetVehicleInfo(string i_LicenseNumber)
        {
            return string.Empty;
        }


        public bool IsExists(string licenseNumber)
        {
            return m_GarageVehicles.ContainsKey(licenseNumber);
        }

        public void ChangeVehicleStatus(string licenseNumber, eVehicleStatus eVehicleStatus)
        {
            if (!m_GarageVehicles.ContainsKey(licenseNumber))
            {
                throw new ArgumentException("Vehicle with the license number: '{0}' not exists", licenseNumber);
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
