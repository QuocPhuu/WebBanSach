using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.DesignPattern.Builder
{
    public class OrderDetailBuilder
    {
        private OrderDetail _orderDetail;

        public OrderDetailBuilder()
        {
            _orderDetail = new OrderDetail();
        }

        public OrderDetailBuilder WithFinalPrice(decimal price)
        {
            _orderDetail.FinalPrice = price;
            return this;
        }

        public OrderDetail Build()
        {
            return _orderDetail;
        }
    }
}