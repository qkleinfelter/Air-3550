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
            // note: Elevation is in units of feet, this gets converted in the Haversine formula but could probably
            // converted in seeding for a cleaner haversine if wanted, just will need to update that function
            var cle = new Airport { AirportId = 1, AirportCode = "CLE", City = "Cleveland", State = "Ohio", Country = "USA", Latitude = 41.411667M, Longitude = -81.849722M, Elevation = 791 };
            var atl = new Airport { AirportId = 2, AirportCode = "ATL", City = "Atlanta", State = "Georgia", Country = "USA", Latitude = 33.636667M, Longitude = -84.428056M, Elevation = 1027 };
            var lax = new Airport { AirportId = 3, AirportCode = "LAX", City = "Los Angeles", State = "California", Country = "USA", Latitude = 33.9425M, Longitude = -118.408056M, Elevation = 125 };
            var mdw = new Airport { AirportId = 4, AirportCode = "MDW", City = "Chicago", State = "Illinois", Country = "USA", Latitude = 41.786111M, Longitude = -87.7525M, Elevation = 620 };
            var dal = new Airport { AirportId = 5, AirportCode = "DAL", City = "Dallas", State = "Texas", Country = "USA", Latitude = 32.847222M, Longitude = -96.851667M, Elevation = 486 };
            var den = new Airport { AirportId = 6, AirportCode = "DEN", City = "Denver", State = "Colorado", Country = "USA", Latitude = 39.861667M, Longitude = -104.673056M, Elevation = 5430 };
            var lga = new Airport { AirportId = 7, AirportCode = "LGA", City = "New York", State = "New York", Country = "USA", Latitude = 40.775M, Longitude = -73.875M, Elevation = 19 };
            var mia = new Airport { AirportId = 8, AirportCode = "MIA", City = "Miami", State = "Florida", Country = "USA", Latitude = 25.793333M, Longitude = -80.290556M, Elevation = 9 };
            var sea = new Airport { AirportId = 9, AirportCode = "SEA", City = "Seattle", State = "Washington", Country = "USA", Latitude = 47.448889M, Longitude = -122.309444M, Elevation = 433 };
            var bna = new Airport { AirportId = 10, AirportCode = "BNA", City = "Nashville", State = "Tennessee", Country = "USA", Latitude = 36.126667M, Longitude = -86.681944M, Elevation = 599 };
            modelBuilder.Entity<Airport>().HasData(
                cle, atl, lax, mdw, dal, den, lga, mia, sea, bna
            );

            // Seeding Plane Model data
            // note: MaxDistance is in units of kilometers, to work properly with our Haversine Function
            // which results in a distance in meters, if we decide to adjust haversine we need to adjust these
            var p737max = new Plane { PlaneId = 1, Model = "Boeing 737 MAX", MaxSeats = 230, MaxDistance = 6570 };
            var p747 = new Plane { PlaneId = 2, Model = "Boeing 747", MaxSeats = 416, MaxDistance = 14815 };
            var p757 = new Plane { PlaneId = 3, Model = "Boeing 757", MaxSeats = 199, MaxDistance = 6241 };
            var p777 = new Plane { PlaneId = 4, Model = "Boeing 777", MaxSeats = 550, MaxDistance = 17395 };
            modelBuilder.Entity<Plane>().HasData(
                p737max, p747, p757, p777
            );

            // Seeding Staff Accounts
            // note: passwords are hashed with SHA-512
            // currently staff passwords are the exact same as their loginid
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, LoginId = "marketing_manager", HashedPass = "360caebd9edb68609c0933bade3565350e59e284cc503ce61bf0eebd42fb7e5bd657a71ed1498225168757e7f1095920411cce27779e0c778ec52535deae2040", UserRole = Role.MARKETING_MANAGER },
                new User { UserId = 2, LoginId = "load_engineer", HashedPass = "41ec260efa3aa054a91cc6cf9441e3652637f75946c8fa6c2e926f289e095f46e62e19a28e02fb0fc25dd047b1f04e6e03cf930d464b540c40c0045eb6b7e252", UserRole = Role.LOAD_ENGINEER },
                new User { UserId = 3, LoginId = "accounting_manager", HashedPass = "ab8f196b4521a3aba1de420fe6ef552ce406d8551c3aef370f909cea85abbc77ca1d698ef31f659097eee16e365975047ff403df1aae6cc7ef54595c3ae4d172", UserRole = Role.ACCOUNTING_MANAGER },
                new User { UserId = 4, LoginId = "flight_manager", HashedPass = "e2289f3f3a66a81f5ffb52dff1a09cd2ae91a39eb248230d2907084679c6bacdb4a880da7835ca72003d8c37e107cf91c5f9795678303754eba1be42039bff4d", UserRole = Role.FLIGHT_MANAGER }
            );

            // Seeding Flight Schedule
            // note: we must instantiate anonymous types here otherwise we run into errors when migrating
            // each new piece of the flight schedule should have the exact same fields as this first entry,
            // with FlightId and FlightNumber incrementing (maybe we remove FlightNumber in the future, not sure)
            // associated ids for airports and planes can be seen in seeding above or directly in the db

            // i'm not yet sure how we want to go about this but I think an easy thing to start with would be
            // filling out a basic flight schedule on a single day, then in code populating that exact same flight schedule
            // out for 6 months when the application is loaded (if the flights haven't been changed)
            // for now if you're adding more flights the easiest way is to probably just leave them all as
            // a departuretime and arrival time on 2021, 5, 7 for easy adjustment later if needed
            modelBuilder.Entity<Flight>().HasData(
                // SEA -> DEN
                new { FlightId = 1, OriginAirportId = 9, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(6, 10, 0), Cost = 75, TicketsPurchased = 75, FlightNumber = 1 },
                new { FlightId = 2, OriginAirportId = 9, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(9, 35, 0), Cost = 75, TicketsPurchased = 75, FlightNumber = 2 },
                new { FlightId = 3, OriginAirportId = 9, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(14, 10, 0), Cost = 75, TicketsPurchased = 75, FlightNumber = 3 },
                new { FlightId = 4, OriginAirportId = 9, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(18, 35, 0), Cost = 75, TicketsPurchased = 75, FlightNumber = 4 },
                // DEN -> SEA
                new { FlightId = 5, OriginAirportId = 6, DestinationAirportId = 9, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(6, 50, 0), Cost = 75, TicketsPurchased = 75, FlightNumber = 5 },
                new { FlightId = 6, OriginAirportId = 6, DestinationAirportId = 9, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(9, 30, 0), Cost = 75, TicketsPurchased = 75, FlightNumber = 6 },
                new { FlightId = 7, OriginAirportId = 6, DestinationAirportId = 9, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(15, 55, 0), Cost = 75, TicketsPurchased = 75, FlightNumber = 7 },
                new { FlightId = 8, OriginAirportId = 6, DestinationAirportId = 9, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(20, 55, 0), Cost = 75, TicketsPurchased = 75, FlightNumber = 8 },
                // LAX -> DEN
                new { FlightId = 9, OriginAirportId = 3, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(6, 5, 0), Cost = 75, TicketsPurchased = 75, FlightNumber = 9 },
                new { FlightId = 10, OriginAirportId = 3, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(11, 55, 0), Cost = 75, TicketsPurchased = 75, FlightNumber = 10 },
                new { FlightId = 11, OriginAirportId = 3, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(14, 25, 0), Cost = 75, TicketsPurchased = 75, FlightNumber = 11 },
                new { FlightId = 12, OriginAirportId = 3, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(17, 55, 0), Cost = 75, TicketsPurchased = 75, FlightNumber = 12 },
                // DEN -> LAX
                new { FlightId = 13, OriginAirportId = 6, DestinationAirportId = 3, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(8, 0, 0), Cost = 75, TicketsPurchased = 75, FlightNumber = 13 },
                new { FlightId = 14, OriginAirportId = 6, DestinationAirportId = 3, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(12, 55, 0), Cost = 75, TicketsPurchased = 75, FlightNumber = 14 },
                new { FlightId = 15, OriginAirportId = 6, DestinationAirportId = 3, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(17, 0, 0), Cost = 75, TicketsPurchased = 75, FlightNumber = 15 },
                new { FlightId = 16, OriginAirportId = 6, DestinationAirportId = 3, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(20, 35, 0), Cost = 75, TicketsPurchased = 75, FlightNumber = 16 },
                // LAX -> DAL
                new { FlightId = 17, OriginAirportId = 3, DestinationAirportId = 5, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(6, 10, 0), Cost = 75, TicketsPurchased = 75, FlightNumber = 17 },
                new { FlightId = 18, OriginAirportId = 3, DestinationAirportId = 5, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(11, 40, 0), Cost = 75, TicketsPurchased = 75, FlightNumber = 18 },
                new { FlightId = 19, OriginAirportId = 3, DestinationAirportId = 5, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(14, 50, 0), Cost = 75, TicketsPurchased = 75, FlightNumber = 19 },
                new { FlightId = 20, OriginAirportId = 3, DestinationAirportId = 5, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(18, 10, 0), Cost = 75, TicketsPurchased = 75, FlightNumber = 20 },
                // DAL -> LAX
                new { FlightId = 21, OriginAirportId = 5, DestinationAirportId = 3, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(7, 30, 0), Cost = 75, TicketsPurchased = 75, FlightNumber = 21 },
                new { FlightId = 22, OriginAirportId = 5, DestinationAirportId = 3, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(9, 45, 0), Cost = 75, TicketsPurchased = 75, FlightNumber = 22 },
                new { FlightId = 23, OriginAirportId = 5, DestinationAirportId = 3, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(14, 10, 0), Cost = 75, TicketsPurchased = 75, FlightNumber = 23 },
                new { FlightId = 24, OriginAirportId = 5, DestinationAirportId = 3, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(20, 50, 0), Cost = 75, TicketsPurchased = 75, FlightNumber = 24 }
            );
        }
    }
}
