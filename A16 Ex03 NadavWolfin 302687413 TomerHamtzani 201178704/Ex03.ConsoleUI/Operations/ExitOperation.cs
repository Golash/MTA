using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI.Operations
{
    class ExitOperation : UserOperation
    {
        public ExitOperation() : base(null,"Exit","Exit")
        {

        }
        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
