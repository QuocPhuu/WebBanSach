using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DesignPattern.Factory_Method
{
    //Favorite Product Controller
    public interface IProductFactory
    {
        Product CreateProduct(int productId);
    }
}
