namespace SystemConfigurator.Config.Database.Seeder.UMA;

public class MandatorySeeder: MainSeeder
{
    private SystemDbUMAContext _dbCtx;
    public MandatorySeeder(SystemDbUMAContext dbCtx) : base(dbCtx)
    {
        this._dbCtx = dbCtx;
    }

    public async Task Seed()
    {
        // Seed Device Types
        if(!this._dbCtx.DeviceTypes.Any())
        {
            this._dbCtx.DeviceTypes.AddRange(
                new Model.UMA.LOV.DeviceType
                {
                    DeviceTypeId = "iOS"
                },
                new Model.UMA.LOV.DeviceType
                {
                    DeviceTypeId = "Android"
                },
                new Model.UMA.LOV.DeviceType
                {
                    DeviceTypeId = "Web"
                },
                new Model.UMA.LOV.DeviceType
                {
                    DeviceTypeId = "Debug"
                }
            );

            await this._dbCtx.SaveChangesAsync();
            Console.WriteLine("Device Type is seeded successfully!");
        }else
        {
            Console.WriteLine("Device Type already seeded, skipping...");
        }

        var dummyUser = this._dbCtx.Users.FirstOrDefault(u => u.Email == "futami@gmail.com");

        if(dummyUser == null)
        {
            dummyUser = new Model.UMA.User
            {
                Email = "futami@gmail.com",
                Status = 0,
                Name = "Futami Manami"
            };
        }else
        {
            Console.WriteLine("Dummy user of futami@gmail.com already exists, skipping...");
            return;
        }
    }
}