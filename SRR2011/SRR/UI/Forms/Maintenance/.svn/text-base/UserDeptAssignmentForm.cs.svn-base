using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Disney.iDash.Shared;
using Disney.iDash.LocalData;
using Disney.iDash.SRR.BusinessLayer;

namespace Disney.iDash.SRR.UI.Forms.Maintenance
{
    public partial class UserDeptAssignmentForm : Disney.iDash.SRR.UI.Forms.Common.BaseDataForm
    {
        private UserDeptInfo _userDept = new UserDeptInfo();

        #region Public Methods
        //-----------------------------------------------------------------------------------------
        public UserDeptAssignmentForm()
        {
            InitializeComponent();
        }

        public void Setup()
        {

			viewItems.Appearance.EvenRow.BackColor = Properties.Settings.Default.EvenRowBackColor;
			viewItems.Appearance.OddRow.BackColor = Properties.Settings.Default.OddRowBackColor;
			viewItems.Appearance.FocusedRow.BackColor = Properties.Settings.Default.FocusedRowBackColor;

			btnAdd.DataBindings.Add("Enabled", gridItems.EmbeddedNavigator.Buttons.ButtonByButtonType(DevExpress.XtraEditors.NavigatorButtonType.Append), "Enabled");
			btnDelete.DataBindings.Add("Enabled", gridItems.EmbeddedNavigator.Buttons.ButtonByButtonType(DevExpress.XtraEditors.NavigatorButtonType.Remove), "Enabled");
			
			cboDept.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
            {
                ErrorDialog.Show(ex, extraInfo, terminateApplication);
            });

            _userDept.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
            {
                ErrorDialog.Show(ex, extraInfo, terminateApplication);
            });

            _userDept.ChangedEvent += ((sender, e) =>
                {
                    base.FormState = FormStates.Dirty;
                });

			_userDept.ProgressEvent += ((Message, percentageComplete)=>
				{
					UpdateProgress(Message, percentageComplete);
				});

            lstDepartments.OnChanged += ((sender, e) =>
                {
                    base.FormState = FormStates.Dirty;
					_userDept.IsDirty = true;
                });

            lstDepartments.OnMoveItem += ((source, target, selectedItems, moveAll) =>
                {
                    var moved = false;
                    var sourceRows = (DataTable)source.DataSource;
                    var targetRows = (DataTable)target.DataSource;

                    if (moveAll)
                    {
                        foreach (DataRow row in sourceRows.Rows)
							if (row.RowState != DataRowState.Deleted)
								targetRows.Rows.Add(row.ItemArray);
                        sourceRows.Rows.Clear();
                        moved = true;
                    }
                    else
                    {

                        var temp = new List<DataRow>();

                        foreach (DataRowView item in selectedItems)
                        {
                            temp.Add(item.Row);
                            targetRows.Rows.Add(item.Row.ItemArray);
                        }

                        foreach (DataRow row in temp)
                        {
                            var row1 = sourceRows.Select("ID=" + row["ID"].ToString());
                            if (row1 != null && row1.Length > 0)
                                row1[0].Delete();
                        }

                        moved = true;
                    }

                    return moved;
                });

