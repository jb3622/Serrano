/*
 * Author:  Marc Levine
 * Project: SRR
 * Date:    From March 2011
 * 
 * Description
 * User Department maintenance business object.
 * 
 * The class offers two methods of managing a user/department relationship.  The first is to specify a department
 * and obtain a dataset of all users who are associated with the department.  The second is to supply either a AS400 or IP
 * username and obtain two lists of assigned departments and available departments.  The UI will handle the returned results
 * accordingly.
*/
using System;
using System.Collections.Generic;
using System.Data;
using Disney.iDash.LocalData;
using Disney.iDash.Shared;

namespace Disney.iDash.SRR.BusinessLayer
{
    public class UserDeptInfo : BusinessBase
    {

        private DataTable _selectedDepartments = null;
        private DataTable _availableDepartments = null;

        private SRRDataSet _dataSet = new SRRDataSet();
        private SRRDataSetTableAdapters.DSUSRDATableAdapter _adapter = new SRRDataSetTableAdapters.DSUSRDATableAdapter();

        public string NetworkId {get; private set;}
        public string IPID {get; private set;}
        public decimal DepartmentId {get; private set;}

        public enum Modes
        {
            Department,
            User
        }

        /// <summary>
        /// Mode determines whether user is editing user/dept assignments by department or by user.
        /// </summary>
        public Modes Mode { get; private set; }

        public UserDeptInfo()
        {            
            _dataSet.DSUSRDA.RowChanged += ((sender, e) =>
            {
                IsDirty = true;
            });

            _dataSet.DSUSRDA.RowDeleted += ((sender, e) =>
            {
                IsDirty = true;
            });

            _dataSet.DSUSRDA.TableNewRow += ((semder, e) =>
            {
                e.Row["USDEPT"] = this.DepartmentId;
                e.Row["USAUTH"] = 10;
                e.Row["USCHGU"] = Session.User.NetworkId.ToUpper();
				e.Row["USCHGD"] = DateTime.Now.ToString();
                IsDirty = true;
            });

            Properties.Settings.Default["ConnectionString"] = Factory.ConnectionString;
            IsDirty = false;
        }

        /// <summary>
        /// Clear all data and reset the dirty flag
        /// </summary>
        public void Clear()
        {
            _selectedDepartments = null;
            _availableDepartments = null;
            _dataSet.DSUSRDA.Rows.Clear();

            this.DepartmentId=0;
            this.NetworkId=string.Empty;
            this.IPID = string.Empty;
            
            IsDirty = false;
        }

        /// <summary>
        /// Search for a NetworkId or IP Id and populate the list of selected and available departments
        /// </summary>
        /// <param name="key"></param>
        /// <param name="useIP"></param>
        /// <returns></returns>
        public bool Search(string key, bool useIP)
        {
            var found = false;

            Clear();

            this.Mode = UserDeptInfo.Modes.User;

            // Either find the Island Pacific ID using the AS400 network Id or find the AS400 ID using the Island Pacific Id
            if (useIP)
            {
                this.NetworkId = Factory.GetValue("DSUSRDA", "USUSPR", "USIPPR", key, string.Empty).ToString();
                this.IPID = key;
            }               
            else
            {
                this.NetworkId = key;
                this.IPID = Factory.GetValue("DSUSRDA", "USIPPR", "USUSPR", key, string.Empty).ToString();
            }

            // At least one user/dept record must exist to be able assign departments to users.
            if (this.NetworkId != string.Empty && this.IPID != string.Empty && Factory.OpenConnection())
                try
                {
                    var whereClause = (useIP ? "USIPPR = '" + key + "'" : "USUSPR = '" + key + "'");
                    _selectedDepartments = Factory.CreateTable(Properties.Resources.SQLUserDeptAssignedDepts.Replace("<whereClause>", whereClause));
                    _availableDepartments = Factory.CreateTable(Properties.Resources.SQLUserDeptAvailableDepts.Replace("<whereClause>", whereClause));

                    _selectedDepartments.RowChanged += ((sender, e) =>
                    {
                        IsDirty = true;
                    });

                    _selectedDepartments.RowDeleted += ((sender, e) =>
                    {
                        IsDirty = true;
                    });

                    _selectedDepartments.AcceptChanges();
                    _availableDepartments.AcceptChanges();

                    found = true;
                }
                catch (Exception ex)
                {
                    ExceptionHandler.RaiseException(ex, "Search: " + key);
                }
                finally
                {
                    Factory.CloseConnection();
                }

            IsDirty = false;
            return found;
        }

        /// <summary>
        /// Search for a department and populate the list of assigned users.
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public bool Search(decimal departmentId)
        {
            var found = false;
            
            Clear();

            this.Mode = Modes.Department;
            this.DepartmentId = departmentId;

            if (Factory.OpenConnection())
                try
                {
                    _adapter.ClearBeforeFill = true;
                    found = _adapter.FillByDept(_dataSet.DSUSRDA, departmentId) > 0;
                }
                catch (Exception ex)
                {
                    ExceptionHandler.RaiseException(ex, "Search: " + departmentId.ToString());
                }
                finally
                {
                    Factory.CloseConnection();
                }

            IsDirty = false;
            return found;
        }

