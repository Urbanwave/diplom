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
    public class InvestorService : IInvestorService
    {
        ApplicationDbContext db;

        public InvestorService()
        {
            db = new ApplicationDbContext();
        }

        public List<ApplicationUser> GetAllInvestors(int page, int pageSize)
        {
            var roleId = db.Roles.Where(x => x.Name == "Investor").Select(x => x.Id).First();
            return db.Users.Where(x => x.Roles.Select(z =>z.RoleId).Contains(roleId)).Include("City").Include("Industries").Include("Currency").OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList(); 
        }

        public int GetInvestorsCount()
        {
            var roleId = db.Roles.Where(x => x.Name == "Investor").Select(x => x.Id).First();
            return db.Users.Where(x => x.Roles.Select(z => z.RoleId).Contains(roleId)).Count();
        }

        public ApplicationUser GetInvestorById(string id)
        {
            var roleId = db.Roles.Where(x => x.Name == "Investor").Select(x => x.Id).First();
            return db.Users.Where(x => x.Roles.Select(z => z.RoleId).Contains(roleId) && x.Id == id).Include("City").Include("Industries").Include("Currency").First();
        }

        public void DeleteInvestorById(string id)
        {
            var investor = GetInvestorById(id);
            db.Users.Remove(investor);
            db.SaveChanges();
        }
    }
}
