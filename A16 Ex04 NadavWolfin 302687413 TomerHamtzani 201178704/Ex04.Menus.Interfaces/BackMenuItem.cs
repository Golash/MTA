using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    /// <summary>
    /// Menu that represent a moving to previous menu operation
    /// </summary>
    internal class BackMenuItem : MenuItem
    {
        /// <summary>
        /// Create a new instance of <see cref="BackMenuItem"/>
        /// </summary>
        /// <param name="i_MenuToBack">The menu to return to</param>
        public BackMenuItem(MenuItem i_MenuToBack) : base("Back")
        {
            m_MenuToBack = i_MenuToBack;
        }

        /// <summary>
        /// Override the <see cref="MenuItem.GetSelectedMenuItem"/> of the <see cref="MenuItem"/> class
        /// The method will return the <seealso cref="MenuItem"/> that need to back to
        /// </summary>
        /// <returns></returns>
        internal override MenuItem GetSelectedMenuItem()
        {
            return m_MenuToBack;
        }

        /// <summary>
        /// Indicate if the current menu is an action menu, Action menu is an action that response to action and dosen't contain sub menus
        /// Always return false.
        /// </summary>
        public override bool IsAction
        {
            get
            {
                return false;
            }
        }

        private MenuItem m_MenuToBack;
    }
}
