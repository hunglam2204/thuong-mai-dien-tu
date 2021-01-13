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
    public class CompanyController : Controller
    {
        //Database
        DataContext db = new DataContext();

        //----------------Danh sách cty----------------
        public ActionResult Index()
        {
            return View(db.company.ToList());
        }

        //----------------Thêm cty----------------
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "CompanyID, CompanyName")] Company companies)
        {
            try
            {

                db.company.Add(companies);
                db.SaveChanges();
            }
            catch (Exception)
            {
            }
            return RedirectToAction("index", "Company");
        }

        //----------------Cập nhật cty----------------
        public ActionResult Edit(int? id)
        {
            Company companies = db.company.Find(id);
            return View(companies);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "CompanyID, CompanyName")] Company companies)
        {
            db.Entry(companies).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Company");
        }
        //----------------Xóa cty----------------
        public ActionResult Delete(int id)
        {
            Company companies = db.company.FirstOrDefault(x => x.CompanyID == id);
            return View(companies);
        }
        [Route("Delete")]
        public ActionResult ConfirmDelete([Bind(Include = "CompanyID, CompanyName")] Company companies, int id)
        {
            companies = db.company.FirstOrDefault(x => x.CompanyID == id);
            db.company.Remove(companies);
            db.SaveChanges();
            return RedirectToAction("Index", "Company");
        }
    }
}