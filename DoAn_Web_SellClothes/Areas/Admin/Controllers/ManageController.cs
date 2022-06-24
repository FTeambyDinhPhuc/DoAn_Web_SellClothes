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
        //========================================================================================
        //private int Receiptcount()
        //{
        //    var count = db.Invoices.OrderByDescending(s => s.IdInvoice).Count();
        //    masage
        //    return count;
        //}

        public ActionResult Receipt()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("LogIn", "Account");
            }
            var list = db.Invoices.OrderByDescending(s => s.IdInvoice).ToList();
            foreach (var item in list)
            {
                if (item.StatusInvoice == false)
                {
                    ViewBag.StatusInvoice = "Chưa giao hàng";
                }
                else if (item.StatusInvoice == true)
                {
                    ViewBag.StatusInvoice = "Đã giao hàng";
                }
                if (item.Paid == false)
                {
                    ViewBag.Paid = "Chưa thanh toán";
                }
                else if (item.Paid == true)
                {
                    ViewBag.Paid = "Đã thanh toán";
                }
            }
            return View(list);
        }
        public ActionResult DetailReceipt(int id)
        {
            //InvoiceDetail ct = db.InvoiceDetails.Where(n => n.IdInvoice == id).;
            ViewBag.ma = db.Invoices.SingleOrDefault(n => n.IdInvoice == id);
            var ct = (from s in db.InvoiceDetails where s.IdInvoice == id select s).ToList();
            return View(ct);
        }
        public ActionResult xacnhan(int id)
        {
            //InvoiceDetail ct = db.InvoiceDetails.Where(n => n.IdInvoice == id).;

            Invoice xn = db.Invoices.SingleOrDefault(n => n.IdInvoice == id);
            
            return View(xn);
        }
        //========================================================================================
        private List<Account> Customer(int count)
        {
            return db.Accounts.OrderByDescending(s => s.IdAccount).Take(count).ToList();
        }

        public ActionResult Customer()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("LogIn", "Account");
            }                   
            var list = db.Accounts.OrderByDescending(s => s.IdAccount ).ToList();
            return View(list);
        }
        //========================================================================================
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
        //========================================================================================
        public ActionResult TypesClothes()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("LogIn", "Account");
            }
            var list = db.ProductTypes.OrderByDescending(s => s.IdProductType ).ToList();
            return View(list);
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
        //============================================================================================
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
            var list = db.Products.OrderByDescending(s => s.IdProduct).ToList();
            return View(list);
        }

        // hiển thị màn hình thêm sản phẩm
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

        //action thêm sản phẩm
        [HttpPost]
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
            var date = DateTime.UtcNow.Date;
            var mota = collection["describe"];            
            var loai = collection["Loai"];
            var size = collection["Size"];
            var sl = collection["quality"];
            var status = collection["status"];

            var filename = Path.GetFileName(fileUpload.FileName); 
            var path = Path.Combine(Server.MapPath("~/Assets/img/Clothes"), filename);
            //if (System.IO.File.Exists(path))
            //{
            //    ViewBag.ThongBaoAnh = "Hình Ảnh Đã Tồn Tại";
            //    return this.AddProduct();
            //}
            
            fileUpload.SaveAs(path);
            pr.NameProduct = ten;
            pr.ImageProduct = filename;
            pr.PriceProduct = int.Parse(gia);
            pr.DescribeProduct = mota;
            pr.CreateDate = date;
            pr.IdProductType = Int32.Parse(loai);
            //pr.StatusProduct = int.Parse(status);
            //pr.QuantityProduct = int.Parse(sl);
            db.Products.InsertOnSubmit(pr);
            db.SubmitChanges();
            
;

            dt.IdSizeProduct = Int32.Parse(size);
            dt.IdProduct = pr.IdProduct;
            dt.SoLuongTon = int.Parse(sl);
            db.ProductDetails.InsertOnSubmit(dt);
            db.SubmitChanges();
            return RedirectToAction("Product", "Manage");
        }

        //hiển thị màn hình chỉnh sửa sản phẩm
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
        //action sửa sản phẩm
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
            }else if(sp.StatusProduct !=1 || sp.StatusProduct != 0)
            {
                ViewData["1"] = "Bạn đã nhập sai !";
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
        //========================================================================================
        public ActionResult DetailProduct( int id)
        {
            //var ct = db.ProductDetails.SingleOrDefault(n => n.IdProduct == id);
            var ct = from c in db.ProductDetails where c.IdProduct == id select c;
            int idp = (from i in db.ProductDetails where i.IdProduct == id select i.IdProduct).FirstOrDefault();
            Session["idp"]=id;
            return View(ct);
        }
        [HttpGet]
        public ActionResult AddProductDetailSize()
        {
            ViewBag.Size = new SelectList(db.SizeProducts.ToList().OrderBy(n => n.NameSizeProduct), "IdSizeProduct", "NameSizeProduct");
            ViewBag.IdProduct = Session["idp"];
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddProductDetailSize( ProductDetail pr, FormCollection collection, string url)
        {
            ViewBag.Size = new SelectList(db.SizeProducts.ToList().OrderBy(n => n.NameSizeProduct), "IdSizeProduct", "NameSizeProduct");
            var sl = collection["Sl"];
            var size = collection["Size"];
            int idpd = (int)Session["idp"];
            int idsize = Int32.Parse(size);
            var idsizeProduct = (from s in db.ProductDetails where s.IdProduct == idpd select s).ToList();
            foreach(var item in idsizeProduct)
            {
                if (idsize == item.IdSizeProduct)
                {
                    pr.IdProduct = idpd;
                    pr.IdSizeProduct = idsize;
                    item.SoLuongTon += int.Parse(sl);
                    pr.SoLuongTon = item.SoLuongTon;
                    db.SubmitChanges();
                    return RedirectToAction("DetailProduct", new {id = idpd});
                }
            }
            pr.IdProduct = idpd;
            pr.IdSizeProduct = idsize;
            pr.SoLuongTon = int.Parse(sl);
            db.ProductDetails.InsertOnSubmit(pr);
            db.SubmitChanges();
            return RedirectToAction("DetailProduct", new { id = idpd});
        }

        public ActionResult DeleteProductDetail(int id)
        {
            int size = int.Parse(Request.QueryString["size"]);

            ProductDetail sp = db.ProductDetails.Where(n => n.IdProduct == id && n.IdSizeProduct == size).SingleOrDefault();

            db.ProductDetails.DeleteOnSubmit(sp);
            db.SubmitChanges();
            return RedirectToAction("Product");
        }

        public ActionResult DetailProducts(int id)
        {
            Product ct = db.Products.SingleOrDefault(n => n.IdProduct == id);
            return View(ct);
        }
    }
}