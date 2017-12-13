using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Data.Repos
{
    public class ProjectRepo
    {
        private readonly AppDbContext _context;

        public List<Project> Projects => _context.Projects.ToList();

        public List<ExpertEval> ExpertEvals => _context.ExpertEvals.ToList();

        public List<Eval> Evals => _context.Evals.ToList();

        public ProjectRepo(AppDbContext context)
        {
            _context = context;
        }

        public void Create(ExpertEval expertEval)
        {
            _context.ExpertEvals.Add(expertEval);
            _context.Evals.AddRange(expertEval.Evals);
            _context.SaveChanges();
        }

        public void CreateOrUpdate(ExpertEval expertEval)
        {
            _context.ExpertEvals.AddOrUpdate(expertEval);
            foreach (var eval in expertEval.Evals)
                _context.Evals.AddOrUpdate(eval);
       //     _context.SaveChanges();
        }

        public void CreateOrUpdate(Project project)
        {
            _context.Projects.AddOrUpdate(project);
          //  _context.SaveChanges();
        }

        public void CreateOrUpdate(DevCost devCost)
        {
            _context.CbQjs.RemoveRange(_context.CbQjs.Where(e => e.DevCostId == devCost.ProjectId));
         //   _context.SaveChanges();
            foreach (var cbQj in devCost.CbQjs)
                _context.CbQjs.AddOrUpdate(cbQj);
            _context.DevCosts.AddOrUpdate(devCost);
            // _context.SaveChanges();
        }

        public void CreateOrUpdate(OpCost opCost)
        {
            _context.EmpTimes.RemoveRange(_context.EmpTimes.Where(e => e.OpCostId == opCost.ProjectId));
            _context.Equips.RemoveRange(_context.Equips.Where(e => e.OpCostId == opCost.ProjectId));
            foreach (var empTime in opCost.EmpTimes)
                _context.EmpTimes.AddOrUpdate(empTime);
            foreach (var equip in opCost.Equips)
                _context.Equips.AddOrUpdate(equip);
            _context.OpCosts.AddOrUpdate(opCost);
        }

        public void CreateOrUpdate(EcoEffect ecoEffect)
        {
            _context.EcoEffects.AddOrUpdate(ecoEffect);
        }


        public void Save()
        {
            _context.SaveChanges();
        }


    }
}
