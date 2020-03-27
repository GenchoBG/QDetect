using QDetect.Data.Models;
using System.Linq;

namespace QDetect.Services.Interfaces
{
    public interface IReportService
    {
        Report Add(int personId, int imageId);
        void Delete(int id);
        void Archive(int id);
        IQueryable<Report> GetByPersonId(int personId);
    }
}
