﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn_Web_SellClothes.Areas.Admin.Controllers
{
    public class ManageController : Controller
    {
        // GET: Admin/Manage
        public ActionResult Receipt()
        {
            return View();
        }

        public ActionResult Customer()
        {
            return View();
        }

        public ActionResult Sex()
        {
            return View();
        }

        public ActionResult TypesClothes()
        {
            return View();
        }
        public ActionResult Product()
        {
            return View();
        }

    }
}