using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex04.Menus.Delegates;

namespace Ex04.Menus.Delegates.Actions
{
    public class ShowDateAction : MenuItemActionBase
    {
        /// <summary>
        /// Create a new instance of <see cref="ShowDateAction"/>
        /// </summary>
        public ShowDateAction() :
            base(new List<MenuItem>() { new MenuItem("Show Date") })
        {
        }

        /// <summary>
        /// Execute the <see cref="ShowDateAction"/> action.
        /// The actions will display the current date
        /// </summary>
        public override void Execute()
        {
            Console.WriteLine("The current data is: {0}", DateTime.Now.ToShortDateString());
        }
    }
}
