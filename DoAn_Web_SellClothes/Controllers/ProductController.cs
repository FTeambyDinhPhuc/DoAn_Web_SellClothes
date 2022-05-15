using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn_Web_SellClothes.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult ProductPage()
        {
            return View();
        }
    }
}