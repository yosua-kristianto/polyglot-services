using Microsoft.EntityFrameworkCore;
using SystemConfigurator.Model.CMA;
using SystemConfigurator.Model.CMA.LOV;

namespace SystemConfigurator.Config.Database;

public class SystemDbCMAContext : DbContext
{
    public SystemDbCMAContext(DbContextOptions<SystemDbCMAContext> options) : base(options) { }

    // Main Entity
    public DbSet<Customer> Customers { get; set; }

    // LOV (List of Values) Entities
    public DbSet<Gender> Genders { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<MaritalStatus> MaritalStatuses { get; set; }
    public DbSet<IdentityType> IdentityTypes { get; set; }
    public DbSet<Nationality> Nationalities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>()
            .HasIndex(e => e.Email)
            .IsUnique();

        modelBuilder.Entity<Customer>()
            .HasIndex(e => e.AccountNo)
            .IsUnique();

        modelBuilder.Entity<Customer>()
            .HasOne(e => e.Gender)
            .WithMany(e => e.Customers)
            .HasForeignKey(e => e.GenderId);

        modelBuilder.Entity<Customer>()
            .HasOne(e => e.Title)
            .WithMany(e => e.Customers)
            .HasForeignKey(e => e.TitleId);

        modelBuilder.Entity<Customer>()
            .HasOne(e => e.MaritalStatus)
            .WithMany(e => e.Customers)
            .HasForeignKey(e => e.MaritalStatusId);

        modelBuilder.Entity<Customer>()
            .HasOne(e => e.IdentityType)
            .WithMany(e => e.Customers)
            .HasForeignKey(e => e.IdentityTypeId);

        // Customer -> Nationality relationship
        modelBuilder.Entity<Customer>()
            .HasOne(e => e.Nationality)
            .WithMany(e => e.Customers)
            .HasForeignKey(e => e.NationalityId);
    }
}