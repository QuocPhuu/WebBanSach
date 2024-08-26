using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.DesignPattern.Builder
{
    //Order Controller
    public class OrderBuilder
    {
        private Order _order;

        public OrderBuilder()
        {
            _order = new Order();
        }

        public OrderBuilder WithId(int id)
        {
            _order.IdOrder = id;
            return this;
        }

        public OrderBuilder WithAddress(string address)
        {
            _order.Address = address;
            return this;
        }

        public OrderBuilder WithDate(DateTime date)
        {
            _order.DateOrder = date;
            return this;
        }

        public OrderBuilder WithStatus(int status)
        {
            _order.StatusOrder = status;
            return this;
        }

        public Order Build()
        {
            return _order;
        }
    }
}