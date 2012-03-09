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
using DevExpress.XtraGrid.Views.Grid;

namespace Disney.iDash.SRR.UI.Forms.Maintenance
{
    public partial class StoreLeadTimeForm : Disney.iDash.SRR.UI.Forms.Common.BaseDataForm
    {
        private BusinessLayer.StoreInfo _storeInfo = new BusinessLayer.StoreInfo();

        #region Public methods
        //-----------------------------------------------------------------------------------------
        public StoreLeadTimeForm()
        {
            InitializeComponent();
        }

		public void Setup()
		{
			viewItems.Appearance.EvenRow.BackColor = Properties.Settings.Default.EvenRowBackColor;
			viewItems.Appearance.OddRow.BackColor = Properties.Settings.Default.OddRowBackColor;
			viewItems.Appearance.FocusedRow.BackColor = Properties.Settings.Default.FocusedRowBackColor;

			lstMarkets.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
			{
				ErrorDialog.Show(ex, extraInfo, terminateApplication);
			});

			_storeInfo.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
			{
				ErrorDialog.Show(ex, extraInfo, terminateApplication);
			});

			_storeInfo.ChangedEvent += ((sender, e) =>
			{
				base.FormState = FormStates.Dirty;
			});
		}

        public void ShowForm()
        {
            this.MdiParent = FormUtils.FindMdiParent();
            RefreshControls();
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.DiscardChanges();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

		private void lstMarkets_EditValueChanged(object sender, EventArgs e)
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
            gridItems.DataSource = null;
            gridItems.Visible = false;
        }

        internal override void RefreshButtons()
        {
            btnSave.Enabled = base.FormState == FormStates.Dirty;
            btnCancel.Enabled = base.FormState != FormStates.Idle;
            btnSearch.Enabled = base.FormState == FormStates.Idle && lstMarkets.EditValue != null && lstMarkets.EditValue.ToString() != string.Empty;
            lstMarkets.Enabled = base.FormState == FormStates.Idle;           
        }

        internal override bool Save()
        {
            base.ShowSavingMessage();

            var saved = _storeInfo.SaveLeadTimes();
            if (saved)
                base.FormState = FormStates.Idle;

            base.ClearStatusMessage();
            
            return saved;
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region Private methods
        //-----------------------------------------------------------------------------------------
        private void RefreshControls()
        {
            lstMarkets.RefreshControl();
        }

        private void RefreshGrid()
        {
            viewItems.ClearColumnsFilter();
            viewItems.ClearGrouping();
            viewItems.ClearSorting();

            base.ShowSearchingMessage();

            gridItems.DataSource = _storeInfo.GetStoreLeadTimes(lstMarkets.EditValue.ToString().Replace(", ", "','"));

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

        private void riLeadTime_Spin(object sender, DevExpress.XtraEditors.Controls.SpinEventArgs e)
        {
            e.Handled = true;
        }
        //-----------------------------------------------------------------------------------------
        #endregion
    }
}
