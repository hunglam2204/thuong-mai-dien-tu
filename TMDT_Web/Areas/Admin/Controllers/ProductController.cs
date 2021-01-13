using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMDT_Web.Data;
using TMDT_Web.Models.Domain;

namespace TMDT_Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        //Database
        DataContext db = new DataContext();

        //Hiển thị danh sách sản phẩm
        public ActionResult Index(int? page, string error)
        {
            ViewBag.error = error;
            //Page list
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            return View(db.product.ToList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Search(string opt, string search, int? page)
        {
            //Page list
            int pageSize = 15;
            int pageNumber = (page ?? 1);

            if (opt == "ProductName")
            {
                var proList = db.product.ToList();
                foreach (var check in proList)
                {
                    if (check.ProductName == search)
                    {
                        var proName = db.product.Where(x => x.ProductName == search).ToList();
                        ViewBag.opt = opt;
                        ViewBag.search = search;
                        return View(proName.ToPagedList(pageNumber, pageSize));
                    }
                    else
                    {
                        return RedirectToAction("Index", "Product", new { error = "Can not find product name!" });
                    }
                }
            }
            else if (opt == "Company")
            {
                var proList = db.product.ToList();
                foreach (var check in proList)
                {
                    if (check.Brand.Company.CompanyName == search)
                    {
                        var comName = db.product.Where(x => x.Brand.Company.CompanyName == search).ToList();
                        ViewBag.opt = opt;
                        ViewBag.search = search;
                        return View(comName.ToPagedList(pageNumber, pageSize));
                    }
                    else
                    {
                        return RedirectToAction("Index", "Product", new { error = "Can not find company name!" });
                    }
                }
            }
            else
            {
                var proList = db.product.ToList();
                foreach (var check in proList)
                {
                    if (check.Brand.BrandName == search)
                    {
                        var brandName = db.product.Where(x => x.Brand.BrandName == search).ToList();
                        ViewBag.opt = opt;
                        ViewBag.search = search;
                        return View(brandName.ToPagedList(pageNumber, pageSize));
                    }
                    else
                    {
                        return RedirectToAction("Index", "Product", new { error = "Can not find brand!" });
                    }
                }
            }
            return View();
        }
        //----------------Thêm cty----------------
        public ActionResult Create()
        {
            ViewData["companies"] = db.company.ToList();
            ViewData["brands"] = db.brand.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "ProductID, ProductName, Price, Color, size, Date, Description, Quantity, Image, BrandID")] Product products, HttpPostedFileBase Image)
        {
            if (Image.ContentLength == 0)
            {
                products.Image = "abc.jpg";
            }
            else
            {
                string _fileName = Path.GetFileName(Image.FileName);
                products.Image = _fileName;
                string _path = Path.Combine(Server.MapPath("~/Images/Products/"), _fileName);
                Image.SaveAs(_path);
            }

            db.product.Add(products);
            db.SaveChanges();
            return RedirectToAction("index", "Product");
        }

        //----------------Cập nhật cty----------------
        public ActionResult Edit(int? id)
        {
            ViewData["companies"] = db.company.ToList();
            ViewData["brands"] = db.brand.ToList();
            Product products = db.product.FirstOrDefault(x => x.ProductID == id);
            return View(products);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "ProductID, ProductName, Price, Color, size, Date, Description, Quantity, Image, BrandID")] Product products, HttpPostedFileBase Image, string Image1, DateTime Date1)
        {
            if (!ModelState.IsValid)
            {
                products.Date = Date1;
            }

            if (Image == null)
            {
                products.Image = Image1;
            }
            else
            {
                string _fileName = Path.GetFileName(Image.FileName);
                products.Image = _fileName;
                string _path = Path.Combine(Server.MapPath("~/Images/Products/"), _fileName);
                Image.SaveAs(_path);
            }

            db.Entry(products).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Product");
        }
        //----------------Xóa cty----------------
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Product products = db.product.FirstOrDefault(x => x.ProductID == id);
            return View(products);
        }
        [Route("Delete")]
        public ActionResult ConfirmDelete([Bind(Include = "ProductID, ProductName, Price, Color, size, Date, Description, Quantity, Image")] Product products, int id)
        {
            products = db.product.FirstOrDefault(x => x.ProductID == id);
            db.product.Remove(products);
            db.SaveChanges();
            return RedirectToAction("Index", "Product");
        }
    }
}