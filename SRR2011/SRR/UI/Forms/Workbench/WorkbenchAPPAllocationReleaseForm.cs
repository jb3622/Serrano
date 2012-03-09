using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Disney.iDash.Shared;
using Disney.iDash.SRR.BusinessLayer;

namespace Disney.iDash.SRR.UI.Forms.Workbench
{
    public partial class WorkbenchAPPAllocationReleaseForm : Disney.iDash.SRR.UI.Forms.Common.BaseDataForm
    {
        private WorkbenchManualInfo _packItem = new WorkbenchManualInfo();
        private StockItem m_StockItem;

        #region Public methods
        //-----------------------------------------------------------------------------------------
        public bool RefreshParent { get; private set; }
        
		public WorkbenchAPPAllocationReleaseForm()
        {
            InitializeComponent();
            _packItem.ExceptionHandler.ExceptionEvent +=((ex, extraInfo, terminateApplication)=>
                {
                    ErrorDialog.Show(ex, extraInfo, terminateApplication);
                });

            _packItem.ExceptionHandler.AlertEvent += ((message, caption, alertType) =>
                {
                    ErrorDialog.ShowAlert(message, caption, alertType);
                });

			_packItem.ChangedEvent += ((sender, e) =>
				{
					FormState = FormStates.Dirty;
				});			

            RefreshParent = false;
        }

