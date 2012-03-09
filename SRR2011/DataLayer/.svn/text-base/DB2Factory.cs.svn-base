/*
 * Author:  Marc Levine
 * Project: SRR
 * Date:    From March 2011
 * 
 * Description
 * This class provides connectivity and data functionality to DB2 / AS400 iSeries as OleDb Objects
 *  
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Disney.iDash.Shared;
using System.Text.RegularExpressions;

namespace Disney.iDash.DataLayer
{
    public sealed class DB2Factory
    {
        private const string kTempPath = "\\iDash\\";

		private static OleDbConnection _con;
		private static List<OleDbTransaction> _transactions = new List<OleDbTransaction>();
		private static OleDbTransaction _transaction = null;
		private ConnectionState _previousConnectionState = ConnectionState.Closed;

        public ExceptionHandler ExceptionHandler = new ExceptionHandler();
		public TimeSpan ExecutionTime { get; private set; }

		public DB2Factory()
		{
			if (_con == null)
			{
				_con = new OleDbConnection();
				_con.StateChange += ((sender, e) =>
				{
					if (System.Diagnostics.Debugger.IsAttached)
						Console.WriteLine("Connection state {0} at {1}", e.CurrentState, DateTime.Now);
				});
			}
		}

		public bool LogSQL
		{
			get { return Properties.Settings.Default.LogSQL; }
			set { Properties.Settings.Default["LogSQL"] = value; }
		}

        /// <summary>
        /// Build a connection string from the parameters supplied.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="hostName"></param>
        /// <param name="database"></param>
        /// <param name="libraryList"></param>
        /// <param name="db2NamingConvention"></param>
        /// <param name="blockSize"></param>
        /// <returns></returns>
        public string BuildConnectionString(string username, string password, string hostName, string database, string libraryList, bool db2NamingConvention=true, int blockSize=6000)
        {
            var sb = new OleDbConnectionStringBuilder();
            sb.Add("Provider", "IBMDASQL.DataSource.1");
            sb.Add("Data source", hostName);
            sb.Add("Password", password);
            sb.Add("User ID", username);
            sb.Add("Persist Security Info", true);
            sb.Add("Initial Catalog", database);
			sb.Add("Block Fetch", true);
            sb.Add("Convert Date Time To Char", "FALSE");
            sb.Add("Data Compression", true);
            sb.Add("Library List", libraryList);
            sb.Add("Naming Convention", (db2NamingConvention ? 1 : 0));
            sb.Add("Block Size", blockSize);
            return sb.ConnectionString;
        }

        /// <summary>
        /// Get/Set the connection string as defined in the app.config file.
        /// </summary>
        public string ConnectionString
        {
            set { Properties.Settings.Default["ConnectionString"] = value;}
            get { return Properties.Settings.Default["ConnectionString"].ToString(); }
        }

        /// <summary>
        /// Test a connection to the database.  This is used when validating a user login.  If a connection
        /// can be established the user has entered the correct Id and Password.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="hostName"></param>
        /// <param name="database"></param>
        /// <param name="libraryList"></param>
        /// <returns></returns>
        public bool TestConnection(string username, string password, string hostName, string database, string libraryList)
        {
            var result = false;

            if (_con.State == ConnectionState.Open)
                _con.Close();
            
            try
            {
                _con.ConnectionString = BuildConnectionString(username, password, hostName, database, libraryList);
                _con.Open();
                ConnectionString = _con.ConnectionString;
                result = true;
            }
            catch (OleDbException ex)
            {
                ExceptionHandler.SetException(ex);
            }
            catch (Exception ex)
            {
                ExceptionHandler.SetException(ex);               
            }
            finally
            {
                CloseConnection();
            }

            return result;
        }

        /// <summary>
        /// Create a transaction
        /// </summary>
        /// <returns></returns>
        public OleDbTransaction BeginTransaction()
        {
			_transaction = null;
			if (_con.State == ConnectionState.Open)
			{
				_transaction = _con.BeginTransaction();
				_transactions.Add(_transaction);
			}
			return _transaction;
        }

		public int TransactionCount
		{
			get
			{
				var qry = from t in _transactions
						  where t != null && t.Connection != null
						  select t;

				var activeTransactions = qry.Count();
				if (System.Diagnostics.Debugger.IsAttached)
					Console.WriteLine("Total {0} Active {1} transactions", _transactions.Count, activeTransactions);
				return activeTransactions;
			}
		}

        /// <summary>
        /// Open a preconfigured (default) connection
        /// </summary>
        /// <returns></returns>
        public bool OpenConnection()
        {
            return OpenConnection(Properties.Settings.Default.ConnectionString);
        }
      
        /// <summary>
        /// Open a connection with a specified connnection string.  
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public bool OpenConnection(string connectionString)
        {
            var result = true;

            if (_con.ConnectionString != connectionString)
                _con.Close();

			_previousConnectionState = _con.State;
            
            if (_con.State == ConnectionState.Closed)
            {
                try
                {
                    _con.ConnectionString = connectionString;
                    _con.Open();
                }
                catch (OleDbException ex)
                {
                    ExceptionHandler.RaiseException(ex, "OpenConnection(" + connectionString + ")");
                    result = false;
                }
                catch (Exception ex)
                {
                    ExceptionHandler.RaiseException(ex, "OpenConnection(" + connectionString + ")");
                    result = false;
                }
            }
            return result;
        }

        /// <summary>
        /// Close a connection
        /// </summary>
        public void CloseConnection()
        {
            if (_previousConnectionState == ConnectionState.Closed && _con != null && _con.State == System.Data.ConnectionState.Open)
                _con.Close();
        }

        /// <summary>
        /// Create and return an active command based on the results of a SQL string
        /// </summary>
        /// <param name="commandString"></param>
        /// <returns></returns>
        public OleDbCommand CreateCommand(string commandString)
        {
            return CreateCommand(commandString, CommandType.Text);
        }

        /// <summary>
        /// Create and return an active command based on the results of a SQL string and type.  Optional parameters may be specified.
        /// </summary>
        /// <param name="commandString"></param>
        /// <param name="commandType"></param>
        /// <param name="oleDbParameters"></param>
        /// <returns></returns>
        public OleDbCommand CreateCommand(string commandString, CommandType commandType = CommandType.StoredProcedure, params OleDbParameter[] oleDbParameters)
        {
            OleDbCommand cmd = null;
            try
            {
                cmd = new OleDbCommand(commandString, _con);
                cmd.CommandTimeout = Properties.Settings.Default.CommandTimeout;
                cmd.CommandType = commandType;

				if (_transaction != null && _transaction.Connection != null)
					cmd.Transaction = _transaction;

                if (commandType == CommandType.StoredProcedure)
                    cmd.Parameters.AddRange(oleDbParameters);
                else
                    foreach (var param in oleDbParameters)
                        cmd.CommandText = cmd.CommandText.Replace(param.ParameterName, param.Value.ToString());
            }
            catch (OleDbException ex)
            {
                ExceptionHandler.RaiseException(ex, "CreateCommand()");
            }
            catch (Exception ex)
            {
                ExceptionHandler.RaiseException(ex, "CreateCommand()");
            }
            return cmd;

        }

        /// <summary>
        /// Create an and return OleDb Parameter
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <param name="oleDbType"></param>
        /// <param name="size"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public OleDbParameter CreateParameter(string parameterName, object value, OleDbType oleDbType, int size, ParameterDirection direction = ParameterDirection.Input)
        {
            var param = new OleDbParameter(parameterName, oleDbType, size);
            if (param != null)
            {
                param.Value = value;
                param.Direction = direction;
            }
            return param;
        }

        public OleDbParameter CreateParameter(string parameterName, object value, OleDbType oleDbType, int size, byte decimals, ParameterDirection direction = ParameterDirection.Input)
        {
            var param = new OleDbParameter(parameterName, oleDbType, size);
            if (param != null)
            {
                param.Value = value;
                param.Direction = direction;
                param.Precision = (byte) size;               
                param.Scale = decimals;
            }
            return param;
        }

        /// <summary>
        /// Create a datareader object based on the results of the command.  The reader
        /// will be returned in an open state.
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public OleDbDataReader CreateReader(OleDbCommand cmd)
        {
            OleDbDataReader reader = null;
            try
            {
                if (this.LogSQL)
                    WriteSQL(cmd.CommandText);
				var startTime = DateTime.Now;
                reader = cmd.ExecuteReader();
				ExecutionTime = DateTime.Now.Subtract(startTime);
            }
            catch (OleDbException ex)
            {
                ExceptionHandler.RaiseException(ex, "CreateReader()");
            }
            catch (Exception ex)
            {
                ExceptionHandler.RaiseException(ex, "CreateReader()");
            }
            return reader;
        }

        /// <summary>
        /// Return a datatable based on the execution results of the commands
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public DataTable CreateTable(OleDbCommand cmd, int maxRows = 0)
        {
            DataTable table = null;
            OleDbDataReader reader = null;
            try
            {
				var startTime = DateTime.Now;
				reader = CreateReader(cmd);
                if (reader != null)
                {
                    table = new DataTable();
                    table.BeginLoadData();
                   
                    if (maxRows == 0)
                        table.Load(reader);
                    else
                    {
                        foreach (DataRow schemaRow in reader.GetSchemaTable().Rows)
                        {
                            var col = new DataColumn();
                            col.AllowDBNull = Convert.ToBoolean(schemaRow["AllowDBNull"]);
                            col.AutoIncrement = Convert.ToBoolean(schemaRow["IsAutoIncrement"]);
                            col.ColumnName = schemaRow["ColumnName"].ToString();
                            col.DataType = Type.GetType(schemaRow["DataType"].ToString());
                            col.Unique = Convert.ToBoolean(schemaRow["IsUnique"]);
                            col.ReadOnly = Convert.ToBoolean(schemaRow["IsReadOnly"]);
                            table.Columns.Add(col);
                        }

                        var row = 0;                       
                        while (reader.Read() && row++ < maxRows)
                        {
                            object[] values = new object[reader.FieldCount];
                            reader.GetValues(values);
                            table.LoadDataRow(values, true);
                        }
                    }
                    if (table != null && table.Rows.Count > 0)
                        table.AcceptChanges();
                    table.EndLoadData();
                    ExecutionTime = DateTime.Now.Subtract(startTime);
                }
            }
            catch (OleDbException ex)
            {
                ExceptionHandler.RaiseException(ex, "CreateTable()");
            }
            catch (Exception ex)
            {
                ExceptionHandler.RaiseException(ex, "CreateTable()");
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }
            return table;
        }
        
        /// <summary>
        /// Return a datatable based on the result from the SQL statement.
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable CreateTable(string sql, int maxRows = 0)
        {
            DataTable table = null;
            try
            {
                var cmd = CreateCommand(sql);
                if (cmd != null)
                    table = CreateTable(cmd, maxRows);

            }
            catch (OleDbException ex)
            {
                ExceptionHandler.RaiseException(ex, "CreateTable()");
            }
            catch (Exception ex)
            {
                ExceptionHandler.RaiseException(ex, "CreateTable()");
            }
            return table;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="className"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool CreateClassTemplate(DataTable table, string className, string filename)
        {
            var result = false;
            var buffer = new StringBuilder();

            buffer.AppendLine("public class " + className);
            buffer.AppendLine("{");

            foreach (DataColumn col in table.Columns)
                buffer.AppendLine("\tpublic " + col.DataType.Name.ToLower() + " " + col.ColumnName.Capitalise() + " {get; set;}");

            buffer.Append(Properties.Resources.CreateClassTemplateDefinition.Replace("<className>", className));

            try
            {
                using (var tw = new StreamWriter(filename, false))
                {
                    tw.Write(buffer.ToString());
                    tw.Close();
                }
                result = true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.SetException(ex);
            }

            return result;
        }

        /// <summary>
        /// Return True if 'keyValue' is found in 'Key' in 'table'
        /// </summary>
        /// <param name="table"></param>
        /// <param name="key"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public bool LookupKey(string table, string key, object keyValue)
        {
            var result = false;
            var keyValueStr = string.Empty;

            if (OpenConnection())
                try
                {
                    if (keyValue.GetType() == typeof(string))
                        keyValueStr = "'" + keyValue.ToString() + "'";
                    else
                        keyValueStr = keyValue.ToString();

                    var cmd = CreateCommand(string.Format("SELECT COUNT(*) AS Count FROM {0} WHERE {1}={2}", table, key, keyValueStr));
                    var value = (int) cmd.ExecuteScalar();
                    result = (value > 0);
                }
                catch (OleDbException ex)
                {
                    ExceptionHandler.RaiseException(ex, "LookupKey()");
                }
                catch (Exception ex)
                {
                    ExceptionHandler.RaiseException(ex, "LookupKey");
                }
            return result;
        }

        public bool LookupKey(string table, string whereClause)
        {
            var result = false;
            var keyValueStr = string.Empty;

            if (OpenConnection())
                try
                {
                    var cmd = CreateCommand(string.Format("SELECT COUNT(*) AS Count FROM {0} WHERE {1}", table, whereClause));
                    var value = (int)cmd.ExecuteScalar();
                    result = (value > 0);
                }
                catch (OleDbException ex)
                {
                    ExceptionHandler.RaiseException(ex, "LookupKey()");
                }
                catch (Exception ex)
                {
                    ExceptionHandler.RaiseException(ex, "LookupKey");
                }
            return result;
        }

        /// <summary>
        /// Return the value of 'field' from 'table' where 'key' = 'keyValue' 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="field"></param>
        /// <param name="key"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public object GetValue(string table, string field, string key, object keyValue, object defaultValue = null)
        {
            object result = null;
            var keyValueStr = string.Empty;

            if (OpenConnection())
                try
                {
                    if (keyValue.GetType() == typeof(string))
                        keyValueStr = "'" + keyValue.ToString() + "'";
                    else
                        keyValueStr = keyValue.ToString();

                    var cmd = CreateCommand(string.Format("SELECT {0} FROM {1} WHERE {2}={3}", field, table, key, keyValueStr));
                    result = cmd.ExecuteScalar();
                    if (result == null)
                        result = defaultValue;

                }
                catch (OleDbException ex)
                {
                    ExceptionHandler.RaiseException(ex, "GetValue");
                }
                catch (Exception ex)
                {
                    ExceptionHandler.RaiseException(ex, "GetValue");
                }
            return result;
        }

        public object GetValue(string table, string field, string whereClause, object defaultValue = null)
        {
            object result = null;
            var keyValueStr = string.Empty;

            if (OpenConnection())
                try
                {

                    var cmd = CreateCommand(string.Format("SELECT {0} FROM {1} WHERE {2}", field, table, whereClause, keyValueStr));
                    result = cmd.ExecuteScalar();
                    if (result == null)
                        result = defaultValue;

                }
                catch (OleDbException ex)
                {
                    ExceptionHandler.RaiseException(ex, "GetValue");
                }
                catch (Exception ex)
                {
                    ExceptionHandler.RaiseException(ex, "GetValue");
                }
            return result;
        }

        /// <summary>
        /// Submit a job using the default wrapper and job queue as defined in .config file.
        /// [Deprecated - use CallDSIPWRAP]
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="programName"></param>
        /// <param name="jobParams"></param>
        /// <returns></returns>
        public bool SubmitJob(string jobName, string programName, List<string> parameters)
        {
            return SubmitJob(Properties.Settings.Default.DefaultWrapper, jobName, programName, Properties.Settings.Default.DefaultJobQueue, parameters);
        }

        /// <summary>
        /// Submit Job 
        /// [Deprecated - use CallDSIPWRAP]
        /// </summary>
        /// <param name="wrapper"></param>
        /// <param name="jobName"></param>
        /// <param name="programName"></param>
        /// <param name="jobQueue"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        // CALL QSYS/QCMDEXC('SBMJOB CMD(CALL PGM(DSIPWRAP) PARM(''S883TC0001'' ''DS883SC'' ''DMLEVINE   T'' ''N'' ''N'') JOBQ(QGPL/QTXTSRCH))', 0000000112.00000) 
        public bool SubmitJob(string wrapper, string jobName, string programName, string jobQueue, List<string> parameters)
        {
            var sql = string.Format("SBMJOB CMD(CALL PGM({0}) PARM('{1}' '{2}' {3})) JOBQ({4})",
                wrapper, jobName, programName, "'" + string.Join("' '", parameters) + "'", jobQueue);
            return CallPgm(sql);
        }

        /// <summary>
        /// Run a Job using a generic .NET wrapper (DSIWRAP).
        /// option1 & 2 are 153 character fields.
        /// </summary>
        /// <param name="job"></param>
        /// <param name="program"></param>
        /// <param name="option1"></param>
        /// <param name="option2"></param>
        /// <param name="submit"></param>
        /// <param name="error"></param>
        /// <returns></returns>
		public bool SubmitDSIPWRAP(string job, string program, string option1 = "", string option2 = "", char submit = 'Y', char error = ' ', OleDbTransaction tran = null)
        {
            var sql = string.Format("SBMJOB CMD(CALL PGM(DSIPWRAP) PARM('{0}' '{1}' '{2}' '{3}' '{4}' '{5}')) JOBQ({6})",
                job, program, option1.PadRight(153), option2.PadRight(153), submit, error, Properties.Settings.Default.DefaultJobQueue);
            return CallPgm(sql, tran);
        }
        
        /// <summary>
        /// Run a Job using a generic .NET wrapper (DSIWRAP).
        /// option1 & 2 are 153 character fields.
        /// </summary>
        /// <param name="job"></param>
        /// <param name="program"></param>
        /// <param name="option1"></param>
        /// <param name="option2"></param>
        /// <param name="submit"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool CallDSIPWRAP(string job, string program, string option1 = "", string option2 = "", char submit = 'N', char error = ' ', OleDbTransaction tran = null)
        {
            var sql = string.Format("CALL PGM(DSIPWRAP) PARM('{0}' '{1}' '{2}' '{3}' '{4}' '{5}')",
                job, program, option1.PadRight(153), option2.PadRight(153), submit, error);
            return CallPgm(sql, tran);
        }
        
        /// <summary>
        /// Run a program on the AS400.
        /// </summary>
        /// <param name="cmdtext"></param>
        /// <returns></returns>
        public bool CallPgm(String cmdtext, OleDbTransaction tran = null)
        {
            bool result = false;
            var sql = "CALL QSYS/QCMDEXC('" + cmdtext.Replace("'", "''") + "', " + cmdtext.Length.ToString("0000000000.00000") + ")";

            if (OpenConnection())
                try
                {
                    var cmd = CreateCommand(sql);
                    if (this.LogSQL)
                        WriteSQL(cmd.CommandText);
					cmd.Transaction = tran;
					var startTime = DateTime.Now;
                    cmd.ExecuteNonQuery();
					ExecutionTime = DateTime.Now.Subtract(startTime);
                    result = true;
                }
                catch (Exception ex)
                {
                    ExceptionHandler.RaiseException(ex, "CallPgm");
                }
            return result;
        }

        /// <summary>
        /// Search for each token defined in tokens contained in sql expression and replace
        /// with the value in values.  For string/character tokens or any other data type
        /// that requires quotes or delimiters, these should be included in the SQL statement
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="tokens"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public string ReplaceTokens(string sql, List<string> tokens, List<object> values, string leftDelimiter = "", string rightDelimiter = "")
        {
            if (tokens.Count > values.Count)
                throw new Exception("ReplaceTokens: More tokens than values supplied");
            else
            {
                var sb = new StringBuilder();
                sb.Append(sql);
                for (var i = 0; i < tokens.Count; i++)
                    sb.Replace(leftDelimiter + tokens[i] + rightDelimiter, values[i].ToString());
                return sb.ToString();
            }
        }

        /// <summary>
        /// Search for each token defined in parameters and replace with corresponding value.  
        /// For string/character tokens or any other data type that requires quotes or delimiters, 
        /// these should be included in the SQL statement
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="tokens"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public string ReplaceTokens(string sql, Dictionary<string, object> parameters, string leftDelimiter = "", string rightDelimiter = "")
        {
            var sb = new StringBuilder();
            sb.Append(sql);
            foreach (var kvp in parameters)
                sb.Replace(leftDelimiter + kvp.Key + rightDelimiter, kvp.Value.ToString());
            return sb.ToString();
        }

        public void WriteSQL(string sql)
        {
            var fileName = GetUniqueFilename(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + kTempPath, "iDash.{0:00000000}.sql");

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                try
                {
                    sw.WriteLine(sql);
                    sw.Close();
                }
                catch (Exception ex)
                {
                    ExceptionHandler.SetException(ex);
                }
            }
        }

        /// <summary>
        /// Delete log files older than [purgeOlder] days.
        /// </summary>
        /// <param name="purgeOlder"></param>
        public static void PurgeLogFiles(int purgeOlder = 7)
        {
            var di = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + kTempPath);
            var files = di.GetFiles("iDash*.sql");
            var today = System.DateTime.Now.Date;

            foreach (var file in files)
                try
                {
                    if (today.Subtract(file.CreationTime.Date).Days > purgeOlder)
                        file.Delete();
                }
                catch
                {
                }               
        }

        private string GetUniqueFilename(string path, string filemask)
        {
            var directory = new System.IO.DirectoryInfo(path);
            var index = (directory == null ? 0 : directory.GetFiles().Count());
            var filename = string.Empty;
            do
            {
                filename = path + string.Format(filemask, ++index);                           
            } 
            while (System.IO.File.Exists(filename));
            return filename;
        }

        public delegate object CustomConversionDelegate(PropertyInfo propertyInfo, object value);

        public List<T> PopulateProperties<T>(DataTable table, Type type, CustomConversionDelegate customConversion)
        {
            List<T> items = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                T item = (T) Activator.CreateInstance(type);
                foreach (var pi in item.GetType().GetProperties())
                    if (table.Columns.Contains(pi.Name))
                    {
                        var value = row[pi.Name];
                        if (value != null && value != System.DBNull.Value)
                            if (value.GetType().Name == pi.PropertyType.Name)
                                pi.SetValue(item, value, null);
                            else
                                pi.SetValue(item, customConversion(pi, value), null);
                    }
                items.Add(item);
            }
            return items;
        }

        public DateTime? ConvertDate(string db2Date)
        {
            DateTime? date = null;
            //0123456789012345678901
            //2011-07-26-03.20.14.121000
            var pattern = @"\d{4}\-\d{2}\-\d{2}\-\d{2}\.\d{2}\.\d{2}\.\d{6}";
            var match = Regex.Match(db2Date, pattern);

            if (match.Success)
                foreach (Group group in match.Groups)
                    foreach (Capture capture in group.Captures)
                        date = new DateTime(Convert.ToInt32(capture.Value.Left(4)),
                                            Convert.ToInt32(capture.Value.Substring(5, 2)), 
                                            Convert.ToInt32(capture.Value.Substring(8, 2)),
                                            Convert.ToInt32(capture.Value.Substring(11, 2)), 
                                            Convert.ToInt32(capture.Value.Substring(14, 2)), 
                                            Convert.ToInt32(capture.Value.Substring(17, 2)));
            return date;
        }

        public string ReplaceDate(string exprWithdb2Date, string dateformat = "dd-MMM-yyyy HH:mm:ss")
        {
            var pattern = @"\d{4}\-\d{2}\-\d{2}\-\d{2}\.\d{2}\.\d{2}\.\d{6}";
            var date = ConvertDate(exprWithdb2Date);
            if (date.HasValue)
                return Regex.Replace(exprWithdb2Date, pattern, date.Value.ToString(dateformat));
            else
                return exprWithdb2Date;
        }

		public void ListParameters(OleDbParameterCollection parameters)
		{
			Console.WriteLine("Name\tType\tSize\tDirection\tValue");
			Console.WriteLine("----\t----\t----\t---------\t-----");
			foreach (OleDbParameter param in parameters)
				Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", param.ParameterName, param.OleDbType.ToString(), param.Size, param.Direction.ToString(), param.Value);
			Console.WriteLine();
		}

		public void listSchema(DataTable table)
		{
			Console.WriteLine("Name\tType\tMaxLen\tNullable\tCaption");
			Console.WriteLine("----\t----\t------\t--------\t------");
			foreach (DataColumn col in table.Columns)
				Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", col.ColumnName, col.DataType.Name, col.MaxLength, col.AllowDBNull, col.Caption);
			Console.WriteLine();
		}
	}
}
