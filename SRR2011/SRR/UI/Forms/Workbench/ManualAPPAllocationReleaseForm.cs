using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Disney.iDash.Shared;
using Disney.iDash.SRR.BusinessLayer;
using DevExpress.XtraPrinting;
using System.Diagnostics;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace Disney.iDash.SRR.UI.Forms.Workbench
{
    public partial class ManualAPPAllocationReleaseForm : Disney.iDash.SRR.UI.Forms.Common.BaseDataForm
    {
        private WorkbenchManualInfo _packItem = new WorkbenchManualInfo();

        #region Public methods
        //-----------------------------------------------------------------------------------------
        public ManualAPPAllocationReleaseForm()
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

        }

        public void ShowForm(bool stowaway = false)
        {
            _packItem.Mode = (stowaway ? WorkbenchManualInfo.Modes.Stowaway : WorkbenchManualInfo.Modes.Manual);
            this.Text = (stowaway ? "Stowaway APP Allocation Release" : "Manual APP Allocation Release");
            colStore.Caption = (stowaway ? "Stowaway Store" : "Store");
            this.MdiParent = FormUtils.FindMdiParent();

			RefreshControls(true);
            RefreshButtons();
            this.Show();

        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region Overrides
        //-----------------------------------------------------------------------------------------
        internal override void Clear()
        {
            _packItem.UnlockItem();
            ClearError();
            ClearResults();           
            base.Clear();
			chkOverAllocationAlert.Enabled = true;
        }

        internal override void Search()
        {
            var valid = false;
            base.ShowSearchingMessage();

            ClearError();
            ClearResults();

            if (rbItem.Checked && ctrlPackItem.HasValue)
            {
                valid = _packItem.IsValidPackItem(new StockItem(ctrlPackItem.ItemClass, ctrlPackItem.ItemVendor, ctrlPackItem.ItemStyle, ctrlPackItem.ItemColour, ctrlPackItem.ItemSize));
                if (valid)
                    txtUPC.EditValue = _packItem.StockItem.UPC;
                else
                    txtUPC.EditValue = null;
            }
            else if (rbUPC.Checked && txtUPC.EditValue != null && txtUPC.EditValue.ToString() != string.Empty)
            {
                valid = _packItem.IsValidPackItem(txtUPC.EditValue.ToString());
                if (valid)
                {
                    ctrlPackItem.ItemClass = _packItem.StockItem.Class;
                    ctrlPackItem.ItemVendor = _packItem.StockItem.Vendor;
                    ctrlPackItem.ItemStyle = _packItem.StockItem.Style;
                    ctrlPackItem.ItemColour = _packItem.StockItem.Colour;
                    ctrlPackItem.ItemSize = _packItem.StockItem.Size;
                }
                else
                    ctrlPackItem.Clear();
            }

            if (valid)
            {
                if (_packItem.IsItemLocked())
                    ShowError("Stock item is locked by: " + _packItem.LockedBy);
                else
                {
                    RefreshHoldings();
                    RefreshGrids();
                    _packItem.LockItem();
                    FormState = FormStates.Clean;
                }
            }
            else
                ShowError("Stock item not found or stock item is not part of a pack");

            base.ClearStatusMessage();

        }

        internal override void RefreshButtons()
        {
            switch (base.FormState)
            {
                case FormStates.Idle:
                    btnSearch.Enabled = true;
                    btnExport.Enabled = false;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
					ctrlPackItem.Clear();
					txtUPC.EditValue = null;
                    ClearError();
                    ClearResults();
                    splitContainerControl1.Visible = false;
                    break;

                case FormStates.Clean:
                    btnSearch.Enabled = false;
                    btnExport.Enabled = viewAPMasterItem.RowCount > 0 && viewAllocations.RowCount>0;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = true;
                    splitContainerControl1.Visible = true;
                    break;

                case FormStates.Dirty:
                    btnSearch.Enabled = false;
                    btnExport.Enabled = viewAPMasterItem.RowCount > 0 && viewAllocations.RowCount>0;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    break;
            }

			rbItem.Enabled = base.FormState == FormStates.Idle;
			rbUPC.Enabled = base.FormState == FormStates.Idle;
			
			ctrlPackItem.Enabled = rbItem.Enabled && rbItem.Checked;
			txtUPC.Enabled = rbUPC.Enabled && rbUPC.Checked;

            base.RefreshButtons();
        }

        internal override bool Save()
        {
            base.ShowSavingMessage();
            var saved = _packItem.Save();

            if (saved)
                base.FormState = FormStates.Idle;

            this.Cursor = Cursors.Default;
            base.ClearStatusMessage();
            return saved;
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region event handlers
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
            }
        }


        private void ManualAPAllocationRelease_FormClosing(object sender, FormClosingEventArgs e)
        {
            //_packItem.OverAllocationAlert = chkOverAllocationAlert.Checked;
        }

        private void ManualAPAllocationRelease_Shown(object sender, EventArgs e)
        {
            chkOverAllocationAlert.Checked = _packItem.OverAllocationAlert;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Question.YesNo("Do you want to apply these allocations?", "Confirm") && Save())
                this.FormState = FormStates.Idle;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (base.DiscardChanges())
                Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbItem_CheckedChanged(object sender, EventArgs e)
        {
            RefreshControls();
        }

        private void rbUPC_CheckedChanged(object sender, EventArgs e)
        {
            RefreshControls();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }
        
        private void viewAllocations_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            // prevent editing of locked stores.
            if (!viewAllocations.IsGroupRow(e.FocusedRowHandle) && e.FocusedRowHandle != GridControl.InvalidRowHandle && e.FocusedRowHandle != GridControl.AutoFilterRowHandle && e.FocusedRowHandle != GridControl.NewItemRowHandle)
                viewAllocations.Columns[WorkbenchManualInfo.colReleaseQty].OptionsColumn.AllowEdit = ((int)viewAllocations.GetFocusedRowCellValue("LOCKED")) == 0;
        }

		private void viewAllocations_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
			base.CellHighLights.SetRowCellStyle(viewAllocations, e, Color.Black, Color.Red);
        }

        private void viewAllocations_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (e.Value != null && e.Value.ToString() != string.Empty) 
            {
                if (viewAllocations.FocusedColumn.FieldName == WorkbenchManualInfo.colReleaseStore)
                {
                    Double oldValue = Convert.ToDouble(viewAllocations.FocusedValue);
                    Double newValue = Convert.ToDouble(e.Value);

                    if (oldValue == 0 && newValue == 1)
                    {
                        var relQty = Convert.ToDecimal(viewAllocations.GetFocusedRowCellValue(colReleaseQty));

                        if (relQty < 0)
                        {
                            e.ErrorText = "Release quantity cannot be a negative quantity.";
                            e.Valid = false;
                        }
                    }
                }                
            }

            if (viewAllocations.FocusedColumn.FieldName == WorkbenchManualInfo.colReleaseQty)
            {
                if (e.Value == null || e.Value == System.DBNull.Value)
                    e.Value = 0;
                else if (e.Value.ToString() == string.Empty)
                    e.Value = 0;
            }

            if (viewAllocations.FocusedColumn.FieldName == WorkbenchManualInfo.colReleaseQty && e.Value != null && e.Value.ToString() != string.Empty) 
            {
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
                FormState = FormStates.Dirty;
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
        //-----------------------------------------------------------------------------------------
        #endregion

        #region Private methods
        //-----------------------------------------------------------------------------------------
        private void RefreshControls(bool init=false)
        {
            if (init)
            {
                ctrlPackItem.DepartmentId = -1;
                ctrlPackItem.RefreshCombos();
                FormState = FormStates.Idle;
            }

            if (rbItem.Checked)
            {
                ctrlPackItem.Enabled = true;
                ctrlPackItem.ShowZeros = false;
                txtUPC.EditValue = null;
                txtUPC.Enabled = false;
            }
            else
            {
                ctrlPackItem.Clear();
                ctrlPackItem.Enabled = false;
                ctrlPackItem.ShowZeros = true;
                txtUPC.Enabled = true;
            }
        }

        private void ClearGrids()
        {
            gridAPMasterItem.DataSource = null;
            gridAllocations.DataSource = null;
            CellHighLights.Clear();
        }

        private void RefreshGrids()
        {
            gridAPMasterItem.DataSource = _packItem.GetPackDetails();
            gridAllocations.DataSource = _packItem.GetAllocations();
            CellHighLights.Clear();
            ShowHoldings();
        }

        private void ClearHoldings()
        {
            _packItem.Holding = 0;
            ShowHoldings();
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

        private void ClearResults()
        {
            ClearHoldings();
            ClearGrids();
        }

        private void ClearError()
        {
            lblErrorMessage.Visible = false;
        }

        private void ShowError(string error)
        {
            lblErrorMessage.Text = error;
            lblErrorMessage.Visible = true;
        }

        private void viewAllocations_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }


        //-----------------------------------------------------------------------------------------
        #endregion
    }
}
