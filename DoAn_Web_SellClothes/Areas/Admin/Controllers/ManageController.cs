using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using DoAn_Web_SellClothes.Models;
using PagedList.Mvc;
using System.IO;

namespace DoAn_Web_SellClothes.Areas.Admin.Controllers
{
    public class ManageController : Controller
    {
        DataClasses1DataContext db =new DataClasses1DataContext();
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

        public ActionResult TypesClothes(int id)
        {          
            var list = db.ProductTypes.Where(n => n.IdProductType == id );
            return View(list.Single());
        }
        public ActionResult SizeProducts(int id)
        {
            var list = db.SizeProducts.Where(n => n.IdSizeProduct == id);
            return View(list.Single());
        }
        private List<Product> produdtt(int count)
        {
            return db.Products.OrderByDescending(s => s.IdProduct).Take(count).ToList();
        }
        public ActionResult Product(int ? page)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("LogIn", "Account");
            }
            int pagesize = 25;
            int pageNum = (page ?? 1);
            var list = db.Products.OrderByDescending(s => s.IdProduct).ToList();
            return View(list.ToPagedList(pageNum, pagesize));
        }
        

    }
}