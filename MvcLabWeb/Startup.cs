using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcLabWeb.Startup))]
namespace MvcLabWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
