using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QDetect.Web.ViewModels
{
    public class PeopleViewModel
    {
        public string Name { get; set; }

        public string UCN { get; set; }

        public string City { get; set; }

        public string QuanratineEndDate { get; set; }

        public string Image { get; set; }

        public bool HasReports { get; set; }
    }
}
