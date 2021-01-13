using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TMDT_Web.Data;
using TMDT_Web.Models;
using TMDT_Web.Models.Domain;

namespace TMDT_Web.Controllers
{
    public class AuthenticationController : Controller
    {
        DataContext db = new DataContext();
        // GET: Authentication
        //Giao diện trang người dùng
        public ActionResult Index(string error, string errorEmail)
        {
            ViewBag.error = error;
            ViewBag.errorEmail = errorEmail;
            return View(db.account.FirstOrDefault(x => x.UserName == User.Identity.Name));
        }

        //Chức năng và giao diện đăng nhập
        public ActionResult Login(string error)
        {
            ViewBag.error = error;
            //Đếm sản phẩm trong giỏ hàng
            List<Item> cart = (List<Item>)Session["cart"];
            if (Session["cart"] == null)
            {
                ViewData["countCartProducts"] = 0;
            }
            else
            {
                ViewData["countCartProducts"] = cart.Count;
            }
            return View();
        }
        [HttpPost]
        public ActionResult DoLogin([Bind(Include = "Email, Password")] Account user)
        {
            var accounts = db.account.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);
            if (accounts == null)
            {
                return RedirectToAction("Login", "Authentication", new { error = "Email or Password invalid!" });
            }
            else if (user.Email == accounts.Email && user.Password == accounts.Password)
            {
                user.UserName = accounts.UserName;
                FormsAuthentication.SetAuthCookie(user.UserName, false);
                if(accounts.RoleID == 1)
                {
                    return RedirectToAction("index", "AdminHome",new { area = "Admin" });
                }
                else
                {
                    return RedirectToAction("index", "Home");
                }
            }
            return View();
        }

        //Chức năng và giao diện đăng nhập
        public ActionResult Register(string errorEmail, string errorUser)
        {
            ViewBag.errorEmail = errorEmail;
            ViewBag.errorUser = errorUser;
            //Đếm sản phẩm trong giỏ hàng
            List<Item> cart = (List<Item>)Session["cart"];
            if (Session["cart"] == null)
            {
                ViewData["countCartProducts"] = 0;
            }
            else
            {
                ViewData["countCartProducts"] = cart.Count;
            }
            return View();
        }
        [HttpPost]
        public ActionResult DoRegister([Bind(Include = "FirstName, LastName, UserName, PhoneNumber, Address, Age, Email, Password")] Account register)
        {
            var check = db.account.Count();
            var checkEmail = db.account.FirstOrDefault(x => x.Email == register.Email || x.UserName == register.UserName);
            if (checkEmail == null)
            {
                Account account = new Account();
                account.FirstName = register.FirstName;
                account.LastName = register.LastName;
                account.UserName = register.UserName;
                account.PhoneNumber = register.PhoneNumber;
                account.Address = register.Address;
                account.Age = register.Age;
                account.Email = register.Email;
                account.Password = register.Password;
                account.Avatar = "user.png";

                if (check == 0)
                {
                    account.RoleID = 1;
                }
                else
                {
                    account.RoleID = 2;
                }

                db.account.Add(account);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            else if (register.Email == checkEmail.Email && register.UserName == checkEmail.UserName)
            {
                return RedirectToAction("Register", "Authentication", new { errorEmail = "Email already exist!", errorUser = "User already exist!" });
            }
            else if (register.Email == checkEmail.Email)
            {
                return RedirectToAction("Register", "Authentication", new { errorEmail = "Email already exist!" });
            }
            else
            {
                return RedirectToAction("Register", "Authentication", new { errorUser = "User already exist!" });
            }
        }
        //Chức năng đăng xuất

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        //Chức năng đổi mật khẩu
        public ActionResult ChangePassword(int id, string cpw, string npw, string cnpw)
        {
            var acc = db.account.FirstOrDefault(x => x.UserID == id);
            if (cpw == acc.Password && npw == cnpw)
            {
                acc.Password = npw;
                db.Entry(acc).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Authentication");
            }
            else
            {
                return RedirectToAction("Index", "Authentication",new { error="You have to type your password"});
            }
        }
        //Chức năng ảnh giao diện
        public ActionResult ChangeAvatar(HttpPostedFileBase Image)
        {
            var account = db.account.FirstOrDefault(x => x.UserName == User.Identity.Name);
            if (Image.ContentLength == 0)
            {
                account.Avatar = "user.png";
            }
            string _fileName = Path.GetFileName(Image.FileName);
            account.Avatar = _fileName;
            string _path = Path.Combine(Server.MapPath("~/Images/Avatar/"), _fileName);
            Image.SaveAs(_path);
            db.Entry(account).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Authentication");
        }
        //Chức năng đổi tên
        public ActionResult ChangeFullName(int id,string FirstName, string LastName)
        {
            var acc = db.account.FirstOrDefault(x => x.UserID == id);
            acc.FirstName = FirstName;
            acc.LastName = LastName;
            db.Entry(acc).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Authentication");
        }
        //Chức năng đổi địa chỉ
        public ActionResult ChangeAddress(int id,string Address)
        {
            var acc = db.account.FirstOrDefault(x => x.UserID == id);
            acc.Address = Address;
            db.Entry(acc).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Authentication");
        }
        //Chức năng đổi tuổi
        public ActionResult ChangeAge(int id, int Age)
        {
            var acc = db.account.FirstOrDefault(x => x.UserID == id);
            acc.Age = Age;
            db.Entry(acc).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Authentication");
        }
        //Chức năng đổi mail
        public ActionResult ChangeEmail(int id, string Mail)
        {
            var acc = db.account.FirstOrDefault(x => x.UserID == id);

            var checkEmail = db.account.FirstOrDefault(x => x.Email == Mail);
            if (checkEmail == null)
            {
                acc.Email = Mail;
                db.Entry(acc).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Authentication");
            }
            else if (Mail == checkEmail.Email)
            {
                return RedirectToAction("Index", "Authentication", new { errorEmail = "Email already exist!" });
            }
            return View();
        }
    }
}