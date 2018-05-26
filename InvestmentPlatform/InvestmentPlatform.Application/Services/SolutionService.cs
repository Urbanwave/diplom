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

        public List<Solution> GetAllSolutions(int page, int pageSize)
        {
            return db.Solutions.Include("City").Include("Currency").Include("ImplementationStatus")
                .OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<FavoriteSolution> GetAllFavoriteSolutionsByUserId(string id)
        {
            return db.FavoriteSolutions.Where(x => x.FollowedUserId == id).ToList();
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

        public int GetSolutionsAmount()
        {
            return db.Solutions.Count();
        }

        public void AddFavoriteSolution(FavoriteSolution favoriteSolution)
        {
            db.FavoriteSolutions.Add(favoriteSolution);
            db.SaveChanges();
        }

        public void RemoveFavoriteSolution(FavoriteSolution favoriteSolution)
        {
            db.FavoriteSolutions.Remove(favoriteSolution);
            db.SaveChanges();
        }


        public List<int> GetFavoriteSolutionsByUserId(string id)
        {
            return db.FavoriteSolutions.Where(x => x.FollowedUserId == id).Select(x => x.FollowedSolutionId).ToList();
        }
    }
}
