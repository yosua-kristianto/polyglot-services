using System;
using System.Data.Common;
using SysConfig.Model.NMA.LOV;
using SystemConfigurator.Config.Database;
using SystemConfigurator.Config.Database.Seeder;

namespace SystemConfigurator.Config.Database.Seeder.NMA;

public class PersonalNotificationSeeder : MainSeeder
{
    private SystemDbNMAContext _dbCtx;

    public PersonalNotificationSeeder(SystemDbNMAContext ctx): base(ctx){ this._dbCtx = ctx; }

    public async Task Seed()
    {
        if (!this._dbCtx.NotificationPlatformTypes.Any())
        {
            this._dbCtx.NotificationPlatformTypes.AddRange([
                new NotificationPlatformType(){Id = "WHATSAPP"},
                new NotificationPlatformType(){Id = "SMS"},
                new NotificationPlatformType(){Id = "MAIL"},
                new NotificationPlatformType(){Id = "OTP"},
            ]);

            this._dbCtx.SaveChanges();
            Console.WriteLine("Personal Notification Type is seeded successfully!");
        }
        else
        {
            Console.WriteLine("Personal Notification Type is already seeded!");
        }
    }

}
