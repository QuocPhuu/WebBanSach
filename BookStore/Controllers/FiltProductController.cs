using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using BookStore.DesignPattern.Strategy;

namespace BookStore.Controllers
{
    public class FiltProductController : Controller
    {
        // GET: FiltProduct

        private readonly BookStoreEntities _db;

        public FiltProductController()
        {
            _db = new BookStoreEntities();
        }

        public ActionResult Index()
        {
            return View();
        }

        private ActionResult FilterProducts(IPriceRangeStrategy strategy, int id)
        {
            var products = strategy.FilterProducts(_db, id).ToList();
            ViewBag.CategoryProd = _db.Categories.FirstOrDefault(n => n.CategoryID == id);
            ViewBag.IdCategory = id;
            return View(products);
        }

        //dưới 100.000đ
        public ActionResult Under100vndAllProduct(int id)
        {
            var strategy = new Under100vndStrategy();
            return FilterProducts(strategy, id);
        }

        //từ 100.000 đến 250.000
        public ActionResult From100to250vndAllProduct(int id)
        {
            var strategy = new From100to250vndStrategy();
            return FilterProducts(strategy, id);
        }

        //từ 250.000 đến 500.000
        public ActionResult From250to500vndAllProduct(int id)
        {
            var strategy = new From250to500vndStrategy();
            return FilterProducts(strategy, id);
        }

        //trên 500.000
        public ActionResult Over500vndAllProduct(int id)
        {
            var strategy = new Over500vndStrategy();
            return FilterProducts(strategy, id);
        }

        //giá thấp -> cao
        public ActionResult IncreaseWithPrice(int id)
        {
            var strategy = new IncreaseWithPriceStrategy();
            return FilterProducts(strategy, id);
        }

        //giá cao -> thấp
        public ActionResult DescreaseWithPrice(int id)
        {
            var strategy = new DecreaseWithPriceStrategy();
            return FilterProducts(strategy, id);
        }
    }
}