using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    /// <summary>
    /// Menu that represent a exit from the menu UI operation
    /// </summary>
    internal class ExitMenuItem : MenuItem
    {
        /// <summary>
        /// Create a new instance of <see cref="ExitMenuItem"/>
        /// </summary>
        public ExitMenuItem() : base("Exit")
        {
        }

        /// <summary>
        /// This function is not need to be called because a <see cref="ExitMenuItem"/> has no menu to select from
        /// Always throw an <see cref="InvalidOperationException"/>
        /// </summary>
        /// <returns>throw <see cref="InvalidOperationException"/></returns>
        internal override MenuItem GetSelectedMenuItem()
        {
            throw new InvalidOperationException("Exit menu item hasn't sub menu");
        }

        /// <summary>
        /// Always return false.
        /// Indicate is the current menu is an action menu, Action menu is an action that response to action and dosen't contain sub menus
        /// </summary>
        public override bool IsAction
        {
            get
            {
                return false;
            }
        }
    }
}
