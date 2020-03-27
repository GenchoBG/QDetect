using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QDetect.Data.Models;
using QDetect.Services.Interfaces;

namespace QDetect.Services.Implementations
{
    public class PeopleService : IPeopleService
    {
        public Task<Person> AddAsync(string name, string link, string ucn, string city, List<double> embedding, DateTime quarantine)
        {
            throw new NotImplementedException();
        }

        public Task UpdateQuarantineAsync(int id, DateTime quarantine)
        {
            throw new NotImplementedException();
        }

        public Task<Person> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Person> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
