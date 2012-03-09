using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace Disney.iDash.Shared.Forms
{
    internal partial class ErrorDialogForm : Form
    {
        private bool _expanded;
        
        internal ErrorDialogForm()
        {
            InitializeComponent();
            txtMessage.ReadOnly = true;
            txtMessage.ForeColor = Color.Red;
        }

        internal void ShowError(string ErrorMessage)
        {
            ShowError(ErrorMessage, "Error");
        }

        internal void ShowError(string ErrorMessage, string Caption)
        {
            txtMessage.Text = ErrorMessage;
            Text = Caption;
            HideStackTrace();
            ShowDialog();
        }

        internal void ShowError(Exception ex)
        {
            ShowError(ex, "Error");
        }

        internal void ShowError(Exception ex, string caption)
        {
            txtMessage.Text = ex.Message;
            if (ex.InnerException != null)
                txtMessage.Text += "\r\n\r\n" + ex.InnerException.Message;

            if (ex.Data.Count > 0)
                foreach (DictionaryEntry de in ex.Data)
                    this.txtMessage.Text += "\r\n" + de.Key + ": " + de.Value;

            Text = caption;
            txtStackTrace.Text = ex.StackTrace;
            if (txtStackTrace.Text.Length == 0)
                HideStackTrace();
            ShowDialog();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(txtMessage.Text + "\r\n" + txtStackTrace.Text);
            MessageBox.Show("This message has been copied to the clipboard", "Copy", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void HideStackTrace()
        {
            btnStackTrace.Visible = false;
            btnCopy.Visible = false;
            btnSend.Visible = false;
            btnOk.Anchor = AnchorStyles.Bottom;
            btnOk.Left = (Width/2) - (btnOk.Width/2);
        }


        private void btnStackTrace_Click(object sender, EventArgs e)
        {
            if (_expanded)
            {
                Height = 150;
                txtMessage.Visible = true;
                _expanded = false;
            }
            else
            {
                Height = 300;
                txtMessage.Visible = false;
                _expanded = true;
            }
            txtStackTrace.Visible = !txtMessage.Visible;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            var outlook = new Outlook.Application();
            var message = (Outlook.MailItem) outlook.CreateItem(Outlook.OlItemType.olMailItem);
            var assemblyInfo = new Shared.AssemblyInfo();
            message.To = Properties.Settings.Default.HelpDeskEmail;
            message.Subject = "Application Error - " + assemblyInfo.AssemblyProduct;
            message.Body = txtMessage.Text + "\n\n" + txtStackTrace.Text;
            message.Display(true);
        }
       
    }
}