/*
 * Author:  Marc Levine
 * Project: SRR
 * Date:    From March 2011
 * 
 * Description
 * Standard error handler that can be included in any other class.  Using this approach each handler can be wired up to the next
 * to ensure that errors are propagated up to a form.
 *  
*/
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Security;
using System.Text;

namespace Disney.iDash.Shared
{
	public class ExceptionHandler
	{

        public enum AlertType
        {
            Information,
            Warning,
            Error
        }

		public delegate void ExceptionDelegate(Exception ex, string extraInfo, bool terminateApplication);
		public event ExceptionDelegate ExceptionEvent;

        public delegate void AlertDelegate(string message, string caption, AlertType alertType);
        public event AlertDelegate AlertEvent;

		public Exception LastException { get; private set; }
		public void ClearException()
		{
			LastException = null;
		}

		public void SetException(Exception ex)
		{
			LastException = ex;
		}

        public void RaiseAlert(string message, string caption, AlertType alertType)
        {
            if (AlertEvent == null)
                throw new Exception(message);
            else
                AlertEvent(message, caption, alertType);
        }

		public void RaiseException(Exception ex, string extraInfo)
		{
            RaiseException(ex, extraInfo, false);
		}

		public void RaiseException(Exception ex, string extraInfo, bool terminateApplication)
		{
            LastException = ex;
            LogException(ex, extraInfo);

            if (ExceptionEvent == null)
                MessageBox.Show(ex.Message, extraInfo, MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (ex.Message.ToLower().Contains("duplicate") && ex.Message.ToLower().Contains("unique"))
                    ex = new Exception("Duplicates are not allowed");
                else if (ex.Message.ToLower().Contains("key") && ex.Message.ToLower().Contains("unique"))
                    ex = new Exception("Duplicates are not allowed");
                else if (ex.Message.ToLower().Contains("delete") && ex.Message.ToLower().Contains("reference"))
                    ex = new Exception("Cannot delete this record because it is referenced by other records");

                ExceptionEvent(ex, extraInfo, false);
            }
        }

        public void LogException(Exception ex, string extraInfo)
        {
            var assemblyInfo = new Shared.AssemblyInfo();
            try
            {
                if (!EventLog.SourceExists(assemblyInfo.AssemblyProduct))
                    System.Diagnostics.EventLog.CreateEventSource(assemblyInfo.AssemblyProduct, "Application");

                if (EventLog.SourceExists(assemblyInfo.AssemblyProduct))
                System.Diagnostics.EventLog.WriteEntry(assemblyInfo.AssemblyProduct, extraInfo + "\n\n" + ex.ToString() + "\n\n" + ex.StackTrace, System.Diagnostics.EventLogEntryType.Error);
            }
            catch (Exception)
            {
                // ignore errors
            }
/*
            catch (SecurityException ex1)
            {
                var sb = new StringBuilder();
                sb.AppendLine(ex1.Message);
                if (ex1.InnerException != null)
                    sb.AppendLine("Inner exception: " + ex1.InnerException.Message);

                if (ex1.FailedAssemblyInfo != null)
                    sb.AppendLine("Failed Assembly: " + ex1.FailedAssemblyInfo.FullName);
                
                if (ex1.Method != null)
                    sb.AppendLine("Method: " + ex1.Method.Name);
                
                if (ex1.PermissionState != null)
                    sb.AppendLine("Permission State: " + ex1.PermissionState);

                sb.AppendLine("Zone: " + ex1.Zone.ToString());               
                sb.AppendLine(ex1.StackTrace);

                MessageBox.Show(sb.ToString(), "LogException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.ToString(), "LogException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
 */
        }
	}
}
