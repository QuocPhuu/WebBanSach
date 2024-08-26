using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.DesignPattern.Composite
{
    //News Controller
    public abstract class NewsComponent
    {
        public abstract List<News> GetNews();
    }
}