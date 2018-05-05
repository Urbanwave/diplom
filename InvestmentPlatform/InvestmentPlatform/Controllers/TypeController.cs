using InvestmentPlatform.Application.Interfaces;
using InvestmentPlatform.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvestmentPlatform.Controllers
{
    public class TypeController : Controller
    {
        ITypeService typeService { get; set; }

        public TypeController()
        {
            typeService = new TypeService();
        }

        public ActionResult GetImplementationStatuses()
        {
            var statuses = typeService.GetAllImplementationStatuses();
            return Json(statuses, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCurrencies()
        {
            var currencies = typeService.GetAllCurrencies();
            return Json(currencies, JsonRequestBehavior.AllowGet);
        }
    }
}