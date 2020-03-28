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
    public class ImageReceiverService : IImageReceiverService
    {
        private const double SimilarityThreshhold = 13;

        private readonly QDetectDbContext db;
        private readonly IReportService reportService;

        public ImageReceiverService(QDetectDbContext db)
        {
            this.db = db;
        }

        public async Task ProcessAsync(string link, IList<IList<double>> embeddings)
        {
            var image = new Image()
            {
                Link = link
            };

            foreach (var embedding in embeddings)
            {
                var (mostSimilar, distance) = FindMostSimilarImage(embedding);

                if (distance <= SimilarityThreshhold)
                {
                    var person = mostSimilar.Embeddings.OrderBy(e => this.EucledianDistance(embedding, e)).First().Person;

                    var embeddingRecord = new Embedding()
                    {
                        Image = image,
                        Person = person
                    };

                    for (int i = 0; i < embedding.Count; i++)
                    {
                        embeddingRecord.Values.Add(new EmbeddingValue()
                        {
                            Index = i,
                            Value = embedding[i]
                        });
                    }

                    image.Embeddings.Add(embeddingRecord);

                    var report = new Report()
                    {
                        Date = DateTime.Now,
                        Image = image,
                        IsArchived = false,
                        Person = person
                    };

                    person.Images.Add(new PersonImage()
                    {
                        Person = person,
                        Image = image
                    });

                    await this.db.Reports.AddAsync(report);
                    await this.db.SaveChangesAsync();
                }
            }
        }

        private (Image, double) FindMostSimilarImage(IList<double> embedding)
        {
            if (!this.db.Images.Any())
            {
                return (null, double.MaxValue);
            }

            var mostSimilar = this.db
                                    .Images
                                    .Include(i => i.Embeddings)
                                    .ThenInclude(e => e.Values)
                                    .ToList()
                                    .OrderBy(i =>
                                        i.Embeddings.Max(e => this.EucledianDistance(embedding, e)))
                                    .First();

            return (mostSimilar, mostSimilar.Embeddings.Max(e => this.EucledianDistance(embedding, e)));
        }

        private double EucledianDistance(IList<double> embedding1, Embedding embedding2)
        {
            var values = embedding2.Values.OrderBy(v => v.Index).Select(v => v.Value).ToList();

            return this.EucledianDistance(embedding1, values);
        }

        private double EucledianDistance(IList<double> embedding1, IList<double> embedding2)
        {
            var sum = 0D;
            for (int i = 0; i < embedding1.Count; i++)
            {
                sum += Math.Pow(embedding1[i] - embedding2[i], 2);
            }

            return Math.Sqrt(sum);
        }
    }
}
