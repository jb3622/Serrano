using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Disney.iDash.SRR.BusinessLayer;
using Disney.iDash.Shared;

namespace Disney.iDash.SRR.UI.Forms.Maintenance
{
    public partial class StoreDeleteScheduleForm : Disney.iDash.SRR.UI.Forms.Common.BaseDataForm
    {
        private StoreInfo _storeInfo = new StoreInfo();

        #region Public methods
        //-----------------------------------------------------------------------------------------
        public StoreDeleteScheduleForm()
        {
            InitializeComponent();
        }

		public void Setup()
		{
			_storeInfo.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
			{
				ErrorDialog.Show(ex, extraInfo, terminateApplication);
			});

			_storeInfo.ExceptionHandler.AlertEvent += ((message, caption, alertType) =>
			{
				ErrorDialog.ShowAlert(message, caption, alertType);
			});

			_storeInfo.ChangedEvent += ((sender, e) =>
			{
				base.FormState = FormStates.Dirty;
				chkShowProcessedItems.Enabled = false;
			});

			viewItems.Appearance.OddRow.BackColor = Properties.Settings.Default.OddRowBackColor;
			viewItems.Appearance.EvenRow.BackColor = Properties.Settings.Default.EvenRowBackColor;
			viewItems.Appearance.FocusedRow.BackColor = Properties.Settings.Default.FocusedRowBackColor;
			colStore.AppearanceCell.BackColor = Properties.Settings.Default.EditableColumnBackcolor;
			colScheduleDate.AppearanceCell.BackColor = Properties.Settings.Default.EditableColumnBackcolor;

			riStoreLookUp.DataSource = _storeInfo.GetStores();
		}

        public void ShowForm()
        {
            this.MdiParent = FormUtils.FindMdiParent();
            RefreshGrid();
            this.Show();
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region Private event handlers
        //-----------------------------------------------------------------------------------------
        private void btnAdd_Click(object sender, EventArgs e)
        {
            viewItems.AddNewRow();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Question.YesNo("Mark/Unmark selected rows as deleted?", this.Text))
            {
                if (_storeInfo.SetDeletedFlag(viewItems.GetSelectedRows()))
                    viewItems.ClearSelection();
                gridItems.RefreshDataSource();
            }
        }

        private void chkShowProcessedItems_CheckStateChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
                this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
			if (base.DiscardChanges())
				RefreshGrid();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void viewItems_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            btnDelete.Enabled = true;
            var statusFlag = viewItems.GetFocusedRowCellValue(colStatus);

            if (statusFlag != null && statusFlag.ToString() == "Y")
            {
                colStore.OptionsColumn.AllowEdit = false;
                colScheduleDate.OptionsColumn.AllowEdit = false;
            }
            else
            {
                colStore.OptionsColumn.AllowEdit = true;
                colScheduleDate.OptionsColumn.AllowEdit = true;
            }
        }

		private void viewItems_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
		{
			if (e.Row != null)
			{
				var rowView = (DataRowView)e.Row;
				var items = (DataTable)gridItems.DataSource;

				if (string.IsNullOrEmpty(rowView.Row[colScheduleDate.FieldName].ToString()))
				{
					e.ErrorText = "Schedule date must be entered. ";
					e.Valid = false;
				}
				else
					rowView.Row[colScheduleDate.FieldName] = Convert.ToDateTime(rowView.Row[colScheduleDate.FieldName]).ToString("yyyy-MM-dd");

				if (e.Valid && (string.IsNullOrEmpty(rowView.Row[colStore.FieldName].ToString()) || rowView.Row[colStore.FieldName].ToString() == "0"))
				{
					e.ErrorText = "Please select a store. ";
					e.Valid = false;
				}
			
			}
		}

		private void viewItems_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
		{
			e.WindowCaption = "Please correct the error";
			if (e.ErrorText.Contains("STOREID") && e.ErrorText.Contains("constrained") && e.ErrorText.Contains("unique"))
				e.ErrorText = "A store can only be listed once in this schedule. ";
		}

        //-----------------------------------------------------------------------------------------
        #endregion

        #region Overrides
        //-----------------------------------------------------------------------------------------
        internal override void RefreshButtons()
        {
            btnSave.Enabled = base.FormState == FormStates.Dirty;
            btnCancel.Enabled = base.FormState != FormStates.Idle;
        }

        internal override bool Save()
        {
            base.ShowSavingMessage();
            var saved = _storeInfo.SaveStoreDeletionSchedule();

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
        private void RefreshGrid()
        {
            viewItems.ClearColumnsFilter();
            viewItems.ClearGrouping();
            viewItems.ClearSorting();

            base.ShowSearchingMessage();

            gridItems.DataSource = _storeInfo.GetStoreDeletionSchedule(chkShowProcessedItems.Checked);
            gridItems.Visible = true;

            viewItems.BestFitColumns();

			btnAdd.Enabled = true;
			btnDelete.Enabled = viewItems.RowCount > 0;
            chkShowProcessedItems.Enabled = true;

            base.FormState = FormStates.Clean;
            base.ClearStatusMessage();
            RefreshButtons();
        }
        #endregion


    }
}
