﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToyStoreApi;

#nullable disable

namespace ToyStoreApi.Migrations
{
    [DbContext(typeof(PurchaseDbContext))]
    [Migration("20221014185309_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.10");

            modelBuilder.Entity("ToyStoreApi.Models.PurchaseModel", b =>
                {
                    b.Property<string>("UPC")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Cost")
                        .HasColumnType("TEXT");

                    b.Property<string>("PurchaserEmail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UPC");

                    b.ToTable("Purchases");
                });
#pragma warning restore 612, 618
        }
    }
}
