using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn_Web_SellClothes.Controllers
{
    public class HistoryController : Controller
    {
        // GET: History
        public ActionResult History()
        {
            return View();
        }

        public ActionResult HistoryDetails()
        {
            return View();
        }
    }
}