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
    public class AdminController : Controller
    {
        ISolutionService solutionService { get; set; }
        IInvestorService investorService { get; set; }
        IAuthorService authorService { get; set; }

        public AdminController()
        {
            solutionService = new SolutionService();
            investorService = new InvestorService();
            authorService = new AuthorService();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Solutions()
        {
            ViewBag.IsProfilePage = true;

            var allSolutionViewModel = new Application.ViewModels.AllSolutionsViewModel();

            var projectAmount = solutionService.GetSolutionsAmount();
            allSolutionViewModel.AllProjectsCount = projectAmount;

            var solutions = solutionService.GetAllSolutions(1, int.MaxValue);
            var solutionViewModels = new List<SolutionViewModel>();

            foreach (var solution in solutions)
            {
                var solutionViewModel = new SolutionViewModel();

                solutionViewModel.Id = solution.Id;
                solutionViewModel.Title = solution.Title;
                solutionViewModels.Add(solutionViewModel);
            }

            allSolutionViewModel.SolutionViewModels = solutionViewModels;

            return View(allSolutionViewModel);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Authors()
        {
            ViewBag.IsProfilePage = true;

            var allAuthorsViewModel = new AllAuthorsViewModel();
            var authors = authorService.GetAllAuthors(1, int.MaxValue);
            var authorViewModels = new List<AuthorEditViewModel>();

            foreach (var author in authors)
            {
                var authorViewModel = new AuthorEditViewModel();

                authorViewModel.Id = author.Id;
                authorViewModel.FirstName = author.FirstName;
                authorViewModel.LastName = author.LastName;
                authorViewModel.JobTitle = author.JobTitle;

                authorViewModels.Add(authorViewModel);
            }

            allAuthorsViewModel.AuthorViewModels = authorViewModels;

            return View(allAuthorsViewModel); ;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Investors()
        {
            ViewBag.IsProfilePage = true;

            var allInvestorsViewModel = new AllInvestorsViewModel();
            var investors = investorService.GetAllInvestors(1, int.MaxValue);
            var investorViewModels = new List<InvestorViewModel>();

            foreach (var investor in investors)
            {
                var investorViewModel = new InvestorViewModel();

                investorViewModel.Id = investor.Id;
                investorViewModel.FirstName = investor.FirstName;
                investorViewModel.LastName = investor.LastName;
                investorViewModel.CompanyName = investor.CompanyName;

                investorViewModels.Add(investorViewModel);
            }

            allInvestorsViewModel.InvestorViewModels = investorViewModels;

            return View(allInvestorsViewModel);
        }
    }
}