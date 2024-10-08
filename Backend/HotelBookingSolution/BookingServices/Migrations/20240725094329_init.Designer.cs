﻿//// <auto-generated />
//using System;
//using BookingServices.Contexts;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.EntityFrameworkCore.Metadata;
//using Microsoft.EntityFrameworkCore.Migrations;
//using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

//#nullable disable

//namespace BookingServices.Migrations
//{
//    [DbContext(typeof(HotelBookingContext))]
//    [Migration("20240725094329_init")]
//    partial class init
//    {
//        protected override void BuildTargetModel(ModelBuilder modelBuilder)
//        {
//#pragma warning disable 612, 618
//            modelBuilder
//                .HasAnnotation("ProductVersion", "6.0.32")
//                .HasAnnotation("Relational:MaxIdentifierLength", 128);

//            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

//            modelBuilder.Entity("Booking", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

//                    b.Property<DateTime>("CheckInDate")
//                        .HasColumnType("datetime2");

//                    b.Property<DateTime>("CheckOutDate")
//                        .HasColumnType("datetime2");

//                    b.Property<int>("HotelId")
//                        .HasColumnType("int");

//                    b.Property<bool>("IsCancelled")
//                        .HasColumnType("bit");

//                    b.Property<bool>("IsPaid")
//                        .HasColumnType("bit");

//                    b.Property<int>("NumberOfGuests")
//                        .HasColumnType("int");

//                    b.Property<decimal>("TotalAmount")
//                        .HasColumnType("decimal(18,2)");

//                    b.Property<int>("UserId")
//                        .HasColumnType("int");

//                    b.HasKey("Id");

//                    b.ToTable("Bookings");
//                });

//            modelBuilder.Entity("BookingServices.Models.BookingDetail", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

//                    b.Property<int>("BookingId")
//                        .HasColumnType("int");

//                    b.Property<int>("HotelId")
//                        .HasColumnType("int");

//                    b.Property<decimal>("Rent")
//                        .HasColumnType("decimal(18,2)");

//                    b.Property<int>("RoomNumber")
//                        .HasColumnType("int");

//                    b.HasKey("Id");

//                    b.HasIndex("BookingId");

//                    b.ToTable("BookingDetail");
//                });

//            modelBuilder.Entity("BookingServices.Models.BookingDetail", b =>
//                {
//                    b.HasOne("Booking", "Booking")
//                        .WithMany("BookingDetails")
//                        .HasForeignKey("BookingId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.Navigation("Booking");
//                });

//            modelBuilder.Entity("Booking", b =>
//                {
//                    b.Navigation("BookingDetails");
//                });
//#pragma warning restore 612, 618
//        }
//    }
//}
