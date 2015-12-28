using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Exceptions
{
    public class InvalidEngineTypeException : Exception
    {
        public InvalidEngineTypeException()
        {

        }
        public InvalidEngineTypeException(string message) : base(message)
        {

        }
    }
}
