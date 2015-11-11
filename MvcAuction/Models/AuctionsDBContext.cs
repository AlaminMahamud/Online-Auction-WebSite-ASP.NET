using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace MvcAuction.Models
{
    public class AuctionsDBContext : DbContext
    {
        public DbSet<Auction> Auctions { get; set; }
        static AuctionsDBContext ()
            {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AuctionsDBContext>());
            }
    }

}