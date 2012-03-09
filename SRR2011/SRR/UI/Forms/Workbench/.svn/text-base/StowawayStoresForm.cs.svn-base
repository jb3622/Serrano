using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Disney.iDash.LocalData;
using Disney.iDash.Shared;
using Disney.iDash.SRR.BusinessLayer;

namespace Disney.iDash.SRR.UI.Forms.Workbench
{
	public partial class StowawayStoresForm : Disney.iDash.SRR.UI.Forms.Common.BaseDataForm
	{
		private StowawayStoresInfo _stowawayStores = new StowawayStoresInfo();
		private const string kGridRegistryKey = "StowawayStores";

		#region Public methods
		//-----------------------------------------------------------------------------------------
		public StowawayStoresForm()
		{
			InitializeComponent();
		}

		public void Setup()
		{
			_stowawayStores.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
			{
				ErrorDialog.Show(ex, extraInfo, terminateApplication);
			});

			_stowawayStores.ExceptionHandler.AlertEvent += ((message, caption, alertType) =>
			{
				ErrorDialog.ShowAlert(message, caption, alertType);
			});

			cboDepartment.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
			{
				ErrorDialog.Show(ex, extraInfo, terminateApplication);
			});

			cboDepartment.ExceptionHandler.AlertEvent += ((message, caption, alertType) =>
			{
				ErrorDialog.ShowAlert(message, caption, alertType);
			});

