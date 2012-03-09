using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Disney.iDash.LocalData
{
    public partial class LocalDataEntities : ObjectContext
    {
        public eEnvironment GetEnvironment(LocalDataEntities entities, string environmentName)
        {
            return entities.eEnvironments.Where((e) => (e.EnvironmentName == environmentName)).FirstOrDefault();
        }
    }
}
