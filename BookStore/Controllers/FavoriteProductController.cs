using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.DesignPattern.Factory_Method;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class FavoriteProductController : Controller
    {
        private BookStoreEntities db = new BookStoreEntities();
        private IProductFactory productFactory;

        public FavoriteProductController()
        {
            productFactory = new ProductFactory(db);
        }

        // GET: FavoriteProduct
        public ActionResult FavoriteList(int id)
        {
            var favoriteProducts = db.FavoriteProducts.Where(n => n.UserID == id).ToList();

            List<Product> products = new List<Product>();
            foreach (var item in favoriteProducts)
            {
                int productId = item.ProductID ?? 0;
                products.Add(productFactory.CreateProduct(productId));
            }

            ViewBag.ProductInfor = products;

            return View(favoriteProducts);
        }

        [HttpPost]
        public ActionResult InsertListFavorite(FavoriteProduct favoriteProd)
        {
            if (ModelState.IsValid)
            {
                var productAvail = db.FavoriteProducts.FirstOrDefault(p => p.ProductID == favoriteProd.ProductID && p.UserID == favoriteProd.UserID);
                if (productAvail != null)
                    return RedirectToAction("Index/" + favoriteProd.ProductID, "Details");
                else
                {
                    db.FavoriteProducts.Add(favoriteProd);
                    db.SaveChanges();
                    return RedirectToAction("FavoriteList/" + favoriteProd.UserID, "FavoriteProduct");
                }
            }
            return View("Index/" + favoriteProd.ProductID, "Details");
        }

        public ActionResult DeleteProduct(FavoriteProduct favoriteProd)
        {
            if (ModelState.IsValid)
            {
                var prod = db.FavoriteProducts.FirstOrDefault(p => p.ProductID == favoriteProd.ProductID && p.UserID == favoriteProd.UserID);
                db.FavoriteProducts.Remove(prod);
                db.SaveChanges();
            }
            return RedirectToAction("FavoriteList/" + favoriteProd.UserID, "FavoriteProduct");
        }
    }
}