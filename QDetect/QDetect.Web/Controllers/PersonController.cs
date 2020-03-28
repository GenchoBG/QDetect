using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QDetect.Services.Interfaces;
using QDetect.Web.BindingModels;

namespace QDetect.Web.Controllers
{
    public class PersonController : Controller
    {
        private readonly ICloudinaryService cloudinaryService;
        private readonly IPeopleService peopleService;

        public PersonController(ICloudinaryService cloudinaryService, IPeopleService peopleService)
        {
            this.cloudinaryService = cloudinaryService;
            this.peopleService = peopleService;
        }

        [HttpGet]
        public async Task<IActionResult> Upload()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(PersonUploadBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var link = await this.cloudinaryService.UploadPictureAsync(model.Image, Guid.NewGuid().ToString());

            var person = await this.peopleService.AddAsync(model.Name, link, model.UCN, model.City, model.Embedding, model.Quarantine);

            return this.Ok(person);
        }
    }
}
