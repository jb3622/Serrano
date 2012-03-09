using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using Disney.iDash.Shared;
using Disney.iDash.SRR.BusinessLayer;

namespace Disney.iDash.SRR.UI.Forms.Common
{
    public partial class BaseForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {

		private SysInfo _sysInfo = new SysInfo();
		public SystemLock SystemLock { get; set; }
		
        public BaseForm()
        {
            InitializeComponent();
			_sysInfo.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
			{
				ErrorDialog.Show(ex, extraInfo, terminateApplication);
			});
        }

        public void ClearStatusMessage()
        {
            barStatus.Visibility = BarItemVisibility.Never;
        }

        public void UpdateStatusMessage(string message = "", bool error = false)
        {
            if (barStatus.Visibility != BarItemVisibility.Always)
                barStatus.Visibility = BarItemVisibility.Always;
            barStatus.Caption = message;
            barStatus.Appearance.ForeColor = (error ? Color.Red : Color.Black);
        }

        private void BaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.DesignMode)
                FormUtils.SaveFormLocation(this, true);

			if (SystemLock != null)
			{
				var unlockResult = _sysInfo.TryUnlock(SystemLock);
				if (!unlockResult.Success)
					ErrorDialog.ShowAlert(unlockResult.FailureReason, "Unlock Failed", ExceptionHandler.AlertType.Error);
			}

        }

        private void BaseForm_Shown(object sender, EventArgs e)
        {
            if (!this.DesignMode)
                FormUtils.RestoreFormLocation(this, true);
        }

    }
}