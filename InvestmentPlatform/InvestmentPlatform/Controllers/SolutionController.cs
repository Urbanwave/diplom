using InvestmentPlatform.Application.Interfaces;
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

        public ActionResult Filter(AllSolutionsViewModel model, string sortBy, string sortDestination,
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
                Session["Filter"] = model;
            } else
            if (Session["Filter"] != null)
            {
                model = (AllSolutionsViewModel)Session["Filter"];
            }

            int pageSize = 6;

            if(model.ToInvestmentSize == 0 && model.FromInvestmentSize == 0)
            {
                model.ToInvestmentSize = int.MaxValue;
            }
            else
            if (model.ToInvestmentSize == 0 && model.FromInvestmentSize != 0)
            {
                model.ToInvestmentSize = int.MaxValue;
            }

            var countriesCities = locationService.GetCountriesCityIds(model.SelectedCountries);
            model.SelectedCities = model.SelectedCities.Union(countriesCities).ToList();

            var solutions = solutionService.GetAllSolutions(1, int.MaxValue).Where(x => (x.SolutionDescription.Contains(searchString)
            || x.Title.Contains(searchString) || x.Currency.Name.Contains(searchString) || x.City.Name.Contains(searchString)
            || x.InvestmentSize.ToString().Contains(searchString))        
            && x.InvestmentSize >= model.FromInvestmentSize && x.InvestmentSize <= model.ToInvestmentSize);
            
            if(model.SelectedCities.Count > 0)
            {
                solutions = solutions.Where(x => model.SelectedCities.Contains(x.CityId));
            }

            if (model.SelectedSolutionStatuses.Count > 0)
            {
                solutions = solutions.Where(x => model.SelectedSolutionStatuses.Contains(x.ImplementationStatusId));
            }

            if (model.SelectedSolutionTypes.Count > 0)
            {
                solutions = solutions.Where(x => x.SolutionTypes.Any(y => model.SelectedSolutionTypes.Contains(y.Id)));
            }

            if (model.SelectedIndustries.Count > 0)
            {
                solutions = solutions.Where(x => x.Industries.Any(y => model.SelectedIndustries.Contains(y.Id)));
            }

            if(sortBy == "date")
            {
                if (sortDestination == "up")
                {
                    solutions = solutions.OrderBy(x => x.DateCreated);
                }
                else
                {
                    solutions = solutions.OrderByDescending(x => x.DateCreated);
                }
            } else
            if(sortBy == "investmentSize")
            {
                if (sortDestination == "up")
                {
                    solutions = solutions.OrderBy(x => x.InvestmentSize);
                }
                else
                {
                    solutions = solutions.OrderByDescending(x => x.InvestmentSize);
                }
            }

            ViewBag.SolutionsAmount = solutions.Count();

            var solutionViewModels = new List<SolutionViewModel>();

            foreach (var solution in solutions)
            {
                var solutionViewModel = new SolutionViewModel();

                MapSolutionViewModel(solutionViewModel, solution);
                solutionViewModel.City.Country = locationService.GetCountryByCityId(solution.CityId);
                solutionViewModels.Add(solutionViewModel);
            }

            return PartialView(solutionViewModels.ToPagedList(page, pageSize));
        }

        public ActionResult All()
        {
            var allSolutionViewModel = new AllSolutionsViewModel();

            ViewBag.IsSolutionsPage = true;

            allSolutionViewModel.Countries = locationService.GetAllCountries().OrderBy(x => x.Name).ToList(); ;
            allSolutionViewModel.Cities = locationService.GetAllCities().OrderBy(x => x.Name).ToList();
            allSolutionViewModel.Industries = typeService.GetAllIndustries().OrderBy(x => x.Name).ToList();
            allSolutionViewModel.ImplementationStatuses = typeService.GetAllImplementationStatuses().OrderBy(x => x.Name).ToList();
            allSolutionViewModel.SolutionTypes = typeService.GetAllSolutionTypes().OrderBy(x => x.Name).ToList();

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
                    solutionViewModel.FileName = "../Content/Images/profile/" + solution.LogoFileName;
                }
            }

            return View(solutionViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Author, Admin")]
        public ActionResult Edit(SolutionViewModel solutionViewModel, HttpPostedFileBase file)
        {
            ValidateSolutionModel(solutionViewModel);

            var pictureName = string.Empty;

            if (file != null)
            {
                pictureName = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("/Content/Images/profile"), pictureName);
                file.SaveAs(path);
                solutionViewModel.FileName = "../Content/Images/profile/" + pictureName;
            }

            if (ModelState.IsValid)
            {
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

            var pictureName = string.Empty;

            if (file != null)
            {
                pictureName = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("/Content/Images/profile"), pictureName);
                file.SaveAs(path);
                solutionViewModel.FileName = "../Content/Images/profile/" + pictureName;
            }

            if (ModelState.IsValid)
            {
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
                    UserId = User.Identity.GetUserId(),
                    DateCreated = DateTime.Now
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
            solutionViewModel.FileName = !string.IsNullOrEmpty(solution.LogoFileName) ? solution.LogoFileName : "solutiondefault.png";
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
            solutionViewModel.Email = solution.User.Email;
            solutionViewModel.DateCreated = solution.DateCreated;

            var userId = User.Identity.GetUserId();
            var favoriteSolution = solutionService.GetAllFavoriteSolutionsByUserId(userId);

            solutionViewModel.IsFollowed = favoriteSolution.Select(x => x.FollowedSolutionId).Contains(solution.Id);
        }
    }
}