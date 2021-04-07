﻿// <auto-generated />
using System;
using Air_3550.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Database.Migrations
{
    [DbContext(typeof(AirContext))]
    [Migration("20210407232741_SeedPlanesAndStaff")]
    partial class SeedPlanesAndStaff
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("Air_3550.Models.Airport", b =>
                {
                    b.Property<int>("AirportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AirportCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("AirportId1")
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Elevation")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("Decimal(8,6)");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("Decimal(9,6)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("AirportId");

                    b.HasIndex("AirportId1");

                    b.ToTable("Airports");

                    b.HasData(
                        new
                        {
                            AirportId = 1,
                            AirportCode = "CLE",
                            City = "Cleveland",
                            Country = "USA",
                            Elevation = 791,
                            Latitude = 41.411667m,
                            Longitude = -81.849722m,
                            State = "Ohio"
                        },
                        new
                        {
                            AirportId = 2,
                            AirportCode = "ATL",
                            City = "Atlanta",
                            Country = "USA",
                            Elevation = 1027,
                            Latitude = 33.636667m,
                            Longitude = -84.428056m,
                            State = "Georgia"
                        },
                        new
                        {
                            AirportId = 3,
                            AirportCode = "LAX",
                            City = "Los Angeles",
                            Country = "USA",
                            Elevation = 125,
                            Latitude = 33.9425m,
                            Longitude = -118.408056m,
                            State = "California"
                        },
                        new
                        {
                            AirportId = 4,
                            AirportCode = "MDW",
                            City = "Chicago",
                            Country = "USA",
                            Elevation = 620,
                            Latitude = 41.786111m,
                            Longitude = -87.7525m,
                            State = "Illinois"
                        },
                        new
                        {
                            AirportId = 5,
                            AirportCode = "DAL",
                            City = "Dallas",
                            Country = "USA",
                            Elevation = 486,
                            Latitude = 32.847222m,
                            Longitude = -96.851667m,
                            State = "Texas"
                        },
                        new
                        {
                            AirportId = 6,
                            AirportCode = "DEN",
                            City = "Denver",
                            Country = "USA",
                            Elevation = 5430,
                            Latitude = 39.861667m,
                            Longitude = -104.673056m,
                            State = "Colorado"
                        },
                        new
                        {
                            AirportId = 7,
                            AirportCode = "LGA",
                            City = "New York",
                            Country = "USA",
                            Elevation = 19,
                            Latitude = 40.775m,
                            Longitude = -73.875m,
                            State = "New York"
                        },
                        new
                        {
                            AirportId = 8,
                            AirportCode = "MIA",
                            City = "Miami",
                            Country = "USA",
                            Elevation = 9,
                            Latitude = 25.793333m,
                            Longitude = -80.290556m,
                            State = "Florida"
                        },
                        new
                        {
                            AirportId = 9,
                            AirportCode = "SEA",
                            City = "Seattle",
                            Country = "USA",
                            Elevation = 433,
                            Latitude = 47.448889m,
                            Longitude = -122.309444m,
                            State = "Washington"
                        },
                        new
                        {
                            AirportId = 10,
                            AirportCode = "BNA",
                            City = "Nashville",
                            Country = "USA",
                            Elevation = 599,
                            Latitude = 36.126667m,
                            Longitude = -86.681944m,
                            State = "Tennessee"
                        });
                });

            modelBuilder.Entity("Air_3550.Models.Flight", b =>
                {
                    b.Property<int>("FlightId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Cost")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DestinationAirportId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FlightNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("OriginAirportId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PlaneTypePlaneId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TicketsPurchased")
                        .HasColumnType("INTEGER");

                    b.HasKey("FlightId");

                    b.HasIndex("DestinationAirportId");

                    b.HasIndex("OriginAirportId");

                    b.HasIndex("PlaneTypePlaneId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("Air_3550.Models.Plane", b =>
                {
                    b.Property<int>("PlaneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaxDistance")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaxSeats")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("PlaneId");

                    b.ToTable("Planes");

                    b.HasData(
                        new
                        {
                            PlaneId = 1,
                            MaxDistance = 6570,
                            MaxSeats = 230,
                            Model = "Boeing 737 MAX"
                        },
                        new
                        {
                            PlaneId = 2,
                            MaxDistance = 14815,
                            MaxSeats = 416,
                            Model = "Boeing 747"
                        },
                        new
                        {
                            PlaneId = 3,
                            MaxDistance = 6241,
                            MaxSeats = 199,
                            Model = "Boeing 757"
                        },
                        new
                        {
                            PlaneId = 4,
                            MaxDistance = 17395,
                            MaxSeats = 550,
                            Model = "Boeing 777"
                        });
                });

            modelBuilder.Entity("Air_3550.Models.Ticket", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ArrivalDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DepartureDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("FlightId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PaymentType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Price")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TripId")
                        .HasColumnType("INTEGER");

                    b.HasKey("TicketId");

                    b.HasIndex("FlightId");

                    b.HasIndex("TripId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Air_3550.Models.Trip", b =>
                {
                    b.Property<int>("TripId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DestinationAirportId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("OriginAirportId")
                        .HasColumnType("INTEGER");

                    b.HasKey("TripId");

                    b.HasIndex("DestinationAirportId");

                    b.HasIndex("OriginAirportId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("Air_3550.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("HashedPass")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserRole")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            HashedPass = "360caebd9edb68609c0933bade3565350e59e284cc503ce61bf0eebd42fb7e5bd657a71ed1498225168757e7f1095920411cce27779e0c778ec52535deae2040",
                            LoginId = "marketing_manager",
                            UserRole = 1
                        },
                        new
                        {
                            UserId = 2,
                            HashedPass = "41ec260efa3aa054a91cc6cf9441e3652637f75946c8fa6c2e926f289e095f46e62e19a28e02fb0fc25dd047b1f04e6e03cf930d464b540c40c0045eb6b7e252",
                            LoginId = "load_engineer",
                            UserRole = 2
                        },
                        new
                        {
                            UserId = 3,
                            HashedPass = "ab8f196b4521a3aba1de420fe6ef552ce406d8551c3aef370f909cea85abbc77ca1d698ef31f659097eee16e365975047ff403df1aae6cc7ef54595c3ae4d172",
                            LoginId = "accounting_manager",
                            UserRole = 3
                        },
                        new
                        {
                            UserId = 4,
                            HashedPass = "e2289f3f3a66a81f5ffb52dff1a09cd2ae91a39eb248230d2907084679c6bacdb4a880da7835ca72003d8c37e107cf91c5f9795678303754eba1be42039bff4d",
                            LoginId = "flight_manager",
                            UserRole = 4
                        });
                });

            modelBuilder.Entity("Air_3550.Models.Airport", b =>
                {
                    b.HasOne("Air_3550.Models.Airport", null)
                        .WithMany("ConnectedAirports")
                        .HasForeignKey("AirportId1");
                });

            modelBuilder.Entity("Air_3550.Models.Flight", b =>
                {
                    b.HasOne("Air_3550.Models.Airport", "Destination")
                        .WithMany()
                        .HasForeignKey("DestinationAirportId");

                    b.HasOne("Air_3550.Models.Airport", "Origin")
                        .WithMany()
                        .HasForeignKey("OriginAirportId");

                    b.HasOne("Air_3550.Models.Plane", "PlaneType")
                        .WithMany()
                        .HasForeignKey("PlaneTypePlaneId");

                    b.Navigation("Destination");

                    b.Navigation("Origin");

                    b.Navigation("PlaneType");
                });

            modelBuilder.Entity("Air_3550.Models.Ticket", b =>
                {
                    b.HasOne("Air_3550.Models.Flight", "Flight")
                        .WithMany()
                        .HasForeignKey("FlightId");

                    b.HasOne("Air_3550.Models.Trip", "Trip")
                        .WithMany("Tickets")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flight");

                    b.Navigation("Trip");
                });

            modelBuilder.Entity("Air_3550.Models.Trip", b =>
                {
                    b.HasOne("Air_3550.Models.Airport", "Destination")
                        .WithMany()
                        .HasForeignKey("DestinationAirportId");

                    b.HasOne("Air_3550.Models.Airport", "Origin")
                        .WithMany()
                        .HasForeignKey("OriginAirportId");

                    b.Navigation("Destination");

                    b.Navigation("Origin");
                });

            modelBuilder.Entity("Air_3550.Models.Airport", b =>
                {
                    b.Navigation("ConnectedAirports");
                });

            modelBuilder.Entity("Air_3550.Models.Trip", b =>
                {
                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
