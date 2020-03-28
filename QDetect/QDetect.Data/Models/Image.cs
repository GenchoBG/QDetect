using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QDetect.Data.Models
{
    public class Image
    {
        public int Id { get; set; }

        [Required]
        public string Link { get; set; }

        public ICollection<Embedding> Embeddings { get; set; } = new HashSet<Embedding>();

        public ICollection<PersonImage> Persons { get; set; } = new HashSet<PersonImage>();
    }
}
