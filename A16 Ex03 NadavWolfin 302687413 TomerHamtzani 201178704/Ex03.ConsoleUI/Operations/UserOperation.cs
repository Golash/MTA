using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI.Operations
{
    internal abstract class UserOperation
    {
        public UserOperation(GarageManager i_Manager, string i_Name, string i_DisplayName)
        {
            m_GarageManager = i_Manager;
            m_Name = i_Name;
            m_DisplayName = i_DisplayName;
        }

        public string DisplayName
        {
            get
            {
                return m_DisplayName;
            }

            set
            {
                m_DisplayName = value;
            }
        }

        public string ReadLicenseNumber()
        {
            Console.Write("Insert license number: ");
            return Console.ReadLine();
        }

        public abstract void Execute();

        protected string m_Name;
        protected string m_DisplayName;
        protected GarageManager m_GarageManager;
    }
}
