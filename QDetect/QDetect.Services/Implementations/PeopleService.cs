using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QDetect.Data;
using QDetect.Data.Models;
using QDetect.Services.Interfaces;

namespace QDetect.Services.Implementations
{
    public class PeopleService : IPeopleService
    {
        private readonly QDetectDbContext context;

        public PeopleService(QDetectDbContext context)
        {
            this.context = context;
        }

        public async Task<Person> AddAsync(string name, string link, string ucn, string city, List<double> embedding, DateTime quarantine)
        {

            var person = new Person
            {
                Name = name,
                City = city,
                UCN = ucn,
                QuarantineEndDate = quarantine,
            };

            var image = new Image
            {
                Link = link
            };

            await context.Images.AddAsync(image);

            person.Images.Add(new PersonImage
            {
                Image = image,
                Person = person
            });

            await context.Persons.AddAsync(person);

            var embed = new Embedding
            {
                Image = image,
                Person = person
            };

            image.Embeddings.Add(embed);
            context.Images.Update(image);

            embed.Values = embedding.Select((e, index) => new EmbeddingValue
            {
                EmbeddingId = embed.Id,
                Value = e,
                Index = index
            }).ToList();

            context.Embeddings.Add(embed);


            await context.Persons.AddAsync(person);
            await context.SaveChangesAsync();

            return person;
        }

        public async Task EditAsync(int id, string name, string ucn, string city, DateTime quarantine)
        {
            if (!await ContainsUserAsync(id))
            {
                throw new ArgumentException("Invalid person id");
            }

            var person = await context.Persons.FirstAsync(p => p.Id == id);

            person.Name = name;
            person.UCN = ucn;
            person.QuarantineEndDate = quarantine;
            person.City = city;

            context.Persons.Update(person);
            await context.SaveChangesAsync();
        }

        public async Task<Person> GetAsync(int id)
        {
            if (!await ContainsUserAsync(id))
            {
                throw new ArgumentException("Invalid person id");
            }

            return await context.Persons.FirstAsync(p => p.Id == id);
        }

        public IQueryable<Person> GetAll()
        {
            return context.Persons;
        }

        public async Task<string> GetPersonImageLink(int id)
        {
            if (!await ContainsUserAsync(id))
            {
                throw new ArgumentException("Invalid person id");
            }

            var person = await context.Persons.FirstAsync(p => p.Id == id);

            return person.Images.FirstOrDefault().Image.Link;
        }

        public async Task<bool> ContainsUserAsync(int id)
        {
            return await context.Persons.AnyAsync(p => p.Id == id);
        }
    }
}
