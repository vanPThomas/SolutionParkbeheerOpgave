using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Model
{
    public class DBContext : DbContext
    {
        public DbSet<HuurcontractEF> Huurcontracten { get; set; }
        public DbSet<HuurderEF> Huurders { get; set; }
        public DbSet<HuisEF> Huizen { get; set; }
        public DbSet<ParkEF> Parken { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source=HIMEKO\SQLEXPRESS;Initial Catalog=ParkbeheerDB;Integrated Security=True;TrustServerCertificate=true"
            );
        }
    }
}
