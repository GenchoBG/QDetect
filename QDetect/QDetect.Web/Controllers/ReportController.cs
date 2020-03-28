using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QDetect.Services.Interfaces;

namespace QDetect.Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly IImageReceiverService imageReceiverService;
        private readonly IReportService reportService;
        private readonly ICloudinaryService cloudinaryService;

        public ReportController(IImageReceiverService imageReceiverService, IReportService reportService, ICloudinaryService cloudinaryService)
        {
            this.imageReceiverService = imageReceiverService;
            this.reportService = reportService;
            this.cloudinaryService = cloudinaryService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile image, IList<IList<double>> embeddings)
        {
            var link = await this.cloudinaryService.UploadPictureAsync(image, Guid.NewGuid().ToString());

            await this.imageReceiverService.ProcessAsync(link, embeddings);

            return this.Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Archive(int id)
        {
            if (!await reportService.ContainsAsync(id))
            {
                return Redirect("/Home/Index");
            }

            await reportService.ArchiveAsync(id);

            return this.Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await reportService.ContainsAsync(id))
            {
                return Redirect("/Home/Index");
            }

            await reportService.DeleteAsync(id);

            return this.Ok();
        }
    }
}
