using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QDetect.Data.Models
{
    public class Embedding
    {
        public int Id { get; set; }

        public int ImageId { get; set; }

        [Required]
        public Image Image { get; set; }

        public int PersonId { get; set; }
        
        [Required]
        public Person Person { get; set; }

        public ICollection<EmbeddingValue> Values { get; set; } = new HashSet<EmbeddingValue>();
    }
}
