using Microsoft.EntityFrameworkCore;
using SystemConfigurator.Model.UMA;
using SystemConfigurator.Model.UMA.LOV;

namespace SystemConfigurator.Config.Database;

public class SystemDbUMAContext : DbContext
{
    public SystemDbUMAContext(DbContextOptions<SystemDbUMAContext> options) : base(options){}

    public DbSet<User> Users { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<DeviceType> DeviceTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
        .HasIndex(userEntity => userEntity.Email)
        .IsUnique();

        modelBuilder.Entity<Device>()
            .HasOne(deviceEntity => deviceEntity.User)
            .WithMany(userEntity => userEntity.Devices)
            .HasForeignKey(deviceEntity => deviceEntity.UserId);

        modelBuilder.Entity<Device>()
            .HasOne(e => e.DeviceType)
            .WithMany(e => e.Devices)
            .HasForeignKey(e => e.DeviceTypeId);
    }

    // Define DbSets for your entities here
    // public DbSet<YourEntity> YourEntities { get; set; }
}