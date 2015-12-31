using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI.Operations
{
    internal class VehicleDetailsOperation : UserOperation
    {
        public VehicleDetailsOperation(GarageManager i_Manager) : 
            base(i_Manager, "VehicleDetailsOperation", "Display Vehicle Details")
        {
        }

        public override void Execute()
        {
            Console.Write("Insert vehicle license number: ");
            string licenseNumber = Console.ReadLine();
            Console.WriteLine(m_GarageManager.GetVehicleInfo(licenseNumber));
        }
    }
}
