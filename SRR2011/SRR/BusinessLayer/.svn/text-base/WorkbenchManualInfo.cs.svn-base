/*
 * Author:  Marc Levine
 * Project: SRR
 * Date:    From March 2011
 * 
 * Description
 * Manually allocate stock to a store or stowaway store.
 */
using System;
using System.Collections.Generic;
using System.Data;
using Disney.iDash.LocalData;
using Disney.iDash.Shared;
using System.Data.OleDb;

namespace Disney.iDash.SRR.BusinessLayer
{
    public class WorkbenchManualInfo : BusinessBase
    {
        private DataTable _packDetails = new DataTable();
        private DataTable _allocations = new DataTable();

        private decimal _allocated = 0;
        private decimal _holding = 0;

        public enum Modes
        {
            Manual,
            Stowaway,
            Workbench
        }

        // pack details
        public const string colUnits = "UNITS";
        public const string colUPC = "UPC";
        public const string colDescription = "DESCRIPTION";
        public const string colDepartment = "DEPARTMENT";
        public const string colClass = "CLASS";
        public const string colVendor = "VENDOR";
        public const string colStyle = "STYLE";
        public const string colColour = "COLOUR";
        public const string colSize = "SIZE";

        // Allocations
        public const string colMarket = "MARKET";
        public const string colStore = "STORE";
        public const string colStoreName = "STORENAME";
        public const string colStoreGrade = "STOREGRADE";
        public const string colQuantity = "QUANTITY";
        public const string colReleaseQty = "RELEASEQTY";
        public const string colAdditionalUnits = "ADDITIONALUNITS";
        public const string colReleaseStore = "RELEASESTORE";
        public const string colContinentalSeq = "CONTINENTALSEQ";
        public const string colAllocated = "ALLOCATED";
        public const string colLocked = "LOCKED";

        public StockItem StockItem { get; private set; }
        public string LockedBy { get; private set; }
        public Modes Mode { get; set; }
        public string Member { get; set; }
        public string Workbench { get; set; }

        public WorkbenchManualInfo()
        {
            StockItem = new StockItem();
        }

        #region Public methods & properties
        //-----------------------------------------------------------------------------------------

        public bool OverAllocationAlert
        {
            get { return RegUtils.GetCustomSetting("OverAllocationAlert", false); }
            set { RegUtils.SetCustomSetting("OverAllocationAlert", value); }
        }

        /// <summary>
        /// Validate a stock item UPC.  It must exist and be defined as a pack item
        /// </summary>
        /// <param name="upc"></param>
        /// <returns></returns>
        public bool IsValidPackItem(string upc)
        {
            return _IsValidPackItem(Properties.Resources.SQLPackItemValidate + " AND IUPD='" + upc + "'");
        }

        /// <summary>
        /// Validate a stock item using class, vendor, style etc.  It must exist and be defined as a pack item.
        /// </summary>
        /// <param name="stockItem"></param>
        /// <returns></returns>
        public bool IsValidPackItem(StockItem stockItem)
        {
            return _IsValidPackItem(SetParameters(stockItem, Properties.Resources.SQLPackItemValidate +
                " AND h.ICLS=<class> AND h.IVEN=<vendor> AND h.ISTY=<style> AND h.ICLR=<colour> AND h.ISIZ=<size>"));
        }

        /// <summary>
        /// Get data for pack details
        /// </summary>
        /// <param name="stockItem"></param>
        /// <returns></returns>
        public DataTable GetPackDetails()
        {
            _packDetails = null;
            if (StockItem.HasValue && Factory.OpenConnection())
                try
                {
                    var sql = SetParameters(StockItem, Properties.Resources.SQLPackItemGetPackDetails);
                    _packDetails = Factory.CreateTable(sql);
                }
                catch (Exception ex)
                {
                    ExceptionHandler.RaiseException(ex, "GetItemData" + StockItem.ToString());
                }
                finally
                {
                    Factory.CloseConnection();
                }
            return _packDetails;
        }

