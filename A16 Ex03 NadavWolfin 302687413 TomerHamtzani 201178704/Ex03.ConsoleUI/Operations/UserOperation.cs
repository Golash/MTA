using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI.Operations
{
    internal abstract class UserOperation
    {
        public UserOperation(GarageManager manager, string name, string displayName)
        {
            m_GarageManager = manager;
            m_Name = name;
            m_DisplayName = displayName;
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

        public string Name 
        {
            get
            {
                return m_Name;
            }
            protected set
            {
                m_Name = value;
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
