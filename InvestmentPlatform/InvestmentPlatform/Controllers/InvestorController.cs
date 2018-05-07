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

        private void MapInvestorViewModel(InvestorViewModel investorViewModel, ApplicationUser investor)
        {
           // investorViewModel.Currency = solution.Currency;
            investorViewModel.FileName = investor.LogoFileName;
            investorViewModel.City = investor.City;
           // investorViewModel.InvestmentSize = investor.InvestmentSize;
            investorViewModel.CompanyName = investor.UserName;
            
        }
    }
}