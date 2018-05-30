using InvestmentPlatform.Application.Interfaces;
using InvestmentPlatform.Application.Services;
using InvestmentPlatform.Application.ViewModels;
using InvestmentPlatform.Domain.Models;
using InvestmentPlatform.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvestmentPlatform.Controllers
{
    public class InvestorController : Controller
    {
        IInvestorService investorService { get; set; }
        ILocationService locationService { get; set; }
        IUserService userService { get; set; }
        ISolutionService solutionService { get; set; }
        ITypeService typeService { get; set; }

        public InvestorController()
        {
            investorService = new InvestorService();
            locationService = new LocationService();
            userService = new UserService();
            typeService = new TypeService();
            solutionService = new SolutionService();
        }

        [Authorize(Roles = "Investor")]
        public ActionResult Index()
        {
            var investorViewModel = new InvestorViewModel();

            var userId = User.Identity.GetUserId();

            var investor = investorService.GetInvestorById(userId);

            if (investor != null)
            {
                MapInvestorViewModel(investorViewModel, investor);
                investor.City.Country = locationService.GetCountryByCityId(investor.CityId);
            }

            return View(investorViewModel);
        }

        [Authorize(Roles = "Investor")]
        public ActionResult FavoriteSolutions(int page = 1)
        {
            int pageSize = 3;
            var allSolutionViewModel = new AllSolutionsViewModel();

            allSolutionViewModel.Countries = locationService.GetAllCountries();
            allSolutionViewModel.Cities = locationService.GetAllCities();
            allSolutionViewModel.Industries = typeService.GetAllIndustries();
            allSolutionViewModel.ImplementationStatuses = typeService.GetAllImplementationStatuses();
            allSolutionViewModel.SolutionTypes = typeService.GetAllSolutionTypes();

            var userId = User.Identity.GetUserId();
            var favoriteSolutionIds = solutionService.GetFavoriteSolutionsByUserId(userId);
            var solutions = solutionService.GetAllSolutions(page, pageSize).Where(x => favoriteSolutionIds.Contains(x.Id));

            var projectAmount = solutions.Count();

            if (projectAmount % pageSize > 0)
            {
                allSolutionViewModel.ProjectAmount = projectAmount / pageSize + 1;
            }
            else
            {
                allSolutionViewModel.ProjectAmount = projectAmount / pageSize;
            }

            var solutionViewModels = new List<SolutionViewModel>();

            foreach (var solution in solutions)
            {
                var solutionViewModel = new SolutionViewModel();

                MapSolutionViewModel(solutionViewModel, solution);
                solution.City.Country = locationService.GetCountryByCityId(solution.CityId);
                solutionViewModels.Add(solutionViewModel);
            }

            allSolutionViewModel.SolutionViewModels = solutionViewModels;

            return View(allSolutionViewModel);
        }


        public ActionResult All(int page = 1)
        {
            int pageSize = 3;
            var allInvestorsViewModel = new AllInvestorsViewModel();
            var investors = investorService.GetAllInvestors(page, pageSize);
            var investorViewModels = new List<InvestorViewModel>();

            var projectAmount = investorService.GetInvestorsCount();

            if (projectAmount % pageSize > 0)
            {
                allInvestorsViewModel.ProjectAmount = projectAmount / pageSize + 1;
            }
            else
            {
                allInvestorsViewModel.ProjectAmount = projectAmount / pageSize;
            }

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

        [Authorize(Roles = "Investor")]
        public ActionResult Edit()
        {
            var investorViewModel = new InvestorEditViewModel();

            var userId = User.Identity.GetUserId();
            var user = userService.GetUserById(userId);

            if (user != null)
            {
                MapRegisterInvestorViewModel(investorViewModel, user);
                investorViewModel.City.Country = locationService.GetCountryByCityId(user.CityId);
                investorViewModel.InvestmentSectors = typeService.GetAllIndustries();
            }

            return View(investorViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Investor")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InvestorEditViewModel model, HttpPostedFileBase file)
        {
            ValidateInvestorModel(model);
            if (ModelState.IsValid)
            {
                var pictureName = string.Empty;

                if (file != null)
                {
                    pictureName = Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("/Content/Images/profile"), pictureName);
                    file.SaveAs(path);
                }

                var userId = User.Identity.GetUserId();
                var user = userService.GetUserById(userId);

                userService.UpdateUser(model, user, pictureName);

                typeService.AddIndustriesToUser(user.Id, model.SelectedInvestmentSectors);

                return RedirectToAction("Index", "Investor");
            }

            model.InvestmentSectors = typeService.GetAllIndustries();
            return View("Edit", model);
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

        private void MapRegisterInvestorViewModel(InvestorEditViewModel investorRegisterViewModel, ApplicationUser investor)
        {
            investorRegisterViewModel.FirstName = investor.FirstName;
            investorRegisterViewModel.LastName = investor.LastName;
            investorRegisterViewModel.Curency = investor.Currency;
            investorRegisterViewModel.CurrencyId = investor.Currency.Id;
            investorRegisterViewModel.FileName = investor.LogoFileName;
            investorRegisterViewModel.Id = investor.Id;
            investorRegisterViewModel.City = investor.City;
            investorRegisterViewModel.CityId = investor.CityId;
            investorRegisterViewModel.SelectedInvestmentSectors = investor.Industries.Select(x => x.Id).ToList();
            investorRegisterViewModel.Website = investor.Website;
            investorRegisterViewModel.Email = investor.Email;
            investorRegisterViewModel.CompanyName = investor.CompanyName;
            investorRegisterViewModel.CompanyDescription = investor.CompanyDescription;
            investorRegisterViewModel.InvestmentSize = investor.InvestmentSize;
        }

        private void ValidateInvestorModel(InvestorEditViewModel model)
        {
            if (model.CityId == 0)
            {
                ModelState.AddModelError("", "Please select a city");
            }

            if (model.CurrencyId == 0)
            {
                ModelState.AddModelError("", "Please select a currency");
            }

            if (model.SelectedInvestmentSectors == null || model.SelectedInvestmentSectors.Count == 0)
            {
                ModelState.AddModelError("", "Please select at least one investment sector");
            }
        }

        private void MapSolutionViewModel(SolutionViewModel solutionViewModel, Solution solution)
        {
            solutionViewModel.Currency = solution.Currency;
            solutionViewModel.FileName = solution.LogoFileName;
            solutionViewModel.Id = solution.Id;
            solutionViewModel.City = solution.City;
            solutionViewModel.CityId = solution.CityId;
            solutionViewModel.CurrencyId = solution.CurrencyId;
            solutionViewModel.InvestmentSize = solution.InvestmentSize;
            solutionViewModel.Title = solution.Title;
            solutionViewModel.ImplementationStatusId = solution.ImplementationStatusId;
            solutionViewModel.SelectedIndustries = solution.Industries.Select(x => x.Id).ToList();
            solutionViewModel.SelectedSolutionTypes = solution.SolutionTypes.Select(x => x.Id).ToList();
            solutionViewModel.UniqueInfo = solution.UniqueInfo;
            solutionViewModel.SolutionDescription = solution.SolutionDescription;
            solutionViewModel.Industries = solution.Industries;
            solutionViewModel.SolutionTypes = solution.SolutionTypes;
            solutionViewModel.ImplementationStatus = solution.ImplementationStatus;

            var userId = User.Identity.GetUserId();
            var favoriteSolution = solutionService.GetAllFavoriteSolutionsByUserId(userId);

            solutionViewModel.IsFollowed = favoriteSolution.Select(x => x.FollowedSolutionId).Contains(solution.Id);
        }
    }
}