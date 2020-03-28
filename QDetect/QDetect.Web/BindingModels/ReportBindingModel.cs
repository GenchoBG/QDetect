using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace QDetect.Web.BindingModels
{
    public class ReportBindingModel
    {
        public string Link { get; set; }

        public IFormFile Image { get; set; }

        public IList<IList<double>> Embeddings { get; set; }
    }
}
