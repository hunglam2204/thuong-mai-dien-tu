using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMDT_Web.Data;
using TMDT_Web.Models.Domain;

namespace TMDT_Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        DataContext db = new DataContext();
        // GET: Admin/Users
        public ActionResult Index(string error)
        {
            ViewBag.error = error;
            var users = db.account.ToList();
            return View(users);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.roleID = new SelectList(db.role, "RoleID", "setRole");
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "UserID, FirstName, LastName, Address, Age, Email, Password, RoleID")] Account account)
        {
            string str = account.Email;
            char[] sperator = { '@', ' ' };

            string[] strlist = str.Split(sperator);
            foreach (string s in strlist)
            {
                account.UserName = s;
                break;
            }
            db.account.Add(account);
            db.SaveChanges();
            ViewBag.roleID = new SelectList(db.role, "RoleID", "setRole", account.RoleID);
            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            ViewBag.roleID = new SelectList(db.role, "RoleID", "setRole");
            var account = db.account.FirstOrDefault(x => x.UserID == id);
            return View(account);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "UserID, FirstName, LastName, Address, Age, Email, Password, RoleID")] Account account, string userName)
        {
            account.UserName = userName;
            db.Entry(account).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            ViewBag.roleID = new SelectList(db.role, "RoleID", "setRole", account.RoleID);
            return RedirectToAction("Index", "User");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Account account = db.account.FirstOrDefault(x => x.UserID == id);
            return View(account);
        }
        [Route("Delete")]
        public ActionResult ConfirmDelete([Bind(Include = "UserID, FirstName, LastName, Address, Age, Email, Password, RoleID")] Account account, int id)
        {
            account = db.account.FirstOrDefault(x => x.UserID == id);
            db.account.Remove(account);
            db.SaveChanges();
            return RedirectToAction("index", "user");
        }
        public ActionResult UserOrder(int? id)
        {
            var orders = db.order.ToList();
            foreach (var item in orders)
            {
                if (item.UserID == id)
                {
                    var order = db.order.Where(x => x.UserID == id).ToList();
                    return View(order);
                }
                else
                {
                    return RedirectToAction("index", "user", new { error="User dont have order!"});
                }
            }
            return View();
        }
    }
}