using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.DesignPattern.Strategy
{
    public class From100to250vndStrategy : IPriceRangeStrategy
    {
        public IQueryable<Product> FilterProducts(BookStoreEntities db, int categoryId)
        {
            var products = db.Products.OrderByDescending(p => p.ProductID).Where(p => p.Price > 100000 && p.Price <= 250000);
            if (categoryId != 0)
                products = products.Where(item => item.CategoryID == categoryId);
            return products;
        }
    }
}