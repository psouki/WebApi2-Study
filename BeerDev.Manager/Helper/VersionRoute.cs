using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Routing;

namespace BeerDev.Manager.Helper
{
    public class VersionRoute : RouteFactoryAttribute
    {
        public int AllowedVersion { get; }

        public VersionRoute(string template, int allowedVersion) : base(template)
        {
            AllowedVersion = allowedVersion;
        }

        public override IDictionary<string, object> Constraints
        {
            get
            {
                var constraints = new HttpRouteValueDictionary
                {
                    {
                         "version",
                         new VersionConstrain(AllowedVersion)
                    }
                };
                return constraints;
            }
        }
    }
}