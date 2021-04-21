using Air_3550.Models;
using Database.Models;
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
        public DbSet<ScheduledFlight> ScheduledFlights { get; set; }
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
            var p737max = new Plane { PlaneId = 1, Model = "Boeing 737 MAX", MaxSeats = 230 };
            var p747 = new Plane { PlaneId = 2, Model = "Boeing 747", MaxSeats = 416 };
            var p757 = new Plane { PlaneId = 3, Model = "Boeing 757", MaxSeats = 199 };
            var p777 = new Plane { PlaneId = 4, Model = "Boeing 777", MaxSeats = 550 };
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
                new { FlightId = 1, OriginAirportId = 9, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(6, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 1 },
                new { FlightId = 2, OriginAirportId = 9, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(9, 35, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 2 },
                new { FlightId = 3, OriginAirportId = 9, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(14, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 3 },
                new { FlightId = 4, OriginAirportId = 9, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(18, 35, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 4 },
                // DEN -> SEA
                new { FlightId = 5, OriginAirportId = 6, DestinationAirportId = 9, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(6, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 5 },
                new { FlightId = 6, OriginAirportId = 6, DestinationAirportId = 9, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(9, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 6 },
                new { FlightId = 7, OriginAirportId = 6, DestinationAirportId = 9, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(15, 55, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 7 },
                new { FlightId = 8, OriginAirportId = 6, DestinationAirportId = 9, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(20, 55, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 8 },
                // LAX -> DEN
                new { FlightId = 9, OriginAirportId = 3, DestinationAirportId = 6, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(6, 5, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 9 },
                new { FlightId = 10, OriginAirportId = 3, DestinationAirportId = 6, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(11, 55, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 10 },
                new { FlightId = 11, OriginAirportId = 3, DestinationAirportId = 6, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(14, 25, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 11 },
                new { FlightId = 12, OriginAirportId = 3, DestinationAirportId = 6, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(17, 55, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 12 },
                // DEN -> LAX
                new { FlightId = 13, OriginAirportId = 6, DestinationAirportId = 3, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(8, 0, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 13 },
                new { FlightId = 14, OriginAirportId = 6, DestinationAirportId = 3, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(12, 55, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 14 },
                new { FlightId = 15, OriginAirportId = 6, DestinationAirportId = 3, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(17, 0, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 15 },
                new { FlightId = 16, OriginAirportId = 6, DestinationAirportId = 3, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(20, 35, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 16 },
                // LAX -> DAL
                new { FlightId = 17, OriginAirportId = 3, DestinationAirportId = 5, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(6, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 17 },
                new { FlightId = 18, OriginAirportId = 3, DestinationAirportId = 5, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(11, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 18 },
                new { FlightId = 19, OriginAirportId = 3, DestinationAirportId = 5, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(14, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 19 },
                new { FlightId = 20, OriginAirportId = 3, DestinationAirportId = 5, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(18, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 20 },
                // DAL -> LAX
                new { FlightId = 21, OriginAirportId = 5, DestinationAirportId = 3, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(7, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 21 },
                new { FlightId = 22, OriginAirportId = 5, DestinationAirportId = 3, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(9, 45, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 22 },
                new { FlightId = 23, OriginAirportId = 5, DestinationAirportId = 3, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(14, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 23 },
                new { FlightId = 24, OriginAirportId = 5, DestinationAirportId = 3, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(20, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 24 },
                // DEN -> DAL
                new { FlightId = 25, OriginAirportId = 6, DestinationAirportId = 5, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(7, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 25 },
                new { FlightId = 26, OriginAirportId = 6, DestinationAirportId = 5, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(9, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 26 },
                new { FlightId = 27, OriginAirportId = 6, DestinationAirportId = 5, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(14, 05, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 27 },
                new { FlightId = 28, OriginAirportId = 6, DestinationAirportId = 5, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(18, 55, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 28 },
                // DAL -> DEN
                new { FlightId = 29, OriginAirportId = 5, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(7, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 29 },
                new { FlightId = 30, OriginAirportId = 5, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(11, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 30 },
                new { FlightId = 31, OriginAirportId = 5, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(16, 45, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 31 },
                new { FlightId = 32, OriginAirportId = 5, DestinationAirportId = 6, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(20, 55, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 32 },
                // DEN -> MDW
                new { FlightId = 33, OriginAirportId = 6, DestinationAirportId = 4, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(8, 25, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 33 },
                new { FlightId = 34, OriginAirportId = 6, DestinationAirportId = 4, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(10, 55, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 34 },
                new { FlightId = 35, OriginAirportId = 6, DestinationAirportId = 4, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(13, 05, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 35 },
                new { FlightId = 36, OriginAirportId = 6, DestinationAirportId = 4, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(18, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 36 },
                // MDW -> DEN
                new { FlightId = 37, OriginAirportId = 4, DestinationAirportId = 6, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(6, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 37 },
                new { FlightId = 38, OriginAirportId = 4, DestinationAirportId = 6, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(10, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 38 },
                new { FlightId = 39, OriginAirportId = 4, DestinationAirportId = 6, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(12, 45, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 39 },
                new { FlightId = 40, OriginAirportId = 4, DestinationAirportId = 6, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(16, 45, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 40 },
                // DEN -> BNA
                new { FlightId = 41, OriginAirportId = 6, DestinationAirportId = 10, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(6, 45, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 41 },
                new { FlightId = 42, OriginAirportId = 6, DestinationAirportId = 10, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(9, 15, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 42 },
                new { FlightId = 43, OriginAirportId = 6, DestinationAirportId = 10, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(15, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 43 },
                new { FlightId = 44, OriginAirportId = 6, DestinationAirportId = 10, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(18, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 44 },
                // BNA -> DEN
                new { FlightId = 45, OriginAirportId = 10, DestinationAirportId = 6, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(6, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 45 },
                new { FlightId = 46, OriginAirportId = 10, DestinationAirportId = 6, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(9, 05, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 46 },
                new { FlightId = 47, OriginAirportId = 10, DestinationAirportId = 6, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(15, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 47 },
                new { FlightId = 48, OriginAirportId = 10, DestinationAirportId = 6, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(17, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 48 },
                // DEN -> ATL
                new { FlightId = 49, OriginAirportId = 6, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(7, 0, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 49 },
                new { FlightId = 50, OriginAirportId = 6, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(9, 15, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 50 },
                new { FlightId = 51, OriginAirportId = 6, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(14, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 51 },
                new { FlightId = 52, OriginAirportId = 6, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(18, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 52 },
                // ATL -> DEN
                new { FlightId = 53, OriginAirportId = 2, DestinationAirportId = 6, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(7, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 53 },
                new { FlightId = 54, OriginAirportId = 2, DestinationAirportId = 6, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(11, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 54 },
                new { FlightId = 55, OriginAirportId = 2, DestinationAirportId = 6, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(14, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 55 },
                new { FlightId = 56, OriginAirportId = 2, DestinationAirportId = 6, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(18, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 56 },
                // MDW -> CLE
                new { FlightId = 57, OriginAirportId = 4, DestinationAirportId = 1, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(7, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 57 },
                new { FlightId = 58, OriginAirportId = 4, DestinationAirportId = 1, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(9, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 58 },
                new { FlightId = 59, OriginAirportId = 4, DestinationAirportId = 1, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(12, 45, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 59 },
                new { FlightId = 60, OriginAirportId = 4, DestinationAirportId = 1, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(16, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 60 },
                // CLE -> MDW
                new { FlightId = 61, OriginAirportId = 1, DestinationAirportId = 4, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(7, 0, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 61 },
                new { FlightId = 62, OriginAirportId = 1, DestinationAirportId = 4, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(10, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 62 },
                new { FlightId = 63, OriginAirportId = 1, DestinationAirportId = 4, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(15, 35, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 63 },
                new { FlightId = 64, OriginAirportId = 1, DestinationAirportId = 4, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(19, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 64 },
                // MDW -> ATL
                new { FlightId = 65, OriginAirportId = 4, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(9, 00, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 65 },
                new { FlightId = 66, OriginAirportId = 4, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(11, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 66 },
                new { FlightId = 67, OriginAirportId = 4, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(13, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 67 },
                new { FlightId = 68, OriginAirportId = 4, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(17, 35, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 68 },
                // ATL -> MDW
                new { FlightId = 69, OriginAirportId = 2, DestinationAirportId = 4, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(6, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 69 },
                new { FlightId = 70, OriginAirportId = 2, DestinationAirportId = 4, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(9, 35, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 70 },
                new { FlightId = 71, OriginAirportId = 2, DestinationAirportId = 4, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(12, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 71 },
                new { FlightId = 72, OriginAirportId = 2, DestinationAirportId = 4, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(18, 25, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 72 },
                // BNA -> CLE
                new { FlightId = 73, OriginAirportId = 10, DestinationAirportId = 1, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(8, 45, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 73 },
                new { FlightId = 74, OriginAirportId = 10, DestinationAirportId = 1, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(11, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 74 },
                new { FlightId = 75, OriginAirportId = 10, DestinationAirportId = 1, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(14, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 75 },
                new { FlightId = 76, OriginAirportId = 10, DestinationAirportId = 1, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(18, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 76 },
                // CLE -> BNA
                new { FlightId = 77, OriginAirportId = 1, DestinationAirportId = 10, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(7, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 77 },
                new { FlightId = 78, OriginAirportId = 1, DestinationAirportId = 10, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(9, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 78 },
                new { FlightId = 79, OriginAirportId = 1, DestinationAirportId = 10, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(15, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 79 },
                new { FlightId = 80, OriginAirportId = 1, DestinationAirportId = 10, PlaneTypePlaneId = 1, DepartureTime = new TimeSpan(19, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 80 },
                // BNA -> ATL
                new { FlightId = 81, OriginAirportId = 10, DestinationAirportId = 2, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(8, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 81 },
                new { FlightId = 82, OriginAirportId = 10, DestinationAirportId = 2, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(10, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 82 },
                new { FlightId = 83, OriginAirportId = 10, DestinationAirportId = 2, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(14, 35, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 83 },
                new { FlightId = 84, OriginAirportId = 10, DestinationAirportId = 2, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(17, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 84 },
                // ATL -> BNA
                new { FlightId = 85, OriginAirportId = 2, DestinationAirportId = 10, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(8, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 85 },
                new { FlightId = 86, OriginAirportId = 2, DestinationAirportId = 10, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(9, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 86 },
                new { FlightId = 87, OriginAirportId = 2, DestinationAirportId = 10, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(15, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 87 },
                new { FlightId = 88, OriginAirportId = 2, DestinationAirportId = 10, PlaneTypePlaneId = 3, DepartureTime = new TimeSpan(20, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 88 },
                // CLE -> ATL
                new { FlightId = 89, OriginAirportId = 1, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(8, 0, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 89 },
                new { FlightId = 90, OriginAirportId = 1, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(9, 45, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 90 },
                new { FlightId = 91, OriginAirportId = 1, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(13, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 91 },
                new { FlightId = 92, OriginAirportId = 1, DestinationAirportId = 2, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(19, 0, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 92 },
                // ATL -> CLE
                new { FlightId = 93, OriginAirportId = 2, DestinationAirportId = 1, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(8, 05, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 93 },
                new { FlightId = 94, OriginAirportId = 2, DestinationAirportId = 1, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(11, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 94 },
                new { FlightId = 95, OriginAirportId = 2, DestinationAirportId = 1, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(14, 05, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 95 },
                new { FlightId = 96, OriginAirportId = 2, DestinationAirportId = 1, PlaneTypePlaneId = 2, DepartureTime = new TimeSpan(18, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 96 },
                // ATL -> LGA
                new { FlightId = 97, OriginAirportId = 2, DestinationAirportId = 7, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(9, 0, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 97 },
                new { FlightId = 98, OriginAirportId = 2, DestinationAirportId = 7, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(11, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 98 },
                new { FlightId = 99, OriginAirportId = 2, DestinationAirportId = 7, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(13, 50, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 99 },
                new { FlightId = 100, OriginAirportId = 2, DestinationAirportId = 7, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(16, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 100 },
                // LGA -> ATL
                new { FlightId = 101, OriginAirportId = 7, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(9, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 101 },
                new { FlightId = 102, OriginAirportId = 7, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(11, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 102 },
                new { FlightId = 103, OriginAirportId = 7, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(14, 0, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 103 },
                new { FlightId = 104, OriginAirportId = 7, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(17, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 104 },
                // ATL -> MIA
                new { FlightId = 105, OriginAirportId = 2, DestinationAirportId = 8, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(9, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 105 },
                new { FlightId = 106, OriginAirportId = 2, DestinationAirportId = 8, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(10, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 106 },
                new { FlightId = 107, OriginAirportId = 2, DestinationAirportId = 8, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(13, 45, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 107 },
                new { FlightId = 108, OriginAirportId = 2, DestinationAirportId = 8, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(18, 40, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 108 },
                // MIA -> ATL
                new { FlightId = 109, OriginAirportId = 8, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(9, 30, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 109 },
                new { FlightId = 110, OriginAirportId = 8, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(11, 20, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 110 },
                new { FlightId = 111, OriginAirportId = 8, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(13, 45, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 111 },
                new { FlightId = 112, OriginAirportId = 8, DestinationAirportId = 2, PlaneTypePlaneId = 4, DepartureTime = new TimeSpan(17, 10, 0), Cost = -1, TicketsPurchased = 0, FlightNumber = 112 }
            );
        }
    }
}
