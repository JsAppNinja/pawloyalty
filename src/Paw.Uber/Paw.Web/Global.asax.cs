using Paw.Services.Attributes;
using Paw.Services.Common;
using StackExchange.Profiling;
using StackExchange.Profiling.EntityFramework6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Hangfire;
using Paw.Services.Messages.Caching;
using Paw.Services.Messages;

namespace Paw.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            System.Web.Http.GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            MiniProfilerEF6.Initialize();
            MiniProfiler.Settings.Results_List_Authorize = IsUserAllowedToSeeMiniProfilerUI;

            ModelMetadataProviders.Current = new CustomDataAnnotationsModelMetadataProvider();
            //ModelBinders.Binders.Add(typeof(IProviderId), ProviderModelBinder());

            this.ConfigureHangfire();


        }

        protected void Application_BeginRequest()
        {
            if (Request.IsLocal)
            {
                MiniProfiler.Start();
            }
        }

        protected void ConfigureHangfire()
        {
            Hangfire.GlobalConfiguration.Configuration.UseSqlServerStorage("DataContext");

            RecurringJob.AddOrUpdate("crefresh", () => CacheRefresh(), "0/2 * * * *", queue: "crefresh");
        }

        public static void CacheRefresh()
        {
            // Step 1. Get Cache Keys
            List<Guid> activeProviderIdList = CacheHelper.GetActiveProviderIdList(40);

            // Step 2. Action
            foreach (Guid id in activeProviderIdList)
            {
                BackgroundJob.Enqueue(() => SeedCacheHelper.SeedProvider(id));
            }

        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }

        private bool IsUserAllowedToSeeMiniProfilerUI(HttpRequest httpRequest)
        {
            // Implement your own logic for who 
            // should be able to access ~/mini-profiler-resources/results-index
            var principal = httpRequest.RequestContext.HttpContext.User;
            return principal.IsInRole("Developer");
        }
    }
}
