/*
 * Author:  Marc Levine
 * Project: SRR
 * Date:    From March 2011
 * 
 * Description
 * Used to highlight (in red) any changes to a cell in a DevExpress grid.  
 * 
 * The methods
 *	SetRowCellStyle
 *	CellValueChanged
 *	
 * must be wired up to the appropriate event handler for the grid.
 *  
*/
using System.Collections.Generic;
using System.Drawing;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;

namespace Disney.iDash.Shared
{
    public class CellHighlighter
    {
        // List of cells (rows and columns) whose content has been modified.
        private List<Cell> _cells = new List<Cell>();

        public void Clear()
        {
            _cells.Clear();
        }

        // For cells that have been modified their row and column reference will be contained in
        // _cells.  If found then set thr fore colour of the cell to the highlight colour otherwise
        // set it to the default colour.
		public void SetRowCellStyle(DevExpress.XtraGrid.Views.Grid.GridView view, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e, Color defaultColor, Color highlightColor)
		{
			if (e.RowHandle != GridControl.InvalidRowHandle && e.RowHandle != GridControl.AutoFilterRowHandle && e.RowHandle != GridControl.NewItemRowHandle)
			{
				if (_cells.Find((c) => (c.ColumnName == e.Column.Name && c.RowIndex == view.GetDataSourceRowIndex(e.RowHandle))) == null)
				{
					e.Appearance.ForeColor = defaultColor;
					e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, e.Appearance.Font.Style | FontStyle.Regular);
				}
				else
				{
					e.Appearance.ForeColor = highlightColor;
					e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, e.Appearance.Font.Style | FontStyle.Bold);
				}
			}
		}

        // If a cell has been modified add a reference to it to _cells.
		public void CellValueChanged(DevExpress.XtraGrid.Views.Grid.GridView view, CellValueChangedEventArgs e, string overrideColumnName = "")
        {
			if (e.RowHandle != GridControl.InvalidRowHandle && e.RowHandle != GridControl.AutoFilterRowHandle && e.RowHandle != GridControl.NewItemRowHandle)
			{
                var columnName = (overrideColumnName == string.Empty ? e.Column.Name : overrideColumnName);
                var newCell = new Cell(view.GetDataSourceRowIndex(e.RowHandle), columnName);

				if (_cells.Find((c) => (c.ColumnName == newCell.ColumnName && c.RowIndex == newCell.RowIndex)) == null)
					_cells.Add(newCell);
			}
        }

    }

    class Cell
    {
        internal int RowIndex { get; set; }
        internal string ColumnName { get; set; }

        internal Cell(int rowIndex, string columnName)
        {
            RowIndex = rowIndex;
            ColumnName = columnName;
        }
    }
}
