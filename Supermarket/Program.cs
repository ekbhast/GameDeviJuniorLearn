using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket
{
    class Supermarket
    {
        private Queue<Customer> _customers = new Queue<Customer>();
        private Dictionary<string, int> _products = new Dictionary<string, int>();
        private int _balance = 0;

        public void Work()
        {

        }
    }

    class Customer
    {
        private int _balance;
        private Dictionary <string, int> _cart = new Dictionary<string, int>();
        private Dictionary <string, int> _bag = new Dictionary<string, int>();


    }

    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
