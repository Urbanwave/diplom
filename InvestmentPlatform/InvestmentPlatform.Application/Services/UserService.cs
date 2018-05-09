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
    public class UserService : IUserService
    {
        ApplicationDbContext db;

        public UserService()
        {
            db = new ApplicationDbContext();
        }

        public void MarkIndustriesAsUnchanged(ApplicationUser user)
        {
            foreach (var industry in user.Industries)
            {
                db.Entry(industry).State = EntityState.Unchanged;
            }

            db.SaveChanges();
        }
    }
}
