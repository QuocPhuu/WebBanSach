using BookStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Web;

namespace BookStore.DesignPattern.Facade
{
    //Detail Controller
    public class BookStoreFacade
    {
        private BookStoreEntities db;

        public BookStoreFacade()
        {
            db = new BookStoreEntities();
        }

        public void AddComment(Comment comment)
        {
            db.Comments.Add(comment);
            db.SaveChanges();
        }

        public void DeleteComment(int commentId)
        {
            var comment = db.Comments.FirstOrDefault(c => c.id == commentId);
            if (comment != null)
            {
                db.Comments.Remove(comment);
                db.SaveChanges();
            }
        }
    }
}