using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.ObjectModel;

namespace MvcAuction.Models
{
    public class Auction
    {
        [Required]
        public long Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Category { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 200, MinimumLength = 5)]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime StartedTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }

        [DataType(DataType.Currency)]
        public decimal StartPrice { get; set; }

        [DataType(DataType.Currency)]
        public decimal? CurrentPrice { get; set; }

        public virtual Collection<Bid> Bids { get; private set; }
        public int BidCount
        {
            get { return Bids.Count; }
        }

        public Auction()
        {
            Bids = new Collection<Bid>();
        }
    }
}