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
        public DbSet<ExpertEval> ExpertEvals { get; set; }
        public DbSet<Eval> Evals { get; set; }
        public DbSet<DevCost> DevCosts { get; set; }
        public DbSet<CbQj> CbQjs { get; set; }
        public DbSet<OpCost> OpCosts { get; set; }
        public DbSet<EmpTime> EmpTimes { get; set; }
        public DbSet<Equip> Equips { get; set; }
        public DbSet<EcoEffect> EcoEffects { get; set; }



    }
}
