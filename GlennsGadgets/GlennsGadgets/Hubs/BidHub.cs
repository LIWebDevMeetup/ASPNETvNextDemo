using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using GlennsGadgets.Models;
using System.Threading.Tasks;

namespace GlennsGadgets.Hubs
{
    [HubName("bids")]
    public class BidHub : Hub
    {
        internal static List<Gadget> db = new List<Gadget>();
        internal static List<User> users = new List<User>();

        public override Task OnDisconnected(bool stopCalled)
        {
            var id = Context.ConnectionId;
            if (users.Any(q => q.Id == id))
            {
                users.Remove(users.First(q => q.Id == id));
                Clients.All.setCount(users.Count);
            }
            return base.OnDisconnected(stopCalled);
        }

        static BidHub()
        {
            // Create Fake Data
            db.Add(new Gadget { Title = "Gadget 1", Description = "It opens beer bottles" });
            db.Add(new Gadget { Title = "Gadget 2", Description = "It opens beer bottles" });
            db.Add(new Gadget { Title = "Gadget 3", Description = "It opens beer bottles" });
            db.Add(new Gadget { Title = "Gadget 4", Description = "It opens beer bottles" });
        }

        public void Connect(string sessionId)
        {
            // remember who is online... 
            users.Add(new User { SessionId = sessionId, Id = Context.ConnectionId });
        }

        public void GetSessionCount()
        {
            Clients.All.setCount(users.Count);
        }

        public void PlaceBid(string title)
        {
            var g = db.FirstOrDefault(q => q.Title == title);
            if (g != null)
            {
                var amt = (g.Bids.Any()) ? g.Bids.Max(a => a.BidAmount) : 0;
                amt++;
                var user = users.FirstOrDefault(q => q.Id == Context.ConnectionId);

                g.Bids.Add(new GadgetBid { BidAmount = amt, BidDate = DateTime.Now, Username = user.Id });
                Clients.All.updateGadgetBids(title, amt);
            }
        }

    }
}