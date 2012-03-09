using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Disney.iDash.SRR.BusinessLayer;
using Disney.iDash.Shared;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace Disney.iDash.SRR.UI.Forms.Workbench
{
    public partial class StoreClassSelectorDialog : Disney.iDash.SRR.UI.Forms.Common.BaseParameters
    {
        private bool _editMode = false;
		private const string kStoreGridName = "StoreClassSelector_Stores";
		private const string kClassGridName = "StoreClassSelector_Class";

		public bool StoresAvailable { get; private set; }

        public StoreClassSelectorDialog(bool editMode = false)
        {
            InitializeComponent();
            viewClass.Appearance.OddRow.BackColor = Properties.Settings.Default.OddRowBackColor;
            viewClass.Appearance.EvenRow.BackColor = Properties.Settings.Default.EvenRowBackColor;
			viewClass.Appearance.FocusedRow.BackColor = Properties.Settings.Default.FocusedRowBackColor;

            viewStore.Appearance.OddRow.BackColor = Properties.Settings.Default.OddRowBackColor;
			viewStore.Appearance.EvenRow.BackColor = Properties.Settings.Default.EvenRowBackColor;
			viewStore.Appearance.FocusedRow.BackColor = Properties.Settings.Default.FocusedRowBackColor;
            _editMode = editMode;
        }

        public override void ShowDetailedForm(DetailedWorkbenchInfo workbench)
        {
            base.Instance = workbench;
            base.Instance.Stage = BusinessLayer.DetailedWorkbenchInfo.Stages.SelectStore;

			if (RefreshStoreView())
			{
				StoresAvailable = true;
				this.RestoreFormLocation();
				btnNext.Enabled = false;
				this.Show();
				this.Cursor = Cursors.Default;
			}
			else
			{
				StoresAvailable = false;
				MessageBox.Show("No stores available that match your selection critiera", "Store/Class", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				this.Close();
			}
        }

        private void viewStore_DoubleClick(object sender, EventArgs e)
        {
            base.Instance.Item.Class = null;
            ShowWorkbench();
        }

        private void viewStore_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                base.Instance.StoreId = (decimal)viewStore.GetFocusedRowCellValue("STORE");
                base.Instance.StoreText = viewStore.GetFocusedRowCellValue("STORENAME").ToString();
                btnNext.Enabled = true;
                RefreshClassView();
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(ex, "ViewStore_FocusRowChanged");
            }
        }

        private void viewClass_DoubleClick(object sender, EventArgs e)
        {
            ShowWorkbench();
        }

        private void viewClass_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                base.Instance.Item.Class = Convert.ToDecimal(viewClass.GetFocusedRowCellValue("CLASS"));
                btnNext.Enabled = true;
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(ex, "ViewClass_FocusRowChanged");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            base.ParentFormType = typeof(DetailedFocusGroupsForm);
            base.ShowParentForm();
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            ShowWorkbench();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

		private void StoreClassSelector_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!e.Cancel)
			{
				base.SaveLayout(this.viewStore, kStoreGridName);
				base.SaveLayout(this.viewClass, kClassGridName);
			}

		}
        
        private bool RefreshStoreView()
        {
			var result = false;
            Instance.Stage = BusinessLayer.DetailedWorkbenchInfo.Stages.SelectStore;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();

				if (gridStore.DataSource != null)
					base.SaveLayout(this.viewStore, kStoreGridName);

                gridStore.DataSource = Instance.GetData();
                if (gridStore.DataSource != null && viewStore.Columns.Count > 0)
                {
                    FormatColumns(viewStore);
                    viewStore.Columns[DetailedWorkbenchInfo.colStore].Caption = "Store";
                    viewStore.Columns[DetailedWorkbenchInfo.colStoreName].Caption = "Store Name";
                    viewStore.Columns[DetailedWorkbenchInfo.colStoreType].Caption = "Store Type";
                    viewStore.Columns[DetailedWorkbenchInfo.colWorkBench].Caption = "Workbench";
                    viewStore.Columns[DetailedWorkbenchInfo.colMarket].Caption = "Market";
                    viewStore.Columns[DetailedWorkbenchInfo.colSalesLW].Caption = "Sales LW";
                    viewStore.Columns[DetailedWorkbenchInfo.colSalesTW].Caption = "Sales TW";
                    viewStore.Columns[DetailedWorkbenchInfo.colCurrentROS].Caption = "Current ROS";
                    viewStore.Columns[DetailedWorkbenchInfo.colStockOnHand].Caption = "Stock on Hand";
                    viewStore.Columns[DetailedWorkbenchInfo.colSmoothedStoreCover].Caption = "Smoothed Store Cover";
                    viewStore.Columns[DetailedWorkbenchInfo.colAllocated].Caption = "Allocated";
                    viewStore.Columns[DetailedWorkbenchInfo.colShipped].Caption = "Shipped";
                    viewStore.Columns[DetailedWorkbenchInfo.colTotalStockRequired].Caption = "Total Stock Required";
                    viewStore.Columns[DetailedWorkbenchInfo.colIdealAllocQty].Caption = "Ideal Alloc. Qty.";
                    viewStore.Columns[DetailedWorkbenchInfo.colProposedAllocQty].Caption = "Proposed Alloc. Qty.";
                    viewStore.Columns[DetailedWorkbenchInfo.colOutOfStock].Caption = "Out of Stock";
                    viewStore.BestFitColumns();
                    base.LoadLayout(this.viewClass, kStoreGridName);
                }
				result = viewStore.RowCount > 0;
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(ex, "RefreshStoreView");
            }
            this.Cursor = Cursors.Default;
			return result;
        }

        private void RefreshClassView()
        {
            Instance.Stage = BusinessLayer.DetailedWorkbenchInfo.Stages.SelectClass;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();

				if (gridClass.DataSource != null)
					base.SaveLayout(this.viewClass, kClassGridName);

                gridClass.DataSource = Instance.GetData();
                if (gridClass.DataSource != null && viewClass.Columns.Count > 0)
                {
                    FormatColumns(viewClass);
                    viewClass.Columns[DetailedWorkbenchInfo.colClass].Caption = "Class";
                    viewClass.Columns[DetailedWorkbenchInfo.colStoreType].Caption = "Store Type";
                    viewClass.Columns[DetailedWorkbenchInfo.colWorkBench].Caption = "Workbench";
                    viewClass.Columns[DetailedWorkbenchInfo.colSalesLW].Caption = "Sales LW";
                    viewClass.Columns[DetailedWorkbenchInfo.colSalesTW].Caption = "Sales TW";
                    viewClass.Columns[DetailedWorkbenchInfo.colCurrentROS].Caption = "Current ROS";
                    viewClass.Columns[DetailedWorkbenchInfo.colStockOnHand].Caption = "Stock on Hand";
                    viewClass.Columns[DetailedWorkbenchInfo.colSmoothedStoreCover].Caption = "Smoothed Store Cover";
                    viewClass.Columns[DetailedWorkbenchInfo.colAllocated].Caption = "Allocated";
                    viewClass.Columns[DetailedWorkbenchInfo.colShipped].Caption = "Shipped";
                    viewClass.Columns[DetailedWorkbenchInfo.colTotalStockRequired].Caption = "Total Stock Required";
                    viewClass.Columns[DetailedWorkbenchInfo.colIdealAllocQty].Caption = "Ideal Alloc. Qty.";
                    viewClass.Columns[DetailedWorkbenchInfo.colProposedAllocQty].Caption = "Proposed Alloc. Qty.";
                    viewClass.Columns[DetailedWorkbenchInfo.colOutOfStock].Caption = "Out of Stock";
                    viewClass.BestFitColumns();
                    base.LoadLayout(this.viewClass, kClassGridName);
                }

            }
            catch (Exception ex)
            {
                ErrorDialog.Show(ex, "RefreshClassView");
            }
            this.Cursor = Cursors.Default;
        }

        private void ShowWorkbench()
        {
            this.Close();

            var frm = new DetailedWorkbenchForm(_editMode);
            frm.ParentFormType = typeof(DetailedFocusGroupsForm);
            frm.ShowDetailedForm(base.Instance);
        }

        public void FormatColumns(GridView view)
        {
            foreach (GridColumn col in view.Columns)
            {
                switch (col.ColumnType.Name)
                {
                    case "Boolean":
                        col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        break;

                    case "Int":
                        col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        col.DisplayFormat.FormatString = "#,##0";
                        break;

                    case "Float":
                    case "Double":
                        col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        col.DisplayFormat.FormatString = "#,##0.00";
                        break;

                    case "DateTime":
                        col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        col.DisplayFormat.FormatString = "dd-MMM-yyyy";
                        break;

                }
            }
        }

    }
}
