using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using BookStore.DesignPattern.Builder;

namespace BookStore.Controllers
{
    public class OrderController : Controller
    {
        BookStoreEntities db = new BookStoreEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetOrder(int id)
        {
            var orderList = (from o in db.Orders orderby o.IdOrder descending where o.UserID == id select o).ToList();

            ViewBag.UserId = id;
            return View(orderList);
        }

        public ActionResult OrderDetail(int id)
        {
            var listProdOrder = db.OrderDetails.Where(p => p.IdOrder == id).ToList();

            decimal finalPrice = listProdOrder.Sum(item => (decimal)item.FinalPrice);

            var order = new OrderBuilder()
                .WithId(id)
                .WithAddress(db.Orders.FirstOrDefault(o => o.IdOrder == id)?.Address)
                .WithDate(db.Orders.FirstOrDefault(o => o.IdOrder == id)?.DateOrder ?? DateTime.MinValue)
                .WithStatus(db.Orders.FirstOrDefault(o => o.IdOrder == id)?.StatusOrder ?? 0)
                .Build();

            ViewBag.Address = order.Address;
            ViewBag.Date = order.DateOrder;
            ViewBag.Id = order.IdOrder;
            ViewBag.Status = order.StatusOrder;

            ViewBag.FinalPrice = finalPrice;
            ViewBag.Customer = order;

            return View(listProdOrder);
        }

        public ActionResult CancelOrder(int id)
        {
            var order = db.Orders.FirstOrDefault(o => o.IdOrder == id);
            order.StatusOrder = 1;
            db.SaveChanges();

            int idUser = order.UserID.GetValueOrDefault();
            return RedirectToAction("GetOrder/" + idUser, "Order");
        }
    }
}