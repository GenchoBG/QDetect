using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QDetect.Services.Interfaces;

namespace QDetect.Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService reportService;

        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public Task<IActionResult> Create(int personId, int imageId)
        {
            return this.View();
        }

    }
}
