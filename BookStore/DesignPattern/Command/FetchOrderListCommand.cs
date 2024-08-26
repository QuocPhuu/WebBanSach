using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.DesignPattern.Command
{
    public class FetchOrderListCommand : ICommand
    {
        private readonly BookStoreEntities _db;

        public FetchOrderListCommand(BookStoreEntities db)
        {
            _db = db;
        }

        public void Execute(Controller controller)
        {
            var orderList = (from order in _db.Orders orderby order.IdOrder descending select order).ToList();
            controller.ViewData["OrderList"] = orderList;
        }
    }
}