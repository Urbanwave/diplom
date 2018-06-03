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

        List<City> GetAllCities();

        List<City> GetCitiesByCountryId(int countryId);

        int GetCountryIdByCityId(int cityId);

        Country GetCountryByCityId(int cityId);

        List<int> GetCountriesCityIds(List<int> countriesIds);
    }
}
