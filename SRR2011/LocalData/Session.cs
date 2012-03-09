/*
 * Author:  Marc Levine
 * Project: SRR
 * Date:    From March 2011
 * 
 * Description
 * Static class containing a varied collection of run time operating values.
 * 
 *  
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Disney.iDash.Shared;
using System.Linq;
using System.Data.EntityClient;
using System.Net;

namespace Disney.iDash.LocalData
{
    public static class Session
    {
        public static Disney.iDash.LocalData.eUser User { get; set; }
        public static Disney.iDash.LocalData.eEnvironment Environment { get; set; }

        public static Exception LastException { get; private set; }

        public static void ClearException()
        {
            LastException = null;
        }

        /// <summary>
        /// Get/Set the form skin colours.
        /// </summary>
        public static string SkinColor
        {
            get
            {
                return RegUtils.GetCustomSetting("Skin", "Money Twins");
            }
            set
            {
                RegUtils.SetCustomSetting("Skin", value);
            }
        }

        public static string LastEnvironmentName
        {
            get
            {
                return RegUtils.GetCustomSetting("LastEnvironmentName", string.Empty);
            }
            set
            {
                if (value != string.Empty)
                    RegUtils.SetCustomSetting("LastEnvironmentName", value);
            }
        }

        public static string GetLocalConfigFileName()
        {
            return System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\iDash\\LocalData.sdf";
        }

        public static bool CopyRemoteConfigFile(string remoteFilename)
        {
            var result = false;
            var localFilename = GetLocalConfigFileName();

            ClearException();

            try
            {
                if (string.IsNullOrEmpty(remoteFilename))
                    result = true;
                else if (System.IO.File.Exists(remoteFilename))
                {
                    System.IO.File.Copy(remoteFilename, localFilename, true);
                    result = true;
                }
                else
                    using (var client = new WebClient())
                    {
                        client.DownloadFile(remoteFilename, localFilename);
                        result = true;
                    }
            }
            catch (Exception ex)
            {
                LastException = ex;
            }

            if (result)
                LocalDataConnection = localFilename;

            return result;

        }

        public static string _localDataConnection = string.Empty;
        public static string LocalDataConnection
        {
            get
            {
                return _localDataConnection;
            }

            set
            {

                var connectionStrings = System.Configuration.ConfigurationManager.ConnectionStrings;
                var connectionInfo = connectionStrings["LocalDataEntities"];
                if (connectionInfo != null)
                    _localDataConnection = connectionInfo.ConnectionString.Replace(@"Data Source=|DataDirectory|\LocalData.sdf", string.Format(@"Data Source={0}", value));

            }
        }

        public static bool TestConnection(string datapath, double requiredVersion)
        {
            var connected = false;
            EntityConnection connection = null;

            try
            {
                var connectionStrings = System.Configuration.ConfigurationManager.ConnectionStrings;
                var connectionInfo = connectionStrings["LocalDataEntities"];
                if (connectionInfo == null)
                    LastException = new Exception("LocalDataEntities is missing from app.config");
                else
                {
                    var connectionString = connectionInfo.ConnectionString.Replace(@"Data Source=|DataDirectory|\LocalData.sdf", string.Format(@"Data Source={0}", datapath));
                    connection = new EntityConnection(connectionString);
                    connection.Open();

                    connected = CheckVersion(datapath, requiredVersion, connection);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    LastException = ex;
                else
                    LastException = ex.InnerException;
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return connected;
        }

        private static bool CheckVersion(string datapath, double requiredVersion, EntityConnection connection)
        {
            var versionOk = false;
            var context = new LocalDataEntities(connection);
            var actualVersion = 0d;
            var qry = from s in context.Settings
                      select s;
            try
            {
                actualVersion = qry.FirstOrDefault().Version;
                if (actualVersion >= requiredVersion)
                    versionOk = true;
                else
                    LastException = new Exception(string.Format("Local data file: {0} is version {1}.  Version {2} is required", datapath, actualVersion, requiredVersion));
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    LastException = ex;
                else
                    LastException = ex.InnerException;
            }

            return versionOk;
        }
    }
}
