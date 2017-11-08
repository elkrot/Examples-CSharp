using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnionApp.Web.Startup))]
namespace OnionApp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
