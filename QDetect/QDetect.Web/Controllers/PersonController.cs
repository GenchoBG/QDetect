using System.Linq;
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

        public async Task<IActionResult> Upload()
        {
            return this.View();
        }

        public async Task<IActionResult> All()
        {
            var peoples = await peopleService.GetAll().Select(p => new PeopleViewModel
            {
                Name = p.Name,
                City = p.City,
                Image = p.Images.FirstOrDefault().Image.Link,
                QuanratineEndDate = p.QuarantineEndDate.ToLocalTime().ToString(),
                UCN = p.UCN,
                HasReports = p.Reports.Any()
            }).ToListAsync();

            var viewModel = new PeopleListingPageViewModel();

            viewModel.Peoples = peoples;

            return this.View(viewModel);
        }
    }
}
