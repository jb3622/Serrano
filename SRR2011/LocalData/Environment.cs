using System.Data.Objects.DataClasses;
/*
 * Author:  Marc Levine
 * Project: SRR
 * Date:    From March 2011
 * 
 * Description
 * Environments object contains a list of environments obtained from the Environments local data table.
 * 
 * Each user can be authorised to access zero or more environments.  
 * 
 * An environment is basically a connection string plus additional information.
 *  
*/
using System.Deployment.Application;
using System.Reflection;
using System.Text;

namespace Disney.iDash.LocalData
{

    public partial class eEnvironment : EntityObject
    {
        public new string ToString()
        {
            var result = new StringBuilder();
            result.AppendFormat("Environment:\n\t{0}\n\n", this.EnvironmentName);
            result.AppendFormat("Hostname:\n\t{0}\n\n", this.Hostname);
            result.AppendFormat("Domain:\n\t{0}\n\n", this.Domain);
            result.AppendFormat("Database:\n\t{0}\n\n", this.Database);
			result.AppendFormat("Library List:\n\t{0}\n\n", this.LibraryList);
			result.AppendFormat("Local data path:\n\t{0}\n\n", Session.LocalDataConnection);
			result.AppendFormat("Execution Path:\n\t{0}\n", Assembly.GetEntryAssembly().Location);

			if (ApplicationDeployment.IsNetworkDeployed)
				result.AppendFormat("Deployed from:\n\n\t{0}\n", ApplicationDeployment.CurrentDeployment.UpdateLocation.AbsoluteUri);

            return result.ToString();
        }
    }
}
