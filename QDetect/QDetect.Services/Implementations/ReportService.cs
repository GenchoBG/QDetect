using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QDetect.Data;
using QDetect.Data.Models;
using QDetect.Services.Interfaces;

namespace QDetect.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly QDetectDbContext context;

        public ReportService(QDetectDbContext context)
        {
            this.context = context;
        }

        public Task<bool> ContainsAsync(int id)
        {
            return context.Reports.AnyAsync(r => r.Id == id);
        }

        public async Task<Report> AddAsync(int personId, int imageId)
        {
            if (!await context.Persons.AnyAsync(p => p.Id == personId))
            {
                throw new ArgumentException("Invalid person id");
            }

            if (!await context.Images.AnyAsync(i => i.Id == imageId))
            {
                throw new ArgumentException("Invalid image id");
            }

            var report = new Report
            {
                ImageId = imageId,
                Image = context.Images.First(i => i.Id == imageId),
                PersonId = personId,
                Person = context.Persons.First(p => p.Id == personId),
                Date = DateTime.UtcNow,
                IsArchived = false,
            };

            await context.Reports.AddAsync(report);
            await context.SaveChangesAsync();

            return report;
        }

        public async Task DeleteAsync(int id)
        {
            if (!await context.Reports.AnyAsync(r => r.Id == id))
            {
                throw new ArgumentException("Invalid report id");
            }

            var report = await context.Reports.FirstAsync(r => r.Id == id);

            context.Reports.Remove(report);
            await context.SaveChangesAsync();
        }

        public async Task ArchiveAsync(int id)
        {
            if (!await context.Reports.AnyAsync(r => r.Id == id))
            {
                throw new ArgumentException("Invalid report id");
            }

            var report = await context.Reports.FirstAsync(r => r.Id == id);
            report.IsArchived = true;

            context.Reports.Update(report);
            await context.SaveChangesAsync();
        }

        public IQueryable<Report> GetByPersonId(int personId)
        {
            if (!context.Persons.Any(p => p.Id == personId))
            {
                throw new ArgumentException("Invalid person id");
            }

            return context.Reports
                .Where(r => r.PersonId == personId && !r.IsArchived)
                .Include(r => r.Person)
                .Include(r => r.Image);
        }
    }
}
