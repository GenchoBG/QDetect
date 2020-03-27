using QDetect.Data.Models;
using System.Linq;

namespace QDetect.Services.Interfaces
{
    public interface IReportService
    {
        Report Add(int personId, int embeddingId, int imageId);
        void Delete(int id);
        void Archive(int id);
        IQueryable<Report> GetByUser(int userId);
    }
}
