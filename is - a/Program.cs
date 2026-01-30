using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace is___a
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Knight warrior1 = new Knight(100, 10);
            Barbarian warrior2 = new Barbarian(100, 1, 7, 2);
            warrior1.TakeDamage(500);
            warrior2.TakeDamage(250);

            Console.WriteLine("Рыцарь:");
            warrior1.ShowHealt();

            Console.WriteLine("Варвар:");
            warrior2.ShowHealt();
        }
    }

    class Warrior
    {
        public int Health;
        public int Armor;
        public int Damage;

        public Warrior(int health, int armor, int damage)
        {
            Health = health;
            Armor = armor;
            Damage = damage;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage - Armor;
        }

        public void ShowHealt()
        {
            Console.WriteLine(Health);
        }
    }

    class Knight : Warrior
    {
        public Knight(int health, int damage) : base(health, 5, damage)
        {
        }

        public void Prey()
        {
            Armor += 2;
        }
    }

    class Barbarian : Warrior
    {
        public int AttacSpeed;
        public Barbarian(int health, int armor, int damage, int attacSpeed): base(health, armor, damage * attacSpeed) { }
        public void Shout()
        {
            Armor -= 2;
            Health += 10;
        }
    }
}
