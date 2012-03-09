using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Disney.iDash.SRR.BusinessLayer;
using Disney.iDash.Shared;

namespace Disney.iDash.SRR.UI.Forms
{
    public partial class Toolbar : Disney.iDash.BaseClasses.Forms.AddinMenu
    {
		private SysInfo _sysInfo = new SysInfo();

        public Toolbar()
        {
            InitializeComponent();
			_sysInfo.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication)=>
				{
					ErrorDialog.Show(ex, extraInfo, terminateApplication);
				});
           
        }

        private void barSummary_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!IsLoading)
            {
                IsLoading = true;
                var frm = new Forms.Workbench.SummaryWorkbenchForm();
				frm.Tag = rpSRR.Text;
                frm.ShowSummaryForm(new SummaryWorkbenchInfo());
                IsLoading = false;
            }
        }

        private void barDetailedParameters_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!IsLoading)
            {
                IsLoading = true;
				var frm = new Forms.Workbench.DetailedParametersForm();
				frm.Tag = rpSRR.Text;
				frm.ShowDetailedForm(null);
                IsLoading = false;
            }
        }

        private void barDetailedFocusGroups_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!IsLoading)
            {
                IsLoading = true;
                var frm = new Forms.Workbench.DetailedFocusGroupsForm();
				frm.Tag = rpSRR.Text;
                frm.ShowDetailedForm(null);
                IsLoading = false;
            }
        }

        private void barStowaway_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
			if (!IsLoading)
			{
				IsLoading = true;
				var frm = (Forms.Workbench.StowawayStoresForm)FormUtils.SelectInstance(new Forms.Workbench.StowawayStoresForm());

				if (frm.SystemLock == null)
				{
					var systemLock = _sysInfo.SetSystemLock(rpSRR.Text, e.Item.Caption);
					if (systemLock.Success)
					{
						frm.SystemLock = systemLock;
						frm.Tag = rpSRR.Text;
						frm.Setup();
						frm.ShowForm();
					}
					else
						MessageBox.Show(systemLock.FailureReason, e.Item.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}
				IsLoading = false;
			}

        }

        private void barManualAPP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!IsLoading)
            {
                IsLoading = true;
				var frm = new Forms.Workbench.ManualAPPAllocationReleaseForm();
				frm.Tag = rpSRR.Text;
                frm.ShowForm();
                IsLoading = false;
            }
        }

        private void barStowawayAPP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!IsLoading)
            {
                IsLoading = true;
                var frm = new Forms.Workbench.ManualAPPAllocationReleaseForm();
				frm.Tag = rpSRR.Text;
                frm.ShowForm(true);
                IsLoading = false;
            }
        }

        private void barWorkBenchExtract_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!IsLoading)
            {
                IsLoading = true;
				var frm = (Forms.Maintenance.WorkbenchExtractDialog)FormUtils.SelectInstance(new Forms.Maintenance.WorkbenchExtractDialog());

				if (frm.SystemLock == null)
				{
					var systemLock = _sysInfo.SetSystemLock(rpSRR.Text, e.Item.Caption);

					if (systemLock.Success)
					{
						frm.SystemLock = systemLock;
						frm.Tag = rpSRR.Text;
						frm.ShowForm();
					}
					else
						MessageBox.Show(systemLock.FailureReason, e.Item.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}
                IsLoading = false;
            }
        }

        private void barItemStoreAssignment_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!IsLoading)
            {
                IsLoading = true;
				var frm = (Forms.Maintenance.StockItemStoreAssignmentForm)FormUtils.SelectInstance(new Forms.Maintenance.StockItemStoreAssignmentForm());

				if (frm.SystemLock == null)
				{
					var systemLock = _sysInfo.SetSystemLock(rpSRR.Text, e.Item.Caption);

					if (systemLock.Success)
					{
						frm.SystemLock = systemLock;
						frm.Tag = rpSRR.Text;
						frm.Setup();
						frm.ShowForm();
					}
					else
						MessageBox.Show(systemLock.FailureReason, e.Item.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}
				IsLoading = false;

            }
        }

        private void barUserDeptAuthority_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!IsLoading)
            {
                IsLoading = true;

				var frm = (Forms.Maintenance.UserDeptAssignmentForm)FormUtils.SelectInstance(new Forms.Maintenance.UserDeptAssignmentForm());

				if (frm.SystemLock == null)
				{
					var systemLock = _sysInfo.SetSystemLock(rpSRR.Text, e.Item.Caption);

					if (systemLock.Success)
					{
						frm.SystemLock = systemLock;
						frm.Tag = rpSRR.Text;
						frm.Setup();
						frm.ShowForm();
					}
					else
						MessageBox.Show(systemLock.FailureReason, e.Item.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}
                IsLoading = false;
            }
        }

        private void barMinDispQty_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!IsLoading)
            {
                IsLoading = true;
				var frm = (Forms.Maintenance.StockItemMinDisplayQtyForm)FormUtils.SelectInstance(new Forms.Maintenance.StockItemMinDisplayQtyForm());

				if (frm.SystemLock == null)
				{
					var systemLock = _sysInfo.SetSystemLock(rpSRR.Text, e.Item.Caption);

					if (systemLock.Success)
					{
						frm.SystemLock = systemLock;
						frm.Tag = rpSRR.Text;
						frm.Setup();
						frm.ShowForm();
					}
					else
						MessageBox.Show(systemLock.FailureReason, e.Item.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}
                IsLoading = false;
            }
        }

        private void barStoreLeadTime_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!IsLoading)
            {
                IsLoading = true;
				var frm = (Forms.Maintenance.StoreLeadTimeForm)FormUtils.SelectInstance(new Forms.Maintenance.StoreLeadTimeForm());

				if (frm.SystemLock == null)
				{
					var systemLock = _sysInfo.SetSystemLock(rpSRR.Text, e.Item.Caption);

					if (systemLock.Success)
					{
						frm.SystemLock = systemLock;
						frm.Tag = rpSRR.Text;
						frm.Setup();
						frm.ShowForm();
					}
					else
						MessageBox.Show(systemLock.FailureReason, e.Item.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}
                IsLoading = false;
            }
        }

        private void barDeptStoreGrades_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!IsLoading)
            {
                IsLoading = true;
				var frm = (Forms.Maintenance.DeptStoreGradeForm)FormUtils.SelectInstance(new Forms.Maintenance.DeptStoreGradeForm());

				if (frm.SystemLock == null)
				{
					var systemLock = _sysInfo.SetSystemLock(rpSRR.Text, e.Item.Caption);

					if (systemLock.Success)
					{
						frm.SystemLock = systemLock;
						frm.Tag = rpSRR.Text;
						frm.Setup();
						frm.ShowForm();
					}
					else
						MessageBox.Show(systemLock.FailureReason, e.Item.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}
                IsLoading = false;
            }
        }

        private void barStoreDeletionSchedule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!IsLoading)
            {
                IsLoading = true;
				var frm = (Forms.Maintenance.StoreDeleteScheduleForm)FormUtils.SelectInstance(new Forms.Maintenance.StoreDeleteScheduleForm());

				if (frm.SystemLock == null)
				{
					var systemLock = _sysInfo.SetSystemLock(rpSRR.Text, e.Item.Caption);

					if (systemLock.Success)
					{
						frm.SystemLock = systemLock;
						frm.Tag = rpSRR.Text;
						frm.Setup();
						frm.ShowForm();
					}
					else
						MessageBox.Show(systemLock.FailureReason, e.Item.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}
                IsLoading = false;
            }
        }

        private void barStoreGroups_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!IsLoading)
            {
                IsLoading = true;
                var frm = (Forms.Maintenance.StoreGroupsForm)FormUtils.SelectInstance(new Forms.Maintenance.StoreGroupsForm());

                if (frm.SystemLock == null)
                {
                    var systemLock = _sysInfo.SetSystemLock(rpSRR.Text, e.Item.Caption);

                    if (systemLock.Success)
                    {
                        frm.SystemLock = systemLock;
                        frm.Tag = rpSRR.Text;
                        frm.Setup();
                        frm.ShowForm();
                    }
                    else
                        MessageBox.Show(systemLock.FailureReason, e.Item.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                IsLoading = false;
            }
        }

    }
}
