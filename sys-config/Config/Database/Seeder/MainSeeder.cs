using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemConfigurator.Config.Database.Seeder
{
    public abstract class MainSeeder
    {
        public MainSeeder(DbContext dbCtx) { }
    }
}
