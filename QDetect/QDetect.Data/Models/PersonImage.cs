using System;
using System.Collections.Generic;
using System.Text;

namespace QDetect.Data.Models
{
    public class PersonImage
    {
        public int PersonId { get; set; }

        public Person Person { get; set; }

        public int ImageId { get; set; }

        public Image Image { get; set; }
    }
}
