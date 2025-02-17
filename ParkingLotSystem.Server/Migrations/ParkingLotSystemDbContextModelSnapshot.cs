﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParkingLotSystem.DataAccess.Contexts;

#nullable disable

namespace ParkingLotSystem.Server.Migrations
{
    [DbContext(typeof(ParkingLotSystemDbContext))]
    partial class ParkingLotSystemDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ParkingLotSystem.Server.Core.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("varchar(60)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(32)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<int>("SiteID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SiteID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ParkingLotSystem.Server.Core.Entities.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ApartmentNumber")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("EntryTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ExitTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("OwnerName")
                        .IsRequired()
                        .HasColumnType("varchar(60)");

                    b.Property<int>("SiteID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SiteID");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("Site", b =>
                {
                    b.Property<int>("SiteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SiteID"));

                    b.Property<int>("ClientID")
                        .HasColumnType("int");

                    b.Property<string>("SiteName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("SiteSecret")
                        .IsRequired()
                        .HasColumnType("varchar(36)");

                    b.HasKey("SiteID");

                    b.ToTable("Sites");
                });

            modelBuilder.Entity("ParkingLotSystem.Server.Core.Entities.User", b =>
                {
                    b.HasOne("Site", "Site")
                        .WithMany("Users")
                        .HasForeignKey("SiteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Site");
                });

            modelBuilder.Entity("ParkingLotSystem.Server.Core.Entities.Vehicle", b =>
                {
                    b.HasOne("Site", "Site")
                        .WithMany("Vehicles")
                        .HasForeignKey("SiteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Site");
                });

            modelBuilder.Entity("Site", b =>
                {
                    b.Navigation("Users");

                    b.Navigation("Vehicles");
                });
#pragma warning restore 612, 618
        }
    }
}
