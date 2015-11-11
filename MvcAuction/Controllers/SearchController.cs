using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcAuction.Controllers
{
    public class SearchController : AsyncController
    {
        public async Task<ActionResult> Auctions(string keyword)
        {
            var auctions = await Task.Run<IEnumerable<Models.Auction>>
                (
                    ()=> 
                    {
                        var db = new Models.AuctionsDBContext();
                        return db.Auctions.Where(x=>x.Title.Contains(keyword)).ToArray();
                    }
                );
            return Json(auctions, JsonRequestBehavior.AllowGet);
        }
    }
}
