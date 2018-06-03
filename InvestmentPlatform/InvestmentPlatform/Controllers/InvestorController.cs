﻿using InvestmentPlatform.Application.Interfaces;
using InvestmentPlatform.Application.Services;
using InvestmentPlatform.Application.ViewModels;
using InvestmentPlatform.Domain.Models;
using InvestmentPlatform.Models;
using Microsoft.AspNet.Identity;
using PagedList;
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

            ViewBag.IsProfilePage = true;

            var userId = User.Identity.GetUserId();

            var investor = investorService.GetInvestorById(userId);

            if (investor != null)
            {
                MapInvestorViewModel(investorViewModel, investor);
                investor.City.Country = locationService.GetCountryByCityId(investor.CityId);
            }

            return View(investorViewModel);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            investorService.DeleteInvestorById(id);

            return RedirectToAction("Investors", "Admin");
        }

        [Authorize(Roles = "Investor")]
        public ActionResult FavoriteSolutions(int page = 1)
        {
            ViewBag.IsProfilePage = true;

            int pageSize = 6;
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

        public ActionResult All()
        {
            var allInvestorsViewModel = new AllInvestorsViewModel();

            ViewBag.IsInvestorsPage = true;

            allInvestorsViewModel.Countries = locationService.GetAllCountries().OrderBy(x => x.Name).ToList(); ;
            allInvestorsViewModel.Cities = locationService.GetAllCities().OrderBy(x => x.Name).ToList();
            allInvestorsViewModel.Industries = typeService.GetAllIndustries().OrderBy(x => x.Name).ToList();

            return View(allInvestorsViewModel);
        }

        public ActionResult Filter(AllInvestorsViewModel model, string sortBy, string sortDestination,
            string searchString = "", int page = 1, bool firstShown = false)
        {
            if (page == 0)
            {
                page = 1;
            }

            ViewBag.SearchString = searchString;
            ViewBag.SortBy = sortBy;
            ViewBag.SortDestination = sortDestination;

            if (model != null && !firstShown)
            {
                Session["FilterInvestor"] = model;
            }
            else
            if (Session["FilterInvestor"] != null)
            {
                model = (AllInvestorsViewModel)Session["FilterInvestor"];
            }

            int pageSize = 6;

            var countriesCities = locationService.GetCountriesCityIds(model.SelectedCountries);
            model.SelectedCities = model.SelectedCities.Union(countriesCities).ToList();

            var investors = investorService.GetAllInvestors(1, int.MaxValue).Where(x => x.CompanyName.Contains(searchString)
            || x.CompanyDescription.Contains(searchString) || x.FirstName.Contains(searchString) || x.LastName.Contains(searchString)
            || x.City.Name.Contains(searchString) || x.InvestmentSize.ToString().Contains(searchString));

            if (model.SelectedCities.Count > 0)
            {
                investors = investors.Where(x => model.SelectedCities.Contains(x.CityId));
            }

            if (model.SelectedIndustries.Count > 0)
            {
                investors = investors.Where(x => x.Industries.Any(y => model.SelectedIndustries.Contains(y.Id)));
            }

            if (sortBy == "date")
            {
                if (sortDestination == "up")
                {
                    investors = investors.OrderBy(x => x.DateCreated);
                }
                else
                {
                    investors = investors.OrderByDescending(x => x.DateCreated);
                }
            }
            else
            if (sortBy == "investmentSize")
            {
                if (sortDestination == "up")
                {
                    investors = investors.OrderBy(x => x.InvestmentSize);
                }
                else
                {
                    investors = investors.OrderByDescending(x => x.InvestmentSize);
                }
            }

            ViewBag.InvestorsAmount = investors.Count();

            var investorViewModels = new List<InvestorViewModel>();

            foreach (var investor in investors)
            {
                var investorViewModel = new InvestorViewModel();

                MapInvestorViewModel(investorViewModel, investor);
                investorViewModel.City.Country = locationService.GetCountryByCityId(investor.CityId);
                investorViewModels.Add(investorViewModel);
            }

            return PartialView(investorViewModels.ToPagedList(page, pageSize));
        }

        [Authorize(Roles = "Investor")]
        public ActionResult Edit()
        {
            ViewBag.IsProfilePage = true;

            var investorViewModel = new InvestorEditViewModel();

            var userId = User.Identity.GetUserId();
            var user = userService.GetUserById(userId);

            if (user != null)
            {
                MapRegisterInvestorViewModel(investorViewModel, user);
                investorViewModel.City.Country = locationService.GetCountryByCityId(user.CityId);
                investorViewModel.InvestmentSectors = typeService.GetAllIndustries();
                investorViewModel.FileName = "../Content/Images/profile/" + user.LogoFileName;
            }

            return View(investorViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Investor")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InvestorEditViewModel model, HttpPostedFileBase file)
        {
            ValidateInvestorModel(model);

            var pictureName = string.Empty;

            if (file != null)
            {
                pictureName = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("/Content/Images/profile"), pictureName);
                file.SaveAs(path);
                model.FileName = "../Content/Images/profile/" + pictureName;
            }

            if (ModelState.IsValid)
            {    
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
                investorViewModel.City.Country = locationService.GetCountryByCityId(investor.CityId);
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
            investorViewModel.Email = investor.Email;
            investorViewModel.Website = investor.Website;
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
            investorRegisterViewModel.Email = investor.Email;
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