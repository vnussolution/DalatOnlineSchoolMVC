using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DalatOnlineSchool.DAL;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Infrastructure;
using System.Text;
using System.Xml;
using System.IO;

// namespace for the EdmxWriter class

namespace DalatOnlineSchool
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            
            //Database.SetInitializer( new SchoolInitializer());
            //DbInterception.Add(new SchoolInterceptorLogging());
            //DbInterception.Add(new SchoolInterceptorTransientErrors());

            // generate Entity Diagram
            string path = AppDomain.CurrentDomain.GetData("DataDirectory").ToString() +"EntityDiagram.edmx";
            using (var ctx = new SchoolContext())
            {
                using (var writer = new XmlTextWriter(path, Encoding.Default))
                {
                    EdmxWriter.WriteEdmx(ctx, writer);
                }
            }
        }
    }
}
