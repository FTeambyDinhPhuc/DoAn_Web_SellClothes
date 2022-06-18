using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAn_Web_SellClothes.Models;
namespace DoAn_Web_SellClothes.Models
{
    public class Giohang
    {
        DataClasses1DataContext data = new DataClasses1DataContext();
        public int? iIdProduct { get; set; }
        public int? iSize { get; set; }
        public string iImageProduct { get; set; }
        public string iNameProduct { get; set; }
        public int iPriceProduct { get; set; }
        public int? iQuantityProduct { get; set; }

        public string iSizeProduct  { get; set; }
        public int iThanhTien
        {
            get { return (int)(iQuantityProduct * iPriceProduct); }
        }
        //Khởi tạo giỏ hành theo Mã sản phẩm truyền vào với số lượng mạc định là 1
        public Giohang(int? idProduct, int? sizeProduct)
        {
            iIdProduct = idProduct;
            iSize = sizeProduct;
            Product product = data.Products.Single(n => n.IdProduct == iIdProduct);
            iImageProduct = product.ImageProduct;
            iNameProduct = product.NameProduct;
            iPriceProduct = product.PriceProduct;
            var sizeproduct = data.SizeProducts.FirstOrDefault(p => p.IdSizeProduct == iSize);
            iSizeProduct = sizeproduct.NameSizeProduct;           
            iQuantityProduct = 1;
 
        }

    }
}