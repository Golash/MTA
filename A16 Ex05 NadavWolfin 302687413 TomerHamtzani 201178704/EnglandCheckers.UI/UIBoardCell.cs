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
            Enabled = i_BoardCell.Enabled;
            BackColor = Enabled ? r_DefaultColor : r_DisableColor;

            r_BoardCell = i_BoardCell;
            r_BoardCell.BoardCellChanged += m_BoardCell_BoardCellChanged;

            Size = new Size(k_CellSideSize, k_CellSideSize);
            Margin = new Padding(0);

            if (Enabled)
            {
                updateText();
                TextAlign = ContentAlignment.MiddleCenter;
                Click += boardButton_Click;
                IsSelected = false;
            }
        }

        private void m_BoardCell_BoardCellChanged(object obj, EventArgs e)
        {
            updateCell();
        }

        private void updateCell()
        {
            updateText();
            updateColor();
        }

        private void updateColor()
        {
            BackColor = IsSelected ? r_SelectedColor : r_DefaultColor;
        }

        private void updateText()
        {
            Text = r_BoardCell.Coin != null ? r_BoardCell.Coin.ToString() : string.Empty;
        }

        public BoardPoint BoardPoint
        {
            get
            {
                return r_BoardCell.BoardPoint;
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
        private readonly BoardCell r_BoardCell;
        internal const int k_CellSideSize = 50;
    }
}
