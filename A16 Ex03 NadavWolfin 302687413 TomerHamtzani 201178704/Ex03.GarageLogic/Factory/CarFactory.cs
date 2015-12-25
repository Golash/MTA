using Ex03.GarageLogic.Helpers;
using Ex03.GarageLogic.Exceptions;
using Ex03.GarageLogic.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            m_SupportedVehicle = new Dictionary<string,Type>();
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

        // TODO: Dictionary
        private static Dictionary<string,Type> m_SupportedVehicle;



        public static Vehicle CreateVehicle(string licenseNumber, string modelName, string vehicleTypeName)
        {
            if (!m_SupportedVehicle.ContainsKey(vehicleTypeName))
            {
                throw new NotSupportedVehicleType(vehicleTypeName);
            }

            Vehicle vehicle = null;
            Type vehicleType = m_SupportedVehicle[vehicleTypeName];
            if (vehicleType == typeof(RegularCar))
            {
                vehicle = new RegularCar(licenseNumber, modelName);
            }
            else if (vehicleType == typeof(ElectricCar))
            {
                vehicle = new ElectricCar(licenseNumber, modelName);
            }
            if (vehicleType == typeof(RegularMotorcycle))
            {
                vehicle = new RegularMotorcycle(licenseNumber, modelName);
            }
            else if (vehicleType == typeof(ElectricMotorcycle))
            {
                vehicle = new ElectricMotorcycle(licenseNumber, modelName);
            }
            else if (vehicleType == typeof(Truck))
            {
                vehicle = new Truck(licenseNumber, modelName);
            } 
            
            return vehicle;
        }
    }
}
