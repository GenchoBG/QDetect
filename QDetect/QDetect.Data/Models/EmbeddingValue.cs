using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QDetect.Data.Models
{
    public class EmbeddingValue
    {
        public int Id { get; set; }
        
        public int EmbeddingId { get; set; }

        [Required]
        public Embedding Embedding { get; set; }

        public int Index { get; set; }

        public double Value { get; set; }
    }
}
