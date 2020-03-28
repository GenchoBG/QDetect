using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using QDetect.Data.Models;

namespace QDetect.Web.BindingModels
{
    public class PersonInfoBindingModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Enter Valid UCN")]
        public string UCN { get; set; }

        [Required]
        [MaxLength(30)]
        public string City { get; set; }

        public string QuarantineEndDate { get; set; }
    }
}
