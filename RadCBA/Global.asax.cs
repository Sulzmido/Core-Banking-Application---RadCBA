using RadCBA.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RadCBA
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private AppContext db = new AppContext();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            if(db.AccountConfigurations.Count() < 1)
            {
                db.AccountConfigurations.Add(new AccountConfiguration { FinancialDate = DateTime.Now, IsBusinessOpen = false });
                db.SaveChanges();
                db.Dispose();
            }
            
        }
    }
}
