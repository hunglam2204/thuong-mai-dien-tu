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
    public class BrandController : Controller
    {
        DataContext db = new DataContext();
        // GET: Admin/Brands
        public ActionResult Index()
        {
            return View(db.brand.ToList());
        }
        public ActionResult Create()
        {
            ViewBag.CompanyID = new SelectList(db.company, "CompanyID", "CompanyName");
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "BrandID, BrandName, CompanyID")] Brand brands)
        {
            db.brand.Add(brands);
            db.SaveChanges();
            ViewBag.CompanyID = new SelectList(db.company, "CompanyID", "CompanyName", brands.CompanyID);
            return RedirectToAction("Index", "Brand");
        }
        public ActionResult Edit(int id)
        {
            ViewBag.CompanyID = new SelectList(db.company, "CompanyID", "CompanyName");
            var brand = db.brand.Find(id);
            return View(brand);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "BrandID, BrandName, CompanyID")] Brand brands)
        {
            db.Entry(brands).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            ViewBag.CompanyID = new SelectList(db.company, "CompanyID", "CompanyName", brands.CompanyID);
            return RedirectToAction("Index", "Brand");
        }
        public ActionResult Delete(int id)
        {
            Brand brands = db.brand.FirstOrDefault(x => x.BrandID == id);
            return View(brands);
        }
        [Route("Delete")]
        public ActionResult ConfirmDelete([Bind(Include = "BrandID, BrandName, CompanyID")] Brand brands, int id)
        {
            brands = db.brand.FirstOrDefault(x => x.BrandID == id);
            db.brand.Remove(brands);
            db.SaveChanges();
            return RedirectToAction("Index", "Brand");
        }
    }
}