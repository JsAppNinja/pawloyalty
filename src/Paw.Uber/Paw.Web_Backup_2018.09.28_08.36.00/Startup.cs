using Microsoft.Owin;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.StaticFiles.ContentTypes;
using Owin;

[assembly: OwinStartupAttribute(typeof(Paw.Web.Startup))]
namespace Paw.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            
        }
    }
}
