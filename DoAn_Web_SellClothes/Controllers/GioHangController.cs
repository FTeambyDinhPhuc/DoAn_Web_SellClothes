using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_Web_SellClothes.Models;
namespace DoAn_Web_SellClothes.Controllers
{
    public class GioHangController : Controller
    {
        DataClasses1DataContext data = new DataClasses1DataContext();
        // GET: GioHang
        public List<Giohang> LayGioHang()
        {
            List<Giohang> listgiohang = Session["Giohang"] as List<Giohang>;
            if(listgiohang==null)
            {
                //Nếu giỏ hàng chưa tồn tại thì khởi tạo listGioHang
                listgiohang = new List<Giohang>();
                Session["Giohang"] = listgiohang;
            }
            return listgiohang;
        }
        //Thêm vào giỏ hàng
        public ActionResult ThemGioHang(int iMasach, string strURL)
        {
            //Lấy ra sessionm  
            List<Giohang> listgiohang = LayGioHang();
            //K
        }
    
    }
}