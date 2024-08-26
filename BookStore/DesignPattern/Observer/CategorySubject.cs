using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BookStore.Models;

namespace BookStore.DesignPattern.Observer
{
    public class CategorySubject
    {
        private List<ICategoryObserver> observers = new List<ICategoryObserver>();
        private List<Category> categories = new List<Category>();

        private BookStoreEntities db = new BookStoreEntities();

        public void Attach(ICategoryObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(ICategoryObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in observers)
            {
                observer.Update(categories);
            }
        }

        public void AddCategory(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
            Notify();
        }

        public void EditCategory(Category category)
        {
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            Notify();
        }

        public void RemoveCategory(int categoryId)
        {
            Category category = db.Categories.Find(categoryId);
            if (category != null)
            {
                //db.Categories.Attach(category);
                //db.Entry(category).State = EntityState.Detached;
                db.Categories.Remove(category);
                db.SaveChanges();
                Notify();
            }
        }
    }
}