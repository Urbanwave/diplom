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
    public class AuthorController : Controller
    {
        IInvestorService investorService { get; set; }
        ILocationService locationService { get; set; }
        IUserService userService { get; set; }
        ITypeService typeService { get; set; }
        ISolutionService solutionService { get; set; }
        IAuthorService authorService { get; set; }

        public AuthorController()
        {
            investorService = new InvestorService();
            locationService = new LocationService();
            userService = new UserService();
            typeService = new TypeService();
            solutionService = new SolutionService();
            authorService = new AuthorService();
        }

        [Authorize(Roles = "Author")]
        public ActionResult Index()
        {
            var authorViewModel = new AuthorEditViewModel();

            var userId = User.Identity.GetUserId();

            var author = authorService.GetAuthorById(userId);

            if (author != null)
            {
                MapRegisterAuthorViewModel(authorViewModel, author);
                authorViewModel.City.Country = locationService.GetCountryByCityId(author.CityId);
            }

            return View(authorViewModel);
        }

        public ActionResult View(string id)
        {
            var authorViewModel = new AuthorEditViewModel();

            var author = authorService.GetAuthorById(id);

            if (author != null)
            {
                MapRegisterAuthorViewModel(authorViewModel, author);
                authorViewModel.City.Country = locationService.GetCountryByCityId(author.CityId);
            }

            return View(authorViewModel);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            authorService.DeleteAuthorById(id);

            return RedirectToAction("Authors", "Admin");
        }

        [Authorize(Roles = "Author")]
        public ActionResult Solutions(int page = 1)
        {
            int pageSize = 3;
            var allSolutionViewModel = new AllSolutionsViewModel();

            allSolutionViewModel.Countries = locationService.GetAllCountries();
            allSolutionViewModel.Cities = locationService.GetAllCities();
            allSolutionViewModel.Industries = typeService.GetAllIndustries();
            allSolutionViewModel.ImplementationStatuses = typeService.GetAllImplementationStatuses();
            allSolutionViewModel.SolutionTypes = typeService.GetAllSolutionTypes();

            var userId = User.Identity.GetUserId();
            var solutions = solutionService.GetAllUserSolutions(page, pageSize, userId);

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

        [Authorize(Roles = "Author")]
        public ActionResult Edit()
        {
            var authorEditViewModel = new AuthorEditViewModel();

            var userId = User.Identity.GetUserId();
            var user = userService.GetUserById(userId);

            if (user != null)
            {
                MapRegisterAuthorViewModel(authorEditViewModel, user);
            }

            return View(authorEditViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Author")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AuthorEditViewModel model, HttpPostedFileBase file)
        {
            ValidateAuthorModel(model);
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

                return RedirectToAction("Index", "Home");
            }

            return View("Edit", model);
        }

        private void MapRegisterAuthorViewModel(AuthorEditViewModel authorRegisterViewModel, ApplicationUser author)
        {
            authorRegisterViewModel.FirstName = author.FirstName;
            authorRegisterViewModel.LastName = author.LastName;
            authorRegisterViewModel.FileName = author.LogoFileName;
            authorRegisterViewModel.JobTitle = author.JobTitle;
            authorRegisterViewModel.Id = author.Id;
            authorRegisterViewModel.City = author.City;
            authorRegisterViewModel.CityId = author.CityId;
            authorRegisterViewModel.Email = author.Email;
            authorRegisterViewModel.Website = author.Website;
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

        private void ValidateAuthorModel(AuthorEditViewModel model)
        {
            if (model.CityId == 0)
            {
                ModelState.AddModelError("", "Please select a city");
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