            // Refresh the IP User Combo
            var lookupSource = new LookupSource();
            lookupSource.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
                {
                    ErrorDialog.Show(ex, extraInfo, terminateApplication);
                });

            riUSIPPR.DataSource = lookupSource.GetItems(LookupSource.LookupTypes.IPUsers);

        }

        public void ShowForm()
        {
            this.MdiParent = FormUtils.FindMdiParent();
            RefreshControls();
            this.Show();
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region Private event handlers
        //-----------------------------------------------------------------------------------------
        private void btnSearch_Click(object sender, EventArgs e)
        {
            base.ShowSearchingMessage();

            if (rbDepartment.Checked)
                RefreshGrid();
            else
                RefreshList();
           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbDepartment_CheckedChanged(object sender, EventArgs e)
        {
            RefreshButtons();
        }

        private void rbAS400_CheckedChanged(object sender, EventArgs e)
        {
            RefreshButtons();
        }

        private void rbIPName_CheckedChanged(object sender, EventArgs e)
        {
            RefreshButtons();
        }

		private void txtAS400Profile_EditValueChanged(object sender, EventArgs e)
		{
			RefreshButtons();
		}

        private void cboDept_EditValueChanged(object sender, EventArgs e)
        {
            txtAS400Profile.EditValue = null;
            cboIPUsers.EditValue = null;
            RefreshButtons();
        }

        private void cboIPUsers_EditValueChanged(object sender, EventArgs e)
        {
            cboDept.EditValue = null;
            RefreshButtons();
        }

        private void txtIPProfile_EditValueChanged(object sender, EventArgs e)
        {
            cboDept.EditValue = null;
            RefreshButtons();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.DiscardChanges();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void gridItems_VisibleChanged(object sender, EventArgs e)
        {
            btnDelete.Visible = gridItems.Visible;
            btnAdd.Visible = gridItems.Visible;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            viewItems.AddNewRow();
            viewItems.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Question.YesNo("Delete selected rows?", this.Text))
                viewItems.DeleteSelectedRows();
        }

		// don't allow users to edit an existing entry.
		private void viewItems_ShowingEditor(object sender, CancelEventArgs e)
		{
			e.Cancel = !(viewItems.IsNewItemRow(viewItems.FocusedRowHandle));
			if (e.Cancel)
				riUSIPPR.Buttons[0].Visible=false;
			else
				riUSIPPR.Buttons[0].Visible=true;
		}

		private void viewItems_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
		{
			if (e.Row != null)
				try
				{
					var row = ((DataRowView)e.Row).Row;
					if (row["USUSPR"] == null || row["USUSPR"].ToString() == string.Empty)
					{
						e.Valid = false;
						e.ErrorText = "Please enter an AS400 user ID";
					}
					else if (row["USIPPR"] == null || row["USIPPR"].ToString() == string.Empty)
					{
						e.Valid = false;
						e.ErrorText = "Please select an IP name";
					}
					else
					{
						if (_userDept.CheckAssociation((DataTable) gridItems.DataSource, row["USUSPR"].ToString(), row["USIPPR"].ToString()))
						{
							e.Valid = false;
							e.ErrorText = row["USIPPR"].ToString() + " has been associated with another AS400 user";
						}
					}
				}
				catch (Exception ex)
				{
					ErrorDialog.Show(ex, "ValidateRow");
				}

		}

		private void viewItems_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
		{
			if (e.ErrorText.Contains("constrained") && e.ErrorText.Contains("unique"))
			{
				e.WindowCaption = "Please correct the error";
				e.ErrorText = "A user may be associated with a department only once.";
			}
			
		}
        //-----------------------------------------------------------------------------------------
        #endregion

        #region Overrides
        //-----------------------------------------------------------------------------------------
        internal override void Clear()
        {
            rbDepartment.Checked = true;
            cboDept.EditValue = null;
            txtAS400Profile.EditValue = null;
            cboIPUsers.EditValue = null;

            gridItems.DataSource = null;
            gridItems.Visible = false;

            lstDepartments.Visible = false;
        }

        internal override void RefreshButtons()
        {
            btnSave.Enabled = base.FormState == FormStates.Dirty;
            btnCancel.Enabled = base.FormState != FormStates.Idle;
			btnSearch.Enabled = base.FormState == FormStates.Idle && (cboDept.EditValue != null || txtAS400Profile.EditValue != null || cboIPUsers.EditValue != null);
			
            cboDept.Enabled = rbDepartment.Checked && base.FormState == FormStates.Idle;
			txtAS400Profile.Enabled = rbAS400.Checked && base.FormState == FormStates.Idle;
			cboIPUsers.Enabled = rbIPName.Checked && base.FormState == FormStates.Idle;

			rbAS400.Enabled = base.FormState == FormStates.Idle;
			rbDepartment.Enabled = base.FormState == FormStates.Idle;
			rbIPName.Enabled = base.FormState == FormStates.Idle;

        }

        internal override bool Save()
        {
            base.ShowSavingMessage();
            
            var saved = _userDept.Save();
            if (saved)
                base.FormState = FormStates.Idle;

            base.ClearStatusMessage();

            return saved;
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region Private methods
        //-----------------------------------------------------------------------------------------
        private void RefreshControls()
        {
			cboDept.WhereClause = "1=1";
//            cboDept.WhereClause = string.Format("USUSPR = '{0}'", Session.User.NetworkId.ToUpper());
            cboDept.RefreshControl();
            cboIPUsers.RefreshControl();
            cboIPUsers.Enabled = false;
        }

        private void RefreshGrid()
        {
            viewItems.ClearColumnsFilter();
            viewItems.ClearGrouping();
            viewItems.ClearSorting();

            if (cboDept.EditValue != null && _userDept.Search((decimal)cboDept.EditValue))
            {
                gridItems.DataSource = _userDept.GetAssignedUsers;
                gridItems.Visible = true;
                lstDepartments.Visible = false;
                base.FormState = FormStates.Clean;
                base.ClearStatusMessage();
            }
            else
            {
                base.FormState = FormStates.Idle;
                base.UpdateStatusMessage("Not found", true);
            }
        }

        private void RefreshList()
        {
            if ((rbAS400.Checked && txtAS400Profile.EditValue != null && _userDept.Search(txtAS400Profile.EditValue.ToString(), false))
                || (rbIPName.Checked && cboIPUsers.EditValue != null && _userDept.Search(cboIPUsers.EditValue.ToString(), true)))
            {

                txtAS400Profile.EditValue = _userDept.NetworkId;
                cboIPUsers.EditValue = _userDept.IPID;

                lstDepartments.AvailableDataSource = _userDept.GetAvailableDepartments;
                lstDepartments.AvailableDisplayMember = "DEPARTMENT";
                lstDepartments.AvailableValueMember = "ID";
                lstDepartments.SelectedDataSource = _userDept.GetSelectedDepartments;
                lstDepartments.SelectedDisplayMember = "DEPARTMENT";
                lstDepartments.SelectedValueMember = "ID";

                lstDepartments.Visible = true;
                gridItems.Visible = false;

                base.FormState = FormStates.Clean;
                base.ClearStatusMessage();
            }
            else
            {
                base.FormState = FormStates.Idle;
                this.Cursor = Cursors.Default;
                base.UpdateStatusMessage("Not found", true);
            }
        }
        //-----------------------------------------------------------------------------------------
        #endregion
    }
}
