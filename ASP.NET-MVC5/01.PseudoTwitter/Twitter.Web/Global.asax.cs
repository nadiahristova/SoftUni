using System.Data.Entity;

using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;

using Twitter.Data;
using Twitter.Data.Migrations;

namespace Twitter.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer<TwitterContext>(new MigrateDatabaseToLatestVersion<TwitterContext, Configuration>());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
