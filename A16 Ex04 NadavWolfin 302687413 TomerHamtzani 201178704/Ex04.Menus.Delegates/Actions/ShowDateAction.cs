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
        public ShowDateAction() :
            base(new List<MenuItem>() { new MenuItem("Show Date") })
        {
        }

        public override void Execute()
        {
            Console.WriteLine("The current data is: {0}", DateTime.Now.ToShortDateString());
        }
    }
}
