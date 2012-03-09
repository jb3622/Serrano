using System;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Disney.iDash.DataLayer;
using Disney.iDash.LocalData;
using Disney.iDash.Shared;
using Disney.iDash.SRR.BusinessLayer;
using System.Collections.Generic;

namespace Disney.iDash.Framework.Forms
{
    public partial class LoginDialog : DevExpress.XtraEditors.XtraForm
    {
        protected const int MaxAttempts = 3;
        protected int _attempts = 0;
        
        protected LocalDataEntities _entities = null;

        protected DB2Factory _factory = new DB2Factory();
		protected SysInfo _sysInfo = new SysInfo();

		#region Public methods 
		//-----------------------------------------------------------------------------------------
		public LoginDialog()
        {
            InitializeComponent();
            _factory.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
                {
                    ErrorDialog.Show(ex, extraInfo);
                });

			_sysInfo.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
				{
					ErrorDialog.Show(ex, extraInfo);
				});
        }

        public virtual bool ShowForm()
        {
            _entities = new LocalDataEntities(Session.LocalDataConnection);

            this.txtNetworkId.Text = System.Environment.UserName;
            RefreshEnvironmentCombo();
            ShowVersionNumber();
            groupLogin.Visible = true;

            return this.ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }
		//-----------------------------------------------------------------------------------------
		#endregion

		#region Protected event handlers
		//-----------------------------------------------------------------------------------------
        protected virtual void btnLogin_Click(object sender, EventArgs e)
        {
            _attempts++;
            UpdateStatusMessage("Authorising...");
            this.Cursor = Cursors.WaitCursor;
            System.Windows.Forms.Application.DoEvents();

            var environment = _entities.eEnvironments.Where((env) => (env.EnvironmentName == cboEnvironment.Text)).FirstOrDefault();
            var validUser = _factory.TestConnection(txtNetworkId.Text, txtPassword.Text, environment.Hostname, environment.Database, string.Join(",", environment.LibraryList));
            this.Cursor = Cursors.Default;

			if (validUser)
			{

                Session.User = _entities.eUsers.Where((u) => (u.NetworkId.ToLower() == txtNetworkId.Text.ToLower())).FirstOrDefault();
				if (Session.User != null && Session.User.Active.HasValue && Session.User.Active.Value)
				{
					Session.Environment = environment;
					if (_sysInfo.IsSystemAvailable())
					{
						UpdateStatusMessage("Success!");
						Thread.Sleep(2000);
						this.DialogResult = DialogResult.OK;
						this.Close();
					}
					else
					{
						UpdateStatusMessage("System is unavailable", true);
						validUser = false;
					}
				}
				else
				{
					UpdateStatusMessage("User is not active", true);
					validUser = false;
				}
			}
			else
			{
				UpdateStatusMessage(_factory.ExceptionHandler.LastException.Message, true);
				_factory.ExceptionHandler.LogException(_factory.ExceptionHandler.LastException, "Login");
			}

            if (!validUser && _attempts > MaxAttempts)
            {
                UpdateStatusMessage("Too many login attempts", true);
                Thread.Sleep(2000);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

		protected void LoginDialog_Shown(object sender, EventArgs e)
		{
			if (System.Diagnostics.Debugger.IsAttached)
			{
				var encryption = new EncryptData();
				txtPassword.EditValue = encryption.DecryptString(RegUtils.GetCustomSetting("DevPwd", ""));
			}
            timer1.Enabled = true;
        }

		protected void txtUsername_Leave(object sender, EventArgs e)
		{
			RefreshEnvironmentCombo();
		}

		protected void txtNetworkId_EditValueChanged(object sender, EventArgs e)
		{
			RefreshButtons();
		}

		protected void txtPassword_EditValueChanged(object sender, EventArgs e)
		{
			RefreshButtons();
		}

		protected void cboEnvironment_EditValueChanged(object sender, EventArgs e)
		{
			RefreshButtons();
		}

		//-----------------------------------------------------------------------------------------
		#endregion

		#region Protected methods
		//-----------------------------------------------------------------------------------------
		protected void RefreshButtons()
        {
			btnLogin.Enabled = txtPassword.Text.Length > 0 && txtNetworkId.Text.Length > 0 && cboEnvironment.EditValue != null;
        }

        protected virtual void ShowVersionNumber()
        {
            if (ApplicationDeployment.IsNetworkDeployed)
				UpdateStatusMessage( "v" + ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(4));
			else
				UpdateStatusMessage("v" + Assembly.GetEntryAssembly().GetName().Version.ToString());
        }

        protected void RefreshEnvironmentCombo()
        {
            cboEnvironment.Properties.DataSource = null;
            
			if (txtNetworkId.Text != string.Empty)
			{
                var qry = from user in _entities.eUsers
                          from env in user.vEnvironments
                          where user.NetworkId.ToLower() == txtNetworkId.Text.ToLower()
                          orderby env.EnvironmentName                          
                          select env;

                var data = qry.ToList<eEnvironment>();
                cboEnvironment.Properties.DataSource = data;

                if (data.Count() > 0)
                {
                    var lastEnvironment = data.Where((e) => (e.EnvironmentName == Session.LastEnvironmentName)).FirstOrDefault();
                    if (lastEnvironment == null)
                        cboEnvironment.EditValue = data[0].Id;
                    else
                        cboEnvironment.EditValue = lastEnvironment.Id;
                }
                else
                    cboEnvironment.EditValue = null;
			}
			else
				cboEnvironment.EditValue = null;
        }

        protected void UpdateStatusMessage(string message, bool error = false)
        {
            Graphics graphics = this.CreateGraphics();
            SizeF textSize = graphics.MeasureString(message, this.LoginStatusMessage.Font);

            if (textSize.Width > this.Width * 0.9)
            {
                this.LoginStatusMessage.Text = string.Empty;
                MessageBox.Show(message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.LoginStatusMessage.Text = message;
                this.LoginStatusMessage.ForeColor = (error ? Color.Red : Color.Blue);
                System.Windows.Forms.Application.DoEvents();
            }
        }

        protected void LoginDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cboEnvironment.EditValue != null)
                Session.LastEnvironmentName = cboEnvironment.Text;

			if (System.Diagnostics.Debugger.IsAttached)
			{
				var encryption = new EncryptData();
				RegUtils.SetCustomSetting("DevPwd", encryption.EncryptString(txtPassword.EditValue.ToString()));
			}
		}
		//-----------------------------------------------------------------------------------------
		#endregion
	}
}