using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QDetect.Services.Interfaces;
using QDetect.Web.BindingModels;
using QDetect.Web.ViewModels;

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

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var reports = this.reportService.GetByPersonId(id).ToList();

            var reportsModel = new List<ReportViewModel>();

            foreach (var report in reports)
            {
                reportsModel.Add(new ReportViewModel()
                {
                    Id = report.Id,
                    ImageLink = report.Image.Link
                });
            }

            return this.View(reportsModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReportBindingModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Link) || !model.Embeddings.Any())
            {
                return this.BadRequest();
            }

            var embeddingsArray = model.Embeddings.Select(e => e.ToArray()).ToArray();

            var embeddings = new List<IList<double>>(embeddingsArray[0].Length);

            if (embeddingsArray[0].Length != 128)
            {
                var flattened = model.Embeddings.SelectMany(l => l).ToArray();

                var current = new List<double>();

                for (int i = 0; i <= flattened.Length; i++)
                {
                    if (i != 0 && i % 128 == 0)
                    {
                        embeddings.Add(current);
                        current = new List<double>();
                        if (i == flattened.Length)
                        {
                            break;
                        }
                    }

                    current.Add(flattened[i]);
                }

                await this.imageReceiverService.ProcessAsync(model.Link, embeddings);
            }
            else
            {
                await this.imageReceiverService.ProcessAsync(model.Link, model.Embeddings);
            }
            

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
