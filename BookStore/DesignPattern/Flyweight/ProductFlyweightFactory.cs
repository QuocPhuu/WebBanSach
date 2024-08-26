using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Controllers.Flyweight
{
    public class ProductFlyweightFactory
    {
        private Dictionary<int, ProductFlyweight> flyweights = new Dictionary<int, ProductFlyweight>();

        public ProductFlyweight GetProductFlyweight(int productId, string productName, string authorName, decimal initialPrice, decimal price, int categoryId, string image, int? amount, string productIntroduction)
        {
            if (!flyweights.ContainsKey(productId))
            {
                flyweights[productId] = new ProductFlyweight(productId, productName, authorName, initialPrice, price, categoryId, image, amount, productIntroduction);
            }
            return flyweights[productId];
        }
    }
}