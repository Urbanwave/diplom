using InvestmentPlatform.Application.Interfaces;
using InvestmentPlatform.Application.Services;
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

        public SolutionController()
        {
            typeService = new TypeService();
            solutionService = new SolutionService();
            locationService = new LocationService();
        }

        // GET: Solution
        public ActionResult View(int id)
        {
            var solutionViewModel = new SolutionViewModel();

            var solution = solutionService.GetSolutionById(id);

            if (solution != null)
            {
                MapSolutionViewModel(solutionViewModel, solution);
                solution.City.Country = locationService.GetCountryByCityId(solution.CityId);
            }

            return View(solutionViewModel);
        }

        public ActionResult All()
        {
            var solutions = solutionService.GetAllSolutions();
            var solutionViewModels = new List<SolutionViewModel>();

            foreach (var solution in solutions)
            {
                var solutionViewModel = new SolutionViewModel();

                MapSolutionViewModel(solutionViewModel, solution);
                solution.City.Country = locationService.GetCountryByCityId(solution.CityId);
                solutionViewModels.Add(solutionViewModel);
            }

            return View(solutionViewModels);
        }

        [Authorize(Roles = "Author")]
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
                }
            }

            return View(solutionViewModel);
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

                solution.SolutionTypes.AddRange(typeService.GetSolutionTypesByIds(solutionViewModel.SelectedSolutionTypes));
                solution.Industries.AddRange(typeService.GetIndustriesByIds(solutionViewModel.SelectedIndustries));

                solutionService.AddSolution(solution);
                return RedirectToAction("All");
            }

            return RedirectToAction("Edit", solutionViewModel);
        }

        private void ValidateSolutionModel(SolutionViewModel model)
        {
            if (model.CityId == 0)
            {
                ModelState.AddModelError("", "Please select a city");
            }

            if (model.SelectedSolutionTypes?.Count == 0)
            {
                ModelState.AddModelError("", "Please select at least one kind of solution");
            }

            if (model.SelectedIndustries?.Count == 0)
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
        }
    }
}