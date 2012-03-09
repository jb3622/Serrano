/*
 * Author:  Marc Levine
 * Project: SRR
 * Date:    From March 2011
 * 
 * Description
 * Provides base functionality to all DetailedFocusGroups, DetailedParameters, DetailedWorkbench forms
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using Disney.iDash.SRR.BusinessLayer;
using Disney.iDash.Shared;
using DevExpress.Utils;

namespace Disney.iDash.SRR.UI.Forms.Common
{
    public partial class BaseParameters : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        private DetailedWorkbenchInfo _instance = new DetailedWorkbenchInfo();
        private Type _parentFormType = null;
        private bool _dirty = false;
        private bool _hasErrors = false;

        public BaseParameters()
        {
            InitializeComponent();

            _instance.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
                {
                    ErrorDialog.Show(ex, extraInfo, terminateApplication);
                });

            _instance.ExceptionHandler.AlertEvent += ((message, caption, alertType) =>
                {
                    ErrorDialog.ShowAlert(message, caption, alertType);
                });

			_instance.ChangedEvent +=((sender, e)=>
				{
					this.IsDirty = _instance.IsDirty;
				});
        }

		public bool Setup()
		{
			return _instance.Setup();
		}

		public DetailedWorkbenchInfo Instance
        {
            get { return _instance; }
            set 
            {
				if (value != null)
					_instance = value;
            }
        }

        public void ClearStatusMessage()
        {
			if (this.InvokeRequired)
			{
				MethodInvoker mi = delegate { ClearStatusMessage(); };
				this.Invoke(mi);
			}
			else
			{
				barStatus.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
				barProgress.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
			}
        }

        public void UpdateStatusMessage(string message = "", bool error = false)
        {
			if (this.InvokeRequired)
			{
				MethodInvoker mi = delegate { UpdateStatusMessage(message, error); };
				this.Invoke(mi);
			}
			else
			{
				if (message == string.Empty)
					barStatus.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
				else
					barStatus.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

				barStatus.Caption = message;
				barStatus.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Content;
				barStatus.Appearance.ForeColor = (error ? Color.Red : Color.Black);
				Application.DoEvents();
			}
        }

        public void UpdateProgress(string message, int percentageComplete = 0)
        {
			if (this.InvokeRequired)
			{
				MethodInvoker mi = delegate { UpdateProgress(message, percentageComplete); };
				this.Invoke(mi);
			}
			else
			{
				barProgress.EditValue = percentageComplete;

				if (percentageComplete == 0)
					barProgress.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
				else
					barProgress.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
				barProgress.Refresh();
			}
			UpdateStatusMessage(message);
		}

        public Type ParentFormType
        {
            get { return _parentFormType; }
            set { _parentFormType = value; }
        }

        public virtual void ShowDetailedForm(DetailedWorkbenchInfo workbenchInfo)
        {

        }

        public virtual void ShowSummaryForm(SummaryWorkbenchInfo summaryWorkbenchInfo)
        {

        }

        public void LoadLayout(DevExpress.XtraGrid.Views.Grid.GridView view, string gridName, bool clearSortFilterGrouping = false)
        {
            if (!this.DesignMode)
            {
                var regPath = RegUtils.RegBasePath("GridLayout\\" + gridName);
				if (RegUtils.PathExists(regPath))
				{
					view.RestoreLayoutFromRegistry(regPath.ToString());
					if (clearSortFilterGrouping)
					{
						view.ClearColumnsFilter();
						view.ClearGrouping();
						view.ClearSorting();
					}
				}
            }
        }

        public void SaveLayout(DevExpress.XtraGrid.Views.Grid.GridView view, string gridName)
        {
            if (!this.DesignMode)
            {
                var regPath = RegUtils.RegBasePath("GridLayout\\" + gridName);
				var options = new OptionsLayoutGrid();
                options.Columns.RemoveOldColumns = true;
                options.Columns.AddNewColumns = true;
                options.StoreDataSettings = false;
                view.SaveLayoutToRegistry(regPath.ToString(), options);
            }
        }

        public void ResetLayout(DevExpress.XtraGrid.Views.Grid.GridView view, string gridName)
        {
            RegUtils.DeleteSetting("GridLayout\\" + gridName);
            view.ClearColumnsFilter();
            view.ClearGrouping();
            view.ClearSorting();

            if (view.GetType() == typeof(DevExpress.XtraGrid.Views.BandedGrid.BandedGridView))
            {
                var bandedView = (DevExpress.XtraGrid.Views.BandedGrid.BandedGridView) view;
                foreach (DevExpress.XtraGrid.Views.BandedGrid.GridBand band in bandedView.Bands)
                    band.Visible=true;
        }
        }
        
        public void ShowParentForm()
        {
			Application.UseWaitCursor = true;
			Application.DoEvents();

			var frm = Activator.CreateInstance(this.ParentFormType) as Common.BaseParameters;
            if (frm != null)
            {
                frm.ShowDetailedForm(this.Instance);
                frm.Activate();
				frm.BringToFront();
				frm.Focus();
            }
            else
                MessageBox.Show("Cannot show parent form", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

			Application.UseWaitCursor = false;
        }

        public void RestoreFormLocation()
        {
            if (!this.DesignMode)
                FormUtils.RestoreFormLocation(this, true);
        }

        public bool IsDirty
        {
            get { return _dirty; }
            set { _dirty = value; }
        }

        public bool HasErrors
        {
            get { return _hasErrors; }
            set { _hasErrors = value; }
        }

        public bool GoBack()
        {
            var proceed = true;

			if (this.IsDirty)
				switch (ConfirmQuit())
				{
					case System.Windows.Forms.DialogResult.Yes:
						if (this.Instance.ApplyChanges())
						{
							this.IsDirty = false;
							proceed = true;
						}
						break;

					case System.Windows.Forms.DialogResult.No:
						this.Instance.DiscardChanges();
						this.IsDirty = false;
						proceed = true;
						break;

					case System.Windows.Forms.DialogResult.Cancel:
						proceed = false;
						break;
				}
			else
				// Always unlock items even if nothing changed to ensure we remove any filegroups.
				this.Instance.ReleaseLocks();

            return proceed;
        }

        private DialogResult ConfirmQuit()
        {
            return Question.YesNoCancel("Changes have been made.  Do you want to save them?", this.Text);
        }

        private void BaseParameters_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.DesignMode)
            {
                if (FormUtils.TagContains(this, "ForceClose"))
                   this.IsDirty = false;

                if (this.IsDirty)
                    switch (ConfirmQuit())
                    {
                        case System.Windows.Forms.DialogResult.Yes:
                            if (this.Instance.ApplyChanges())
                                this.IsDirty = false;
                            else
                                e.Cancel = true;
                            break;

                        case System.Windows.Forms.DialogResult.No:
							this.Instance.DiscardChanges();
                            this.IsDirty = false;
                            break;

                        case System.Windows.Forms.DialogResult.Cancel:
                            e.Cancel = true;
                            break;
                    }
				else
					// Always unlock items even if nothing changed to ensure we remove any filegroups.
					this.Instance.ReleaseLocks();

                if (!e.Cancel)
                    FormUtils.SaveFormLocation(this, true);
            }
        }

    }
}