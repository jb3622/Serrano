using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Disney.iDash.Shared;

namespace Disney.iDash.SRR.UI.Forms.Maintenance
{
    public partial class WorkbenchExtractDialog : Disney.iDash.SRR.UI.Forms.Common.BaseForm
    {
        private BusinessLayer.WorkbenchExtract _workbenchExtract = new BusinessLayer.WorkbenchExtract();

        public WorkbenchExtractDialog()
        {
            InitializeComponent();
            _workbenchExtract.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
                {
                    ErrorDialog.Show(ex, extraInfo);
                });
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ShowForm()
        {
            this.ShowDialog();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (_workbenchExtract.IsExtractRunning())
                MessageBox.Show("Extract is already running, please try later", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            else
            {
                base.UpdateStatusMessage("Running extract...");
                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                var result = false;
                result = _workbenchExtract.Extract(rgExtract.EditValue.ToString());
                this.Cursor = Cursors.Default;

                if (result)
                {
                    base.ClearStatusMessage();
                    MessageBox.Show("Extract(s) running.  You will be notified by email when it is complete.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                    base.UpdateStatusMessage("Extract failed.", true);
            }
        }

    }
}
