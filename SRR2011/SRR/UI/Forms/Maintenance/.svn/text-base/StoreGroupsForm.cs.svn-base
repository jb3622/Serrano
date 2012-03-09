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
    public partial class StoreGroupsForm : Disney.iDash.SRR.UI.Forms.Common.BaseDataForm
    {
        private StoreGroupsCollection _storeGroups = new StoreGroupsCollection();

        #region Public methods
        //-----------------------------------------------------------------------------------------
        public StoreGroupsForm()
        {
            InitializeComponent();
        }

        public void Setup()
        {
            viewItems.Appearance.EvenRow.BackColor = Properties.Settings.Default.EvenRowBackColor;
            viewItems.Appearance.OddRow.BackColor = Properties.Settings.Default.OddRowBackColor;
            viewItems.Appearance.FocusedRow.BackColor = Properties.Settings.Default.FocusedRowBackColor;
            viewItems.Appearance.SelectedRow.BackColor = Properties.Settings.Default.SelectedRowBackColor;
            viewItems.Appearance.SelectedRow.ForeColor = Properties.Settings.Default.SelectedRowForeColor;

            _storeGroups.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
                {
                    ErrorDialog.Show(ex, extraInfo, terminateApplication);
                });

            _storeGroups.ChangedEvent += ((sender, e) =>
                {
                    base.FormState = FormStates.Dirty;
                });

            _storeGroups.ProgressEvent += ((message, percentageComplete) =>
                {
                    base.UpdateProgress(message, percentageComplete);
                });
        }

        public void ShowForm()
        {
            this.MdiParent = FormUtils.FindMdiParent();
            this.Show();
        }        
        //-----------------------------------------------------------------------------------------
        #endregion

        #region Private event handlers
        //-----------------------------------------------------------------------------------------
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
                this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void viewItems_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            _storeGroups.IsDirty = true;
        }

        private void viewItems_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == colSelected)
                _storeGroups.IsDirty = true;
        }

        private void StoreGroupsForm_Shown(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void viewItems_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            btnTick.Enabled = viewItems.SelectedRowsCount > 0;
            btnUntick.Enabled = btnTick.Enabled;
        }

        private void btnTick_Click(object sender, EventArgs e)
        {
            TickSelected(true);
        }

        private void btnUntick_Click(object sender, EventArgs e)
        {
            TickSelected(false);
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
        }

        internal override bool Save()
        {
            base.ShowSavingMessage();

            var saved = _storeGroups.Save();
            if (saved)
            {
                base.FormState = FormStates.Idle;
                LookupSource.ClearCache(LookupSource.LookupTypes.StoreGroups);
            }
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

            if (_storeGroups.Refresh())
                gridItems.DataSource = _storeGroups.Items;

            if (viewItems.RowCount == 0)
            {
                gridItems.Visible = false;
                base.FormState = FormStates.Idle;
            }
            else
            {
                gridItems.Visible = true;
                viewItems.BestFitColumns();
                base.FormState = FormStates.Clean;
            }

            base.ClearStatusMessage();
        }

        private void TickSelected(bool ticked)
        {
            foreach (var rowHandle in viewItems.GetSelectedRows())
                viewItems.SetRowCellValue(rowHandle, colSelected, ticked);
            viewItems.ClearSelection();
        }

        //-----------------------------------------------------------------------------------------
        #endregion
    }
}
