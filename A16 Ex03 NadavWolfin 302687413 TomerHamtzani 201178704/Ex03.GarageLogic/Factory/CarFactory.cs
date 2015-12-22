using Ex03.GarageLogic.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Factory
{
    public static class CarFactory
    {
        static CarFactory()
        {
            m_SupportedVehicle = LoadSupportedVehicle();
        }

        private static IEnumerable<Type> LoadSupportedVehicle()
        {
            return Assembly.GetAssembly(typeof(CarFactory)).GetTypes()
           .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Vehicle)));
        }

        public static IEnumerable<Type> SupportedVehicle
        {
            get
            {
                // Return a copy of the supported vehicle
                return m_SupportedVehicle.ToList();
            }
        }

        static IEnumerable<Type> m_SupportedVehicle;


    }
}
