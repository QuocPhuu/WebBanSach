using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.DesignPattern.Observer
{
    //Admin Categories Controller
    public interface ICategoryObserver
    {
        void Update(List<Category> categories);
    }
}
