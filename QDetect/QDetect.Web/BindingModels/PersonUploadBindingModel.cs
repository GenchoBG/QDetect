using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace QDetect.Web.BindingModels
{
    public class PersonUploadBindingModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        [Required]
        public string UCN { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public double[] Embedding { get; set; }

        [Required]
        public DateTime Quarantine { get; set; }
    }
}
