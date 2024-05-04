using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using PosTech.TechChallenge.Contacts.Domain;

namespace PosTech.TechChallenge.Contacts.Infra.Context;

public class AplicationDbContext : DbContext
{
    private readonly string _connectionString;

    public AplicationDbContext()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public AplicationDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DbSet<Contact> Contact { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.ToTable("Contacts");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR(250)")
                .IsRequired()
                .HasMaxLength(250);

            entity.Property(e => e.DDD)
                .HasColumnName("DDD")
                .HasColumnType("TINYINT")
                .IsRequired()
                .HasMaxLength(2);

            entity.Property(e => e.PhoneNumber)
                .HasColumnName("PhoneNumber")
                .HasColumnType("NVARCHAR(9)")
                .IsRequired()
                .HasMaxLength(9);

            entity.Property(e => e.Email)
                .HasColumnName("Email")
                .HasColumnType("NVARCHAR(250)")
                .IsRequired()
                .HasMaxLength(250);
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
