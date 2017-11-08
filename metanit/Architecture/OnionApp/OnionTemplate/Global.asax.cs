using Ninject;
using Ninject.Modules;
using OnionApp.Web.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OnionApp.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            
            var kernel = new StandardKernel();
            System.Web.Mvc.DependencyResolver.SetResolver(new OnionApp.Web.Util.NinjectDependencyResolver(kernel));

        }
    }
}