			cboStore.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
			{
				ErrorDialog.Show(ex, extraInfo, terminateApplication);
			});

			cboStore.ExceptionHandler.AlertEvent += ((message, caption, alertType) =>
			{
				ErrorDialog.ShowAlert(message, caption, alertType);
			});

			_stowawayStores.ChangedEvent += ((sender, e) =>
			{
				base.FormState = FormStates.Dirty;
			});


			colItem.FieldName = StowawayStoresInfo.colItem;
			colUPC.FieldName = StowawayStoresInfo.colUPC;
			colItemDescription.FieldName = StowawayStoresInfo.colItemDescription;
			colDistro.FieldName = StowawayStoresInfo.colDistro;
			colCurRingFenced.FieldName = StowawayStoresInfo.colCurRingFenced;
			colNewRingFence.FieldName = StowawayStoresInfo.colNewRingFenced;
			colVoidQty.FieldName = StowawayStoresInfo.colVoidQty;
			colStoreId.FieldName = StowawayStoresInfo.colStoreId;
			colReleaseQty.FieldName = StowawayStoresInfo.colReleaseQty;
		}

		public void ShowForm()
		{
			this.MdiParent = FormUtils.FindMdiParent();
			RefreshControls();
			this.Show();
		}
		//-----------------------------------------------------------------------------------------
		#endregion

		#region Overrides
		//-----------------------------------------------------------------------------------------
		internal override void Clear()
		{
			cboDepartment.EditValue = null;
			cboStore.EditValue = null;
			ClearGrid();
			base.Clear();
		}

		internal override void Search()
		{
			RefreshGrid();
		}

		internal override void RefreshButtons()
		{
			switch (base.FormState)
			{
				case FormStates.Idle:
					cboDepartment.Enabled = true;
					cboStore.Enabled = true;
					btnSearch.Enabled =  (cboDepartment.EditValue != null && cboStore.EditValue != null);
					btnSave.Enabled = false;
					btnCancel.Enabled = false;
					break;

				case FormStates.Clean:
					cboDepartment.Enabled = false;
					cboStore.Enabled = false;
					btnSearch.Enabled = false;
					btnSave.Enabled = false;
					btnCancel.Enabled = true;
					break;

				case FormStates.Dirty:
					cboDepartment.Enabled = false;
					cboStore.Enabled = false;
					btnSearch.Enabled = false;
					btnSave.Enabled = true;
					btnCancel.Enabled = true;
					break;
			}

			base.RefreshButtons();
		}

		internal override bool Save()
		{
			base.ShowSavingMessage();
			var saved = _stowawayStores.Save();

			if (saved)
				base.FormState = FormStates.Idle;

			this.Cursor = Cursors.Default;
			base.ClearStatusMessage();
			return saved;
		}

		//-----------------------------------------------------------------------------------------
		#endregion

		#region Private event handlers
		//-----------------------------------------------------------------------------------------
		private void btnSearch_Click(object sender, EventArgs e)
		{
			RefreshGrid();
		}

		private void cboDepartment_EditValueChanged(object sender, EventArgs e)
		{
			RefreshButtons();
		}
		
		private void cboStore_EditValueChanged(object sender, EventArgs e)
		{
			RefreshButtons();
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

		private void viewItems_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
		{
			base.CellHighLights.SetRowCellStyle(viewItems, e, Color.Black, Color.Red);
		}

		private void viewItems_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
		{
			/*
			 * User cannot allocate more stock than available in current ring fenced
			 * Must allocate in Distro multiples.
			 */
			if (viewItems.FocusedColumn == colVoidQty || viewItems.FocusedColumn == colReleaseQty)
			{
				var distro = (decimal)viewItems.GetFocusedRowCellValue(colDistro);
				var curRingFenced = (decimal)viewItems.GetFocusedRowCellValue(colCurRingFenced);
				decimal voidQty = 0;
				decimal releaseQty = 0;

				if (viewItems.FocusedColumn == colVoidQty)
				{
					voidQty = (decimal)(e.Value ?? 0m);
					if (voidQty % distro != 0 || voidQty < 0)
					{
						e.Valid = false;
						e.ErrorText = "Void Quantity must be greated than zero and multiples of " + distro.ToString();
					}
				}
				else
					voidQty = (decimal)viewItems.GetFocusedRowCellValue(colVoidQty);

				if (viewItems.FocusedColumn == colReleaseQty)
				{
					releaseQty = (decimal)(e.Value ?? 0m);
					if (releaseQty % distro != 0 || releaseQty < 0)
					{
						e.Valid = false;
						e.ErrorText = "Release Quantity must be greated than zero and multiples of " + distro.ToString();
					}
				}
				else
					releaseQty = (decimal)viewItems.GetFocusedRowCellValue(colReleaseQty);

				if (e.Valid)
				{
					if (voidQty + releaseQty > curRingFenced)
					{
						e.Valid = false;
						e.ErrorText = "You cannot allocate more stock than currently ring fenced";
					}
					else
						viewItems.SetFocusedRowCellValue(colNewRingFence, curRingFenced - voidQty - releaseQty);
				}
			}
		}

		private void viewItems_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			if (this.Visible)
			{
				base.CellHighLights.CellValueChanged(viewItems, e);
				FormState = FormStates.Dirty;
			}
		}

		private void StowawayStores_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!e.Cancel)
				base.SaveLayout(this.viewItems, kGridRegistryKey);

		}
		//-----------------------------------------------------------------------------------------
		#endregion

		#region Private methods
		//-----------------------------------------------------------------------------------------
		private void RefreshControls()
		{
			var lookupSource = new LookupSource();
			lookupSource.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
			{
				ErrorDialog.Show(ex, extraInfo, terminateApplication);
			});

			riStore.DataSource = lookupSource.GetItems(LookupSource.LookupTypes.Stores);

			cboDepartment.WhereClause = string.Format("USUSPR = '{0}'", Session.User.NetworkId.ToUpper());
			cboDepartment.RefreshControl();
			cboStore.RefreshControl();
		}

		private void ClearGrid()
		{
			CellHighLights.Clear();
			gridItems.Visible = false;
		}

		private void RefreshGrid()
		{
            viewItems.ClearColumnsFilter();
            viewItems.ClearGrouping();
            viewItems.ClearSorting();

			ClearGrid();
			base.ShowSearchingMessage();

			if (gridItems.DataSource != null)
				base.SaveLayout(this.viewItems, kGridRegistryKey);

			gridItems.DataSource = _stowawayStores.GetData((decimal) cboDepartment.EditValue, (decimal) (cboStore.EditValue ?? "0"));

			gridItems.Visible = viewItems.RowCount > 0;
			viewItems.BestFitColumns();
			base.ClearStatusMessage();

			if (!gridItems.Visible)
				base.UpdateStatusMessage("Not found", true);

			base.LoadLayout(this.viewItems, kGridRegistryKey);
		}

        private void riQty_Spin(object sender, DevExpress.XtraEditors.Controls.SpinEventArgs e)
        {
            e.Handled = true;
        }

		//-----------------------------------------------------------------------------------------
		#endregion
	}
}
