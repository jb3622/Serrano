using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using Disney.iDash.Shared;
using Disney.iDash.SRR.BusinessLayer;
using DevExpress.XtraBars.Ribbon;

namespace Disney.iDash.SRR.UI.Forms.Common
{
    public partial class BaseDataForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private FormStates _formState = FormStates.Idle;
        private CellHighlighter _highlights = new CellHighlighter();
		private SysInfo _sysInfo = new SysInfo();
		public SystemLock SystemLock {get; set;}
				
		public enum FormStates
        {
            Idle,
            Clean,
            Dirty,
            Error
        }

        public BaseDataForm()
        {
            InitializeComponent();
			_sysInfo.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
				{
					ErrorDialog.Show(ex, extraInfo, terminateApplication);
				});
        }

		public CellHighlighter CellHighLights
		{
			get { return _highlights; }
		}

        /// <summary>
        /// FormState indicates the current state the form is in.
        /// 
        /// Idle - waiting for user to enter some information and then click SEARCH.
        ///     The Clear() method will be invoked.
        ///     
        /// Clean - Search results on screen, data in unchanged state
        /// Dirty - Search results changed.
        /// 
        /// RefreshButtons() will be invoked on change of state.
        /// </summary>
        public FormStates FormState
        {
            get { return _formState; }
            set
            {
                if (_formState != value)
                {
                    _formState = value;
                    if (_formState == FormStates.Idle)
                        Clear();

                    if (_formState != FormStates.Dirty)
                        CellHighLights.Clear();

                    RefreshButtons();
                }
            }
        }

        public bool DiscardChanges()
        {
            bool discard = true;

            if (FormUtils.TagContains(this, "ForceClose"))
                this.FormState = FormStates.Idle;

            if (this.FormState == FormStates.Dirty)
                switch (Question.YesNoCancel("Changes have been made.  Do you want to save them?", this.Text))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        if (this.Save())
                        {
                            this.FormState = FormStates.Idle;
                            this.Close();
                        }
                        else
                            discard = false;
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        this.FormState = FormStates.Idle;
                        break;

                    case System.Windows.Forms.DialogResult.Cancel:
                        discard = false;
                        break;
                }
            else
                this.FormState = FormStates.Idle;

            return discard;
        }

        public void ClearStatusMessage()
        {
            barStatus.Visibility = BarItemVisibility.Never;
            barProgress.Visibility = BarItemVisibility.Never;
            this.Cursor = Cursors.Default;
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

        public void ShowSearchingMessage()
        {
            this.Cursor = Cursors.WaitCursor;
            UpdateStatusMessage("Searching...");
            Application.DoEvents();
        }

        public void ShowSavingMessage()
        {
            UpdateStatusMessage("Saving...");
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();
        }

        public void ShowNotFoundMessage()
        {
            UpdateStatusMessage("Not found", true);
            this.Cursor = Cursors.Default;
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

		public void LoadLayout(DevExpress.XtraGrid.Views.Grid.GridView view, string gridName)
		{
			if (!this.DesignMode)
			{
				var regPath = RegUtils.RegBasePath("GridLayout\\" + gridName);
				if (RegUtils.PathExists(regPath))
					view.RestoreLayoutFromRegistry(regPath.ToString());
			}
		}

		public void SaveLayout(DevExpress.XtraGrid.Views.Grid.GridView view, string gridName)
		{
			if (!this.DesignMode)
			{
				var regPath = RegUtils.RegBasePath("GridLayout\\" + gridName);
				view.SaveLayoutToRegistry(regPath.ToString());
			}
		}

		public void ResetLayout(DevExpress.XtraGrid.Views.Grid.GridView view, string gridName)
		{
			RegUtils.DeleteSetting("GridLayout\\" + gridName);
		}

        protected virtual void BaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.DesignMode)
            {
                e.Cancel = !DiscardChanges();
                if (!e.Cancel)
                {
                    FormUtils.SaveFormLocation(this, true);
                    if (SystemLock != null)
                    {
                        var unlockResult = _sysInfo.TryUnlock(SystemLock);
                        if (!unlockResult.Success)
                            ErrorDialog.ShowAlert(unlockResult.FailureReason, "Unlock Failed", ExceptionHandler.AlertType.Error);
                    }
                }
            }
        }

        private void BaseForm_Shown(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                FormUtils.RestoreFormLocation(this, true);
                FormState = FormStates.Idle;
            }
        }

        internal virtual void Clear()
        {
        }

        internal virtual void Search()
        {
        }

        internal virtual void RefreshButtons()
        {
        }

        internal virtual bool Save()
        {
            return true;
        }

    }
}