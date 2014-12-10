using System;
using System.Collections.Generic;

namespace GlennsGadgets.Models
{
    public class Gadget
    {
        public Gadget()
        {
            this.Bids = new List<GadgetBid>();
        }
        public string Title { get; set; }

        public string Description { get; set; }

        public List<GadgetBid> Bids { get; set; }
    }

    public class GadgetBid
    {
        public string Username { get; set; }
        public DateTime BidDate { get; set; }

        public double BidAmount { get; set; }
    }

    public class User
    {
        public string SessionId { get; set; }
        public string Id { get; set; }
    }
}