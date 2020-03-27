using System;
using System.Linq;
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

        public Report Add(int personId, int imageId)
        {
            if (!context.Persons.Any(p => p.Id == personId))
            {
                throw new ArgumentException("Invalid person id");
            }

            if (!context.Images.Any(i => i.Id == imageId))
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

            context.Reports.Add(report);
            context.SaveChanges();

            return report;
        }

        public void Delete(int id)
        {
            if (!context.Reports.Any(r => r.Id == id))
            {
                throw new ArgumentException("Invalid report id");
            }

            var report = context.Reports.First(r => r.Id == id);

            context.Reports.Remove(report);
            context.SaveChanges();
        }

        public void Archive(int id)
        {
            if (!context.Reports.Any(r => r.Id == id))
            {
                throw new ArgumentException("Invalid report id");
            }

            var report = context.Reports.First(r => r.Id == id);
            report.IsArchived = true;

            context.Reports.Update(report);
            context.SaveChanges();
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
