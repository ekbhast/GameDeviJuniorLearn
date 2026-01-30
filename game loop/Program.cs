using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace game_loop
{
    internal class Program
    {
        static void Main(string[] args)
        {
                Behavior[] behaviors =
                {
                    new Walker(),
                    new Jumper(),
                };
            while (true)
            {
                foreach (var behavior in behaviors)
                {
                    behavior.Update();
                    Thread.Sleep(1000);
                }
            }
        }
    }

    class Behavior
    {
        public virtual void Update() { }
    }

    class Walker : Behavior 
    {
        public override void Update() 
        {
            Console.WriteLine("Иду");
        }
    }

    class Jumper : Behavior
    {
        public override void Update()
        {
            Console.WriteLine("Прыгаю");
        }
    }
}
