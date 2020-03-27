using System.ComponentModel.DataAnnotations;

namespace QDetect.Data.Models
{
    public class PersonImage
    {
        public int PersonId { get; set; }

        [Required]
        public Person Person { get; set; }

        public int ImageId { get; set; }

        [Required]
        public Image Image { get; set; }
    }
}
