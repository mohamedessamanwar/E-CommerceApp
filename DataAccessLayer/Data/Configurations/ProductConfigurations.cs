using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Data.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> modelBuilder)
        {
            modelBuilder.Property(p => p.CreateOn).HasDefaultValueSql("GETDATE()");
            // modelBuilder.Property(p => p.UpdateOn).HasDefaultValueSql("GETDATE()");
            modelBuilder.Property(p => p.Name).HasMaxLength(255);
            modelBuilder.Property(p => p.Model).HasMaxLength(255);
            modelBuilder.Property(p => p.Discount).HasDefaultValue(0.00);
            modelBuilder.HasIndex(p => p.Name);
            modelBuilder.Property(p => p.Status).HasDefaultValue("Available");
            modelBuilder.HasIndex(P=>P.Description);
            modelBuilder.
            Property(p => p.Name)
           .HasPrecision(18, 2);
            modelBuilder.
            Property(p => p.Discount)
           .HasPrecision(18, 2);
            modelBuilder.
           Property(p => p.CurrentPrice)
           .HasPrecision(18, 2);
            // Adjust the precision and scale as needed
            // modelBuilder.Property(p => p.Description).HasMaxLength(255);
            modelBuilder.Property(b => b.CurrentPrice)
                      .HasComputedColumnSql("Price * (1 - (Discount / 100))");
            modelBuilder.HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryID);

            List<Product> ProductList = new List<Product>
        {
            new Product{Id=1,CategoryID=4,Name="LENOVO Legion 5 Pro",Model="82JQ00TQED",Price=46999 ,
                Description="Processor AMD Ryzen™ 7 5800H(8C / 16T, 3.2 / 4.4GHz, 4MB L2 / 16MB L3)\r\nGraphics\r\nNVIDIA® GeForce RTX™ 3060 6GB GDDR6, Boost Clock 1425 / 1702MHz, TGP 130W\r\nMemory\r\n2x 8GB SO-DIMM DDR4-3200\r\nUp to 32GB DDR4-3200 offering\r\nStorage\r\n1TB SSD M.2 2280 PCIe® 3.0x4 NVMe®\r\n"},
            new Product{Id=2,CategoryID=5,Name="Asus ZenBook 14 UX3402ZA",Model="UX3402ZA-OLED007W",Price=43499,
                Description="Processor: Intel® Core™ i7-1260P 12th Generation 12C / 16T Processor 2.1 GHz (18M Cache, up to 4.7 GHz, 4P+8E cores)\r\nGraphics: \"Intel® Iris Xe Graphics\"\r\nMemory: 16GB LPDDR5 on board\r\nStorage: 1TB M.2 NVMe™ PCIe® 3.0 SSD\r\nDisplay: 14.0-inch, 2.8K (2880 x 1800) OLED 16:10 aspect ratio, 0.2ms response time, 90Hz refresh rate, 400nits, 600nits HDR peak brightness, 100% DCI-P3 /touch screen, (Screen-to-body ratio)90%"},
            new Product{Id=3,CategoryID=4,Name="LENOVO IdeaPad Gaming",Model="LENOVO IdeaPad Gaming",Price=27999,
                Description="Processor\r\nAMD Ryzen 5 5600H (6C / 12T, 3.3 / 4.2GHz, 3MB L2 / 16MB L3)\r\nGraphics\r\nNVIDIA GeForce RTX 3050 Ti 4GB GDDR6, Boost Clock 1485 / 1597.5MHz, TGP 85W\r\nMemory\r\n1x 8GB SO-DIMM DDR4-3200\r\nStorage\r\n256GB SSD M.2 2242 PCIe 3.0x4 NVMe + 1TB HDD\r\nDisplay\r\n15.6\" FHD (1920x1080) IPS 250nits Anti-glare, 45% NTSC, 120Hz\r\nOperating System\r\nWindows 11 Home, English\r\nKeyboard\r\nWhite Backlit, English (US)"},
            new Product{Id=4,CategoryID=3,Name="NOTEBOOK-HP-AMD-15s",Model="eq2009ne",Price=16666,
                Description="AMD Ryzen™ 7 5700U (up to 4.3 GHz max boost clock, 8 MB L3 cache, 8 cores, 16 threads) 1 2 \r\nIntegrated,AMD Radeon™ Graphics .8 GB DDR4-3200 MHz RAM (1 x 8 GB) 512 GB PCIe® NVMe™ M.2 SSD\r\n39.6 cm (15.6\") diagonal, FHD (1920 x 1080), micro-edge, anti-glare, 250 nits, 45% NTSC 3\r\nFull-size, jet black keyboard with numeric keypad"}
        };
            List<Product> productss = new List<Product>
{
    new Product
    {
        Id = 5,
        Name = "Apple MacBook Air",
        Price = 40000,
        Discount = 50,
        Description = "The Apple MacBook Air is a lightweight and portable laptop with excellent battery life.",
        Model = "MacBook Air",
        CategoryID = 1
    },
    new Product
    {
        Id = 6,
        Name = "Apple MacBook Pro",
        Price = 80000,
        Discount =20,
        Description = "The Apple MacBook Pro is a high-performance laptop loved by professionals.",

        Model = "MacBook Pro",
        CategoryID = 1
    },

    new Product
    {
        Id = 7,
        Name = "Dell XPS 13",
        Price = 40000,
        Discount = 0,
        Description = "The Dell XPS 13 is a sleek and powerful laptop with a stunning display.",
        Model = "XPS 13",
        CategoryID = 2
    },
    new Product
    {
        Id = 8,
        Name = "Dell Inspiron 15",
        Price = 35000,
        Discount = 13,
        Description = "The Dell Inspiron 15 is a versatile laptop suitable for everyday use.",
        Model = "Inspiron 15",
        CategoryID = 2
    },
    new Product
    {
        Id = 9,
        Name = "HP Spectre x360",
        Price = 25000,
        Discount = 15,
        Description = "The HP Spectre x360 is a stylish 2-in-1 laptop with powerful performance.",
        Model = "Spectre x360",
        CategoryID = 3
    },
    new Product
    {
        Id = 10,
        Name = "HP Pavilion 14",
        Price = 15000,
        Discount = 60,
        Description = "The HP Pavilion 14 is a budget-friendly laptop with decent specifications.",
        Model = "Pavilion 14",
        CategoryID = 3
    },
        new Product

    {
        Id = 11,
        Name = "Apple MacBook Air",
        Price = 28000,
        Discount = 10,
        Description = "The Apple MacBook Air is a lightweight and portable laptop with excellent battery life.",
        Model = "MacBook Air",
        CategoryID = 1
    },
    new Product
    {
        Id = 12,
        Name = "Apple MacBook Pro",
        Price = 30000,
        Discount = 12,
        Description = "The Apple MacBook Pro is a high-performance laptop loved by professionals.",
        Model = "MacBook Pro",
        CategoryID = 1
    },
    new Product
    {
        Id = 13,
        Name = "Apple iMac",
        Price = 16000,
        Discount = 0,
        Description = "The Apple iMac is a sleek and powerful all-in-one desktop computer.",
        Model = "iMac",
        CategoryID = 1
    },

    new Product
    {
        Id = 14,
        Name = "Dell XPS 13",
        Price = 14000,
        Discount = 90,
        Description = "The Dell XPS 13 is a sleek and powerful laptop with a stunning display.",
        Model = "XPS 13",
        CategoryID = 2
    },
    new Product
    {
        Id = 15,
        Name = "Dell Inspiron 15",
        Price = 30000,
        Discount = 18,
        Description = "The Dell Inspiron 15 is a versatile laptop suitable for everyday use.",
        Model = "Inspiron 15",
        CategoryID = 2
    },
    new Product
    {
        Id = 16,
        Name = "Dell G5 Gaming Desktop",
        Price = 38000,
        Discount = 20,
        Description = "The Dell G5 Gaming Desktop is a powerful gaming machine with immersive graphics.",
        Model = "G5 Gaming Desktop",
        CategoryID = 2
    },

    new Product
    {
        Id = 17,
        Name = "HP Spectre x360",
        Price = 26000,
        Discount = 19,
        Description = "The HP Spectre x360 is a stylish 2-in-1 laptop with powerful performance.",
        Model = "Spectre x360",
        CategoryID = 3
    },
    new Product
    {
        Id = 18,
        Name = "HP Pavilion 14",
        Price = 6000,
        Discount = 0,
        Description = "The HP Pavilion 14 is a budget-friendly laptop with decent specifications.",
        Model = "Pavilion 14",
        CategoryID = 3
    },
    new Product
    {
        Id = 19,
        Name = "HP EliteBook 840",
        Price = 50000,
        Discount = 80,
        Description = "The HP EliteBook 840 is a business laptop with top-notch security features.",
        Model = "EliteBook 840",
        CategoryID = 3
    },

    new Product
    {
        Id = 20,
        Name = "Apple MacBook Air",
        Price = 18000,
        Discount = 15,
        Description = "The Apple MacBook Air is a lightweight and portable laptop with excellent battery life.",
        Model = "MacBook Air",
        CategoryID = 1
    },

    new Product
    {
        Id = 21,
        Name = "Dell XPS 13",
        Price = 13000,
        Discount = 5,
        Description = "The Dell XPS 13 is a sleek and powerful laptop with a stunning display.",
        Model = "XPS 13",
        CategoryID = 2
    },

    new Product
    {
        Id = 22,
        Name = "HP Spectre x360",
        Price = 12000,
        Discount = 10,
        Description = "The HP Spectre x360 is a stylish 2-in-1 laptop with powerful performance.",
        Model = "Spectre x360",
        CategoryID = 3
    },

    new Product
    {
        Id = 23,
        Name = "Lenovo ThinkCentre M720",
        Price = 15000,
        Discount = 6,
        Description = "The Lenovo ThinkCentre M720 is a compact and reliable desktop computer for business use.",
        Model = "ThinkCentre M720",
        CategoryID = 4
    },

    new Product
    {
        Id = 24,
        Name = "ASUS ROG Strix G15",
        Price = 80000,
        Discount = 60,
        Description = "The ASUS ROG Strix G15 is a powerful gaming desktop with RGB lighting and high-performance components.",
        Model = "ROG Strix G15",
        CategoryID = 5
    },
    new Product
    {
        Id = 25,
        Name = "Acer Aspire TC",
        Price = 18000,
        Discount = 15,
        Description = "The Acer Aspire TC is a budget-friendly desktop computer with decent performance.",
        Model = "Aspire TC",
        CategoryID = 6
    },

    new Product
    {
        Id = 26,
        Name = "Dell Inspiron 27",
        Price = 22000,
        Discount = 10,
        Description = "The Dell Inspiron 27 is an all-in-one desktop computer with a large display and powerful performance.",
        Model = "Inspiron 27",
        CategoryID = 2
    },
    new Product
    {
        Id = 27,
        Name = "ASUS ZenBook Pro",
        Price = 28000,
        Discount = 15,
        Description = "The ASUS ZenBook Pro is a premium laptop with a stunning 4K display and high-performance components.",
        Model = "ZenBook Pro",
        CategoryID = 5
    },
    new Product
    {
        Id = 28,
        Name = "HP Pavilion Gaming Desktop",
        Price = 15000,
        Discount = 80,
        Description = "The HP Pavilion Gaming Desktop is a gaming powerhouse with advanced graphics and smooth gameplay.",
        Model = "Pavilion Gaming Desktop",
        CategoryID = 3
    },
    new Product
    {
        Id = 29,
        Name = "Lenovo Legion Y540",
        Price = 20000,
        Discount = 12,
        Description = "The Lenovo Legion Y540 is a gaming laptop with powerful hardware and immersive gaming experience.",
        Model = "Legion Y540",
        CategoryID = 4
    },

    new Product
    {
        Id = 30,
        Name = "Apple iMac",
        Price = 24000,
        Discount = 20,
        Description = "The Apple iMac is a sleek all-in-one desktop computer with a stunning Retina display and powerful performance.",
        Model = "iMac",
        CategoryID = 1
    },
    new Product
    {
        Id = 31,
        Name = "Dell G5 Gaming Laptop",
        Price = 18000,
        Discount = 10,
        Description = "The Dell G5 is a gaming laptop with high-performance hardware and immersive gaming features.",
        Model = "G5 Gaming Laptop",
        CategoryID = 2
    },
    new Product
    {
        Id = 32,
        Name = "HP Envy 15",
        Price = 16000,
        Discount = 15,
        Description = "The HP Envy 15 is a premium laptop with a sleek design and powerful performance for multimedia and productivity tasks.",
        Model = "Envy 15",
        CategoryID = 3
    },
    new Product
    {
        Id = 33,
        Name = "Lenovo IdeaCentre 5",
        Price = 8990,
        Discount = 50,
        Description = "The Lenovo IdeaCentre 5 is a compact and versatile desktop computer suitable for home and office use.",
        Model = "IdeaCentre 5",
        CategoryID = 4
    },

    new Product
    {
        Id = 34,
        Name = "ASUS VivoBook S15",
        Price = 9990,
        Discount = 0,
        Description = "The ASUS VivoBook S15 is a stylish and lightweight laptop with a vibrant display and long battery life.",
        Model = "VivoBook S15",
        CategoryID = 5
    },
    new Product
    {
        Id = 35,
        Name = "Samsung Galaxy Book Pro",
        Price = 14990,
        Discount = 10,
        Description = "The Samsung Galaxy Book Pro is a thin and lightweight laptop with a stunning AMOLED display and powerful performance.",
        Model = "Galaxy Book Pro",
        CategoryID = 10
    },
    new Product
    {
        Id = 36,
        Name = "Dell Alienware Aurora R10",
        Price = 28000,
        Discount = 20,
        Description = "The Dell Alienware Aurora R10 is a high-performance gaming desktop with powerful hardware and customizable lighting.",
        Model = "Alienware Aurora R10",
        CategoryID = 2
    },
    new Product
    {
        Id = 37,
        Name = "HP Omen 15",
        Price = 17999,
        Discount = 15,
        Description = "The HP Omen 15 is a gaming laptop with a sleek design, high-refresh-rate display, and powerful performance for gaming enthusiasts.",
        Model = "Omen 15",
        CategoryID = 3
    },

    new Product
    {
        Id = 38,
        Name = "Apple MacBook Air",
        Price = 12990,
        Discount = 10,
        Description = "The Apple MacBook Air is a lightweight and portable laptop with a stunning Retina display and all-day battery life.",
        Model = "MacBook Air",
        CategoryID = 1
    },
    new Product
    {
        Id = 39,
        Name = "Razer Blade 15",
        Price = 23990,
        Discount = 15,
        Description = "The Razer Blade 15 is a premium gaming laptop with a sleek design, high-refresh-rate display, and powerful performance.",
        Model = "Blade 15",
        CategoryID = 9
    },
    new Product
    {
        Id = 40,
        Name = "Lenovo ThinkPad X1 Carbon",
        Price = 18990,
        Discount = 60,
        Description = "The Lenovo ThinkPad X1 Carbon is a premium business laptop with a durable build, long battery life, and top-notch performance.",
        Model = "ThinkPad X1 Carbon",
        CategoryID = 4
    },
    new Product
    {
        Id = 41,
        Name = "ASUS ROG Zephyrus G14",
        Price = 17000,
        Discount = 0,
        Description = "The ASUS ROG Zephyrus G14 is a powerful gaming laptop with an ultra-portable design and impressive performance.",
        Model = "ROG Zephyrus G14",
        CategoryID = 5
    },
    new Product
    {
        Id = 42,
        Name = "MSI GS66 Stealth",
        Price = 23999,
        Discount = 19,
        Description = "The MSI GS66 Stealth is a high-performance gaming laptop with a sleek design and powerful components.",
        Model = "GS66 Stealth",
        CategoryID = 8
    },
    new Product
    {
        Id = 43,
        Name = "MSI Prestige 14",
        Price = 15990,
        Discount = 17,
        Description = "The MSI Prestige 14 is a stylish and powerful laptop designed for creative professionals.",
        Model = "Prestige 14",
        CategoryID = 8
    },

    new Product
    {
        Id = 44,
        Name = "Microsoft Surface Laptop 4",
        Price = 23000,
        Discount = 14,
        Description = "The Microsoft Surface Laptop 4 is a sleek and versatile laptop with a premium design and excellent performance.",
        Model = "Surface Laptop 4",
        CategoryID = 7
    },
    new Product
    {
        Id = 45,
        Name = "Microsoft Surface Pro 7",
        Price = 20000,
        Discount = 0,
        Description = "The Microsoft Surface Pro 7 is a powerful 2-in-1 tablet-laptop hybrid with a detachable keyboard and versatile functionality.",
        Model = "Surface Pro 7",
        CategoryID = 7
    },



    };


            modelBuilder.HasData(ProductList);
            modelBuilder.HasData(productss);

        }
    }
}

