using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_Web_SellClothes.Models;
namespace DoAn_Web_SellClothes.Controllers
{
    public class ProductController : Controller
    {
        DataClasses1DataContext data = new DataClasses1DataContext();
        // GET: Product
        public ActionResult ProductPage()
        {
            return View();
        }
        
        //public ActionResult ProductDetails()
        //{
           // return View();
        //}
        private List<Product> LaySanPhamMoi(int count)
        {
            return data.Products.OrderByDescending(s => s.UpdateDate).Take(count).ToList();
        }
        public ActionResult SanPhamMoi()
        {
            var sanphammoi = LaySanPhamMoi(4);
            return PartialView(sanphammoi);
        }
        public ActionResult SanPhamNam()
        {
            var sanphamnam = from spn in data.ProductTypes where spn.IdSex == 1 select spn;
            return PartialView(sanphamnam);
        }
        public ActionResult SanPhamNu()
        {
            var sanphamnu = from spnu in data.ProductTypes where spnu.IdSex == 0 select spnu;
            return PartialView(sanphamnu);
        }
        public ActionResult AllSanPham()
        {
            var allsanpham = from allsp in data.Products select allsp;
            return PartialView(allsanpham);
        }
        public ActionResult SPTheoLoaiNam(int id)
        {
            var sanphamloainam = from spln in data.Products where spln.IdProductType == id select spln;
            return View(sanphamloainam);
        }
        public ActionResult SPTheoLoaiNu(int id)
        {
            var sanphamloainam = from spln in data.Products where spln.IdProductType == id select spln;
            return View(sanphamloainam);
        }
        public ActionResult ProductDetails(int id)
        {
            var sanpham = from sp in data.Products where sp.IdProduct == id select sp;
            return View(sanpham.Single());
        }
    }
}