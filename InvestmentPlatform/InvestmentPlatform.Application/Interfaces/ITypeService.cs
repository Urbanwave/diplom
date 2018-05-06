using InvestmentPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPlatform.Application.Interfaces
{
    public interface ITypeService
    {
        List<SolutionType> GetAllSolutionTypes();

        List<ImplementationStatus> GetAllImplementationStatuses();

        List<Currency> GetAllCurrencies();

        List<Industry> GetAllIndustries();

        List<SolutionType> GetSolutionTypesByIds(List<int> ids);

        List<Industry> GetIndustriesByIds(List<int> ids);
    }
}
