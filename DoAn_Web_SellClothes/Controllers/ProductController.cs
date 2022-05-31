using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_Web_SellClothes.Models;
using PagedList;
namespace DoAn_Web_SellClothes.Controllers
{
    public class ProductController : Controller
    {
        DataClasses1DataContext data = new DataClasses1DataContext();
        // GET: Product
        private List<Product> LaySanPhamMoi(int count)
        {
            return data.Products.OrderByDescending(s => s.IdProduct).Take(count).ToList();
        }

        public ActionResult ProductPage()
        {
            return View();
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
        public ActionResult AllSanPham(int? page)
        {
            // 1. Tham số int? dùng để thể hiện null và kiểu int
            // page có thể có giá trị là null và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo LinkID mới có thể phân trang.
            var allsanpham = (from allsp in data.Products select allsp).ToList();

            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 9;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các Link được phân trang theo kích thước và số trang.
            allsanpham = allsanpham.OrderByDescending(p => p.IdProduct).ToList();
            return PartialView(allsanpham.ToPagedList(pageNumber,pageSize));
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
        public ActionResult SizeProduct(int id)
        {
            var sizeproduct = from sp in data.Products join s in data.SizeProducts on sp.IdProduct equals s.IdProduct where sp.IdProduct == id select s.NameSizeProduct;
            return PartialView(sizeproduct);
        }
    }
}