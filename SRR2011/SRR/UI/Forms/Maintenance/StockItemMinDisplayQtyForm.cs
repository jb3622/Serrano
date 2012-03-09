using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Disney.iDash.Shared;
using Disney.iDash.LocalData;
using Disney.iDash.SRR.BusinessLayer;

namespace Disney.iDash.SRR.UI.Forms.Maintenance
{
    public partial class StockItemMinDisplayQtyForm : Disney.iDash.SRR.UI.Forms.Common.BaseDataForm
    {
        private StockInfo _stockInfo = new StockInfo();

        #region Public methods
        //-----------------------------------------------------------------------------------------
        public StockItemMinDisplayQtyForm()
        {
            InitializeComponent();
        }

		public void Setup()
		{
			viewItems.Appearance.EvenRow.BackColor = Properties.Settings.Default.EvenRowBackColor;
			viewItems.Appearance.OddRow.BackColor = Properties.Settings.Default.OddRowBackColor;
			viewItems.Appearance.FocusedRow.BackColor = Properties.Settings.Default.FocusedRowBackColor;

			colMinDispQty.AppearanceCell.BackColor = Properties.Settings.Default.EditableColumnBackcolor;

			cboDept.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
			{
				ErrorDialog.Show(ex, extraInfo, terminateApplication);
			});

			_stockInfo.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
			{
				ErrorDialog.Show(ex, extraInfo, terminateApplication);
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
            RefreshItemCombo();
            this.Show();
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region Private event handlers
        //-----------------------------------------------------------------------------------------
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

        private void cboDept_EditValueChanged(object sender, EventArgs e)
        {
            RefreshItemCombo();
            RefreshButtons();
        }

        private void rbUPC_CheckedChanged(object sender, EventArgs e)
        {
            RefreshControls();
            RefreshButtons();
        }

        private void rbStockItem_CheckedChanged(object sender, EventArgs e)
        {
            RefreshControls();
            RefreshButtons();
        }

        private void ctrlItem_EditValueChanged(object sender, EventArgs e)
        {
            RefreshButtons();
        }

        private void txtUPC_EditValueChanged(object sender, EventArgs e)
        {
            RefreshButtons();
        }

        private void spinMinDisplayQty_EditValueChanged(object sender, EventArgs e)
        {
            RefreshButtons();
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
        //-----------------------------------------------------------------------------------------
        #endregion

        #region Overrides
        //-----------------------------------------------------------------------------------------
        internal override void Clear()
        {
            cboDept.EditValue = null;
            ctrlItem.Clear();
            txtUPC.EditValue = null;
            spinMinDisplayQty.Value = 0;
            gridItems.DataSource = null;
            gridItems.Visible = false;
        }

        internal override void RefreshButtons()
        {
            btnSave.Enabled = base.FormState == FormStates.Dirty;
            btnCancel.Enabled = base.FormState != FormStates.Idle;

            btnSearch.Enabled = base.FormState == FormStates.Idle &&
                ((cboDept.EditValue != null && Convert.ToDecimal(cboDept.EditValue) > 0) 
                || (rbStockItem.Checked && ctrlItem.HasValue) 
                || (rbUPC.Checked && txtUPC.EditValue != null && txtUPC.EditValue.ToString() != string.Empty) 
                || spinMinDisplayQty.Value != 0);

			cboDept.Enabled = base.FormState == FormStates.Idle;
			rbStockItem.Enabled = base.FormState == FormStates.Idle;
			rbUPC.Enabled = base.FormState == FormStates.Idle;
			ctrlItem.Enabled = base.FormState == FormStates.Idle && rbStockItem.Checked;
			txtUPC.Enabled = base.FormState == FormStates.Idle && rbUPC.Checked;
			spinMinDisplayQty.Enabled = base.FormState == FormStates.Idle;
        }

        internal override bool Save()
        {
            base.ShowSavingMessage();
            var saved = _stockInfo.SaveMinDisplayQuantities();

            if (saved)
                base.FormState = FormStates.Idle;
            
            this.Cursor = Cursors.Default;
            base.ClearStatusMessage();
            return saved;
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region Private methods
        //-----------------------------------------------------------------------------------------
        private void RefreshControls()
        {
            txtUPC.Enabled = rbUPC.Checked;
            if (!rbUPC.Checked)
                txtUPC.EditValue = null;

            ctrlItem.Enabled = rbStockItem.Checked;
            if (!rbStockItem.Checked)
                ctrlItem.Clear();
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

        private void RefreshDeptCombo()
        {
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            cboDept.WhereClause = string.Format("USUSPR = '{0}'", Session.User.NetworkId.ToUpper());
            cboDept.RefreshControl();

            this.Cursor = Cursors.Default;
        }
        
        private void RefreshGrid()
        {
            viewItems.ClearColumnsFilter();
            viewItems.ClearGrouping();
            viewItems.ClearSorting();

            base.ShowSearchingMessage();
            
            if (ctrlItem.HasValue)
                gridItems.DataSource = _stockInfo.GetMinDisplayQuantities(cboDept.EditValue, ctrlItem.StockItem, spinMinDisplayQty.Value);
            else
                gridItems.DataSource = _stockInfo.GetMinDisplayQuantities(cboDept.EditValue, txtUPC.EditValue, spinMinDisplayQty.Value);
            
            if (viewItems.RowCount == 0)
            {
                gridItems.Visible = false;
                base.ShowNotFoundMessage();
                base.FormState = FormStates.Idle;
            }
            else
            {
                gridItems.Visible = true;
                viewItems.BestFitColumns();
                base.ClearStatusMessage();
                base.FormState = FormStates.Clean;
            }
        }

        private void riMinDispQty_Spin(object sender, DevExpress.XtraEditors.Controls.SpinEventArgs e)
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
        //-----------------------------------------------------------------------------------------
        #endregion


    }
}
