using System;
using System.IO;
using System.Linq;

namespace Traveler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string text = File.ReadAllText(@"~\..\..\..\..\..\..\data\input.txt");

            Console.WriteLine(text);

            var result = TravelParser.Run(text);

            foreach (var item in result)
            {
                Console.WriteLine("X={0} Y={1} D={2}", item.x, item.y, item.direction);
            }
        }
    }
}
