using InvestmentPlatform.Application.Interfaces;
using InvestmentPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPlatform.Application.Services
{
    class InvestorService : IInvestmentService
    {
        ApplicationDbContext db;

        public InvestorService()
        {
            db = new ApplicationDbContext();
        }

        public List<ApplicationUser> GetAllInvestors()
        {
            var roleId = db.Roles.Where(x => x.Name == "Investor").Select(x => x.Id).First();
            return db.Users.ToList().Where(x => x.Roles.Select(z =>z.RoleId).Contains(roleId)).ToList(); 
        }
    }
}
