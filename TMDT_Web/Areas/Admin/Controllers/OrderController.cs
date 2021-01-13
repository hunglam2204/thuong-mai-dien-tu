using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using TMDT_Web.Data;

namespace TMDT_Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        DataContext db = new DataContext();
        // GET: Admin/Orders
        public ActionResult Index()
        {
            var orders = db.order.ToList();
            return View(orders);
        }
        public ActionResult Accept(int id)
        {
            var accept = db.order.FirstOrDefault(x => x.OrderID == id);
            accept.Status = 1;
            db.Entry(accept).State = EntityState.Modified;
            db.SaveChanges();
            //Tính tích điểm cho ng dùng
            if (accept.UserID != null)
            {
                int sum = 0;
                var prices = db.orderDetail.Where(x => x.OrderID == id).ToList();
                for (int i = 0; i < prices.Count; i++)
                {
                    sum += prices[i].Price;
                }
                var total = sum;
                if (total >= 1000000)
                {
                    var points = db.account.FirstOrDefault(x => x.UserID == accept.Account.UserID);
                    if (points.Points == null)
                    {
                        points.Points = 0;
                    }
                    points.Points = points.Points + 1;
                    db.Entry(points).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }
            ////Mail
            //MailMessage msg = new MailMessage();

            //msg.From = new MailAddress("dohongthaisdd@gmail.com");
            //msg.To.Add(accept.Account.Email);
            //msg.Subject = "KingStore";
            //msg.Body = "Chúng tôi đã xác nhận đơn hàng id:" + accept.OrderID + ", Cám ơn Khách hàng";
            ////msg.Priority = MailPriority.High;


            //using (SmtpClient client = new SmtpClient())
            //{
            //    client.EnableSsl = true;
            //    client.UseDefaultCredentials = false;
            //    client.Credentials = new NetworkCredential("dohongthaisdd@gmail.com", "dohongthai");
            //    client.Host = "smtp.gmail.com";
            //    client.Port = 587;
            //    client.DeliveryMethod = SmtpDeliveryMethod.Network;

            //    client.Send(msg);
            //}


            return RedirectToAction("index", "order");
        }
        public ActionResult Transport(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var cancel = db.order.FirstOrDefault(x => x.OrderID == id);
                cancel.Status = 3;
                db.Entry(cancel).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                var cancel = db.order.FirstOrDefault(x => x.OrderPhoneNumber == id);
                cancel.Status = 3;
                db.Entry(cancel).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("index", "order");
        }
        public ActionResult Finish(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var cancel = db.order.FirstOrDefault(x => x.OrderID == id);
                cancel.Status = 4;
                db.Entry(cancel).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                var cancel = db.order.FirstOrDefault(x => x.OrderPhoneNumber == id);
                cancel.Status = 4;
                db.Entry(cancel).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("index", "order");
        }
        public ActionResult Cancel(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var cancel = db.order.FirstOrDefault(x => x.OrderID == id);
                cancel.Status = 2;
                db.Entry(cancel).State = EntityState.Modified;
                db.SaveChanges();

                var increaseProduct = db.orderDetail.FirstOrDefault(x => x.OrderID == cancel.OrderID);
                increaseProduct.Product.Quantity += 1;
                db.Entry(increaseProduct).State = EntityState.Modified;
                db.SaveChanges();


                ////Mail
                //MailMessage msg = new MailMessage();
                //msg.From = new MailAddress("dohongthaisdd@gmail.com");
                //msg.To.Add(cancel.Account.Email);
                //msg.Subject = "KingStore";
                //msg.Body = "Đơn Hàng:" + cancel.OrderID + ", Đã bị hủy";
                ////msg.Priority = MailPriority.High;
                //using (SmtpClient client = new SmtpClient())
                //{
                //    client.EnableSsl = true;
                //    client.UseDefaultCredentials = false;
                //    client.Credentials = new NetworkCredential("dohongthaisdd@gmail.com", "dohongthai");
                //    client.Host = "smtp.gmail.com";
                //    client.Port = 587;
                //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //    client.Send(msg);
                //}
            }
            else
            {
                var cancel = db.order.FirstOrDefault(x => x.OrderPhoneNumber == id);
                cancel.Status = 2;
                db.Entry(cancel).State = EntityState.Modified;
                db.SaveChanges();

                var increaseProduct = db.orderDetail.FirstOrDefault(x => x.OrderID == cancel.OrderID);
                increaseProduct.Product.Quantity += 1;
                db.Entry(increaseProduct).State = EntityState.Modified;
                db.SaveChanges();
                ////Mail
                //MailMessage msg = new MailMessage();
                //msg.From = new MailAddress("dohongthaisdd@gmail.com");
                //msg.To.Add(cancel.Account.Email);
                //msg.Subject = "KingStore";
                //msg.Body = "Đơn hàng :" + cancel.OrderID + ", Đã bị hủy";
                ////msg.Priority = MailPriority.High;
                //using (SmtpClient client = new SmtpClient())
                //{
                //    client.EnableSsl = true;
                //    client.UseDefaultCredentials = false;
                //    client.Credentials = new NetworkCredential("dohongthaisdd@gmail.com", "dohongthai");
                //    client.Host = "smtp.gmail.com";
                //    client.Port = 587;
                //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //    client.Send(msg);
                //}
            }
            return RedirectToAction("index", "order");
        }
        public ActionResult Detail(int id)
        {
            var detail = db.orderDetail.Where(x => x.OrderID == id).ToList();
            return View(detail);
        }
    }
}