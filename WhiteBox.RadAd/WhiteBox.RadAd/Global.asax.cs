using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WhiteBox.RadAd
{
    using System.Web.Http;
    using Kernel.Log;

    public class MvcApplication : System.Web.HttpApplication
    {
        public static readonly BaseLog Log = new BaseLog();
 
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
