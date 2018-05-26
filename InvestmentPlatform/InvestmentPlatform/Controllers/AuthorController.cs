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

        public AuthorController()
        {
            investorService = new InvestorService();
            locationService = new LocationService();
            userService = new UserService();
            typeService = new TypeService();
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
    }
}