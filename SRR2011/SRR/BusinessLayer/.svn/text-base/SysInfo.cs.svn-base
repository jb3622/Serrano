/*
 * Author:  Marc Levine
 * Project: SRR
 * Date:    From March 2011
 * 
 * Description
 * Miscellaneous class to handle System related functions.  Currently it is used to:
 * 
 * Determine whether the system is available
 * Get any system messages
 * Get constants
 * Get/Set and Try to unlock single instance proces
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Disney.iDash.DataLayer;
using Disney.iDash.LocalData;
using Disney.iDash.Shared;

namespace Disney.iDash.SRR.BusinessLayer
{
    public class SysInfo
    {
        private DB2Factory _factory = new DB2Factory();
        public ExceptionHandler ExceptionHandler = new ExceptionHandler();

        public SysInfo()
        {
            _factory.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
            {
                ExceptionHandler.RaiseException(ex, extraInfo, terminateApplication);
            });
        }

        public bool IsSystemAvailable(string application = "")
        {
			var available = true;
			if (_factory.TransactionCount == 0)
			{
				var systemLock = GetSystemLock("IDASH", application.ToUpper());
				available = systemLock.LockStatus != SystemLock.LockTypes.Current;
			}
			return available;
        }

        public List<SysInfoMessage> GetMessages(List<string> systemTags = null)
        {
            var messages = new List<SysInfoMessage>();
            if (_factory.TransactionCount == 0 && _factory.OpenConnection())
                try
                {
                    var tokens = new Dictionary<string, object>
                    {
                        {"<networkId>", Session.User.NetworkId.ToUpper()},
                        {"<systemTags>", (systemTags == null ? "*ALL" :  string.Join("','", systemTags))},
                        {"<time>", DateTime.Now.TimeOfDay.ToString("hhmm")}
                    };

					var logSQL = _factory.LogSQL;
					_factory.LogSQL = false;
                    var table = _factory.CreateTable(_factory.ReplaceTokens(Properties.Resources.SQLSystemMessagesSelect, tokens));
					_factory.LogSQL = logSQL;

                    if (table != null)
                        messages = _factory.PopulateProperties<SysInfoMessage>(table, typeof(SysInfoMessage), new DB2Factory.CustomConversionDelegate(ConvertType));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.RaiseException(ex, "GetMessages");
                }
                finally
                {
                    _factory.CloseConnection();
                }

            return messages;
        }

		public string GetConstant(string key)
		{
			var constValue = string.Empty;
		
			if (_factory.OpenConnection())
				try
				{
					var cmd = _factory.CreateCommand("DS789HS1", CommandType.StoredProcedure,
						_factory.CreateParameter("P_KEY1", "SRR PHASE 2".PadRight(30), System.Data.OleDb.OleDbType.Char, 30, ParameterDirection.Input),
						_factory.CreateParameter("P_KEY2", key.ToUpper().PadRight(30), System.Data.OleDb.OleDbType.Char, 30, ParameterDirection.Input),
						_factory.CreateParameter("P_KEY3", new string(' ', 20), System.Data.OleDb.OleDbType.Char, 20, ParameterDirection.Input),
						_factory.CreateParameter("P_VAL1", new string(' ', 50), System.Data.OleDb.OleDbType.Char, 50, ParameterDirection.InputOutput),
						_factory.CreateParameter("P_VAL2", new string(' ', 90), System.Data.OleDb.OleDbType.Char, 90, ParameterDirection.InputOutput),
						_factory.CreateParameter("P_VAL3", 0m, System.Data.OleDb.OleDbType.Decimal, 20, 5, ParameterDirection.InputOutput),
						_factory.CreateParameter("P_VAL4", 0m, System.Data.OleDb.OleDbType.Decimal, 30, 5, ParameterDirection.InputOutput),
						_factory.CreateParameter("P_FND", "", System.Data.OleDb.OleDbType.Char, 1, ParameterDirection.InputOutput));
				
					cmd.ExecuteNonQuery();

					var p_fnd = cmd.Parameters["P_FND"];
					var p_val1 = cmd.Parameters["P_VAL1"];

					if (p_fnd != null && p_fnd.Value.ToString() == "Y" && p_val1 != null)
						constValue = p_val1.Value.ToString().Trim();
				
				}
				catch (Exception ex)
				{
					ExceptionHandler.RaiseException(ex, "GetConstant");
				}
				finally
				{
					_factory.CloseConnection();
				}

			return constValue;
		}

		/// <summary>
		/// GetSystemLock will enquire whether a lock for area (and optionally subArea) is currently set by the current user.
		/// </summary>
		/// <param name="area"></param>
		/// <param name="subArea"></param>
		/// <returns></returns>
		public SystemLock GetSystemLock(string area, string subArea = "")
		{
			var systemLock = new SystemLock(area, subArea);

			if (_factory.OpenConnection())
				try
				{
					var cmd = _factory.CreateCommand("DS887TS1", CommandType.StoredProcedure,
						_factory.CreateParameter("P_ACTION", "E", System.Data.OleDb.OleDbType.Char, 1, ParameterDirection.Input),
						_factory.CreateParameter("P_AREA", systemLock.Area, System.Data.OleDb.OleDbType.Char, 20, ParameterDirection.Input),
						_factory.CreateParameter("P_SUBAREA", systemLock.SubArea, System.Data.OleDb.OleDbType.Char, 20, ParameterDirection.InputOutput),
						_factory.CreateParameter("P_USERID", systemLock.LockedBy, System.Data.OleDb.OleDbType.Char, 50, ParameterDirection.InputOutput),
						_factory.CreateParameter("P_LOCKSTS", " ", System.Data.OleDb.OleDbType.Char, 1, ParameterDirection.InputOutput));

					cmd.ExecuteNonQuery();

					systemLock.SubArea = (cmd.Parameters["P_SUBAREA"].Value ?? string.Empty).ToString().ToUpper();
					systemLock.LockedBy = (cmd.Parameters["P_USERID"].Value ?? string.Empty).ToString().ToUpper();
					systemLock.LockStatus = systemLock.GetLockStatus((cmd.Parameters["P_LOCKSTS"].Value ?? string.Empty).ToString().ToUpper());
				}
				catch (Exception ex)
				{
					ExceptionHandler.RaiseException(ex, "GetSystemLock");
				}
				finally
				{
					_factory.CloseConnection();
				}

			return systemLock;
		}

		/// <summary>
		/// Attempt to set a system lock for the current area [and subArea] for the current / specific user.
		/// </summary>
		/// <param name="area"></param>
		/// <param name="subArea"></param>
		/// <param name="userId"></param>
		/// <returns></returns>
		public SystemLock SetSystemLock(string area, string subArea = "", string userId = "")
		{
			var systemLock = new SystemLock(area, subArea, userId);

			if (_factory.OpenConnection())
				try
				{
					var cmd = _factory.CreateCommand("DS887TS1", CommandType.StoredProcedure,
						_factory.CreateParameter("P_ACTION", "L", System.Data.OleDb.OleDbType.Char, 1, ParameterDirection.Input),
						_factory.CreateParameter("P_AREA", systemLock.Area, System.Data.OleDb.OleDbType.Char, 20, ParameterDirection.Input),
						_factory.CreateParameter("P_SUBAREA", systemLock.SubArea, System.Data.OleDb.OleDbType.Char, 20, ParameterDirection.InputOutput),
						_factory.CreateParameter("P_USERID", systemLock.LockedBy, System.Data.OleDb.OleDbType.Char, 50, ParameterDirection.InputOutput),
						_factory.CreateParameter("P_LOCKSTS", " ", System.Data.OleDb.OleDbType.Char, 1, ParameterDirection.InputOutput));

					cmd.ExecuteNonQuery();

					systemLock.SubArea = (cmd.Parameters["P_SUBAREA"].Value ?? string.Empty).ToString().ToUpper();
					systemLock.LockedBy = (cmd.Parameters["P_USERID"].Value ?? string.Empty).ToString().ToUpper();
					systemLock.LockStatus = systemLock.GetLockStatus((cmd.Parameters["P_LOCKSTS"].Value ?? string.Empty).ToString().ToUpper());

				}
				catch (Exception ex)
				{
					ExceptionHandler.RaiseException(ex, "SetSystemLock");
				}
				finally
				{
					_factory.CloseConnection();
				}

			return systemLock;
		}

		/// <summary>
		/// Unlock a system lock.
		/// </summary>
		/// <param name="systemLock"></param>
		/// <returns></returns>
		public SystemLock TryUnlock(SystemLock systemLock)
		{
			var result = new SystemLock();
			if (_factory.OpenConnection())
				try
				{
					var cmd = _factory.CreateCommand("DS887TS1", CommandType.StoredProcedure,
						_factory.CreateParameter("P_ACTION", "U", System.Data.OleDb.OleDbType.Char, 1, ParameterDirection.Input),
						_factory.CreateParameter("P_AREA", systemLock.Area, System.Data.OleDb.OleDbType.Char, 20, ParameterDirection.Input),
						_factory.CreateParameter("P_SUBAREA", systemLock.SubArea, System.Data.OleDb.OleDbType.Char, 20, ParameterDirection.InputOutput),
						_factory.CreateParameter("P_USERID", systemLock.LockedBy, System.Data.OleDb.OleDbType.Char, 50, ParameterDirection.InputOutput),
						_factory.CreateParameter("P_LOCKSTS", " ", System.Data.OleDb.OleDbType.Char, 1, ParameterDirection.InputOutput));

					cmd.ExecuteNonQuery();

					result.Area = systemLock.Area;
					result.SubArea = (cmd.Parameters["P_SUBAREA"].Value ?? string.Empty).ToString().ToUpper(); 
					result.LockedBy = (cmd.Parameters["P_USERID"].Value ?? string.Empty).ToString().ToUpper();
					result.LockStatus = result.GetLockStatus((cmd.Parameters["P_LOCKSTS"].Value ?? string.Empty).ToString().ToUpper());

				}
				catch (Exception ex)
				{
					ExceptionHandler.RaiseException(ex, "TryUnlock");
				}
				finally
				{
					_factory.CloseConnection();
				}

			return result;
		}

        private object ConvertType(PropertyInfo property, object value)
        {
            object result = null;
            if (property.PropertyType == typeof(Boolean) && value.GetType() == typeof(string))
                result = value != null && (value.ToString().Contains('1') || value.ToString().ToUpper().Contains('Y'));
            return result;
        }

    }

}
