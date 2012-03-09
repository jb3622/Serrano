/*
 * Author:  Marc Levine
 * Project: SRR
 * Date:    From March 2011
 * 
 * Description
 * Manually allocate stock to a store or stowaway store.
 */
using System;
using System.Data;
using System.Data.OleDb;
using Disney.iDash.Shared;

namespace Disney.iDash.SRR.BusinessLayer
{
    public class StowawayStoresInfo : BusinessBase
    {
		private DataTable _data = null;

		public const string colMemberName = "MBRNAM";
		public const string colStoreId = "SRSTR";
		public const string colItem = "ITEM";
		public const string colClass = "SRCLS";
		public const string colVendor = "SRVEN";
		public const string colStyle = "SRSTY";
		public const string colColour = "SRCLR";
		public const string colSize = "SRSIZ";
		public const string colUPC = "SRUPC";
		public const string colItemDescription = "ISDS";
		public const string colDistro = "IMLT";
		public const string colCurRingFenced = "RFQTY";
		public const string colNewRingFenced = "NRQTY";
		public const string colVoidQty = "VDQTY";
		public const string colReleaseQty = "RLQTY";

		public decimal DepartmentId { get; private set; }
		public decimal StoreId { get; private set; }

		public DataTable GetData(decimal departmentId, decimal storeId)
		{
			DepartmentId = departmentId;
			StoreId = storeId;

			_data = null;

			if (Factory.OpenConnection())
				try
				{
					var cmd = Factory.CreateCommand("DS887WS1", CommandType.StoredProcedure,
						Factory.CreateParameter("P_DEPT", departmentId, System.Data.OleDb.OleDbType.Decimal, 3, ParameterDirection.Input),
						Factory.CreateParameter("P_STORE", storeId, System.Data.OleDb.OleDbType.Decimal, 3, ParameterDirection.Input));

					_data = Factory.CreateTable(cmd);
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

		public bool Save()
		{
			var result = false;
			if (_data.GetChanges().Rows.Count > 0)
			{
				if (Factory.OpenConnection())
					try
					{
						var cmd = Factory.CreateCommand("DS887XS1", CommandType.StoredProcedure,
							Factory.CreateParameter("P_MBR", 0, OleDbType.Char, 10, ParameterDirection.Input),
							Factory.CreateParameter("P_ALCNUM", 0, OleDbType.Decimal, 5, 0, ParameterDirection.Input),
							Factory.CreateParameter("P_STORE", 0, OleDbType.Decimal, 3, 0, ParameterDirection.Input),
							Factory.CreateParameter("P_CLASS", 0, OleDbType.Decimal, 4, 0, ParameterDirection.Input),
							Factory.CreateParameter("P_VENDOR", 0, OleDbType.Decimal, 5, 0, ParameterDirection.Input),
							Factory.CreateParameter("P_STYLE", 0, OleDbType.Decimal, 4, 0, ParameterDirection.Input),
							Factory.CreateParameter("P_COLOUR", 0, OleDbType.Decimal, 3, 0, ParameterDirection.Input),
							Factory.CreateParameter("P_SIZE", 0, OleDbType.Decimal, 4, 0, ParameterDirection.Input),
							Factory.CreateParameter("P_QTY", 0, OleDbType.Decimal, 7, 0, ParameterDirection.Input),
							Factory.CreateParameter("P_STATUS", "V", OleDbType.Char, 1, ParameterDirection.Input),
							Factory.CreateParameter("P_RQSTYPE", "STOWAWAY", OleDbType.Char, 20, ParameterDirection.Input));

						foreach (DataRow row in _data.GetChanges().Rows)
						{
							var voidQty = (decimal)row[colVoidQty];
							var releaseQty = (decimal) row[colReleaseQty];
							var storeId = (decimal)row[colStoreId];
							var existingStoreId = (decimal)row[colStoreId];
							var curRingFenced = (decimal)row[colNewRingFenced];
							var newRingFenced = (decimal)row[colNewRingFenced];

							if (voidQty != 0)
								WriteRow(cmd, row, 'V', voidQty, existingStoreId);

							if (releaseQty != 0)
							{
								WriteRow(cmd, row, 'C', releaseQty, storeId);
								if (storeId != existingStoreId)
									WriteRow(cmd, row, 'V', releaseQty, existingStoreId);
							}

							if (newRingFenced != 0 && newRingFenced != curRingFenced)
								WriteRow(cmd, row, 'R', newRingFenced, storeId);
						}

						Factory.SubmitDSIPWRAP("S886QC0001", "DS886PC", cmd.Parameters["P_MBR"].Value.ToString(), string.Empty, 'N');
						result = true;
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
			return result;
		}

		public void WriteRow(OleDbCommand cmd, DataRow row, char status, decimal value, decimal storeId)
		{
			cmd.Parameters["P_MBR"].Value = row[colMemberName];
			cmd.Parameters["P_STORE"].Value = storeId;
			cmd.Parameters["P_CLASS"].Value = (decimal)row[colClass];
			cmd.Parameters["P_VENDOR"].Value = (decimal)row[colVendor];
			cmd.Parameters["P_STYLE"].Value = (decimal)row[colStyle];
			cmd.Parameters["P_COLOUR"].Value = (decimal)row[colColour];
			cmd.Parameters["P_SIZE"].Value = (decimal)row[colSize];
			cmd.Parameters["P_STATUS"].Value = status;
			cmd.Parameters["P_QTY"].Value = value;

			if (status == 'V')
				cmd.Parameters["P_RQSTYPE"].Value = "GIVE BACK DETAIL";
			else
				cmd.Parameters["P_RQSTYPE"].Value = "STOWAWAY";

			cmd.ExecuteNonQuery();

		}

    }
}
