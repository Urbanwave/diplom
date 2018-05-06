using InvestmentPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPlatform.Application.Interfaces
{
    interface IInvestmentService
    {
        List<ApplicationUser> GetAllInvestors();
    }
}
