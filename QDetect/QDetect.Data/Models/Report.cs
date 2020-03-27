using System;

namespace QDetect.Data.Models
{
    public class Report
    {
        public int Id { get; set; }

        public string Image { get; set; }

        public DateTime Date { get; set; }

        public int PersonId { get; set; }

        public Person Person { get; set; }

        public bool IsArchived { get; set; }
    }
}
