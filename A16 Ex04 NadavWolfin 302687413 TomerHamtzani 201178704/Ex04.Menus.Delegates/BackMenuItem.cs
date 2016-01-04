using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Delegates
{
    internal class BackMenuItem : MenuItem
    {
        public BackMenuItem(MenuItem i_MenuToBack, MenuItem i_Parent) : base("Back")
        {
            m_MenuToBack = i_MenuToBack;
        }

        internal override MenuItem GetSelectedMenuItem()
        {
            return m_MenuToBack;
        }

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
