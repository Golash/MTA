using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    static class Factory
    {
        static Vehicle CreateVehicle(Type i_VehicleType)
        {
            Vehicle vehicle = null;

            //switch (i_VehicleType)
            //{
            //    case typeof(ElectricCar):
            //        vehicle = new ElectricCar();
            //        break;
            //    case typeof(RegularCar):
            //        vehicle = new RegularCar();
            //        break;
            //    case typeof(ElectricMotorcycle):
            //        vehicle = new ElectricMotorcycle();
            //        break;
            //    case typeof(RegularMotorcycle):
            //        vehicle = new RegularMotorcycle();
            //        break;
            //    case typeof(Truck):
            //        vehicle = new Truck();
            //        break;
            //    default:
            //        throw new Exception("Unknow Vehicle Type");
            //}

            return vehicle;
        }


        static List<Type> m_SupportedVehicle = new List<Type>
        { typeof(ElectricCar), typeof(RegularCar), typeof(ElectricMotorcycle), typeof(RegularMotorcycle), typeof(Truck) };
    }

    
}
