using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DesignPattern.Strategy
{
    public interface IPriceRangeStrategy
    {
        IQueryable<Product> FilterProducts(BookStoreEntities db, int categoryId);
    }
}
