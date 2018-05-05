using InvestmentPlatform.Application.Interfaces;
using InvestmentPlatform.Application.Services;
using InvestmentPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvestmentPlatform.Controllers
{
    public class SolutionController : Controller
    {
        ITypeService typeService { get; set; }

        public SolutionController()
        {
            typeService = new TypeService();
        }

        // GET: Solution
        public ActionResult View()
        {
            var testSolutionModel = new SolutionViewModel();
            testSolutionModel.Title = "Solution title";
            testSolutionModel.CityId = 5;
            testSolutionModel.ImplementationStatusId = 2;
            testSolutionModel.SolutionDescription = "test description test descriptiontest descriptiontest descriptiontest descriptiontest descriptiontest descriptiontest descriptiontest descriptiontest descriptiontest descriptiontest descriptiontest description";
            testSolutionModel.Industry = "Test Industry";
            testSolutionModel.FromInvestmentSize = 3;
            testSolutionModel.ToInvestmentSize = 5;
            testSolutionModel.CurrencyId = 2;
            testSolutionModel.UniqueInfo = "Something unique about solution test";
            return View(testSolutionModel);
        }

        public ActionResult All()
        {
            return View();
        }

        public ActionResult Edit()
        {
            var solutionViewModel = new SolutionViewModel();

            solutionViewModel.SolutionTypes = typeService.GetAllSolutionTypes();

            return View(solutionViewModel);
        }
    }
}