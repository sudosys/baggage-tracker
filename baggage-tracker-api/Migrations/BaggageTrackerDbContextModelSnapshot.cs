﻿// <auto-generated />
using System;
using BaggageTrackerApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BaggageTrackerApi.Migrations
{
    [DbContext(typeof(BaggageTrackerDbContext))]
    partial class BaggageTrackerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BaggageTrackerApi.Entities.Baggage", b =>
                {
                    b.Property<Guid>("BaggageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BaggageName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("BaggageStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("BaggageId");

                    b.HasIndex("UserId");

                    b.ToTable("bt_baggages");

                    b.HasData(
                        new
                        {
                            BaggageId = new Guid("6bd8c8db-a5ba-4e29-a7ae-a3786a98ec01"),
                            BaggageName = "Blue Samsonite Case",
                            BaggageStatus = "Undefined",
                            UserId = 1L
                        },
                        new
                        {
                            BaggageId = new Guid("0d58ae42-ca7b-43f1-8502-3d5d9b2c4eae"),
                            BaggageName = "Benetti Sports Bag",
                            BaggageStatus = "Undefined",
                            UserId = 1L
                        },
                        new
                        {
                            BaggageId = new Guid("b42c7dd7-8fcd-477f-9822-7bd14862e3b8"),
                            BaggageName = "Lightweight PP Collection",
                            BaggageStatus = "Undefined",
                            UserId = 1L
                        },
                        new
                        {
                            BaggageId = new Guid("0543b03e-7449-4507-adf2-5d24f248c678"),
                            BaggageName = "Samsonite Popsoda",
                            BaggageStatus = "Undefined",
                            UserId = 2L
                        },
                        new
                        {
                            BaggageId = new Guid("8f161fdd-3549-49e6-aa59-1de5ca1383d5"),
                            BaggageName = "Fantana Matrix PP Hard Shell",
                            BaggageStatus = "Undefined",
                            UserId = 3L
                        },
                        new
                        {
                            BaggageId = new Guid("55cc9206-a6c7-4f32-8a3f-5e5142ff400e"),
                            BaggageName = "Canvas Explorer Holdall",
                            BaggageStatus = "Undefined",
                            UserId = 3L
                        });
                });

            modelBuilder.Entity("BaggageTrackerApi.Entities.Flight", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("FlightNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("bt_flights");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            FlightNumber = "TK5094",
                            UserId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            FlightNumber = "TK5094",
                            UserId = 2L
                        },
                        new
                        {
                            Id = 3L,
                            FlightNumber = "TK2745",
                            UserId = 3L
                        });
                });

            modelBuilder.Entity("BaggageTrackerApi.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("bt_users");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            FullName = "Avery Thompson",
                            Password = "711f24d8676c4462bcb9b8d6ff12e524483afcff5ea9ba726fba772c296b214c",
                            Role = "Passenger",
                            Username = "avery.thompson"
                        },
                        new
                        {
                            Id = 2L,
                            FullName = "Sebastian Morales",
                            Password = "46498d3d669434f320a45770a9b8ab8cbc16bd7dfeeb724c5503b2cb9d3d395e",
                            Role = "Passenger",
                            Username = "sebastian.morales"
                        },
                        new
                        {
                            Id = 3L,
                            FullName = "Olivia Martinez",
                            Password = "0cc4bbe5ac4df909798c2ccd0844f15a86a694457758e42d9ce52e7d39e9e256",
                            Role = "Passenger",
                            Username = "olivia.martinez"
                        },
                        new
                        {
                            Id = 4L,
                            FullName = "Lukas Cruz",
                            Password = "9a18e4407334e70436cc60b0c53e8f384fc4d0934d2bc3c7d70573b63fc72a64",
                            Role = "Personnel",
                            Username = "lukas.cruz"
                        });
                });

            modelBuilder.Entity("BaggageTrackerApi.Entities.Baggage", b =>
                {
                    b.HasOne("BaggageTrackerApi.Entities.User", "User")
                        .WithMany("Baggages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BaggageTrackerApi.Entities.Flight", b =>
                {
                    b.HasOne("BaggageTrackerApi.Entities.User", "User")
                        .WithOne("ActiveFlight")
                        .HasForeignKey("BaggageTrackerApi.Entities.Flight", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BaggageTrackerApi.Entities.User", b =>
                {
                    b.Navigation("ActiveFlight");

                    b.Navigation("Baggages");
                });
#pragma warning restore 612, 618
        }
    }
}
