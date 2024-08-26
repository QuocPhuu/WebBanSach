using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.DesignPattern.Composite
{
    public class NewsComposite : NewsComponent
    {
        private List<NewsComponent> _children;

        public NewsComposite()
        {
            _children = new List<NewsComponent>();
        }

        public void Add(NewsComponent newsComponent)
        {
            _children.Add(newsComponent);
        }

        public void Remove(NewsComponent newsComponent)
        {
            _children.Remove(newsComponent);
        }

        public override List<News> GetNews()
        {
            List<News> newsList = new List<News>();
            foreach (var child in _children)
            {
                newsList.AddRange(child.GetNews());
            }
            return newsList;
        }
    }
}