using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using BookStore.Models;
using System.Data.Entity;
using BookStore.DesignPattern.Facade;

namespace BookStore.Controllers
{
    public class DetailsController : Controller
    {
        private BookStoreFacade facade;

        public DetailsController()
        {
            facade = new BookStoreFacade();
        }

        BookStoreEntities db = new BookStoreEntities();

        public ActionResult Index(int id)
        {
            ViewBag.ProdDetails = db.Products.FirstOrDefault(n => n.ProductID == id);
            int thisProdCategories = db.Products.FirstOrDefault(n => n.ProductID == id).CategoryID;
            ViewBag.ThisProdCategories = db.Categories.FirstOrDefault(n => n.CategoryID == thisProdCategories);
            ViewBag.ProductList = (from p in db.Products where p.CategoryID == thisProdCategories && p.ProductID != id select p).ToList().Take(10);
            ViewBag.CommentList = (from c in db.Comments orderby c.id descending where c.ProductID == id select c).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AddComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                facade.AddComment(comment);
            }
            int productId = comment.ProductID.GetValueOrDefault();
            return RedirectToAction("Index/" + productId, "Details");
        }

        public ActionResult DeleteComment(int id)
        {
            var cmt = db.Comments.Where(c => c.id == id).FirstOrDefault();
            int idProduct = cmt.ProductID.GetValueOrDefault();
            if (ModelState.IsValid)
            {
                facade.DeleteComment(id);
            }
            return RedirectToAction("Index/" + idProduct, "Details");
        }
    }
}