        public DataTable GetSelectedDepartments
        {
            get {return _selectedDepartments;}
        }

        public DataTable GetAvailableDepartments
        {
            get {return _availableDepartments;}
        }

        public DataTable GetAssignedUsers
        {
            get { return _dataSet.DSUSRDA; }
        }

        /// <summary>
        /// Save any changes that have been made.
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            var saved = false;

            if (IsDirty && Factory.OpenConnection())
                if (Mode == Modes.Department)
                    try
                    {
                        foreach (DataRow row in _dataSet.DSUSRDA.Rows)
                            row.EndEdit();
                        _adapter.Update(_dataSet.DSUSRDA);
                        saved = true;
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.RaiseException(ex, "Save");
                    }
                    finally
                    {
                        Factory.CloseConnection();
                    }
                else
                {
                    var trans = Factory.BeginTransaction();
                    try
                    {
						var rowIndex = 0;
                        if (_availableDepartments.GetChanges() != null)
                        {
							var cmdDelete = Factory.CreateCommand("DS888CS1", CommandType.StoredProcedure,
								Factory.CreateParameter("P_DEPT", 0, System.Data.OleDb.OleDbType.Decimal, 3, ParameterDirection.Input),
								Factory.CreateParameter("P_USER", "", System.Data.OleDb.OleDbType.Char, 10, ParameterDirection.Input));

                            foreach (DataRow row in _availableDepartments.GetChanges().Rows)
                            {
								OnProgress("Removing existing assignments", ++rowIndex * 100 / _availableDepartments.GetChanges().Rows.Count);

                                if (row.RowState == DataRowState.Added)
                                {
									cmdDelete.Parameters["P_DEPT"].Value = row["ID"].ToString();
									cmdDelete.Parameters["P_USER"].Value = this.NetworkId.ToUpper();
									cmdDelete.Transaction = trans;
									cmdDelete.ExecuteNonQuery();
                                }
                            }
                        }

						rowIndex = 0;
						if (_selectedDepartments.GetChanges() != null)
						{

							var cmdInsert = Factory.CreateCommand("DS888ES1", CommandType.StoredProcedure,
								Factory.CreateParameter("P_DEPT", 0, System.Data.OleDb.OleDbType.Decimal, 5, ParameterDirection.Input),
								Factory.CreateParameter("P_USER", this.NetworkId, System.Data.OleDb.OleDbType.Char, 10, ParameterDirection.Input),
								Factory.CreateParameter("P_IPPR", this.IPID, System.Data.OleDb.OleDbType.Char, 10, ParameterDirection.Input),
								Factory.CreateParameter("P_CHGU", this.NetworkId.ToUpper(), System.Data.OleDb.OleDbType.Char, 10, ParameterDirection.Input));

							foreach (DataRow row in _selectedDepartments.GetChanges().Rows)
							{
								OnProgress("Adding new assignments", ++rowIndex * 100 / _selectedDepartments.GetChanges().Rows.Count);

								if (row.RowState == DataRowState.Added)
								{
									cmdInsert.Parameters["P_DEPT"].Value = row["ID"].ToString();									
                                    cmdInsert.Transaction = trans;
									cmdInsert.ExecuteNonQuery();
								}
							}
						}
                        trans.Commit();
                        saved = true;
                    }
                    catch (Exception ex)
                    {
                        if (trans != null && trans.Connection != null)
                            trans.Rollback();
                        ExceptionHandler.RaiseException(ex, "Save");
                    }
                    finally
                    {
                        Factory.CloseConnection();
                    }
                }

            if (saved)
                IsDirty = false;
        
            return saved;
        }

		/// <summary>
		/// Check that the AS400Id is not associated with any other IP Id.
		/// </summary>
		/// <param name="AS400Id"></param>
		/// <param name="IPID"></param>
		/// <returns></returns>
		public bool CheckAssociation(DataTable gridSource, string AS400Id, string IPID)
		{
			var associated = false;
			if (Factory.OpenConnection())
				try
				{
					var rows = gridSource.Select(string.Format("USUSPR<>'{0}' AND USIPPR='{1}'", AS400Id, IPID));
					var table = Factory.CreateTable("SELECT USUSPR FROM DSUSRDA WHERE USIPPR='" + IPID + "'");
					if (rows.Length>0 || (table != null && table.Rows.Count > 0 && table.Rows[0]["USUSPR"].ToString() != AS400Id))
						associated = true;
				}
				catch (Exception ex)
				{
					ExceptionHandler.RaiseException(ex, "CheckAssociation");
				}
				finally
				{
					Factory.CloseConnection();
				}
			return associated;
		}
	}
}
