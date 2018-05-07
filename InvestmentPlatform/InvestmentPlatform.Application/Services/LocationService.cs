using InvestmentPlatform.Application.Interfaces;
using InvestmentPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPlatform.Application.Services
{
    public class LocationService : ILocationService
    {
        ApplicationDbContext db;

        public LocationService()
        {
            db = new ApplicationDbContext();
        }

        public List<Country> GetAllCountries()
        {
            return db.Countries.ToList();
        }

        public List<City> GetAllCities()
        {
            return db.Cities.ToList();
        }

        public List<City> GetCitiesByCountryId(int countryId)
        {
            return db.Cities.Where(x => x.CountryId == countryId).ToList();
        }

        public int GetCountryIdByCityId(int cityId)
        {
            return db.Cities.Where(x => x.Id == cityId).First().CountryId;
        }

        public Country GetCountryByCityId(int cityId)
        {
            return db.Countries.Where(x => x.Cities.Select(z => z.Id).Contains(cityId)).First();
        }
    }
}
