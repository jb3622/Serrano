using System;
using System.Windows.Forms;
using Disney.iDash.SRR.BusinessLayer;
using Disney.iDash.Shared;
using Disney.iDash.SRR.Controls;

namespace Disney.iDash.SRR.UI.Forms.Workbench
{
    public partial class DetailedParametersForm : Disney.iDash.SRR.UI.Forms.Common.BaseParameters
    {
		private bool _resetControls = false;

        #region Public methods
        //-----------------------------------------------------------------------------------------
        public DetailedParametersForm()
        {
            InitializeComponent();
            cboEventCode.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
            {
                ErrorDialog.Show(ex, extraInfo, terminateApplication);
            });
            cboFranchise.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
            {
                ErrorDialog.Show(ex, extraInfo, terminateApplication);
            });
            cboStore.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
            {
                ErrorDialog.Show(ex, extraInfo, terminateApplication);
            });
            cboStoreGrade.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
            {
                ErrorDialog.Show(ex, extraInfo, terminateApplication);
            });
            cboMarket.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
            {
                ErrorDialog.Show(ex, extraInfo, terminateApplication);
            });
            lstPromotions.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
            {
                ErrorDialog.Show(ex, extraInfo, terminateApplication);
            });

            rgParameter.Properties.Items.Clear();
            foreach (var param in DetailedWorkbenchInfo.GetParameters)
                rgParameter.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(param.Parameter, param.Description));

        }

        public override void ShowDetailedForm(DetailedWorkbenchInfo workbench)
        {
			_resetControls = (workbench == null);

			if (workbench != null)
				base.Instance = workbench;
		
			base.Instance.Stage = BusinessLayer.DetailedWorkbenchInfo.Stages.Parameters;
			this.RestoreFormLocation();
			this.Show();
        }

        //-----------------------------------------------------------------------------------------
        #endregion

        #region Private event handlers
        //-----------------------------------------------------------------------------------------
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rgSummaryLevel_EditValueChanged(object sender, EventArgs e)
        {
			if (this.Visible)
				RefreshControls();
        }

        private void DepartmentSelector_ComboLeave(DepartmentSelectorControl.ControlNames controlName)
        {
			if (controlName == DepartmentSelectorControl.ControlNames.Department)
			{
				RefreshItemControl();
				btnView.Enabled = base.Instance.DepartmentId > 0;
				btnEdit.Enabled = btnView.Enabled;
			}
		
        }

		private void DepartmentSelector_EditValueChanged(DepartmentSelectorControl.ControlNames controlName)
		{
			if (controlName == DepartmentSelectorControl.ControlNames.Department)
			{
				base.Instance.DepartmentId = DepartmentSelector.DepartmentId;
				btnView.Enabled = base.Instance.DepartmentId > 0;
				btnEdit.Enabled = btnView.Enabled;
			}
		}

        private void lstPromotions_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            lblCheckedCount.Text = string.Format("{0} selected", lstPromotions.CheckedItemsCount());
        }

		private void btnView_Click(object sender, EventArgs e)
		{
			ShowWorkbench();
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			ShowWorkbench(true);
		}

		private void btnReset_Click(object sender, EventArgs e)
		{
			Reset(true);
		}

		private void DetailedParametersForm_Shown(object sender, EventArgs e)
		{
			Reset(_resetControls);
		}
        //-----------------------------------------------------------------------------------------
        #endregion

        #region Private methods
        //-----------------------------------------------------------------------------------------
		private void Reset(bool resetControls = false)
		{
			this.Cursor = Cursors.WaitCursor;
			Application.DoEvents();
			DepartmentSelector.RefreshControls();
			cboFranchise.RefreshControl();
			cboEventCode.RefreshControl();

			if (resetControls)
			{
				base.Instance.Reset();
				base.Instance.Stage = DetailedWorkbenchInfo.Stages.Parameters;
				ctrlItem.Clear();
				DepartmentSelector.Clear();
				rgParameter.EditValue = DetailedWorkbenchInfo.Parameters.ItemLevel;
				RefreshControls(false, false, false, false, false);
				RefreshItemControl();
			}
			else
			{
				rgParameter.EditValue = base.Instance.Parameter;
				RefreshControls();
			}

			UpdateControlValues();

			this.Cursor = Cursors.Default;
		}

		private void RefreshControls()
        {

            switch ((DetailedWorkbenchInfo.Parameters)rgParameter.EditValue)
            {
                case DetailedWorkbenchInfo.Parameters.StyleLevel:
                case DetailedWorkbenchInfo.Parameters.ItemLevel:
                    RefreshControls(false, false, false, false, false);
                    break;

                case DetailedWorkbenchInfo.Parameters.StyleMarketLevel:
                case DetailedWorkbenchInfo.Parameters.ItemMarketLevel:
                    RefreshControls(true, false, false, false, true);
                    break;

                case DetailedWorkbenchInfo.Parameters.StyleGradeLevel:
                case DetailedWorkbenchInfo.Parameters.ItemGradeLevel:
                    RefreshControls(true, false, true, false, true);
                    break;

                case DetailedWorkbenchInfo.Parameters.StyleStoreLevel:
                case DetailedWorkbenchInfo.Parameters.ItemStoreLevel:
                    RefreshControls(true, true, true, true, true);
                    break;
            }
        }

        private void RefreshControls(bool promotions, bool store, bool storeGrade, bool storeGroup, bool market)
        {
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            lstPromotions.Enabled = promotions;
            cboStore.Enabled = store;
            cboStoreGrade.Enabled = storeGrade;
            cboStoreGroup.Enabled = storeGroup;
            cboMarket.Enabled = market;

            if (promotions)
                lstPromotions.RefreshControl();
            else
                lstPromotions.UnCheckAll();

            if (store)
                cboStore.RefreshControl();
            else
                cboStore.EditValue = Convert.ToDecimal(0);

            if (storeGrade)
				cboStoreGrade.RefreshControl();
            else
                cboStoreGrade.EditValue = null;

			if (storeGroup)
				cboStoreGroup.RefreshControl();
			else
				cboStoreGroup.EditValue = null;

            if (market)
				cboMarket.RefreshControl();
            else
                cboMarket.EditValue = null;

            this.Cursor = Cursors.Default;
        }
		
		private void RefreshItemControl()
		{
			if (ctrlItem.Enabled && ctrlItem.DepartmentId != base.Instance.DepartmentId || base.Instance.DepartmentId != DepartmentSelector.DepartmentId)
			{
				this.Cursor = Cursors.WaitCursor;
				Application.DoEvents();
				base.Instance.DepartmentId = DepartmentSelector.DepartmentId;
				ctrlItem.DepartmentId = base.Instance.DepartmentId;
				ctrlItem.RefreshCombos();
				this.Cursor = Cursors.Default;
			}
		}

		private void ShowWorkbench(bool editMode = false)
		{
			this.Cursor = Cursors.WaitCursor;
			Application.DoEvents(); UpdateWorkbenchInfo();
			var valid = base.Instance.IsValid();
			this.Cursor = Cursors.Default;

			if (valid)
			{
				var frm = new DetailedWorkbenchForm(editMode);
				frm.ParentFormType = this.GetType();
				frm.ShowDetailedForm(base.Instance);
				this.Cursor = Cursors.Default;
				this.Close();
			}
			else
				MessageBox.Show(base.Instance.GetValidationErrors(), "Validation Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

        private void UpdateControlValues()
        {
            DepartmentSelector.DepartmentId = base.Instance.DepartmentId;
            DepartmentSelector.Workbench = base.Instance.Workbench;
            DepartmentSelector.StoreType = base.Instance.StoreType;

            rgParameter.EditValue = base.Instance.Parameter;

			cboStore.EditValue = base.Instance.StoreId;
			cboStoreGrade.EditValue = base.Instance.StoreGradeId;
			cboStoreGroup.EditValue = base.Instance.StoreGroup;
			cboMarket.EditValue = base.Instance.Market;

			ctrlItem.ItemClass = base.Instance.Item.Class;
			ctrlItem.ItemVendor = base.Instance.Item.Vendor;
			ctrlItem.ItemStyle = base.Instance.Item.Style;
			ctrlItem.ItemColour = base.Instance.Item.Colour;
			ctrlItem.ItemSize = base.Instance.Item.Size;

			cboFranchise.EditValue = base.Instance.FranchiseId;
			txtCoordinateGroup.EditValue = base.Instance.CoordinateGroup;

			lstPromotions.SetCheckedItems(base.Instance.Promotions);

			cboEventCode.EditValue = base.Instance.EventCodeId;
			txtUPC.EditValue = base.Instance.UPC;

            chkExcludeItemsWithZeroStock.Checked= base.Instance.ExcludeZeroStock;
            chkShowFullPriceLines.Checked= base.Instance.ShowFullPriceLines;
            chkShowMarkDownLines.Checked= base.Instance.ShowMarkedDownLines;
            chkShowProposedMarkedDownLines.Checked= base.Instance.ShowProposedMarkedDownLines;

            btnView.Enabled = base.Instance.DepartmentId > 0;
			btnEdit.Enabled = btnView.Enabled;

        }

        private void UpdateWorkbenchInfo()
        {
            base.Instance.DepartmentId = DepartmentSelector.DepartmentId;
            base.Instance.DepartmentText = DepartmentSelector.DepartmentText;
            base.Instance.Workbench = DepartmentSelector.Workbench;
            base.Instance.StoreType = DepartmentSelector.StoreType;

            base.Instance.Parameter = (DetailedWorkbenchInfo.Parameters) rgParameter.EditValue;

            base.Instance.StoreId = Convert.ToDecimal(cboStore.EditValue);
            base.Instance.StoreGradeId = cboStoreGrade.EditValue as string;
            base.Instance.StoreGroup = cboStoreGroup.EditValue as string;
            base.Instance.Market = cboMarket.EditValue as string;

            base.Instance.Item.Class = ctrlItem.ItemClass;
            base.Instance.Item.Vendor = ctrlItem.ItemVendor;
            base.Instance.Item.Style = ctrlItem.ItemStyle;
            base.Instance.Item.Colour = ctrlItem.ItemColour;
            base.Instance.Item.Size = ctrlItem.ItemSize;

            base.Instance.FranchiseId = cboFranchise.EditValue as string;
            base.Instance.CoordinateGroup = txtCoordinateGroup.EditValue as string;

            base.Instance.Promotions = lstPromotions.GetCheckedItems();

            base.Instance.EventCodeId = cboEventCode.EditValue as string;
            base.Instance.UPC = txtUPC.EditValue as string;
            base.Instance.ExcludeZeroStock = chkExcludeItemsWithZeroStock.Checked;
            base.Instance.ShowFullPriceLines = chkShowFullPriceLines.Checked;
            base.Instance.ShowMarkedDownLines = chkShowMarkDownLines.Checked;
            base.Instance.ShowProposedMarkedDownLines = chkShowProposedMarkedDownLines.Checked;

            base.Instance.EventCodeText = cboEventCode.Text;
            base.Instance.FranchiseText = cboFranchise.Text;
            base.Instance.StoreGradeText = cboStoreGrade.Text;
            base.Instance.StoreText = cboStore.Text;
           
        }
        //-----------------------------------------------------------------------------------------
        #endregion
    }
}
