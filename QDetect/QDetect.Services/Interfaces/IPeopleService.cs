using QDetect.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QDetect.Services.Interfaces
{
    public interface IPeopleService
    {
        Task<Person> AddAsync(string name, string link, string ucn, string city, List<double> embedding, DateTime quarantine);

        Task UpdateQuarantineAsync(int id, DateTime quarantine);

        Task<Person> GetAsync(int id);

        IQueryable<Person> GetAll();
    }
}
