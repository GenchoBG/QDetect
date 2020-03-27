using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QDetect.Data.Models
{
    public class Person
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

        public DateTime QuarantineEndDate { get; set; }

        public ICollection<Report> Reports { get; set; } = new HashSet<Report>();

        public ICollection<Image> Images { get; set; } = new HashSet<Image>();
    }
}
