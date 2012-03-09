using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Disney.iDash.Shared;
using Disney.iDash.LocalData;
using Disney.iDash.SRR.BusinessLayer;
using System.Collections;

namespace Disney.iDash.SRR.Controls
{
    public partial class DepartmentSelectorControl : DevExpress.XtraEditors.XtraUserControl
    {
        public DepartmentSelectorControl()
        {
            InitializeComponent();
            cboDepartment.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
            {
                ErrorDialog.Show(ex, extraInfo, terminateApplication);
            });

            cboWorkbench.Properties.DataSource = new ArrayList(Constants.GetWorkbenches);
            SetupCombo(cboWorkbench);

			cboStoreType.Properties.DataSource = new ArrayList(Constants.GetStoreTypes);
            SetupCombo(cboStoreType);
        }

        public enum ControlNames
        {
            Department, Workbench, StoreType
        }

        public delegate void ControlHandler(ControlNames controlName);

        [Description("Fired when any of the combo values have changed")]
        public event ControlHandler EditValueChanged;

        [Description("Fired when focus is lost from any of the combos")]
        public event ControlHandler ComboLeave;

        public void RefreshControls()
        {
            cboDepartment.WhereClause = string.Format("USUSPR = '{0}'", Session.User.NetworkId.ToUpper());
            cboDepartment.RefreshControl();
        }

		internal void Clear()
		{
			DepartmentId = -1m;
			StoreType = Constants.StoreTypes.BricksAndMortar;
			Workbench = Constants.Workbenches.Daily;
		}

        public Decimal DepartmentId
        {
            get 
            { 
				decimal Id = -1;
                if (cboDepartment.EditValue == null || !Decimal.TryParse(cboDepartment.EditValue.ToString(), out Id))
                    return -1;
                else
                    return Id; 
            }
            set 
            { 
                cboDepartment.EditValue = value; 
            }
        }

        public string DepartmentText
        {
            get { return cboDepartment.Text; }
        }

        public Constants.Workbenches Workbench
        {
            get
            {
                if (cboWorkbench.EditValue == null)
					return Constants.Workbenches.Daily;
                else
					return (Constants.Workbenches)cboWorkbench.EditValue;
            }
            set 
            { 
                cboWorkbench.EditValue = value; 
            }
        }

		public Constants.StoreTypes StoreType
        {
            get
            {
                if (cboStoreType.EditValue == null)
					return Constants.StoreTypes.BricksAndMortar;
                else
					return (Constants.StoreTypes)cboStoreType.EditValue;
            }
            set 
            { 
                cboStoreType.EditValue = value; 
            }
        }

        public IStyleController StyleController
        {
            get { return cboDepartment.StyleController; }
            set 
            { 
                cboDepartment.StyleController = value;
                cboWorkbench.StyleController = value;
                cboStoreType.StyleController = value;
            }
        }

        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            if (EditValueChanged != null)
                EditValueChanged(ControlNames.Department);
        }

        private void cboWorkbench_EditValueChanged(object sender, EventArgs e)
        {
            if (EditValueChanged != null)
                EditValueChanged(ControlNames.Workbench);
        }

        private void cboStoreType_EditValueChanged(object sender, EventArgs e)
        {
            if (EditValueChanged != null)
                EditValueChanged(ControlNames.StoreType);
        }

        private void cboDepartment_Leave(object sender, EventArgs e)
        {
            if (ComboLeave != null)
                ComboLeave(ControlNames.Department);
        }

        private void cboWorkbench_Leave(object sender, EventArgs e)
        {
            if (ComboLeave != null)
                ComboLeave(ControlNames.Workbench);
        }

        private void cboStoreType_Leave(object sender, EventArgs e)
        {
            if (ComboLeave != null)
                ComboLeave(ControlNames.StoreType);
        }

        private void SetupCombo(LookUpEdit combo)
        {
            combo.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Value"));
            combo.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Key"));
            combo.Properties.Columns["Key"].Visible = false;
            combo.Properties.DisplayMember = "Value";
            combo.Properties.ValueMember = "Key";
            combo.Properties.ShowHeader = false;
            combo.Properties.ShowFooter = false;
        }
	}
}
