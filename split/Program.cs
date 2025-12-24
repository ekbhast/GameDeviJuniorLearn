using System;

namespace split
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string text = "Lorem ipsum dolor sit amet, " +
                "consectetur adipiscing elit, sed do eiusmod " +
                "tempor incididunt ut labore et dolore magna aliqua.";

            string[] words = text.Split();

            foreach( string word in words) 
            {
                Console.WriteLine(word);
            }
        }
    }
}
