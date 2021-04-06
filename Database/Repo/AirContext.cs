using Air_3550.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Air_3550.Repo
{
    public class AirContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Trip> Trips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            Console.WriteLine("Entered OnConfiguring");
            var appDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var airDir = Path.Combine(appDataDirectory, "Air 3550 Team 11");
            Directory.CreateDirectory(airDir);
            var dbPath = Path.Combine(airDir, "air3550.db");
            options.UseSqlite(@"Data Source=" + dbPath);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Console.WriteLine("Entered OnModelCreating");

            // Seeding airport data
            modelBuilder.Entity<Airport>().HasData(
                new Airport { AirportId = 1, AirportCode = "CLE", City = "Cleveland", State = "Ohio", Country = "USA", Latitude = 41.411667M, Longitude = -81.849722M, Elevation = 791 },
                new Airport { AirportId = 2, AirportCode = "ATL", City = "Atlanta", State = "Georgia", Country = "USA", Latitude = 33.636667M, Longitude = -84.428056M, Elevation = 1027 },
                new Airport { AirportId = 3, AirportCode = "LAX", City = "Los Angeles", State = "California", Country = "USA", Latitude = 33.9425M, Longitude = -118.408056M, Elevation = 125 },
                new Airport { AirportId = 4, AirportCode = "MDW", City = "Chicago", State = "Illinois", Country = "USA", Latitude = 41.786111M, Longitude = -87.7525M, Elevation = 620 },
                new Airport { AirportId = 5, AirportCode = "DAL", City = "Dallas", State = "Texas", Country = "USA", Latitude = 32.847222M, Longitude = -96.851667M, Elevation = 486 },
                new Airport { AirportId = 6, AirportCode = "DEN", City = "Denver", State = "Colorado", Country = "USA", Latitude = 39.861667M, Longitude = -104.673056M, Elevation = 5430 },
                new Airport { AirportId = 7, AirportCode = "LGA", City = "New York", State = "New York", Country = "USA", Latitude = 40.775M, Longitude = -73.875M, Elevation = 19 },
                new Airport { AirportId = 8, AirportCode = "MIA", City = "Miami", State = "Florida", Country = "USA", Latitude = 25.793333M, Longitude = -80.290556M, Elevation = 9 },
                new Airport { AirportId = 9, AirportCode = "SEA", City = "Seattle", State = "Washington", Country = "USA", Latitude = 47.448889M, Longitude = -122.309444M, Elevation = 433 },
                new Airport { AirportId = 10, AirportCode = "BNA", City = "Nashville", State = "Tennessee", Country = "USA", Latitude = 36.126667M, Longitude = -86.681944M, Elevation = 599}
            );
        }
    }
}
