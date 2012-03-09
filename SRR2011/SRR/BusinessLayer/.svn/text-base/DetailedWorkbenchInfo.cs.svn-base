/*
 * Author:  Marc Levine
 * Project: SRR
 * Date:    From March 2011
 * 
 * Description
 * This is a helper class used as a repository.  It holds all the data from each form as the user
 * goes through the process of selecting criteria.  It is also responsible for constructing the SQL
 * used at each stage and validating data entered into each form.
 *  
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Disney.iDash.LocalData;
using Disney.iDash.Shared;

namespace Disney.iDash.SRR.BusinessLayer
{
    public class DetailedWorkbenchInfo : BusinessBase
    {
        #region Enumerations and static data
        //-------------------------------------------------------------------------------
        public enum Stages
        {
            None,
            FocusGroups,
            SelectStore,
            SelectClass,
            Parameters,
            DetailedWorkbench
        }

        public enum FocusGroups
        {
            Unselected,
            NewLinesReceived,
            FastestCoverStores,
            SlowestCoverStores,
            FastestCoverLines,
            SlowestCoverLines,
            TopRetailPerformers
        }

        private static Dictionary<FocusGroups, string> _focusGroups = new Dictionary<FocusGroups, string>{
            {FocusGroups.NewLinesReceived, "New lines received"},
            {FocusGroups.FastestCoverStores, "Fastest cover stores"},
            {FocusGroups.SlowestCoverStores, "Slowest cover stores"},
            {FocusGroups.FastestCoverLines, "Fastest cover lines"},
            {FocusGroups.SlowestCoverLines, "Slowest cover lines"},
            {FocusGroups.TopRetailPerformers, "Top retail performers"}};

        // the value assigned to ItemStoreLevel must not be changed as this is the actual level used on the AS400.
        public enum Parameters
        {
			ItemStoreLevel = 1,
            ItemGradeLevel,
            ItemMarketLevel,
			ItemLevel,
			StyleStoreLevel,
            StyleGradeLevel,
            StyleMarketLevel,
            StyleLevel,
        }

        private static List<ParameterInfo> _parameters = new List<ParameterInfo>{
            new ParameterInfo(Parameters.StyleLevel, "Style level", "SL", "DSSRWBSL6", true),
            new ParameterInfo(Parameters.StyleMarketLevel, "Style market level", "SM", "DSSRWBSMB"),
            new ParameterInfo(Parameters.StyleGradeLevel, "Style grade level", "SG", "DSSRWBSGB"),
            new ParameterInfo(Parameters.StyleStoreLevel, "Style store level", "SS", "DSSRWBSSB", true),
            new ParameterInfo(Parameters.ItemLevel, "Item level", "IT", "DSSRWBITC", true),
            new ParameterInfo(Parameters.ItemMarketLevel, "Item market level", "IM", "DSSRWBIMD"),
            new ParameterInfo(Parameters.ItemGradeLevel, "Item grade level", "IG", "DSSRWBIGD"),
            new ParameterInfo(Parameters.ItemStoreLevel, "Item store level", "IS", "DSSRWBIS4", true)};

        /// <summary>
        /// Used to map the numeric hierarchy levels in the detailed workbench grid to text representations.
        /// </summary>
        private static Dictionary<int, string> _hierarchyLevels = new Dictionary<int, string>{
                {1, "IS"},  {2, "IG"}, {3, "IM"}, {4, "I"}, {5, "SS"}, {6, "SG"}, {7, "SM"}, {8, "S"},
                {9, "CS"}, {10, "CG"}, {11, "CM"}, {12, "C"}, {13, "DS"}, {14, "DG"}, {15, "DM"}, {16, "D"}};

        private List<string> _validationErrors = new List<string>();
        private DataTable _data = null;

		private List<InherittedInfo> _inherittedValues = new List<InherittedInfo>();
        private List<LeverLock> _leverLocks = new List<LeverLock>();
		private GiveItBackCollection _giveItBackItems = new GiveItBackCollection();
		private SysInfo _sysInfo = new SysInfo();
        public int FetchLimit {get; set;}

		//-------------------------------------------------------------------------------
        #endregion

        #region Public properties and methods
        //-------------------------------------------------------------------------------

		public const string colItem = "ITEM";
		public const string colClass = "CLASS";
		public const string colVendor = "VENDOR";
		public const string colStyle = "STYLE";
		public const string colColour = "COLOUR";
		public const string colSize = "SIZE";
		public const string colDescription = "DESCRIPTION";
		public const string colMarket = "MARKET";
		public const string colGrade = "GRADE";
		public const string colGradeDescription = "GRADEDESCRIPTION";
		public const string colStore = "STORE";
		public const string colStoreName = "STORENAME";
		public const string colStoreType = "STORETYPE";
		public const string colWorkBench = "WORKBENCH";
		public const string colSalesLW = "SALESLW";
		public const string colSalesTW = "SALESTW";
		public const string colSmoothedRateOfSale = "SMOOTHEDRATEOFSALE";
		public const string colNewSmoothedRateOfSale = "NEWSMOOTHEDRATEOFSALE";
		public const string colStoreSOH = "STORESOH";
		public const string colSmoothedStoreCover = "SMOOTHEDSTORECOVER";
		public const string colTotalSmoothedStoreCover = "TOTALSMOOTHEDSTORECOVER";
		public const string colOriginalStockRequired = "ORIGINALSTOCKREQUIRED";
		public const string colTotalStockRequired = "TOTALSTOCKREQUIRED";
		public const string colRingFenced = "RINGFENCED";
		public const string colAllocated = "ALLOCATED";
		public const string colGiveItBack = "GIVEITBACK";
		public const string colShipped = "SHIPPED";
		public const string colMinDispQty = "MINDISPQTY";

		public const string colIdealAllocQty = "IDEALALLOCQTY";
		public const string colProposedAllocQty = "PROPOSEDALLOCQTY";
		public const string colAllocationLevel = "ALLOCATIONLEVEL";
        public const string colHideQuantities = "HIDEQUANTITIES";

		public const string colDCStock = "DCSTOCK";
		public const string colEDCAPPStock = "EDCAPPSTOCK";
		public const string colSmoothedEDCCover = "SMOOTHEDEDCCOVER";
		public const string colNextOrderDate = "NEXTORDERDATE";
		public const string colNextOrderQty = "NEXTORDERQTY";

		public const string colCurPattern = "CURPATTERN";
		public const string colNewPattern = "NEWPATTERN";
		public const string colPatternHierarchyLevel = "PATTERNHIERARCHYLEVEL";
		public const string colPatternThisLevel = "PATTERNTHISLEVEL";
		public const string colPatternThisLevelOriginal = "PATTERNTHISLEVELORIGINAL";
		public const string colPatternStatus = "PATTERNSTATUS";
		
		public const string colCurUpliftFactor = "CURUPLIFTFACTOR";
		public const string colNewUpliftFactor = "NEWUPLIFTFACTOR";
		public const string colUpliftHierarchyLevel = "UPLIFTHIERARCHYLEVEL";
		public const string colUpliftThisLevel = "UPLIFTTHISLEVEL";
		public const string colUpliftThisLevelOriginal = "UPLIFTTHISLEVELORIGINAL";
		public const string colUpliftStatus = "UPLIFTSTATUS";
		
		public const string colCurCutOff = "CURCUTOFF";
		public const string colNewCutOff = "NEWCUTOFF";
		public const string colCutOffHierarchyLevel = "CUTOFFHIERARCHYLEVEL";
		public const string colCutOffThisLevel = "CUTOFFTHISLEVEL";
		public const string colCutOffThisLevelOriginal = "CUTOFFTHISLEVELORIGINAL";
		public const string colCutOffStatus = "CUTOFFSTATUS";
		
		public const string colCurAllocFlag = "CURALLOCFLAG";
		public const string colNewAllocFlag = "NEWALLOCFLAG";
		public const string colAllocFlagHierarchyLevel = "ALLOCFLAGHIERARCHYLEVEL";
		public const string colAllocFlagThisLevel = "ALLOCFLAGTHISLEVEL";
		public const string colAllocFlagThisLevelOriginal = "ALLOCFLAGTHISLEVELORIGINAL";
		public const string colAllocFlagStatus = "ALLOCFLAGSTATUS";
		
		public const string colCurSmoothingFactor = "CURSMOOTHINGFACTOR";
		public const string colNewSmoothingFactor = "NEWSMOOTHINGFACTOR";
		public const string colSmoothingFactorHierarchyLevel = "SMOOTHINGFACTORHIERARCHYLEVEL";
		public const string colSmoothingFactorThisLevel = "SMOOTHINGFACTORTHISLEVEL";
		public const string colSmoothingFactorThisLevelOriginal = "SMOOTHINGFACTORTHISLEVELORIGINAL";
		public const string colSmoothingFactorStatus = "SMOOTHINGFACTORSTATUS";
		
		public const string colDaysOutOfStock = "DAYSOUTOFSTOCK";
		public const string colUPC = "UPC";
		public const string colPack = "PACK";
		public const string colAPPItemNo = "APPITEMNO";
		public const string colError = "ERROR";

		// Used in StoreClassSelector Form
		public const string colCurrentROS = "CURRENTROS";
		public const string colStockOnHand = "STOCKONHAND";
		public const string colOutOfStock = "OUTOFSTOCK";

		public const string kStatusDeleted = "D";
		public const string kStatusChanged = "C";
		public const string kStatusAdded = "A";
		public const string kStatusUnchanged = "U";

        public Stages Stage { get; set; }
        public string ErrorMessage { get; private set; }
        public decimal DailyFilegroup { get; private set; }
        public decimal WeeklyFilegroup { get; private set; }
        public decimal DepartmentId { get; set; }
        public string DepartmentText { get; set; }
        public Constants.Workbenches Workbench { get; set; }
		public Constants.StoreTypes StoreType { get; set; }
        public bool ExcludeZeroStock { get; set; }
        public bool ShowFullPriceLines { get; set; }
        public bool ShowMarkedDownLines { get; set; }
        public bool ShowProposedMarkedDownLines { get; set; }
        public Parameters PreviousParameter { get; set; }
        public Parameters Parameter { get; set; }
        public StockItem Item { get; private set; }
		public SelectedItem SelectedItem { get; private set; }

        // Parameter selection
        public decimal StoreId { get; set; }
        public string StoreText { get; set; }
        public string StoreGradeId { get; set; }
        public string StoreGradeText { get; set; }
        public string StoreGroup { get; set; }
        public string Market { get; set; }
        public string FranchiseId { get; set; }
        public string FranchiseText { get; set; }
        public string CoordinateGroup { get; set; }
        public string EventCodeId { get; set; }
        public string EventCodeText { get; set; }
        public string UPC { get; set; }
        public List<LookupItem> Promotions { get; set; }
        public string StoreList { get; set; }

        // Focus groups
        public FocusGroups FocusGroup { get; set; }
        public bool AllMarkets { get; set; }
        public List<LookupItem> Markets { get; set; }
        public decimal NumberOf { get; set; }
		public bool FirstTime { get; set; }
        public bool _giveItBackChanged = false;


		public DetailedWorkbenchInfo()
		{
			_sysInfo.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
			{
				this.ExceptionHandler.RaiseException(ex, extraInfo, terminateApplication);
			});

			Reset();
		}

		public void Reset()
		{
			foreach (var pi in this.GetType().GetProperties().Where(p=>p.CanWrite))
			{
				switch (pi.PropertyType.Name.ToLower())
				{
					case "string":
						pi.SetValue(this, string.Empty, null);
						break;

					case "double":
						pi.SetValue(this, 0d, null);
						break;

					case "decimal":
						pi.SetValue(this, 0m, null);
						break;

					case "int16":
					case "int32":
					case "int64":
						pi.SetValue(this, 0, null);
						break;

					case "bool":
						pi.SetValue(this, false, null);
						break;

				}
			}

			this.Workbench = Constants.Workbenches.Daily;
			this.StoreType = Constants.StoreTypes.BricksAndMortar;
			this.FocusGroup = FocusGroups.Unselected;
			this.Parameter = Parameters.ItemLevel;
			this.ShowFullPriceLines = true;
			this.ShowMarkedDownLines = true;
			this.ShowProposedMarkedDownLines = true;
			this.ExcludeZeroStock = true;
			this.Stage = DetailedWorkbenchInfo.Stages.None;
			this.Promotions = new List<LookupItem>();
			this.Markets = new List<LookupItem>();

			this.Item = new StockItem();
			this.SelectedItem = new SelectedItem();

			this.NumberOf = 4;
		}

		public bool Setup()
		{
			var result = false;

			try
			{                
				if (GiveItBackCollection.SetGiveItBackMode(_sysInfo))
					result = true;
				else
					ExceptionHandler.RaiseAlert("GiveItBackCollection.SetGiveItBackMode() Failed to set GiveItBackMode", "Prerequiste Model Run Check failed", Shared.ExceptionHandler.AlertType.Warning);
			}
			catch (Exception ex)
			{
				this.ExceptionHandler.RaiseException(ex, "Setup");
			}

			return result;
		}

        public static ParameterInfo GetParameterInfo(Parameters parameter)
        {
            var qry = _parameters.First((p) => (p.Parameter == parameter));
            return qry;
        }

        public static string GetParameterName(Parameters parameter)
        {
            return GetParameterInfo(parameter).Description;
        }

        public static List<ParameterInfo> GetParameters
        {
            get { return _parameters; }
        }

        public static string GetFocusGroupName(FocusGroups focusGroup)
        {
            return _focusGroups[focusGroup];
        }

        public static Dictionary<FocusGroups, string> GetFocusGroups
        {
            get { return _focusGroups; }
        }

        public static Dictionary<int, string> GetHierarchyLevels
        {
            get { return _hierarchyLevels; }
        }

		public string GetMember(Constants.Workbenches workbench)
        {
			return GetMember(Constants.GetWorkbenchName(workbench));
        }

		public GiveItBackCollection GetGiveItBackItems
		{
			get { return _giveItBackItems;}
		}

		public string GetMember(string workbench)
		{
			var member = string.Empty;
			if (workbench == Constants.kDaily)
				member = "F" + DailyFilegroup.ToString("000");
			else if (workbench == Constants.kWeekly)
				member = "DPT" + DepartmentId.ToString("000") + WeeklyFilegroup.ToString("000");
			return member;
		}

        /// <summary>
        /// Return a datatable of patters for us when changing values in the detailed workbench.
        /// </summary>
         public List<LookupItem> GetPatterns
        {
            get
            {
                var lookup = new LookupSource();
                lookup.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
                {
                    ExceptionHandler.RaiseException(ex, extraInfo, terminateApplication);
                });
                return lookup.GetItems(LookupSource.LookupTypes.Patterns);
            }
        }

		private bool _getNextFileGroupRunning = false;
        /// <summary>
        ///  Attempt to set the appropriate (daily or weekly) filegroup property, if not already set.
        /// </summary>
        /// <param name="workbench"></param>
        /// <returns></returns>
		public bool GetNextFileGroup(string workbench)
        {
            var result = false;

			if (!_getNextFileGroupRunning)
			{
				_getNextFileGroupRunning = true;
				if (workbench != Constants.kWeekly && workbench != Constants.kDaily)
					ExceptionHandler.RaiseAlert("Invalid workbench: " + workbench, "Invalid Workbench", Shared.ExceptionHandler.AlertType.Error);

				else if ((workbench == Constants.kWeekly && WeeklyFilegroup != 0) || (workbench == Constants.kDaily && DailyFilegroup != 0))
					result = true;

				else if (Factory.OpenConnection())
					try
					{
						OnProgress("Obtaining next " + workbench + " filegroup");

						var cmd = Factory.CreateCommand("DS886ES1", CommandType.StoredProcedure,
							Factory.CreateParameter("puser", Session.User.NetworkId.ToUpper(), OleDbType.Char, 10, ParameterDirection.Input),
							Factory.CreateParameter("pdept", this.DepartmentId, OleDbType.Decimal, 3, ParameterDirection.Input),
							Factory.CreateParameter("pdlvl", "1", OleDbType.Char, 1, ParameterDirection.Input),
							Factory.CreateParameter("pperd", workbench.ToUpper().Left(1), OleDbType.Char, 1, ParameterDirection.Input),
							Factory.CreateParameter("pfgrp", 0, OleDbType.Decimal, 3, ParameterDirection.InputOutput),
							Factory.CreateParameter("perr", string.Empty, OleDbType.Char, 128, ParameterDirection.InputOutput));

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
						ExceptionHandler.RaiseException(ex, "GetNextFileGroup");
					}
					finally
					{
						Factory.CloseConnection();
					}
				_getNextFileGroupRunning = false;
			}
            return result;
        }

		private bool _lockItemRunning = false;
        /// <summary>
        /// Attempt to lock the current item prior to editing it.
        /// </summary>
        /// <param name="workbench"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool LockItem(string workbench, StockItem item)
        {
            var locked = false;

			if (!_lockItemRunning)
			{
				_lockItemRunning = true;

                var leverLock = new LeverLock
                {
                    Class = item.Class.Value,
//                    Filegroup = (workbench == Constants.kDaily ? DailyFilegroup : WeeklyFilegroup),
                    Style = item.Style.Value,
                    Vendor = item.Vendor.Value,
                    Workbench = workbench
                };

				if (workbench != Constants.kWeekly && workbench != Constants.kDaily)
					ExceptionHandler.RaiseAlert("Invalid workbench: " + workbench, "Invalid Workbench", Shared.ExceptionHandler.AlertType.Error);

                else if (_leverLocks.Contains(leverLock))
                    locked = true;

				else if (Factory.OpenConnection())
					try
					{
						var cmd = Factory.CreateCommand("DS886GS1", CommandType.StoredProcedure,
							Factory.CreateParameter("puser", Session.User.NetworkId.ToUpper(), OleDbType.Char, 10, ParameterDirection.Input),
							Factory.CreateParameter("pdept", this.DepartmentId, OleDbType.Decimal, 3, ParameterDirection.Input),
							Factory.CreateParameter("pcls", item.Class, OleDbType.Decimal, 4, ParameterDirection.Input),
							Factory.CreateParameter("pven", item.Vendor, OleDbType.Decimal, 5, ParameterDirection.Input),
							Factory.CreateParameter("psty", item.Style, OleDbType.Decimal, 4, ParameterDirection.Input),
							Factory.CreateParameter("pdlvl", "1", OleDbType.Char, 1, ParameterDirection.Input),
							Factory.CreateParameter("pperd", workbench.Left(1), OleDbType.Char, 1, ParameterDirection.Input),
							Factory.CreateParameter("pfgrp", (workbench == Constants.kDaily ? DailyFilegroup : WeeklyFilegroup), OleDbType.Decimal, 3, ParameterDirection.Input),
							Factory.CreateParameter("perr", string.Empty, OleDbType.Char, 128, ParameterDirection.InputOutput));

						cmd.ExecuteNonQuery();

						if (cmd.Parameters["perr"].Value == System.DBNull.Value || cmd.Parameters["perr"].Value.ToString() == string.Empty)
                        {
                            _leverLocks.Add(leverLock);
							locked = true;
                        }
						else
							this.ErrorMessage = Factory.ReplaceDate(cmd.Parameters["perr"].Value.ToString());
					}
					catch (Exception ex)
					{
						ExceptionHandler.RaiseException(ex, "LockItem");
					}
					finally
					{
						Factory.CloseConnection();
					}
				_lockItemRunning = false;
			}
            return locked;
        }

        /// <summary>
        /// Unlock all items locked in this worbench.  Called from DiscardChanges.  Not intended to be called directly
        /// </summary>
        /// <returns></returns>
        public bool ReleaseLocks()
        {
            var unlocked = false;

            if (Factory.OpenConnection())
                try
                {
                    var cmd = Factory.CreateCommand("DS886JS1", CommandType.StoredProcedure,
                        Factory.CreateParameter("puser", Session.User.NetworkId.ToUpper(), OleDbType.Char, 10, ParameterDirection.Input),
                        Factory.CreateParameter("pdept", this.DepartmentId, OleDbType.Decimal, 3, ParameterDirection.Input),
                        Factory.CreateParameter("pperd", "X", OleDbType.Char, 1, ParameterDirection.Input),
                        Factory.CreateParameter("pfgrp", 0, OleDbType.Decimal, 3, ParameterDirection.Input),
                        Factory.CreateParameter("pdlvl", "1", OleDbType.Char, 1, ParameterDirection.Input),
                        Factory.CreateParameter("perr", string.Empty, OleDbType.Char, 128, ParameterDirection.InputOutput));

                    if (DailyFilegroup != 0)
                    {
                        OnProgress("Unlocking daily items");
                        cmd.Parameters["pperd"].Value = "D";
                        cmd.Parameters["pfgrp"].Value = DailyFilegroup;
                        cmd.ExecuteNonQuery();
						DailyFilegroup = 0;
                    }

                    if (WeeklyFilegroup != 0)
                    {
                        OnProgress("Unlocking weekly items");
                        cmd.Parameters["pperd"].Value = "W";
                        cmd.Parameters["pfgrp"].Value = WeeklyFilegroup;
                        cmd.ExecuteNonQuery();
						WeeklyFilegroup = 0;
                    }

                    _leverLocks.Clear();
                    unlocked = true;

                }
                catch (Exception ex)
                {
                    ExceptionHandler.RaiseException(ex, "UnlockItem");
                    unlocked = false;
                }
                finally
                {
                    Factory.CloseConnection();
                }
            return unlocked;
        }

		/// <summary>
		/// Calls procedure to close files that have been left open on AS400 as a result of a call to GetInheritance or ModelRun.
		/// </summary>
		/// <returns></returns>
		public void CloseAS400Files()
		{
			if (Factory.OpenConnection())
				try
				{
					var cmd = Factory.CreateCommand("DS890CS1", CommandType.StoredProcedure);
					cmd.ExecuteNonQuery();
				}
				catch (Exception ex)
				{
					ExceptionHandler.RaiseException(ex, "CloseAS400Files");
				}
				finally
				{
					Factory.CloseConnection();
				}
		}

        /// <summary>
        /// Returns a table of data matching the criteria for the current state.
        /// </summary>
        /// <returns></returns>
        public DataTable GetData()
        {           
            var sql = this.CreateSQL();
			var timeStart = DateTime.Now;
            _data = null;

            if (Factory.OpenConnection())
                try
                {
					OnProgress("Loading...");
                    _data = Factory.CreateTable(sql, FetchLimit);

                    if (_data == null || _data.Rows.Count == 0)
                        OnProgress("No records available");
                    else
                    {
                        // Bug 294: Error happens when more than one copy of the Detailed Workbench is 
                        // open.
                        lock (this)
                        {
                            if (!_data.Columns.Contains("ERROR"))
                            {
                                var errCol = new DataColumn("ERROR", typeof(System.String));
                                errCol.MaxLength = 128;
                                errCol.AllowDBNull = true;
                                _data.Columns.Add(errCol);
                            }
                        }

                        OnProgress(string.Format("{0:#,##0} record(s) loaded in {1:0.0} seconds", _data.Rows.Count, DateTime.Now.Subtract(timeStart).TotalSeconds));
                    }
					IsDirty = false;
                }
                catch (Exception ex)
                {
                    ExceptionHandler.RaiseException(ex, "GetData");
                }
                finally
                {
                    Factory.CloseConnection();
                }

            return _data;
        }
       
        public int GetRawChangedCount
        {
            get
            {
                if (_data == null || _data.GetChanges() == null)
                    return 0;
                else
                    return _data.GetChanges().Rows.Count;
            }
        }

        public DataTable GetLastResult(bool showChanges = false)
        {
            DataTable lastResult = null;

            if (_data != null)
                if (showChanges && GetRawChangedCount>0)
                    lastResult = _data.GetChanges();
                else
                    lastResult = _data;

            return lastResult;
        }

        /// <summary>
        /// Validate data fields for the current stage.
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            _validationErrors.Clear();

            switch (this.Stage)
            {
                case Stages.None:
                    break;

                case Stages.Parameters:
                    if (this.UPC != null && this.UPC != string.Empty && this.Item.Class.HasValue)
                        _validationErrors.Add("You may select Item# or UPC");

                    if (!IsValidUPC())
                        _validationErrors.Add("Invalid UPC");

                    if (CountValues(this.StoreId, this.StoreGradeId, this.StoreGroup, this.Market) > 1)
                        _validationErrors.Add("You may select one of: Store, Store Grade, Store Group or Market");

                    if (!this.ShowFullPriceLines && !this.ShowMarkedDownLines && !this.ShowProposedMarkedDownLines)
                        _validationErrors.Add("You must select one of: Show full price, Marked down or Proposed marked down lines");
                    break;

                case Stages.FocusGroups:
                    if ((this.FocusGroup == FocusGroups.FastestCoverStores || this.FocusGroup == FocusGroups.SlowestCoverStores) && !this.AllMarkets && this.Markets.Count == 0)
                        _validationErrors.Add("You must select at least one market");

                    if (this.ShowFullPriceLines == false && this.ShowMarkedDownLines == false)
                        _validationErrors.Add("You must select either Show Full or Show Marked down Lines");
                    break;

                case Stages.DetailedWorkbench:
                    break;

            }
            return _validationErrors.Count == 0;
        }

        /// <summary>
        /// Return a string terminated by cr/lf of form error messages
        /// </summary>
        /// <returns></returns>
        public string GetValidationErrors()
        {
            return string.Join("\n", _validationErrors);
        }

        /// <summary>
        /// This is a wrapper function to test UPC exists.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsValidUPC(string value = null)
        {
            if (value == null)
                value = this.UPC;

            return string.IsNullOrEmpty(value) || Factory.LookupKey("IPITHDRC", "IUPD", value);
        }

        /// <summary>
        ///  Save any changes made to the model.
        /// </summary>
        /// <returns></returns>
        public bool ApplyChanges()
        {
            return RunModel(true);
        }

        /// <summary>
        /// Run the model and update the source table with the results.
        /// </summary>
        /// <param name="applyChanges"></param>
        /// <returns></returns>
        public bool RunModel(bool applyChanges = false)
        {
            var runCompleted = true;
			var dailyChanges = 0;
			var weeklyChanges = 0;

			if (this.WeeklyFilegroup != 0)
				weeklyChanges = RunModelCountChanges(Constants.Workbenches.Weekly);
			if (this.DailyFilegroup != 0)
				dailyChanges = RunModelCountChanges(Constants.Workbenches.Daily);

            // must check we still have a lock for the file group?
            if (dailyChanges > 0 || weeklyChanges > 0)
            {
                if (Factory.OpenConnection())
                {
                    var tran = Factory.BeginTransaction();
                    var error = string.Empty;
                    try
                    {
                        DataTable weeklyResults = null;
                        DataTable dailyResults = null;

                        // Clear any existing items for this run.                       
                        if (this.WeeklyFilegroup != 0)
                            error = RunModelClear(applyChanges, Constants.Workbenches.Weekly, tran);

                        if (error == string.Empty && this.DailyFilegroup != 0)
                            error = RunModelClear(applyChanges, Constants.Workbenches.Daily, tran);

                        if (error == string.Empty)
                            error = RunModelInsert(applyChanges, tran);

                        if (error == string.Empty && applyChanges && _giveItBackItems.Count() > 0)
                            error = RunModelGiveItBack(applyChanges, tran);

                        if (error == string.Empty)
                        {
                            // Now run the model
                            var cmd = Factory.CreateCommand("DS887FS1", CommandType.StoredProcedure,
                                    Factory.CreateParameter("p_fgrp", 0, OleDbType.Decimal, 3, ParameterDirection.Input),
                                    Factory.CreateParameter("p_dept", this.DepartmentId.ToString("000"), OleDbType.Decimal, 3, ParameterDirection.Input),
                                    Factory.CreateParameter("p_period", "X", OleDbType.Char, 1, ParameterDirection.Input),
                                    Factory.CreateParameter("p_user", Session.User.NetworkId.ToUpper(), OleDbType.Char, 10, ParameterDirection.Input),
                                    Factory.CreateParameter("p_mode", (applyChanges ? "A" : "M"), OleDbType.Char, 1, ParameterDirection.Input),
                                    Factory.CreateParameter("p_error", string.Empty, OleDbType.Char, 80, ParameterDirection.InputOutput));

                            cmd.Transaction = tran;

                            if (this.DailyFilegroup != 0 && dailyChanges > 0 && (this.Workbench == Constants.Workbenches.Daily || this.Workbench == Constants.Workbenches.Both))
                            {
                                OnProgress((applyChanges ? "Applying changes" : "Model Run") + " (3/6): Daily");
                                cmd.Parameters["p_period"].Value = "D";
                                cmd.Parameters["p_fgrp"].Value = this.DailyFilegroup.ToString("000");
                                dailyResults = Factory.CreateTable(cmd);
                                error = cmd.Parameters["p_error"].Value.ToString();
                            }

                            if (error == string.Empty && this.WeeklyFilegroup != 0 && weeklyChanges > 0 && (this.Workbench == Constants.Workbenches.Weekly || this.Workbench == Constants.Workbenches.Both))
                            {
                                OnProgress((applyChanges ? "Applying changes" : "Model Run") + " (4/6): Weekly");
                                cmd.Parameters["p_period"].Value = "W";
                                cmd.Parameters["p_fgrp"].Value = this.WeeklyFilegroup.ToString("000");
                                weeklyResults = Factory.CreateTable(cmd);
                                error = cmd.Parameters["p_error"].Value.ToString();
                            }

                            if (error == string.Empty)
                            {
                                tran.Commit();

                                _giveItBackItems.Clear();

                                if (dailyResults != null)
                                    RunModelUpdateResults(applyChanges, dailyResults, Constants.kDaily);
                                if (weeklyResults != null)
                                    RunModelUpdateResults(applyChanges, weeklyResults, Constants.kWeekly);

                                runCompleted = true;
                            }
                        }

                        if (error == string.Empty)
                            OnProgress("Complete.");
                        else
                        {
                            if (tran != null && tran.Connection != null)
                                tran.Rollback();
                            ExceptionHandler.RaiseAlert(error, (applyChanges ? "Apply changes failed" : "Model Run failed"), Shared.ExceptionHandler.AlertType.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (tran != null && tran.Connection != null)
                            tran.Rollback();
                        ExceptionHandler.RaiseException(ex, "RunModel");
                    }
                    finally
                    {
                        Factory.CloseConnection();
                    }
                }
            }
            else
            {
                if (applyChanges)
                    this.ReleaseLocks();
                ExceptionHandler.RaiseAlert("Levers are set to their original values.", "Apply / Model run is not required", Shared.ExceptionHandler.AlertType.Warning);
            }

            // Filegroups will have been release and records unlocked in DS887FS1
            if (applyChanges && runCompleted)
            {
                DailyFilegroup = 0;
                WeeklyFilegroup = 0;
            }
			
            return runCompleted;
        }

		private int RunModelCountChanges(Constants.Workbenches workbench)
		{
			var changes = 0;

			if (_data != null)
			{
				foreach (DataRow row in _data.Rows)
				{
                    row.EndEdit();

					if (row.RowState == DataRowState.Modified && row[colWorkBench].ToString() == workbench.ToString())
					{
						var rowChanged = false;

                        if (this._giveItBackChanged == true)
                        {
                            rowChanged = true;
                        }

						if (row[colAllocFlagStatus].ToString() != kStatusUnchanged && (row[colAllocFlagThisLevel].ToString() != row[colAllocFlagThisLevelOriginal].ToString() || row[colNewAllocFlag].ToString() != row[colCurAllocFlag].ToString()))
							rowChanged = true;
						else
							row[colAllocFlagStatus] = kStatusUnchanged;

						if (row[colPatternStatus].ToString() != kStatusUnchanged && (row[colPatternThisLevel].ToString() != row[colPatternThisLevelOriginal].ToString() || row[colNewPattern].ToString() != row[colCurPattern].ToString()))
							rowChanged = true;
						else
							row[colPatternStatus] = kStatusUnchanged;

						if (row[colUpliftStatus].ToString() != kStatusUnchanged && (row[colUpliftThisLevel].ToString() != row[colUpliftThisLevelOriginal].ToString() || row[colNewUpliftFactor].ToString() != row[colCurUpliftFactor].ToString()))
							rowChanged = true;
						else
							row[colUpliftStatus] = kStatusUnchanged;

						if (row[colCutOffStatus].ToString() != kStatusUnchanged && (row[colCutOffThisLevel].ToString() != row[colCutOffThisLevelOriginal].ToString() || row[colNewCutOff].ToString() != row[colCurCutOff].ToString()))
							rowChanged = true;
						else
							row[colCutOffStatus] = kStatusUnchanged;

						if (row[colSmoothingFactorStatus].ToString() != kStatusUnchanged && (row[colSmoothingFactorThisLevel].ToString() != row[colSmoothingFactorThisLevelOriginal].ToString() || row[colNewSmoothingFactor].ToString() != row[colCurSmoothingFactor].ToString()))
							rowChanged = true;
						else
							row[colSmoothingFactorStatus] = kStatusUnchanged;

                        if (rowChanged)
                            changes++;
                        else
                        {
                            row.RejectChanges();
                            // Don't want to do this as we could be deleting a lock that is actually required.
//                            ReleaseLock(row[colWorkBench].ToString(), new StockItem(row));
                        }
							
					}
				}
			}

			return changes;
		}

		private string RunModelClear(bool applyChanges, Constants.Workbenches workbench, OleDbTransaction tran)
		{
			var error = string.Empty;
			OnProgress((applyChanges ? "Applying changes" : "Model Run") + " (1/6): Clearing workfile: " + workbench.ToString());

			var cmd = Factory.CreateCommand("DS887KS1", CommandType.StoredProcedure,
				Factory.CreateParameter("p_fgrp", (workbench == Constants.Workbenches.Weekly ? this.WeeklyFilegroup.ToString("000") : this.DailyFilegroup.ToString("000")), OleDbType.Char, 3, ParameterDirection.Input),
				Factory.CreateParameter("p_dept", this.DepartmentId.ToString("000"), OleDbType.Char, 3, ParameterDirection.Input),
				Factory.CreateParameter("p_period", workbench.ToString().ToUpper(), OleDbType.Char, 1, ParameterDirection.Input),
				Factory.CreateParameter("p_error", string.Empty, OleDbType.Char, 80, ParameterDirection.InputOutput));
			cmd.Transaction = tran;

			try
			{
				cmd.ExecuteNonQuery();
				error = cmd.Parameters["p_error"].Value.ToString();
			}
			catch (Exception ex)
			{
				error = ex.Message + " in RunModelClear";
			}

			return error;
		}

		private string RunModelInsert(bool applyChanges, OleDbTransaction tran)
		{
			var result = string.Empty;
			var error = string.Empty;

			// Insert each changed row into the table
			var cmd = Factory.CreateCommand("DS887LS1", CommandType.StoredProcedure,
				Factory.CreateParameter("P_FGRP", "", OleDbType.Char, 3, ParameterDirection.Input),
				Factory.CreateParameter("P_PERD", "", OleDbType.Char, 1, ParameterDirection.Input),
				Factory.CreateParameter("P_MRWLVL", 0, OleDbType.Decimal, 2, ParameterDirection.Input),
				Factory.CreateParameter("P_MRWDPT", "", OleDbType.Char, 3, ParameterDirection.Input),
				Factory.CreateParameter("P_MRWSTP", "", OleDbType.Char, 1, ParameterDirection.Input),
				Factory.CreateParameter("P_MRWCLS", 0, OleDbType.Decimal, 4, ParameterDirection.Input),
				Factory.CreateParameter("P_MRWVND", 0, OleDbType.Decimal, 5, ParameterDirection.Input),
				Factory.CreateParameter("P_MRWSTY", 0, OleDbType.Decimal, 4, ParameterDirection.Input),
				Factory.CreateParameter("P_MRWCLR", 0, OleDbType.Decimal, 3, ParameterDirection.Input),
				Factory.CreateParameter("P_MRWSIZ", 0, OleDbType.Decimal, 4, ParameterDirection.Input),
				Factory.CreateParameter("P_MRWSTR", 0, OleDbType.Decimal, 3, ParameterDirection.Input),
				Factory.CreateParameter("P_MRWGRD", "", OleDbType.Char, 3, ParameterDirection.Input),
				Factory.CreateParameter("P_MRWMKT", "", OleDbType.Char, 2, ParameterDirection.Input),
				Factory.CreateParameter("P_MRWGBK", "", OleDbType.Char, 1, ParameterDirection.Input),
				Factory.CreateParameter("P_MRWFWF", 0, OleDbType.Decimal, 5, 2, ParameterDirection.Input),
				Factory.CreateParameter("P_MRWFWFSTS", "", OleDbType.Char, 1, ParameterDirection.Input),
				Factory.CreateParameter("P_MRWCCO", 0, OleDbType.Decimal, 5, 2, ParameterDirection.Input),
				Factory.CreateParameter("P_MRWCCOSTS", "", OleDbType.Char, 1, ParameterDirection.Input),
				Factory.CreateParameter("P_MRWALF", "", OleDbType.Char, 1, ParameterDirection.Input),
				Factory.CreateParameter("P_MRWALFSTS", "", OleDbType.Char, 1, ParameterDirection.Input),
				Factory.CreateParameter("P_MRWSMF", 0, OleDbType.Decimal, 5, 2, ParameterDirection.Input),
				Factory.CreateParameter("P_MRWSMFSTS", "", OleDbType.Char, 1, ParameterDirection.Input),
				Factory.CreateParameter("P_MRWPAL", 0, OleDbType.Decimal, 3, ParameterDirection.Input),
				Factory.CreateParameter("P_MRWPALSTS", "", OleDbType.Char, 1, ParameterDirection.Input),
				Factory.CreateParameter("P_ERROR", "", OleDbType.Char, 80, ParameterDirection.InputOutput));
			cmd.Transaction = tran;

			var counter = 0;
			foreach (DataRow row in _data.GetChanges().Rows)
			{
				// Apply values to parameters and execute the command
				OnProgress((applyChanges ? "Applying changes" : "Model Run") + " (2/6): Building model", (++counter * 100 / _data.GetChanges().Rows.Count));
				cmd.Parameters["P_FGRP"].Value = (row["WORKBENCH"].ToString() == "Daily" ? this.DailyFilegroup.ToString("000") : this.WeeklyFilegroup.ToString("000"));
				cmd.Parameters["P_PERD"].Value = row["WORKBENCH"].ToString().Left(1);
				cmd.Parameters["P_MRWLVL"].Value = Convert.ToDecimal(this.Parameter);
				cmd.Parameters["P_MRWDPT"].Value = this.DepartmentId.ToString("000");
				cmd.Parameters["P_MRWSTP"].Value = (row["STORETYPE"].ToString() == Constants.kOnline ? "C" : "B");
				cmd.Parameters["P_MRWCLS"].Value = Convert.ToDecimal(row["CLASS"]);
				cmd.Parameters["P_MRWVND"].Value = Convert.ToDecimal(row["VENDOR"]);
				cmd.Parameters["P_MRWSTY"].Value = Convert.ToDecimal(row["STYLE"]);
				cmd.Parameters["P_MRWCLR"].Value = Convert.ToDecimal(row["COLOUR"]);
				cmd.Parameters["P_MRWSIZ"].Value = Convert.ToDecimal(row["SIZE"]);
				cmd.Parameters["P_MRWSTR"].Value = Convert.ToDecimal(row["STORE"]);
				cmd.Parameters["P_MRWGRD"].Value = row["GRADE"].ToString();
				cmd.Parameters["P_MRWMKT"].Value = row["MARKET"].ToString();
				cmd.Parameters["P_MRWGBK"].Value = row["GIVEITBACK"].ToString();
				cmd.Parameters["P_MRWFWF"].Value = Convert.ToDecimal(row["NEWUPLIFTFACTOR"]);
				cmd.Parameters["P_MRWFWFSTS"].Value = row["UPLIFTSTATUS"].ToString();
				cmd.Parameters["P_MRWCCO"].Value = Convert.ToDecimal(row["NEWCUTOFF"]);
				cmd.Parameters["P_MRWCCOSTS"].Value = row["CUTOFFSTATUS"].ToString();
				cmd.Parameters["P_MRWALF"].Value = row["NEWALLOCFLAG"].ToString();
				cmd.Parameters["P_MRWALFSTS"].Value = row["ALLOCFLAGSTATUS"].ToString();
				cmd.Parameters["P_MRWSMF"].Value = Convert.ToDecimal(row["NEWSMOOTHINGFACTOR"]);
				cmd.Parameters["P_MRWSMFSTS"].Value = row["SMOOTHINGFACTORSTATUS"].ToString();
				cmd.Parameters["P_MRWPAL"].Value = Convert.ToDecimal(row["NEWPATTERN"]);
				cmd.Parameters["P_MRWPALSTS"].Value = row["PATTERNSTATUS"].ToString();
				
				error = string.Empty;
				try
				{
					cmd.ExecuteNonQuery();
					error = cmd.Parameters["P_ERROR"].Value.ToString();
				}
				catch (Exception ex)
				{
					error = ex.Message;
				}

				if (error != string.Empty)
				{
					row["ERROR"] = error;
					result = "Multiple insert errors - See the Error column";
				}
			}
			return result;
		}

        private void RunModelUpdateResults(bool applyChanges, DataTable sourceTable, string workbench)
        {
            var counter = 0;

            foreach (DataRow sourceRow in sourceTable.Rows)
            {
                OnProgress((applyChanges ? "Applying changes" : "Model Run") + " (5/6): Applying changes to " + workbench + " workbench", (++counter * 100 / sourceTable.Rows.Count));

                var criteria = new StringBuilder();
                criteria.AppendFormat("CLASS={0}", sourceRow["MRWCLS"].ToString());
				criteria.AppendFormat(" AND VENDOR={0}", sourceRow["MRWVND"].ToString());
                criteria.AppendFormat(" AND STYLE={0}", sourceRow["MRWSTY"].ToString());
                criteria.AppendFormat(" AND COLOUR={0}", sourceRow["MRWCLR"].ToString());
                criteria.AppendFormat(" AND SIZE={0}", sourceRow["MRWSIZ"].ToString());
                criteria.AppendFormat(" AND WORKBENCH='{0}'", workbench);
                criteria.AppendFormat(" AND STORETYPE='{0}'", (sourceRow["MRWSTP"].ToString() == "C" ? Constants.kOnline : Constants.kBricksAndMortar));

				if (sourceRow["MRWSTR"].ToString() != "0")
					criteria.AppendFormat(" AND STORE={0}", sourceRow["MRWSTR"].ToString());
				
                if (sourceRow["MRWGRD"].ToString() != string.Empty)
                    criteria.AppendFormat(" AND GRADE='{0}'", sourceRow["MRWGRD"].ToString());
				
                if (sourceRow["MRWMKT"].ToString() != string.Empty)
                    criteria.AppendFormat(" AND MARKET='{0}'", sourceRow["MRWMKT"].ToString());

                var targetRows = _data.Select(criteria.ToString());
                if (targetRows != null)
                {
                    foreach (DataRow targetRow in targetRows)
                    {
                        targetRow[colTotalStockRequired] = sourceRow["MRWTSR"];

                        // Don't set Ideal or Proposed alloc for online stores at anything other than item/style level.
                        if (targetRow[colStoreType].ToString() == Constants.kBricksAndMortar || ((targetRow[colStoreType].ToString() == Constants.kOnline && (this.Parameter == Parameters.ItemLevel || this.Parameter == Parameters.StyleLevel))))
                        {
                            targetRow[colIdealAllocQty] = sourceRow["MRWIAQ"];
                            targetRow[colProposedAllocQty] = sourceRow["MRWPAQ"];
                        }

                        targetRow[colNewSmoothedRateOfSale] = sourceRow["MRWSRS"];

						if (workbench == Constants.kDaily)
						{
							var gib = new GiveItBack();
                            if (gib.SetValues(targetRow, Convert.ToDecimal(sourceRow["MRWTSR"])))
                            {
                                _giveItBackItems.AddItem(gib);    
                            }							
						}
					}
                }
            }
        }

		private string RunModelGiveItBack(bool applyChanges, OleDbTransaction tran)
		{
			var result = string.Empty;

			try
			{
				
				OnProgress((applyChanges ? "Applying changes" : "Model Run") + " (6/6): Give It Back");

				var cmd = Factory.CreateCommand("DS887RS1", CommandType.StoredProcedure,
					Factory.CreateParameter("P_FILGRP", "F" + this.DailyFilegroup.ToString("000"), OleDbType.Char, 4, ParameterDirection.Input),
					Factory.CreateParameter("P_ALCNUM", 0, OleDbType.Decimal, 5, 0, ParameterDirection.Input),
					Factory.CreateParameter("P_STORE", 0, OleDbType.Decimal, 3, 0, ParameterDirection.Input),
					Factory.CreateParameter("P_CLASS", 0, OleDbType.Decimal, 4, 0, ParameterDirection.Input),
					Factory.CreateParameter("P_VENDOR", 0, OleDbType.Decimal, 5, 0, ParameterDirection.Input),
					Factory.CreateParameter("P_STYLE", 0, OleDbType.Decimal, 4, 0, ParameterDirection.Input),
					Factory.CreateParameter("P_COLOUR", 0, OleDbType.Decimal, 3, 0, ParameterDirection.Input),
					Factory.CreateParameter("P_SIZE", 0, OleDbType.Decimal, 4, 0, ParameterDirection.Input),
					Factory.CreateParameter("P_QTY", 0, OleDbType.Decimal, 7, 0, ParameterDirection.Input),
					Factory.CreateParameter("P_STATUS", "V", OleDbType.Char, 1, ParameterDirection.Input),
					Factory.CreateParameter("P_RQSTYPE", "GIVE BACK DETAIL", OleDbType.Char, 20, ParameterDirection.Input));

				cmd.Transaction = tran;

				foreach (var item in _giveItBackItems)
				{                     
					cmd.Parameters["P_STORE"].Value = 0;
					cmd.Parameters["P_CLASS"].Value = item.StockItem.Class;
					cmd.Parameters["P_VENDOR"].Value = item.StockItem.Vendor;
					cmd.Parameters["P_STYLE"].Value = item.StockItem.Style;
					cmd.Parameters["P_COLOUR"].Value = item.StockItem.Colour;
					cmd.Parameters["P_SIZE"].Value = item.StockItem.Size;
					cmd.Parameters["P_QTY"].Value = item.GiveItBackValue;
					cmd.ExecuteNonQuery();
				}


                SysInfo sysInfo = new SysInfo();
                var constValue = sysInfo.GetConstant("RING FENCED DC STOCK ENVIRO");

                if (constValue != string.Empty)
                {
                    if (constValue == "N")
                    {
                        Factory.SubmitDSIPWRAP("S886IC0001", "DS886HC", new string(' ', 19) + this.DailyFilegroup.ToString("000"), "", 'N', ' ', tran);                        
                    }
                    else
                    {
                        Factory.SubmitDSIPWRAP("S885YC0001", "DS885XC", new string(' ', 19) + this.DailyFilegroup.ToString("000"), "", 'N', ' ', tran);
                    }
                }				

			}
			catch (Exception ex)
			{
				result = "RunModelGiveItBack: " + ex.Message;
			}

			return result;
		}

        /// <summary>
        /// Cancel changes made to the levers.  Unlocks all records and releases filegroups
        /// </summary>
        public void DiscardChanges()
        {
            ReleaseLocks();
        }
        //-------------------------------------------------------------------------------
        #endregion

        #region Private properties and methods
        //-------------------------------------------------------------------------------
        private string CreateSQL()
        {
            var sb = new StringBuilder();
            var sql = string.Empty;
            
            var parameterInfo = GetParameterInfo(this.Parameter);
			var orderBy = "1";

            switch (this.Stage)
            {
                case Stages.SelectStore:
                    sb.Append(Properties.Resources.SQLFocusGroupsSelectStores);
                    AddCriteria(sb, "SDDPT", this.DepartmentId);

                    if (!this.AllMarkets)
                        AddCriteria(sb, "SDMKT", this.Markets);

					if (this.Workbench != Constants.Workbenches.Both)
						AddCriteria(sb, "SDWBENCH", Constants.GetWorkbenchName(this.Workbench).Left(1));

					if (this.StoreType != Constants.StoreTypes.Both)
						AddCriteria(sb, "SDSTRTYP", Constants.GetStoreTypeName(this.StoreType).Left(1));

                    if (this.FocusGroup == FocusGroups.FastestCoverStores)
                        sb.Append(" ORDER BY SDSWOC");
                    else
                        sb.Append(" ORDER BY SDSWOC DESC");

                    sb.Append(string.Format(" FETCH FIRST {0} ROWS ONLY", this.NumberOf));

                    sql = sb.ToString().Replace("<prefix>", string.Empty);
                    break;

                case Stages.SelectClass:
                    sb.Append(Properties.Resources.SQLFocusGroupsSelectClasses.Replace("<storeId>", this.StoreId.ToString()).Replace("<departmentId>", this.DepartmentId.ToString()));

					if (this.Workbench != Constants.Workbenches.Both)
						AddCriteria(sb, "DCWBENCH", Constants.GetWorkbenchName(this.Workbench).Left(1));

					if (this.StoreType != Constants.StoreTypes.Both)
						AddCriteria(sb, "DCSTRTYP", Constants.GetStoreTypeName(this.StoreType).Left(1));

					sql = sb.ToString().Replace("<prefix>", string.Empty);
                    break;

                case Stages.DetailedWorkbench:

					WeeklyFilegroup = 0;
					DailyFilegroup = 0;

					_inherittedValues.Clear();
                    _giveItBackItems.Clear();
                    _leverLocks.Clear();

					sb.Append(Properties.Resources.SQLDetailedWorkbench);
                        sb.Append(" WHERE <prefix>DPT=" + this.DepartmentId.ToString());

                    if (this.Promotions.Count > 0)
                    {
                        var promo = string.Empty;
						foreach (LookupItem value in this.Promotions)
                        {
                            if (promo != string.Empty)
                                promo += ",";
                            promo += string.Format("'{0}'", value.Id);
                        }                        
                        
                        /*
                            no store number at grade or market level.
                            must obtain from DSSRSGR to get store info
                        */
                        switch (this.Parameter)
                        {
                            case Parameters.ItemStoreLevel:
                            case Parameters.StyleStoreLevel:
                                sb.AppendFormat(" AND <prefix>STR IN (SELECT DISTINCT STORE_NUMBER FROM DSPROSTR WHERE PROMOTION_NAME IN ({0}))", promo);
                                break;

                            case Parameters.ItemGradeLevel:
                            case Parameters.StyleGradeLevel:
                                sb.Append(" AND EXISTS (SELECT DISTINCT STORE_NUMBER FROM DSPROSTR JOIN DSSRSGR g ON g.GDGRADE = <tableName>.<prefix>GRD AND g.GDDEPT = " + this.DepartmentId.ToString());
                                sb.AppendFormat(" WHERE PROMOTION_NAME IN ({0}))", promo);
                                break;

                            case Parameters.ItemMarketLevel:
                            case Parameters.StyleMarketLevel:
                                sb.Append(" AND EXISTS (SELECT DISTINCT STORE_NUMBER FROM DSPROSTR JOIN DSSRSGR g ON g.GDMKT = <tableName>.<prefix>MKT AND g.GDDEPT = " + this.DepartmentId.ToString());
                                sb.AppendFormat(" WHERE PROMOTION_NAME IN ({0}))", promo);
                                break;
                        }

                        sb.AppendFormat(" AND <prefix>UPC IN (SELECT DISTINCT ITEM_UPC FROM DSPROITM WHERE PROMOTION_NAME IN ({0}))", promo);                       
                    }

                    AddCriteria(sb, "FAMC", this.FranchiseId);
                    AddCriteria(sb, "COGP", this.CoordinateGroup);

                    if (!string.IsNullOrEmpty(this.StoreGroup))
                    {
                        var sarr = Factory.GetValue("IPSTRTB", "SARR", "SKEY", "U" + this.StoreGroup, string.Empty).ToString();
                        if (sarr != string.Empty)
                            sb.AppendFormat(" AND SUBSTRING('{0}', CAST(<prefix>STR AS INTEGER), 1)='1'", sarr);
                    }

                    if (!this.ShowFullPriceLines && !this.ShowMarkedDownLines && this.ShowProposedMarkedDownLines)
                        sb.Append(" AND <prefix>PMDWN='Y'");

                    else if (!this.ShowFullPriceLines && this.ShowMarkedDownLines && !this.ShowProposedMarkedDownLines)
                        sb.Append(" AND <prefix>MDEV<>'' AND <prefix>PMDWN<>'Y'");

                    else if (!this.ShowFullPriceLines && this.ShowMarkedDownLines && this.ShowProposedMarkedDownLines)
                        sb.Append(" AND <prefix>MDEV<>''");

                    else if (this.ShowFullPriceLines && !this.ShowMarkedDownLines && !this.ShowProposedMarkedDownLines)
                        sb.Append(" AND <prefix>MDEV='' AND <prefix>PMDWN<>'Y'");

                    else if (this.ShowFullPriceLines && !this.ShowMarkedDownLines && this.ShowProposedMarkedDownLines)
                        sb.Append(" AND <prefix>MDEV=''");

                    else if (this.ShowFullPriceLines && this.ShowMarkedDownLines && !this.ShowProposedMarkedDownLines)
                        sb.Append(" AND <prefix>PMDWN<>'Y'");

                    AddCriteria(sb, this.ExcludeZeroStock, " AND <prefix>ESOH>0");
                    
                    if (this.SelectedItem.StockItem.HasValue)
                    {
                        AddCriteria(sb, "CLS", this.SelectedItem.StockItem.Class);
						AddCriteria(sb, "VEN", this.SelectedItem.StockItem.Vendor);
						AddCriteria(sb, "STY", this.SelectedItem.StockItem.Style);
						AddCriteria(sb, "CLR", this.SelectedItem.StockItem.Colour);
						AddCriteria(sb, "SIZ", this.SelectedItem.StockItem.Size);

						AddCriteria(sb, "MKT", this.SelectedItem.Market);
						AddCriteria(sb, "STR", this.SelectedItem.StoreId);
						AddCriteria(sb, "GRD", this.SelectedItem.GradeId);			
					}
                    else
                    {
                        if (string.IsNullOrEmpty(this.UPC))
                        {
                            AddCriteria(sb, "CLS", this.Item.Class);
                            AddCriteria(sb, "VEN", this.Item.Vendor);
                            AddCriteria(sb, "STY", this.Item.Style);
                            AddCriteria(sb, "CLR", this.Item.Colour);
                            AddCriteria(sb, "SIZ", this.Item.Size);
                        }
                        else
                            AddCriteria(sb, "UPC", this.UPC);
                    
						if (this.Parameter == Parameters.ItemMarketLevel || this.Parameter == Parameters.StyleMarketLevel)
							AddCriteria(sb, "MKT", this.Market);

						if (this.Parameter == Parameters.ItemGradeLevel || this.Parameter == Parameters.StyleGradeLevel)
						{
							AddCriteria(sb, "MKT", this.Market);
							AddCriteria(sb, "GRD", this.StoreGradeId);
						}

						if (this.Parameter == Parameters.ItemStoreLevel || this.Parameter == Parameters.StyleStoreLevel)
						{
							AddCriteria(sb, "MKT", this.Market);
							AddCriteria(sb, "GRD", this.StoreGradeId);
							AddCriteria(sb, "STR", this.StoreId);
						}
					}

                    switch (this.StoreType)
                    {
                        case Constants.StoreTypes.BricksAndMortar:
                            AddCriteria(sb, "STRTYP", "B");
                            break;
						case Constants.StoreTypes.Online:
                            AddCriteria(sb, "STRTYP", "C");
                            break;
                    }

                    if (this.Workbench != Constants.Workbenches.Both)
                        AddCriteria(sb, "WBENCH", Constants.GetWorkbenchName(this.Workbench).Left(1));

                        FetchLimit = 0;
                        switch (this.FocusGroup)
                        {
                            case FocusGroups.NewLinesReceived:
                                var receiptDate = DateTime.Today.AddDays((double)this.NumberOf * -7);
                                AddCriteria(sb, "FRDT", receiptDate, ">=");
                                break;

                            case FocusGroups.FastestCoverLines:
                                FetchLimit = (int)this.NumberOf;
                                orderBy = "<prefix>CLRK DESC";
                                break;

                            case FocusGroups.SlowestCoverLines:
                                FetchLimit = (int)this.NumberOf;
                                orderBy = "<prefix>CLRK ASC";
                                break;

                            case FocusGroups.TopRetailPerformers:
                                FetchLimit = (int)this.NumberOf;
                                orderBy = "<prefix>RPRK ASC";
                                break;
                        }
					
					if (orderBy != string.Empty)
						sb.Append(" ORDER BY " + orderBy);

					sql = sb.ToString();

					if (this.Parameter.ToString().Contains("Style"))
						sql = sql.Replace("<item>", "RIGHT('0000' || <prefix>CLS, 4) || '-' || RIGHT('00000' || <prefix>VEN, 5) || '-' || RIGHT('0000' || <prefix>STY, 4) || '-' || RIGHT('000' || <prefix>CLR, 3)");
					else
						sql = sql.Replace("<item>", "RIGHT('0000' || <prefix>CLS, 4) || '-' || RIGHT('00000' || <prefix>VEN, 5) || '-' || RIGHT('0000' || <prefix>STY, 4) || '-' || RIGHT('000' || <prefix>CLR, 3) || '-' || RIGHT('0000' || <prefix>SIZ, 4)");


                    if (PreviousParameter == Parameters.StyleGradeLevel || PreviousParameter == Parameters.ItemGradeLevel)
                    {
                        if (Parameter == Parameters.StyleMarketLevel || Parameter == Parameters.ItemMarketLevel)
                        {
                            //Bugzilla: Bug#185 Switching level "upwards" from grade level to market level, does not
                            //work. This is because the grade is not always populated on the market level WB tables
                            parameterInfo = GetParameterInfo(this.PreviousParameter);
                        }
                    }

					sql = sql.Replace("<prefix>", parameterInfo.Prefix)
                        .Replace("<departmentId>", this.DepartmentId.ToString())
                        .Replace("<tableName>", parameterInfo.WeeklyTable);

					FirstTime = false;

                    break;
            }


            return sql;
        }

        private void AddCriteria(StringBuilder sb, string fieldName, string value)
        {
            if (!string.IsNullOrEmpty(value))
                sb.Append(string.Format(" AND <prefix>{0}='{1}'", fieldName, value));
        }

        private void AddCriteria(StringBuilder sb, string fieldName, List<LookupItem> values)
        {
            if (values.Count > 0)
            {
                var items = new List<string>();
                foreach (LookupItem value in values)
                    items.Add(value.Id.ToString());

                sb.Append(string.Format(" AND <prefix>{0} IN ('{1}')", fieldName, string.Join("','", items)));
            }
        }

        private void AddCriteria(StringBuilder sb, string fieldName, decimal? value)
        {
            if (value.HasValue && value.Value>0)
                sb.Append(string.Format(" AND <prefix>{0}={1}", fieldName, value));
        }

        private void AddCriteria(StringBuilder sb, string fieldName, DateTime? value, string mathOperation="=")
        {
            if (value.HasValue)
				sb.Append(string.Format(" AND <prefix>{0}{1}'{2}'", fieldName, mathOperation, value.Value.ToString("yyyy-MM-dd")));
        }

        private void AddCriteria(StringBuilder sb, bool criteria, string expression)
        {
            if (criteria)
                sb.Append(expression);
        }

        private int CountValues(params object[] items)
        {
            var count = 0;
            foreach (var item in items)
            {
                if (item != null && item.ToString() != string.Empty && item.ToString() != "0")
                    count++;
            }
            return count;
        }

        public InherittedInfo GetInherittedValue(DataRow row, DetailedWorkbenchInfo.Parameters parameter)
        {
			IEnumerable<InherittedInfo> qry = null;

			switch (parameter)
			{
				case Parameters.ItemLevel:
				case Parameters.StyleLevel:
						qry = from i in _inherittedValues
						  where i.DepartmentId == this.DepartmentId
							&& i.StoreType == (row[colStoreType] ?? string.Empty).ToString()
							&& i.Member.Left(1) == (row[colWorkBench] ?? string.Empty).ToString().Left(1)
							&& i.Class == (decimal)(row[colClass] ?? 0)
							&& i.Vendor == (decimal)(row[colVendor] ?? 0)
							&& i.Style == (decimal)(row[colStyle] ?? 0)
							&& i.Colour == (decimal)(row[colColour] ?? 0)
							&& i.Size == (decimal)(row[colSize] ?? 0)
						  select i;
						break;

				case Parameters.ItemMarketLevel:
				case Parameters.StyleMarketLevel:
						qry = from i in _inherittedValues
						  where i.DepartmentId == this.DepartmentId
							&& i.StoreType == (row[colStoreType] ?? string.Empty).ToString()
							&& i.Member.Left(1) == (row[colWorkBench] ?? string.Empty).ToString().Left(1)
							&& i.Class == (decimal)(row[colClass] ?? 0)
							&& i.Vendor == (decimal)(row[colVendor] ?? 0)
							&& i.Style == (decimal)(row[colStyle] ?? 0)
							&& i.Colour == (decimal)(row[colColour] ?? 0)
							&& i.Size == (decimal)(row[colSize] ?? 0)
							&& i.Market == (row[colMarket] ?? string.Empty).ToString()
						  select i;
						break;

				case Parameters.ItemGradeLevel:
				case Parameters.StyleGradeLevel:
						qry = from i in _inherittedValues
						  where i.DepartmentId == this.DepartmentId
							&& i.StoreType == (row[colStoreType] ?? string.Empty).ToString()
							&& i.Member.Left(1) == (row[colWorkBench] ?? string.Empty).ToString().Left(1)
							&& i.Class == (decimal)(row[colClass] ?? 0)
							&& i.Vendor == (decimal)(row[colVendor] ?? 0)
							&& i.Style == (decimal)(row[colStyle] ?? 0)
							&& i.Colour == (decimal)(row[colColour] ?? 0)
							&& i.Size == (decimal)(row[colSize] ?? 0)
							&& i.Grade == (row[colGrade] ?? string.Empty).ToString()
						  select i;
						break;

				case Parameters.ItemStoreLevel:
				case Parameters.StyleStoreLevel:
						qry = from i in _inherittedValues
						  where i.DepartmentId == this.DepartmentId
							&& i.StoreType == (row[colStoreType] ?? string.Empty).ToString()
							&& i.Member.Left(1) == (row[colWorkBench] ?? string.Empty).ToString().Left(1)
							&& i.Class == (decimal)(row[colClass] ?? 0)
							&& i.Vendor == (decimal)(row[colVendor] ?? 0)
							&& i.Style == (decimal)(row[colStyle] ?? 0)
							&& i.Colour == (decimal)(row[colColour] ?? 0)
							&& i.Size == (decimal)(row[colSize] ?? 0)
							&& i.Store == (decimal) (row[colStore] ?? 0)
						  select i;
						break;
			}

			var inherittedValue = qry.FirstOrDefault();

			if (inherittedValue == null)
			{
				inherittedValue = new InherittedInfo(this, row);
				if (Factory.OpenConnection())
					try
					{
						var cmd = Factory.CreateCommand("DS883HS1", CommandType.StoredProcedure,
							Factory.CreateParameter("PDEPT", inherittedValue.DepartmentId, OleDbType.Decimal, 3, ParameterDirection.Input),
							Factory.CreateParameter("PSRSTRTYP", inherittedValue.GetStoreType(), OleDbType.Char, 1, ParameterDirection.Input),
							Factory.CreateParameter("PMODE", inherittedValue.Mode, OleDbType.Char, 1, ParameterDirection.Input),
							Factory.CreateParameter("PCLASS", inherittedValue.Class, OleDbType.Decimal, 4, ParameterDirection.Input),
							Factory.CreateParameter("PVENDOR", inherittedValue.Vendor, OleDbType.Decimal, 5, ParameterDirection.Input),
							Factory.CreateParameter("PSTYLE", inherittedValue.Style, OleDbType.Decimal, 4, ParameterDirection.Input),
							Factory.CreateParameter("PCOLOUR", inherittedValue.Colour, OleDbType.Decimal, 3, ParameterDirection.Input),
							Factory.CreateParameter("PSIZE", inherittedValue.Size, OleDbType.Decimal, 4, ParameterDirection.Input),
							Factory.CreateParameter("PHIERLVL", inherittedValue.HierarchyLevel.ToString("00"), OleDbType.Char, 2, ParameterDirection.Input),
							Factory.CreateParameter("PLVLVALUE", inherittedValue.HierarchyValue, OleDbType.Char, 3, ParameterDirection.Input),
							Factory.CreateParameter("PMEMBER", inherittedValue.Member, OleDbType.Char, 10, ParameterDirection.Input),
							Factory.CreateParameter("PSTRMKT", inherittedValue.Market, OleDbType.Char, 2, ParameterDirection.Input),
							Factory.CreateParameter("PSTRGRD", inherittedValue.Grade, OleDbType.Char, 3, ParameterDirection.Input),

							Factory.CreateParameter("PSUMSTR", 0, OleDbType.Decimal, 3, ParameterDirection.Input),

							Factory.CreateParameter("PALCFLG", inherittedValue.AllocationLever.Value, OleDbType.Char, 1, ParameterDirection.InputOutput),
							Factory.CreateParameter("PCOVCUTO", inherittedValue.CutOffLever.Value, OleDbType.Decimal, 5, 2, ParameterDirection.InputOutput),
							Factory.CreateParameter("PSRPATT", inherittedValue.PatternLever.Value, OleDbType.Decimal, 3, ParameterDirection.InputOutput),
							Factory.CreateParameter("PSMFFCT", inherittedValue.SmoothingLever.Value, OleDbType.Decimal, 5, 2, ParameterDirection.InputOutput),
							Factory.CreateParameter("PUPLFTFCT", inherittedValue.UpliftLever.Value, OleDbType.Decimal, 5, 2, ParameterDirection.InputOutput),

                            // Probably don't need these parameters
							Factory.CreateParameter("PALFTLV", inherittedValue.AllocationLever.AtThisLevel, OleDbType.Char, 1, ParameterDirection.InputOutput),
							Factory.CreateParameter("PCCOTLV", inherittedValue.CutOffLever.AtThisLevel, OleDbType.Char, 1, ParameterDirection.InputOutput),
							Factory.CreateParameter("PPAIFTLV", inherittedValue.PatternLever.AtThisLevel, OleDbType.Char, 1, ParameterDirection.InputOutput),
							Factory.CreateParameter("PSMFTLV", inherittedValue.SmoothingLever.AtThisLevel, OleDbType.Char, 1, ParameterDirection.InputOutput),
							Factory.CreateParameter("PFWFTLV", inherittedValue.UpliftLever.AtThisLevel, OleDbType.Char, 1, ParameterDirection.InputOutput),

							Factory.CreateParameter("PSRAFLVL", inherittedValue.AllocationLever.HierarchyLevel, OleDbType.Decimal, 2, ParameterDirection.InputOutput),
							Factory.CreateParameter("PSRCCLVL", inherittedValue.CutOffLever.HierarchyLevel, OleDbType.Decimal, 2, ParameterDirection.InputOutput),
							Factory.CreateParameter("PSRPALVL", inherittedValue.PatternLever.HierarchyLevel, OleDbType.Decimal, 2, ParameterDirection.InputOutput),
							Factory.CreateParameter("PSRSFLVL", inherittedValue.SmoothingLever.HierarchyLevel, OleDbType.Decimal, 2, ParameterDirection.InputOutput),
							Factory.CreateParameter("PSRFFLVL", inherittedValue.UpliftLever.HierarchyLevel, OleDbType.Decimal, 2, ParameterDirection.InputOutput));

						cmd.ExecuteNonQuery();
                        
						inherittedValue.AllocationLever.Value = (string)(cmd.Parameters["PALCFLG"].Value ?? string.Empty);
						inherittedValue.CutOffLever.Value = (decimal) (cmd.Parameters["PCOVCUTO"].Value ?? 0);
						inherittedValue.PatternLever.Value = (decimal) (cmd.Parameters["PSRPATT"].Value ?? 0);
						inherittedValue.SmoothingLever.Value = (decimal) (cmd.Parameters["PSMFFCT"].Value ?? 0);
						inherittedValue.UpliftLever.Value = (decimal) (cmd.Parameters["PUPLFTFCT"].Value ?? 0);

                        // Probably don't need these parameters
						inherittedValue.AllocationLever.AtThisLevel = (string)  (cmd.Parameters["PALFTLV"].Value ?? string.Empty);
						inherittedValue.CutOffLever.AtThisLevel = (string)(cmd.Parameters["PCCOTLV"].Value ?? string.Empty);
						inherittedValue.PatternLever.AtThisLevel = (string)(cmd.Parameters["PPAIFTLV"].Value ?? string.Empty);
						inherittedValue.SmoothingLever.AtThisLevel = (string)(cmd.Parameters["PSMFTLV"].Value ?? string.Empty);
						inherittedValue.UpliftLever.AtThisLevel = (string)(cmd.Parameters["PFWFTLV"].Value ?? string.Empty);

						inherittedValue.AllocationLever.HierarchyLevel = (decimal)  (cmd.Parameters["PSRAFLVL"].Value ?? 0);
						inherittedValue.CutOffLever.HierarchyLevel = (decimal)  (cmd.Parameters["PSRCCLVL"].Value ?? 0);
						inherittedValue.PatternLever.HierarchyLevel = (decimal)  (cmd.Parameters["PSRPALVL"].Value ?? 0);
						inherittedValue.SmoothingLever.HierarchyLevel = (decimal)  (cmd.Parameters["PSRSFLVL"].Value ?? 0);
						inherittedValue.UpliftLever.HierarchyLevel = (decimal)  (cmd.Parameters["PSRFFLVL"].Value ?? 0);

						inherittedValue.Exists = true;
						_inherittedValues.Add(inherittedValue);
					}
					catch (Exception ex)
					{
						ExceptionHandler.RaiseException(ex, "GetInherittedValue()");
					}
					finally
					{
						Factory.CloseConnection();
					}
			}

			return inherittedValue;
        }

        //-------------------------------------------------------------------------------
        #endregion

    }

    // Used to maintain a cache of locks so that we don't necessarily need to hit AS400 for each sequential row.
    public class LeverLock : IEquatable<LeverLock>
    {
        public decimal Class { get; set; }
        public decimal Vendor { get; set; }
        public decimal Style { get; set; }
        public string Workbench { get; set; }
//        public decimal Filegroup { get; set; }

        public bool Equals(LeverLock other)
        {
            return (this.Class == other.Class 
                && this.Vendor == other.Vendor 
                && this.Style == other.Style 
                && this.Workbench == other.Workbench);
//                && this.Filegroup == other.Filegroup);               
        }
    }

    public class ParameterInfo
    {
        public DetailedWorkbenchInfo.Parameters Parameter { get; internal set; }
        public string Description { get; internal set; }
        public string Prefix { get; internal set; }
        public string WeeklyTable { get; internal set; }
        public string DailyTable { get; internal set; }
        public string BothTable { get; internal set; }
		public bool FocusGroups { get; internal set; }

        public ParameterInfo()
        {
            Parameter = DetailedWorkbenchInfo.Parameters.ItemLevel;
            Description = string.Empty;
            Prefix = string.Empty;
            WeeklyTable = string.Empty;
			FocusGroups = false;
        }

        public ParameterInfo(DetailedWorkbenchInfo.Parameters parameter, string description, string prefix, string tableName, bool focusGroups = false)
        {
            Parameter = parameter;
            Description = description;
            Prefix = prefix;
            WeeklyTable = tableName;
			FocusGroups = focusGroups;
        }
    }

    // Used when drilling to a different level.
	public class SelectedItem
	{
		public StockItem StockItem { get; private set; }
		public string Market { get; private set; }
		public decimal? StoreId { get; private set; }
		public string GradeId { get; private set; }

		public SelectedItem()
		{
			StockItem = new StockItem();
		}

		public void Clear()
		{
			StockItem.Clear();
			Market = null;
			StoreId = null;
			GradeId = null;
		}

		public void SetItem(decimal? itemClass, decimal? itemVendor, decimal? itemStyle)
		{
			this.StockItem.Class = itemClass;
			this.StockItem.Vendor = itemVendor;
			this.StockItem.Style = itemStyle;
			this.StockItem.Colour = null;
			this.StockItem.Size = null;
		}

		public void SetItem(decimal? itemClass, decimal? itemVendor, decimal? itemStyle, decimal? itemColour, decimal? itemSize)
		{
			this.StockItem.Class = itemClass;
			this.StockItem.Vendor = itemVendor;
			this.StockItem.Style = itemStyle;
			this.StockItem.Colour = itemColour;
			this.StockItem.Size = itemSize;
		}

		public void SetMarket(string market)
		{
			this.Market = market;
		}

		public void SetStoreId(decimal? storeId)
		{
			this.StoreId = storeId;
		}

		public void SetGradeId(string gradeId)
		{
			this.GradeId = gradeId;
		}

	}

    // Used for the inheritted hierarchy cache.
	public class InherittedInfo
	{
		public class LeverInfo<T>
		{
			public T Value { get; set; }
			public string AtThisLevel { get; set; }
			public decimal HierarchyLevel { get; set; }

			public LeverInfo()
			{
				AtThisLevel = " ";
				HierarchyLevel = 1;
			}

		}

		public decimal DepartmentId { get; private set; }
		public string StoreType { get; private set; }
		public char Mode { get; private set; } // M for model run.
		public decimal Class { get; private set; }
		public decimal Vendor { get; private set; }
		public decimal Style { get; private set; }
		public decimal Colour { get; private set; }
		public decimal Size { get; private set; }
		public int HierarchyLevel { get; private set; }
		public string HierarchyValue { get; private set; }
		public string Member { get; private set; }
		public decimal Store { get; private set; }
		public string Grade { get; private set; }
		public string Market { get; private set; }

		public LeverInfo<string> AllocationLever { get; private set; }
		public LeverInfo<decimal> CutOffLever { get; private set; }
		public LeverInfo<decimal> PatternLever { get; private set; }
		public LeverInfo<decimal> SmoothingLever { get; private set; }
		public LeverInfo<decimal> UpliftLever { get; private set; }

		public bool Exists { get; internal set; }

		public string GetStoreType()
		{
			if (this.StoreType == Constants.kBricksAndMortar)
				return "B";
			else
				return "C";
		}

		public InherittedInfo()
		{
			AllocationLever = new LeverInfo<string>();
			CutOffLever = new LeverInfo<decimal>();
			PatternLever = new LeverInfo<decimal>();
			SmoothingLever = new LeverInfo<decimal>();
			UpliftLever = new LeverInfo<decimal>();
			Exists = false;
		}

		public InherittedInfo(DetailedWorkbenchInfo workbenchInfo, DataRow row)
		{
			AllocationLever = new LeverInfo<string>();
			CutOffLever = new LeverInfo<decimal>();
			PatternLever = new LeverInfo<decimal>();
			SmoothingLever = new LeverInfo<decimal>();
			UpliftLever = new LeverInfo<decimal>();

			DepartmentId = workbenchInfo.DepartmentId;
			Class = (decimal) (row[DetailedWorkbenchInfo.colClass] ?? 0);
			Vendor = (decimal)(row[DetailedWorkbenchInfo.colVendor] ?? 0);
			Style = (decimal)(row[DetailedWorkbenchInfo.colStyle] ?? 0);
			Colour = (decimal)(row[DetailedWorkbenchInfo.colColour] ?? 0);
			Size = (decimal)(row[DetailedWorkbenchInfo.colSize] ?? 0);
			Mode = 'M';
			HierarchyLevel = (int) workbenchInfo.Parameter;
			Exists = false;

			Store = Convert.ToDecimal(row[DetailedWorkbenchInfo.colStore] ?? 0m);
			Grade = (row[DetailedWorkbenchInfo.colGrade] ?? string.Empty).ToString();
			Market = (row[DetailedWorkbenchInfo.colMarket] ?? string.Empty).ToString();

			switch (workbenchInfo.Parameter)
			{
				case DetailedWorkbenchInfo.Parameters.ItemLevel:
				case DetailedWorkbenchInfo.Parameters.StyleLevel:
					HierarchyValue = string.Empty;
					break;

				case DetailedWorkbenchInfo.Parameters.ItemMarketLevel:
				case DetailedWorkbenchInfo.Parameters.StyleMarketLevel:
					HierarchyValue = Market;				
					break;

				case DetailedWorkbenchInfo.Parameters.ItemStoreLevel:
				case DetailedWorkbenchInfo.Parameters.StyleStoreLevel:
					HierarchyValue = Store.ToString();
					break;

				case DetailedWorkbenchInfo.Parameters.ItemGradeLevel:
				case DetailedWorkbenchInfo.Parameters.StyleGradeLevel:
					HierarchyValue = Grade;
					break;
			}

			if ((row[DetailedWorkbenchInfo.colWorkBench] ?? string.Empty).ToString() == Constants.kDaily)
				Member = "DLOAD";
			else
				Member = "WELOAD";

			StoreType = (row[DetailedWorkbenchInfo.colStoreType] ?? string.Empty).ToString();

			AllocationLever.Value = "N";
			CutOffLever.Value = 0;
			PatternLever.Value = 0;
			SmoothingLever.Value = 0;
			UpliftLever.Value = 0;
		}
	}

}
