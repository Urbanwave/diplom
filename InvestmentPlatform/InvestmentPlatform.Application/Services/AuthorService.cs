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
    public class AuthorService : IAuthorService
    {
        ApplicationDbContext db;
        ISolutionService solutionService { get; set; }

        public AuthorService()
        {
            db = new ApplicationDbContext();
            solutionService = new SolutionService();
        }

        public ApplicationUser GetAuthorById(string id)
        {
            var roleId = db.Roles.Where(x => x.Name == "Author").Select(x => x.Id).First();
            return db.Users.Where(x => x.Roles.Select(z => z.RoleId).Contains(roleId) && x.Id == id).Include("City").First();
        }

        public List<ApplicationUser> GetAllAuthors(int page, int pageSize)
        {
            var roleId = db.Roles.Where(x => x.Name == "Author").Select(x => x.Id).First();
            return db.Users.Where(x => x.Roles.Select(z => z.RoleId).Contains(roleId)).Include("City").Include("Industries").Include("Currency").OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public void DeleteAuthorById(string id)
        {
            var author = GetAuthorById(id);
            var authorSolutions = solutionService.GetAllUserSolutions(1, int.MaxValue, id);

            foreach (var solution in authorSolutions)
            {
                solutionService.DeleteSolutionById(solution.Id, id, false);
            }
            db.Users.Remove(author);
            db.SaveChanges();
        }
    }
}
