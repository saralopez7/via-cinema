﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using VIACinemaApp.Models;

namespace VIACinemaApp.Migrations
{
    [DbContext(typeof(VIACinemaAppContext))]
    [Migration("20180329152259_Available-Movies")]
    partial class AvailableMovies
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VIACinemaApp.Models.AvailableMovies", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AvailableDate");

                    b.Property<int>("AvailableSeats");

                    b.Property<int>("MovieId");

                    b.HasKey("Id");

                    b.ToTable("AvailableMovies");
                });

            modelBuilder.Entity("VIACinemaApp.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Director");

                    b.Property<double>("Duration");

                    b.Property<int>("Genre");

                    b.Property<string>("MovieTitle");

                    b.Property<string>("Plot");

                    b.Property<int>("Rating");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Movie");
                });
#pragma warning restore 612, 618
        }
    }
}