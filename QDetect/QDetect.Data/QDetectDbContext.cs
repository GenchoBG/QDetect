using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using QDetect.Data.Models;

namespace QDetect.Data
{
    public class QDetectDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public DbSet<Report> Reports { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Embedding> Embeddings { get; set; }

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
                .HasForeignKey(r => r.PersonId);

            modelBuilder.Entity<Report>()
                .HasOne<Image>()
                .WithMany()
                .HasForeignKey(r => r.ImageId);

            modelBuilder.Entity<Embedding>()
                .HasOne(e => e.Person)
                .WithMany()
                .HasForeignKey(e => e.PersonId);

            modelBuilder.Entity<Person>()
                .HasMany(p => p.Images)
                .WithOne(pi => pi.Person)
                .HasForeignKey(pi => pi.PersonId);

            modelBuilder.Entity<Image>()
                .HasMany<PersonImage>()
                .WithOne(pi => pi.Image)
                .HasForeignKey(pi => pi.ImageId);
            
            modelBuilder.Entity<Embedding>()
                .HasMany(e => e.Values)
                .WithOne(v => v.Embedding)
                .HasForeignKey(ev => ev.EmbeddingId);

            modelBuilder.Entity<Image>()
                .HasMany(i => i.Embeddings)
                .WithOne(e => e.Image)
                .HasForeignKey(e => e.ImageId);

            modelBuilder.Entity<PersonImage>()
                .HasKey(pi => new { pi.ImageId, pi.PersonId });
        }
    }
}
