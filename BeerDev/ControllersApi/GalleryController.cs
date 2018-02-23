using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;
using System.Web.Http;
using BeerDev.Entities;
using BeerDev.Repository.Interfaces;
using BeerDev.Repository.Repositories;
using BeerDev.Util;

namespace BeerDev.ControllersApi
{
    public class GalleryController : ApiController
    {
        private readonly IPictureGalleryRepository _repository;
        public GalleryController()
        {
            _repository = new PictureRepository();
        }

        public IHttpActionResult Get()
        {
            IEnumerable<PictureGallery> pics = _repository.GetAll();
            string result = JsonHelper<PictureGallery>.Serialize(pics);

            if (string.IsNullOrEmpty(result))
            {
                NotFound();
            }
            return Ok(result);
        }
    }
}
