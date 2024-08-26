using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.DesignPattern.Command
{
    public class FetchConfirmOrderCommand : ICommand
    {
        private readonly BookStoreEntities _db;
        private readonly int _orderId;

        public FetchConfirmOrderCommand(BookStoreEntities db, int orderId)
        {
            _db = db;
            _orderId = orderId;
        }

        public void Execute(Controller controller)
        {
            var prodListOrder = _db.OrderDetails.Where(o => o.IdOrder == _orderId).ToList();
            foreach (var item in prodListOrder)
            {
                var product = _db.Products.FirstOrDefault(p => p.ProductID == item.ProductID);
                product.amount -= item.Quantity;
            }
            var order = _db.Orders.FirstOrDefault(o => o.IdOrder == _orderId);
            order.StatusOrder = 2;
            _db.SaveChanges();
        }
    }
}