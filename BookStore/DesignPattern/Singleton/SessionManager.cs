using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Models;

namespace BookStore.DesignPattern.Singleton
{
    //User Controller
    public class SessionManager
    {
        private static SessionManager instance;
        private static readonly object lockObject = new object();

        private SessionManager() { }

        public static SessionManager Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new SessionManager();
                    }
                    return instance;
                }
            }
        }

        public object Account
        {
            get { return HttpContext.Current.Session["Account"]; }
            set { HttpContext.Current.Session["Account"] = value; }
        }
    }
}