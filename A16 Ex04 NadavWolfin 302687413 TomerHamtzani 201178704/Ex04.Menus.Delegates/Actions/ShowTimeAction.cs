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
        public ShowTimeAction() :
            base(new List<MenuItem>() { new MenuItem("Show Time") })
        {
        }

        public override void Execute()
        {
            Console.WriteLine("The current time is: {0}", DateTime.Now.ToShortTimeString());
        }
    }
}
