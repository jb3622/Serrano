using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlServerCe;

namespace ConvertLocalData
{
    public class ConvertData
    {

        private SqlCeConnection _oldConnection = new SqlCeConnection();
        private SqlCeConnection _newConnection = new SqlCeConnection();
        public Exception LastException { get; private set; }

        public void ConvertTables()
        {
            if (OpenConnections())
            {
                if (ConvertUsers())
                {
                    ConvertUserEnvironments();
                    ConvertUserApplications();
                    ConvertUserMenuOptions();
                }
                CloseConnections();
            }
        }

        private bool OpenConnections()
        {
            var result = false;

            try
            {
                Console.WriteLine("Opening connections");

                _oldConnection.ConnectionString = Properties.Settings.Default.OldConnection;
                _newConnection.ConnectionString = Properties.Settings.Default.NewConnection;

                _oldConnection.Open();
                _newConnection.Open();
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return result;
        }

        private void CloseConnections()
        {
            Console.WriteLine("Closing connections");
            if (_oldConnection != null && _oldConnection.State == ConnectionState.Open)
                _oldConnection.Close();

            if (_newConnection != null && _newConnection.State == ConnectionState.Open)
                _newConnection.Close();
        }

        private bool ConvertUsers()
        {

            var result = false;
            SqlCeCommand cmd = null;

            Console.WriteLine("Converting users");

            try
            {
                cmd = new SqlCeCommand("delete from users", _newConnection);
                cmd.ExecuteNonQuery();

                var table = CreateTable("select * from users");
                if (table != null)
                {
                    cmd.CommandText = "SET IDENTITY_INSERT Users ON";
                    cmd.ExecuteNonQuery();
                    var cmdInsert = new SqlCeCommand("insert into users (Id, NetworkId, Firstname, Lastname, eMail, Active) VALUES (@Id, @networkid, @firstname, @lastname, @email, @active)", _newConnection);

                    cmdInsert.CommandType = CommandType.Text;
                    cmdInsert.Parameters.Add("@Id", SqlDbType.Int, 4, "Id");
                    cmdInsert.Parameters.Add("@NetworkId", SqlDbType.NVarChar, 100, "NetworkId");
                    cmdInsert.Parameters.Add("@Firstname", SqlDbType.NVarChar, 100, "Firstname");
                    cmdInsert.Parameters.Add("@Lastname", SqlDbType.NVarChar, 100, "Lastname");
                    cmdInsert.Parameters.Add("@eMail", SqlDbType.NVarChar, 128, "eMail");
                    cmdInsert.Parameters.Add("@Active", SqlDbType.Bit, 1, "Active");

                    foreach (DataRow row in table.Rows)
                    {
                        cmdInsert.Parameters["@Id"].Value = Convert.ToInt32(row["Id"]);
                        cmdInsert.Parameters["@networkId"].Value = row["NetworkId"].ToString();
                        cmdInsert.Parameters["@firstname"].Value = row["Firstname"].ToString();
                        cmdInsert.Parameters["@lastname"].Value = row["Lastname"].ToString();
                        cmdInsert.Parameters["@email"].Value = row["eMail"].ToString();
                        cmdInsert.Parameters["@active"].Value = Convert.ToBoolean(row["Active"]);
                        cmdInsert.ExecuteNonQuery();
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                cmd.CommandText = "SET IDENTITY_INSERT Users OFF";
                cmd.ExecuteNonQuery();
            }
            return result;
        }

        private bool ConvertUserEnvironments()
        {
            var result = false;
            Console.WriteLine("Converting user environments");

            try
            {
                var table = CreateTable("SELECT * FROM UserEnvironments");
                if (table != null)
                {
                    var cmdInsert = new SqlCeCommand("INSERT INTO UserEnvironments (UserId, EnvironmentId) VALUES (@UserId, @EnvironmentId)", _newConnection);
                    cmdInsert.Parameters.Add("@UserId", SqlDbType.Int, 4, "UserId");
                    cmdInsert.Parameters.Add("@EnvironmentId", SqlDbType.Int, 4, "EnvironmentId");

                    foreach (DataRow row in table.Rows)
                    {
                        cmdInsert.Parameters["@UserId"].Value = Convert.ToInt32(row["UserId"]);
                        cmdInsert.Parameters["@EnvironmentId"].Value = Convert.ToInt32(row["EnvironmentId"]);
                        cmdInsert.ExecuteNonQuery();
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return result;
        }

        private bool ConvertUserApplications()
        {
            var result = false;
            Console.WriteLine("Converting user applications");

            try
            {
                var table = CreateTable("SELECT * FROM UserApplications");
                if (table != null)
                {
                    var cmdInsert = new SqlCeCommand("INSERT INTO UserApplications (UserId, ApplicationId) VALUES (@UserId, @ApplicationId)", _newConnection);
                    cmdInsert.Parameters.Add("@UserId", SqlDbType.Int, 4, "UserId");
                    cmdInsert.Parameters.Add("@ApplicationId", SqlDbType.Int, 4, "ApplicationId");

                    foreach (DataRow row in table.Rows)
                    {
                        cmdInsert.Parameters["@UserId"].Value = Convert.ToInt32(row["UserId"]);
                        cmdInsert.Parameters["@ApplicationId"].Value = Convert.ToInt32(row["ApplicationId"]);
                        cmdInsert.ExecuteNonQuery();
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return result;
        }

        private bool ConvertUserMenuOptions()
        {
            var result = false;
            var map = new Dictionary<string, int> 
                { 
                    {"DeptStoreGrades", 12},
                    {"StoreDeletionSchedule", 10},
                    {"StoreLeadTimes", 11},
                    {"UserDeptAuthority", 9},
                    {"WorkbenchExtract", 13}
                };

            Console.WriteLine("Converting user menu options");

            try
            {
                var table = CreateTable("SELECT * FROM UserMenuOptions");
                if (table != null)
                {
                    var menuOptionId = 0;
                    var cmdInsert = new SqlCeCommand("INSERT INTO UserMenuOptions (UserId, MenuOptionId) VALUES (@UserId, @MenuOptionId)", _newConnection);
                    cmdInsert.Parameters.Add("@UserId", SqlDbType.Int, 4, "UserId");
                    cmdInsert.Parameters.Add("@MenuOptionId", SqlDbType.Int, 4, "MenuOptionId");

                    foreach (DataRow row in table.Rows)
                    {
                        if (map.TryGetValue(row["DisabledMenuOption"].ToString(), out menuOptionId))
                        {
                            cmdInsert.Parameters["@UserId"].Value = Convert.ToInt32(row["UserId"]);
                            cmdInsert.Parameters["@MenuOptionId"].Value = menuOptionId;
                            cmdInsert.ExecuteNonQuery();
                        }
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return result;
        }

        private DataTable CreateTable(string sql)
        {
            SqlCeDataReader reader = null;
            DataTable table = null;
            try
            {
                var cmdSelect = new SqlCeCommand(sql, _oldConnection);
                reader = cmdSelect.ExecuteReader();
                if (reader != null)
                {
                    table = new DataTable();
                    table.Load(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("CreateTable: {0}\n{1}", sql, ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }
            if (table == null)
                Console.WriteLine(sql + " returned no records");

            return table;
        }
    }
}
