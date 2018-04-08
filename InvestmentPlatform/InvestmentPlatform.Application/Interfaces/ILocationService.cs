using InvestmentPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPlatform.Application.Interfaces
{
    public interface ILocationService
    {
        List<Country> GetAllCountries();

        List<City> GetCitiesByCountryId(int countryId);
    }
}
