using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMDT_Web.Data;

namespace TMDT_Web.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminHomeController : Controller
    {
        DataContext db = new DataContext();
        // GET: Admin/AdminHome
        public ActionResult Index()
        {
            ViewBag.userCount = db.account.Count();
            ViewBag.companyCount = db.company.Count();
            ViewBag.brandCount = db.brand.Count();
            ViewBag.productCount = db.product.Count();
            ViewBag.productQuantityCount = db.product.Sum(x => x.Quantity);
            ViewBag.orderCount = db.order.Count();
            ViewBag.waiting = db.order.Where(x => x.Status == null).Count();
            ViewBag.accept = db.order.Where(x => x.Status == 1).Count();
            ViewBag.cancel = db.order.Where(x => x.Status == 2).Count();
            return View();
        }
        public ActionResult Chart()
        {
            ViewBag.userCount = db.account.Count();
            ViewBag.companyCount = db.company.Count();
            ViewBag.brandCount = db.brand.Count();
            ViewBag.productCount = db.product.Sum(x => x.Quantity);
            ViewBag.orderCount = db.order.Count();
            ViewBag.accept = db.order.Where(x => x.Status == 1).Count();
            ViewBag.cancel = db.order.Where(x => x.Status == 2).Count();
            return View();
        }
    }
}