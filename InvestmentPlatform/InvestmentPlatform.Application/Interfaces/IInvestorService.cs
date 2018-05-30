using InvestmentPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPlatform.Application.Interfaces
{
    public interface IInvestorService
    {
        List<ApplicationUser> GetAllInvestors(int page, int pageSize);

        int GetInvestorsCount();

        ApplicationUser GetInvestorById(string id);

        void DeleteInvestorById(string id);
    }
}
