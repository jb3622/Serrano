using Disney.iDash.SRR.BusinessLayer;
using System.Collections.Generic;

namespace Disney.iDash.SRR.Controls
{
    public partial class CriteriaSummaryControl : DevExpress.XtraEditors.XtraUserControl
    {
        public CriteriaSummaryControl()
        {
            InitializeComponent();
        }

        public void SetValues(DetailedWorkbenchInfo workbenchInfo)
        {
            txtStoreType.EditValue = Constants.GetStoreTypeName(workbenchInfo.StoreType);
			txtWorkbench.EditValue = Constants.GetWorkbenchName(workbenchInfo.Workbench);
			txtParameter.EditValue = DetailedWorkbenchInfo.GetParameterName(workbenchInfo.Parameter);

            if (workbenchInfo.FocusGroup != DetailedWorkbenchInfo.FocusGroups.Unselected)
                txtFocusGroup.EditValue = workbenchInfo.NumberOf.ToString() + " " + DetailedWorkbenchInfo.GetFocusGroupName(workbenchInfo.FocusGroup);

            txtDepartment.EditValue = ALL(workbenchInfo.DepartmentText);

            if (workbenchInfo.SelectedItem.StockItem.HasValue)
            {
				txtItemClass.EditValue = ALL(workbenchInfo.SelectedItem.StockItem.Class);
				txtItemVendor.EditValue = ALL(workbenchInfo.SelectedItem.StockItem.Vendor);
				txtItemStyle.EditValue = ALL(workbenchInfo.SelectedItem.StockItem.Style);
				txtItemColour.EditValue = ALL(workbenchInfo.SelectedItem.StockItem.Colour);
				txtItemSize.EditValue = ALL(workbenchInfo.SelectedItem.StockItem.Size);

				txtStore.EditValue = ALL(workbenchInfo.SelectedItem.StoreId);
				txtStoreGrade.EditValue = ALL(workbenchInfo.SelectedItem.GradeId);
				txtMarket.EditValue = ALL(workbenchInfo.SelectedItem.Market);			

            }
            else
            {
                txtItemClass.EditValue = ALL(workbenchInfo.Item.Class);
                txtItemVendor.EditValue = ALL(workbenchInfo.Item.Vendor);
                txtItemStyle.EditValue = ALL(workbenchInfo.Item.Style);
                txtItemColour.EditValue = ALL(workbenchInfo.Item.Colour);
                txtItemSize.EditValue = ALL(workbenchInfo.Item.Size);

		        txtStore.EditValue = ALL(workbenchInfo.StoreId);
	            txtStoreGrade.EditValue = ALL(workbenchInfo.StoreGradeId);

				if (workbenchInfo.Markets.Count > 0)
					txtMarket.EditValue = Selected(workbenchInfo.Markets);
				else
					txtMarket.EditValue = ALL(workbenchInfo.Market);
			}

            txtFranchise.EditValue = ALL(workbenchInfo.FranchiseId);
            txtCoordinateGroup.EditValue = ALL(workbenchInfo.CoordinateGroup);
            txtUPC.EditValue = ALL(workbenchInfo.UPC);
            txtEventCode.EditValue = ALL(workbenchInfo.EventCodeText);
            txtStoreGroup.EditValue = ALL(workbenchInfo.StoreGroup);

            if (workbenchInfo.Promotions.Count == 0)
                txtPromotions.EditValue = "ALL";
            else
                txtPromotions.EditValue = Selected(workbenchInfo.Promotions);

            chkExcludeItemsWithZeroStock.Checked = workbenchInfo.ExcludeZeroStock;
            chkShowProposedMarkedDownLines.Checked = workbenchInfo.ShowProposedMarkedDownLines;
            chkShowMarkDownLines.Checked = workbenchInfo.ShowMarkedDownLines;
            chkShowFullPriceLines.Checked = workbenchInfo.ShowFullPriceLines;
        }

        public void SetFileGroup(DetailedWorkbenchInfo workbenchInfo)
        {
            if (workbenchInfo.DailyFilegroup <= 0)
            {
                txtDailyFileGroup.Text = "N/A";
                txtDailyFileGroup.ToolTip = string.Empty;
                txtDailyFileGroup.ToolTipTitle = string.Empty;
            }
            else
            {
                txtDailyFileGroup.Text = workbenchInfo.DailyFilegroup.ToString("000");
				txtDailyFileGroup.ToolTip = workbenchInfo.GetMember(Constants.Workbenches.Daily);
                txtDailyFileGroup.ToolTipTitle = "Daily Member";
				txtDailyFileGroup.ShowToolTips = true;
            }

            if (workbenchInfo.WeeklyFilegroup <= 0)
            {
                txtWeeklyFileGroup.Text = "N/A";
                txtWeeklyFileGroup.ToolTip = string.Empty;
                txtWeeklyFileGroup.ToolTipTitle = string.Empty;
            }
            else
            {
                txtWeeklyFileGroup.Text = workbenchInfo.WeeklyFilegroup.ToString("000");
				txtWeeklyFileGroup.ToolTip = workbenchInfo.GetMember(Constants.Workbenches.Weekly);
                txtWeeklyFileGroup.ToolTipTitle = "Weekly Member";
				txtWeeklyFileGroup.ShowToolTips = true;
            }
        }

        private string ALL(string value)
        {
            if (string.IsNullOrEmpty(value))
                return "ALL";
            else
                return value;
        }

        private string ALL(decimal? value)
        {
            if (value.HasValue && value.Value > 0)
                return value.ToString();
            else
                return "ALL";
        }

        private string Selected(List<LookupItem> items)
        {
            var result = string.Empty;
            foreach (var item in items)
            {
                if (result != string.Empty)
                    result += ", ";
                result += item.Id.ToString();
    }

            return result;
}

    }
}
