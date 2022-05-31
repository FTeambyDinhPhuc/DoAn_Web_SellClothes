using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DoAn_Web_SellClothes.Models;

namespace DoAn_Web_SellClothes.Controllers
{
    public class AccountController : Controller
    {
        DataClasses1DataContext data = new DataClasses1DataContext();
        // GET: Account
        private static readonly int CHECK_EMAIL = 1;
        public ActionResult AccountInformation()
        {
            return View();
        }
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
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection collection, Account ac)
        {
            var hoten = collection["fullname"];
            var email = collection["email"];
            var matkhau = collection["password"];
            var matkhaunhaplai = collection["confirmpassword"];
            if (String.IsNullOrEmpty(hoten) || String.IsNullOrEmpty(email) || String.IsNullOrEmpty(matkhau) || String.IsNullOrEmpty(matkhaunhaplai))
            {
                ViewData["Error"] = "Vui lòng điền đầy đủ nội dung";
                return this.Register();
            }
            else if(checkUser(email,CHECK_EMAIL))
            {
                ViewData["Error"] = "Tài khoản đã tồn tại";
                return this.Register();
            }
            else if(!String.Equals(matkhau.ToString(),matkhaunhaplai.ToString()))
            {
                ViewData["Error"] = "Mật khẩu không khớp";
                return this.Register();
            }
            else
            {
                ac.FullName = hoten;
                ac.Email = email;
                ac.PasswordUser =MD5Hash(matkhau);
                data.Accounts.InsertOnSubmit(ac);
                data.SubmitChanges();
                return RedirectToAction("LogIn");
            }
        }

        private bool checkUser(string str, int value)
        {
            if(value==1)
            {
                var a = data.Accounts.FirstOrDefault(p => p.Email == str);
                if (a != null) return true;
            }
            return false;
        }
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(FormCollection collection)
        {
            var tendangnhap = collection["username"];
            var matkhau = collection["password"];
            var user = data.Accounts.SingleOrDefault(p => p.Email == tendangnhap);
            if(String.IsNullOrEmpty(tendangnhap)||String.IsNullOrEmpty(matkhau))
            {
                ViewData["Error"] = "Vui lòng điền đầy đủ nội dung";
                return this.LogIn();
            }else if(user==null)
            {
                ViewData["Error"] = "Sai tài khoản";
                return this.LogIn();
            }
            else if (!String.Equals(MD5Hash(matkhau), user.PasswordUser))
            {
                ViewData["Error"] = "Sai mật khẩu";
                return this.LogIn();
            }
            else
            {
                Session["user"] = user;
                Session["name"] = user.FullName;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult _User()
        {
            return PartialView();
        }
        public ActionResult LogOut()
        {
            Session["user"] = null;
            Session["name"] = null;
            return RedirectToAction("Index", "Home");
        }

    }
}