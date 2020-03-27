using Microsoft.EntityFrameworkCore;

namespace QDetect.Data
{
    public class QDetectDbContext : DbContext
    {
        public QDetectDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        }
    }
}
