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
        [HttpGet]
        public ActionResult AddProduct()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("LogIn", "Account");
            }
            ViewBag.type = new SelectList(db.ProductTypes.ToList().OrderBy(n => n.IdProductType), "IdProductType", "NameProductType");
            ViewBag.size = new SelectList(db.SizeProducts.ToList().OrderBy(n => n.NameSizeProduct), "IdSizeProduct", "NameSizeProduct");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddProduct(Product sp, FormCollection collection, HttpPostedFileBase fileUpload)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("LogIn", "Account");
            }
            ViewBag.type = new SelectList(db.ProductTypes.ToList().OrderBy(n => n.IdProductType), "IdProductType", "NameProductType");
            ViewBag.size = new SelectList(db.SizeProducts.ToList().OrderBy(n => n.NameSizeProduct), "IdSizeProduct", "NameSizeProduct");

            var ten = collection["name"];
            var gia = collection["price"];
            var Date = collection["update"];
            var mota = collection["describe"];            
            var loai = collection["type"];
            //var size = collection["Size"];


            var image = Path.GetFileName(fileUpload.FileName);
            var path = Path.Combine(Server.MapPath("~/Assets/img/Clothes)"), image);
            if (System.IO.File.Exists(path))
            {
                ViewBag.ThongBaoAnh = "Hình Ảnh Đã Tồn Tại";
                return View();
            }
            else
            {
                fileUpload.SaveAs(path);
            }          

            sp.NameProduct = ten;
            sp.ImageProduct = image;
            sp.PriceProduct = int.Parse(gia);
            sp.DescribeProduct = mota;
            sp.UpdateDate = DateTime.Parse(Date);
            sp.IdProductType = Int32.Parse(loai);
            //sp.id = string.Parse(size);
            db.Products.InsertOnSubmit(sp);
            db.SubmitChanges();
            return RedirectToAction("Product", "Manage");
        }

    }
}