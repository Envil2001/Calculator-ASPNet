using Microsoft.EntityFrameworkCore;
using WebApp.Entities;

namespace WebApp;

public class AppDbContext : DbContext
{
    public DbSet<ContactEntity> Contacts { get; set; }
    public DbSet<OrganizationEntity> Organizations { get; set; }
    private string DbPath { get; set; }

    public AppDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "contacts.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={DbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContactEntity>()
            .Property(e => e.OrganizationId)
            .HasDefaultValue(101);

        modelBuilder.Entity<ContactEntity>().HasData(
            new ContactEntity { Id = 1, Name = "Adam", Email = "adam@wsei.edu.pl", Phone = "127813268163", Birth = new DateTime(2000, 10, 10), OrganizationId = 101 },
            new ContactEntity { Id = 2, Name = "Ewa", Email = "ewa@wsei.edu.pl", Phone = "293443823478", Birth = new DateTime(1999, 8, 10), OrganizationId = 102 }
        );

        modelBuilder.Entity<OrganizationEntity>()
            .OwnsOne(e => e.Address);

        modelBuilder.Entity<OrganizationEntity>()
            .ToTable("organizations")
            .HasData(
                new OrganizationEntity { Id = 101, Title = "WSEI", Nip = "83492384", Regon = "13424234" },
                new OrganizationEntity { Id = 102, Title = "Firma", Nip = "2498534", Regon = "0873439249" }
            );

        modelBuilder.Entity<OrganizationEntity>()
            .OwnsOne(e => e.Address)
            .HasData(
                new { OrganizationEntityId = 101, City = "Kraków", Street = "Św. Filipa 17", PostalCode = "31-150", Region = "małopolskie" },
                new { OrganizationEntityId = 102, City = "Kraków", Street = "Krowoderska 45/6", PostalCode = "31-150", Region = "małopolskie" }
            );
    }
}