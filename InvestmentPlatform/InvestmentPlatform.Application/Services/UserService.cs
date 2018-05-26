using InvestmentPlatform.Application.Interfaces;
using InvestmentPlatform.Application.ViewModels;
using InvestmentPlatform.Domain.Models;
using InvestmentPlatform.Models;
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
        ITypeService typeService { get; set; }

        public UserService()
        {
            db = new ApplicationDbContext();
            typeService = new TypeService();
        }

        public ApplicationUser GetUserById(string id)
        {
            return db.Users.Include("Industries").Include("City").Include("Currency")
                .Where(x => x.Id == id).FirstOrDefault();
        }

        public void UpdateUser(InvestorEditViewModel model, ApplicationUser user, string pictureName)
        {
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.CityId = model.CityId;
            user.LogoFileName = string.IsNullOrEmpty(pictureName) ? user.LogoFileName : pictureName;
            user.CompanyName = model.CompanyName;
            user.CompanyDescription = model.CompanyDescription;
            user.Website = model.Website;
            user.InvestmentSize = model.InvestmentSize;
            user.CurrencyId = model.CurrencyId;
            user.Industries.Clear();

            db.SaveChanges();
        }

        public void UpdateUser(AuthorEditViewModel model, ApplicationUser user, string pictureName)
        {
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.CityId = model.CityId;
            user.LogoFileName = string.IsNullOrEmpty(pictureName) ? user.LogoFileName : pictureName;
            user.Website = model.Website;
            user.JobTitle = model.JobTitle;

            db.SaveChanges();
        }
    }
}
