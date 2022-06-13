using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using DoAn_Web_SellClothes.Models;
using PagedList.Mvc;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace DoAn_Web_SellClothes.Areas.Admin.Controllers
{
    public class ManageController : Controller
    {
        DataClasses1DataContext db =new DataClasses1DataContext();
        // GET: Admin/Manage
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        public ActionResult Receipt(int? page)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("LogIn", "Account");
            }
            int pagesize = 25;
            int pageNum = (page ?? 1);
            var list = db.Invoices.OrderByDescending(s => s.IdInvoice).ToList();
            return View(list.ToPagedList(pageNum, pagesize));
        }

        public ActionResult DetailReceipt()
        {
            return View();
        }

        public ActionResult Customer(int? page)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("LogIn", "Account");
            }
            
            int pagesize = 25;
            int pageNum = (page ?? 1);
            var list = db.Accounts.OrderByDescending(s => s.IdAccount).ToList();
            return View(list.ToPagedList(pageNum, pagesize));
        }

        public ActionResult Sex()
        {
            return View();
        }

        public ActionResult TypesClothes(int ? page)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("LogIn", "Account");
            }
            int pagesize = 25;
            int pageNum = (page ?? 1);
            var list = db.ProductTypes.OrderByDescending(s => s.IdProductType).ToList();
            return View(list.ToPagedList(pageNum, pagesize));
        }
        public ActionResult AddTypesClothes()
        {
            return View();
        }

        public ActionResult EditTypesClothes()
        {
            return View();
        }

        //public ActionResult SizeProducts(int id)
        //{
        //    var list = db.SizeProducts.Where(n => n.IdSizeProduct == id);
        //    return View(list.Single());
        //}
        private List<Invoice> Receipt(int count)
        {
            return db.Invoices.OrderByDescending(s => s.IdInvoice).Take(count).ToList();
        }
        private List<Product> produdtt(int count)
        {
            return db.Products.OrderByDescending(s => s.IdProduct).Take(count).ToList();
        }
        private List<Account> Customer(int count)
        {
            return db.Accounts.OrderByDescending(s => s.IdAccount).Take(count).ToList();
        }
        private List<ProductType> type(int count)
        {
            return db.ProductTypes.OrderByDescending(s => s.IdProductType).Take(count).ToList();
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
            ViewBag.Loai = new SelectList(db.ProductTypes.ToList().OrderBy(n => n.NameProductType), "IdProductType", "NameProductType");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddProduct(Product pr, FormCollection collection, HttpPostedFileBase fileUpload)
        {
            
            if (Session["admin"] == null)
            {
                return RedirectToAction("LogIn", "Account");
            }
            ViewBag.Loai = new SelectList(db.ProductTypes.ToList().OrderBy(n => n.IdProductType), "IdProductType", "NameProductType");
            //ViewBag.size = new SelectList(db.SizeProducts.ToList().OrderBy(n => n.NameSizeProduct), "IdSizeProduct", "NameSizeProduct");

            var ten = collection["name"];
            var gia = collection["price"];
            var Date = collection["update"];
            var mota = collection["describe"];            
            var loai = collection["Loai"];
            var status = collection["status"]; 
            //var size = collection["Size"];


            var filename = Path.GetFileName(fileUpload.FileName);
            var path = Path.Combine(Server.MapPath("~/Assets/img/Clothes"), filename);
            if (System.IO.File.Exists(path))
            {
                ViewBag.ThongBaoAnh = "Hình Ảnh Đã Tồn Tại";
                return View();  
            }
            else
            {
                fileUpload.SaveAs(path);
            }

            pr.NameProduct = ten;
            pr.ImageProduct = filename;
            pr.PriceProduct = int.Parse(gia);
            pr.DescribeProduct = mota;
            pr.UpdateDate = DateTime.Parse(Date);
            pr.IdProductType = Int32.Parse(loai);
            pr.StatusProduct = int.Parse(status);
            //sp.id = string.Parse(size);
            db.Products.InsertOnSubmit(pr);
            db.SubmitChanges();
            return RedirectToAction("Product", "Manage");
        }      
        public ActionResult EditProduct()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            else
            {
                Product sp = db.Products.SingleOrDefault(n => n.IdProduct == id);
                ViewBag.MaSP = sp.IdProduct;
                if (sp == null)
                { 
                    Response.StatusCode = 404;
                    return null;
                }
                return View(sp);
            }
        }
        [HttpPost]
        public ActionResult DeleteProduct(int id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Product", "Manage");
            }
            else
            {
                Product sp = db.Products.SingleOrDefault(n => n.IdProduct == id);
                ViewBag.MaSP = sp.IdProduct;
                if (sp == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                db.Products.DeleteOnSubmit(sp);
                db.SubmitChanges();
                return RedirectToAction("Product");
            }
        }

    }
}