using MvcAuction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MvcAuction.Controllers
{
    public class AuctionsController : Controller
    {
        // GET: Auction
        [AllowAnonymous]
        [OutputCache(Duration = 1)]
        public ActionResult Index()
        {
            var db = new MvcAuction.Models.AuctionsDBContext();
            var auctions = db.Auctions;
            return View(auctions);
        }

        public ActionResult TempDataDemo()
        {
            TempData["SuccessMessage"] = "The Success Message";
            return RedirectToAction("Index");
        }

        public ActionResult Auction(long id)
        {
            var db = new Models.AuctionsDBContext();
            var auction = db.Auctions.Find(id);

            // ViewData["Auction"] = auction;
            // return View();

            return View(auction);
        }

        [HttpGet]
        [OutputCache(Duration = 1)]
        public ActionResult Create()
        {   
            var SelectList = new SelectList(new[] { "Automotive", "Electronics", "Games" });
            ViewBag.SelectList = SelectList;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create([Bind (Exclude ="CurrentPrice")]Models.Auction auction)
        {
            if (ModelState.IsValid)
            {
                //Save it to the Database
                var db = new MvcAuction.Models.AuctionsDBContext();
                db.Auctions.Add(auction);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
                   
            return Create();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Bid(Bid bid)
        {
            var db = new MvcAuction.Models.AuctionsDBContext();
            var auction = db.Auctions.Find(bid.AuctionId);
            if (auction == null)
            {
                ModelState.AddModelError("AuctionId", "Auction Not Found!");
            }
            else if (auction.CurrentPrice >= bid.Amount)
            {
                ModelState.AddModelError("Amount", "Bid Amount Must Exceed Current Bid");
            }
            else
            {
                bid.Username = User.Identity.Name;
                auction.Bids.Add(bid);
                auction.CurrentPrice = bid.Amount;
                db.SaveChanges();
            }

            if (!Request.IsAjaxRequest())
                return RedirectToAction("Auction", new { id = bid.AuctionId});
            return Json
                (
                    new {
                            CurrentPrice = bid.Amount.ToString("C"),
                            BidCount = auction.BidCount
                        }
                );
        }
    }
}