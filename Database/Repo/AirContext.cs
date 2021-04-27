using Air_3550.Models;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace Air_3550.Repo
{
    public class AirContext : DbContext
    {
        // Sets up database tables for the various parts of our schema
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<ScheduledFlight> ScheduledFlights { get; set; }
        public DbSet<Trip> Trips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            Console.WriteLine("Entered OnConfiguring");
            // Configures the database path to be stored in the users appdata
            var appDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            // makes a new directory in the appdata called "Air 3550 Team 11"
            var airDir = Path.Combine(appDataDirectory, "Air 3550 Team 11");
            Directory.CreateDirectory(airDir);
            // defines the database path as the app data directory/Air 3550 Team 11/air3550.db
            var dbPath = Path.Combine(airDir, "air3550.db");
            // sets up EFCore to use this database with Sqlite
            options.UseSqlite(@"Data Source=" + dbPath);
            options.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Console.WriteLine("Entered OnModelCreating");

            // Seeding airport data
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
            var p737max = new Plane { PlaneId = 1, Model = "Boeing 737 MAX", MaxSeats = 230 };
            var p747 = new Plane { PlaneId = 2, Model = "Boeing 747", MaxSeats = 416 };
            var p757 = new Plane { PlaneId = 3, Model = "Boeing 757", MaxSeats = 199 };
            var p777 = new Plane { PlaneId = 4, Model = "Boeing 777", MaxSeats = 550 };
            modelBuilder.Entity<Plane>().HasData(
                p737max, p747, p757, p777
            );

            // Seeding Staff Accounts
            // note: passwords are hashed with SHA-512
            // staff passwords are the exact same as their loginid
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
            modelBuilder.Entity<Flight>().HasData(
                // SEA -> DEN
                new { FlightId = 5, OriginAirportId = 9, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(6, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 5, IsCanceled = false },
                new { FlightId = 10, OriginAirportId = 9, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(7, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 10, IsCanceled = false },
                new { FlightId = 15, OriginAirportId = 9, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(8, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 15, IsCanceled = false },
                new { FlightId = 20, OriginAirportId = 9, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(9, 35, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 20, IsCanceled = false },
                new { FlightId = 25, OriginAirportId = 9, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(10, 15, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 25, IsCanceled = false },
                new { FlightId = 30, OriginAirportId = 9, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(11, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 30, IsCanceled = false },
                new { FlightId = 35, OriginAirportId = 9, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(12, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 35, IsCanceled = false },
                new { FlightId = 40, OriginAirportId = 9, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(14, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 40, IsCanceled = false },
                new { FlightId = 45, OriginAirportId = 9, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(15, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 45, IsCanceled = false },
                new { FlightId = 50, OriginAirportId = 9, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(18, 35, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 50, IsCanceled = false },
                // DEN -> SEA
                new { FlightId = 55, OriginAirportId = 6, DestinationAirportId = 9, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(6, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 55, IsCanceled = false },
                new { FlightId = 60, OriginAirportId = 6, DestinationAirportId = 9, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(7, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 60, IsCanceled = false },
                new { FlightId = 65, OriginAirportId = 6, DestinationAirportId = 9, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(8, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 65, IsCanceled = false },
                new { FlightId = 70, OriginAirportId = 6, DestinationAirportId = 9, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(9, 55, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 70, IsCanceled = false },
                new { FlightId = 75, OriginAirportId = 6, DestinationAirportId = 9, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(10, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 75, IsCanceled = false },
                new { FlightId = 80, OriginAirportId = 6, DestinationAirportId = 9, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(11, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 80, IsCanceled = false },
                new { FlightId = 85, OriginAirportId = 6, DestinationAirportId = 9, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(12, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 85, IsCanceled = false },
                new { FlightId = 90, OriginAirportId = 6, DestinationAirportId = 9, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(13, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 90, IsCanceled = false },
                new { FlightId = 95, OriginAirportId = 6, DestinationAirportId = 9, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(15, 05, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 95, IsCanceled = false },
                new { FlightId = 100, OriginAirportId = 6, DestinationAirportId = 9, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(20, 55, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 100, IsCanceled = false },
                // LAX -> DEN
                new { FlightId = 105, OriginAirportId = 3, DestinationAirportId = 6, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(6, 5, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 105, IsCanceled = false },
                new { FlightId = 110, OriginAirportId = 3, DestinationAirportId = 6, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(7, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 110, IsCanceled = false },
                new { FlightId = 115, OriginAirportId = 3, DestinationAirportId = 6, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(8, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 115, IsCanceled = false },
                new { FlightId = 120, OriginAirportId = 3, DestinationAirportId = 6, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(9, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 120, IsCanceled = false },
                new { FlightId = 125, OriginAirportId = 3, DestinationAirportId = 6, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(10, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 125, IsCanceled = false },
                new { FlightId = 130, OriginAirportId = 3, DestinationAirportId = 6, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(11, 55, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 130, IsCanceled = false },
                new { FlightId = 135, OriginAirportId = 3, DestinationAirportId = 6, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(13, 5, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 135, IsCanceled = false },
                new { FlightId = 140, OriginAirportId = 3, DestinationAirportId = 6, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(14, 25, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 140, IsCanceled = false },
                new { FlightId = 145, OriginAirportId = 3, DestinationAirportId = 6, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(17, 55, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 145, IsCanceled = false },
                new { FlightId = 150, OriginAirportId = 3, DestinationAirportId = 6, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(21, 5, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 150, IsCanceled = false },
                // DEN -> LAX
                new { FlightId = 155, OriginAirportId = 6, DestinationAirportId = 3, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(8, 0, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 155, IsCanceled = false },
                new { FlightId = 160, OriginAirportId = 6, DestinationAirportId = 3, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(9, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 160, IsCanceled = false },
                new { FlightId = 165, OriginAirportId = 6, DestinationAirportId = 3, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(10, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 165, IsCanceled = false },
                new { FlightId = 170, OriginAirportId = 6, DestinationAirportId = 3, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(11, 05, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 170, IsCanceled = false },
                new { FlightId = 175, OriginAirportId = 6, DestinationAirportId = 3, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(12, 55, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 175, IsCanceled = false },
                new { FlightId = 180, OriginAirportId = 6, DestinationAirportId = 3, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(13, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 180, IsCanceled = false },
                new { FlightId = 185, OriginAirportId = 6, DestinationAirportId = 3, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(14, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 185, IsCanceled = false },
                new { FlightId = 190, OriginAirportId = 6, DestinationAirportId = 3, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(17, 0, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 190, IsCanceled = false },
                new { FlightId = 195, OriginAirportId = 6, DestinationAirportId = 3, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(19, 05, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 195, IsCanceled = false },
                new { FlightId = 200, OriginAirportId = 6, DestinationAirportId = 3, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(20, 35, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 200, IsCanceled = false },
                // LAX -> DAL
                new { FlightId = 205, OriginAirportId = 3, DestinationAirportId = 5, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(6, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 205, IsCanceled = false },
                new { FlightId = 210, OriginAirportId = 3, DestinationAirportId = 5, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(7, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 210, IsCanceled = false },
                new { FlightId = 215, OriginAirportId = 3, DestinationAirportId = 5, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(8, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 215, IsCanceled = false },
                new { FlightId = 220, OriginAirportId = 3, DestinationAirportId = 5, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(9, 05, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 220, IsCanceled = false },
                new { FlightId = 225, OriginAirportId = 3, DestinationAirportId = 5, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(10, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 225, IsCanceled = false },
                new { FlightId = 230, OriginAirportId = 3, DestinationAirportId = 5, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(11, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 230, IsCanceled = false },
                new { FlightId = 235, OriginAirportId = 3, DestinationAirportId = 5, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(12, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 235, IsCanceled = false },
                new { FlightId = 240, OriginAirportId = 3, DestinationAirportId = 5, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(14, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 240, IsCanceled = false },
                new { FlightId = 245, OriginAirportId = 3, DestinationAirportId = 5, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(18, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 245, IsCanceled = false },
                new { FlightId = 250, OriginAirportId = 3, DestinationAirportId = 5, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(20, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 250, IsCanceled = false },
                // DAL -> LAX
                new { FlightId = 255, OriginAirportId = 5, DestinationAirportId = 3, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(7, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 255, IsCanceled = false },
                new { FlightId = 260, OriginAirportId = 5, DestinationAirportId = 3, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(8, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 260, IsCanceled = false },
                new { FlightId = 265, OriginAirportId = 5, DestinationAirportId = 3, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(9, 45, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 265, IsCanceled = false },
                new { FlightId = 270, OriginAirportId = 5, DestinationAirportId = 3, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(10, 05, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 270, IsCanceled = false },
                new { FlightId = 275, OriginAirportId = 5, DestinationAirportId = 3, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(11, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 275, IsCanceled = false },
                new { FlightId = 280, OriginAirportId = 5, DestinationAirportId = 3, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(12, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 280, IsCanceled = false },
                new { FlightId = 285, OriginAirportId = 5, DestinationAirportId = 3, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(14, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 285, IsCanceled = false },
                new { FlightId = 290, OriginAirportId = 5, DestinationAirportId = 3, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(15, 55, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 290, IsCanceled = false },
                new { FlightId = 295, OriginAirportId = 5, DestinationAirportId = 3, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(20, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 295, IsCanceled = false },
                new { FlightId = 300, OriginAirportId = 5, DestinationAirportId = 3, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(23, 05, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 300, IsCanceled = false },
                // DEN -> DAL
                new { FlightId = 305, OriginAirportId = 6, DestinationAirportId = 5, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(7, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 305, IsCanceled = false },
                new { FlightId = 310, OriginAirportId = 6, DestinationAirportId = 5, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(8, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 310, IsCanceled = false },
                new { FlightId = 315, OriginAirportId = 6, DestinationAirportId = 5, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(9, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 315, IsCanceled = false },
                new { FlightId = 320, OriginAirportId = 6, DestinationAirportId = 5, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(10, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 320, IsCanceled = false },
                new { FlightId = 325, OriginAirportId = 6, DestinationAirportId = 5, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(11, 55, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 325, IsCanceled = false },
                new { FlightId = 330, OriginAirportId = 6, DestinationAirportId = 5, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(12, 05, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 330, IsCanceled = false },
                new { FlightId = 335, OriginAirportId = 6, DestinationAirportId = 5, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(14, 05, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 335, IsCanceled = false },
                new { FlightId = 340, OriginAirportId = 6, DestinationAirportId = 5, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(15, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 340, IsCanceled = false },
                new { FlightId = 345, OriginAirportId = 6, DestinationAirportId = 5, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(18, 55, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 345, IsCanceled = false },
                new { FlightId = 350, OriginAirportId = 6, DestinationAirportId = 5, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(21, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 350, IsCanceled = false },
                // DAL -> DEN
                new { FlightId = 355, OriginAirportId = 5, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(7, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 355, IsCanceled = false },
                new { FlightId = 360, OriginAirportId = 5, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(8, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 360, IsCanceled = false },
                new { FlightId = 365, OriginAirportId = 5, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(9, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 365, IsCanceled = false },
                new { FlightId = 370, OriginAirportId = 5, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(10, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 370, IsCanceled = false },
                new { FlightId = 375, OriginAirportId = 5, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(11, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 375, IsCanceled = false },
                new { FlightId = 380, OriginAirportId = 5, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(12, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 380, IsCanceled = false },
                new { FlightId = 385, OriginAirportId = 5, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(13, 05, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 385, IsCanceled = false },
                new { FlightId = 390, OriginAirportId = 5, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(16, 45, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 390, IsCanceled = false },
                new { FlightId = 395, OriginAirportId = 5, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(17, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 395, IsCanceled = false },
                new { FlightId = 400, OriginAirportId = 5, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(20, 55, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 400, IsCanceled = false },
                // DEN -> MDW
                new { FlightId = 405, OriginAirportId = 6, DestinationAirportId = 4, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(6, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 405, IsCanceled = false },
                new { FlightId = 410, OriginAirportId = 6, DestinationAirportId = 4, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(8, 25, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 410, IsCanceled = false },
                new { FlightId = 415, OriginAirportId = 6, DestinationAirportId = 4, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(9, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 415, IsCanceled = false },
                new { FlightId = 420, OriginAirportId = 6, DestinationAirportId = 4, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(10, 55, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 420, IsCanceled = false },
                new { FlightId = 425, OriginAirportId = 6, DestinationAirportId = 4, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(11, 25, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 425, IsCanceled = false },
                new { FlightId = 430, OriginAirportId = 6, DestinationAirportId = 4, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(12, 00, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 430, IsCanceled = false },
                new { FlightId = 435, OriginAirportId = 6, DestinationAirportId = 4, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(13, 05, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 435, IsCanceled = false },
                new { FlightId = 440, OriginAirportId = 6, DestinationAirportId = 4, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(14, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 440, IsCanceled = false },
                new { FlightId = 445, OriginAirportId = 6, DestinationAirportId = 4, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(16, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 445, IsCanceled = false },
                new { FlightId = 450, OriginAirportId = 6, DestinationAirportId = 4, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(18, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 450, IsCanceled = false },
                // MDW -> DEN
                new { FlightId = 455, OriginAirportId = 4, DestinationAirportId = 6, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(6, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 455, IsCanceled = false },
                new { FlightId = 460, OriginAirportId = 4, DestinationAirportId = 6, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(7, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 460, IsCanceled = false },
                new { FlightId = 465, OriginAirportId = 4, DestinationAirportId = 6, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(8, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 465, IsCanceled = false },
                new { FlightId = 470, OriginAirportId = 4, DestinationAirportId = 6, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(9, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 470, IsCanceled = false },
                new { FlightId = 475, OriginAirportId = 4, DestinationAirportId = 6, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(10, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 475, IsCanceled = false },
                new { FlightId = 480, OriginAirportId = 4, DestinationAirportId = 6, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(11, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 480, IsCanceled = false },
                new { FlightId = 485, OriginAirportId = 4, DestinationAirportId = 6, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(12, 45, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 485, IsCanceled = false },
                new { FlightId = 490, OriginAirportId = 4, DestinationAirportId = 6, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(14, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 490, IsCanceled = false },
                new { FlightId = 495, OriginAirportId = 4, DestinationAirportId = 6, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(16, 45, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 495, IsCanceled = false },
                new { FlightId = 500, OriginAirportId = 4, DestinationAirportId = 6, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(18, 00, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 500, IsCanceled = false },
                // DEN -> BNA
                new { FlightId = 505, OriginAirportId = 6, DestinationAirportId = 10, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(6, 45, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 505, IsCanceled = false },
                new { FlightId = 510, OriginAirportId = 6, DestinationAirportId = 10, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(7, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 510, IsCanceled = false },
                new { FlightId = 515, OriginAirportId = 6, DestinationAirportId = 10, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(8, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 515, IsCanceled = false },
                new { FlightId = 520, OriginAirportId = 6, DestinationAirportId = 10, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(9, 15, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 520, IsCanceled = false },
                new { FlightId = 525, OriginAirportId = 6, DestinationAirportId = 10, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(10, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 525, IsCanceled = false },
                new { FlightId = 530, OriginAirportId = 6, DestinationAirportId = 10, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(11, 45, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 530, IsCanceled = false },
                new { FlightId = 535, OriginAirportId = 6, DestinationAirportId = 10, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(12, 05, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 535, IsCanceled = false },
                new { FlightId = 540, OriginAirportId = 6, DestinationAirportId = 10, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(13, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 540, IsCanceled = false },
                new { FlightId = 545, OriginAirportId = 6, DestinationAirportId = 10, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(15, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 545, IsCanceled = false },
                new { FlightId = 550, OriginAirportId = 6, DestinationAirportId = 10, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(18, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 550, IsCanceled = false },
                // BNA -> DEN
                new { FlightId = 555, OriginAirportId = 10, DestinationAirportId = 6, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(6, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 555, IsCanceled = false },
                new { FlightId = 560, OriginAirportId = 10, DestinationAirportId = 6, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(7, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 560, IsCanceled = false },
                new { FlightId = 565, OriginAirportId = 10, DestinationAirportId = 6, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(8, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 565, IsCanceled = false },
                new { FlightId = 570, OriginAirportId = 10, DestinationAirportId = 6, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(9, 05, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 570, IsCanceled = false },
                new { FlightId = 575, OriginAirportId = 10, DestinationAirportId = 6, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(10, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 575, IsCanceled = false },
                new { FlightId = 580, OriginAirportId = 10, DestinationAirportId = 6, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(11, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 580, IsCanceled = false },
                new { FlightId = 585, OriginAirportId = 10, DestinationAirportId = 6, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(12, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 585, IsCanceled = false },
                new { FlightId = 590, OriginAirportId = 10, DestinationAirportId = 6, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(15, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 590, IsCanceled = false },
                new { FlightId = 595, OriginAirportId = 10, DestinationAirportId = 6, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(17, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 595, IsCanceled = false },
                new { FlightId = 600, OriginAirportId = 10, DestinationAirportId = 6, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(19, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 600, IsCanceled = false },
                // DEN -> ATL
                new { FlightId = 605, OriginAirportId = 6, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(5, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 605, IsCanceled = false },
                new { FlightId = 610, OriginAirportId = 6, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(7, 0, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 610, IsCanceled = false },
                new { FlightId = 615, OriginAirportId = 6, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(8, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 615, IsCanceled = false },
                new { FlightId = 620, OriginAirportId = 6, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(9, 15, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 620, IsCanceled = false },
                new { FlightId = 625, OriginAirportId = 6, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(10, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 625, IsCanceled = false },
                new { FlightId = 630, OriginAirportId = 6, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(11, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 630, IsCanceled = false },
                new { FlightId = 635, OriginAirportId = 6, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(12, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 635, IsCanceled = false },
                new { FlightId = 640, OriginAirportId = 6, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(14, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 640, IsCanceled = false },
                new { FlightId = 645, OriginAirportId = 6, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(15, 45, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 645, IsCanceled = false },
                new { FlightId = 650, OriginAirportId = 6, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(18, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 650, IsCanceled = false },
                // ATL -> DEN
                new { FlightId = 655, OriginAirportId = 2, DestinationAirportId = 6, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(5, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 655, IsCanceled = false },
                new { FlightId = 660, OriginAirportId = 2, DestinationAirportId = 6, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(7, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 660, IsCanceled = false },
                new { FlightId = 665, OriginAirportId = 2, DestinationAirportId = 6, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(8, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 665, IsCanceled = false },
                new { FlightId = 670, OriginAirportId = 2, DestinationAirportId = 6, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(9, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 670, IsCanceled = false },
                new { FlightId = 675, OriginAirportId = 2, DestinationAirportId = 6, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(10, 05, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 675, IsCanceled = false },
                new { FlightId = 680, OriginAirportId = 2, DestinationAirportId = 6, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(11, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 680, IsCanceled = false },
                new { FlightId = 685, OriginAirportId = 2, DestinationAirportId = 6, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(14, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 685, IsCanceled = false },
                new { FlightId = 690, OriginAirportId = 2, DestinationAirportId = 6, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(15, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 690, IsCanceled = false },
                new { FlightId = 695, OriginAirportId = 2, DestinationAirportId = 6, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(18, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 695, IsCanceled = false },
                new { FlightId = 700, OriginAirportId = 2, DestinationAirportId = 6, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(20, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 700, IsCanceled = false },
                // MDW -> CLE
                new { FlightId = 705, OriginAirportId = 4, DestinationAirportId = 1, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(5, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 705, IsCanceled = false },
                new { FlightId = 710, OriginAirportId = 4, DestinationAirportId = 1, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(7, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 710, IsCanceled = false },
                new { FlightId = 715, OriginAirportId = 4, DestinationAirportId = 1, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(8, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 715, IsCanceled = false },
                new { FlightId = 720, OriginAirportId = 4, DestinationAirportId = 1, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(9, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 720, IsCanceled = false },
                new { FlightId = 725, OriginAirportId = 4, DestinationAirportId = 1, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(10, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 725, IsCanceled = false },
                new { FlightId = 730, OriginAirportId = 4, DestinationAirportId = 1, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(11, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 730, IsCanceled = false },
                new { FlightId = 735, OriginAirportId = 4, DestinationAirportId = 1, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(12, 45, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 735, IsCanceled = false },
                new { FlightId = 740, OriginAirportId = 4, DestinationAirportId = 1, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(14, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 740, IsCanceled = false },
                new { FlightId = 745, OriginAirportId = 4, DestinationAirportId = 1, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(16, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 745, IsCanceled = false },
                new { FlightId = 750, OriginAirportId = 4, DestinationAirportId = 1, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(18, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 750, IsCanceled = false },
                // CLE -> MDW
                new { FlightId = 755, OriginAirportId = 1, DestinationAirportId = 4, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(5, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 755, IsCanceled = false },
                new { FlightId = 760, OriginAirportId = 1, DestinationAirportId = 4, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(7, 0, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 760, IsCanceled = false },
                new { FlightId = 765, OriginAirportId = 1, DestinationAirportId = 4, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(8, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 765, IsCanceled = false },
                new { FlightId = 770, OriginAirportId = 1, DestinationAirportId = 4, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(9, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 770, IsCanceled = false },
                new { FlightId = 775, OriginAirportId = 1, DestinationAirportId = 4, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(10, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 775, IsCanceled = false },
                new { FlightId = 780, OriginAirportId = 1, DestinationAirportId = 4, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(11, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 780, IsCanceled = false },
                new { FlightId = 785, OriginAirportId = 1, DestinationAirportId = 4, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(12, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 785, IsCanceled = false },
                new { FlightId = 790, OriginAirportId = 1, DestinationAirportId = 4, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(15, 35, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 790, IsCanceled = false },
                new { FlightId = 795, OriginAirportId = 1, DestinationAirportId = 4, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(18, 00, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 795, IsCanceled = false },
                new { FlightId = 800, OriginAirportId = 1, DestinationAirportId = 4, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(19, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 800, IsCanceled = false },
                // MDW -> ATL
                new { FlightId = 805, OriginAirportId = 4, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(5, 45, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 805, IsCanceled = false },
                new { FlightId = 810, OriginAirportId = 4, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(7, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 810, IsCanceled = false },
                new { FlightId = 815, OriginAirportId = 4, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(9, 00, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 815, IsCanceled = false },
                new { FlightId = 820, OriginAirportId = 4, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(10, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 820, IsCanceled = false },
                new { FlightId = 825, OriginAirportId = 4, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(11, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 825, IsCanceled = false },
                new { FlightId = 830, OriginAirportId = 4, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(12, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 830, IsCanceled = false },
                new { FlightId = 835, OriginAirportId = 4, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(13, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 835, IsCanceled = false },
                new { FlightId = 840, OriginAirportId = 4, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(15, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 840, IsCanceled = false },
                new { FlightId = 845, OriginAirportId = 4, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(17, 35, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 845, IsCanceled = false },
                new { FlightId = 850, OriginAirportId = 4, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(19, 00, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 850, IsCanceled = false },
                // ATL -> MDW
                new { FlightId = 855, OriginAirportId = 2, DestinationAirportId = 4, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(5, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 855, IsCanceled = false },
                new { FlightId = 860, OriginAirportId = 2, DestinationAirportId = 4, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(6, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 860, IsCanceled = false },
                new { FlightId = 865, OriginAirportId = 2, DestinationAirportId = 4, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(7, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 865, IsCanceled = false },
                new { FlightId = 870, OriginAirportId = 2, DestinationAirportId = 4, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(8, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 870, IsCanceled = false },
                new { FlightId = 875, OriginAirportId = 2, DestinationAirportId = 4, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(9, 35, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 875, IsCanceled = false },
                new { FlightId = 880, OriginAirportId = 2, DestinationAirportId = 4, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(10, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 880, IsCanceled = false },
                new { FlightId = 885, OriginAirportId = 2, DestinationAirportId = 4, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(11, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 885, IsCanceled = false },
                new { FlightId = 890, OriginAirportId = 2, DestinationAirportId = 4, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(12, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 890, IsCanceled = false },
                new { FlightId = 895, OriginAirportId = 2, DestinationAirportId = 4, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(14, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 895, IsCanceled = false },
                new { FlightId = 900, OriginAirportId = 2, DestinationAirportId = 4, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(18, 25, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 900, IsCanceled = false },
                // BNA -> CLE
                new { FlightId = 905, OriginAirportId = 10, DestinationAirportId = 1, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(6, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 905, IsCanceled = false },
                new { FlightId = 910, OriginAirportId = 10, DestinationAirportId = 1, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(7, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 910, IsCanceled = false },
                new { FlightId = 915, OriginAirportId = 10, DestinationAirportId = 1, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(8, 45, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 915, IsCanceled = false },
                new { FlightId = 920, OriginAirportId = 10, DestinationAirportId = 1, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(9, 05, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 920, IsCanceled = false },
                new { FlightId = 925, OriginAirportId = 10, DestinationAirportId = 1, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(10, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 925, IsCanceled = false },
                new { FlightId = 930, OriginAirportId = 10, DestinationAirportId = 1, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(11, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 930, IsCanceled = false },
                new { FlightId = 935, OriginAirportId = 10, DestinationAirportId = 1, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(13, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 935, IsCanceled = false },
                new { FlightId = 940, OriginAirportId = 10, DestinationAirportId = 1, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(14, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 940, IsCanceled = false },
                new { FlightId = 945, OriginAirportId = 10, DestinationAirportId = 1, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(16, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 945, IsCanceled = false },
                new { FlightId = 950, OriginAirportId = 10, DestinationAirportId = 1, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(18, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 950, IsCanceled = false },
                // CLE -> BNA
                new { FlightId = 955, OriginAirportId = 1, DestinationAirportId = 10, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(7, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 955, IsCanceled = false },
                new { FlightId = 960, OriginAirportId = 1, DestinationAirportId = 10, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(8, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 960, IsCanceled = false },
                new { FlightId = 965, OriginAirportId = 1, DestinationAirportId = 10, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(9, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 965, IsCanceled = false },
                new { FlightId = 970, OriginAirportId = 1, DestinationAirportId = 10, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(10, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 970, IsCanceled = false },
                new { FlightId = 975, OriginAirportId = 1, DestinationAirportId = 10, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(11, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 975, IsCanceled = false },
                new { FlightId = 980, OriginAirportId = 1, DestinationAirportId = 10, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(12, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 980, IsCanceled = false },
                new { FlightId = 985, OriginAirportId = 1, DestinationAirportId = 10, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(14, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 985, IsCanceled = false },
                new { FlightId = 990, OriginAirportId = 1, DestinationAirportId = 10, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(15, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 990, IsCanceled = false },
                new { FlightId = 995, OriginAirportId = 1, DestinationAirportId = 10, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(19, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 995, IsCanceled = false },
                new { FlightId = 1000, OriginAirportId = 1, DestinationAirportId = 10, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(21, 00, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1000, IsCanceled = false },
                // BNA -> ATL
                new { FlightId = 1005, OriginAirportId = 10, DestinationAirportId = 2, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(5, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1005, IsCanceled = false },
                new { FlightId = 1010, OriginAirportId = 10, DestinationAirportId = 2, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(6, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1010, IsCanceled = false },
                new { FlightId = 1015, OriginAirportId = 10, DestinationAirportId = 2, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(8, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1015, IsCanceled = false },
                new { FlightId = 1020, OriginAirportId = 10, DestinationAirportId = 2, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(9, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1020, IsCanceled = false },
                new { FlightId = 1025, OriginAirportId = 10, DestinationAirportId = 2, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(10, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1025, IsCanceled = false },
                new { FlightId = 1030, OriginAirportId = 10, DestinationAirportId = 2, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(11, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1030, IsCanceled = false },
                new { FlightId = 1035, OriginAirportId = 10, DestinationAirportId = 2, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(12, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1035, IsCanceled = false },
                new { FlightId = 1040, OriginAirportId = 10, DestinationAirportId = 2, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(13, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1040, IsCanceled = false },
                new { FlightId = 1045, OriginAirportId = 10, DestinationAirportId = 2, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(14, 35, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1045, IsCanceled = false },
                new { FlightId = 1050, OriginAirportId = 10, DestinationAirportId = 2, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(17, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1050, IsCanceled = false },
                // ATL -> BNA
                new { FlightId = 1055, OriginAirportId = 2, DestinationAirportId = 10, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(5, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1055, IsCanceled = false },
                new { FlightId = 1060, OriginAirportId = 2, DestinationAirportId = 10, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(6, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1060, IsCanceled = false },
                new { FlightId = 1065, OriginAirportId = 2, DestinationAirportId = 10, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(8, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1065, IsCanceled = false },
                new { FlightId = 1070, OriginAirportId = 2, DestinationAirportId = 10, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(9, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1070, IsCanceled = false },
                new { FlightId = 1075, OriginAirportId = 2, DestinationAirportId = 10, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(10, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1075, IsCanceled = false },
                new { FlightId = 1080, OriginAirportId = 2, DestinationAirportId = 10, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(11, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1080, IsCanceled = false },
                new { FlightId = 1085, OriginAirportId = 2, DestinationAirportId = 10, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(12, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1085, IsCanceled = false },
                new { FlightId = 1090, OriginAirportId = 2, DestinationAirportId = 10, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(15, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1090, IsCanceled = false },
                new { FlightId = 1095, OriginAirportId = 2, DestinationAirportId = 10, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(18, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1095, IsCanceled = false },
                new { FlightId = 1100, OriginAirportId = 2, DestinationAirportId = 10, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(20, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1100, IsCanceled = false },
                // CLE -> ATL
                new { FlightId = 1105, OriginAirportId = 1, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(5, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1105, IsCanceled = false },
                new { FlightId = 1110, OriginAirportId = 1, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(7, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1110, IsCanceled = false },
                new { FlightId = 1115, OriginAirportId = 1, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(8, 00, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1115, IsCanceled = false },
                new { FlightId = 1120, OriginAirportId = 1, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(9, 45, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1120, IsCanceled = false },
                new { FlightId = 1125, OriginAirportId = 1, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(10, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1125, IsCanceled = false },
                new { FlightId = 1130, OriginAirportId = 1, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(11, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1130, IsCanceled = false },
                new { FlightId = 1135, OriginAirportId = 1, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(13, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1135, IsCanceled = false },
                new { FlightId = 1140, OriginAirportId = 1, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(15, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1140, IsCanceled = false },
                new { FlightId = 1145, OriginAirportId = 1, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(19, 0, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1145, IsCanceled = false },
                new { FlightId = 1150, OriginAirportId = 1, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(21, 00, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1150, IsCanceled = false },
                // ATL -> CLE
                new { FlightId = 1155, OriginAirportId = 2, DestinationAirportId = 1, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(5, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1155, IsCanceled = false },
                new { FlightId = 1160, OriginAirportId = 2, DestinationAirportId = 1, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(7, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1160, IsCanceled = false },
                new { FlightId = 1165, OriginAirportId = 2, DestinationAirportId = 1, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(8, 05, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1165, IsCanceled = false },
                new { FlightId = 1170, OriginAirportId = 2, DestinationAirportId = 1, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(9, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1170, IsCanceled = false },
                new { FlightId = 1175, OriginAirportId = 2, DestinationAirportId = 1, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(10, 45, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1175, IsCanceled = false },
                new { FlightId = 1180, OriginAirportId = 2, DestinationAirportId = 1, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(11, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1180, IsCanceled = false },
                new { FlightId = 1185, OriginAirportId = 2, DestinationAirportId = 1, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(12, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1185, IsCanceled = false },
                new { FlightId = 1190, OriginAirportId = 2, DestinationAirportId = 1, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(14, 05, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1190, IsCanceled = false },
                new { FlightId = 1195, OriginAirportId = 2, DestinationAirportId = 1, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(16, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1195, IsCanceled = false },
                new { FlightId = 1200, OriginAirportId = 2, DestinationAirportId = 1, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(18, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1200, IsCanceled = false },
                // ATL -> LGA
                new { FlightId = 1205, OriginAirportId = 2, DestinationAirportId = 7, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(6, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1205, IsCanceled = false },
                new { FlightId = 1210, OriginAirportId = 2, DestinationAirportId = 7, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(7, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1210, IsCanceled = false },
                new { FlightId = 1215, OriginAirportId = 2, DestinationAirportId = 7, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(9, 0, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1215, IsCanceled = false },
                new { FlightId = 1220, OriginAirportId = 2, DestinationAirportId = 7, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(10, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1220, IsCanceled = false },
                new { FlightId = 1225, OriginAirportId = 2, DestinationAirportId = 7, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(11, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1225, IsCanceled = false },
                new { FlightId = 1230, OriginAirportId = 2, DestinationAirportId = 7, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(12, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1230, IsCanceled = false },
                new { FlightId = 1235, OriginAirportId = 2, DestinationAirportId = 7, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(13, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1235, IsCanceled = false },
                new { FlightId = 1240, OriginAirportId = 2, DestinationAirportId = 7, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(14, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1240, IsCanceled = false },
                new { FlightId = 1245, OriginAirportId = 2, DestinationAirportId = 7, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(16, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1245, IsCanceled = false },
                new { FlightId = 1250, OriginAirportId = 2, DestinationAirportId = 7, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(18, 55, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1250, IsCanceled = false },
                // LGA -> ATL
                new { FlightId = 1255, OriginAirportId = 7, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(5, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1255, IsCanceled = false },
                new { FlightId = 1260, OriginAirportId = 7, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(6, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1260, IsCanceled = false },
                new { FlightId = 1265, OriginAirportId = 7, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(7, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1265, IsCanceled = false },
                new { FlightId = 1270, OriginAirportId = 7, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(9, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1270, IsCanceled = false },
                new { FlightId = 1275, OriginAirportId = 7, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(10, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1275, IsCanceled = false },
                new { FlightId = 1280, OriginAirportId = 7, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(11, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1280, IsCanceled = false },
                new { FlightId = 1285, OriginAirportId = 7, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(12, 05, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1285, IsCanceled = false },
                new { FlightId = 1290, OriginAirportId = 7, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(14, 00, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1290, IsCanceled = false },
                new { FlightId = 1295, OriginAirportId = 7, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(17, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1295, IsCanceled = false },
                new { FlightId = 1300, OriginAirportId = 7, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(19, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1300, IsCanceled = false },
                // ATL -> MIA
                new { FlightId = 1305, OriginAirportId = 2, DestinationAirportId = 8, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(5, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1305, IsCanceled = false },
                new { FlightId = 1310, OriginAirportId = 2, DestinationAirportId = 8, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(6, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1310, IsCanceled = false },
                new { FlightId = 1315, OriginAirportId = 2, DestinationAirportId = 8, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(7, 45, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1315, IsCanceled = false },
                new { FlightId = 1320, OriginAirportId = 2, DestinationAirportId = 8, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(9, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1320, IsCanceled = false },
                new { FlightId = 1325, OriginAirportId = 2, DestinationAirportId = 8, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(10, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1325, IsCanceled = false },
                new { FlightId = 1330, OriginAirportId = 2, DestinationAirportId = 8, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(11, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1330, IsCanceled = false },
                new { FlightId = 1335, OriginAirportId = 2, DestinationAirportId = 8, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(13, 45, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1335, IsCanceled = false },
                new { FlightId = 1340, OriginAirportId = 2, DestinationAirportId = 8, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(15, 25, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1340, IsCanceled = false },
                new { FlightId = 1345, OriginAirportId = 2, DestinationAirportId = 8, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(18, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1345, IsCanceled = false },
                new { FlightId = 1350, OriginAirportId = 2, DestinationAirportId = 8, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(20, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1350, IsCanceled = false },
                // MIA -> ATL
                new { FlightId = 1355, OriginAirportId = 8, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(6, 00, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1355, IsCanceled = false },
                new { FlightId = 1360, OriginAirportId = 8, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(7, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1360, IsCanceled = false },
                new { FlightId = 1365, OriginAirportId = 8, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(8, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1365, IsCanceled = false },
                new { FlightId = 1370, OriginAirportId = 8, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(9, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1370, IsCanceled = false },
                new { FlightId = 1375, OriginAirportId = 8, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(11, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1375, IsCanceled = false },
                new { FlightId = 1380, OriginAirportId = 8, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(12, 05, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1380, IsCanceled = false },
                new { FlightId = 1385, OriginAirportId = 8, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(13, 45, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1385, IsCanceled = false },
                new { FlightId = 1390, OriginAirportId = 8, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(15, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1390, IsCanceled = false },
                new { FlightId = 1395, OriginAirportId = 8, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(17, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1395, IsCanceled = false },
                new { FlightId = 1400, OriginAirportId = 8, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(19, 55, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1400, IsCanceled = false },
                // ATL -> DAL
                new { FlightId = 1405, OriginAirportId = 2, DestinationAirportId = 5, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(5, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1405, IsCanceled = false },
                new { FlightId = 1410, OriginAirportId = 2, DestinationAirportId = 5, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(6, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1410, IsCanceled = false },
                new { FlightId = 1415, OriginAirportId = 2, DestinationAirportId = 5, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(7, 45, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1415, IsCanceled = false },
                new { FlightId = 1420, OriginAirportId = 2, DestinationAirportId = 5, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(8, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1420, IsCanceled = false },
                new { FlightId = 1425, OriginAirportId = 2, DestinationAirportId = 5, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(9, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1425, IsCanceled = false },
                new { FlightId = 1430, OriginAirportId = 2, DestinationAirportId = 5, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(11, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1430, IsCanceled = false },
                new { FlightId = 1435, OriginAirportId = 2, DestinationAirportId = 5, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(12, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1435, IsCanceled = false },
                new { FlightId = 1440, OriginAirportId = 2, DestinationAirportId = 5, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(13, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1440, IsCanceled = false },
                new { FlightId = 1445, OriginAirportId = 2, DestinationAirportId = 5, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(15, 05, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1445, IsCanceled = false },
                new { FlightId = 1450, OriginAirportId = 2, DestinationAirportId = 5, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(17, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1450, IsCanceled = false },
                // DAL -> ATL
                new { FlightId = 1455, OriginAirportId = 5, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(5, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1455, IsCanceled = false },
                new { FlightId = 1460, OriginAirportId = 5, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(6, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1460, IsCanceled = false },
                new { FlightId = 1465, OriginAirportId = 5, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(7, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1465, IsCanceled = false },
                new { FlightId = 1470, OriginAirportId = 5, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(8, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1470, IsCanceled = false },
                new { FlightId = 1475, OriginAirportId = 5, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(10, 15, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1475, IsCanceled = false },
                new { FlightId = 1480, OriginAirportId = 5, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(11, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1480, IsCanceled = false },
                new { FlightId = 1485, OriginAirportId = 5, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(12, 35, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1485, IsCanceled = false },
                new { FlightId = 1490, OriginAirportId = 5, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(14, 45, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1490, IsCanceled = false },
                new { FlightId = 1495, OriginAirportId = 5, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(18, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1495, IsCanceled = false },
                new { FlightId = 1500, OriginAirportId = 5, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(20, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1500, IsCanceled = false }
            );
        }
    }
}
