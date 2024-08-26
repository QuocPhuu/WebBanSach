using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.DesignPattern.Factory_Method
{
    public class ProductFactory : IProductFactory
    {
        private BookStoreEntities db;

        public ProductFactory(BookStoreEntities dbContext)
        {
            db = dbContext;
        }

        public Product CreateProduct(int productId)
        {
            return db.Products.FirstOrDefault(p => p.ProductID == productId);
        }
    }
}