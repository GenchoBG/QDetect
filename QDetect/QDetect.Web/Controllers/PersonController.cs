using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QDetect.Services.Interfaces;
using QDetect.Web.BindingModels;
using QDetect.Web.ViewModels;

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

        public async Task<IActionResult> All()
        {
            var peoples = await peopleService.GetAll().Select(p => new PeopleViewModel
            {
                Id = p.Id,
                Name = p.Name,
                City = p.City,
                QuarantineEndDate = p.QuarantineEndDate.ToLocalTime().ToString(),
                UCN = p.UCN,
                HasReports = p.Reports.Any()
            }).ToListAsync();

            foreach (var person in peoples)
            {
                person.Image = await peopleService.GetPersonImageLink(person.Id);
            }

            var viewModel = new PeopleListingPageViewModel();

            viewModel.Peoples = peoples;

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Info(int id)
        {
            if (!await peopleService.ContainsUserAsync(id))
            {
                return Redirect("/Home/Index");
            }

            var person = await peopleService.GetAsync(id);

            var viewModel = new PeopleInfoViewModel
            {
                Id = person.Id,
                City = person.City,
                HasReports = person.Reports.Any(),
                Image = person.Images.FirstOrDefault().Image.Link,
                Name = person.Name,
                QuanratineEndDate = person.QuarantineEndDate.ToLocalTime().ToString(),
                UCN = person.UCN
            };

            viewModel.Reports = person.Reports.Select(r => new ReportViewModel
            {
                Id = r.Id,
                ImageLink = r.Image.Link
            }).ToList();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PersonInfoBindingModel input)
        {
            if (!ModelState.IsValid)
            {
                return Redirect($"/People/Info?id={input.Id}");
            }

            var newDate = DateTime.Parse(input.QuarantineEndDate);

            await peopleService.EditAsync(input.Id, input.Name, input.UCN, input.City, newDate);

            return Redirect($"/People/Info?id={input.Id}");
        }
    }
}
