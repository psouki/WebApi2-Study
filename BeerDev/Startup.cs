using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

//[assembly: OwinStartup(typeof(BeerDev.Startup))]
[assembly: OwinStartup("BeerDevConfig", typeof(BeerDev.Startup))]


namespace BeerDev
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
