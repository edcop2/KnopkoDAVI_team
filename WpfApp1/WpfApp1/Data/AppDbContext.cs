using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
            : base("DefaultConnection")
        {

        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }


        public DbSet<Project> Projects { get; set; }
        public DbSet<Slave> Slaves { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Equip> Equips { get; set; }
        public DbSet<QualityAttribute> QualityAttributes { get; set; }




    }
}
