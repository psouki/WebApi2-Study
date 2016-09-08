using System.IO;
using System.Web.Hosting;
using System.Web.Http;

namespace BeerDev.ControllersApi
{
    public class GalleryController : ApiController
    {
        public IHttpActionResult Get()
        {
            string result;
            using (StreamReader sr = new StreamReader(HostingEnvironment.MapPath("~/mockData/gallery.json")))
            {
                result = sr.ReadToEnd();
            }

            if (string.IsNullOrEmpty(result))
            {
                NotFound();
            }
            return Ok(result);
        }
    }
}
