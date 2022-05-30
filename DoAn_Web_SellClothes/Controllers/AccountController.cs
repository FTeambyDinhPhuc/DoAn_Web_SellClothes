using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn_Web_SellClothes.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult AccountInformation()
        {
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
    }
}