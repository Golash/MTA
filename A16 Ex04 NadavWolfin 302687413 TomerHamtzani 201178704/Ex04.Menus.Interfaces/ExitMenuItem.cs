using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    internal class ExitMenuItem : MenuItem
    {
        public ExitMenuItem() : base("Exit")
        {
        }

        internal override MenuItem GetSelectedMenuItem()
        {
            throw new InvalidOperationException("Exit menu item hasn't sub menu");
        }

        public override bool IsAction
        {
            get
            {
                return false;
            }
        }
    }
}
