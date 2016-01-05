using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex04.Menus.Delegates;

namespace Ex04.Menus.Delegates.Actions
{
    public class ShowTimeAction : MenuItemActionBase
    {
        /// <summary>
        /// Create a new instance of <see cref="ShowTimeAction"/>
        /// </summary>
        public ShowTimeAction() :
            base(new List<MenuItem>() { new MenuItem("Show Time") })
        {
        }

        /// <summary>
        /// Execute the <see cref="ShowDateAction"/> action.
        /// The actions will display the current time
        /// </summary>
        public override void Execute()
        {
            Console.WriteLine("The current time is: {0}", DateTime.Now.ToShortTimeString());
        }
    }
}
