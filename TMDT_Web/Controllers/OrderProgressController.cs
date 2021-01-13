using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMDT_Web.Data;
using TMDT_Web.Models;

namespace TMDT_Web.Controllers
{
    public class OrderProgressController : Controller
    {
        DataContext db = new DataContext();
        // GET: OrderProgress
        public ActionResult Index()
        {
            return View();
        }
        [Route("SearchPhone")]
        [HttpGet]
        //Trang lịch sử mua hàng
        public ActionResult HistoryOrder(int? num)
        {
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
            //Side bar hãng laptop
            ViewBag.SideBar = db.company.ToList();

            if(User.Identity.IsAuthenticated)
            {
                var order = db.order.Where(x => x.Account.UserName == User.Identity.Name).ToList();
                ViewBag.order = order;
                foreach (var item in order)
                {
                    if (item.Status == null)
                    {
                        ViewBag.status = "Đang chờ duyệt";
                    }
                    else if (item.Status == 1)
                    {
                        ViewBag.status = "Đã nhận đơn";
                    }
                    else if (item.Status == 2)
                    {
                        ViewBag.status = "Đơn bị hủy";
                    }
                }
            }
            else
            {
                var order = db.order.Where(x => x.OrderPhoneNumber == num).ToList();
                ViewBag.order = order;
                foreach (var item in order)
                {
                    if (item.Status == null)
                    {
                        ViewBag.status = "Đang chờ duyệt";
                    }
                    else if (item.Status == 1)
                    {
                        ViewBag.status = "Đã nhận đơn";
                    }
                    else if (item.Status == 2)
                    {
                        ViewBag.status = "Đơn bị hủy";
                    }
                }
            }
            return View();
        }

        //Trang chi tiết lịch sử mua hàng
        public ActionResult OrderDetail(int id, int? num)
        {
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
            //Side bar hãng laptop
            ViewBag.SideBar = db.company.ToList();
            if (User.Identity.IsAuthenticated)
            {
                var orderDetail = db.orderDetail.Where(x => x.OrderID == id && x.Order.Account.UserName == User.Identity.Name).ToList();
                //Tăng 2 ngày cho thời gian đặt hàng để đếm ngược
                foreach(var item in orderDetail)
                {
                    ViewBag.time = item.Order.DateTimeOrder.AddDays(2);
                }
                ViewBag.orderDetail = orderDetail;
            }
            else
            {
                var orderDetail = db.orderDetail.Where(x => x.OrderID == id).ToList();
                ViewBag.orderDetail = orderDetail;
            }

            var status = db.orderDetail.FirstOrDefault(x => x.OrderID == id);
            ViewBag.status = status.Order.Status;
            var total = db.orderDetail.Where(x=> x.OrderID == id).Sum(x =>  x.Price);
            ViewBag.total = total;
            return View();
        }
        //Chức năng hủy order
        public ActionResult Cancel(int id)
        {
            var cancel = db.order.FirstOrDefault(x => x.OrderID == id);
            cancel.Status = 2;
            db.Entry(cancel).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("HistoryOrder", "OrderProgress", new { id = cancel.OrderID });
        }
        //Chức năng kiểm tra order dành cho khách hàng vãng lai
        [HttpPost]
        public ActionResult SearchPhone(int? num)
        {
            var orders = db.order.FirstOrDefault(x => x.OrderPhoneNumber == num);
            var order = db.order.ToList();
            foreach(var item in order)
            {
                if(num != item.OrderPhoneNumber)
                {
                    return RedirectToAction("HistoryOrder", "OrderProgress");
                }
                else
                {
                    return RedirectToAction("HistoryOrder", "OrderProgress", new { num = orders.OrderPhoneNumber });
                }
            }
            return View();
        }
    }
}