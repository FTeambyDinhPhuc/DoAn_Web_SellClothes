using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_Web_SellClothes.Models;

namespace DoAn_Web_SellClothes.Areas.Admin.Controllers
{
    
    public class StatisticalController : Controller
    {
        // GET: Admin/Statistical
        DataClasses1DataContext db = new DataClasses1DataContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Statistical()
        {
            ViewBag.Receiptcount = Receiptcount();
            ViewBag.Customercount = Customercount();
            ViewBag.Productcount = Productcount();
            return View();
        }
        private int Customercount()
        {
           var count = db.Accounts.OrderByDescending(s => s.IdAccount).Count();
            return count;
        }
        private int Productcount()
        {
            var count = db.Products.OrderByDescending(s => s.IdProduct).Count();
            return count;
        }
        private int Receiptcount()
        {
            var count = db.Invoices.OrderByDescending(s => s.IdInvoice).Count();
            return count;
        }
    }
}