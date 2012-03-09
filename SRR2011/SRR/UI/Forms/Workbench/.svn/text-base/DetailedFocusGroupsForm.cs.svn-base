using System;
using System.Windows.Forms;
using Disney.iDash.SRR.BusinessLayer;
using Disney.iDash.Shared;
using Disney.iDash.SRR.Controls;

namespace Disney.iDash.SRR.UI.Forms.Workbench
{
    public partial class DetailedFocusGroupsForm : Disney.iDash.SRR.UI.Forms.Common.BaseParameters
    {
		private bool _resetControls = false;

        #region Public methods
        //-----------------------------------------------------------------------------------------
        public DetailedFocusGroupsForm()
        {
            InitializeComponent();          
            lstMarkets.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
            {
                ErrorDialog.Show(ex, extraInfo, terminateApplication);
            });

			ctrlItem.EditValueChanged += ((sender, e) =>
				{
					
				});
			
            rgFocusGroup.Properties.Items.Clear();
            foreach (var kvp in DetailedWorkbenchInfo.GetFocusGroups)
                rgFocusGroup.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(kvp.Key, kvp.Value));

            rgParameter.Properties.Items.Clear();
			foreach (var param in DetailedWorkbenchInfo.GetParameters)
			{
				if (param.FocusGroups)
					rgParameter.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(param.Parameter, param.Description));
			}
        }

        public override void ShowDetailedForm(DetailedWorkbenchInfo workbench)
        {
			_resetControls = (workbench == null);

			if (workbench != null)
				base.Instance = workbench;

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

        private void chkAllMarkets_CheckedChanged(object sender, EventArgs e)
        {
            lstMarkets.Enabled = !chkAllMarkets.Checked;
            if (chkAllMarkets.Checked)
                lstMarkets.UnCheckAll();
        }

        private void rgLinesOrStores_EditValueChanged(object sender, EventArgs e)
        {
			if (this.Visible)
			{
				RefreshControls();
				ctrlItem.Clear();
				base.Instance.StoreId = 0;
				// reset store, class, item.
			}
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

		private void DetailedFocusGroupsForm_Shown(object sender, EventArgs e)
		{
			Reset(_resetControls);
		}
        //-----------------------------------------------------------------------------------------
        #endregion

        #region Private methods
        //-----------------------------------------------------------------------------------------
        private void ShowWorkbench(bool editMode = false)
        {
            UpdateWorkbenchInfo();

            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            var valid = base.Instance.IsValid();
            this.Cursor = Cursors.Default;

            if (valid)
            {
				base.Instance.FirstTime = true;
                if (base.Instance.FocusGroup == DetailedWorkbenchInfo.FocusGroups.FastestCoverStores || base.Instance.FocusGroup == DetailedWorkbenchInfo.FocusGroups.SlowestCoverStores)
                {
                    var frm = new StoreClassSelectorDialog(editMode);
                    frm.ParentFormType = this.GetType();
                    frm.ShowDetailedForm(base.Instance);
					this.Cursor = Cursors.Default;
					if (frm.StoresAvailable)
						this.Close();
                }
                else
                {
                    var frm = new DetailedWorkbenchForm(editMode);
                    frm.ParentFormType = this.GetType();
                    frm.ShowDetailedForm(base.Instance);
                    this.Cursor = Cursors.Default;
                    this.Close();
                }
            }
            else
                MessageBox.Show(base.Instance.GetValidationErrors(), "Validation Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

		private void Reset(bool resetControls = false)
		{
			this.Cursor = Cursors.WaitCursor;
			Application.DoEvents();

			DepartmentSelector.RefreshControls();
			lstMarkets.RefreshControl();

			if (resetControls)
			{
				base.Instance.Reset();
				base.Instance.Stage = BusinessLayer.DetailedWorkbenchInfo.Stages.FocusGroups;
				ctrlItem.Clear();
				DepartmentSelector.Clear();
			}

			if (base.Instance.FocusGroup == BusinessLayer.DetailedWorkbenchInfo.FocusGroups.Unselected)
				base.Instance.FocusGroup = BusinessLayer.DetailedWorkbenchInfo.FocusGroups.NewLinesReceived;

			rgFocusGroup.EditValue = base.Instance.FocusGroup;

			RefreshControls();
			UpdateControlValues();
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
				ctrlItem.Clear();
				ctrlItem.RefreshCombos();
				this.Cursor = Cursors.Default;
			}
		}
        
        private void RefreshControls()
        {
            this.spinNewLinesReceived.Enabled = false;
            this.spinSlowestCoverStores.Enabled = false;
            this.spinFastestCoverLines.Enabled = false;
            this.spinFastestCoverStores.Enabled = false;
            this.spinSlowestCoverLines.Enabled = false;
            this.spinTopRetailPerformers.Enabled = false;

            switch ((DetailedWorkbenchInfo.FocusGroups) rgFocusGroup.EditValue)
            {
                case DetailedWorkbenchInfo.FocusGroups.NewLinesReceived:
                    spinNewLinesReceived.Enabled = true;
                    ctrlItem.Enabled = true;
                    grpMarket.Enabled = false;
                    chkExcludeItemsWithZeroStock.Enabled = true;
                    chkExcludeItemsWithZeroStock.Checked = true;
					rgParameter.EditValue = DetailedWorkbenchInfo.Parameters.ItemLevel;
					rgParameter.Properties.Items.GetItemByValue(DetailedWorkbenchInfo.Parameters.StyleLevel).Enabled = true;
					rgParameter.Properties.Items.GetItemByValue(DetailedWorkbenchInfo.Parameters.StyleStoreLevel).Enabled = false;
					rgParameter.Properties.Items.GetItemByValue(DetailedWorkbenchInfo.Parameters.ItemLevel).Enabled = true;
					rgParameter.Properties.Items.GetItemByValue(DetailedWorkbenchInfo.Parameters.ItemStoreLevel).Enabled = false;				
                    break;

                case DetailedWorkbenchInfo.FocusGroups.FastestCoverStores:
                    this.spinFastestCoverStores.Enabled = true;
                    ctrlItem.Enabled = false;
                    grpMarket.Enabled = true;
					chkAllMarkets.Checked = true;
                    chkExcludeItemsWithZeroStock.Enabled = true;
                    chkExcludeItemsWithZeroStock.Checked = true;
					rgParameter.EditValue = DetailedWorkbenchInfo.Parameters.ItemStoreLevel;
					rgParameter.Properties.Items.GetItemByValue(DetailedWorkbenchInfo.Parameters.StyleLevel).Enabled = false;
					rgParameter.Properties.Items.GetItemByValue(DetailedWorkbenchInfo.Parameters.StyleStoreLevel).Enabled = true;
					rgParameter.Properties.Items.GetItemByValue(DetailedWorkbenchInfo.Parameters.ItemLevel).Enabled = false;
					rgParameter.Properties.Items.GetItemByValue(DetailedWorkbenchInfo.Parameters.ItemStoreLevel).Enabled = true;
                    break;
                
                case DetailedWorkbenchInfo.FocusGroups.SlowestCoverStores:
                    this.spinSlowestCoverStores.Enabled = true;
                    ctrlItem.Enabled = false;
                    grpMarket.Enabled = true;
					chkAllMarkets.Checked = true;
                    chkExcludeItemsWithZeroStock.Enabled = true;
                    chkExcludeItemsWithZeroStock.Checked = true;
					rgParameter.EditValue = DetailedWorkbenchInfo.Parameters.ItemStoreLevel;
					rgParameter.Properties.Items.GetItemByValue(DetailedWorkbenchInfo.Parameters.StyleLevel).Enabled = false;
					rgParameter.Properties.Items.GetItemByValue(DetailedWorkbenchInfo.Parameters.StyleStoreLevel).Enabled = true;
					rgParameter.Properties.Items.GetItemByValue(DetailedWorkbenchInfo.Parameters.ItemLevel).Enabled = false;
					rgParameter.Properties.Items.GetItemByValue(DetailedWorkbenchInfo.Parameters.ItemStoreLevel).Enabled = true;
                    break;

                case DetailedWorkbenchInfo.FocusGroups.FastestCoverLines:
                    this.spinFastestCoverLines.Enabled = true;
                    ctrlItem.Enabled = true;
                    grpMarket.Enabled = false;
                    chkExcludeItemsWithZeroStock.Enabled = false;
                    chkExcludeItemsWithZeroStock.Checked = true;
					rgParameter.EditValue = DetailedWorkbenchInfo.Parameters.ItemLevel;
					rgParameter.Properties.Items.GetItemByValue(DetailedWorkbenchInfo.Parameters.StyleLevel).Enabled = true;
					rgParameter.Properties.Items.GetItemByValue(DetailedWorkbenchInfo.Parameters.StyleStoreLevel).Enabled = false;
					rgParameter.Properties.Items.GetItemByValue(DetailedWorkbenchInfo.Parameters.ItemLevel).Enabled = true;
					rgParameter.Properties.Items.GetItemByValue(DetailedWorkbenchInfo.Parameters.ItemStoreLevel).Enabled = false;	
                    break;

                case DetailedWorkbenchInfo.FocusGroups.SlowestCoverLines:
                    this.spinSlowestCoverLines.Enabled = true;
                    ctrlItem.Enabled = true;
                    grpMarket.Enabled = false;
                    chkExcludeItemsWithZeroStock.Enabled = false;
                    chkExcludeItemsWithZeroStock.Checked = true;
					rgParameter.EditValue = DetailedWorkbenchInfo.Parameters.ItemLevel;
					rgParameter.Properties.Items.GetItemByValue(DetailedWorkbenchInfo.Parameters.StyleLevel).Enabled = true;
					rgParameter.Properties.Items.GetItemByValue(DetailedWorkbenchInfo.Parameters.StyleStoreLevel).Enabled = false;
					rgParameter.Properties.Items.GetItemByValue(DetailedWorkbenchInfo.Parameters.ItemLevel).Enabled = true;
					rgParameter.Properties.Items.GetItemByValue(DetailedWorkbenchInfo.Parameters.ItemStoreLevel).Enabled = false;	
                    break;

                case DetailedWorkbenchInfo.FocusGroups.TopRetailPerformers:
                    this.spinTopRetailPerformers.Enabled = true;
                    ctrlItem.Enabled = true;
                    grpMarket.Enabled = false;
                    chkExcludeItemsWithZeroStock.Enabled = true;
                    chkExcludeItemsWithZeroStock.Checked = true;
					rgParameter.EditValue = DetailedWorkbenchInfo.Parameters.ItemLevel;
					rgParameter.Properties.Items.GetItemByValue(DetailedWorkbenchInfo.Parameters.StyleLevel).Enabled = true;
					rgParameter.Properties.Items.GetItemByValue(DetailedWorkbenchInfo.Parameters.StyleStoreLevel).Enabled = false;
					rgParameter.Properties.Items.GetItemByValue(DetailedWorkbenchInfo.Parameters.ItemLevel).Enabled = true;
					rgParameter.Properties.Items.GetItemByValue(DetailedWorkbenchInfo.Parameters.ItemStoreLevel).Enabled = false;	
                    break;
            }

			if (ctrlItem.Enabled && ctrlItem.DepartmentId != base.Instance.DepartmentId)
            {
                ctrlItem.DepartmentId = base.Instance.DepartmentId;
                ctrlItem.RefreshCombos();
            }
        }

        private void UpdateControlValues()
        {
            DepartmentSelector.DepartmentId = base.Instance.DepartmentId;
            DepartmentSelector.Workbench = base.Instance.Workbench;
            DepartmentSelector.StoreType = base.Instance.StoreType;

            rgParameter.EditValue = base.Instance.Parameter;
            rgFocusGroup.EditValue = base.Instance.FocusGroup;

            switch (base.Instance.FocusGroup)
            {
                case DetailedWorkbenchInfo.FocusGroups.NewLinesReceived:
                    spinNewLinesReceived.Value = base.Instance.NumberOf;
                    break;
                case DetailedWorkbenchInfo.FocusGroups.FastestCoverStores:
                    spinFastestCoverStores.Value = base.Instance.NumberOf;
                    break;
                case DetailedWorkbenchInfo.FocusGroups.SlowestCoverStores:
                    spinSlowestCoverStores.Value = base.Instance.NumberOf;
                    break;
                case DetailedWorkbenchInfo.FocusGroups.FastestCoverLines:
                    spinFastestCoverLines.Value = base.Instance.NumberOf;
                    break;
                case DetailedWorkbenchInfo.FocusGroups.SlowestCoverLines:
                    spinSlowestCoverLines.Value = base.Instance.NumberOf;
                    break;
                case DetailedWorkbenchInfo.FocusGroups.TopRetailPerformers:
                    spinTopRetailPerformers.Value = base.Instance.NumberOf;
                    break;
            }

            chkAllMarkets.Checked = base.Instance.AllMarkets;
			lstMarkets.SetCheckedItems(base.Instance.Markets);

			ctrlItem.ItemClass = base.Instance.Item.Class;
			ctrlItem.ItemVendor = base.Instance.Item.Vendor;
			ctrlItem.ItemStyle = base.Instance.Item.Style;
			ctrlItem.ItemColour = base.Instance.Item.Colour;
			ctrlItem.ItemSize = base.Instance.Item.Size;

            chkExcludeItemsWithZeroStock.Checked = base.Instance.ExcludeZeroStock;
            chkShowFullPriceLines.Checked = base.Instance.ShowFullPriceLines;
            chkShowMarkDownLines.Checked = base.Instance.ShowMarkedDownLines;
            chkShowProposedMarkedDownLines.Checked = base.Instance.ShowProposedMarkedDownLines;

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
            base.Instance.FocusGroup = (DetailedWorkbenchInfo.FocusGroups) rgFocusGroup.EditValue;

            switch (base.Instance.FocusGroup)
            {
                case DetailedWorkbenchInfo.FocusGroups.NewLinesReceived:
                    base.Instance.NumberOf = spinNewLinesReceived.Value;
                    break;
                case DetailedWorkbenchInfo.FocusGroups.FastestCoverStores:
                    base.Instance.NumberOf = spinFastestCoverStores.Value;
                    break;
                case DetailedWorkbenchInfo.FocusGroups.SlowestCoverStores:
                    base.Instance.NumberOf = spinSlowestCoverStores.Value;
                    break;
                case DetailedWorkbenchInfo.FocusGroups.FastestCoverLines:
                    base.Instance.NumberOf = spinFastestCoverLines.Value;
                    break;
                case DetailedWorkbenchInfo.FocusGroups.SlowestCoverLines:
                    base.Instance.NumberOf = spinSlowestCoverLines.Value;
                    break;
                case DetailedWorkbenchInfo.FocusGroups.TopRetailPerformers:
                    base.Instance.NumberOf = spinTopRetailPerformers.Value;
                    break;
            }           
            
            base.Instance.Item.Class = ctrlItem.ItemClass;
            base.Instance.Item.Vendor = ctrlItem.ItemVendor;
            base.Instance.Item.Style = ctrlItem.ItemStyle;
            base.Instance.Item.Colour = ctrlItem.ItemColour;
            base.Instance.Item.Size = ctrlItem.ItemSize;

            base.Instance.AllMarkets = chkAllMarkets.Checked;
            base.Instance.Markets = lstMarkets.GetCheckedItems();

            base.Instance.ExcludeZeroStock = chkExcludeItemsWithZeroStock.Checked;
            base.Instance.ShowFullPriceLines = chkShowFullPriceLines.Checked;
            base.Instance.ShowMarkedDownLines = chkShowMarkDownLines.Checked;
            base.Instance.ShowProposedMarkedDownLines = chkShowProposedMarkedDownLines.Checked;

        }

        //-----------------------------------------------------------------------------------------
        #endregion
    }
}
