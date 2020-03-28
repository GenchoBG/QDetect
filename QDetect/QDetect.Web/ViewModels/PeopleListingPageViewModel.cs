using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QDetect.Web.ViewModels
{
    public class PeopleListingPageViewModel
    {
        public IList<PeopleViewModel> People { get; set; } = new List<PeopleViewModel>();
    }
}
