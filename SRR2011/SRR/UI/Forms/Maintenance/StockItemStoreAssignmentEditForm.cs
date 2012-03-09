using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Disney.iDash.Shared;
using DevExpress.XtraGrid;
using Disney.iDash.SRR.BusinessLayer;

namespace Disney.iDash.SRR.UI.Forms.Maintenance
{
	public partial class StockItemStoreAssignmentEditForm : Disney.iDash.SRR.UI.Forms.Common.BaseDataForm
	{
		private StockInfo _stockInfo = new StockInfo();
		private StockItemStoreAssignmentForm _parent = null;

		public StockItemStoreAssignmentEditForm()
		{
			InitializeComponent();
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
		}

		public void ShowForm(StockItemStoreAssignmentForm parent, decimal deptId)
		{
			_parent = parent;
			ctrlItem.DepartmentId = deptId;
			ctrlItem.RefreshCombos();
			cboStore.RefreshControl();
			this.Show();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (!ctrlItem.StockItem.HasAllValues)
				MessageBox.Show("Please specify a stock item", "Incomplete Data", MessageBoxButtons.OK, MessageBoxIcon.Error);

			else if (cboStore.EditValue == null)
				MessageBox.Show("Please specify a store", "Incomplete Data", MessageBoxButtons.OK, MessageBoxIcon.Error);

			else if (_stockInfo.InsertItemDetails(ctrlItem.DepartmentId, ctrlItem.StockItem, (decimal)cboStore.EditValue, spinOpeningRateOfSale.Value))
			{
				this.Close();
				if (_parent != null && !_parent.IsDisposed && _parent.Visible)
					_parent.RefreshGrid();
			}
		}

		private void ctrlItem_Validating(object sender, CancelEventArgs e)
		{
			var missingItem = false;
			lblItemDescription.Text = string.Empty;

			if (ctrlItem.StockItem.HasAllValues)
			{
				if (ctrlItem.StockItem.IsValid())
				{
					lblItemDescription.Text = ctrlItem.StockItem.GetDescription();
					lblItemDescription.ForeColor = Color.Black;
				}
				else
					missingItem = true;
			}
			else
				missingItem = true;

			if (missingItem && ctrlItem.StockItem.HasValue)
			{
				e.Cancel = true;
				lblItemDescription.Text = "Stock item does not exist";
				lblItemDescription.ForeColor = Color.Red;
			}

		}

	}
}
