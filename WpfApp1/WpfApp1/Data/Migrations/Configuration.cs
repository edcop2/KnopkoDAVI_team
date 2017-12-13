using WpfApp1.Algorithms;
using WpfApp1.Models;

namespace WpfApp1.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WpfApp1.Data.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Data\Migrations";
        }

        protected override void Seed(WpfApp1.Data.AppDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (!context.Projects.Any())
            {
                var defaultProject = Project.Create("TestProject");

                var defaultExpertEval = ExpertEval.Create(defaultProject.Id, 2, 5, 5, 1, 2, 3, 1, 4, 2);
                var defaultDevCost = DevCost.Create(defaultProject.Id, 166.7, 45, 3000, 460, 4, new double[] { 5, 2, 6, 1 },
                    new double[] { 2, 6, 1, 5 }, 5000, 3000, 2000, 4000, 1500);
                var defaultOpCost = OpCost.Create(defaultProject.Id, 2, new double[] { 30, 50 }, new double[] { 170, 200 },
                    2, new[] { 0.5, 0.1 }, new double[] { 15000, 15000 }, new double[] { 5, 2 }, new double[] { 480, 800 },
                    new double[] { 8, 13 });
                var defaultEcoEffect = EcoEffect.Create(defaultProject.Id, 43750, 22855, 5000, 31438);

                context.Projects.AddOrUpdate(defaultProject);
                context.ExpertEvals.AddOrUpdate(defaultExpertEval);
                context.Evals.AddRange(defaultExpertEval.Evals);
                context.DevCosts.AddOrUpdate(defaultDevCost);
                context.CbQjs.AddRange(defaultDevCost.CbQjs);
                context.OpCosts.AddOrUpdate(defaultOpCost);
                context.EmpTimes.AddRange(defaultOpCost.EmpTimes);
                context.Equips.AddRange(defaultOpCost.Equips);
                context.EcoEffects.AddOrUpdate(defaultEcoEffect);

                defaultProject.DevCosts = Expenses.Calculate(166.7, 45, 3000, 460, 4, new double[] { 5, 2, 6, 1 },
                    new double[] { 2, 6, 1, 5 }, 5000, 3000, 2000, 4000, 1500);
                defaultProject.Competitivness = Competitiveness.Calculate(new[] { 2, 5, 5, 1, 2, 3, 1, 4, 2 },
                    new[] { 3, 1, 5, 3, 2, 4, 1, 2, 4 });
                defaultProject.OpCosts = Costs.Calculate(2, new double[] { 30, 50 }, new double[] { 170, 200 }, 2,
                    new[] { 0.5, 0.1 }, new double[] { 15000, 15000 }, new double[] { 5, 2 }, new double[] { 480, 800 },
                    new double[] { 8, 13 });
                defaultProject.EcoEffects = SignEcoEffect.Calculate(43750, 22855, 5000, 31438);

                context.SaveChanges();
            }
        }
    }
}
