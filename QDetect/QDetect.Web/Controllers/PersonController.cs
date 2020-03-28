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

        public async Task<IActionResult> Upload()
        {
            return this.View();
        }
    }
}
