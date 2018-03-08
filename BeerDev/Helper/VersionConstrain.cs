using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Routing;

namespace BeerDev.Helper
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
         
        // The custom constraint just need to verify if it is the right route
        // for this there is the method Match, to check if the route obeys those constraints 
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values,
            HttpRouteDirection routeDirection)
        {
            // We only want to constraint on the outgoing path, so anything different is not subject to the constraint
            if (routeDirection != HttpRouteDirection.UriResolution) return true;

            // Retrieves the version number from the request header
            int version = GetVersionFromRequestHeader(request);
            return version == _allowedVersion;

        }

        private int GetVersionFromRequestHeader(HttpRequestMessage request)
        {
            // try to find the version header in the request headers
            IEnumerable<string> headerValues;
            request.Headers.TryGetValues(VersionHeaderName, out headerValues);
            
            if (headerValues == null || !headerValues.Any()) return DefaultVersion;

            var versionTxt = headerValues.First();

            // returns the requested version
            int version;
            if (!string.IsNullOrEmpty(versionTxt) && int.TryParse(versionTxt, out version))
            {
                return version;
            }

            // if not found or could not parse as a number returns the default and nothing breaks 
            return DefaultVersion;
        }
    }
}