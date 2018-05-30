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
    public class SolutionController : Controller
    {
        ITypeService typeService { get; set; }
        ISolutionService solutionService { get; set; }
        ILocationService locationService { get; set; }
        IUserService userService { get; set; }

        public SolutionController()
        {
            typeService = new TypeService();
            solutionService = new SolutionService();
            locationService = new LocationService();
            userService = new UserService();
        }

        // GET: Solution
        public ActionResult View(int id)
        {
            var solutionViewModel = new SolutionViewModel();

            var solution = solutionService.GetSolutionById(id);

            if (solution != null)
            {
                MapSolutionViewModel(solutionViewModel, solution);
                solutionViewModel.City.Country = locationService.GetCountryByCityId(solution.CityId);
            }

            return View(solutionViewModel);
        }

        [Authorize(Roles = "Author, Admin")]
        public ActionResult Delete(int id = 0)
        {
            var userId = User.Identity.GetUserId();

            solutionService.DeleteSolutionById(id, userId, User.IsInRole("Admin"));

            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Solutions", "Admin");
            }

            return RedirectToAction("Solutions", "Author");
        }

        public ActionResult All(string searchString = "", int page = 1)
        {
            int pageSize = 3;
            var allSolutionViewModel = new AllSolutionsViewModel();

            allSolutionViewModel.Countries = locationService.GetAllCountries();
            allSolutionViewModel.Cities = locationService.GetAllCities();
            allSolutionViewModel.Industries = typeService.GetAllIndustries();
            allSolutionViewModel.ImplementationStatuses = typeService.GetAllImplementationStatuses();
            allSolutionViewModel.SolutionTypes = typeService.GetAllSolutionTypes();

            var projectAmount = solutionService.GetSolutionsAmount();
            allSolutionViewModel.AllProjectsCount = projectAmount;

            if (projectAmount % pageSize > 0)
            {
                allSolutionViewModel.ProjectAmount = projectAmount / pageSize + 1;
            } else
            {
                allSolutionViewModel.ProjectAmount = projectAmount / pageSize;
            }

            var solutions = solutionService.GetAllSolutions(page, pageSize).Where(x => x.SolutionDescription.Contains(searchString)
            || x.Title.Contains(searchString) || x.Currency.Name.Contains(searchString) );
            var solutionViewModels = new List<SolutionViewModel>();

            foreach (var solution in solutions)
            {
                var solutionViewModel = new SolutionViewModel();

                MapSolutionViewModel(solutionViewModel, solution);
                solutionViewModel.City.Country = locationService.GetCountryByCityId(solution.CityId);
                solutionViewModels.Add(solutionViewModel);
            }

            allSolutionViewModel.SolutionViewModels = solutionViewModels;

            return View(allSolutionViewModel);
        }


        [Authorize(Roles = "Author")]
        public ActionResult Add()
        {
            var solutionViewModel = new SolutionViewModel();
            solutionViewModel.SolutionTypes = typeService.GetAllSolutionTypes();
            solutionViewModel.Industries = typeService.GetAllIndustries();

            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            return View(solutionViewModel);
        }

        [Authorize(Roles = "Author, Admin")]
        public ActionResult Edit(int id = 0)
        {
            var solutionViewModel = new SolutionViewModel();
            solutionViewModel.SolutionTypes = typeService.GetAllSolutionTypes();
            solutionViewModel.Industries = typeService.GetAllIndustries();

            if (id != 0)
            {
                var solution = solutionService.GetSolutionById(id);
                if (solution != null)
                {
                    MapSolutionViewModel(solutionViewModel, solution);
                    solutionViewModel.City.Country = locationService.GetCountryByCityId(solution.CityId);
                    solutionViewModel.SolutionTypes = typeService.GetAllSolutionTypes();
                    solutionViewModel.Industries = typeService.GetAllIndustries();
                }
            }

            return View(solutionViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Author, Admin")]
        public ActionResult Edit(SolutionViewModel solutionViewModel, HttpPostedFileBase file)
        {
            ValidateSolutionModel(solutionViewModel);
            if (ModelState.IsValid)
            {
                var pictureName = string.Empty;

                if (file != null)
                {
                    pictureName = Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("/Content/Images/profile"), pictureName);
                    file.SaveAs(path);
                }

                var solution = solutionService.GetSolutionById(solutionViewModel.Id);
                solutionService.UpdateSolution(solutionViewModel, solution, pictureName);

                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Solutions", "Admin");
                }

                return RedirectToAction("Solutions", "Author");
            }

            return View("Edit", solutionViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Author")]
        public ActionResult Save(SolutionViewModel solutionViewModel, HttpPostedFileBase file)
        {
            ValidateSolutionModel(solutionViewModel);
            if (ModelState.IsValid)
            {
                var pictureName = string.Empty;

                if (file != null)
                {
                    pictureName = Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("/Content/Images/profile"), pictureName);
                    file.SaveAs(path);
                }

                var solution = new Solution()
                {
                    CityId = solutionViewModel.CityId,
                    CurrencyId = solutionViewModel.CurrencyId,
                    InvestmentSize = solutionViewModel.InvestmentSize,
                    ImplementationStatusId = solutionViewModel.ImplementationStatusId,
                    LogoFileName = pictureName,
                    SolutionDescription = solutionViewModel.SolutionDescription,
                    Title = solutionViewModel.Title,
                    UniqueInfo = solutionViewModel.UniqueInfo,  
                    UserId = User.Identity.GetUserId()
                };

                solution.SolutionTypes.Clear();
                solution.Industries.Clear();

                solution.SolutionTypes = typeService.GetSolutionTypesByIds(solutionViewModel.SelectedSolutionTypes);
                solution.Industries = typeService.GetIndustriesByIds(solutionViewModel.SelectedIndustries);

                solutionService.AddSolution(solution);
                return RedirectToAction("All");
            }

            TempData["ViewData"] = ViewData;

            return RedirectToAction("Add", solutionViewModel);
        }

        [Authorize(Roles = "Investor")]
        public ActionResult Follow(int id = 0)
        {
            var userId = User.Identity.GetUserId();

            if (!string.IsNullOrEmpty(userId))
            {
                var favoriteSolutions = solutionService.GetAllFavoriteSolutionsByUserId(userId);

                if (favoriteSolutions.Any(x => x.FollowedSolutionId == id))
                {
                    var solutionToRemove = favoriteSolutions.Where(x => x.FollowedSolutionId == id).First();
                    solutionService.RemoveFavoriteSolution(solutionToRemove);
                } else
                {
                    var favoriteSolution = new FavoriteSolution()
                    {
                        FollowedSolutionId = id,
                        FollowedUserId = userId
                    };

                    solutionService.AddFavoriteSolution(favoriteSolution);
                }
            }

            return RedirectToAction("View", new { id = id});
        }

        private void ValidateSolutionModel(SolutionViewModel model)
        {
            if (model.CityId == 0)
            {
                ModelState.AddModelError("", "Please select a city");
            }

            if (model.SelectedSolutionTypes == null || model.SelectedSolutionTypes.Count == 0)
            {
                ModelState.AddModelError("", "Please select at least one kind of solution");
            }

            if (model.SelectedIndustries == null || model.SelectedIndustries.Count == 0)
            {
                ModelState.AddModelError("", "Please select at least one industry");
            }

            if (model.ImplementationStatusId == 0)
            {
                ModelState.AddModelError("", "Please select an implementation status");
            }

            if (model.CurrencyId == 0)
            {
                ModelState.AddModelError("", "Please select a currency");
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