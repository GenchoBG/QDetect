using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using QDetect.Data.Models;

namespace QDetect.Data
{
    public class QDetectDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public DbSet<Report> Reports { get; set; }

        public DbSet<Image> Embeddings { get; set; }

        public DbSet<EmbeddingValue> EmbeddingValues { get; set; }

        public QDetectDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>()
                .HasMany(p => p.Reports)
                .WithOne(r => r.Person)
                .HasForeignKey(r => r.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Image>()
                .HasOne(e => e.Person)
                .WithMany(p => p.Images)
                .HasForeignKey(e => e.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Image>()
                .HasMany(e => e.Values)
                .WithOne(v => v.Embedding)
                .HasForeignKey(ev => ev.EmbeddingId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
