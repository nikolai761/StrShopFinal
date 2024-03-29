﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StrShop.Data;

namespace StrShop.Migrations
{
    [DbContext(typeof(DBconnection))]
    [Migration("20220420170723_new")]
    partial class @new
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StrShop.Data.Models.Category", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName");

                    b.HasKey("id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("StrShop.Data.Models.Item", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<string>("Description");

                    b.Property<string>("ItemName");

                    b.Property<int>("ProducerId");

                    b.Property<int>("StorageId");

                    b.Property<string>("img");

                    b.Property<bool>("isAction");

                    b.Property<int>("price");

                    b.HasKey("id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ProducerId");

                    b.HasIndex("StorageId");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("StrShop.Data.Models.Order", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("adress");

                    b.Property<string>("email");

                    b.Property<string>("name");

                    b.Property<string>("phone");

                    b.Property<string>("surname");

                    b.HasKey("id");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("StrShop.Data.Models.OrderDetail", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ItemId");

                    b.Property<int>("orderId");

                    b.Property<int>("price");

                    b.HasKey("id");

                    b.HasIndex("ItemId");

                    b.HasIndex("orderId");

                    b.ToTable("OrderDetail");
                });

            modelBuilder.Entity("StrShop.Data.Models.Producer", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ProducerDesc");

                    b.Property<string>("ProducerName");

                    b.HasKey("id");

                    b.ToTable("Producer");
                });

            modelBuilder.Entity("StrShop.Data.Models.Role", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "admin"
                        },
                        new
                        {
                            ID = 2,
                            Name = "user"
                        });
                });

            modelBuilder.Entity("StrShop.Data.Models.ShopCartItem", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ShopCartId");

                    b.Property<int?>("itemid");

                    b.Property<int>("price");

                    b.HasKey("id");

                    b.HasIndex("itemid");

                    b.ToTable("ShopCartItems");
                });

            modelBuilder.Entity("StrShop.Data.Models.Storage", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Delivery");

                    b.Property<string>("StorageAddress");

                    b.HasKey("id");

                    b.ToTable("Storage");
                });

            modelBuilder.Entity("StrShop.Data.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Password");

                    b.Property<int?>("RoleId");

                    b.Property<bool>("Unblocked");

                    b.Property<byte[]>("salt");

                    b.HasKey("ID");

                    b.HasIndex("RoleId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Email = "admin",
                            Password = "xZhDxctlQPPAYkcdYTAZOw==",
                            RoleId = 1,
                            Unblocked = true,
                            salt = new byte[] { 36, 116, 197, 22, 90, 80, 76, 68 }
                        });
                });

            modelBuilder.Entity("StrShop.Data.Models.Item", b =>
                {
                    b.HasOne("StrShop.Data.Models.Category", "Category")
                        .WithMany("Items")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("StrShop.Data.Models.Producer", "Producer")
                        .WithMany("Items")
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("StrShop.Data.Models.Storage", "Storage")
                        .WithMany("Items")
                        .HasForeignKey("StorageId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("StrShop.Data.Models.OrderDetail", b =>
                {
                    b.HasOne("StrShop.Data.Models.Item", "item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("StrShop.Data.Models.Order", "order")
                        .WithMany("OrderDelails")
                        .HasForeignKey("orderId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("StrShop.Data.Models.ShopCartItem", b =>
                {
                    b.HasOne("StrShop.Data.Models.Item", "item")
                        .WithMany()
                        .HasForeignKey("itemid")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("StrShop.Data.Models.User", b =>
                {
                    b.HasOne("StrShop.Data.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
