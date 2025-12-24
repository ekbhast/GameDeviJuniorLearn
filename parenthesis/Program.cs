using System;

namespace parenthesis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char openParen = '(';
            char closeParen = ')';

            string parens = "(()))(()";
            int depth = 0;
            int maxDepth = 0;

            bool isValid = true;

            for (int i = 0; i < parens.Length; i++)
            {
                if (parens[i] == openParen)
                {
                    depth++;
                }
                else if (parens[i] == closeParen)
                {
                    depth--;
                }

                if (depth > maxDepth)
                {
                    maxDepth = depth;
                }

                if (depth < 0)
                {
                    isValid = false;
                    break;
                }
            }

            if (depth != 0)
            {
                isValid = false;
            }

            if (isValid)
            {
                Console.WriteLine($"Корректная строка. Максимальная глубина: {maxDepth}");
            }
            else
            {
                Console.WriteLine("Некорректная строка");
            }
        }
    }
}
