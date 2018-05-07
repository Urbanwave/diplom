using InvestmentPlatform.Application.Interfaces;
using InvestmentPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPlatform.Application.Services
{
    public class SolutionService : ISolutionService
    {
        ApplicationDbContext db;

        public SolutionService()
        {
            db = new ApplicationDbContext();
        }

        public List<Solution> GetAllSolutions()
        {
            return db.Solutions.Include("City").Include("Currency").Include("ImplementationStatus").ToList();
        }

        public Solution GetSolutionById(int id)
        {
            return db.Solutions.Include("Industries").Include("SolutionTypes").Include("City").Include("Currency").Include("ImplementationStatus").Where(x => x.Id == id).FirstOrDefault();
        }

        public void AddSolution(Solution solution)
        {
            foreach (var solutionType in solution.SolutionTypes)
            {
                db.Entry(solutionType).State = EntityState.Unchanged;
            }

            foreach (var industry in solution.Industries)
            {
                db.Entry(industry).State = EntityState.Unchanged;
            }

            db.Solutions.Add(solution);
            db.SaveChanges();
        }
    }
}
