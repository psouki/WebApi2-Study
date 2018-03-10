using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using BeerDev.Entities;
using BeerDev.Repository.Interfaces;
using BeerDev.Repository.Repositories;

namespace BeerDev.Api.Controllers
{
    [EnableCors("*", "*", "GET")]
    public class GalleryPicController : ApiController
    {
        private readonly IPictureGalleryRepository _repository;
        public GalleryPicController()
        {
            _repository = new PictureRepository();
        }

        public IHttpActionResult Get()
        {
            IEnumerable<PictureGallery> result = _repository.GetAll();

            if (result == null)
            {
                NotFound();
            }
            return Ok(result);
        }
    }
}
