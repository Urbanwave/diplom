using InvestmentPlatform.Application.Interfaces;
using InvestmentPlatform.Application.Services;
using InvestmentPlatform.Application.ViewModels;
using InvestmentPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvestmentPlatform.Controllers
{
    public class HomeController : Controller
    {
        ISolutionService solutionService { get; set; }
        IUserService userService { get; set; }

        public HomeController()
        {
            solutionService = new SolutionService();
            userService = new UserService();
        }

        public ActionResult Index()
        {
            var mainPageViewModel = new MainPageViewModel();
            var solutionViewModels = new List<SolutionViewModel>();

            var solutions = solutionService.GetAllSolutions(1,int.MaxValue).OrderByDescending(x => x.Id).Take(3);

            foreach (var solution in solutions)
            {
                var solutionViewModel = new SolutionViewModel();

                solutionViewModel.Id = solution.Id;
                solutionViewModel.Title = solution.Title;
                solutionViewModel.FileName = solution.LogoFileName;
                solutionViewModel.SolutionDescription = solution.SolutionDescription;
                solutionViewModels.Add(solutionViewModel);
            }

            mainPageViewModel.SolutionViewModels = solutionViewModels;
            mainPageViewModel.SolutionsAmount = solutionService.GetSolutionsAmount();
            mainPageViewModel.AuthorsAmount = userService.GetAuthorsAmount();
            mainPageViewModel.InvestorsAmount = userService.GetInvestorsAmount();
            mainPageViewModel.FavouriteSolutionsAmount = solutionService.GetFavouriteSolutionsAmount();

            return View(mainPageViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}