/*
 * Author:  Lee Ellis
 * Project: SRR
 * Date:    From May 2011
 * 
 * Description
 * Handles all logic relating to SummaryWorkbench
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Disney.iDash.LocalData;
using Disney.iDash.Shared;
using System.Threading;

namespace Disney.iDash.SRR.BusinessLayer
{
    public class SummaryWorkbenchInfo : BusinessBase
    {
		
		#region Enumerations and static data
        //-------------------------------------------------------------------------------

		public enum SummaryLevel
        {
            Department = 16,
            DepartmentMarket = 15,
            DepartmentGrade = 14,
            DepartmentStore = 13,
            Class = 12,
            ClassMarket = 11,
            ClassGrade = 10,
            ClassStore = 9,
        }

        public enum SummaryLever
        {
            Allocation,
            CutOff,
            Pattern,
            SmoothingFactor,
            UpliftFactor,
        }

        public enum SummaryItemColumns
        {
            HierarchyKey,
            Department,
            Class,
            Market,
            MarketColour,
            MarketSequence,
            Grade,
            GradeDescription,
            Store,
            StoreName,
            KeyValue,
            StoreType,
            WorkBench,
            UpliftFactor,
            UpliftInheritedLevel,
            UpliftActualFlag,
            UpliftOverrideLevel,
            UpliftStatus,
            CutOff,
            CutOffInheritedLevel,
            CutOffActualFlag,
            CutOffOverrideLevel,
            CutOffStatus,
            Allocation,
            AllocationInheritedLevel,
            AllocationActualFlag,
            AllocationOverrideLevel,
            AllocationStatus,
            SmoothFactor,
            SmoothFactorInheritedLevel,
            SmoothFactorActualFlag,
            SmoothFactorOverrideLevel,
            SmoothFactorStatus,
            Pattern,
            PatternInheritedLevel,
            PatternActualFlag,
            PatternOverrideLevel,
            PatternStatus,
            StockRequirement,
            IdealReplenishmentQuantity,
            ProposedAllocationQuantity,
            Changed,
        }

        private static Dictionary<SummaryItemColumns, string> _summaryItemColumns = new Dictionary<SummaryItemColumns, string>{
            {SummaryItemColumns.HierarchyKey, "HierarchyKey"},
            {SummaryItemColumns.Department, "Department"},
            {SummaryItemColumns.Class, "Class"},
            {SummaryItemColumns.Market, "Market"},
            {SummaryItemColumns.MarketColour, "MarketColour"},
            {SummaryItemColumns.MarketSequence, "MarketSequence"},
            {SummaryItemColumns.Grade, "Grade"},
            {SummaryItemColumns.GradeDescription, "GradeDescription"},
            {SummaryItemColumns.Store, "Store"},
            {SummaryItemColumns.StoreName, "StoreName"},
            {SummaryItemColumns.KeyValue, "KeyValue"},
            {SummaryItemColumns.StoreType, "StoreType"},
            {SummaryItemColumns.WorkBench, "WorkBench"},
            {SummaryItemColumns.UpliftFactor, "UpliftFactor"},
            {SummaryItemColumns.UpliftInheritedLevel, "UpliftInheritedLevel"},
            {SummaryItemColumns.UpliftActualFlag, "UpliftActualFlag"},
            {SummaryItemColumns.UpliftOverrideLevel, "UpliftOverrideLevel"},
            {SummaryItemColumns.UpliftStatus, "UpliftStatus"},
            {SummaryItemColumns.CutOff, "CutOff"},
            {SummaryItemColumns.CutOffInheritedLevel, "CutOffInheritedLevel"},
            {SummaryItemColumns.CutOffActualFlag, "CutOffActualFlag"},
            {SummaryItemColumns.CutOffOverrideLevel, "CutOffOverrideLevel"},
            {SummaryItemColumns.CutOffStatus, "CutOffStatus"},
            {SummaryItemColumns.Allocation, "Allocation"},
            {SummaryItemColumns.AllocationInheritedLevel, "AllocationInheritedLevel"},
            {SummaryItemColumns.AllocationActualFlag, "AllocationActualFlag"},
            {SummaryItemColumns.AllocationOverrideLevel, "AllocationOverrideLevel"},
            {SummaryItemColumns.AllocationStatus, "AllocationStatus"},
            {SummaryItemColumns.SmoothFactor, "SmoothFactor"},
            {SummaryItemColumns.SmoothFactorInheritedLevel, "SmoothFactorInheritedLevel"},
            {SummaryItemColumns.SmoothFactorActualFlag, "SmoothFactorActualFlag"},
            {SummaryItemColumns.SmoothFactorOverrideLevel, "SmoothFactorOverrideLevel"},
            {SummaryItemColumns.SmoothFactorStatus, "SmoothFactorStatus"},
            {SummaryItemColumns.Pattern, "Pattern"},
            {SummaryItemColumns.PatternInheritedLevel, "PatternInheritedLevel"},
            {SummaryItemColumns.PatternActualFlag, "PatternActualFlag"},
            {SummaryItemColumns.PatternOverrideLevel, "PatternOverrideLevel"},
            {SummaryItemColumns.PatternStatus, "PatternStatus"},
            {SummaryItemColumns.StockRequirement, "StockRequirement"},
            {SummaryItemColumns.IdealReplenishmentQuantity, "IdealReplenishmentQuantity"},
            {SummaryItemColumns.ProposedAllocationQuantity, "ProposedAllocationQuantity"},
            {SummaryItemColumns.Changed, "Changed"},
        };

        #endregion
        //-------------------------------------------------------------------------------

		public decimal DepartmentId { get; set; }
		public Constants.Workbenches Workbench { get; set; }
		public Constants.StoreTypes StoreType { get; set; }

        // Hierearchy level of the currently displayed grid
        public int SelectedSummaryLevel { get; set; }

        // The name of the AS400 file member that is used for this session
        public string SessionMemberName { get; set; }

        // It was intended that this list of summary items be used to allow us to highlight differences before and after model runs.
        // The changes are now being maintained on the AS400
        //public List<SummaryItem> MasterSummaryItems { get; set; }

		private List<SummaryItem> OriginalItems { get; set; }
        public List<SummaryItem> WorkingSummaryItems { get; set; }
        public List<SummaryItem> ChangedSummaryItems { get; set; }

		public bool IsDailyLocked { get { return DailyFilegroup != 0; } }
		public bool IsWeeklyLocked { get { return WeeklyFilegroup != 0; } }

        public bool IsClicksChanged { get; set; }
        public bool IsBricksChanged { get; set; }

        public new bool IsDirty { get; set; }
        public bool ApplyChangesAllowed { get; set; }
        public bool MustReleaseLocks { get; set; }

        public IEnumerable<DataRow> SummaryData { get; set; }

        // Used for Copy / Paste functionality
        public SummaryItem CopiedSummaryItem { get; set; }

		public string ErrorMessage { get; private set; }
		public decimal DailyFilegroup { get; private set; }
		public decimal WeeklyFilegroup { get; private set; }

        #region Public properties and methods
        //-------------------------------------------------------------------------------

        public SummaryWorkbenchInfo()
        {
            ChangedSummaryItems = new List<SummaryItem>();
        }

        public static string GetSummaryItemName(SummaryItemColumns summaryItem)
        {
            return _summaryItemColumns[summaryItem];
        }

		internal void AddCriteria(StringBuilder sb, string fieldName, string value)
		{
			if (!string.IsNullOrEmpty(value))
				sb.Append(string.Format(" AND <prefix>{0}='{1}'", fieldName, value));
		}

        /// <summary>
        /// Returns a table of data matching the criteria for the current state.
        /// </summary>
        /// <returns></returns>
        public DataTable GetData()
        {
            var table = new DataTable();
            var timeStart = DateTime.Now;
            var sql = this.CreateSQL();

            if (Factory.OpenConnection())
                try
                {
                    OnProgress("Loading...");
                    table = Factory.CreateTable(sql);
                    if (table == null || table.Rows.Count == 0)
                        OnProgress("No records available");
                    else
                        OnProgress(string.Format("{0:#,##0} record(s) loaded in {1:0.0} seconds", table.Rows.Count, DateTime.Now.Subtract(timeStart).TotalSeconds));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.RaiseException(ex, "GetData");
                }
                finally
                {
                    Factory.CloseConnection();
                }

            return table;
        }

        /// <summary>
        /// Returns a list of data matching the Level supplied.
        /// </summary>
        /// <returns></returns>
        public List<SummaryItem> GetLevelData(int Level)
        {
            var query = from i in WorkingSummaryItems
                        where i.HierarchyKey.Equals(Level)
                        orderby
                            i.Department,
                            i.Class,
                            i.MarketSequence,
                            i.Grade,
                            i.Store,
                            i.StoreType,
                            i.WorkBench
                        select i;

            return query.ToList();
        }

        public bool Save()
        {
            var saved = false;

            if (ChangedSummaryItems.Count() > 0 && base.Factory.OpenConnection() && ClearSummaryWorkbenchFile())
            {
                var cmd = Factory.CreateCommand("DS889GS1", CommandType.StoredProcedure);

                cmd.Parameters.Add(new OleDbParameter("P_MBRID", OleDbType.Char, 10));
                cmd.Parameters.Add(new OleDbParameter("P_SWKEY", OleDbType.Decimal, 2));
                cmd.Parameters.Add(new OleDbParameter("P_SWDEPT", OleDbType.Decimal, 3));
                cmd.Parameters.Add(new OleDbParameter("P_SWCLS", OleDbType.Decimal, 4));
                cmd.Parameters.Add(new OleDbParameter("P_SWMKT", OleDbType.Char, 2));
                cmd.Parameters.Add(new OleDbParameter("P_SWMKTC", OleDbType.Decimal, 10));
                cmd.Parameters.Add(new OleDbParameter("P_SWGRD", OleDbType.Char, 3));
                cmd.Parameters.Add(new OleDbParameter("P_SWGRDN", OleDbType.Char, 25));
                cmd.Parameters.Add(new OleDbParameter("P_SWSTR", OleDbType.Decimal, 3));
                cmd.Parameters.Add(new OleDbParameter("P_SWSTRN", OleDbType.Char, 25));
                cmd.Parameters.Add(new OleDbParameter("P_SWKYVL", OleDbType.Char, 5));
                cmd.Parameters.Add(new OleDbParameter("P_SWSTRTYP", OleDbType.Char, 1));
                cmd.Parameters.Add(new OleDbParameter("P_SWWBTYP", OleDbType.Char, 1));

                cmd.Parameters.Add(new OleDbParameter { ParameterName = "P_SWFFCT", OleDbType = System.Data.OleDb.OleDbType.Decimal, Precision = 5, Scale = 2 });
                cmd.Parameters.Add(new OleDbParameter("P_SWFFCTI", OleDbType.Decimal, 2));
                cmd.Parameters.Add(new OleDbParameter("P_SWFFCTA", OleDbType.Char, 1));
                cmd.Parameters.Add(new OleDbParameter("P_SWFFCTO", OleDbType.Decimal, 2));
                cmd.Parameters.Add(new OleDbParameter("P_SWFFCTS", OleDbType.Char, 1));

                cmd.Parameters.Add(new OleDbParameter { ParameterName = "P_SWCCO", OleDbType = System.Data.OleDb.OleDbType.Decimal, Precision = 5, Scale = 2 });
                cmd.Parameters.Add(new OleDbParameter("P_SWCCOI", OleDbType.Decimal, 2));
                cmd.Parameters.Add(new OleDbParameter("P_SWCCOA", OleDbType.Char, 1));
                cmd.Parameters.Add(new OleDbParameter("P_SWCCOO", OleDbType.Decimal, 2));
                cmd.Parameters.Add(new OleDbParameter("P_SWCCOS", OleDbType.Char, 1));

                cmd.Parameters.Add(new OleDbParameter("P_SWAFLG", OleDbType.Char, 1));
                cmd.Parameters.Add(new OleDbParameter("P_SWAFLGI", OleDbType.Decimal, 2));
                cmd.Parameters.Add(new OleDbParameter("P_SWAFLGA", OleDbType.Char, 1));
                cmd.Parameters.Add(new OleDbParameter("P_SWAFLGO", OleDbType.Decimal, 2));
                cmd.Parameters.Add(new OleDbParameter("P_SWAFLGS", OleDbType.Char, 1));

                cmd.Parameters.Add(new OleDbParameter { ParameterName = "P_SWSFCT", OleDbType = System.Data.OleDb.OleDbType.Decimal, Precision = 5, Scale = 2 });
                cmd.Parameters.Add(new OleDbParameter("P_SWSFCTI", OleDbType.Decimal, 2));
                cmd.Parameters.Add(new OleDbParameter("P_SWSFCTA", OleDbType.Char, 1));
                cmd.Parameters.Add(new OleDbParameter("P_SWSFCTO", OleDbType.Decimal, 2));
                cmd.Parameters.Add(new OleDbParameter("P_SWSFCTS", OleDbType.Char, 1));

                cmd.Parameters.Add(new OleDbParameter("P_SWPATT", OleDbType.Decimal, 3));
                cmd.Parameters.Add(new OleDbParameter("P_SWPATTI", OleDbType.Decimal, 2));
                cmd.Parameters.Add(new OleDbParameter("P_SWPATTA", OleDbType.Char, 1));
                cmd.Parameters.Add(new OleDbParameter("P_SWPATTO", OleDbType.Decimal, 2));
                cmd.Parameters.Add(new OleDbParameter("P_SWPATTS", OleDbType.Char, 1));

                cmd.Parameters.Add(new OleDbParameter("P_SWSTRQ", OleDbType.Decimal, 9));
                cmd.Parameters.Add(new OleDbParameter("P_SWRPQT", OleDbType.Decimal, 9));
                cmd.Parameters.Add(new OleDbParameter("P_SWPRAQ", OleDbType.Decimal, 9));
                cmd.Parameters.Add(new OleDbParameter("P_SWCHGD", OleDbType.Char, 1));

                try
                {

                    foreach (SummaryItem summaryItem in ChangedSummaryItems)
                    {
                        cmd.Parameters["P_MBRID"].Value = this.SessionMemberName;
                        cmd.Parameters["P_SWKEY"].Value = summaryItem.HierarchyKey;
                        cmd.Parameters["P_SWDEPT"].Value = summaryItem.Department;
                        cmd.Parameters["P_SWCLS"].Value = summaryItem.Class;
                        cmd.Parameters["P_SWMKT"].Value = summaryItem.Market;
                        cmd.Parameters["P_SWMKTC"].Value = summaryItem.MarketColour;
                        cmd.Parameters["P_SWGRD"].Value = summaryItem.Grade;
                        cmd.Parameters["P_SWGRDN"].Value = summaryItem.GradeDescription;
                        cmd.Parameters["P_SWSTR"].Value = summaryItem.Store;
                        cmd.Parameters["P_SWSTRN"].Value = summaryItem.StoreName;
                        cmd.Parameters["P_SWKYVL"].Value = summaryItem.KeyValue;
                        cmd.Parameters["P_SWSTRTYP"].Value = summaryItem.StoreType.StartsWith("B") ? "B" : "C";
                        cmd.Parameters["P_SWWBTYP"].Value = summaryItem.WorkBench.StartsWith("D") ? "D" : "W";

                        cmd.Parameters["P_SWFFCT"].Value = summaryItem.UpliftFactor;
                        cmd.Parameters["P_SWFFCTI"].Value = summaryItem.UpliftInheritedLevel;
                        cmd.Parameters["P_SWFFCTA"].Value = summaryItem.UpliftActualFlag ? "1" : "0";
                        cmd.Parameters["P_SWFFCTO"].Value = summaryItem.UpliftOverrideLevel;
                        cmd.Parameters["P_SWFFCTS"].Value = summaryItem.UpliftStatus;

                        cmd.Parameters["P_SWCCO"].Value = summaryItem.CutOff;
                        cmd.Parameters["P_SWCCOI"].Value = summaryItem.CutOffInheritedLevel;
                        cmd.Parameters["P_SWCCOA"].Value = summaryItem.CutOffActualFlag ? "1" : "0";
                        cmd.Parameters["P_SWCCOO"].Value = summaryItem.CutOffOverrideLevel;
                        cmd.Parameters["P_SWCCOS"].Value = summaryItem.CutOffStatus;

                        cmd.Parameters["P_SWAFLG"].Value = summaryItem.Allocation;
                        cmd.Parameters["P_SWAFLGI"].Value = summaryItem.AllocationInheritedLevel;
                        cmd.Parameters["P_SWAFLGA"].Value = summaryItem.AllocationActualFlag ? "1" : "0";
                        cmd.Parameters["P_SWAFLGO"].Value = summaryItem.AllocationOverrideLevel;
                        cmd.Parameters["P_SWAFLGS"].Value = summaryItem.AllocationStatus;

                        cmd.Parameters["P_SWSFCT"].Value = summaryItem.SmoothFactor;
                        cmd.Parameters["P_SWSFCTI"].Value = summaryItem.SmoothFactorInheritedLevel;
                        cmd.Parameters["P_SWSFCTA"].Value = summaryItem.SmoothFactorActualFlag ? "1" : "0";
                        cmd.Parameters["P_SWSFCTO"].Value = summaryItem.SmoothFactorOverrideLevel;
                        cmd.Parameters["P_SWSFCTS"].Value = summaryItem.SmoothFactorStatus;

                        cmd.Parameters["P_SWPATT"].Value = summaryItem.Pattern;
                        cmd.Parameters["P_SWPATTI"].Value = summaryItem.PatternInheritedLevel;
                        cmd.Parameters["P_SWPATTA"].Value = summaryItem.PatternActualFlag;
                        cmd.Parameters["P_SWPATTO"].Value = summaryItem.PatternOverrideLevel;
                        cmd.Parameters["P_SWPATTS"].Value = summaryItem.PatternStatus;

                        cmd.Parameters["P_SWSTRQ"].Value = 0;
                        cmd.Parameters["P_SWRPQT"].Value = 0;
                        cmd.Parameters["P_SWPRAQ"].Value = 0;
                        cmd.Parameters["P_SWCHGD"].Value = summaryItem.Changed;

                        Factory.ListParameters(cmd.Parameters);
                        cmd.ExecuteNonQuery();

                    }
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
            }

            if (saved)
                IsDirty = false;

            return saved;
        }

        public bool ClearSummaryWorkbenchFile()
        {
            var cleared = false;
            OleDbCommand cmd = null;
            try
            {
                cmd = Factory.CreateCommand(Properties.Resources.SQLSummaryWorkbenchDelete
                    .Replace("<member>", this.SessionMemberName)
                    .Replace("<departmentId>", this.DepartmentId.ToString()));
                cmd.ExecuteNonQuery();

                cleared = true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.RaiseException(ex, "ClearSummaryWorkbenchFile");
            }
            return cleared;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="periodType"></param>
        /// <param name="storeType"></param>
        /// <param name="fileGroup"></param>
        /// <param name="leverLevel"></param>
        /// <param name="leverType"></param>
        /// <param name="leverDepartment"></param>
        /// <param name="leverMarket"></param>
        /// <param name="leverGrade"></param>
        /// <param name="leverStore"></param>
        /// <param name="leverClass"></param>
        public void DeleteLowerLevelOverrides(SummaryItem summaryItem, string leverType)
        {
            if (Factory.OpenConnection())
                try
                {
                    var cmd = Factory.CreateCommand("DS887US1", CommandType.StoredProcedure,
                            Factory.CreateParameter("pperd", summaryItem.WorkBench.ToUpper().ToString().Left(1), System.Data.OleDb.OleDbType.Char, 1, ParameterDirection.Input),
                            Factory.CreateParameter("ptype", (summaryItem.StoreType == Constants.kOnline ? "C" : "B"), System.Data.OleDb.OleDbType.Char, 1, ParameterDirection.Input),
                            Factory.CreateParameter("pfgrp", summaryItem.WorkBench.ToUpper().ToString().Left(1) == "D" ? this.DailyFilegroup : this.WeeklyFilegroup, System.Data.OleDb.OleDbType.Decimal, 3, ParameterDirection.Input),
                        // LLvl         DEC (2,0)             16 - 9
                            Factory.CreateParameter("plvl", this.SelectedSummaryLevel, System.Data.OleDb.OleDbType.Decimal, 2, ParameterDirection.Input),
                        // LTyp       CHAR ( 1)             ‘A ‘/ ‘C ‘/ ‘U’ /’ P’ /’ S’ 
                            Factory.CreateParameter("pltype", leverType, System.Data.OleDb.OleDbType.Char, 1, ParameterDirection.Input),
                        // Dept      DEC (3, 0)             999
                            Factory.CreateParameter("pdept", summaryItem.Department, System.Data.OleDb.OleDbType.Decimal, 3, ParameterDirection.Input),
                        // LMkt      CHAR (2)             ‘UK’
                            Factory.CreateParameter("plmkt", summaryItem.Market, System.Data.OleDb.OleDbType.Char, 2, ParameterDirection.Input),
                        // LGrd      CHAR (3)              ‘999’
                            Factory.CreateParameter("plgrd", summaryItem.Grade, System.Data.OleDb.OleDbType.Char, 3, ParameterDirection.Input),
                        // LStr        DEC (3, 0)             999
                            Factory.CreateParameter("pstr", summaryItem.Store, System.Data.OleDb.OleDbType.Decimal, 3, ParameterDirection.Input),
                        // LCls        DEC (4, 0)             9999
                            Factory.CreateParameter("pcls", summaryItem.Class, System.Data.OleDb.OleDbType.Decimal, 4, ParameterDirection.Input)
                            );

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    ExceptionHandler.RaiseException(ex, "DeleteLowerLevelOverrides");
                }
                finally
                {
                    Factory.CloseConnection();
                }
        }

        /// <summary>
        /// Calls As400 stored procedure to apply the users changed figures to the lever files
        /// </summary>
        /// <returns></returns>
        public bool ApplyChanges()
        {
            var result = false;

            if (Factory.OpenConnection())
                try
                {
                    this.MustReleaseLocks = false;

                    var cmd = Factory.CreateCommand("DS887JS1", CommandType.StoredProcedure,
                            Factory.CreateParameter("puser", Session.User.NetworkId.ToUpper(), System.Data.OleDb.OleDbType.Char, 10, ParameterDirection.Input),
                            Factory.CreateParameter("pdept", this.DepartmentId, System.Data.OleDb.OleDbType.Decimal, 3, ParameterDirection.Input),
                            Factory.CreateParameter("pperd", (DailyFilegroup > 0 && WeeklyFilegroup > 0) ? "B" : (DailyFilegroup > 0) ? "D" : "W", System.Data.OleDb.OleDbType.Char, 1, ParameterDirection.Input),
                            Factory.CreateParameter("ptype", (IsBricksChanged && IsClicksChanged) ? "A" : IsBricksChanged ? "B" : "C", System.Data.OleDb.OleDbType.Char, 1, ParameterDirection.Input),
                            Factory.CreateParameter("pfmbr", SessionMemberName, System.Data.OleDb.OleDbType.Char, 10, ParameterDirection.Input),
                            Factory.CreateParameter("pdfgrp", DailyFilegroup, System.Data.OleDb.OleDbType.Decimal, 3, ParameterDirection.Input),
                            Factory.CreateParameter("pwfgrp", WeeklyFilegroup, System.Data.OleDb.OleDbType.Decimal, 3, ParameterDirection.Input),
                            Factory.CreateParameter("perr", string.Empty, System.Data.OleDb.OleDbType.Char, 128, ParameterDirection.InputOutput));

					cmd.ExecuteNonQuery();

                    if (cmd.Parameters["perr"].Value == System.DBNull.Value || cmd.Parameters["perr"].Value.ToString() == string.Empty)
                    {
                        result = true;

                        WeeklyFilegroup = 0;
                        DailyFilegroup = 0;
                    }
                    else
                    {
                        this.ErrorMessage = cmd.Parameters["perr"].Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    ExceptionHandler.RaiseException(ex, "ApplyChanges");
                }
                finally
                {
                    Factory.CloseConnection();
                }

            return result;
        }

        /// <summary>
        /// Calls As400 stored procedure to complete a model run based on the users changed figures
        /// </summary>
        /// <returns></returns>
        public bool RunModel()
        {
            var result = false;

            if (Factory.OpenConnection())
                try
                {
                    var cmd = Factory.CreateCommand("DS887MS1", CommandType.StoredProcedure,
                            Factory.CreateParameter("puser", Session.User.NetworkId.ToUpper(), System.Data.OleDb.OleDbType.Char, 10, ParameterDirection.Input),
                            Factory.CreateParameter("pdept", this.DepartmentId, System.Data.OleDb.OleDbType.Decimal, 3, ParameterDirection.Input),
                            
                            Factory.CreateParameter("pperd", (DailyFilegroup > 0 && WeeklyFilegroup > 0) ? "B" : (DailyFilegroup > 0) ? "D" : "W", System.Data.OleDb.OleDbType.Char, 1, ParameterDirection.Input),
                            Factory.CreateParameter("ptype", (IsBricksChanged && IsClicksChanged) ? "A" : IsBricksChanged ? "B" : "C", System.Data.OleDb.OleDbType.Char, 1, ParameterDirection.Input),
//                            Factory.CreateParameter("pperd", Workbench == Workbenches.Daily ? "D" : Workbench == Workbenches.Weekly ? "W" : "B", System.Data.OleDb.OleDbType.Char, 1, ParameterDirection.Input),
//                            Factory.CreateParameter("ptype", StoreType == StoreTypes.BricksAndMortar ? "B" : StoreType == StoreTypes.Online ? "C" : "A", System.Data.OleDb.OleDbType.Char, 1, ParameterDirection.Input),
    
                            Factory.CreateParameter("pfmbr", SessionMemberName, System.Data.OleDb.OleDbType.Char, 10, ParameterDirection.Input),
                            Factory.CreateParameter("pdfgrp", DailyFilegroup, System.Data.OleDb.OleDbType.Decimal, 3, ParameterDirection.Input),
                            Factory.CreateParameter("pwfgrp", WeeklyFilegroup, System.Data.OleDb.OleDbType.Decimal, 3, ParameterDirection.Input),
                            Factory.CreateParameter("perr", string.Empty, System.Data.OleDb.OleDbType.Char, 128, ParameterDirection.InputOutput));

                    cmd.ExecuteNonQuery();

                    if (cmd.Parameters["perr"].Value == System.DBNull.Value || cmd.Parameters["perr"].Value.ToString() == string.Empty)
                    {
                        result = true;
                    }
                    else
                    {
                        this.ErrorMessage = cmd.Parameters["perr"].Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    ExceptionHandler.RaiseException(ex, "ModelRun");
                }
                finally
                {
                    Factory.CloseConnection();
                }

            return result;
        }

        /// <summary>
        /// Controls obtaining summary workbench data
        /// </summary>
        public void InitialDataLoad()
        {
            AliasCreate();
            var data = GetData();
            if (data == null)
                SummaryData = null;
            else
                SummaryData = data.AsEnumerable();
            WorkingSummaryItems = GetWorkingData(-1);

			if (OriginalItems == null)
				OriginalItems = GetWorkingData(-1);
        }

        /// <summary>
        /// Sets initial grid display level
        /// </summary>
        public void InitialiseScreen()
        {
            SelectedSummaryLevel = 16;
        }

        /// <summary>
        /// Creates the alias in the summary file. Required to access the specified member in the file
        /// </summary>
        /// <returns></returns>
        public bool AliasCreate()
        {
            var result = false;

            if (Factory.OpenConnection())
                try
                {
                    var sql = Properties.Resources.SQLSummaryWorkbenchAliasCreate
                        .Replace("<alias>", this.SessionMemberName)
                        .Replace("<member>", this.SessionMemberName);

                    var cmd = Factory.CreateCommand(sql);
                    result = cmd.ExecuteNonQuery() > 0;
                }
                catch (OleDbException ex)
                {
                    if (!ex.Message.StartsWith("SQL0601")) // it is OK if the alias member already exists
                        ExceptionHandler.RaiseException(ex, "AliasCreate");
                }
                catch (Exception ex)
                {
                    ExceptionHandler.RaiseException(ex, "AliasCreate");
                }
                finally
                {
                    Factory.CloseConnection();
                }

            return result;
        }

        /// <summary>
        /// Drops the alias for the summary file
        /// </summary>
        /// <returns></returns>
        public bool AliasDrop()
        {
            var result = false;

            if (Factory.OpenConnection())
                try
                {
                    var sql = Properties.Resources.SQLSummaryWorkbenchAliasDrop
                        .Replace("<alias>", this.SessionMemberName);

                    var cmd = Factory.CreateCommand(sql);
                    result = cmd.ExecuteNonQuery() > 0;
                }
                catch (OleDbException ex)
                {
                    if (!ex.Message.Contains("*FILE not found")) // it is OK if the alias does not exists
                        ExceptionHandler.RaiseException(ex, "AliasDrop");
                }
                catch (Exception ex)
                {
                    ExceptionHandler.RaiseException(ex, "AliasDrop");
                }
                finally
                {
                    Factory.CloseConnection();
                }

            return result;
        }

		private bool _lockFilesRunning = false;
        /// <summary>
        /// Call AS400 stored procedure to lock files summary workbench files by department 
        /// </summary>
        /// <param name="workbench"></param>
        /// <returns></returns>
        public bool LockFiles(string workbench)
        {
            var result = false;

			if (!_lockFilesRunning)
			{
				_lockFilesRunning = true;
				if ((workbench == Constants.kWeekly && WeeklyFilegroup != 0) || (workbench == Constants.kDaily && DailyFilegroup != 0))
					result = true;
				else if (Factory.OpenConnection())
					try
					{
						OnProgress("Obtaining next " + workbench + " filegroup");

						var cmd = Factory.CreateCommand("DS886ES1", CommandType.StoredProcedure,
							Factory.CreateParameter("puser", Session.User.NetworkId.ToUpper(), System.Data.OleDb.OleDbType.Char, 10, ParameterDirection.Input),
							Factory.CreateParameter("pdept", this.DepartmentId, System.Data.OleDb.OleDbType.Decimal, 3, ParameterDirection.Input),
							Factory.CreateParameter("pdlvl", "0", System.Data.OleDb.OleDbType.Char, 1, ParameterDirection.Input),
							Factory.CreateParameter("pperd", workbench.ToUpper().ToString().Left(1), System.Data.OleDb.OleDbType.Char, 1, ParameterDirection.Input),
							Factory.CreateParameter("pfgrp", 0, System.Data.OleDb.OleDbType.Decimal, 3, ParameterDirection.InputOutput),
							Factory.CreateParameter("perr", string.Empty, System.Data.OleDb.OleDbType.Char, 128, ParameterDirection.InputOutput));

						cmd.ExecuteNonQuery();

						if (cmd.Parameters["perr"].Value == System.DBNull.Value || cmd.Parameters["perr"].Value.ToString() == string.Empty)
						{
							if (workbench == Constants.kWeekly)
								WeeklyFilegroup = Convert.ToDecimal(cmd.Parameters["pfgrp"].Value);
							else
								DailyFilegroup = Convert.ToDecimal(cmd.Parameters["pfgrp"].Value);
							result = true;
						}
						else
							this.ErrorMessage = Factory.ReplaceDate(cmd.Parameters["perr"].Value.ToString());
					}
					catch (Exception ex)
					{
						ExceptionHandler.RaiseException(ex, "LockFiles");
					}
					finally
					{
						Factory.CloseConnection();
					}

				_lockFilesRunning = false;
			}
            return result;
        }

        /// <summary>
        /// Determines if locks need to be released
        /// </summary>
        public void ReleaseLocks()
        {
            if (IsDailyLocked)
                ReleaseLock(Constants.Workbenches.Daily);
            if (IsWeeklyLocked)
                ReleaseLock(Constants.Workbenches.Weekly);
        }

        /// <summary>
        /// Controls refresh of screen with new data
        /// </summary>
        public void WorkbenchInitialLoad()
        {
            InitialiseWorkbench();
            InitialDataLoad();
            InitialiseScreen();
        }

        /// <summary>
        /// Used to save all changed items back to the AS400
        /// </summary>
        public bool SaveChanges()
        {
            bool saved = false;
            try
            {
				for (int i=0; i<WorkingSummaryItems.Count; i++)
				{
					var item = WorkingSummaryItems[i];
					var originalItem = OriginalItems[i];

					if (item.Changed.Equals("1"))
					{

#pragma warning disable 219
						var rowChanged = false;
						if (item.AllocationStatus != "U" && 
							(item.Allocation != originalItem.Allocation
							|| item.AllocationActualFlag != originalItem.AllocationActualFlag
							|| item.AllocationInheritedLevel != originalItem.AllocationInheritedLevel
							|| item.AllocationOverrideLevel != originalItem.AllocationOverrideLevel))
							rowChanged = true;
						else
							item.AllocationStatus = "U";

						if (item.CutOffStatus != "U" &&
							(item.CutOff != originalItem.CutOff
							|| item.CutOffActualFlag != originalItem.CutOffActualFlag
							|| item.CutOffInheritedLevel != originalItem.CutOffInheritedLevel
							|| item.CutOffOverrideLevel != originalItem.CutOffOverrideLevel))
							rowChanged = true;
						else
							item.CutOffStatus = "U";

						if (item.PatternStatus != "U" &&
							(item.Pattern != originalItem.Pattern
							|| item.PatternActualFlag != originalItem.PatternActualFlag
							|| item.PatternInheritedLevel != originalItem.PatternInheritedLevel
							|| item.PatternOverrideLevel != originalItem.PatternOverrideLevel))
							rowChanged = true;
						else
							item.PatternStatus = "U";

						if (item.SmoothFactorStatus != "U" &&
							(item.SmoothFactor != originalItem.SmoothFactor
							|| item.SmoothFactorActualFlag != originalItem.SmoothFactorActualFlag
							|| item.SmoothFactorInheritedLevel != originalItem.SmoothFactorInheritedLevel
							|| item.SmoothFactorOverrideLevel != originalItem.SmoothFactorOverrideLevel))
							rowChanged = true;
						else
							item.SmoothFactorStatus = "U";

						if (item.UpliftStatus != "U" &&
							(item.UpliftFactor != originalItem.UpliftFactor
							|| item.UpliftActualFlag != originalItem.UpliftActualFlag
							|| item.UpliftInheritedLevel != originalItem.UpliftInheritedLevel
							|| item.UpliftOverrideLevel != originalItem.UpliftOverrideLevel))
							rowChanged = true;
						else
							item.UpliftStatus = "U";

                        //if (!rowChanged)
                        //    item.Changed = "0";
                    }
#pragma warning restore 219
                }

                var changedItems = from summaryItem in WorkingSummaryItems
                                   where summaryItem.Changed.Equals("1")
                                   select summaryItem;

                ChangedSummaryItems = changedItems.ToList();
                saved = Save();

            }
            catch (Exception ex)
            {
                ExceptionHandler.RaiseException(ex, "SaveChanges");
            }

            return saved;
        }

        /// <summary>
        /// Reloads original data to working list
        /// </summary>
        public void RebuildWorkingSummaryItems()
        {
            WorkingSummaryItems = GetWorkingData(-1);
            DailyFilegroup = 0;
            WeeklyFilegroup = 0;
        }

        /// <summary>
        /// Controls propogation of changes to all levels
        /// </summary>
        /// <param name="FromLevel"></param>
        public void PropagateChanges(int FromLevel)
        {
            PropagateAllocationChanges(FromLevel);
            PropagateCutOffChanges(FromLevel);
            PropagatePatternChanges(FromLevel);
            PropagateSmoothChanges(FromLevel);
            PropagateUpliftChanges(FromLevel);
        }

        /// <summary>
        /// Provides file member cleanup and department unlocking
        /// </summary>
        /// <returns></returns>
        public bool CleanupAS400FileMembers()
        {
            var result = false;

            if (this.MustReleaseLocks)
                if (Factory.OpenConnection())
                    try
                    {
                        OnProgress("Unlocking department");

                        var cmd = Factory.CreateCommand("DS887IS1", CommandType.StoredProcedure,
                            Factory.CreateParameter("puser", Session.User.NetworkId.ToUpper(), System.Data.OleDb.OleDbType.Char, 10, ParameterDirection.Input),
                            Factory.CreateParameter("pdept", this.DepartmentId, System.Data.OleDb.OleDbType.Decimal, 3, ParameterDirection.Input),
                            Factory.CreateParameter("pdlvl", "0", System.Data.OleDb.OleDbType.Char, 1, ParameterDirection.Input),
                            Factory.CreateParameter("pdfgrp", DailyFilegroup, System.Data.OleDb.OleDbType.Decimal, 3, ParameterDirection.Input),
                            Factory.CreateParameter("pwfgrp", WeeklyFilegroup, System.Data.OleDb.OleDbType.Decimal, 3, ParameterDirection.Input),
                            Factory.CreateParameter("pfmbr", SessionMemberName, System.Data.OleDb.OleDbType.Char, 10, ParameterDirection.Input),
                            Factory.CreateParameter("perr", string.Empty, System.Data.OleDb.OleDbType.Char, 128, ParameterDirection.InputOutput));

                        cmd.ExecuteNonQuery();

                        if (cmd.Parameters["perr"].Value == System.DBNull.Value || cmd.Parameters["perr"].Value.ToString() == string.Empty)
                        {
                            result = true;
                            WeeklyFilegroup = 0;
                            DailyFilegroup = 0;
                        }
                        else
                        {
                            this.ErrorMessage = cmd.Parameters["perr"].Value.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.RaiseException(ex, "CleanupAS400FileMembers");
                    }
                    finally
                    {
                        Factory.CloseConnection();
                    }

            return result;
        }

        /// <summary>
        /// Handles removing overrides at lower levels
        /// </summary>
        /// <param name="clickedSummaryItem"></param>
        /// <param name="currentSummaryLever"></param>
        public void RemoveOverridesBelowLevel(SummaryItem clickedSummaryItem, SummaryWorkbenchInfo.SummaryLever currentSummaryLever)
        {
            // Update all records at lower levels that match the key from this level
            var query = from row in WorkingSummaryItems
                        where row.HierarchyKey < SelectedSummaryLevel // Must be a lower level

                        where row.StoreType.Equals(clickedSummaryItem.StoreType)
                        where row.WorkBench.Equals(clickedSummaryItem.WorkBench)
                        select row;

            // Key must match

            //Dept
            if (new int[] { 16, 15, 14, 13, 12, 11, 10, 9 }.Contains(SelectedSummaryLevel))
            {
                query = query.Where(r => r.Department.Equals(clickedSummaryItem.Department));
            }

            //Market
            if (new int[] { 15, 14, 13, 11, 10, 9 }.Contains(SelectedSummaryLevel))
            {
                query = query.Where(r => r.Market.Equals(clickedSummaryItem.Market));
            }

            //Grade
            if (new int[] { 14, 13, 10, 9 }.Contains(SelectedSummaryLevel))
            {
                query = query.Where(r => r.Grade.Equals(clickedSummaryItem.Grade));
            }

            //Class
            if (new int[] { 12, 11, 10, 9 }.Contains(SelectedSummaryLevel))
            {
                query = query.Where(r => r.Class.Equals(clickedSummaryItem.Class));
            }

            // Select Actual Lever Overrides only
            if (currentSummaryLever == SummaryLever.Allocation)
            {
                query = query.Where(r => r.AllocationActualFlag.Equals(true));
            }
            if (currentSummaryLever == SummaryLever.CutOff)
            {
                query = query.Where(r => r.CutOffActualFlag.Equals(true));
            }
            if (currentSummaryLever == SummaryLever.Pattern)
            {
                query = query.Where(r => r.PatternActualFlag.Equals(true));
            }
            if (currentSummaryLever == SummaryLever.SmoothingFactor)
            {
                query = query.Where(r => r.SmoothFactorActualFlag.Equals(true));
            }
            if (currentSummaryLever == SummaryLever.UpliftFactor)
            {
                query = query.Where(r => r.UpliftActualFlag.Equals(true));
            }

            foreach (SummaryItem lowerLevelSummaryItem in query)
            {
                if (currentSummaryLever == SummaryLever.Allocation)
                {
                    lowerLevelSummaryItem.AllocationActualFlag = false;
                    lowerLevelSummaryItem.AllocationStatus = "D";
                    lowerLevelSummaryItem.Changed = "1";
                }
                if (currentSummaryLever == SummaryLever.CutOff)
                {
                    lowerLevelSummaryItem.CutOffActualFlag = false;
                    lowerLevelSummaryItem.CutOffStatus = "D";
                    lowerLevelSummaryItem.Changed = "1";
                }
                if (currentSummaryLever == SummaryLever.Pattern)
                {
                    lowerLevelSummaryItem.PatternActualFlag = false;
                    lowerLevelSummaryItem.PatternStatus = "D";
                    lowerLevelSummaryItem.Changed = "1";
                }
                if (currentSummaryLever == SummaryLever.SmoothingFactor)
                {
                    lowerLevelSummaryItem.SmoothFactorActualFlag = false;
                    lowerLevelSummaryItem.SmoothFactorStatus = "D";
                    lowerLevelSummaryItem.Changed = "1";
                }
                if (currentSummaryLever == SummaryLever.UpliftFactor)
                {
                    lowerLevelSummaryItem.UpliftActualFlag = false;
                    lowerLevelSummaryItem.UpliftStatus = "D";
                    lowerLevelSummaryItem.Changed = "1";
                }

            }
        }

        /// <summary>
        /// Wrapper function to determine whether the work files and members are ready for a model run or apply to be performed.
        /// This is indicated by an M existing on the locking file for the current department, user and filegroup
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsModelRunAvailable()
        {
            bool isAvailable = false;

            var members = "";
            var workbenchTypes = "";
            var expectedFileGroupsReady = 0;

            OnProgress("Checking files are ready for processing...");

            if (DailyFilegroup > 0)
            {
                expectedFileGroupsReady++;
                members += "'F" + DailyFilegroup.ToString("000") + "'";
                workbenchTypes += "'DAILY'";
            }
            if (DailyFilegroup > 0 && WeeklyFilegroup > 0)
            {
                members += ", ";
                workbenchTypes += ", ";
            }
            if (WeeklyFilegroup > 0)
            {
                expectedFileGroupsReady++;
                members += "'DPT" + DepartmentId.ToString("000") + WeeklyFilegroup.ToString("000") + "'";
                workbenchTypes += "'WEEKLY'";
            }

            var sql = "LOUSR = '<user>' AND LODEPT = <department> AND LOMBR IN (<members>) AND LOTYP IN (<workbechTypes>) AND LOMOD = 'S' AND LOGUID = 'M'"
                .Replace("<user>", Session.User.NetworkId.ToUpper())
                .Replace("<department>", this.DepartmentId.ToString())
                .Replace("<members>", members)
                .Replace("<workbechTypes>", workbenchTypes);

			// automatic retry for 60 seconds
			var timeStart = System.DateTime.Now;
			while (!isAvailable && System.DateTime.Now.Subtract(timeStart).TotalSeconds < 60)
			{
				var readyFileGroupCount = Factory.GetValue("DSSRLCK", "COUNT(*)", sql, 0);
				// System is ready for a model run if the count of records found equals the number of filegroups that are locked.
				if (readyFileGroupCount != null)
					if (Convert.ToInt32(readyFileGroupCount) == expectedFileGroupsReady)
						isAvailable = true;

				// wait a second.
				if (!isAvailable)
					Thread.Sleep(1000);
			}
            OnProgress("");

            return isAvailable;

        }

        //-------------------------------------------------------------------------------
        #endregion

        #region Private properties and methods
        //-------------------------------------------------------------------------------

        /// <summary>
        /// Used to find all changed allocation flags at the grid/level we are navigating away from
        /// </summary>
        /// <param name="fromLevel"></param>
        private void PropagateAllocationChanges(int fromLevel)
        {
            // Process all changed records
            foreach (SummaryItem changedSummaryItem in WorkingSummaryItems
                .Where(i => "CA".Contains(i.AllocationStatus)) // process changed records
                .Where(i => i.HierarchyKey.Equals(fromLevel)) // for the current level
                )
            {
                ApplyAllocationToLowerLevels(fromLevel, changedSummaryItem);
                ApplyAllocationToHigherLevels(fromLevel, changedSummaryItem);
            }
        }

        /// <summary>
        /// Used to find all changed cutoffs at the grid/level we are navigating away from
        /// </summary>
        /// <param name="fromLevel"></param>
        private void PropagateCutOffChanges(int fromLevel)
        {
            // Process all changed records
            foreach (SummaryItem changedSummaryItem in WorkingSummaryItems
                .Where(i => "CA".Contains(i.CutOffStatus)) // process changed records
                .Where(i => i.HierarchyKey.Equals(fromLevel)) // for the current level
                )
            {
                ApplyCutoffToLowerLevels(fromLevel, changedSummaryItem);
                ApplyCutoffToHigherLevels(fromLevel, changedSummaryItem);
            }

        }

        /// <summary>
        /// Used to find all changed patterns at the grid/level we are navigating away from
        /// </summary>
        /// <param name="fromLevel"></param>
        private void PropagatePatternChanges(int fromLevel)
        {
            // Process all changed records
            foreach (SummaryItem changedSummaryItem in WorkingSummaryItems
                .Where(i => "CA".Contains(i.PatternStatus)) // process changed records
                .Where(i => i.HierarchyKey.Equals(fromLevel)) // for the current level
                )
            {
                ApplyPatternToLowerLevels(fromLevel, changedSummaryItem);
                ApplyPatternToHigherLevels(fromLevel, changedSummaryItem);
            }

        }

        /// <summary>
        /// Used to find all changed smoothing factors at the grid/level we are navigating away from
        /// </summary>
        /// <param name="fromLevel"></param>
        private void PropagateSmoothChanges(int fromLevel)
        {
            // Process all changed records
            foreach (SummaryItem changedSummaryItem in WorkingSummaryItems
                .Where(i => "CA".Contains(i.SmoothFactorStatus)) // process changed records
                .Where(i => i.HierarchyKey.Equals(fromLevel)) // for the current level
                )
            {
                ApplySmoothFactorToLowerLevels(fromLevel, changedSummaryItem);
                ApplySmoothFactorToHigherLevels(fromLevel, changedSummaryItem);
            }

        }

        /// <summary>
        /// Used to find all changed uplift factors at the grid/level we are navigating away from
        /// </summary>
        /// <param name="fromLevel"></param>
        private void PropagateUpliftChanges(int FromLevel)
        {
            // Process all changed records
            foreach (SummaryItem changedSummaryItem in WorkingSummaryItems
                .Where(i => "CA".Contains(i.UpliftStatus)) // process changed records
                .Where(i => i.HierarchyKey.Equals(FromLevel)) // for the current level
                )
            {
                ApplyUpliftFactorToLowerLevels(FromLevel, changedSummaryItem);
                ApplyUpliftFactorToHigherLevels(FromLevel, changedSummaryItem);
            }
        }

        /// <summary>
        /// Applies changes from the current level to the inherited values at lower levels
        /// </summary>
        /// <param name="FromLevel"></param>
        /// <param name="changedSummaryItem"></param>
        public void ApplyAllocationToLowerLevels(int FromLevel, SummaryItem changedSummaryItem)
        {
            // get lever records that will need this change Propagated to
            var query = from row in WorkingSummaryItems
                        where row.HierarchyKey < FromLevel // Must be a lower level
                        where row.StoreType.Equals(changedSummaryItem.StoreType)
                        where row.WorkBench.Equals(changedSummaryItem.WorkBench)
                        where row.AllocationInheritedLevel >= FromLevel // Must be previously inherited from at least the current level
                        where row.AllocationActualFlag != true // Must not over-write existing lower level levers
                        select row;

            //Dept
            if (new int[] { 16, 15, 14, 13, 12, 11, 10, 9 }.Contains(FromLevel))
            {
                query = query.Where(r => r.Department.Equals(changedSummaryItem.Department));
            }

            //Market
            if (new int[] { 15, 14, 13, 11, 10, 9 }.Contains(FromLevel))
            {
                query = query.Where(r => r.Market.Equals(changedSummaryItem.Market));
            }

            //Grade
            if (new int[] { 14, 13, 10, 9 }.Contains(FromLevel))
            {
                query = query.Where(r => r.Grade.Equals(changedSummaryItem.Grade));
            }

            //Class
            if (FromLevel <= 12
                && new int[] { 12, 11, 10, 9 }.Contains(FromLevel))
            {
                query = query.Where(r => r.Class.Equals(changedSummaryItem.Class));
            }

            // Update all records at the lower level that match the key from this level, until we meet another Override
            foreach (SummaryItem lowerLevelSummaryItem in query)
            {
                // check the type of change and apply an override where the lower level is not already an actual
                if ("CA".Contains(changedSummaryItem.AllocationStatus))
                {
                    lowerLevelSummaryItem.Allocation = changedSummaryItem.Allocation;
                    lowerLevelSummaryItem.AllocationInheritedLevel = changedSummaryItem.HierarchyKey;
                }
            }
        }

        /// <summary>
        /// Applies changes from the current level to the inherited values at lower levels
        /// </summary>
        /// <param name="FromLevel"></param>
        /// <param name="changedSummaryItem"></param>
        public void ApplyCutoffToLowerLevels(int FromLevel, SummaryItem changedSummaryItem)
        {
            var query = from row in WorkingSummaryItems
                        where row.HierarchyKey < FromLevel // Must be a lower level
                        where row.StoreType.Equals(changedSummaryItem.StoreType)
                        where row.WorkBench.Equals(changedSummaryItem.WorkBench)
                        where row.CutOffInheritedLevel >= FromLevel // Must be previously inherited from at least the current level
                        where row.CutOffActualFlag != true // Must not over-write existing lower level levers
                        select row;

            //Dept
            if (new int[] { 16, 15, 14, 13, 12, 11, 10, 9 }.Contains(FromLevel))
            {
                query = query.Where(r => r.Department.Equals(changedSummaryItem.Department));
            }

            //Market
            if (new int[] { 15, 14, 13, 11, 10, 9 }.Contains(FromLevel))
            {
                query = query.Where(r => r.Market.Equals(changedSummaryItem.Market));
            }

            //Grade
            if (new int[] { 14, 13, 10, 9 }.Contains(FromLevel))
            {
                query = query.Where(r => r.Grade.Equals(changedSummaryItem.Grade));
            }

            //Class
            if (FromLevel <= 12
                && new int[] { 12, 11, 10, 9 }.Contains(FromLevel))
            {
                query = query.Where(r => r.Class.Equals(changedSummaryItem.Class));
            }

            // Update all records at the lower level that match the key from this level, until we meet another Override
            foreach (SummaryItem lowerLevelSummaryItem in query)
            {
                // check the type of change and apply an override where the lower level is not already an actual
                if ("CA".Contains(changedSummaryItem.CutOffStatus))
                {
                    lowerLevelSummaryItem.CutOff = changedSummaryItem.CutOff;
                    lowerLevelSummaryItem.CutOffInheritedLevel = changedSummaryItem.HierarchyKey;
                }
            }
        }

        /// <summary>
        /// Applies changes from the current level to the inherited values at lower levels
        /// </summary>
        /// <param name="FromLevel"></param>
        /// <param name="changedSummaryItem"></param>
        public void ApplyPatternToLowerLevels(int FromLevel, SummaryItem changedSummaryItem)
        {
            var query = from row in WorkingSummaryItems
                        where row.HierarchyKey < FromLevel // Must be a lower level
                        where row.StoreType.Equals(changedSummaryItem.StoreType)
                        where row.WorkBench.Equals(changedSummaryItem.WorkBench)
                        where row.PatternInheritedLevel >= FromLevel // Must be previously inherited from at least the current level
                        where row.PatternActualFlag != true // Must not over-write existing lower level levers
                        select row;

            //Dept
            if (new int[] { 16, 15, 14, 13, 12, 11, 10, 9 }.Contains(FromLevel))
            {
                query = query.Where(r => r.Department.Equals(changedSummaryItem.Department));
            }

            //Market
            if (new int[] { 15, 14, 13, 11, 10, 9 }.Contains(FromLevel))
            {
                query = query.Where(r => r.Market.Equals(changedSummaryItem.Market));
            }

            //Grade
            if (new int[] { 14, 13, 10, 9 }.Contains(FromLevel))
            {
                query = query.Where(r => r.Grade.Equals(changedSummaryItem.Grade));
            }

            //Class
            if (FromLevel <= 12
                && new int[] { 12, 11, 10, 9 }.Contains(FromLevel))
            {
                query = query.Where(r => r.Class.Equals(changedSummaryItem.Class));
            }

            // Update all records at the lower level that match the key from this level, until we meet another Override
            foreach (SummaryItem lowerLevelSummaryItem in query)
            {

                // check the type of change and apply an override where the lower level is not already an actual
                if ("CA".Contains(changedSummaryItem.PatternStatus))
                {
                    lowerLevelSummaryItem.Pattern = changedSummaryItem.Pattern;
                    lowerLevelSummaryItem.PatternInheritedLevel = changedSummaryItem.HierarchyKey;
                }
            }
        }

        /// <summary>
        /// Applies changes from the current level to the inherited values at lower levels
        /// </summary>
        /// <param name="FromLevel"></param>
        /// <param name="changedSummaryItem"></param>
        public void ApplySmoothFactorToLowerLevels(int FromLevel, SummaryItem changedSummaryItem)
        {
            var query = from row in WorkingSummaryItems
                        where row.HierarchyKey < FromLevel // Must be a lower level
                        where row.StoreType.Equals(changedSummaryItem.StoreType)
                        where row.WorkBench.Equals(changedSummaryItem.WorkBench)
                        where row.SmoothFactorInheritedLevel >= FromLevel // Must be previously inherited from at least the current level
                        where row.SmoothFactorActualFlag != true // Must not over-write existing lower level levers
                        select row;

            //Dept
            if (new int[] { 16, 15, 14, 13, 12, 11, 10, 9 }.Contains(FromLevel))
            {
                query = query.Where(r => r.Department.Equals(changedSummaryItem.Department));
            }

            //Market
            if (new int[] { 15, 14, 13, 11, 10, 9 }.Contains(FromLevel))
            {
                query = query.Where(r => r.Market.Equals(changedSummaryItem.Market));
            }

            //Grade
            if (new int[] { 14, 13, 10, 9 }.Contains(FromLevel))
            {
                query = query.Where(r => r.Grade.Equals(changedSummaryItem.Grade));
            }

            //Class
            if (FromLevel <= 12
                && new int[] { 12, 11, 10, 9 }.Contains(FromLevel))
            {
                query = query.Where(r => r.Class.Equals(changedSummaryItem.Class));
            }

            // Update all records at the lower level that match the key from this level, until we meet another Override
            foreach (SummaryItem lowerLevelSummaryItem in query)
            {

                // check the type of change and apply an override where the lower level is not already an actual
                if ("CA".Contains(changedSummaryItem.SmoothFactorStatus))
                {
                    lowerLevelSummaryItem.SmoothFactor = changedSummaryItem.SmoothFactor;
                    lowerLevelSummaryItem.SmoothFactorInheritedLevel = changedSummaryItem.HierarchyKey;
                }
            }
        }

        /// <summary>
        /// Applies changes from the current level to the inherited values at lower levels
        /// </summary>
        /// <param name="FromLevel"></param>
        /// <param name="changedSummaryItem"></param>
        public void ApplyUpliftFactorToLowerLevels(int FromLevel, SummaryItem changedSummaryItem)
        {
            var query = from row in WorkingSummaryItems
                        where row.HierarchyKey < FromLevel // Must be a lower level
                        where row.StoreType.Equals(changedSummaryItem.StoreType)
                        where row.WorkBench.Equals(changedSummaryItem.WorkBench)
                        where row.UpliftInheritedLevel >= FromLevel // Must be previously inherited from at least the current level
                        where row.UpliftActualFlag != true // Must not over-write existing lower level levers
                        select row;

            //Dept
            if (new int[] { 16, 15, 14, 13, 12, 11, 10, 9 }.Contains(FromLevel))
            {
                query = query.Where(r => r.Department.Equals(changedSummaryItem.Department));
            }

            //Market
            if (new int[] { 15, 14, 13, 11, 10, 9 }.Contains(FromLevel))
            {
                query = query.Where(r => r.Market.Equals(changedSummaryItem.Market));
            }

            //Grade
            if (new int[] { 14, 13, 10, 9 }.Contains(FromLevel))
            {
                query = query.Where(r => r.Grade.Equals(changedSummaryItem.Grade));
            }

            //Class
            if (FromLevel <= 12
                && new int[] { 12, 11, 10, 9 }.Contains(FromLevel))
            {
                query = query.Where(r => r.Class.Equals(changedSummaryItem.Class));
            }

            // Update all records at the lower level that match the key from this level, until we meet another Override
            foreach (SummaryItem lowerLevelSummaryItem in query)
            {
                // check the type of change and apply an override where the lower level is not already an actual
                if ("CA".Contains(changedSummaryItem.UpliftStatus)) // TODO: Should this apply 'D'eletions from higher levels? e.g. when a Remove Levers from Level has been applied to the level above?
                {
                    lowerLevelSummaryItem.UpliftFactor = changedSummaryItem.UpliftFactor;
                    lowerLevelSummaryItem.UpliftInheritedLevel = changedSummaryItem.HierarchyKey;
                }
            }
        }

        /// <summary>
        /// Applies changes from the current level to the 'has override at lower level' values at higher levels
        /// </summary>
        /// <param name="FromLevel"></param>
        /// <param name="changedSummaryItem"></param>
        private void ApplyAllocationToHigherLevels(int FromLevel, SummaryItem changedSummaryItem)
        {
            if (FromLevel < 16)
            {
                // We need to Propagate changes to each of the higher levels

                // get higher level lever records that will need to reflect this change in their 'lower level overrides'
                var queryLeversWithLowerLevelOverrides = from row in WorkingSummaryItems
                                                         where row.StoreType.Equals(changedSummaryItem.StoreType)
                                                         where row.WorkBench.Equals(changedSummaryItem.WorkBench)
                                                         where row.AllocationOverrideLevel <= FromLevel // Must previously not have had Overrides at lower levels
                                                         select row;

                // establish key fields for higher levels
                switch (FromLevel)
                {
                    // Dept/Mkt
                    case 15:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16));
                        break;
                    // Dept/Mkt/Grd
                    case 14:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16)
                                     || (r.HierarchyKey.Equals(15)
                                     && r.Market.Equals(changedSummaryItem.Market))
                                     );
                        break;
                    // Dept/Mkt/Grd/Str
                    case 13:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16)
                                    || (r.HierarchyKey.Equals(15)
                                     && r.Market.Equals(changedSummaryItem.Market))
                                    || (r.HierarchyKey.Equals(14)
                                     && r.Market.Equals(changedSummaryItem.Market)
                                     && r.Grade.Equals(changedSummaryItem.Grade))
                                     );
                        break;
                    // Cls
                    case 12:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16));
                        break;
                    // Cls/Mkt
                    case 11:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16)
                                || (r.HierarchyKey.Equals(12)
                                && r.Class.Equals(changedSummaryItem.Class))
                                     );
                        break;
                    // Cls/Mkt/Grd
                    case 10:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16)
                                || (r.HierarchyKey.Equals(12)
                                && r.Class.Equals(changedSummaryItem.Class))
                                || (r.HierarchyKey.Equals(11)
                                && r.Class.Equals(changedSummaryItem.Class)
                                && r.Market.Equals(changedSummaryItem.Market))
                                     );
                        break;
                    // Cls/Mkt/Grd/Str
                    case 9:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16)
                                || (r.HierarchyKey.Equals(12)
                                && r.Class.Equals(changedSummaryItem.Class))
                                || (r.HierarchyKey.Equals(11)
                                && r.Class.Equals(changedSummaryItem.Class)
                                && r.Market.Equals(changedSummaryItem.Market))
                                || (r.HierarchyKey.Equals(10)
                                && r.Class.Equals(changedSummaryItem.Class)
                                && r.Market.Equals(changedSummaryItem.Market)
                                && r.Grade.Equals(changedSummaryItem.Grade))
                                     );
                        break;
                }

                // Update all records at the higher level that match the key details from this level
                foreach (SummaryItem higherLevelSummaryItem in queryLeversWithLowerLevelOverrides)
                {
                    // check the type of change and apply an override where the lower level is not already an actual
                    if ("CA".Contains(changedSummaryItem.AllocationStatus))
                    {
                        higherLevelSummaryItem.AllocationOverrideLevel = changedSummaryItem.HierarchyKey;
                    }
                }
            }
        }

        /// <summary>
        /// Applies changes from the current level to the 'has override at lower level' values at higher levels
        /// </summary>
        /// <param name="FromLevel"></param>
        /// <param name="changedSummaryItem"></param>
        private void ApplyCutoffToHigherLevels(int FromLevel, SummaryItem changedSummaryItem)
        {
            if (FromLevel < 16)
            {
                // We need to Propagate changes to each of the higher levels

                // get higher level lever records that will need to reflect this change in their 'lower level overrides'
                var queryLeversWithLowerLevelOverrides = from row in WorkingSummaryItems
                                                         where row.StoreType.Equals(changedSummaryItem.StoreType)
                                                         where row.WorkBench.Equals(changedSummaryItem.WorkBench)
                                                         where row.CutOffOverrideLevel <= FromLevel // Must previously not have had Overrides at lower levels
                                                         select row;

                // establish key fields for higher levels
                switch (FromLevel)
                {
                    // Dept/Mkt
                    case 15:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16));
                        break;
                    // Dept/Mkt/Grd
                    case 14:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16)
                                     || (r.HierarchyKey.Equals(15)
                                     && r.Market.Equals(changedSummaryItem.Market))
                                     );
                        break;
                    // Dept/Mkt/Grd/Str
                    case 13:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16)
                                    || (r.HierarchyKey.Equals(15)
                                     && r.Market.Equals(changedSummaryItem.Market))
                                    || (r.HierarchyKey.Equals(14)
                                     && r.Market.Equals(changedSummaryItem.Market)
                                     && r.Grade.Equals(changedSummaryItem.Grade))
                                     );
                        break;
                    // Cls
                    case 12:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16));
                        break;
                    // Cls/Mkt
                    case 11:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16)
                                || (r.HierarchyKey.Equals(12)
                                && r.Class.Equals(changedSummaryItem.Class))
                                     );
                        break;
                    // Cls/Mkt/Grd
                    case 10:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16)
                                || (r.HierarchyKey.Equals(12)
                                && r.Class.Equals(changedSummaryItem.Class))
                                || (r.HierarchyKey.Equals(11)
                                && r.Class.Equals(changedSummaryItem.Class)
                                && r.Market.Equals(changedSummaryItem.Market))
                                     );
                        break;
                    // Cls/Mkt/Grd/Str
                    case 9:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16)
                                || (r.HierarchyKey.Equals(12)
                                && r.Class.Equals(changedSummaryItem.Class))
                                || (r.HierarchyKey.Equals(11)
                                && r.Class.Equals(changedSummaryItem.Class)
                                && r.Market.Equals(changedSummaryItem.Market))
                                || (r.HierarchyKey.Equals(10)
                                && r.Class.Equals(changedSummaryItem.Class)
                                && r.Market.Equals(changedSummaryItem.Market)
                                && r.Grade.Equals(changedSummaryItem.Grade))
                                     );
                        break;
                }

                // Update all records at the higher level that match the key details from this level
                foreach (SummaryItem higherLevelSummaryItem in queryLeversWithLowerLevelOverrides)
                {
                    // check the type of change and apply an override where the lower level is not already an actual
                    if ("CA".Contains(changedSummaryItem.CutOffStatus))
                    {
                        higherLevelSummaryItem.CutOffOverrideLevel = changedSummaryItem.HierarchyKey;
                    }
                }
            }
        }

        /// <summary>
        /// Applies changes from the current level to the 'has override at lower level' values at higher levels
        /// </summary>
        /// <param name="FromLevel"></param>
        /// <param name="changedSummaryItem"></param>
        private void ApplyPatternToHigherLevels(int FromLevel, SummaryItem changedSummaryItem)
        {
            if (FromLevel < 16)
            {
                // We need to Propagate changes to each of the higher levels

                // get higher level lever records that will need to reflect this change in their 'lower level overrides'
                var queryLeversWithLowerLevelOverrides = from row in WorkingSummaryItems
                                                         where row.StoreType.Equals(changedSummaryItem.StoreType)
                                                         where row.WorkBench.Equals(changedSummaryItem.WorkBench)
                                                         where row.PatternOverrideLevel <= FromLevel // Must previously not have had Overrides at lower levels
                                                         select row;

                // establish key fields for higher levels
                switch (FromLevel)
                {
                    // Dept/Mkt
                    case 15:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16));
                        break;
                    // Dept/Mkt/Grd
                    case 14:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16)
                                     || (r.HierarchyKey.Equals(15)
                                     && r.Market.Equals(changedSummaryItem.Market))
                                     );
                        break;
                    // Dept/Mkt/Grd/Str
                    case 13:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16)
                                    || (r.HierarchyKey.Equals(15)
                                     && r.Market.Equals(changedSummaryItem.Market))
                                    || (r.HierarchyKey.Equals(14)
                                     && r.Market.Equals(changedSummaryItem.Market)
                                     && r.Grade.Equals(changedSummaryItem.Grade))
                                     );
                        break;
                    // Cls
                    case 12:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16));
                        break;
                    // Cls/Mkt
                    case 11:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16)
                                || (r.HierarchyKey.Equals(12)
                                && r.Class.Equals(changedSummaryItem.Class))
                                     );
                        break;
                    // Cls/Mkt/Grd
                    case 10:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16)
                                || (r.HierarchyKey.Equals(12)
                                && r.Class.Equals(changedSummaryItem.Class))
                                || (r.HierarchyKey.Equals(11)
                                && r.Class.Equals(changedSummaryItem.Class)
                                && r.Market.Equals(changedSummaryItem.Market))
                                     );
                        break;
                    // Cls/Mkt/Grd/Str
                    case 9:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16)
                                || (r.HierarchyKey.Equals(12)
                                && r.Class.Equals(changedSummaryItem.Class))
                                || (r.HierarchyKey.Equals(11)
                                && r.Class.Equals(changedSummaryItem.Class)
                                && r.Market.Equals(changedSummaryItem.Market))
                                || (r.HierarchyKey.Equals(10)
                                && r.Class.Equals(changedSummaryItem.Class)
                                && r.Market.Equals(changedSummaryItem.Market)
                                && r.Grade.Equals(changedSummaryItem.Grade))
                                     );
                        break;
                }

                // Update all records at the higher level that match the key details from this level
                foreach (SummaryItem higherLevelSummaryItem in queryLeversWithLowerLevelOverrides)
                {
                    // check the type of change and apply an override where the lower level is not already an actual
                    if ("CA".Contains(changedSummaryItem.PatternStatus))
                    {
                        higherLevelSummaryItem.PatternOverrideLevel = changedSummaryItem.HierarchyKey;
                    }
                }
            }
        }

        /// <summary>
        /// Applies changes from the current level to the 'has override at lower level' values at higher levels
        /// </summary>
        /// <param name="FromLevel"></param>
        /// <param name="changedSummaryItem"></param>
        private void ApplySmoothFactorToHigherLevels(int FromLevel, SummaryItem changedSummaryItem)
        {
            if (FromLevel < 16)
            {
                // We need to Propagate changes to each of the higher levels

                // get higher level lever records that will need to reflect this change in their 'lower level overrides'
                var queryLeversWithLowerLevelOverrides = from row in WorkingSummaryItems
                                                         where row.StoreType.Equals(changedSummaryItem.StoreType)
                                                         where row.WorkBench.Equals(changedSummaryItem.WorkBench)
                                                         where row.SmoothFactorOverrideLevel <= FromLevel // Must previously not have had Overrides at lower levels
                                                         select row;

                // establish key fields for higher levels
                switch (FromLevel)
                {
                    // Dept/Mkt
                    case 15:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16));
                        break;
                    // Dept/Mkt/Grd
                    case 14:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16)
                                     || (r.HierarchyKey.Equals(15)
                                     && r.Market.Equals(changedSummaryItem.Market))
                                     );
                        break;
                    // Dept/Mkt/Grd/Str
                    case 13:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16)
                                    || (r.HierarchyKey.Equals(15)
                                     && r.Market.Equals(changedSummaryItem.Market))
                                    || (r.HierarchyKey.Equals(14)
                                     && r.Market.Equals(changedSummaryItem.Market)
                                     && r.Grade.Equals(changedSummaryItem.Grade))
                                     );
                        break;
                    // Cls
                    case 12:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16));
                        break;
                    // Cls/Mkt
                    case 11:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16)
                                || (r.HierarchyKey.Equals(12)
                                && r.Class.Equals(changedSummaryItem.Class))
                                     );
                        break;
                    // Cls/Mkt/Grd
                    case 10:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16)
                                || (r.HierarchyKey.Equals(12)
                                && r.Class.Equals(changedSummaryItem.Class))
                                || (r.HierarchyKey.Equals(11)
                                && r.Class.Equals(changedSummaryItem.Class)
                                && r.Market.Equals(changedSummaryItem.Market))
                                     );
                        break;
                    // Cls/Mkt/Grd/Str
                    case 9:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16)
                                || (r.HierarchyKey.Equals(12)
                                && r.Class.Equals(changedSummaryItem.Class))
                                || (r.HierarchyKey.Equals(11)
                                && r.Class.Equals(changedSummaryItem.Class)
                                && r.Market.Equals(changedSummaryItem.Market))
                                || (r.HierarchyKey.Equals(10)
                                && r.Class.Equals(changedSummaryItem.Class)
                                && r.Market.Equals(changedSummaryItem.Market)
                                && r.Grade.Equals(changedSummaryItem.Grade))
                                     );
                        break;
                }

                // Update all records at the higher level that match the key details from this level
                foreach (SummaryItem higherLevelSummaryItem in queryLeversWithLowerLevelOverrides)
                {
                    // check the type of change and apply an override where the lower level is not already an actual
                    if ("CA".Contains(changedSummaryItem.SmoothFactorStatus))
                    {
                        higherLevelSummaryItem.SmoothFactorOverrideLevel = changedSummaryItem.HierarchyKey;
                    }
                }
            }
        }

        /// <summary>
        /// Applies changes from the current level to the 'has override at lower level' values at higher levels
        /// </summary>
        /// <param name="FromLevel"></param>
        /// <param name="changedSummaryItem"></param>
        private void ApplyUpliftFactorToHigherLevels(int FromLevel, SummaryItem changedSummaryItem)
        {
            if (FromLevel < 16)
            {
                // We need to Propagate changes to each of the higher levels

                // get higher level lever records that will need to reflect this change in their 'lower level overrides'
                var queryLeversWithLowerLevelOverrides = from row in WorkingSummaryItems
                                                         where row.StoreType.Equals(changedSummaryItem.StoreType)
                                                         where row.WorkBench.Equals(changedSummaryItem.WorkBench)
                                                         where row.UpliftOverrideLevel <= FromLevel // Must previously not have had Overrides at lower levels
                                                         select row;

                // establish key fields for higher levels
                switch (FromLevel)
                {
                    // Dept/Mkt
                    case 15:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16));
                        break;
                    // Dept/Mkt/Grd
                    case 14:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16)
                                     || (r.HierarchyKey.Equals(15)
                                     && r.Market.Equals(changedSummaryItem.Market))
                                     );
                        break;
                    // Dept/Mkt/Grd/Str
                    case 13:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16)
                                    || (r.HierarchyKey.Equals(15)
                                     && r.Market.Equals(changedSummaryItem.Market))
                                    || (r.HierarchyKey.Equals(14)
                                     && r.Market.Equals(changedSummaryItem.Market)
                                     && r.Grade.Equals(changedSummaryItem.Grade))
                                     );
                        break;
                    // Cls
                    case 12:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16));
                        break;
                    // Cls/Mkt
                    case 11:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16)
                                || (r.HierarchyKey.Equals(12)
                                && r.Class.Equals(changedSummaryItem.Class))
                                     );
                        break;
                    // Cls/Mkt/Grd
                    case 10:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16)
                                || (r.HierarchyKey.Equals(12)
                                && r.Class.Equals(changedSummaryItem.Class))
                                || (r.HierarchyKey.Equals(11)
                                && r.Class.Equals(changedSummaryItem.Class)
                                && r.Market.Equals(changedSummaryItem.Market))
                                     );
                        break;
                    // Cls/Mkt/Grd/Str
                    case 9:
                        queryLeversWithLowerLevelOverrides = queryLeversWithLowerLevelOverrides
                            .Where(r => r.HierarchyKey.Equals(16)
                                || (r.HierarchyKey.Equals(12)
                                && r.Class.Equals(changedSummaryItem.Class))
                                || (r.HierarchyKey.Equals(11)
                                && r.Class.Equals(changedSummaryItem.Class)
                                && r.Market.Equals(changedSummaryItem.Market))
                                || (r.HierarchyKey.Equals(10)
                                && r.Class.Equals(changedSummaryItem.Class)
                                && r.Market.Equals(changedSummaryItem.Market)
                                && r.Grade.Equals(changedSummaryItem.Grade))
                                     );
                        break;
                }

                // Update all records at the higher level that match the key details from this level
                foreach (SummaryItem higherLevelSummaryItem in queryLeversWithLowerLevelOverrides)
                {
                    // check the type of change and apply an override where the lower level is not already an actual
                    if ("CA".Contains(changedSummaryItem.UpliftStatus))
                    {
                        higherLevelSummaryItem.UpliftOverrideLevel = changedSummaryItem.HierarchyKey;
                    }
                }
            }
        }

        /// <summary>
        /// Builds SQL statement to retrieve summary workbench data
        /// </summary>
        /// <returns></returns>
        private string CreateSQL()
        {
            var sb = new StringBuilder();
            var sql = string.Empty;

            sb.Append(Properties.Resources.SQLSummaryWorkbench);

            switch (this.StoreType)
            {
				case Constants.StoreTypes.BricksAndMortar:
                    AddCriteria(sb, "SWSTRTYP", "B");
                    break;
				case Constants.StoreTypes.Online:
                    AddCriteria(sb, "SWSTRTYP", "C");
                    break;
            }

			if (this.Workbench != Constants.Workbenches.Both)
				AddCriteria(sb, "SWWBTYP", Constants.GetWorkbenchName(this.Workbench).Left(1));

            sql = sb.ToString().Replace("<prefix>", "")
                .Replace("<member>", this.SessionMemberName)
                .Replace("<departmentId>", this.DepartmentId.ToString());

            return sql;
        }

        /// <summary>
        /// Creates alias on AS400 summary workbench file so we can access it directly
        /// </summary>
        /// <returns></returns>
        private string AliasCreateSQL()
        {
            var sb = new StringBuilder();
            var sql = string.Empty;

            sb.Append(Properties.Resources.SQLSummaryWorkbenchAliasCreate);

            sql = sb.ToString()
                .Replace("<alias>", this.SessionMemberName)
                .Replace("<member>", this.SessionMemberName);

            return sql;
        }

        /// <summary>
        /// Converts datarows into a list of SummaryItem business objects
        /// </summary>
        /// <param name="Level"></param>
        /// <returns></returns>
        private List<SummaryItem> GetWorkingData(int Level)
        {
			var query = from row in this.SummaryData
						orderby
							row.Field<decimal?>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.HierarchyKey)) ?? 0,
							row.Field<decimal?>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Department)) ?? 0,
							row.Field<decimal?>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Class)) ?? 0,
							row.Field<string>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.MarketSequence)),
							row.Field<string>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Grade)),
							row.Field<decimal?>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Store)) ?? 0,
							row.Field<string>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.StoreType)),
							row.Field<string>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.WorkBench))

						select new SummaryItem
						{
							HierarchyKey = (row.Field<decimal?>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.HierarchyKey)) ?? 0),
							Department = (row.Field<decimal?>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Department)) ?? 0),
							Class = (row.Field<decimal?>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Class)) ?? 0),
							Market = row.Field<string>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Market)),
							MarketColour = (row.Field<decimal?>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.MarketColour)) ?? 0),
							MarketSequence = (row.Field<string>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.MarketSequence))),
							Grade = row.Field<string>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Grade)),
							GradeDescription = row.Field<string>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.GradeDescription)),
							Store = (row.Field<decimal?>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Store)) ?? 0),
							StoreName = row.Field<string>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.StoreName)),
							KeyValue = row.Field<string>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.KeyValue)),
							StoreType = row.Field<string>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.StoreType)),
							WorkBench = row.Field<string>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.WorkBench)),
							StockRequirement = (row.Field<decimal?>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.StockRequirement)) ?? 0),
							IdealReplenishmentQuantity = (row.Field<decimal?>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.IdealReplenishmentQuantity)) ?? 0),
							ProposedAllocationQuantity = (row.Field<decimal?>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.ProposedAllocationQuantity)) ?? 0),
							Changed = row.Field<string>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Changed)),

							UpliftFactor = (row.Field<decimal?>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.UpliftFactor)) ?? 0),
							UpliftInheritedLevel = (row.Field<decimal?>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.UpliftInheritedLevel)) ?? 0),
							UpliftActualFlag = row.Field<string>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.UpliftActualFlag)) == "1" ? true : false,
							UpliftOverrideLevel = (row.Field<decimal?>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.UpliftOverrideLevel)) ?? 0),
							UpliftStatus = (row.Field<string>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.UpliftStatus)) == ""
								? " " : row.Field<string>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.UpliftStatus))),

							CutOff = (row.Field<decimal?>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.CutOff)) ?? 0),
							CutOffInheritedLevel = (row.Field<decimal?>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.CutOffInheritedLevel)) ?? 0),
							CutOffActualFlag = row.Field<string>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.CutOffActualFlag)) == "1" ? true : false,
							CutOffOverrideLevel = (row.Field<decimal?>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.CutOffOverrideLevel)) ?? 0),
							CutOffStatus = (row.Field<string>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.CutOffStatus)) == ""
								? " " : row.Field<string>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.CutOffStatus))),

							Allocation = row.Field<string>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Allocation)),
							AllocationInheritedLevel = (row.Field<decimal?>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.AllocationInheritedLevel)) ?? 0),
							AllocationActualFlag = row.Field<string>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.AllocationActualFlag)) == "1" ? true : false,
							AllocationOverrideLevel = (row.Field<decimal?>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.AllocationOverrideLevel)) ?? 0),
							AllocationStatus = (row.Field<string>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.AllocationStatus)) == ""
								? " " : row.Field<string>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.AllocationStatus))),

							SmoothFactor = (row.Field<decimal?>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.SmoothFactor)) ?? 0),
							SmoothFactorInheritedLevel = (row.Field<decimal?>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.SmoothFactorInheritedLevel)) ?? 0),
							SmoothFactorActualFlag = row.Field<string>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.SmoothFactorActualFlag)) == "1" ? true : false,
							SmoothFactorOverrideLevel = (row.Field<decimal?>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.SmoothFactorOverrideLevel)) ?? 0),
							SmoothFactorStatus = (row.Field<string>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.SmoothFactorStatus)) == ""
								? " " : row.Field<string>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.SmoothFactorStatus))),

							Pattern = (row.Field<decimal?>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Pattern)) ?? 0),
							PatternInheritedLevel = (row.Field<decimal?>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.PatternInheritedLevel)) ?? 0),
							PatternActualFlag = row.Field<string>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.PatternActualFlag)) == "1" ? true : false,
							PatternOverrideLevel = (row.Field<decimal?>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.PatternOverrideLevel)) ?? 0),
							PatternStatus = (row.Field<string>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.PatternStatus)) == ""
								? " " : row.Field<string>(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.PatternStatus)))
						};

            if (Level > 0)
                query = query.Where(r => r.HierarchyKey == Level);

            return query.ToList();
        }

        /// <summary>
        /// Call As400 stored procedure to provide data for requested criteria and determines working session member name
        /// </summary>
        /// <returns></returns>
        private bool InitialiseWorkbench()
        {
            var result = false;
			
			OriginalItems = null;

            if (Factory.OpenConnection())
                try
                {
                    OnProgress("Loading...");

                    var cmd = Factory.CreateCommand("DS886ZS1", CommandType.StoredProcedure,
                        Factory.CreateParameter("puser", Session.User.NetworkId.ToUpper(), System.Data.OleDb.OleDbType.Char, 10, ParameterDirection.Input),
                        Factory.CreateParameter("pdept", this.DepartmentId, System.Data.OleDb.OleDbType.Decimal, 3, ParameterDirection.Input),
                        Factory.CreateParameter("pdlvl", "0", System.Data.OleDb.OleDbType.Char, 1, ParameterDirection.Input),
						Factory.CreateParameter("pperd", Workbench == Constants.Workbenches.Daily ? "D" : Workbench == Constants.Workbenches.Weekly ? "W" : "B", System.Data.OleDb.OleDbType.Char, 1, ParameterDirection.Input),
						Factory.CreateParameter("ptype", StoreType == Constants.StoreTypes.BricksAndMortar ? "B" : StoreType == Constants.StoreTypes.Online ? "C" : "A", System.Data.OleDb.OleDbType.Char, 1, ParameterDirection.Input),
                        Factory.CreateParameter("pfmbr", string.Empty, System.Data.OleDb.OleDbType.Char, 10, ParameterDirection.InputOutput),
                        Factory.CreateParameter("perr", string.Empty, System.Data.OleDb.OleDbType.Char, 128, ParameterDirection.InputOutput));

                    cmd.ExecuteNonQuery();

                    MustReleaseLocks = true;

                    if (cmd.Parameters["pfmbr"].Value != System.DBNull.Value && cmd.Parameters["pfmbr"].Value.ToString() != string.Empty)
                    {
                        SessionMemberName = cmd.Parameters["pfmbr"].Value.ToString();
                    }
                    if (cmd.Parameters["perr"].Value == System.DBNull.Value || cmd.Parameters["perr"].Value.ToString() == string.Empty)
                    {
                        result = true;
                    }
                    else
                    {
                        this.ErrorMessage = cmd.Parameters["perr"].Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    ExceptionHandler.RaiseException(ex, "InitialiseWorkbench");
                }
                finally
                {
                    Factory.CloseConnection();
                }

            return result;
        }

        /// <summary>
        /// Calls AS400 stored procedure to release department locks by weekly or daily periods
        /// </summary>
        /// <param name="workbench"></param>
        /// <returns></returns>
        private bool ReleaseLock(Constants.Workbenches workbench)
        {
            var result = false;

            if (Factory.OpenConnection())
                try
                {
                    OnProgress("Releasing " + workbench + " filegroup");

					var cmd = Factory.CreateCommand("DS886JS1", CommandType.StoredProcedure,
						Factory.CreateParameter("puser", Session.User.NetworkId.ToUpper(), OleDbType.Char, 10, ParameterDirection.Input),
						Factory.CreateParameter("pdept", this.DepartmentId, OleDbType.Decimal, 3, ParameterDirection.Input),
						Factory.CreateParameter("pperd", "X", OleDbType.Char, 1, ParameterDirection.Input),
						Factory.CreateParameter("pfgrp", 0, OleDbType.Decimal, 3, ParameterDirection.Input),
						Factory.CreateParameter("pdlvl", "0", OleDbType.Char, 1, ParameterDirection.Input),
						Factory.CreateParameter("perr", string.Empty, OleDbType.Char, 128, ParameterDirection.InputOutput));

					if (workbench == Constants.Workbenches.Daily && DailyFilegroup != 0)
					{
						cmd.Parameters["pperd"].Value = "D";
						cmd.Parameters["pfgrp"].Value = DailyFilegroup;
						cmd.ExecuteNonQuery();

						if (cmd.Parameters["perr"].Value == System.DBNull.Value || cmd.Parameters["perr"].Value.ToString() == string.Empty)
						{
							result = true;
							DailyFilegroup = 0;
						}
						else
							this.ErrorMessage = cmd.Parameters["perr"].Value.ToString();

					}
					else if (workbench == Constants.Workbenches.Weekly && WeeklyFilegroup != 0)
					{
						OnProgress("Unlocking weekly items");
						cmd.Parameters["pperd"].Value = "W";
						cmd.Parameters["pfgrp"].Value = WeeklyFilegroup;
						cmd.ExecuteNonQuery();

						if (cmd.Parameters["perr"].Value == System.DBNull.Value || cmd.Parameters["perr"].Value.ToString() == string.Empty)
						{
							result = true;
							WeeklyFilegroup = 0;
						}
						else
							this.ErrorMessage = cmd.Parameters["perr"].Value.ToString();
					}

                }
                catch (Exception ex)
                {
                    ExceptionHandler.RaiseException(ex, "ReleaseLock");
                }
                finally
                {
                    Factory.CloseConnection();
                }

            return result;
        }

        //-------------------------------------------------------------------------------
        #endregion


    }
}
