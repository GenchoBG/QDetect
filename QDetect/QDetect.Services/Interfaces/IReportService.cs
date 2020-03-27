using System;
using System.Collections.Generic;
using System.Text;

namespace QDetect.Services.Interfaces
{
    public interface IReportService
    {
        void Add();
        void Delete();
        void Archive();
        void GetByUser();
    }
}
