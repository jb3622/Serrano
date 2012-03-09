/*
 * Author:  Marc Levine
 * Project: SRR
 * Date:    From March 2011
 * 
 * Description
 * This class is called from the Workbench Extract form and provides all the functionality to
 * call the required AS400 jobs to run the extract.
 */
using System;
using Disney.iDash.DataLayer;
using Disney.iDash.LocalData;
using Disney.iDash.Shared;

namespace Disney.iDash.SRR.BusinessLayer
{
    public class WorkbenchExtract
    {
        private DB2Factory _factory = new DB2Factory();
        
        public ExceptionHandler ExceptionHandler = new ExceptionHandler();
      
        public WorkbenchExtract()
        {
            _factory.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
            {
                ExceptionHandler.RaiseException(ex, extraInfo, terminateApplication);
            });
        }

		public bool IsExtractRunning()
		{
			var isRunning = false;

			if (_factory.OpenConnection())
				try
				{
					var cmd = _factory.CreateCommand("DS888BS1", System.Data.CommandType.StoredProcedure,
						_factory.CreateParameter("P_ERR", " ", System.Data.OleDb.OleDbType.Char, 1, System.Data.ParameterDirection.InputOutput));
					cmd.ExecuteNonQuery();
					isRunning = cmd.Parameters["P_ERR"].Value != null && cmd.Parameters["P_ERR"].Value.ToString() == "Y";
				}
				catch (Exception ex)
				{
					ExceptionHandler.RaiseException(ex, "IsExtractRunning", false);
				}
				finally
				{
					_factory.CloseConnection();
				}

			return isRunning;
		}

        /// <summary>
        /// Run the extract for the given period.  This will call job/program "S883TC0001", "DS883SC" on
        /// the AS400
        /// </summary>
        /// <param name="extractPeriod"></param>
        /// <returns></returns>
        public bool Extract(string extractPeriod)
        {
            var result = false;
            var option1 = string.Empty;
            
            switch (extractPeriod.ToLower())
            {
                case "daily":
                    option1 = "D";
                    break;
                case "weekly":
                    option1 = "W";
                    break;
                case "weekend":
                    option1 = "S";
                    break;
                default:
                    throw new Exception("ExtractPeriod must be Daily, Weekly or Weekend");
            }

            option1 += Session.User.NetworkId.ToUpper().PadRight(10);
            
            if (Session.Environment.Type == "Production")
                option1 += "P  ";
            else
                option1 += "T  ";

//            result = _factory.SubmitJob("S883TC0001", "DS883SC", new List<string> {option1, "", "N", "N"});
            result = _factory.SubmitDSIPWRAP("S883TC0001", "DS883SC", option1, "", 'N', 'N');
            return result;
        }

    }


}