        /// <summary>
        /// Get DC stock holding
        /// </summary>
        /// <param name="stockItem"></param>
        /// <returns></returns>
        public decimal GetDCStockHolding()
        {
            _holding = 0;
            if (StockItem.HasValue && Factory.OpenConnection())
                try
                {
                    var cmd = Factory.CreateCommand("DS887AS1", CommandType.StoredProcedure,
                        Factory.CreateParameter("P_CLASS", StockItem.Class, System.Data.OleDb.OleDbType.Decimal, 4, ParameterDirection.Input),
                        Factory.CreateParameter("P_VENDOR", StockItem.Vendor, System.Data.OleDb.OleDbType.Decimal, 5, ParameterDirection.Input),
                        Factory.CreateParameter("P_STYLE", StockItem.Style, System.Data.OleDb.OleDbType.Decimal, 4, ParameterDirection.Input),
                        Factory.CreateParameter("P_COLOUR", StockItem.Colour, System.Data.OleDb.OleDbType.Decimal, 3, ParameterDirection.Input),
                        Factory.CreateParameter("P_SIZE", StockItem.Size, System.Data.OleDb.OleDbType.Decimal, 4, ParameterDirection.Input),
                        Factory.CreateParameter("P_STKHLDG", 0, System.Data.OleDb.OleDbType.Decimal, 5, ParameterDirection.InputOutput));

                    cmd.ExecuteNonQuery();
                    _holding = (decimal)(cmd.Parameters["P_STKHLDG"].Value ?? 0m);
                }
                catch (Exception ex)
                {
                    ExceptionHandler.RaiseException(ex, "GetDCStockHolding" + StockItem.ToString());
                }
                finally
                {
                    Factory.CloseConnection();
                }
            return _holding;
        }

        public decimal GetAdditionalUnits(decimal store, decimal releaseQty)
        {
            var additionalUnits = 0m;
            if (Factory.OpenConnection())
                try
                {
                    var cmd = Factory.CreateCommand("DS887CS1", CommandType.StoredProcedure,
                        Factory.CreateParameter("P_CLASS", StockItem.Class, System.Data.OleDb.OleDbType.Decimal, 4, ParameterDirection.Input),
                        Factory.CreateParameter("P_VENDOR", StockItem.Vendor, System.Data.OleDb.OleDbType.Decimal, 5, ParameterDirection.Input),
                        Factory.CreateParameter("P_STYLE", StockItem.Style, System.Data.OleDb.OleDbType.Decimal, 4, ParameterDirection.Input),
                        Factory.CreateParameter("P_COLOUR", StockItem.Colour, System.Data.OleDb.OleDbType.Decimal, 3, ParameterDirection.Input),
                        Factory.CreateParameter("P_SIZE", StockItem.Size, System.Data.OleDb.OleDbType.Decimal, 4, ParameterDirection.Input),
                        Factory.CreateParameter("P_STORE", store, System.Data.OleDb.OleDbType.Decimal, 3, ParameterDirection.Input),
                        Factory.CreateParameter("P_RLSQTY", releaseQty, System.Data.OleDb.OleDbType.Decimal, 5, ParameterDirection.Input),
                        Factory.CreateParameter("P_DCHOLDING", this.Holding, System.Data.OleDb.OleDbType.Decimal, 4, ParameterDirection.Input),
                        Factory.CreateParameter("P_MEMBER", this.Member, System.Data.OleDb.OleDbType.Char, 10, ParameterDirection.Input));

                    Factory.ListParameters(cmd.Parameters);
                    var table = Factory.CreateTable(cmd);
                    if (table != null && table.Rows.Count > 0)
                        additionalUnits = Convert.ToDecimal(table.Rows[0]["AdditionalUnits"] ?? 0m);
                }
                catch (Exception ex)
                {
                    ExceptionHandler.RaiseException(ex, "GetAllocations" + StockItem.ToString());
                }
                finally
                {
                    Factory.CloseConnection();
                }
            return additionalUnits;
        }

