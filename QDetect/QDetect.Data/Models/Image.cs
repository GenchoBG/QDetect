using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QDetect.Data.Models
{
    public class Image
    {
        public int Id { get; set; }

        [Required]
        public string Link { get; set; }

        [Required]
        public int PersonId { get; set; }

        public Person Person { get; set; }

        public ICollection<EmbeddingValue> Values { get; set; } = new HashSet<EmbeddingValue>();
    }
}
