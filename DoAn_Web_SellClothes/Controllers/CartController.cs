using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_Web_SellClothes.Models;
namespace DoAn_Web_SellClothes.Controllers
{
    public class CartController : Controller
    {
        DataClasses1DataContext data = new DataClasses1DataContext();
        // GET: Cart
        public List<Giohang> LayGioHang()
        {
            List<Giohang> listgiohang = Session["Giohang"] as List<Giohang>;
            if (listgiohang == null)
            {
                //Nếu giỏ hàng chưa tồn tại thì khởi tạo listGioHang
                listgiohang = new List<Giohang>();
                Session["Giohang"] = listgiohang;
            }
            return listgiohang;
        }
        //Tổng số lượng
        private int TongSoLuong()
        {
            int tong = 0;
            List<Giohang> listgiohang = Session["Giohang"] as List<Giohang>;
            if (listgiohang != null)
            {
                tong = (int)listgiohang.Sum(n => n.iQuantityProduct);
            }
            return tong;
        }
        //Tính tổng tiền
        private int TongTien()
        {
            int tongtien = 0;
            List<Giohang> listgiohang = Session["Giohang"] as List<Giohang>;
            if (listgiohang != null)
            {
                tongtien = listgiohang.Sum(n => n.iThanhTien);
            }
            return tongtien;
        }
        //Cập nhật số lượng tồn của mỗi sản phẩm
        private void updateSoLuong(InvoiceDetail cthd)
        {
            var sp = data.ProductDetails.Single(p => p.IdProduct == cthd.IdProduct && p.IdSizeProduct == cthd.IdSizeProduct);
            sp.SoLuongTon = sp.SoLuongTon - cthd.Quantity;
            data.SubmitChanges();
        }
        //Thêm sản phẩm vào giỏ hàng
        [HttpPost]
        public ActionResult ThemGioHang(int? idProduct, string strURL) 
        {
            int? sizeid = null;
            //Lấy ra session    
            List<Giohang> listgiohang = LayGioHang();
            Session["Size"] = Request.Form["nameSize"];
            if (Request.Form["nameSize"] == null)
            {
                Session["Error"]= "Vui lòng chọn Size sản phẩm!";
                return Redirect(strURL);
            }
            else sizeid = Int32.Parse(Request.Form["nameSize"].ToString());
            int sl = Int32.Parse(Request.Form["quantity"].ToString());
            int soLuongTon = data.ProductDetails.SingleOrDefault(p => p.IdProduct == idProduct && p.IdSizeProduct == sizeid).SoLuongTon.Value;
            if(sl>soLuongTon)
            {
                Session["sl"] = 1;
                Session["Error1"] = "Số lượng mua lớn hơn số lượng tồn! Vui lòng chọn lại!";
                return Redirect(strURL);
            }
            else
            {
                Session["sl"] = null;
            }
            //Kiểm tra sản phẩm này tồn tại trong Session["Giohang"] chưa?
            Giohang giohang = listgiohang.Find(n => n.iIdProduct == idProduct && n.iSize==sizeid);
            if(giohang==null)
            {

                giohang = new Giohang(idProduct, sizeid, sl);
                listgiohang.Add(giohang);
                return Redirect(strURL);
            }
            else
            {
                giohang.iQuantityProduct++;
                return Redirect(strURL);
            }
        }
        public ActionResult Cart()
        {
            List<Giohang> listgiohang = LayGioHang();
            if(listgiohang.Count==0)
            {
                return RedirectToAction("ProductPage", "Product");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            ViewBag.TongTienShip = TongTien() + 25000;
            return View(listgiohang);
        }
        //Xóa 1 món hàng ra khỏi giỏ hàng
        public ActionResult RemoveItemInCart(int iProductId, int iSizeId)
        {
            List<Giohang> listProductInCart = LayGioHang();
            Giohang sp = listProductInCart.SingleOrDefault(n => n.iIdProduct == iProductId && n.iSize == iSizeId);
            if(sp != null)
            {
                listProductInCart.Remove(sp);
                return RedirectToAction("Cart");
            }
            if(listProductInCart.Count==0)
            {
                return RedirectToAction("ProductPage", "Product");
            }
            return RedirectToAction("Cart");
        }
        //Cập nhật lại số lượng trong giỏ hàng
        public ActionResult UpdateItemInCart(int iProductId, int iSizeId, FormCollection collection)
        {
            List<Giohang> listProductInCart = LayGioHang();
            Giohang sp = listProductInCart.SingleOrDefault(n => n.iIdProduct == iProductId && n.iSize == iSizeId);
            if (sp != null)
            {
                sp.iQuantityProduct = int.Parse(collection["quantity1"]);
            }
            return RedirectToAction("Cart");
        }
        //Xóa toàn bộ giỏ hàng
        public ActionResult RemoveCart()
        {
            List<Giohang> listProductInCart = LayGioHang();
            listProductInCart.Clear();
            return RedirectToAction("ProductPage", "Product");
        }
        [HttpGet]
        public ActionResult Checkout()
        {
            Account ac = (Account)Session["user"];
            Session["name"] = ac.FullName;
            Session["phone"] = ac.PhoneNumber;
            Session["address"] = ac.AddressUser;
            List<Giohang> listgiohang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            ViewBag.TongTienShip = TongTien() + 25000;
            return View(listgiohang);
        }
        [HttpPost]
        public ActionResult Checkout(string strURL, FormCollection collection)
        {
            Account ac = (Account)Session["user"];
            Invoice ddh = new Invoice();
            List<Giohang> gh = LayGioHang();
            //List<InfoCustomerBill> info = null;
            ddh.IdAccount = ac.IdAccount;
            ddh.InvoiceNameReceiver = collection["billing_name"];
            ddh.InvoicePhoneReceiver = collection["billing_phone"];
            ddh.InvoiceAddressReceiver = collection["billing_address"];
            ddh.NoteInvoice = collection["billing_note"];
            data.Invoices.InsertOnSubmit(ddh);
            ddh.InvoiceDate = DateTime.Now;
            ddh.TotalInvoice = TongTien() + 25000;
            ddh.PaymentsInvoice = collection["Payment"];
            ddh.StatusInvoice = false;
            ddh.Paid = false;
            data.Invoices.InsertOnSubmit(ddh);
            data.SubmitChanges();

            foreach (var item in gh)
            {
                InvoiceDetail ctdh = new InvoiceDetail();
                ctdh.IdSizeProduct = (int)item.iSize;
                ctdh.IdProduct = (int)item.iIdProduct;
                ctdh.IdInvoice = ddh.IdInvoice;
                ctdh.Quantity = item.iQuantityProduct;
                ctdh.UnitPrice = item.iPriceProduct;
                updateSoLuong(ctdh);
                data.InvoiceDetails.InsertOnSubmit(ctdh);
            }
            data.SubmitChanges();
            Session["Giohang"] = null;
            return RedirectToAction("Thanks", "Home");
        }
        //public ActionResult Pay(string strURL)
        //{
        //    Account ac = (Account)Session["user"];
        //    Invoice ddh = new Invoice();
        //    List<Giohang> gh = LayGioHang();
        //    //List<InfoCustomerBill> info = null;
        //    ddh.IdAccount = ac.IdAccount;
        //    ddh.InvoiceNameReceiver = ac.FullName;
        //    ddh.InvoicePhoneReceiver = ac.PhoneNumber;
        //    ddh.InvoiceAddressReceiver = ac.AddressUser;
        //    ddh.NoteInvoice = Request.QueryString["billing_note"];
        //    data.Invoices.InsertOnSubmit(ddh);
        //    ddh.InvoiceDate = DateTime.Now;
        //    ddh.TotalInvoice = TongTien() + 25000;
        //    ddh.PaymentsInvoice = "Đã thanh toán";
        //    ddh.StatusInvoice = false;
        //    ddh.Paid = false;
        //    data.Invoices.InsertOnSubmit(ddh);
        //    data.SubmitChanges();
        //    foreach (var item in gh)
        //    {
        //        InvoiceDetail ctdh = new InvoiceDetail();
        //        ctdh.IdSizeProduct = (int)item.iSize;
        //        ctdh.IdProduct = (int)item.iIdProduct;
        //        ctdh.IdInvoice = ddh.IdInvoice;
        //        ctdh.Quantity = item.iQuantityProduct;
        //        ctdh.UnitPrice = item.iPriceProduct;
        //        updateSoLuong(ctdh);
        //        data.InvoiceDetails.InsertOnSubmit(ctdh);
        //    }
        //    data.SubmitChanges();
        //    Session["Giohang"] = null;
        //    return RedirectToAction("Thanks", "Home");
        //}
    }
}