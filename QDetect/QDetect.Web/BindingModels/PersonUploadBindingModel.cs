using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace QDetect.Web.BindingModels
{
    public class PersonUploadBindingModel
    {
        public string Name { get; set; }

        public IFormFile Image { get; set; }

        public string UCN { get; set; }

        public string City { get; set; }

        public List<double> Embedding { get; set; }

        public DateTime Quarantine { get; set; }
    }
}
