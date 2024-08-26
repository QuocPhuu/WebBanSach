using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Controllers.Flyweight
{
    //Category Controller
    public class ProductFlyweight
    {
        public int ProductID { get; }
        public string ProductName { get; }
        public string AuthorName { get; }
        public decimal InitialPrice { get; }
        public decimal Price { get; }
        public int CategoryID { get; }
        public string Image { get; }
        public int? Amount { get; }
        public string ProductIntroduction { get; }

        public ProductFlyweight(int productId, string productName, string authorName, decimal initialPrice, decimal price, int categoryId, string image, int? amount, string productIntroduction)
        {
            ProductID = productId;
            ProductName = productName;
            AuthorName = authorName;
            InitialPrice = initialPrice;
            Price = price;
            CategoryID = categoryId;
            Image = image;
            Amount = amount;
            ProductIntroduction = productIntroduction;
        }
    }
}