using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abstract__class
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Vehicle car = new Car();
            
        }
    }

    abstract class Vehicle
    {
        protected float Speed;

        public abstract void Move();

        public float GetCurrentSpeed()
        {
            return Speed;
        };
    }

    class Car : Vehicle
    {
        public override void Move() 
        {
            Console.WriteLine("Машина едет по асфальту");
        }
}
