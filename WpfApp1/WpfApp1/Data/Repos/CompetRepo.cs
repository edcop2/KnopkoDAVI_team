using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Data.Repos
{
    public class CompetRepo
    {
        private readonly AppDbContext _context;

        public List<ExpertEval> ExpertEvals => _context.ExpertEvals.ToList();

        public List<Eval> Evals => _context.Evals.ToList();

        public CompetRepo(AppDbContext context)
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
            _context.SaveChanges();
        }
        public void Save()
        {
            _context.SaveChanges();
        }


    }
}