        public bool Setup(StockItem stockItem, string workbench, string member)
        {
			var error = string.Empty;
            _packItem.Mode = WorkbenchManualInfo.Modes.Workbench;

			txtItem.Text = stockItem.ToString();
            txtUPC.Text = stockItem.GetUPC();
			txtDescription.Text = stockItem.GetDescription();

			if (_packItem.IsValidPackItem(stockItem))
			{
                if (_packItem.IsItemLocked())
                    error = "Stock item is locked by: " + _packItem.LockedBy;

                else if (!_packItem.LockItem())
                    error = "Failed to lock stock item";

                else if (!_packItem.CalculateMasterQuantities(workbench, member))
                    error = "Failed to calculate Master Quantities";
			}
			else
				error = "Stock item not found or stock item is not part of a pack";

            if (error != string.Empty)
            {
                MessageBox.Show(error, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Close();
            }
            return error == string.Empty;
        }

        public void ShowForm()
        {
            RefreshHoldings();
            RefreshGrids();
            lstMarkets.RefreshControl();
            this.ShowDialog();
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region Overrides
        //-----------------------------------------------------------------------------------------
        internal override void RefreshButtons()
        {
            switch (base.FormState)
            {
                case FormStates.Idle:
                    btnExport.Enabled = false;
                    btnApply.Enabled = false;
                    txtUPC.EditValue = null;
                    splitContainerControl1.Visible = false;
                    break;

                case FormStates.Clean:
                    btnExport.Enabled = viewAPMasterItem.RowCount > 0 && viewAllocations.RowCount > 0;
                    btnApply.Enabled = false;
                    splitContainerControl1.Visible = true;
                    break;

                case FormStates.Dirty:
                    btnExport.Enabled = true;
					btnApply.Enabled = true;
                    break;
            }

            base.RefreshButtons();
        }

        internal override bool Save()
        {
            base.ShowSavingMessage();
            var saved = _packItem.Save();

            if (saved)
            {
                base.FormState = FormStates.Idle;
                this.RefreshParent = true;
            }
            this.Cursor = Cursors.Default;
            base.ClearStatusMessage();
            return saved;
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region Private event handlers
        //-----------------------------------------------------------------------------------------

        protected override void BaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            viewAllocations.ValidateEditor();

            if (FormState == FormStates.Error)
            {
                if (Question.YesNo("Errors were found. All changes will be discarded. Do you want to continue?", this.Text) == true)
                {
                    FormState = FormStates.Idle;
                    base.BaseForm_FormClosing(sender, e);
                    _packItem.OverAllocationAlert = chkOverAllocationAlert.Checked;
                    _packItem.UnlockItem(); 
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                base.BaseForm_FormClosing(sender, e);
                _packItem.OverAllocationAlert = chkOverAllocationAlert.Checked;
                _packItem.UnlockItem(); 
            }
        }

        protected void WorkbenchManualAPPAllocationRelease_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void WorkbenchManualAPPAllocationRelease_Shown(object sender, EventArgs e)
        {
            chkOverAllocationAlert.Checked = _packItem.OverAllocationAlert;

            FormState = FormStates.Clean;
            ClearStatusMessage();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            // loop through the release qty's and fail any that contain 
            // negative release quantities.

            if (Question.YesNo("Do you want to apply these allocations?", "Confirm") && Save())
            {
                viewAllocations.CloseEditor();
                viewAllocations.UpdateCurrentRow();
                viewAPMasterItem.CloseEditor();
                viewAPMasterItem.UpdateCurrentRow();
                
                this.FormState = FormStates.Idle;
                this.Close();
        }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

		private void viewAllocations_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
			base.CellHighLights.SetRowCellStyle(viewAllocations, e, Color.Black, Color.Red);
        }

        private void viewAllocations_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {   
            
            if (viewAllocations.FocusedColumn.FieldName == WorkbenchManualInfo.colReleaseStore)
            {
                Double oldValue = Convert.ToDouble(viewAllocations.FocusedValue);
                Double newValue = Convert.ToDouble(e.Value);

                if (oldValue == 0 && newValue == 1)
                {
                    var relQty  = Convert.ToDecimal(viewAllocations.GetFocusedRowCellValue(colReleaseQty));

                    if (relQty < 0)
                    {
                        e.ErrorText = "Release quantity cannot be a negative quantity.";
                        e.Valid = false;
                    }
                }
            }

            if (viewAllocations.FocusedColumn.FieldName == WorkbenchManualInfo.colReleaseQty)
            {
                if (e.Value == null || e.Value == System.DBNull.Value)
                    e.Value = 0;
                else if (e.Value.ToString() == string.Empty)
                    e.Value = 0;

                var releaseQty = Convert.ToDecimal(e.Value);
				var remainingQty = (decimal)txtRemainingQty.EditValue;
				var currentQty = Convert.ToDecimal(viewAllocations.FocusedValue); 

                if (releaseQty < 0 || releaseQty > 1000)
                {
                    e.ErrorText = "Please enter a value between 0 and 1000";
                    e.Valid = false;
                }
                else if (chkOverAllocationAlert.Checked && (remainingQty - releaseQty + currentQty < 0))
                {
                    e.ErrorText = "Release quantity exceeds available quantity.";
                    e.Valid = false;
                }
                else if (chkOverAllocationAlert.Checked && (releaseQty < 0))
                {
                    e.ErrorText = "Release quantity cannot be a negative quantity.";
                    e.Valid = false;
                }
                else
                {
                    var store = Convert.ToDecimal(viewAllocations.GetFocusedRowCellValue(colStore) ?? 0m);
                    viewAllocations.SetFocusedRowCellValue(colAdditionalSRRUnits, _packItem.GetAdditionalUnits(store, releaseQty));
                }
            }

            if (e.Valid == false)
            {
                this.FormState = FormStates.Error;
            }
            else
            {
                this.FormState = FormStates.Dirty;
            }
        }


        private void viewAllocations_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (this.Visible)
            {
				base.CellHighLights.CellValueChanged(viewAllocations, e);
				if (this.ReleaseCount > 0)
					FormState = FormStates.Dirty;
				else
					FormState = FormStates.Clean;
                ShowHoldings();
				chkOverAllocationAlert.Enabled = false;
            }
        }

        private void viewAllocations_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            ShowHoldings();
        }

        private void riTextEdit_Spin(object sender, DevExpress.XtraEditors.Controls.SpinEventArgs e)
        {
            // disable mouse spinner.
            e.Handled = true;
        }

        private void viewAllocations_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                SendKeys.Send("{DOWN}");
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                DefaultExt = "xlsx",
                AddExtension = true,
                AutoUpgradeEnabled = true,
                CheckPathExists = true,
                FileName = this.Text,
                Filter = "Microsoft Excel Files (*.xlsx)|*.xlsx",
                FilterIndex = 0,
                OverwritePrompt = true,
                Title = "Export to Excel"
            };

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Application.DoEvents();
                var utils = new DevExpressUtils();
                utils.ExportToExcel(dialog.FileName, this.viewAPMasterItem, this.viewAllocations);
            }
        }
        
        private void lstMarkets_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            var refreshRequired = false;
            var markets = lstMarkets.GetCheckedItems();
            var data = (DataTable)gridAllocations.DataSource;

            if (e.State == CheckState.Unchecked)
            {
                if (data != null)
                {
                    foreach (DataRow row in data.Rows)
                    {
                        if (lstMarkets.SelectedValue.ToString() == row[WorkbenchManualInfo.colMarket].ToString())
                        {
                            row[WorkbenchManualInfo.colReleaseStore] = false;
                            refreshRequired = true;
                        }
                    }
                }
            }
            else
            {
                if (data != null)
                    foreach (DataRow row in data.Rows)
                        if (markets.Exists((i) => (i.Id.ToString() == row[WorkbenchManualInfo.colMarket].ToString())))
                        {
                            row[WorkbenchManualInfo.colReleaseStore] = true;
                            refreshRequired = true;
                        }
            }

            if (refreshRequired)
            {
                gridAllocations.RefreshDataSource();
                _packItem.IsDirty = true;
            }
            this.FormState = FormStates.Dirty;
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region Private methods
        //-----------------------------------------------------------------------------------------
        private void RefreshGrids()
        {
            UpdateStatusMessage("Loading data...");
            gridAPMasterItem.DataSource = _packItem.GetPackDetails();
            gridAllocations.DataSource = _packItem.GetAllocations();
            viewAllocations.BestFitColumns();
            viewAPMasterItem.BestFitColumns();
            CellHighLights.Clear();
            ShowHoldings();
        }

        private void ClearHoldings()
        {
            _packItem.Holding = 0;
            ShowHoldings();
        }

		private int ReleaseCount
		{
			get
			{
				var data = (DataTable)gridAllocations.DataSource;
				var releaseCount = 0;
				foreach (DataRow row in data.Rows)
					if (Convert.ToDecimal(row[WorkbenchManualInfo.colReleaseStore]) == 1)
						releaseCount++;
				return releaseCount;                
			}
		}

        private void RefreshHoldings()
        {
            UpdateStatusMessage("Obtaining holdings...");
            _packItem.GetDCStockHolding();
            ShowHoldings();
        }

        public void ShowHoldings()
        {
            txtDCStockHolding.EditValue = _packItem.Holding;
            if (gridAllocations.DataSource == null)
                txtAllocationQty.EditValue = _packItem.SetAllocated(0);
            else
                txtAllocationQty.EditValue = _packItem.CalculateAllocated();
            txtRemainingQty.EditValue = _packItem.Remaining;
        }

        private void gridAllocations_Resize(object sender, EventArgs e)
        {
            colDescription.BestFit();
        }

        private void gridAllocations_Validating(object sender, CancelEventArgs e)
        {

        }

        private void viewAllocations_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            this.FormState = FormStates.Dirty;
        }



        //-----------------------------------------------------------------------------------------
        #endregion

    }
}
