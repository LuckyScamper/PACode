using PADataToDatabase.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace PADataToDatabase.DBContext
{
    public class PADataContext : DbContext
    {
        public PADataContext() : base("Server=localhost;Database=PAData;Trusted_Connection=True;")
        {

        }

        public DbSet<ElectionMap> ElectionMaps { get; set; }
        public DbSet<ZoneCode> ZoneCodes { get; set; }
        public DbSet<ZoneType> ZoneTypes { get; set; }
        public DbSet<Fve> Fves { get; set; }
    }
}
