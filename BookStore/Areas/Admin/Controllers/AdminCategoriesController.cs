using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.DesignPattern.Observer;
using BookStore.Models;

namespace BookStore.Areas.Admin.Controllers
{
    public class AdminCategoriesController : Controller
    {
        private BookStoreEntities db = new BookStoreEntities();
        private CategorySubject categorySubject = new CategorySubject();

        public AdminCategoriesController()
        {
            categorySubject.Attach(new CategoryListView());
            categorySubject.Attach(new CategoryDetailsView());
        }

        // GET: Admin/AdminCategories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: Admin/AdminCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin/AdminCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category, HttpPostedFileBase ImgCate)
        {
            if (ModelState.IsValid)
            {
                if (ImgCate != null)
                {
                    var fileName = Path.GetFileName(ImgCate.FileName);
                    var path = Path.Combine(Server.MapPath("~/image"), fileName);
                    category.CategoryImage = fileName;
                    ImgCate.SaveAs(path);
                }
                categorySubject.AddCategory(category); // Notify observers
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Admin/AdminCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/AdminCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,CategoryName,CategoryImage")] Category category, HttpPostedFileBase ImgCate)
        {
            if (ModelState.IsValid)
            {
                if (ImgCate != null)
                {
                    var fileName = Path.GetFileName(ImgCate.FileName);
                    var path = Path.Combine(Server.MapPath("~/image"), fileName);
                    category.CategoryImage = fileName;
                    ImgCate.SaveAs(path);
                }
                categorySubject.EditCategory(category); // Notify observers
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Admin/AdminCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/AdminCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            categorySubject.RemoveCategory(id); // Notify observers
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}