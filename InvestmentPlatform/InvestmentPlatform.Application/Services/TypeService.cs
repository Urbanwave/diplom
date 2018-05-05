using InvestmentPlatform.Application.Interfaces;
using InvestmentPlatform.Domain.Models;
using System;
using System.Collections.Generic;
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
    }
}
