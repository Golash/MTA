using System;
using System.Windows.Forms;
using System.Drawing;
using EnglandCheckers.Components;

namespace EnglandCheckers.UI
{
    internal class UIBoardCell : Button
    {
        public UIBoardCell(BoardCell i_BoardCell)
        {
            this.Enabled = i_BoardCell.Enabled;
            this.BackColor = this.Enabled ? r_DefaultColor : r_DisableColor;

            m_BoardCell = i_BoardCell;
            m_BoardCell.BoardCellChanged += m_BoardCell_BoardCellChanged;

            this.Size = new Size(k_CellSideSize, k_CellSideSize);
            this.Margin = new Padding(0);

            if (this.Enabled)
            {
                updateText();
                this.TextAlign = ContentAlignment.MiddleCenter;
                this.Click += boardButton_Click;
                IsSelected = false;
            }
        }

        private void m_BoardCell_BoardCellChanged(object obj, EventArgs e)
        {
            UpdateCell();
        }

        public void UpdateCell()
        {
            updateText();
            updateColor();
        }

        private void updateColor()
        {
            this.BackColor = IsSelected ? r_SelectedColor : r_DefaultColor;
        }

        private void updateText()
        {
            this.Text = m_BoardCell.Coin != null ? m_BoardCell.Coin.ToString() : string.Empty;
        }

        public BoardPoint BoardPoint
        {
            get
            {
                return m_BoardCell.BoardPoint;
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
                updateColor();
            }
        }

        private void boardButton_Click(object sender, EventArgs e)
        {
            IsSelected = !IsSelected;
        }

        private bool m_IsSelected;
        private readonly Color r_SelectedColor = Color.LightBlue;
        private readonly Color r_DefaultColor = Color.Transparent;
        private readonly Color r_DisableColor = Color.LightGray;
        private BoardCell m_BoardCell;
        internal const int k_CellSideSize = 50;
    }
}
