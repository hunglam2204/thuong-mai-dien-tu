using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMDT_Web.Data;

namespace TMDT_Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ChatController : Controller
    {
        DataContext db = new DataContext();
        // GET: Admin/Chat
        public ActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                var user = db.account.FirstOrDefault(x => x.UserName == User.Identity.Name);
                var users = db.account.ToList();
                foreach(var items in users)
                {
                    if(items.Status == 1)
                    {
                        ViewBag.online = db.account.Where(x=>x.Status==items.Status).Count();
                    }
                    else
                    {
                        ViewBag.online = 0;
                    }
                }
                return View(user);
            }
            return View();
        }
        public ActionResult ChatRoom()
        {
            var user = db.account.FirstOrDefault(x => x.UserName == User.Identity.Name);
            var users= db.account.ToList();
            ViewBag.users = users;
            return View(user);
        }
    }
}