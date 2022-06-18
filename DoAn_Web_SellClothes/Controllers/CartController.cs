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


        public ActionResult Checkout()
        {
            return View();
        }
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
            Session["QuantityCart"] = tong;
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
        //Cập nhật số lượng tồn của mỗi giày
        private void updateSoLuong(InvoiceDetail cthd)
        {
            var sp = data.ProductDetails.Single(p => p.IdProduct == cthd.IdProduct && p.IdSizeProduct == cthd.IdSizeProduct);
            sp.SoLuongTon = sp.SoLuongTon - cthd.Quantity;
            data.ProductDetails.InsertOnSubmit(sp);
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
            //Kiểm tra sản phẩm này tồn tại trong Session["Giohang"] chưa?
            Giohang giohang = listgiohang.Find(n => n.iIdProduct == idProduct && n.iSize==sizeid);
            if(giohang==null)
            {
              
                giohang = new Giohang(idProduct,sizeid);
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
    }
}