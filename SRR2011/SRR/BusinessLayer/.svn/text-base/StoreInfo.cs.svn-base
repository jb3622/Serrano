/*
 * Author:  Marc Levine
 * Project: SRR
 * Date:    From March 2011
 * 
 * Description
 * Handles all logic relating to stores
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using Disney.iDash.LocalData;
using Disney.iDash.Shared;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace Disney.iDash.SRR.BusinessLayer
{
    public class StoreInfo : BusinessBase
    {
        private DataTable _data = new DataTable();
        private List<DeptGradeStoreItem> _deptGradeStores = new List<DeptGradeStoreItem>();
		private BindingList<StoreGradeItem> _storeGrades = new BindingList<StoreGradeItem>();

		public const string colChangedBy = "CHANGEDBY";
		public const string colChangedDateTime = "CHANGEDDATETIME";
		public const string colDeleteFlag = "DELETEFLAG";
		public const string colDeptId = "DEPTID";
		public const string colDeptName = "DEPTNAME";
        public const string colDescription = "DESCRIPTION";
        public const string colAltDescription = "ALTDESCRIPTION";
		public const string colId = "ID";
		public const string colLastUpdated = "LASTUPDATED";
		public const string colLeadTime = "LEADTIME";
		public const string colNetworkId = "NETWORKID";
		public const string colOldStoreId = "OLDSTOREID";
		public const string colScheduleDate = "SCHEDULEDATE";
		public const string colStatus = "STATUS";
		public const string colStoreId = "STOREID";
		public const string colStoreName = "STORENAME";

		public StoreInfo()
		{
			_storeGrades.ListChanged += ((sender, e) =>
				{
					if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemChanged)
					{
						IsDirty = true;
					}
				});

		}

		void _storeGrades_AddingNew(object sender, AddingNewEventArgs e)
		{
			throw new NotImplementedException();
		}

        #region Store lead times
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// Get a list of stores and their lead times for all markets
        /// </summary>
        /// <returns></returns>
        /// 
        public DataTable GetStoreLeadTimes()
        {
            return GetData(Properties.Resources.SQLStoreLeadTimesSelect + " ORDER BY c.CSDESC, s.SSTR");
        }

        /// <summary>
        /// Get a list of all stores and their lead times for specified markets
        /// </summary>
        /// <param name="markets"></param>
        /// <returns></returns>
        public DataTable GetStoreLeadTimes(List<string> markets)
        {
            return GetData(Properties.Resources.SQLStoreLeadTimesSelect + "AND ds.DCTR IN (<markets>) ORDER BY c.CSDESC, s.SSTR".Replace("<markets>", "'" + string.Join("','", markets) + "'"));
        }

		public DataTable GetStoreLeadTimes(string markets)
		{
			return GetData(Properties.Resources.SQLStoreLeadTimesSelect + "AND ds.DCTR IN (<markets>) ORDER BY c.CSDESC, s.SSTR".Replace("<markets>", "'" + markets + "'"));
		}
		
		/// <summary>
        /// Update any changes made to store lead times.
        /// </summary>
        /// <returns></returns>
        public bool SaveLeadTimes()
        {
            var saved = false;

            if (IsDirty && base.Factory.OpenConnection())
                try
                {
					var rowCount = 0;
					var totalRows = _data.GetChanges().Rows.Count;

                    foreach (DataRow row in _data.GetChanges().Rows)
                    {
						OnProgress("Saving...", (++rowCount * 100 / totalRows));

                        var cmd = Factory.CreateCommand(Properties.Resources.SQLStoreLeadTimesUpdate
                            .Replace("<leadTime>", row[colLeadTime].ToString()).Replace("<storeId>", row[colStoreId].ToString()));
                        cmd.ExecuteNonQuery();
                    }
                    saved = true;
                }
                catch (Exception ex)
                {
                    ExceptionHandler.RaiseException(ex, "SaveLeadTimes");
                }
                finally
                {
                    Factory.CloseConnection();
                }

            if (saved)
                IsDirty = false;

            return saved;
        }
        //------------------------------------------------------------------------------------------
        #endregion

        #region Department Store Grade assignment
        //------------------------------------------------------------------------------------------

        public void Clear()
        {
            _deptGradeStores.Clear();
			_storeGrades.Clear();
        }

		private List<ListItem> _departments = new List<ListItem>();
		public List<ListItem> GetDepartments()
		{
			DataTable table = null;
			if (_departments.Count == 0 && base.Factory.OpenConnection())
			{
				table = base.Factory.CreateTable(Properties.Resources.SQLDepartmentsSelect);
				if (table != null && table.Rows.Count > 0)
					foreach (DataRow row in table.Rows)
					{
						var item = new ListItem(row[colDeptId].ToString(), row[colDeptName].ToString(), "Department");
						_departments.Add(item);
					}
				base.Factory.CloseConnection();
			}

			return _departments;
		}

		private List<ListItem> _grades = new List<ListItem>();
		public List<ListItem> GetGrades()
		{
			DataTable table = null;

			if (_grades.Count == 0 && base.Factory.OpenConnection())
			{
				table = base.Factory.CreateTable(Properties.Resources.SQLLookupStoreGrades);
				if (table != null && table.Rows.Count > 0)
					foreach (DataRow row in table.Rows)
					{
						var item = new ListItem(row[colId].ToString(), row[colDescription].ToString(), row[colAltDescription].ToString(), "StoreGrade");
						_grades.Add(item);
					}
				base.Factory.CloseConnection();
			}

			return _grades;
		}

		public BindingList<StoreGradeItem> GetEmptyStoreGradeList()
		{
			_storeGrades.Clear();
			return _storeGrades;
		}

        /// <summary>
        /// Return a List of all stores and their description for a specific department and grade
        /// </summary>
        /// <returns></returns>
        public List<DeptGradeStoreItem> GetStores(decimal deptId, string gradeId)
        {
            var qryCurrent = from i
                      in _deptGradeStores
                      where i.DeptId == deptId && i.GradeId == gradeId
                      select i;

            var qryOriginal = from i
                      in _deptGradeStores
                      where i.DeptId == deptId && i.OriginalGradeId == gradeId
                      select i;

            if (qryCurrent.FirstOrDefault() == null && qryOriginal.FirstOrDefault() == null && base.Factory.OpenConnection())
            {
                    var table = base.Factory.CreateTable(Properties.Resources.SQLDeptGradeStoresSelect.Replace("<deptId>", deptId.ToString()).Replace("<gradeId>", gradeId.ToString()));
                    if (table != null && table.Rows.Count > 0)
                        foreach (DataRow row in table.Rows)
                        {
                            var item = new DeptGradeStoreItem(deptId, gradeId, 
                                Convert.ToDecimal(row[colStoreId]), 
                                row[colStoreName].ToString(),
                                row[colChangedBy].ToString(),
                                row[colChangedDateTime].ToString());
                            _deptGradeStores.Add(item);
                        }
                    base.Factory.CloseConnection();
                }

            return qryCurrent.ToList();
        }

        public void PasteStoreDeptGrades(List<DeptGradeStoreItem> _items, decimal deptId, string gradeId)
        {
            foreach (var item in _items)
            {
                var foundItem = _deptGradeStores.Find((i) => (i.DeptId == item.DeptId && 
                    i.OriginalGradeId == item.OriginalGradeId &&
                    i.StoreId == item.StoreId));

                if (foundItem != null)
                {
                    foundItem.DeptId = deptId;
                    foundItem.GradeId = gradeId;
                    foundItem.ChangedBy = Session.User.NetworkId;
                    foundItem.ChangedDate = DateTime.Now;
                    IsDirty = true;
                }
            }
        }

        public bool SaveStoreDeptGrades()
        {
            var saved = false;
            var useTransaction = false;

            if (Factory.OpenConnection())
            {
                System.Data.OleDb.OleDbTransaction tran = null;
                var cmd = Factory.CreateCommand("DS891GS1", CommandType.StoredProcedure);

                try
                {
                    cmd.Parameters.Add(new OleDbParameter("P_NEWGRADE", OleDbType.Char, 3));
                    cmd.Parameters.Add(new OleDbParameter("P_GRADENAME", OleDbType.Char, 25));
                    cmd.Parameters.Add(new OleDbParameter("P_OLDGRADE", OleDbType.Char, 3));
                    cmd.Parameters.Add(new OleDbParameter{ ParameterName = "P_CHGU", Value = Session.User.NetworkId.ToUpper(), OleDbType = System.Data.OleDb.OleDbType.Char });
                    cmd.Parameters.Add(new OleDbParameter{ ParameterName = "P_CHGD", Value = System.DateTime.Now, OleDbType = OleDbType.DBTimeStamp});
                    cmd.Parameters.Add(new OleDbParameter("P_DEPT", OleDbType.Decimal, 3));
					cmd.Parameters.Add(new OleDbParameter("P_STORE", OleDbType.Decimal, 3));
					cmd.Parameters.Add(new OleDbParameter{ ParameterName = "P_ALLDEPTS", Value = "N", OleDbType = OleDbType.Char, Size = 1 });

                    var movedItems = _deptGradeStores.Where((i) => (i.Moved == true));
                    if (movedItems != null)
                    {
                        if (useTransaction)
                            tran = Factory.BeginTransaction();

						var rowCount = 0;
						var totalRows = movedItems.Count();

                        foreach (var item in movedItems)
                        {
							OnProgress("Saving", (++rowCount * 100 / totalRows));

                            cmd.Parameters["P_NEWGRADE"].Value = item.GradeId;
                            cmd.Parameters["P_OLDGRADE"].Value = item.OriginalGradeId;
                            cmd.Parameters["P_GRADENAME"].Value = _grades.Where((i) => (i.Id == item.GradeId)).First().AltDescription;
                            cmd.Parameters["P_DEPT"].Value = item.DeptId;
                            cmd.Parameters["P_STORE"].Value = item.StoreId;
                         
                            if (useTransaction)
                                cmd.Transaction = tran;

                            cmd.ExecuteNonQuery();
                        }
                        if (useTransaction)
                            tran.Commit();
                        Clear();
                    }
					saved = true;
				}
                catch (Exception ex)
                {
                    if (useTransaction && tran != null && tran.Connection != null)
                        tran.Rollback();
					ExceptionHandler.RaiseException(ex, "SaveStoreDeptGrades");
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

		public bool SaveStoreGrades()
		{
			var saved = false;
			var useTransaction = false;

			if (Factory.OpenConnection())
			{
				System.Data.OleDb.OleDbTransaction tran = null;
				var cmd = Factory.CreateCommand("DS895GS1", CommandType.StoredProcedure);

				try
				{
					cmd.Parameters.Add(new OleDbParameter("P_GDGRADE", OleDbType.Char, 3));
					cmd.Parameters.Add(new OleDbParameter("P_GDGNAM", OleDbType.Char, 25));
					cmd.Parameters.Add(new OleDbParameter { ParameterName = "P_GDCHGU", Value = Session.User.NetworkId.ToUpper(), OleDbType = System.Data.OleDb.OleDbType.Char, Size = 10 });
					cmd.Parameters.Add(new OleDbParameter { ParameterName = "P_GDCHGD", Value = System.DateTime.Now, OleDbType = OleDbType.DBTimeStamp });
					cmd.Parameters.Add(new OleDbParameter("P_GDSTR", OleDbType.Decimal, 3));

					if (useTransaction)
					{
						tran = Factory.BeginTransaction();
						cmd.Transaction = tran;
					}

					var rowCount = 0;
					var totalRows = _storeGrades.Count();

					foreach (var item in _storeGrades)
					{
						OnProgress("Saving", (++rowCount * 100 / totalRows));

						cmd.Parameters["P_GDGRADE"].Value = item.GradeId;
						cmd.Parameters["P_GDGNAM"].Value = _grades.Where((i) => (i.Id == item.GradeId)).First().AltDescription;
						cmd.Parameters["P_GDSTR"].Value = item.StoreId;

						cmd.ExecuteNonQuery();
					}

					if (useTransaction)
						tran.Commit();
		
					saved = true;
					
				}
				catch (Exception ex)
				{
					if (useTransaction && tran != null && tran.Connection != null)
						tran.Rollback();
					ExceptionHandler.RaiseException(ex, "SaveStoreGrades");
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

        //------------------------------------------------------------------------------------------
        #endregion

        #region Store deletion schedule
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// Return a datatable of all stores and their description.
        /// </summary>
        /// <returns></returns>
        public List<LookupItem> GetStores()
        {
            var lookup = new LookupSource();
            lookup.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
                {
                    ExceptionHandler.RaiseException(ex, extraInfo, terminateApplication);
                });
            
            return lookup.GetItems(LookupSource.LookupTypes.Stores);
        }
        
        public DataTable GetStoreDeletionSchedule(bool showProcessed)
        {
            var table = GetData(Properties.Resources.SQLStoreDeleteScheduleSelect.Replace("<statusList>", (showProcessed ? "'', 'Y'" : "''")));
			if (table != null)
			{
				table.TableNewRow += ((sender, e) =>
					{
						e.Row[colStatus] = string.Empty;
						e.Row[colStoreId] = 0;
						e.Row[colOldStoreId] = 0;
						e.Row[colScheduleDate] = DateTime.Now.Date.ToString("yyyy-MM-dd");
						e.Row[colNetworkId] = Session.User.NetworkId.ToUpper();
						e.Row[colLastUpdated] = DateTime.Now.Date.ToString("yyyy-MM-dd hh:mm:ss");
						e.Row[colDeleteFlag] = 0;
					});

				table.Columns[colStoreId].Unique = true;
				table.Columns[colStoreId].AllowDBNull = false;
			}
            return table;
        }

        public bool SaveStoreDeletionSchedule()
        {
            var saved = false;

            if (IsDirty && base.Factory.OpenConnection())
                try
                {
                    OleDbCommand cmd = null;

                    var cmdInsert = Factory.CreateCommand("DS893GS1", CommandType.StoredProcedure);
                    cmdInsert.Parameters.Add(new OleDbParameter("P_STR", OleDbType.Decimal, 3));
                    cmdInsert.Parameters.Add(new OleDbParameter("P_LDDAT", OleDbType.DBDate));
                    cmdInsert.Parameters.Add(new OleDbParameter{ParameterName = "P_LUSR", OleDbType = OleDbType.Char, Size = 10, Value = Session.User.NetworkId.ToUpper() });
                    cmdInsert.Parameters.Add(new OleDbParameter{ParameterName = "P_LRDAT", OleDbType = OleDbType.DBTimeStamp, Value = System.DateTime.Now });

                    var cmdUpdate = Factory.CreateCommand("DS894GS1", CommandType.StoredProcedure);
                    cmdUpdate.Parameters.Add(new OleDbParameter("P_NEWSTR", OleDbType.Decimal, 3));
                    cmdUpdate.Parameters.Add(new OleDbParameter("P_OLDSTR", OleDbType.Decimal, 3));
                    cmdUpdate.Parameters.Add(new OleDbParameter("P_LDDAT", OleDbType.DBDate));
                    cmdUpdate.Parameters.Add(new OleDbParameter { ParameterName = "P_LUSR", OleDbType = OleDbType.Char, Size = 10, Value = Session.User.NetworkId.ToUpper() });
                    cmdUpdate.Parameters.Add(new OleDbParameter { ParameterName = "P_LRDAT", OleDbType = OleDbType.DBTimeStamp, Value = System.DateTime.Now });

					var rowCount = 0;
					var totalRows = _data.GetChanges().Rows.Count;

                    foreach (DataRow row in _data.GetChanges().Rows)
                    {

						OnProgress("Saving...", (++rowCount * 100 / totalRows));

                        cmd = null;
                        switch (row.RowState)
                        {
                            case DataRowState.Added:
                                if ((int)row[colDeleteFlag] == 1)
                                {
                                    cmd = Factory.CreateCommand(Properties.Resources.SQLStoreDeleteScheduleDelete.Replace("<storeId>", row[colStoreId].ToString()));
                                    cmd.ExecuteNonQuery();
                                }
                                else
                                {
                                    cmdInsert.Parameters["P_STR"].Value = Convert.ToDecimal(row[colStoreId]);
                                    cmdInsert.Parameters["P_LDDAT"].Value = Convert.ToDateTime(row[colScheduleDate]);
                                    cmdInsert.ExecuteNonQuery();
                                }
                                break;

                            case DataRowState.Modified:
                                if ((int)row[colDeleteFlag] == 1)
                                {
                                    cmd = Factory.CreateCommand(Properties.Resources.SQLStoreDeleteScheduleDelete.Replace("<storeId>", row[colStoreId].ToString()));
                                    cmd.ExecuteNonQuery();
                                }
                                else
                                {
                                    cmdUpdate.Parameters["P_NEWSTR"].Value = Convert.ToDecimal(row[colStoreId]);
                                    cmdUpdate.Parameters["P_OLDSTR"].Value = Convert.ToDecimal(row[colOldStoreId]);
                                    cmdUpdate.Parameters["P_LDDAT"].Value = Convert.ToDateTime(row[colScheduleDate]);
                                    cmdUpdate.ExecuteNonQuery();
                                }
                                break;
                        }
                    }
                    saved = true;
                }
                catch (Exception ex)
                {
                    ExceptionHandler.RaiseException(ex, "SaveStoreDeletionSchedule");
                }
                finally
                {
                    Factory.CloseConnection();
                }

            if (saved)
                IsDirty = false; 
            return saved;
        }

        //------------------------------------------------------------------------------------------
        #endregion

        #region Common methods
        //------------------------------------------------------------------------------------------
        public bool SetDeletedFlag(int[] selectedRows)
        {
            var result = false;
            try
            {
                foreach (var rowHandle in selectedRows)
                {
                    var row = _data.Rows[rowHandle];
                    if (row != null)
                        if ((int)row[colDeleteFlag] == 1)
							row[colDeleteFlag] = 0;
                        else
							row[colDeleteFlag] = 1;
                }
                result = true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.RaiseException(ex, "SetItemDeletedFlag");
            }
            return result;
        }
        
        /// <summary>
        /// Internal method to return the store list
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private DataTable GetData(string sql)
        {
            if (base.Factory.OpenConnection())
            {
                _data = base.Factory.CreateTable(sql);
				if (_data != null)
				{
					_data.AcceptChanges();
					_data.RowChanged += ((sender, e) =>
					{
						base.IsDirty = true;
					});
				}
                base.Factory.CloseConnection();
            }
            IsDirty = false;
            return _data;
        }
        //------------------------------------------------------------------------------------------
        #endregion
    }

    public class ListItem
    {
        public string Id { get; set;}
        public string Description { get; set; }
        public string AltDescription { get; set; }
        public object Tag { get; set; }

        public ListItem(string id, string description, object tag = null)
        {
            Id = id;
            Description = description;
            AltDescription = string.Empty;
            Tag = tag;
        }

        public ListItem(string id, string description, string altDescription, object tag = null)
        {
            Id = id;
            Description = description;
            AltDescription = altDescription;
            Tag = tag;
        }
    }

	public class StoreGradeItem
	{
		public decimal StoreId { get; set; }
		public string GradeId { get; set; }
	}

    public class DeptGradeStoreItem
    {
        public decimal DeptId { get; set; }
        public string GradeId { get; set; }
        public decimal StoreId { get; set; }
        public string StoreName { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangedDate { get; set; }
        public string OriginalGradeId { get; private set; }

        public DeptGradeStoreItem(decimal deptId, string gradeId, decimal storeId, string storeName, string changedBy, string changedDate)
        {
            DeptId = deptId;
            GradeId = gradeId;
            OriginalGradeId = gradeId;
            StoreId = storeId;
            StoreName = storeName;
            ChangedBy = changedBy;

            var pattern = @"\d{4}\-\d{2}\-\d{2}\-\d{2}\.\d{2}\.\d{2}\.\d{6}";
            var match = Regex.Match(changedDate, pattern);

            if (match.Success)
                ChangedDate = new DateTime(Convert.ToInt32(changedDate.Substring(0, 4)),
                            Convert.ToInt32(changedDate.Substring(5, 2)),
                            Convert.ToInt32(changedDate.Substring(8, 2)),
                            Convert.ToInt32(changedDate.Substring(11, 2)),
                            Convert.ToInt32(changedDate.Substring(14, 2)),
                            Convert.ToInt32(changedDate.Substring(17, 2)));
            else
                ChangedDate = Convert.ToDateTime(changedDate);
            
        }

        public bool Moved
        {
            get { return GradeId != OriginalGradeId;}
        }
 
    }


}
