using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcAuction.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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
        [OutputCache(Duration = 3600)]
        public ActionResult CategoryNavigation()
        {
            var db = new MvcAuction.Models.AuctionsDBContext();
            var categories = db.Auctions.Select(x=>x.Category).Distinct();
            ViewBag.Categories = categories.ToArray();
            return PartialView();
        }
    }
}