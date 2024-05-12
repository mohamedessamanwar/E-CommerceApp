﻿// <auto-generated />
using System;
using DataAccessLayer.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(ECommerceContext))]
    [Migration("20240505183620_update col")]
    partial class updatecol
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataAccessLayer.Data.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("UpdateOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Apple",
                            UpdateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Dell",
                            UpdateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "HP",
                            UpdateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Lenovo",
                            UpdateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "ASUS",
                            UpdateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 6,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Acer",
                            UpdateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 7,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Microsoft",
                            UpdateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 8,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "MSI",
                            UpdateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 9,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Razer",
                            UpdateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 10,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Samsung",
                            UpdateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("DataAccessLayer.Data.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<decimal>("CurrentPrice")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("decimal(18,2)")
                        .HasComputedColumnSql("Price * (1 - (Discount / 100))");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Discount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0m);

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("UpdateOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CategoryID");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryID = 4,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "Processor AMD Ryzen™ 7 5800H(8C / 16T, 3.2 / 4.4GHz, 4MB L2 / 16MB L3)\r\nGraphics\r\nNVIDIA® GeForce RTX™ 3060 6GB GDDR6, Boost Clock 1425 / 1702MHz, TGP 130W\r\nMemory\r\n2x 8GB SO-DIMM DDR4-3200\r\nUp to 32GB DDR4-3200 offering\r\nStorage\r\n1TB SSD M.2 2280 PCIe® 3.0x4 NVMe®\r\n",
                            Discount = 0m,
                            Model = "82JQ00TQED",
                            Name = "LENOVO Legion 5 Pro",
                            Price = 46999m
                        },
                        new
                        {
                            Id = 2,
                            CategoryID = 5,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "Processor: Intel® Core™ i7-1260P 12th Generation 12C / 16T Processor 2.1 GHz (18M Cache, up to 4.7 GHz, 4P+8E cores)\r\nGraphics: \"Intel® Iris Xe Graphics\"\r\nMemory: 16GB LPDDR5 on board\r\nStorage: 1TB M.2 NVMe™ PCIe® 3.0 SSD\r\nDisplay: 14.0-inch, 2.8K (2880 x 1800) OLED 16:10 aspect ratio, 0.2ms response time, 90Hz refresh rate, 400nits, 600nits HDR peak brightness, 100% DCI-P3 /touch screen, (Screen-to-body ratio)90%",
                            Discount = 0m,
                            Model = "UX3402ZA-OLED007W",
                            Name = "Asus ZenBook 14 UX3402ZA",
                            Price = 43499m
                        },
                        new
                        {
                            Id = 3,
                            CategoryID = 4,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "Processor\r\nAMD Ryzen 5 5600H (6C / 12T, 3.3 / 4.2GHz, 3MB L2 / 16MB L3)\r\nGraphics\r\nNVIDIA GeForce RTX 3050 Ti 4GB GDDR6, Boost Clock 1485 / 1597.5MHz, TGP 85W\r\nMemory\r\n1x 8GB SO-DIMM DDR4-3200\r\nStorage\r\n256GB SSD M.2 2242 PCIe 3.0x4 NVMe + 1TB HDD\r\nDisplay\r\n15.6\" FHD (1920x1080) IPS 250nits Anti-glare, 45% NTSC, 120Hz\r\nOperating System\r\nWindows 11 Home, English\r\nKeyboard\r\nWhite Backlit, English (US)",
                            Discount = 0m,
                            Model = "LENOVO IdeaPad Gaming",
                            Name = "LENOVO IdeaPad Gaming",
                            Price = 27999m
                        },
                        new
                        {
                            Id = 4,
                            CategoryID = 3,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "AMD Ryzen™ 7 5700U (up to 4.3 GHz max boost clock, 8 MB L3 cache, 8 cores, 16 threads) 1 2 \r\nIntegrated,AMD Radeon™ Graphics .8 GB DDR4-3200 MHz RAM (1 x 8 GB) 512 GB PCIe® NVMe™ M.2 SSD\r\n39.6 cm (15.6\") diagonal, FHD (1920 x 1080), micro-edge, anti-glare, 250 nits, 45% NTSC 3\r\nFull-size, jet black keyboard with numeric keypad",
                            Discount = 0m,
                            Model = "eq2009ne",
                            Name = "NOTEBOOK-HP-AMD-15s",
                            Price = 16666m
                        },
                        new
                        {
                            Id = 5,
                            CategoryID = 1,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The Apple MacBook Air is a lightweight and portable laptop with excellent battery life.",
                            Discount = 50m,
                            Model = "MacBook Air",
                            Name = "Apple MacBook Air",
                            Price = 40000m
                        },
                        new
                        {
                            Id = 6,
                            CategoryID = 1,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The Apple MacBook Pro is a high-performance laptop loved by professionals.",
                            Discount = 20m,
                            Model = "MacBook Pro",
                            Name = "Apple MacBook Pro",
                            Price = 80000m
                        },
                        new
                        {
                            Id = 7,
                            CategoryID = 2,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The Dell XPS 13 is a sleek and powerful laptop with a stunning display.",
                            Discount = 0m,
                            Model = "XPS 13",
                            Name = "Dell XPS 13",
                            Price = 40000m
                        },
                        new
                        {
                            Id = 8,
                            CategoryID = 2,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The Dell Inspiron 15 is a versatile laptop suitable for everyday use.",
                            Discount = 13m,
                            Model = "Inspiron 15",
                            Name = "Dell Inspiron 15",
                            Price = 35000m
                        },
                        new
                        {
                            Id = 9,
                            CategoryID = 3,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The HP Spectre x360 is a stylish 2-in-1 laptop with powerful performance.",
                            Discount = 15m,
                            Model = "Spectre x360",
                            Name = "HP Spectre x360",
                            Price = 25000m
                        },
                        new
                        {
                            Id = 10,
                            CategoryID = 3,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The HP Pavilion 14 is a budget-friendly laptop with decent specifications.",
                            Discount = 60m,
                            Model = "Pavilion 14",
                            Name = "HP Pavilion 14",
                            Price = 15000m
                        },
                        new
                        {
                            Id = 11,
                            CategoryID = 1,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The Apple MacBook Air is a lightweight and portable laptop with excellent battery life.",
                            Discount = 10m,
                            Model = "MacBook Air",
                            Name = "Apple MacBook Air",
                            Price = 28000m
                        },
                        new
                        {
                            Id = 12,
                            CategoryID = 1,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The Apple MacBook Pro is a high-performance laptop loved by professionals.",
                            Discount = 12m,
                            Model = "MacBook Pro",
                            Name = "Apple MacBook Pro",
                            Price = 30000m
                        },
                        new
                        {
                            Id = 13,
                            CategoryID = 1,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The Apple iMac is a sleek and powerful all-in-one desktop computer.",
                            Discount = 0m,
                            Model = "iMac",
                            Name = "Apple iMac",
                            Price = 16000m
                        },
                        new
                        {
                            Id = 14,
                            CategoryID = 2,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The Dell XPS 13 is a sleek and powerful laptop with a stunning display.",
                            Discount = 90m,
                            Model = "XPS 13",
                            Name = "Dell XPS 13",
                            Price = 14000m
                        },
                        new
                        {
                            Id = 15,
                            CategoryID = 2,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The Dell Inspiron 15 is a versatile laptop suitable for everyday use.",
                            Discount = 18m,
                            Model = "Inspiron 15",
                            Name = "Dell Inspiron 15",
                            Price = 30000m
                        },
                        new
                        {
                            Id = 16,
                            CategoryID = 2,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The Dell G5 Gaming Desktop is a powerful gaming machine with immersive graphics.",
                            Discount = 20m,
                            Model = "G5 Gaming Desktop",
                            Name = "Dell G5 Gaming Desktop",
                            Price = 38000m
                        },
                        new
                        {
                            Id = 17,
                            CategoryID = 3,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The HP Spectre x360 is a stylish 2-in-1 laptop with powerful performance.",
                            Discount = 19m,
                            Model = "Spectre x360",
                            Name = "HP Spectre x360",
                            Price = 26000m
                        },
                        new
                        {
                            Id = 18,
                            CategoryID = 3,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The HP Pavilion 14 is a budget-friendly laptop with decent specifications.",
                            Discount = 0m,
                            Model = "Pavilion 14",
                            Name = "HP Pavilion 14",
                            Price = 6000m
                        },
                        new
                        {
                            Id = 19,
                            CategoryID = 3,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The HP EliteBook 840 is a business laptop with top-notch security features.",
                            Discount = 80m,
                            Model = "EliteBook 840",
                            Name = "HP EliteBook 840",
                            Price = 50000m
                        },
                        new
                        {
                            Id = 20,
                            CategoryID = 1,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The Apple MacBook Air is a lightweight and portable laptop with excellent battery life.",
                            Discount = 15m,
                            Model = "MacBook Air",
                            Name = "Apple MacBook Air",
                            Price = 18000m
                        },
                        new
                        {
                            Id = 21,
                            CategoryID = 2,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The Dell XPS 13 is a sleek and powerful laptop with a stunning display.",
                            Discount = 5m,
                            Model = "XPS 13",
                            Name = "Dell XPS 13",
                            Price = 13000m
                        },
                        new
                        {
                            Id = 22,
                            CategoryID = 3,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The HP Spectre x360 is a stylish 2-in-1 laptop with powerful performance.",
                            Discount = 10m,
                            Model = "Spectre x360",
                            Name = "HP Spectre x360",
                            Price = 12000m
                        },
                        new
                        {
                            Id = 23,
                            CategoryID = 4,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The Lenovo ThinkCentre M720 is a compact and reliable desktop computer for business use.",
                            Discount = 6m,
                            Model = "ThinkCentre M720",
                            Name = "Lenovo ThinkCentre M720",
                            Price = 15000m
                        },
                        new
                        {
                            Id = 24,
                            CategoryID = 5,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The ASUS ROG Strix G15 is a powerful gaming desktop with RGB lighting and high-performance components.",
                            Discount = 60m,
                            Model = "ROG Strix G15",
                            Name = "ASUS ROG Strix G15",
                            Price = 80000m
                        },
                        new
                        {
                            Id = 25,
                            CategoryID = 6,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The Acer Aspire TC is a budget-friendly desktop computer with decent performance.",
                            Discount = 15m,
                            Model = "Aspire TC",
                            Name = "Acer Aspire TC",
                            Price = 18000m
                        },
                        new
                        {
                            Id = 26,
                            CategoryID = 2,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The Dell Inspiron 27 is an all-in-one desktop computer with a large display and powerful performance.",
                            Discount = 10m,
                            Model = "Inspiron 27",
                            Name = "Dell Inspiron 27",
                            Price = 22000m
                        },
                        new
                        {
                            Id = 27,
                            CategoryID = 5,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The ASUS ZenBook Pro is a premium laptop with a stunning 4K display and high-performance components.",
                            Discount = 15m,
                            Model = "ZenBook Pro",
                            Name = "ASUS ZenBook Pro",
                            Price = 28000m
                        },
                        new
                        {
                            Id = 28,
                            CategoryID = 3,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The HP Pavilion Gaming Desktop is a gaming powerhouse with advanced graphics and smooth gameplay.",
                            Discount = 80m,
                            Model = "Pavilion Gaming Desktop",
                            Name = "HP Pavilion Gaming Desktop",
                            Price = 15000m
                        },
                        new
                        {
                            Id = 29,
                            CategoryID = 4,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The Lenovo Legion Y540 is a gaming laptop with powerful hardware and immersive gaming experience.",
                            Discount = 12m,
                            Model = "Legion Y540",
                            Name = "Lenovo Legion Y540",
                            Price = 20000m
                        },
                        new
                        {
                            Id = 30,
                            CategoryID = 1,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The Apple iMac is a sleek all-in-one desktop computer with a stunning Retina display and powerful performance.",
                            Discount = 20m,
                            Model = "iMac",
                            Name = "Apple iMac",
                            Price = 24000m
                        },
                        new
                        {
                            Id = 31,
                            CategoryID = 2,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The Dell G5 is a gaming laptop with high-performance hardware and immersive gaming features.",
                            Discount = 10m,
                            Model = "G5 Gaming Laptop",
                            Name = "Dell G5 Gaming Laptop",
                            Price = 18000m
                        },
                        new
                        {
                            Id = 32,
                            CategoryID = 3,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The HP Envy 15 is a premium laptop with a sleek design and powerful performance for multimedia and productivity tasks.",
                            Discount = 15m,
                            Model = "Envy 15",
                            Name = "HP Envy 15",
                            Price = 16000m
                        },
                        new
                        {
                            Id = 33,
                            CategoryID = 4,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The Lenovo IdeaCentre 5 is a compact and versatile desktop computer suitable for home and office use.",
                            Discount = 50m,
                            Model = "IdeaCentre 5",
                            Name = "Lenovo IdeaCentre 5",
                            Price = 8990m
                        },
                        new
                        {
                            Id = 34,
                            CategoryID = 5,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The ASUS VivoBook S15 is a stylish and lightweight laptop with a vibrant display and long battery life.",
                            Discount = 0m,
                            Model = "VivoBook S15",
                            Name = "ASUS VivoBook S15",
                            Price = 9990m
                        },
                        new
                        {
                            Id = 35,
                            CategoryID = 10,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The Samsung Galaxy Book Pro is a thin and lightweight laptop with a stunning AMOLED display and powerful performance.",
                            Discount = 10m,
                            Model = "Galaxy Book Pro",
                            Name = "Samsung Galaxy Book Pro",
                            Price = 14990m
                        },
                        new
                        {
                            Id = 36,
                            CategoryID = 2,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The Dell Alienware Aurora R10 is a high-performance gaming desktop with powerful hardware and customizable lighting.",
                            Discount = 20m,
                            Model = "Alienware Aurora R10",
                            Name = "Dell Alienware Aurora R10",
                            Price = 28000m
                        },
                        new
                        {
                            Id = 37,
                            CategoryID = 3,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The HP Omen 15 is a gaming laptop with a sleek design, high-refresh-rate display, and powerful performance for gaming enthusiasts.",
                            Discount = 15m,
                            Model = "Omen 15",
                            Name = "HP Omen 15",
                            Price = 17999m
                        },
                        new
                        {
                            Id = 38,
                            CategoryID = 1,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The Apple MacBook Air is a lightweight and portable laptop with a stunning Retina display and all-day battery life.",
                            Discount = 10m,
                            Model = "MacBook Air",
                            Name = "Apple MacBook Air",
                            Price = 12990m
                        },
                        new
                        {
                            Id = 39,
                            CategoryID = 9,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The Razer Blade 15 is a premium gaming laptop with a sleek design, high-refresh-rate display, and powerful performance.",
                            Discount = 15m,
                            Model = "Blade 15",
                            Name = "Razer Blade 15",
                            Price = 23990m
                        },
                        new
                        {
                            Id = 40,
                            CategoryID = 4,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The Lenovo ThinkPad X1 Carbon is a premium business laptop with a durable build, long battery life, and top-notch performance.",
                            Discount = 60m,
                            Model = "ThinkPad X1 Carbon",
                            Name = "Lenovo ThinkPad X1 Carbon",
                            Price = 18990m
                        },
                        new
                        {
                            Id = 41,
                            CategoryID = 5,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The ASUS ROG Zephyrus G14 is a powerful gaming laptop with an ultra-portable design and impressive performance.",
                            Discount = 0m,
                            Model = "ROG Zephyrus G14",
                            Name = "ASUS ROG Zephyrus G14",
                            Price = 17000m
                        },
                        new
                        {
                            Id = 42,
                            CategoryID = 8,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The MSI GS66 Stealth is a high-performance gaming laptop with a sleek design and powerful components.",
                            Discount = 19m,
                            Model = "GS66 Stealth",
                            Name = "MSI GS66 Stealth",
                            Price = 23999m
                        },
                        new
                        {
                            Id = 43,
                            CategoryID = 8,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The MSI Prestige 14 is a stylish and powerful laptop designed for creative professionals.",
                            Discount = 17m,
                            Model = "Prestige 14",
                            Name = "MSI Prestige 14",
                            Price = 15990m
                        },
                        new
                        {
                            Id = 44,
                            CategoryID = 7,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The Microsoft Surface Laptop 4 is a sleek and versatile laptop with a premium design and excellent performance.",
                            Discount = 14m,
                            Model = "Surface Laptop 4",
                            Name = "Microsoft Surface Laptop 4",
                            Price = 23000m
                        },
                        new
                        {
                            Id = 45,
                            CategoryID = 7,
                            CreateOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentPrice = 0m,
                            Description = "The Microsoft Surface Pro 7 is a powerful 2-in-1 tablet-laptop hybrid with a detachable keyboard and versatile functionality.",
                            Discount = 0m,
                            Model = "Surface Pro 7",
                            Name = "Microsoft Surface Pro 7",
                            Price = 20000m
                        });
                });

            modelBuilder.Entity("DataAccessLayer.Data.Models.Product", b =>
                {
                    b.HasOne("DataAccessLayer.Data.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("DataAccessLayer.Data.Models.Category", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
