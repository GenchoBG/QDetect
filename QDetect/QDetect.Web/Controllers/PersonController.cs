using System.Linq;
using System;
using System.Collections.Generic;
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
        private readonly IReportService reportService;

        public PersonController(ICloudinaryService cloudinaryService, IPeopleService peopleService, IReportService reportService)
        {
            this.cloudinaryService = cloudinaryService;
            this.peopleService = peopleService;
            this.reportService = reportService;
        }

        [HttpGet]   
        public async Task<IActionResult> Upload()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(PersonUploadBindingModel bindingModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var link = await this.cloudinaryService.UploadPictureAsync(bindingModel.Image, Guid.NewGuid().ToString());

            var person = await this.peopleService.AddAsync(bindingModel.Name, link, bindingModel.UCN, bindingModel.City, bindingModel.Embedding.ToList(), bindingModel.Quarantine);

            return this.Ok(person.Id);
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
                HasReports = person.Reports.Any(r => !r.IsArchived),
                Image = await this.peopleService.GetPersonImageLink(person.Id),
                Name = person.Name,
                QuanratineEndDate = person.QuarantineEndDate.ToLocalTime().ToLongDateString(),
                UCN = person.UCN
            };

            var reports = this.reportService.GetByPersonId(person.Id).Select(r => new ReportViewModel
            {
                Id = r.Id,
                ImageLink = r.Image.Link
            }).ToList();

            viewModel.Reports = reports;

            return this.Json(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PersonInfoBindingModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.NotFound();
            }

            var newDate = DateTime.Parse(input.QuarantineEndDate);

            await peopleService.EditAsync(input.Id, input.Name, input.UCN, input.City, newDate);

            return this.Ok();
        }
    }
}
