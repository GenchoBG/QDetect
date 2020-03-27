using QDetect.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace QDetect.Services.Interfaces
{
    public interface IReportService
    {
        Task<Report> AddAsync(int personId, int imageId);

        Task DeleteAsync(int id);

        Task ArchiveAsync(int id);

        IQueryable<Report> GetByPersonId(int personId);
    }
}
