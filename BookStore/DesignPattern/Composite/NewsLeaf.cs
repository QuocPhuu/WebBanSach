using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.DesignPattern.Composite
{
    public class NewsLeaf : NewsComponent
    {
        private News _news;

        public NewsLeaf(News news)
        {
            _news = news;
        }

        public override List<News> GetNews()
        {
            return new List<News> { _news };
        }
    }
}