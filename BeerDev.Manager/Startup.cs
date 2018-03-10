using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BeerDev.Manager.Startup))]
namespace BeerDev.Manager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
