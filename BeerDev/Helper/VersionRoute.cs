using System.Collections.Generic;
using System.Web.Http.Routing;

namespace BeerDev.Helper
{
    // to build a Route Factory Attribute is very simple
    public class VersionRoute : RouteFactoryAttribute
    {
        public int AllowedVersion { get; }

        public VersionRoute(string template, int allowedVersion) : base(template)
        {
            AllowedVersion = allowedVersion;
        }

        // just override the constraints dictionary with the custom constraint
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