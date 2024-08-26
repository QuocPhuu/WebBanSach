using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.DesignPattern.Template_Method;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class HomeController : BaseController
    {
        protected override void PreprocessIndex()
        {
            ViewBag.CategoriesList = db.Categories.ToList();
            ViewBag.ProductsList = (from item in db.Products orderby item.ProductID descending select item).ToList().Take(10);
        }

        protected override void ProcessIndex()
        {
            int firstCate = db.Categories.First().CategoryID;
            int secondCate = db.Categories.FirstOrDefault(c => c.CategoryID != firstCate).CategoryID;
            ViewBag.FirstCate = db.Categories.FirstOrDefault(c => c.CategoryID == firstCate);
            ViewBag.SecondCate = db.Categories.FirstOrDefault(c => c.CategoryID == secondCate);
            ViewBag.ProductsList_1 = db.Products.Where(p => p.CategoryID == firstCate).ToList().Take(10);
            ViewBag.ProductsList_2 = db.Products.Where(p => p.CategoryID == secondCate).ToList().Take(10);
        }

        protected override void PostprocessIndex()
        {
            // Additional post-processing specific to HomeController
        }

        protected override void PreprocessProductCategory(int id)
        {
            var product = (from item in db.Products
                           orderby item.ProductID descending
                           where item.CategoryID == id
                           select item).ToList();
            ViewBag.CategoryProd = db.Categories.FirstOrDefault(n => n.CategoryID == id);
            ViewBag.IdCategory = id;
            ViewBag.Products = product;
        }

        protected override void ProcessProductCategory(int id)
        {
            // No additional processing for HomeController's ProductCategory
        }

        protected override void PostprocessProductCategory(int id)
        {
            // Additional post-processing specific to HomeController's ProductCategory
        }

        protected override void PreprocessAbout()
        {
            // Additional post-processing specific to HomeController's About
        }

        protected override void ProcessAbout()
        {
            // Additional post-processing specific to HomeController's About
        }

        protected override void PostprocessAbout()
        {
            // Additional post-processing specific to HomeController's About
        }
    }
}