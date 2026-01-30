using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace @interface
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }

    interface IMovagle
    {
        void Move();
        void ShowMoveSpeed();
    }

    class Car : IMovagle
    {
        public void Move() { }
        public void ShowMoveSpeed() { }
    }
}
