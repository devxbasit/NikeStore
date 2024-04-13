﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NikeStore.Services.CouponApi.Data;

#nullable disable

namespace NikeStore.Services.CouponApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NikeStore.Services.CouponApi.Models.Coupon", b =>
                {
                    b.Property<int>("CouponId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CouponId"));

                    b.Property<string>("CouponCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("DiscountAmount")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("MinAmount")
                        .HasColumnType("int");

                    b.HasKey("CouponId");

                    b.ToTable("Coupons");

                    b.HasData(
                        new
                        {
                            CouponId = 1,
                            CouponCode = "GUEST_100_OFF",
                            CreatedDateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DiscountAmount = 100,
                            LastUpdatedDateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MinAmount = 1500
                        },
                        new
                        {
                            CouponId = 2,
                            CouponCode = "GUEST_200_OFF",
                            CreatedDateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DiscountAmount = 200,
                            LastUpdatedDateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MinAmount = 2500
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
