using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GlennsGadgets.Controllers
{
    public class DataController : Controller
    {
        // GET: Data
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Gadgets()
        {
            return Json(Hubs.BidHub.db);
        }
    }
}