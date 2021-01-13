using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMDT_Web.Data;

namespace TMDT_Web.Areas.Admin.Controllers
{
    public class FinanceController : Controller
    {
        DataContext db = new DataContext();
        // GET: Admin/Finance
        public ActionResult Index(int? month)
        {
            int[] days = {
                1,2,3,4,5,6,7,8,9,10,
                11,12,13,14,15,16,17,18,19,20,
                21,22,23,24,25,26,27,28,29,30,31};
            ViewBag.Month = month;
            ViewBag.days = days;
            string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            ViewBag.months = months;
            ViewBag.count = 1;
            ViewBag.countDays = 1;
            int[] outcomeDay = days;
            if(month == null)
            {
                ViewBag.alert = "Select Month";
            }
            else
            {
                for (int i = 0; i < days.Length; i++)
                {
                    int y = days[i];
                    var test1 = db.orderDetail.Where(x => x.Order.DateTimeOrder.Day == y && x.Order.DateTimeOrder.Month == month).ToList();
                    if (test1.Count == 0)
                    {
                        outcomeDay[i] = 0;
                    }
                    else
                    {
                        outcomeDay[i] = test1.Sum(x => x.Price);
                    }
                }
                ViewBag.test1 = outcomeDay;
                for (int i = 0; i <= months.Length; i++)
                {
                    if (i == month)
                    {
                        ViewBag.oneMonth = months[i-1];
                    }
                }
                int? monthOutcome = db.orderDetail.Where(x => x.Order.DateTimeOrder.Month == month).Sum(x => (int?)x.Price) ?? 0;
                if (monthOutcome == null) 
                {
                    ViewBag.monthOutcome = "0";
                }
                else
                {
                    ViewBag.monthOutcome = monthOutcome;
                }
            }
            return View();
        }
        public ActionResult GetMonth(int Month)
        {
            return RedirectToAction("Index", "Finance", new { month = Month });
        }
    }
}