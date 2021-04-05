using Air_3550.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Air_3550
{
    class AirContext : DbContext
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
            modelBuilder.Entity<Airport>().HasData(
                new Airport
                {
                    AirportId = 1,
                    AirportCode = "TOL",
                    City = "Toledo",
                    State = "Ohio",
                    Country = "USA",
                    Latitude = 41.586806M,
                    Longitude = -83.807833M,
                    Elevation = 684
                }
                );
        }
    }
}
