/*
 * Author:  Marc Levine
 * Project: SRR
 * Date:    From March 2011
 * 
 * Description
 * This form allows the user to allocate a stock item to a store.
 */
using System;
using Disney.iDash.SRR.BusinessLayer;
using Disney.iDash.Shared;
using System.Drawing;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using Disney.iDash.LocalData;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils;

namespace Disney.iDash.SRR.UI.Forms.Maintenance
{
    public partial class StockItemStoreAssignmentForm : Disney.iDash.SRR.UI.Forms.Common.BaseDataForm
    {
        private StockInfo _stockInfo = new StockInfo();

        #region Public methods
        //-----------------------------------------------------------------------------------------
        public StockItemStoreAssignmentForm()
        {
            InitializeComponent();
        }

		public void Setup()
		{
			viewItems.Appearance.OddRow.BackColor = Properties.Settings.Default.OddRowBackColor;
			viewItems.Appearance.EvenRow.BackColor = Properties.Settings.Default.EvenRowBackColor;
			viewItems.Appearance.FocusedRow.BackColor = Properties.Settings.Default.FocusedRowBackColor; 
			colOpeningROS.AppearanceCell.BackColor = Properties.Settings.Default.EditableColumnBackcolor;

			cboStore.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
			{
				ErrorDialog.Show(ex, extraInfo, terminateApplication);
			});

			_stockInfo.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
			{
				ErrorDialog.Show(ex, extraInfo, terminateApplication);
			});

			_stockInfo.ExceptionHandler.AlertEvent += ((message, caption, alertType) =>
			{
				ErrorDialog.ShowAlert(message, caption, alertType);
			});

			_stockInfo.ChangedEvent += ((sender, e) =>
			{
				base.FormState = FormStates.Dirty;
			});
		}

        public void ShowForm()
        {
            this.MdiParent = FormUtils.FindMdiParent();
            RefreshDeptCombo();
            RefreshStoreCombo();
            this.Show();
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region Private event handlers
        //-----------------------------------------------------------------------------------------
        private void cboDept_EditValueChanged(object sender, EventArgs e)
        {
            RefreshButtons();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.DiscardChanges();
        }

        private void viewItems_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
			base.CellHighLights.SetRowCellStyle(viewItems, e, Color.Black, Properties.Settings.Default.ChangedCellForeColor);
        }

        private void viewItems_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (this.Visible)
            {
				base.CellHighLights.CellValueChanged(viewItems, e);
                base.FormState = FormStates.Dirty;
            }
        }

        private void ctrlItem_EditValueChanged(object sender, EventArgs e)
        {
            RefreshButtons();
        }

        private void cboStore_EditValueChanged(object sender, EventArgs e)
        {
            RefreshButtons();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var frm = new Forms.Maintenance.StockItemStoreAssignmentEditForm();
            frm.ShowForm(this, (decimal) cboDept.EditValue);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ArrayList rows = new ArrayList();

            if (Question.YesNo("Mark/Unmark selected rows as deleted?", this.Text))
            {
                for (int i = 0; i < viewItems.SelectedRowsCount; i++)
			    {
			        if (viewItems.GetSelectedRows()[i] >= 0)
	                {
		                rows.Add(viewItems.GetDataRow(viewItems.GetSelectedRows()[i]));
	                }
			    }

                try 
	            {	        
		            viewItems.BeginUpdate();
                    for (int i = 0; i < rows.Count; i++)
			        {
			            DataRow row = rows[i] as DataRow;
                        if ((int)row["DeleteFlag"] == 1)
                        {
                            row["DeleteFlag"] = 0; 
                        }
                        else
                        {
                            row["DeleteFlag"] = 1;
                        }
			        }
	            }
                finally 
                {
                    viewItems.EndUpdate();
                }

                viewItems.ClearSelection();

                gridItems.RefreshDataSource();
            }
        }

        private void viewItems_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            btnDelete.Enabled = true;
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region Overrides
        //-----------------------------------------------------------------------------------------
        internal override void Clear()
        {
            ctrlItem.Clear();
            cboStore.EditValue = null;
            gridItems.DataSource = null;
            gridItems.Visible = false;
            //btnAdd.Enabled = true;
			btnDelete.Enabled = false;
        }

        internal override void RefreshButtons()
        {
            btnSave.Enabled = base.FormState == FormStates.Dirty;
            btnCancel.Enabled = base.FormState != FormStates.Idle;
            btnSearch.Enabled = base.FormState == FormStates.Idle && cboDept.EditValue != null && (ctrlItem.HasValue || cboStore.EditValue != null);
			btnAdd.Enabled = cboDept.EditValue != null;

			cboDept.Enabled = base.FormState == FormStates.Idle;
			ctrlItem.Enabled = base.FormState == FormStates.Idle;
			cboStore.Enabled = base.FormState == FormStates.Idle;
        }

        internal override bool Save()
        {
            base.ShowSavingMessage();
            var saved = _stockInfo.SaveItemDetails();

			if (saved)
				base.FormState = FormStates.Idle;
			else
				gridItems.RefreshDataSource();
			
            this.Cursor = Cursors.Default;
            base.ClearStatusMessage();
            return saved;
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region Private methods
        //-----------------------------------------------------------------------------------------
        private void RefreshDeptCombo()
        {
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            cboDept.WhereClause = string.Format("USUSPR = '{0}'", Session.User.NetworkId.ToUpper());
            cboDept.RefreshControl();

            this.Cursor = Cursors.Default;
        }

        private void RefreshItemCombo()
        {
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            if (cboDept.EditValue == null)
                ctrlItem.DepartmentId = -1;
            else
                ctrlItem.DepartmentId = (decimal)cboDept.EditValue;

            ctrlItem.RefreshCombos();

            this.Cursor = Cursors.Default;
        }

        private void RefreshStoreCombo()
        {
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            cboStore.RefreshControl();

            this.Cursor = Cursors.Default;
        }

        public void RefreshGrid()
        {
            viewItems.ClearColumnsFilter();
            viewItems.ClearGrouping();
            viewItems.ClearSorting();

            base.ShowSearchingMessage();

            gridItems.DataSource = _stockInfo.GetItemDetails((decimal)cboDept.EditValue, cboStore.EditValue, ctrlItem.StockItem);

            if (viewItems.RowCount == 0)
            {
                gridItems.Visible = false;
                base.ShowNotFoundMessage();
                base.FormState = FormStates.Idle;
            }
            else
            {
                gridItems.Visible = true;
                if (chkSizeColumns.Checked)
                {
                    viewItems.BestFitColumns();    
                }                
                base.ClearStatusMessage();
                base.FormState = FormStates.Clean;
            }
            btnAdd.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void riOpeningRateOfSale_Spin(object sender, DevExpress.XtraEditors.Controls.SpinEventArgs e)
        {
            e.Handled = true;
        }

        private void viewItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                SendKeys.Send("{DOWN}");
            }
        }

        private void cboDept_Leave(object sender, EventArgs e)
        {
            RefreshItemCombo();
        }

        //-----------------------------------------------------------------------------------------
        #endregion
    }

}
