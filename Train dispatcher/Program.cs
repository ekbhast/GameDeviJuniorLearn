using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Train_dispatcher
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class Dispatcher
    {
        private List<Train> _train = new List<Train>();
        private Queue<Passenger> _Passengers = new Queue<Passenger>(); 

        public void Work()
        {

        }

        public void ShowTrains()
        {

        }
    }

    class Train
    {
        private List<Car> _cars = new List<Car>();
    }

    class TrainFabric
    {

    }

    class Car
    {
        private List<Passenger> _passengers = new List<Passenger>();
    }

    class CarFabric
    {

    }

    class Passenger
    {
        public Passenger(int id)
        {
            Id = id;
        }

        public int Id {  get; private set; } 
    }

    class PassengerFabric
    {

    }
}
