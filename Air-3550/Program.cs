using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Air_3550
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new AirContext())
            {
                db.Database.Migrate();
            }
            Console.WriteLine("Database Migrated.");
            using (var db = new AirContext())
            {
                Console.WriteLine("Querying Airports");
                db.Airports.ForEachAsync(airport =>
                {
                    Console.Write(airport.City + ", ");
                    Console.Write(airport.State + ", ");
                    Console.Write(airport.Country + ", ");
                    Console.Write(airport.Latitude + ", ");
                    Console.Write(airport.Longitude + ", ");
                    Console.Write(airport.Elevation);
                    Console.WriteLine();
                });
            }
        }
    }
}
