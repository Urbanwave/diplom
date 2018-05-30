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

        public AuthorService()
        {
            db = new ApplicationDbContext();
        }

        public ApplicationUser GetAuthorById(string id)
        {
            var roleId = db.Roles.Where(x => x.Name == "Author").Select(x => x.Id).First();
            return db.Users.Where(x => x.Roles.Select(z => z.RoleId).Contains(roleId) && x.Id == id).Include("City").First();
        }
    }
}
