using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic.Helpers;
using Ex03.GarageLogic.Exceptions;
using Ex03.GarageLogic.Vehicles;

namespace Ex03.GarageLogic.Factory
{
    internal class CarFactory
    {
        static CarFactory()
        {
            LoadSupportedVehicle();
        }

        private static void LoadSupportedVehicle()
        {
            // TODO add gas type
            m_SupportedVehicle = new Dictionary<string, Type>();
            m_SupportedVehicle.Add("Regular Motorcycle", typeof(RegularMotorcycle));
            m_SupportedVehicle.Add("Electric Motorcycle", typeof(ElectricMotorcycle));
            m_SupportedVehicle.Add("Truck", typeof(Truck));
            m_SupportedVehicle.Add("Electric Car", typeof(ElectricCar));
            m_SupportedVehicle.Add("Regular Car", typeof(RegularCar));
        }

        public static IEnumerable<string> SupportedVehicle
        {
            get
            {
                // Return a copy of the supported vehicle
                return m_SupportedVehicle.Keys;
            }
        }

        private static Dictionary<string, Type> m_SupportedVehicle;

        public static Vehicle CreateVehicle(string i_LicenseNumber, string i_VehicleTypeName)
        {
            if (!m_SupportedVehicle.ContainsKey(i_VehicleTypeName))
            {
                string errorMessage = string.Format("There is no vehicle type: '{0}'", i_VehicleTypeName);
                throw new ArgumentException(errorMessage);
            }   

            Vehicle vehicle = null;
            Type vehicleType = m_SupportedVehicle[i_VehicleTypeName];
            if (vehicleType == typeof(RegularCar))
            {
                vehicle = new RegularCar(i_LicenseNumber);
            }
            else if (vehicleType == typeof(ElectricCar))
            {
                vehicle = new ElectricCar(i_LicenseNumber);
            }

            if (vehicleType == typeof(RegularMotorcycle))
            {
                vehicle = new RegularMotorcycle(i_LicenseNumber);
            }
            else if (vehicleType == typeof(ElectricMotorcycle))
            {
                vehicle = new ElectricMotorcycle(i_LicenseNumber);
            }
            else if (vehicleType == typeof(Truck))
            {
                vehicle = new Truck(i_LicenseNumber);
            } 
            
            return vehicle;
        }
    }
}
