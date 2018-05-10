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

        public List<ApplicationUser> GetAllInvestors()
        {
            var roleId = db.Roles.Where(x => x.Name == "Investor").Select(x => x.Id).First();
            return db.Users.Where(x => x.Roles.Select(z =>z.RoleId).Contains(roleId)).Include("City").Include("Industries").Include("Currency").ToList(); 
        }

        public ApplicationUser GetInvestorById(string id)
        {
            var roleId = db.Roles.Where(x => x.Name == "Investor").Select(x => x.Id).First();
            return db.Users.Where(x => x.Roles.Select(z => z.RoleId).Contains(roleId) && x.Id == id).Include("City").Include("Industries").Include("Currency").First();
        }

    }
}
