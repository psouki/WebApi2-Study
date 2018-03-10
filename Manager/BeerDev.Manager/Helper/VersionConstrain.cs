using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace BeerDev.Manager.Helper
{
    public class VersionConstrain : IHttpRouteConstraint
    {
        public const string VersionHeaderName = "api-version";
        private const int DefaultVersion = 1;
        private readonly int _allowedVersion;

        public VersionConstrain(int allowedVersion)
        {
            _allowedVersion = allowedVersion;
        }
             
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values,
            HttpRouteDirection routeDirection)
        {
            // We only want to constraint on the outgoing path, so anything different is not subject to the constraint
            if (routeDirection != HttpRouteDirection.UriResolution) return true;

            int version = GetVersionFromRequestHeader(request);
            return version == _allowedVersion;

        }

        private int GetVersionFromRequestHeader(HttpRequestMessage request)
        {
            IEnumerable<string> headerValues;
            request.Headers.TryGetValues(VersionHeaderName, out headerValues);

            if (!headerValues.Any()) return DefaultVersion;

            var versionTxt = headerValues.First();

            int version;
            if (!string.IsNullOrEmpty(versionTxt) && int.TryParse(versionTxt, out version))
            {
                return version;
            }

            return DefaultVersion;
        }
    }
}