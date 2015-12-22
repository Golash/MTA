using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI.Operations
{
    internal abstract class UserOperation
    {
        public UserOperation(string name, string displayName)
        {
            
            _Name = name;
            _DisplayName = displayName;
        }

        public string DisplayName
        {
            get
            {
                return _DisplayName;
            }
            set
            {
                _DisplayName = value;
            }
        }

        public string Name 
        {
            get
            {
                return _Name;
            }
            protected set
            {
                _Name = value;
            }
        }

        public abstract void Execute();

        protected string _Name;
        protected string _DisplayName;
    }
}
