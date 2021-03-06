﻿using System;
using System.ComponentModel.DataAnnotations;

namespace QDetect.Data.Models
{
    public class Report
    {
        public int Id { get; set; }

        public int ImageId { get; set; }

        [Required]
        public Image Image { get; set; }

        public DateTime Date { get; set; }

        public int PersonId { get; set; }
        
        [Required]
        public Person Person { get; set; }

        public bool IsArchived { get; set; }
    }
}
