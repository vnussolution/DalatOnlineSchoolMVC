using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;

namespace DalatOnlineSchool.DAL
{
    //The Entity Framework automatically runs the code it finds in a class that derives from DbConfiguration
    // this class created to implement RetryLimitExceededException exception in studentscontroller
    public class SchoolConfiguration1 : DbConfiguration
    {
        public SchoolConfiguration1()
        {
            //SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
            //DbInterception.Add(new SchoolInterceptorTransientErrors());
            //DbInterception.Add(new SchoolInterceptorLogging());
        }
    }
}