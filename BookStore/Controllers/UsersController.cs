using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using BookStore.Models;
using BookStore.DesignPattern.Singleton;

namespace BookStore.Controllers
{
    public class UsersController : Controller
    {
        BookStoreEntities db = new BookStoreEntities();
        // GET: Users

        // Sử dụng Singleton pattern cho SessionManager
        private static readonly SessionManager sessionManager = SessionManager.Instance;

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Customer cust)
        {
            if (ModelState.IsValid)
            {
                var adminAccount = db.AdminAccounts.FirstOrDefault(k => k.Email == cust.UserEmail && k.Password == cust.UserPassword);

                if (adminAccount != null)
                {
                    sessionManager.Account = adminAccount;
                    return RedirectToAction("Index", "Admin/AdminHome");
                }
                var account = db.Customers.FirstOrDefault(k => k.UserEmail == cust.UserEmail && k.UserPassword == cust.UserPassword);
                if (account != null)
                {
                    sessionManager.Account = account;
                    return RedirectToAction("Index", "Home");
                }
                else
                    ViewBag.ThongBao = "*Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();

        }

        public ActionResult Logout()
        {
            sessionManager.Account = null;
            Session["MyCart"] = null;
            return RedirectToAction("Login", "Users");
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Detail([Bind(Include = "UserID,UserName,UserEmail,PhoneNumber,UserPassword,AvatarImage")] Customer customer, HttpPostedFileBase ImageUser)
        {
            if (ModelState.IsValid)
            {
                if (ImageUser != null)
                {
                    //Lấy tên file của hình được up lên
                    var fileName = Path.GetFileName(ImageUser.FileName);

                    //Tạo đường dẫn tới file
                    var path = Path.Combine(Server.MapPath("~/image"), fileName);
                    //Lưu tên

                    customer.AvatarImage = fileName;
                    //Save vào Images Folder
                    ImageUser.SaveAs(path);
                }
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Detail", "Users");
            }
            return View(customer);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection customer)
        {
            if (customer["userPassword"] != customer["rePassword"])
            {
                @ViewBag.Notification = "Mật khẩu xác nhận không chính xác";
                return View();
            }
            else
            {
                string email = customer["userEmail"].ToString();
                var cus = db.Customers.FirstOrDefault(k => k.UserEmail == email);
                if (cus != null)
                {
                    ViewBag.NotificationEmail = "Đã có người đăng ký bằng email này";
                    return View();
                }
                else
                {
                    Customer accout = new Customer();
                    accout.UserName = customer["userName"];
                    accout.UserEmail = customer["userEmail"];
                    accout.PhoneNumber = customer["phoneNumber"];
                    accout.UserPassword = customer["userPassword"];
                    accout.AvatarImage = "user.png";

                    db.Customers.Add(accout);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Users");
                }
            }
        }

    }
}