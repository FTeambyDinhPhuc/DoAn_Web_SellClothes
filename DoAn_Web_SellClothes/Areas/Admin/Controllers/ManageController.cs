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

        private List<Invoice> Receipt(int count)
        {
            return db.Invoices.OrderByDescending(s => s.IdInvoice).Take(count).ToList();
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
        //public ActionResult DetailReceipt(int id)
        //{ n
        //    InvoiceDetail ct = db.InvoiceDetails.SingleOrDefault(n => n.IdInvoice == id);
        //    ViewBag.IdInvoice = Invoice.IdInvoice;
        //    if()
        //}

        private List<Account> Customer(int count)
        {
            return db.Accounts.OrderByDescending(s => s.IdAccount).Take(count).ToList();
        }

        public ActionResult Customer(int? page)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("LogIn", "Account");
            }
            
            int pagesize = 25;
            int pageNum = (page ?? 1);
            var list = db.Accounts.OrderByDescending(s => s.IdAccount ).ToList();
            return View(list.ToPagedList(pageNum, pagesize));
        }

        public ActionResult Sex()
        {
            return View();
        }
        private List<SizeProduct> size(int count)
        {
            return db.SizeProducts.OrderByDescending(s => s.IdSizeProduct).Take(count).ToList();
        }
        private List<ProductType> type(int count)
        {
            return db.ProductTypes.OrderByDescending(s => s.IdProductType).Take(count).ToList();
        }
        private List<ProductDetail> detail(int count)
        {
            return db.ProductDetails.OrderByDescending(s => s.IdProduct).Take(count).ToList();
        }
        public ActionResult TypesClothes(int ? page)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("LogIn", "Account");
            }
            int pagesize = 25;
            int pageNum = (page ?? 1);
            var list = db.ProductTypes.OrderByDescending(s => s.IdProductType ).ToList();
            return View(list.ToPagedList(pageNum, pagesize));
        }
        [HttpGet]
        public ActionResult AddTypesClothes()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("LogIn", "Account");
            }
            
            return View();
        }     

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddTypesClothes(ProductType pr, FormCollection collection)
        {           
            if (Session["admin"] == null)
            {
                return RedirectToAction("LogIn", "Account");
            }
            var ten = collection["name"];        
            var s = collection["sex"];
            if( int.Parse(s) != 1 || int.Parse(s) != 0)
            {
                ViewData["1"] = "Bạn đã nhập sai !";
            }
            pr.NameProductType = ten;
            pr.IdSex = Int32.Parse(s);
            db.ProductTypes.InsertOnSubmit(pr);
            db.SubmitChanges();
            return RedirectToAction("TypesClothes", "Manage");
        }

        [HttpGet]
        public ActionResult EditTypesClothes(int id)
        {

            if (Session["admin"] == null)
            {
                return RedirectToAction("Product", "Manage");
            }
            else
            {
                ProductType type = db.ProductTypes.SingleOrDefault(n => n.IdProductType == id);
                if (type == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }                             
                return View(type);
            }
        }

        [HttpPost, ActionName("EditTypesClothes")]
        public ActionResult eEditTypesClothes(FormCollection collection, int id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Product", "Manage");
            }
            else
            {
                ProductType type = db.ProductTypes.SingleOrDefault(n => n.IdProductType == id);
                if (type == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                UpdateModel(type);
                db.SubmitChanges();
                return RedirectToAction("TypesClothes");
            }
        }

        [HttpGet]
        public ActionResult DeleteProductType(int id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Product", "Manage");
            }
            else
            {
                ProductType type = db.ProductTypes.SingleOrDefault(n => n.IdProductType == id);
                if (type == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(type);
            }
        }

        [HttpPost, ActionName("DeleteProductType")]
        public ActionResult dDeleteProductType(int id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Product", "Manage");
            }
            else
            {
                ProductType type = db.ProductTypes.SingleOrDefault(n => n.IdProductType == id);
                if (type == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                db.ProductTypes.DeleteOnSubmit(type);
                db.SubmitChanges();
                return RedirectToAction("TypesClothes");
            }
        }

        private List<Product> produdt(int count)
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
            ViewBag.Loai = new SelectList(db.ProductTypes.ToList().OrderBy(n => n.NameProductType), "IdProductType", "NameProductType");
            ViewBag.Size = new SelectList(db.SizeProducts.ToList().OrderBy(n => n.NameSizeProduct), "IdSizeProduct", "NameSizeProduct");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddProduct(Product pr, ProductDetail dt, FormCollection collection, HttpPostedFileBase fileUpload)
        {
            
            if (Session["admin"] == null)
            {
                return RedirectToAction("LogIn", "Account");
            }
            ViewBag.Loai = new SelectList(db.ProductTypes.ToList().OrderBy(n => n.IdProductType), "IdProductType", "NameProductType");
            ViewBag.Size = new SelectList(db.SizeProducts.ToList().OrderBy(n => n.NameSizeProduct), "IdSizeProduct", "NameSizeProduct");

            var ten = collection["name"];
            var gia = collection["price"];
            var Date = collection["update"];
            var mota = collection["describe"];            
            var loai = collection["Loai"];
            var size = collection["Size"];
            //var sl = collection["quality"];
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
            //dt.IdSizeProduct = Int32.Parse(size);
            //dt.SoLuongTon = int.Parse(sl);
            db.Products.InsertOnSubmit(pr);
            //db.Products.InsertOnSubmit(dt);
            db.SubmitChanges();
            return RedirectToAction("Product", "Manage");
        }

        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Product", "Manage");
            }
            else
            {
                Product sp = db.Products.SingleOrDefault(n => n.IdProduct == id);
                if (sp == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                ViewBag.Loai = new SelectList(db.ProductTypes.ToList().OrderBy(n => n.IdProductType), "IdProductType", "NameProductType");
                //ViewBag.Size = new SelectList(db.SizeProducts.ToList().OrderBy(n => n.NameSizeProduct), "IdSizeProduct", "NameSizeProduct");
                return View(sp);
            }
        }

        [HttpPost, ActionName("EditProduct")]
        public ActionResult eEditProduct(FormCollection collection, int id, HttpPostedFileBase fileUpload)
        {
            var img = "";
            ViewBag.Loai = new SelectList(db.ProductTypes.ToList().OrderBy(n => n.IdProductType), "IdProductType", "NameProductType");
            if (Session["admin"] == null)
            {
                return RedirectToAction("Product", "Manage");
            }
            if (fileUpload != null)
            {
                img = Path.GetFileName(fileUpload.FileName);
                var path = Path.Combine(Server.MapPath("~/Sản Phẩm"), img);
                if (!System.IO.File.Exists(path))//Sản Phẩm Chưa Tồn Tại
                {
                    fileUpload.SaveAs(path);
                }
            }
            else
            {
                img = collection["Anh"];
            }
            Product sp = db.Products.SingleOrDefault(n => n.IdProduct == id);
            sp.ImageProduct = img;
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            UpdateModel(sp);
            db.SubmitChanges();
            return RedirectToAction("Product");
        }

        [HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Product", "Manage");
            }
            else
            {
                Product sp = db.Products.SingleOrDefault(n => n.IdProduct == id);
                if (sp == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(sp);
            }
        }

        [HttpPost, ActionName("DeleteProduct")]
        public ActionResult dDeleteProduct(int id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Product", "Manage");
            }
            else
            {
                Product sp = db.Products.SingleOrDefault(n => n.IdProduct == id);
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