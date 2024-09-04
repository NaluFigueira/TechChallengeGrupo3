using Microsoft.EntityFrameworkCore;

using PosTech.TechChallenge.Contacts.Query.Domain.Entities;
using PosTech.TechChallenge.Contacts.Query.Domain.Enum;

namespace PosTech.TechChallenge.Contacts.Query.Infra.Context;

public class ContactDbContext : DbContext
{

    public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options)
    {
    }

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

        modelBuilder.Entity<Contact>().HasData([
            new Contact() {
                Id = Guid.NewGuid(),
                DDD = DDDBrazil.DDD_43,
                Email = "pedro-ferreira85@yahoo.com.br",
                Name = "Pedro Henrique Erick Ferreira",
                PhoneNumber = "989340101"
            },
            new Contact() {
                Id = Guid.NewGuid(),
                DDD = DDDBrazil.DDD_63,
                Email = "thomas.pires@credendio.com.br",
                Name = "Thomas Vinicius João Pires",
                PhoneNumber = "989769978"
            },
            new Contact() {
                Id = Guid.NewGuid(),
                DDD = DDDBrazil.DDD_11,
                Email = "julia92@casabellavidros.com.br",
                Name = "Julia Milena Rita Almeida",
                PhoneNumber = "998212236"
            },
            new Contact() {
                Id = Guid.NewGuid(),
                DDD = DDDBrazil.DDD_21,
                Email = "bianca_assis@4now.com.br",
                Name = "Bianca Liz Assis",
                PhoneNumber = "992804701"
            },
            new Contact() {
                Id = Guid.NewGuid(),
                DDD = DDDBrazil.DDD_32,
                Email = "alessandra75@jovempanfmtaubate.com.br",
                Name = "Alessandra Gabrielly Esther Costa",
                PhoneNumber = "985537746"
            },
        ]);
    }

    public DbSet<Contact> Contact { get; set; }
}
