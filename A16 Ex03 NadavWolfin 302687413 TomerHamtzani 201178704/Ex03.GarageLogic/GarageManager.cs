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
            m_VehiclesInGarage = new Dictionary<string, VehicleInGarage>();
        }

        public Vehicle AddNewVehicle(string i_LicenseNumber)
        {
            return null;
        }

        public List<string> GetLicenseNumber()
        {
            List<string> licenseNumbers = new List<string>();

            foreach (string licenseNumber in m_VehiclesInGarage.Keys)
            {
                licenseNumbers.Add(licenseNumber);
            }

            return licenseNumbers;
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

        private Dictionary<string, VehicleInGarage> m_VehiclesInGarage;
    }
}
