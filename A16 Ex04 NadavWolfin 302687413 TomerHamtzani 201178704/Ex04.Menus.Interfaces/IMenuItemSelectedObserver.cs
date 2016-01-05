using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    /// <summary>
    /// Interface that represent an observer for menu selection.
    /// </summary>
    public interface IMenuItemSelectedObserver
    {
        /// <summary>
        /// The function that will called when a menu is selected
        /// </summary>
        /// <param name="i_MenuItem">The menu that was selected</param>
        void ReportSelect(MenuItem i_MenuItem);
    }
}
