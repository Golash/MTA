using EnglandCheckers.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace EnglandCheckers.UI
{
    public delegate void CellSelected(object obj, EventArgs e);

    internal class UIBoardCell : Button
    {
        public UIBoardCell(bool i_Enabled, Coin i_Coin, Components.BoardCell i_BoardCell)
        {
            this.Enabled = i_Enabled;
            this.BackColor = k_DefaultColor;
            m_Coin = i_Coin;
            m_BoardCell = i_BoardCell;

            this.Size = new Size(k_CellSideSize, k_CellSideSize);
            this.Margin = new Padding(0);

            if (this.Enabled)
            {
                updateText();
                this.TextAlign = ContentAlignment.MiddleCenter;
                this.Click += BoardButton_Click;
                m_IsSelected = false;
            }
        }
        
        public Coin Coin
        {
            get
            {
                return m_Coin;
            }
            set
            {
                m_Coin = value;
            }
        }

        public void UpdateCell()
        {
            updateText();
            updateColor();
        }

        private void updateColor()
        {
            this.BackColor = m_IsSelected ? k_SelectedColor : k_DefaultColor;
        }

        private void updateText()
        {
            this.Text = m_Coin != null ? Coin.ToString() : string.Empty;
        }

        public BoardCell BoardCell
        {
            get
            {
                return m_BoardCell;
            }
            set
            {
                m_BoardCell = value;
            }
        }

        public bool IsSelected
        {
            get
            {
                return m_IsSelected;
            }
            set
            {
                m_IsSelected = value;
            }
        }

        private void BoardButton_Click(object sender, EventArgs e)
        {
            m_IsSelected = !m_IsSelected;

            updateColor();

            if (CellSelected != null)
            {
                CellSelected.Invoke(this, EventArgs.Empty);
            }
        }

        private bool m_IsSelected;
        private readonly System.Drawing.Color k_SelectedColor = System.Drawing.Color.LightBlue;
        private readonly System.Drawing.Color k_DefaultColor = System.Drawing.Color.Transparent;
        private Coin m_Coin;
        internal const int k_CellSideSize = 55;
        public event CellSelected CellSelected;
        private BoardCell m_BoardCell;
    }
}
