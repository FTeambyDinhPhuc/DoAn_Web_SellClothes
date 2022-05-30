using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn_Web_SellClothes.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Cart()
        {
            return View();
        }

        public ActionResult Checkout()
        {
            return View();
        }
    }
}