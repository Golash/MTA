using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI.Operations
{
    /// <summary>
    /// This operation given the user the option to exit from the garage managment UI.
    /// The exit operation keep the same interface like all other operations
    /// </summary>
    class ExitOperation : UserOperation
    {
        /// <summary>
        /// Create a new instance of the operation: Exit Operation
        /// </summary>
        /// <param name="manager">The instance of the garage manger to make the operation on</param>
        public ExitOperation() : base(null, "ExitOperation", "Exit")
        {

        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