        /// <summary>
        /// Get Allocations for the stock by store.
        /// </summary>
        /// <param name="stockItem"></param>
        /// <returns></returns>
        public DataTable GetAllocations()
        {
            _allocations = null;
            if (StockItem.HasValue && Factory.OpenConnection())
                try
                {
                    switch (Mode)
                    {
                        case Modes.Manual:
                            {
                                var sql = SetParameters(StockItem, Properties.Resources.SQLPackItemGetAllocations);
                                _allocations = Factory.CreateTable(sql);
                            }
                            break;

                        case Modes.Workbench:
                            {
                                var cmd = Factory.CreateCommand("DS887CS1", CommandType.StoredProcedure,
                                    Factory.CreateParameter("P_CLASS", StockItem.Class, System.Data.OleDb.OleDbType.Decimal, 4, ParameterDirection.Input),
                                    Factory.CreateParameter("P_VENDOR", StockItem.Vendor, System.Data.OleDb.OleDbType.Decimal, 5, ParameterDirection.Input),
                                    Factory.CreateParameter("P_STYLE", StockItem.Style, System.Data.OleDb.OleDbType.Decimal, 4, ParameterDirection.Input),
                                    Factory.CreateParameter("P_COLOUR", StockItem.Colour, System.Data.OleDb.OleDbType.Decimal, 3, ParameterDirection.Input),
                                    Factory.CreateParameter("P_SIZE", StockItem.Size, System.Data.OleDb.OleDbType.Decimal, 4, ParameterDirection.Input),
                                    Factory.CreateParameter("P_STORE", 0m, System.Data.OleDb.OleDbType.Decimal, 3, ParameterDirection.Input),
                                    Factory.CreateParameter("P_RLSQTY", 0m, System.Data.OleDb.OleDbType.Decimal, 5, ParameterDirection.Input),
                                    Factory.CreateParameter("P_DCHOLDING", this.Holding, System.Data.OleDb.OleDbType.Decimal, 4, ParameterDirection.Input),
                                    Factory.CreateParameter("P_MEMBER", this.Member, System.Data.OleDb.OleDbType.Char, 10, ParameterDirection.Input));
                                Factory.ListParameters(cmd.Parameters);
                                _allocations = Factory.CreateTable(cmd);
                            }
                            break;

                        case Modes.Stowaway:
                            {
                                var sql = SetParameters(StockItem, Properties.Resources.SQLPackItemGetStowawayAllocations);
                                _allocations = Factory.CreateTable(sql);
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ExceptionHandler.RaiseException(ex, "GetAllocations" + StockItem.ToString());
                }
                finally
                {
                    Factory.CloseConnection();
                }
            return _allocations;
        }

        private string GetOption1()
        {
            return this.Member.PadRight(10) + (this.Workbench == "Daily" ? "DLOAD" : "WELOAD").PadRight(10) + StockItem.ToString(true);
        }

        public bool CalculateMasterQuantities(string workbench, string member)
        {
            var result = false;

            if (StockItem.HasValue)
            {
                Workbench = workbench;
                Member = member;
                result = Factory.CallDSIPWRAP("S885JC0001", "DS885JC", GetOption1(), string.Empty, 'N');
            }
            return result;
        }

        /// <summary>
        /// Returns true if the specified item is locked
        /// </summary>
        /// <param name="stockItem"></param>
        /// <returns></returns>
        /// 
        public bool IsItemLocked()
        {
            return IsItemLocked(this.StockItem);
        }

        public bool IsItemLocked(StockItem stockItem)
        {
            var result = false;
            if (StockItem.HasValue && Factory.OpenConnection())
                try
                {
                    var sql = SetParameters(stockItem, Properties.Resources.SQLPackItemCheck);
                    var cmd = Factory.CreateCommand(sql);
                    var temp = cmd.ExecuteScalar();
                    if (temp == null)
                        LockedBy = string.Empty;
                    else
                        LockedBy = temp.ToString();
                    result = LockedBy != string.Empty;
                }
                catch (Exception ex)
                {
                    ExceptionHandler.RaiseException(ex, "IsItemLocked");
                }
                finally
                {
                    Factory.CloseConnection();
                }

            return result;
        }

        /// <summary>
        /// Lock a pack item to prevent other users from allocating
        /// </summary>
        /// <returns></returns>
        public bool LockItem()
        {
            var result = false;

            if (StockItem.HasValue && Factory.OpenConnection())
            {
                var cmd = Factory.CreateCommand("DS890GS1", CommandType.StoredProcedure);
                try
                {

                    cmd.Parameters.Add(new OleDbParameter("P_CLASS", StockItem.Class));
                    cmd.Parameters.Add(new OleDbParameter("P_VENDOR", StockItem.Vendor));
                    cmd.Parameters.Add(new OleDbParameter("P_STYLE", StockItem.Style));
                    cmd.Parameters.Add(new OleDbParameter("P_COLOUR", StockItem.Colour));
                    cmd.Parameters.Add(new OleDbParameter("P_SIZE", StockItem.Size));
                    cmd.Parameters.Add(new OleDbParameter("P_USER", Session.User.NetworkId.ToUpper().Left(4)));
                    cmd.Parameters.Add(new OleDbParameter("P_JOB", Session.User.NetworkId.ToUpper()));
                    cmd.Parameters.Add(new OleDbParameter("P_DTTM", System.DateTime.Now.ToString("yyyyMMddHHmm")));
                    cmd.ExecuteNonQuery();
                    result = true;

                }
                catch (Exception ex)
                {
                    ExceptionHandler.RaiseException(ex, "LockItem");
                }
                finally
                {
                    Factory.CloseConnection();
                }
            }
            return result;
        }

        /// <summary>
        /// Unlock and release a pack item
        /// </summary>
        /// <returns></returns>
        public bool UnlockItem()
        {
            var result = false;
            if (StockItem.HasValue && Factory.OpenConnection())
                try
                {
                    var sql = SetParameters(StockItem, Properties.Resources.SQLPackItemUnlock);
                    var cmd = Factory.CreateCommand(sql);
                    result = cmd.ExecuteNonQuery() > 0;
                    LockedBy = string.Empty;
                }
                catch (Exception ex)
                {
                    ExceptionHandler.RaiseException(ex, "UnlockItem");
                }
                finally
                {
                    Factory.CloseConnection();
                }

            return result;
        }


        /// <summary>
        /// Save allocations to a pack item
        /// </summary>
        /// <param name="allocations"></param>
        /// <returns></returns>
        public bool Save()
        {
            var result = false;

            if (OverAllocated)
                ExceptionHandler.RaiseAlert("Cannot apply changes, stock overallocated", "Apply", Shared.ExceptionHandler.AlertType.Error);
            else
            {
                if (_allocations.GetChanges() != null && Factory.OpenConnection())
                {
                    switch (this.Mode)
                    {
                        case Modes.Manual:
                        case Modes.Stowaway:
                            result = SaveManual();
                            break;

                        case Modes.Workbench:
                            result = SaveWorkbench();
                            break;
                    }
                }
            }
            return result;
        }

        private bool SaveManual()
        {
            var result = false;
            var parameters = new Dictionary<string, object>
            {
                {"<departmentId>", _packDetails.Rows[0]["DEPARTMENT"].ToString()},
                {"<class>", StockItem.Class},
                {"<vendor>", StockItem.Vendor}, 
                {"<style>", StockItem.Style},
                {"<colour>", StockItem.Colour},
                {"<size>", StockItem.Size},
                {"<upc>", StockItem.UPC},
                {"<store>", null},
                {"<storeName>", null},
                {"<market>", null},
                {"<storeGrade>", null}, 
                {"<releaseQty>", null},
                {"<dateTime>", System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")},
                {"<userName>", Session.User.NetworkId.ToUpper()}
            };

            try
            {
                // remove any existing entries in the table for the user and stock item.
                var cmdDelete = Factory.CreateCommand(Properties.Resources.SQLPackItemDeleteAllocations.Replace("<upc>", StockItem.UPC).Replace("<userName", Session.User.NetworkId.ToUpper()));
                cmdDelete.ExecuteNonQuery();

                foreach (DataRow row in _allocations.GetChanges().Rows)
                {
                    var releaseQty = Convert.ToDecimal(row[colReleaseQty]);
                    if (releaseQty > 0)
                    {
                        parameters["<store>"] = row[colStore].ToString();
                        parameters["<storeName>"] = row[colStoreName].ToString().Replace("'", "''");
                        parameters["<market>"] = row[colMarket].ToString().Replace("'", "''");
                        parameters["<storeGrade>"] = row[colStoreGrade].ToString().Replace("'", "''");
                        parameters["<releaseQty>"] = releaseQty;
                        var cmdInsert = Factory.CreateCommand(Factory.ReplaceTokens(Properties.Resources.SQLPackItemUpdateAllocations, parameters));
                        cmdInsert.ExecuteNonQuery();
                    }
                }

                result = Factory.CallDSIPWRAP("S885EC0001", "DS885EC", new string(' ', 20) + StockItem.ToString(true) + "MAN");

                this.UnlockItem();
            }
            catch (Exception ex)
            {
                ExceptionHandler.RaiseException(ex, "Apply");
            }
            return result;
        }

        private bool SaveWorkbench()
        {
            var result = false;
            try
            {

                var cmd = Factory.CreateCommand("DS887DS1", CommandType.StoredProcedure,
                    Factory.CreateParameter("P_CLASS", StockItem.Class, System.Data.OleDb.OleDbType.Decimal, 4, ParameterDirection.Input),
                    Factory.CreateParameter("P_VENDOR", StockItem.Vendor, System.Data.OleDb.OleDbType.Decimal, 5, ParameterDirection.Input),
                    Factory.CreateParameter("P_STYLE", StockItem.Style, System.Data.OleDb.OleDbType.Decimal, 4, ParameterDirection.Input),
                    Factory.CreateParameter("P_COLOUR", StockItem.Colour, System.Data.OleDb.OleDbType.Decimal, 3, ParameterDirection.Input),
                    Factory.CreateParameter("P_SIZE", StockItem.Size, System.Data.OleDb.OleDbType.Decimal, 4, ParameterDirection.Input),
                    Factory.CreateParameter("P_STORE", 0m, System.Data.OleDb.OleDbType.Decimal, 3, ParameterDirection.Input),
                    Factory.CreateParameter("P_RLSSTORE", 'N', System.Data.OleDb.OleDbType.Char, 1, ParameterDirection.Input),
                    Factory.CreateParameter("P_USER", Session.User.NetworkId.ToUpper(), System.Data.OleDb.OleDbType.Char, 10, ParameterDirection.Input));

                foreach (DataRow row in _allocations.GetChanges().Rows)
                {
                    cmd.Parameters["P_RLSSTORE"].Value = (Convert.ToDecimal(row[colReleaseStore]) == 1 ? "Y" : "N");
                    cmd.Parameters["P_STORE"].Value = Convert.ToDecimal(row[colStore]);
                    cmd.ExecuteNonQuery();
                }

                result = Factory.CallDSIPWRAP("S885FC0001", "DS885FC", GetOption1());
                if (result)
                    result = Factory.CallDSIPWRAP("S885EC0001", "DS885EC", GetOption1() + "SRR");
            }
            catch (Exception ex)
            {
                ExceptionHandler.RaiseException(ex, "Apply");

            }
            return result;
        }

        public bool Cancel()
        {
            var result = true;

            if (result)
                this.UnlockItem();

            return result;
        }

        /// <summary>
        /// Get / set the DC holding value
        /// </summary>
        public decimal Holding
        {
            get { return _holding; }
            set
            {
                _holding = value;
                _allocated = 0;
            }
        }

        /// <summary>
        /// Reset the allocated value (to zero)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public decimal SetAllocated(decimal value)
        {
            _allocated = value;
            return _allocated;
        }

        /// <summary>
        /// Calculated the total allocated values by summing the RELEASEQTY column of changed values.
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public decimal CalculateAllocated()
        {
            _allocated = 0;
            if (_allocations != null)
                foreach (DataRow row in _allocations.Rows)
                    _allocated += Convert.ToDecimal(row[colReleaseQty]);
            return _allocated;
        }

        /// <summary>
        /// Return the amount of stock in the DC less the amount allocated.
        /// </summary>
        public decimal Remaining
        {
            get { return Holding - _allocated; }
        }

        /// <summary>
        /// Returns TRUE if the user has allocated more stock than is available in the DC.
        /// </summary>
        public bool OverAllocated
        {
            get { return Remaining < 0; }
        }

        //-----------------------------------------------------------------------------------------
        #endregion

        #region Private methods
        //-----------------------------------------------------------------------------------------
        private bool _IsValidPackItem(string sql)
        {
            var result = false;
            StockItem.Clear();
            if (Factory.OpenConnection())
                try
                {
                    var table = Factory.CreateTable(sql);
                    if (table != null && table.Rows.Count > 0)
                    {
                        StockItem.Class = (decimal)table.Rows[0][colClass];
                        StockItem.Vendor = (decimal)table.Rows[0][colVendor];
                        StockItem.Style = (decimal)table.Rows[0][colStyle];
                        StockItem.Colour = (decimal)table.Rows[0][colColour];
                        StockItem.Size = (decimal)table.Rows[0][colSize];
                        StockItem.UPC = table.Rows[0][colUPC].ToString();
                        LockedBy = string.Empty;
                        result = true;
                    }
                }
                catch (Exception ex)
                {
                    ExceptionHandler.RaiseException(ex, "_IsValidPackItem");
                }
                finally
                {
                    Factory.CloseConnection();
                }

            return result;
        }

        private string SetParameters(StockItem stockItem, string sql)
        {
            return sql.Replace("<class>", stockItem.Class.GetValueOrDefault(0).ToString())
                .Replace("<vendor>", stockItem.Vendor.GetValueOrDefault(0).ToString())
                .Replace("<style>", stockItem.Style.GetValueOrDefault(0).ToString())
                .Replace("<colour>", stockItem.Colour.GetValueOrDefault(0).ToString())
                .Replace("<size>", stockItem.Size.GetValueOrDefault(0).ToString());

        }
        //-----------------------------------------------------------------------------------------
        #endregion
    }
}
