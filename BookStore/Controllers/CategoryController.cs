using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Controllers.Flyweight;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class CategoryController : Controller
    {
        private BookStoreEntities db = new BookStoreEntities();
        // GET: Category

        private readonly ProductFlyweightFactory flyweightFactory;

        public CategoryController()
        {
            flyweightFactory = new ProductFlyweightFactory();
        }

        public ActionResult Index(int id)
        {
            var products = db.Products.Where(n => n.CategoryID == id).ToList();

            List<ProductFlyweight> flyweightProducts = new List<ProductFlyweight>();
            foreach (var product in products)
            {
                var flyweight = flyweightFactory.GetProductFlyweight(product.ProductID, product.ProductName, product.AuthorName, product.IntialPrice ?? 0, product.Price, product.CategoryID, product.Image, product.amount, product.ProductIntroduction);
                flyweightProducts.Add(flyweight);
            }

            ViewBag.CategoryProd = db.Categories.FirstOrDefault(n => n.CategoryID == id);
            ViewBag.IdCategory = id;
            return View(flyweightProducts);
        }

        public ActionResult GetAllProduct()
        {
            var product = (from item in db.Products orderby item.ProductID descending select item).ToList();
            return View(product);
        }

        public ActionResult Search(string searchString)
        {
            var result = db.Products.Where(s => s.ProductName.Contains(searchString)).ToList();
            return View(result);
        }

    }
}