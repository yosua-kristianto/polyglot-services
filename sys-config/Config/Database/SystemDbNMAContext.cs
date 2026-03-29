using System;
using Microsoft.EntityFrameworkCore;
using SysConfig.Model.NMA;
using SysConfig.Model.NMA.LOV;

namespace SystemConfigurator.Config.Database;

public class SystemDbNMAContext : DbContext
{
    public SystemDbNMAContext(DbContextOptions<SystemDbNMAContext> options) : base(options){}

    public DbSet<PersonalNotification> PersonalNotifications { get; set; }
    public DbSet<NotificationPlatformType> NotificationPlatformTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PersonalNotification>()
            .HasOne(notification => notification.PlatformType)
            .WithMany(platformType => platformType.PersonalNotifications)
            .HasForeignKey(notification => notification.NotificationPlatformType);
    }

}
