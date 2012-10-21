using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using StudInfoSys.App_Start;
using StudInfoSys.Migrations;
using StudInfoSys.Models;

namespace StudInfoSys
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            // NOTE: This will reset the database based on the Seed method in Configuration class
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudInfoSysContext, Configuration>());
            //Database.SetInitializer( new DropCreateDatabaseIfModelChanges<StudInfoSysContext>());

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            
            Debug.WriteLine("DEBUG: " + e.ToString());

        }
    }
}