/*
 * Author:  Marc Levine
 * Project: SRR
 * Date:    From March 2011
 * 
 * Description
 * StockInfo class.  Provides support functions for handling stock items
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace Disney.iDash.SRR.BusinessLayer
{
    public class StockInfo : BusinessBase
    {
        private DataTable _data = new DataTable();

        public DataTable Data 
        {
            get { return _data; }
            set { _data = value;  }        
        }

		public enum FieldNames
		{
			Department,
			MinDispQty,
			Class,
			Vendor,
			Style,
			Colour,
			Size,
			Store,
			DeleteFlag,
			Error,
			OpeningRateOfSale
		}
		
		/// <summary>
        /// Get a list of one or more stock items and their min display quantity.
        /// Specify one or more parameters.
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="stockItem"></param>
        /// <param name="UPC"></param>
        /// <param name="minDisplayQty"></param>
        /// <returns></returns>
        public DataTable GetMinDisplayQuantities(object departmentId, StockItem stockItem, decimal minDisplayQty)
        {
            return GetMinDisplayQuantities(departmentId, stockItem, null, minDisplayQty);
        }

        public DataTable GetMinDisplayQuantities(object departmentId, object UPC, decimal minDisplayQty)
        {
            return GetMinDisplayQuantities(departmentId, null, UPC, minDisplayQty);
        }

        private DataTable GetMinDisplayQuantities(object departmentId, StockItem stockItem, object UPC, decimal minDisplayQty)
        {
            if (base.Factory.OpenConnection())
            {
                var sql = Properties.Resources.SQLStockInfoSelect;
                var whereClause = new List<string>();

                if (departmentId != null)
                    whereClause.Add("ds.DIDPT=" + departmentId.ToString());

                if (stockItem != null && stockItem.HasValue)
                {
                    whereClause.Add("ds.DICLS=" + stockItem.Class.ToString());
                    if (stockItem.Vendor.HasValue)
                        whereClause.Add("ds.DIVEN=" + stockItem.Vendor.ToString());
                    if (stockItem.Style.HasValue)
                        whereClause.Add("ds.DISTY=" + stockItem.Style.ToString());
                }

                if (UPC != null && UPC.ToString() != string.Empty)
                    whereClause.Add("ip.IUPD='" + UPC.ToString() + "'");

                if (minDisplayQty > 0)
                    whereClause.Add("ds.DMDQT = " + minDisplayQty.ToString());

                if (whereClause.Count == 0)
                    sql = sql.Replace("<whereClause>", string.Empty);
                else
                    sql = sql.Replace("<whereClause>", "WHERE " + string.Join(" AND ", whereClause));

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

        /// <summary>
        /// Save changes to min display quantities.
        /// </summary>
        /// <returns></returns>
        public bool SaveMinDisplayQuantities()
        {
            var saved = false;
            if (IsDirty && base.Factory.OpenConnection())
            {
                var cmd = Factory.CreateCommand("DS892GS1", CommandType.StoredProcedure);
                try
                {
                    cmd.Parameters.Add(new OleDbParameter("P_CLASS", OleDbType.Decimal));
                    cmd.Parameters.Add(new OleDbParameter("P_VENDOR", OleDbType.Decimal));
                    cmd.Parameters.Add(new OleDbParameter("P_STYLE", OleDbType.Decimal));
                    cmd.Parameters.Add(new OleDbParameter("P_COLOUR", OleDbType.Decimal));
                    cmd.Parameters.Add(new OleDbParameter("P_SIZE", OleDbType.Decimal));
                    cmd.Parameters.Add(new OleDbParameter("P_QTY", OleDbType.Decimal));

                    //var updateParams = new Dictionary<string, object>
                    //{
                    //    {"<value>", 0},
                    //    {"<class>", 0},
                    //    {"<vendor>", 0},
                    //    {"<colour>", 0},
                    //    {"<style>", 0},
                    //    {"<size>", 0}
                    //};

                    foreach (DataRow row in _data.GetChanges().Rows)
                    {
                        cmd.Parameters["P_CLASS"].Value = Convert.ToDecimal(row[FieldNames.Class.ToString()]);
                        cmd.Parameters["P_VENDOR"].Value = Convert.ToDecimal(row[FieldNames.Vendor.ToString()]);
                        cmd.Parameters["P_STYLE"].Value = Convert.ToDecimal(row[FieldNames.Style.ToString()]);
                        cmd.Parameters["P_COLOUR"].Value = Convert.ToDecimal(row[FieldNames.Colour.ToString()]);
                        cmd.Parameters["P_SIZE"].Value = Convert.ToDecimal(row[FieldNames.Size.ToString()]); 
                        cmd.Parameters["P_QTY"].Value = Convert.ToDecimal(row[FieldNames.MinDispQty.ToString()]);

                        //updateParams["<value>"] = row[FieldNames.MinDispQty.ToString()].ToString();
                        //updateParams["<class>"] = row[FieldNames.Class.ToString()].ToString();
                        //updateParams["<vendor>"] = row[FieldNames.Vendor.ToString()].ToString();
                        //updateParams["<style>"] = row[FieldNames.Style.ToString()].ToString();
                        //updateParams["<colour>"] = row[FieldNames.Colour.ToString()].ToString();
                        //updateParams["<size>"] = row[FieldNames.Size.ToString()].ToString();

                        //var cmd = Factory.CreateCommand(Factory.ReplaceTokens(Properties.Resources.SQLStockInfoUpdate, updateParams));
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

        public DataTable GetItemDetails(decimal deptId, object storeId, StockItem stockItem)
        {
            if (base.Factory.OpenConnection())
            {
                var sql = Properties.Resources.SQLItemDetailsSelect;
                var whereClause = new List<string>();

                whereClause.Add("SRDEPT=" + deptId.ToString());

                if (storeId != null && (decimal) storeId != 0)
                    whereClause.Add("SRSTR=" + storeId.ToString());

                if (stockItem != null && stockItem.HasValue)
                {
                    whereClause.Add("SRCLS=" + stockItem.Class.ToString());
                    if (stockItem.Vendor.HasValue)
                        whereClause.Add("SRVEN=" + stockItem.Vendor.ToString());
                    if (stockItem.Style.HasValue)
                        whereClause.Add("SRSTY=" + stockItem.Style.ToString());
                    if (stockItem.Colour.HasValue)
                        whereClause.Add("SRCLR=" + stockItem.Colour.ToString());
                    if (stockItem.Size.HasValue)
                        whereClause.Add("SRSIZ=" + stockItem.Size.ToString());
                }

                if (whereClause.Count > 0)
                    sql += " WHERE " + string.Join(" AND ", whereClause);

				sql += " ORDER BY SRCLS, SRVEN, SRSTY, SRCLR, SRSIZ, SRSTR";

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

        /// <summary>
        /// Save changes to store/stock assignments
        /// </summary>
        /// <returns></returns>
        public bool SaveItemDetails()
        {
            var saved = false;

            if (IsDirty && base.Factory.OpenConnection())
                try
                {
					// Should only have modified rows as records are inserted directly.
                    var cmdUpdate = Factory.CreateCommand("DS886LS1", CommandType.StoredProcedure,
                        Factory.CreateParameter("pdept", null, OleDbType.Decimal, 3, ParameterDirection.Input),
                        Factory.CreateParameter("pcls", null, OleDbType.Decimal, 4, ParameterDirection.Input),
                        Factory.CreateParameter("pven", null, OleDbType.Decimal, 5, ParameterDirection.Input),
                        Factory.CreateParameter("psty", null, OleDbType.Decimal, 4, ParameterDirection.Input),
                        Factory.CreateParameter("pclr", null, OleDbType.Decimal, 3, ParameterDirection.Input),
                        Factory.CreateParameter("psiz", null, OleDbType.Decimal, 4, ParameterDirection.Input),
                        Factory.CreateParameter("pstr", null, OleDbType.Decimal, 3, ParameterDirection.Input),
                        Factory.CreateParameter("prosqty", null, OleDbType.Decimal, 6, ParameterDirection.Input),
                        Factory.CreateParameter("perr", null, OleDbType.Char, 128, ParameterDirection.InputOutput));

                    var cmdDelete = Factory.CreateCommand("DS886MS1", CommandType.StoredProcedure,
                        Factory.CreateParameter("pdept", null, OleDbType.Decimal, 3, ParameterDirection.Input),
                        Factory.CreateParameter("pcls", null, OleDbType.Decimal, 4, ParameterDirection.Input),
                        Factory.CreateParameter("pven", null, OleDbType.Decimal, 5, ParameterDirection.Input),
                        Factory.CreateParameter("psty", null, OleDbType.Decimal, 4, ParameterDirection.Input),
                        Factory.CreateParameter("pclr", null, OleDbType.Decimal, 3, ParameterDirection.Input),
                        Factory.CreateParameter("psiz", null, OleDbType.Decimal, 4, ParameterDirection.Input),
                        Factory.CreateParameter("pstr", null, OleDbType.Decimal, 3, ParameterDirection.Input),
                        Factory.CreateParameter("perr", null, OleDbType.Char, 128, ParameterDirection.InputOutput));

                    saved = true;
                    foreach (DataRow row in _data.Rows)
						if (row.RowState == DataRowState.Modified)
	                    {					
							var stockItem = new StockItem(row);
							if ((int)row[FieldNames.DeleteFlag.ToString()] == 1)
							{
								cmdDelete.Parameters["pdept"].Value = (decimal)row[FieldNames.Department.ToString()];
								cmdDelete.Parameters["pcls"].Value = (decimal)row[FieldNames.Class.ToString()];
								cmdDelete.Parameters["pven"].Value = (decimal)row[FieldNames.Vendor.ToString()];
								cmdDelete.Parameters["psty"].Value = (decimal)row[FieldNames.Style.ToString()];
								cmdDelete.Parameters["pclr"].Value = (decimal)row[FieldNames.Colour.ToString()];
								cmdDelete.Parameters["psiz"].Value = (decimal)row[FieldNames.Size.ToString()];
								cmdDelete.Parameters["pstr"].Value = (decimal)row[FieldNames.Store.ToString()];
                                cmdDelete.Parameters["perr"].Value = string.Empty;
								cmdDelete.ExecuteNonQuery();

								if (cmdDelete.Parameters["perr"].Value != System.DBNull.Value && cmdDelete.Parameters["perr"].Value.ToString() != string.Empty)
								{
									row[FieldNames.Error.ToString()] = cmdDelete.Parameters["perr"].Value.ToString();
									saved = false;
								}

							}
							else
							{
								cmdUpdate.Parameters["pdept"].Value = (decimal)row[FieldNames.Department.ToString()];
								cmdUpdate.Parameters["pcls"].Value = (decimal)row[FieldNames.Class.ToString()];
								cmdUpdate.Parameters["pven"].Value = (decimal)row[FieldNames.Vendor.ToString()];
								cmdUpdate.Parameters["psty"].Value = (decimal)row[FieldNames.Style.ToString()];
								cmdUpdate.Parameters["pclr"].Value = (decimal)row[FieldNames.Colour.ToString()];
								cmdUpdate.Parameters["psiz"].Value = (decimal)row[FieldNames.Size.ToString()];
								cmdUpdate.Parameters["pstr"].Value = (decimal)row[FieldNames.Store.ToString()];
								cmdUpdate.Parameters["prosqty"].Value = (decimal)row[FieldNames.OpeningRateOfSale.ToString()];
								cmdUpdate.Parameters["perr"].Value = string.Empty;
								cmdUpdate.ExecuteNonQuery();

								if (cmdUpdate.Parameters["perr"].Value != System.DBNull.Value && cmdUpdate.Parameters["perr"].Value.ToString() != string.Empty)
								{
									row[FieldNames.Error.ToString()] = cmdUpdate.Parameters["perr"].Value.ToString();
									saved = false;
								}

							}

                        }
                    
                }
                catch (Exception ex)
                {
                    ExceptionHandler.RaiseException(ex, "Save");
                }
                finally
                {
                    Factory.CloseConnection();
                }

            if (saved)
                IsDirty = false;

            return saved;
        }

		public bool InsertItemDetails(decimal deptId, StockItem stockItem, decimal storeId, decimal openingRateOfSale)
		{
			var inserted = false;
			if (Factory.OpenConnection())
				try
				{
					var cmdInsert = Factory.CreateCommand("DS886KS1", CommandType.StoredProcedure,
						Factory.CreateParameter("pdept", deptId, OleDbType.Decimal, 3, ParameterDirection.Input),
						Factory.CreateParameter("pcls", stockItem.Class, OleDbType.Decimal, 4, ParameterDirection.Input),
						Factory.CreateParameter("pven", stockItem.Vendor, OleDbType.Decimal, 5, ParameterDirection.Input),
						Factory.CreateParameter("psty", stockItem.Style, OleDbType.Decimal, 4, ParameterDirection.Input),
						Factory.CreateParameter("pclr", stockItem.Colour, OleDbType.Decimal, 3, ParameterDirection.Input),
						Factory.CreateParameter("psiz", stockItem.Size, OleDbType.Decimal, 4, ParameterDirection.Input),
						Factory.CreateParameter("pstr", storeId, OleDbType.Decimal, 3, ParameterDirection.Input),
						Factory.CreateParameter("prosqty", openingRateOfSale, OleDbType.Decimal, 6, ParameterDirection.Input),
						Factory.CreateParameter("perr", string.Empty, OleDbType.Char, 128, ParameterDirection.InputOutput));

					cmdInsert.ExecuteNonQuery();

					if (cmdInsert.Parameters["perr"].Value != System.DBNull.Value && cmdInsert.Parameters["perr"].Value.ToString() != string.Empty)
						ExceptionHandler.RaiseAlert(cmdInsert.Parameters["perr"].Value.ToString(), "Insert Item Details", Shared.ExceptionHandler.AlertType.Error);
					else
						inserted = true;

				}
				catch (Exception ex)
				{
					ExceptionHandler.RaiseException(ex, "Insert Item Details");
				}
				finally
				{
					Factory.CloseConnection();
				}
			return inserted;
		}
	}
}
