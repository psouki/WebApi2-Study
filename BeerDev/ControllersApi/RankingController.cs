using System.IO;
using System.Web.Hosting;
using System.Web.Http;

namespace BeerDev.ControllersApi
{
    public class RankingController : ApiController
    {
        public IHttpActionResult Get()
        {
            string result;
            using (StreamReader sr = new StreamReader(HostingEnvironment.MapPath("~/mockData/beers.json")))
            {
                result = sr.ReadToEnd();
            }

            if (string.IsNullOrEmpty(result))
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
