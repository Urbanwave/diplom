using InvestmentPlatform.Application.Interfaces;
using InvestmentPlatform.Application.Services;
using InvestmentPlatform.Application.ViewModels;
using InvestmentPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvestmentPlatform.Controllers
{
    public class InvestorController : Controller
    {
        IInvestorService investorService { get; set; }
        ILocationService locationService { get; set; }

        public InvestorController()
        {
            investorService = new InvestorService();
            locationService = new LocationService();
        }

        public ActionResult All()
        {
            var allInvestorsViewModel = new AllInvestorsViewModel();
            var investors = investorService.GetAllInvestors();
            var investorViewModels = new List<InvestorViewModel>();

            foreach (var investor in investors)
            {
                var investorViewModel = new InvestorViewModel();

                MapInvestorViewModel(investorViewModel, investor);
                investor.City.Country = locationService.GetCountryByCityId(investor.CityId);
                investorViewModels.Add(investorViewModel);
            }

            allInvestorsViewModel.InvestorViewModels = investorViewModels;

            return View(allInvestorsViewModel);
        }

        public ActionResult View(string id)
        {
            var investorViewModel = new InvestorViewModel();

            var investor = investorService.GetInvestorById(id);

            if (investor != null)
            {
                MapInvestorViewModel(investorViewModel, investor);
                investor.City.Country = locationService.GetCountryByCityId(investor.CityId);
            }

            return View(investorViewModel);
        }

        private void MapInvestorViewModel(InvestorViewModel investorViewModel, ApplicationUser investor)
        {
            investorViewModel.Id = investor.Id;
            investorViewModel.Currency = investor.Currency;
            investorViewModel.FileName = investor.LogoFileName;
            investorViewModel.City = investor.City;
            investorViewModel.InvestmentSize = investor.InvestmentSize;
            investorViewModel.CompanyName = investor.CompanyName;
            investorViewModel.CompanyDescription = investor.CompanyDescription;
            investorViewModel.Industries = investor.Industries;
        }
    }
}