using Hangfire;
using Microsoft.Owin;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.StaticFiles.ContentTypes;
using Owin;
using Paw.Web.Filters;

[assembly: OwinStartupAttribute(typeof(Paw.Web.Startup))]
namespace Paw.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.UseHangfireServer();
            app.UseHangfireDashboard("/jobs", new DashboardOptions
            {
                Authorization = new[] { new HangfireDashboardFilter() }
            });
        }
    }
}
