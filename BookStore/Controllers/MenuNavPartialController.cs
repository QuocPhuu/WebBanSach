using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class MenuNavPartialController : Controller
    {
        BookStoreEntities db = new BookStoreEntities();
        // GET: MenuNavPartial
        public ActionResult MenuNav()
        {
            ViewBag.CategoryNavList = db.Categories.ToList();
            return PartialView();
        }
    }
}