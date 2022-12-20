using System.Globalization;
using Humanizer;

namespace DotNetCore.Console
{
    using System;

    internal class Program
    {
        static void Main(string[] args)
        {
            //int counter = 10;

            //for (int i = 0; i < counter; i++)
            //{
            //    Console.WriteLine($"Hello, World ! , Iam Max Bayne {i} \n");
            //}

            var result = 1520.ToWords(new CultureInfo("ar"));
            Console.WriteLine(result);

        }
    }
}