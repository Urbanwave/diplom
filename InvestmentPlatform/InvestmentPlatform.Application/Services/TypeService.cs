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
    public class TypeService : ITypeService
    {
        ApplicationDbContext db;

        public TypeService()
        {
            db = new ApplicationDbContext();
        }

        public List<SolutionType> GetAllSolutionTypes()
        {
            return db.SolutionTypes.ToList();
        }

        public List<ImplementationStatus> GetAllImplementationStatuses()
        {
            return db.ImplementationStatuses.ToList();
        }

        public List<Currency> GetAllCurrencies()
        {
            return db.Currencies.ToList();
        }

        public List<Industry> GetAllIndustries()
        {
            return db.Industries.ToList();
        }

        public List<SolutionType> GetSolutionTypesByIds(List<int> ids)
        {
            return db.SolutionTypes.Where(x => ids.Contains(x.Id)).ToList();
        }

        public List<Industry> GetIndustriesByIds(List<int> ids)
        {
            return db.Industries.Where(x => ids.Contains(x.Id)).ToList();
        }

        public void AddIndustriesToUser(string userId, List<int> industryIds)
        {
            var user = db.Users.Where(x => x.Id == userId).First();
            user.Industries = GetIndustriesByIds(industryIds);

            foreach (var industry in user.Industries)
            {
                db.Entry(industry).State = EntityState.Unchanged;
            }

            db.SaveChanges();
        }
    }
}
