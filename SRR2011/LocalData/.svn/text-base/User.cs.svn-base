using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using Disney.iDash.Shared;
using System.Data.Objects.DataClasses;

namespace Disney.iDash.LocalData
{
    public partial class eUser : EntityObject
    {
        public string Fullname { get { return Firstname + " " + Lastname; } }
		public string DisplayName { get { return Fullname + " (" + NetworkId.ToUpper() + ")"; } }

        public int ApplicationCount
        {
            get
            {
                if (vApplications == null)
                    return 0;
                else
                    return vApplications.Count;
            }
        }

        public int EnvironmentCount
        {
            get
            {
                if (vEnvironments == null)
                    return 0;
                else
                    return vEnvironments.Count;
            }
        }
    
        public int MenuOptionCount
        {
            get
            {
                if (vMenuOptions == null)
                    return 0;
                else
                    return vMenuOptions.Count;
            }
        }
    }
}
