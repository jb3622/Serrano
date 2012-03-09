using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Disney.iDash.SRR.BusinessLayer;
using Disney.iDash.Shared;

namespace Disney.iDash.SRR.Controls
{
    public partial class LookupComboControl : DevExpress.XtraEditors.XtraUserControl
    {
        public ExceptionHandler ExceptionHandler = new ExceptionHandler();
        public event EventHandler EditValueChanged;

        public string WhereClause { get; set; }
        public string AllowedCharacters { get; set; }

		private LookupSource.LookupTypes _lookupType = LookupSource.LookupTypes.None;
        private LookupSource _source = new LookupSource();
		private bool _invalidValue = false;

		public LookupComboControl()
        {
            InitializeComponent();
            WhereClause = string.Empty;
            AllowedCharacters = string.Empty;

            _source.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
            {
                ExceptionHandler.RaiseException(ex, extraInfo, terminateApplication);
            });
        }

        public LookupSource.LookupTypes LookupType
        {
            get { return _lookupType; }
            set { _lookupType = value;}
        }

        public new bool Enabled
        {
            get { return cboLookup.Enabled; }
            set 
            { 
                cboLookup.Enabled = value; 
            }
        }

		public bool RefreshControl()
		{
			var result = false;

			try
			{
				if (_lookupType == LookupSource.LookupTypes.None)
				{
					cboLookup.Properties.DataSource = null;
					cboLookup.Properties.ValueMember = string.Empty;
					cboLookup.Properties.DisplayMember = string.Empty;
					cboLookup.EditValue = null;
				}
				else if (!this.DesignMode)
				{

					var info = _source.GetInfo(_lookupType);
					var lastValue = cboLookup.EditValue;

					cboLookup.Properties.ValueMember = "Id";
					if (info.UseValueMemberAsDisplayMember)
						cboLookup.Properties.DisplayMember = "Id";
					else
						cboLookup.Properties.DisplayMember = "Description";

					cboLookup.Properties.DataSource = _source.GetItems(_lookupType, WhereClause);
					cboLookup.EditValue = lastValue;
					result = true;
				}
			}
			catch (Exception ex)
			{
				ExceptionHandler.RaiseException(ex, "RefreshControl");
			}

			return result;
		}

        public int MaxLength
        {
            get { return cboLookup.Properties.MaxLength; }
            set { cboLookup.Properties.MaxLength = value; }
        }

        public object EditValue
        {
            get { return cboLookup.EditValue; }
            set { cboLookup.EditValue = value; }
        }

        public new string Text
        {
            get { return cboLookup.Text; }
        }

        public IStyleController StyleController
        {
            get { return cboLookup.StyleController; }
            set {cboLookup.StyleController = value; }
        }

        private void cboLookup_EditValueChanged(object sender, EventArgs e)
        {
            if (EditValueChanged != null)
                EditValueChanged(sender, e);
        }

        private void cboLookup_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!(AllowedCharacters == string.Empty || AllowedCharacters.IndexOf(e.KeyChar)>0 || e.KeyChar == '\b'));
        }

        private void cboLookup_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
                cboLookup.EditValue = null;
        }

		private void cboLookup_Validating(object sender, CancelEventArgs e)
		{
			e.Cancel = _invalidValue;
			_invalidValue = false;
		}

		private void cboLookup_Popup(object sender, EventArgs e)
		{
			cboLookup.Properties.View.BestFitColumns();
		}

		private void cboLookup_ProcessNewValue(object sender, DevExpress.XtraEditors.Controls.ProcessNewValueEventArgs e)
		{
			if (e.DisplayValue.ToString() != string.Empty)
			{
				cboLookup.ErrorText = "'" + e.DisplayValue + "' is not available";
				MessageBox.Show("'" + cboLookup.Text + "' is not available", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				e.Handled = false;
				_invalidValue = true;
			}
		}

    }
}
