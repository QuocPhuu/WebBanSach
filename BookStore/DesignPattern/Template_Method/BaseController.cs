using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.DesignPattern.Template_Method
{
    //Home Controller
    public abstract class BaseController : Controller
    {
        protected BookStoreEntities db = new BookStoreEntities();

        public virtual ActionResult Index()
        {
            PreprocessIndex();
            ProcessIndex();
            PostprocessIndex();
            return View();
        }

        protected abstract void PreprocessIndex();
        protected abstract void ProcessIndex();
        protected abstract void PostprocessIndex();

        public virtual ActionResult ProductCategory(int id)
        {
            PreprocessProductCategory(id);
            ProcessProductCategory(id);
            PostprocessProductCategory(id);
            return View();
        }

        protected abstract void PreprocessProductCategory(int id);
        protected abstract void ProcessProductCategory(int id);
        protected abstract void PostprocessProductCategory(int id);

        public virtual ActionResult About()
        {
            PreprocessAbout();
            ProcessAbout();
            PostprocessAbout();
            return View();
        }

        protected abstract void PreprocessAbout();
        protected abstract void ProcessAbout();
        protected abstract void PostprocessAbout();
    }
}