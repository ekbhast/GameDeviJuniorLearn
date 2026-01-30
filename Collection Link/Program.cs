using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection_Link
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Cart cart = new Cart();
            cart.ShowProducts();
        }
    }


    class Cart
    {
        private List<Product> _products = new List<Product>();

        public Cart()
        {
            _products.Add(new Product("Яблоко"));
            _products.Add(new Product("Банан"));
            _products.Add(new Product("Апельсин"));
            _products.Add(new Product("Груша"));
        }

        public void ShowProducts()
        {
            foreach (var product in _products) 
            {
                Console.WriteLine(product.Name);
            }
        }
    }

    class Product
    {
        public string Name { get; private set; }

        public Product(string name)
        {
            Name = name;
        }
    }
}
