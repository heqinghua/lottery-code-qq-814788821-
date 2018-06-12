using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Data
{
    public class YtgDbContextConfiguration : DbMigrationsConfiguration<YtgDbContext>
    {
        public YtgDbContextConfiguration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
            

            
        }
    }
}
