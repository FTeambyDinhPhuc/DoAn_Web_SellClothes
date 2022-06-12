using DoAn_Web_SellClothes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DoAn_Web_SellClothes.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
     
        DataClasses1DataContext db = new DataClasses1DataContext();
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
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }
        // GET: Admin/Account
        [HttpPost]
        public ActionResult LogIn(FormCollection collection)
        {
            var name = collection["username"];
            var pass = collection["password"];
            
            var ad = db.AdminAccounts.SingleOrDefault(n => n.UserNameAdmin == name);
            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(pass))
            {                               
            }
            else if (!String.Equals(name, ad.UserNameAdmin))
            {
                ViewData["1"] = "Sai tài khoản";            
            }
            else if (!String.Equals(MD5Hash(pass), ad.PasswordAdmin))
            {
                ViewData["2"] = "Sai Mật khẩu !";             
            }
            else
            {
                Session["admin"] = ad;
                return RedirectToAction("Statistical", "Statistical");
            }
            return View();
            
        }
        public ActionResult LogOut()
        {
            Session["admin"] = null;
            return RedirectToAction("LogIn", "Account");
        }
        public ActionResult ChangePassword(FormCollection collection)
        {
            var po = collection["passold"];
            var pn = collection["passnew"];
            var pa = collection["passagain"];
            var ad = db.AdminAccounts.SingleOrDefault(n => n.PasswordAdmin == po);
            if (String.IsNullOrEmpty(po) || String.IsNullOrEmpty(pn) || String.IsNullOrEmpty(pa))
            {
            }                  
            //else if (!String.Equals(MD5Hash(po), ad.PasswordAdmin))
            //{
            //    ViewData["1"] = "Sai Mật khẩu !";
            //}
            else if (!String.Equals(MD5Hash(pn), MD5Hash(pa)))
            {
                ViewData["3"] = "Xác nhận mật khẩu chưa đúng !";
            }
            else if(String.Equals(MD5Hash(po), MD5Hash(pn)))
            {
                ViewData["2"] = "Không được đặt lại mật khẩu củ!";
            }
            else
            {                 
                ad.PasswordAdmin = MD5Hash(po);
                db.SubmitChanges();
                return RedirectToAction("Statistical", "Statistical");
            }
            return View();
        }
    }
}