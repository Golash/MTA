using Ex03.GarageLogic.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class GarageVehicle
    {
        public GarageVehicle(Vehicle i_Vehicle, VehicleOwnerDetails i_VehicleOwnerDetails, eVehicleStatus i_VehicleStatus)
        {
            m_Vehicle = i_Vehicle;
            m_OwnerDetails = i_VehicleOwnerDetails;
            m_Status = i_VehicleStatus;
        }

        public eVehicleStatus Status
        {
            get
            {
                return m_Status;
            }
            set
            {
                m_Status = value;
            }
        }

        public VehicleOwnerDetails OwnerDetails
        {
            get
            {
                return m_OwnerDetails;
            }
            set
            {
                m_OwnerDetails = value;
            }
        }

        public Vehicle Vehicle
        {
            get
            {
                return m_Vehicle;
            }
            set
            {
                m_Vehicle = value;
            }
        }
        private Vehicle m_Vehicle;
        private VehicleOwnerDetails m_OwnerDetails;
        private eVehicleStatus m_Status;
    }
}
