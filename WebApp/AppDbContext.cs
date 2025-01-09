using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Entities;

namespace WebApp;

public class AppDbContext : IdentityDbContext<IdentityUser>
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
        base.OnModelCreating(modelBuilder); // Вызов базового метода для Identity

        // Конфигурация для ContactEntity
        modelBuilder.Entity<ContactEntity>()
            .Property(e => e.OrganizationId)
            .HasDefaultValue(101);

        modelBuilder.Entity<ContactEntity>().HasData(
            new ContactEntity { Id = 1, Name = "Adam", Email = "adam@wsei.edu.pl", Phone = "127813268163", Birth = new DateTime(2000, 10, 10), OrganizationId = 101 },
            new ContactEntity { Id = 2, Name = "Ewa", Email = "ewa@wsei.edu.pl", Phone = "293443823478", Birth = new DateTime(1999, 8, 10), OrganizationId = 102 }
        );

        // Конфигурация для OrganizationEntity
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

        // Добавление ролей и пользователей для Identity
        string ADMIN_ID = Guid.NewGuid().ToString();
        string ROLE_ID = Guid.NewGuid().ToString();

        // Добавление роли администратора
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Name = "admin",
            NormalizedName = "ADMIN",
            Id = ROLE_ID,
            ConcurrencyStamp = ROLE_ID
        });

        // Создание пользователя-администратора
        var admin = new IdentityUser
        {
            Id = ADMIN_ID,
            Email = "admin@example.com",
            EmailConfirmed = true,
            UserName = "admin",
            NormalizedUserName = "ADMIN@EXAMPLE.COM",
            NormalizedEmail = "ADMIN@EXAMPLE.COM"
        };

        // Хеширование пароля
        PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
        admin.PasswordHash = ph.HashPassword(admin, "Admin123!");

        // Сохранение пользователя
        modelBuilder.Entity<IdentityUser>().HasData(admin);

        // Назначение роли администратора пользователю
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = ROLE_ID,
            UserId = ADMIN_ID
        });

        // Добавление второго пользователя с ролью USER
        string USER_ID = Guid.NewGuid().ToString();
        var user = new IdentityUser
        {
            Id = USER_ID,
            Email = "user@wsei.edu.pl",
            EmailConfirmed = true,
            UserName = "user",
            NormalizedUserName = "USER",
            NormalizedEmail = "USER@WSEI.EDU.PL"
        };

        user.PasswordHash = ph.HashPassword(user, "User123!");

        modelBuilder.Entity<IdentityUser>().HasData(user);

        // Добавление роли USER
        string USER_ROLE_ID = Guid.NewGuid().ToString();
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Name = "user",
            NormalizedName = "USER",
            Id = USER_ROLE_ID,
            ConcurrencyStamp = USER_ROLE_ID
        });

        // Назначение роли USER пользователю
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = USER_ROLE_ID,
            UserId = USER_ID
        });
    }
}