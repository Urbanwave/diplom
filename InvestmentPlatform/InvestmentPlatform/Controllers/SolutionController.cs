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
        // GET: Solution
        public ActionResult View()
        {
            var testSolutionModel = new SolutionViewModel();
            testSolutionModel.Title = "Solution title";
            testSolutionModel.Location = "Test Location";
            testSolutionModel.SolutionDescription = "test description test descriptiontest descriptiontest descriptiontest descriptiontest descriptiontest descriptiontest descriptiontest descriptiontest descriptiontest descriptiontest descriptiontest description";
            testSolutionModel.KindProduct = true;
            testSolutionModel.Industry = "Test Industry";
            testSolutionModel.Images = @"F:\diplom\InvestmentPlatform\InvestmentPlatform\Content\images\homepage";
            testSolutionModel.ImplementationStatus = "test ImplementationStatus";
            testSolutionModel.TotalCost = "test TotalCost";
            testSolutionModel.InvestmentSize = "test InvestmentSize";
            testSolutionModel.UniqueInfo = "Something unique about solution test";
            return View(testSolutionModel);
        }

        public ActionResult All()
        {
            return View();
        }

    }
}