using System;
using System.Linq;

namespace Air_3550
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            using (var db = new AirContext())
            {
                Console.WriteLine("Querying Airports");
                var toledoExpress = db.Airports.Where(a => a.City == "Toledo").First();
                Console.WriteLine(toledoExpress.City);
                Console.WriteLine(toledoExpress.State);
                Console.WriteLine(toledoExpress.Country);
                Console.WriteLine(toledoExpress.Latitude);
                Console.WriteLine(toledoExpress.Longitude);
                Console.WriteLine(toledoExpress.Elevation);
            }
            
        }
    }
}
