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

        Task EditAsync(int id, string name, string ucn, string city, DateTime quarantine);

        Task<Person> GetAsync(int id);

        IQueryable<Person> GetAll();
    }
}
