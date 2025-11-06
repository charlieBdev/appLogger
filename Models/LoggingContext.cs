using Microsoft.EntityFrameworkCore;

namespace appLogger.Models
{
    public class LoggingContext : DbContext
    {
        private readonly string _connectionString;

        public LoggingContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<Logging> Loggings { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Logging>(entity =>
            {
                entity.ToTable("logging"); // lowercase table name
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Timestamp).HasColumnName("timestamp");
                entity.Property(e => e.Level).HasColumnName("level");
                entity.Property(e => e.Message).HasColumnName("message");
            });
        }
    }
}
