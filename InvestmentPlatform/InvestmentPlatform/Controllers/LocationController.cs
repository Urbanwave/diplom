using InvestmentPlatform.Application.Interfaces;
using InvestmentPlatform.Application.Services;
using InvestmentPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvestmentPlatform.Controllers
{
    public class LocationController : Controller
    {
        ILocationService locationService { get; set; }

        public LocationController()
        {
            locationService = new LocationService();
        }

        public ActionResult GetCountries()
        {
            var countries = locationService.GetAllCountries();
            return Json(countries, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCities(int countryId)
        {
            var cities = locationService.GetCitiesByCountryId(countryId);
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCountryIdByCityId(int cityId)
        {
            var countryId = locationService.GetCountryIdByCityId(cityId);
            return Json(countryId, JsonRequestBehavior.AllowGet);
        }
    }
}