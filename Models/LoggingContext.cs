using Microsoft.EntityFrameworkCore;

namespace appLogger.Models
{
    public class LoggingContext : DbContext
    {
        // Constructor for dynamic options (e.g., dynamic connection string)
        public LoggingContext(DbContextOptions<LoggingContext> options)
            : base(options)
        {
        }

        // DbSet for logs
        public DbSet<Logging> Loggings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Logging>(entity =>
            {
                entity.ToTable("logging"); // lowercase table name
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Timestamp).HasColumnName("timestamp");
                entity.Property(e => e.Level).HasColumnName("level");
                entity.Property(e => e.Message).HasColumnName("message");
                entity.Property(e => e.Username).HasColumnName("username");
            });
        }
    }
}
