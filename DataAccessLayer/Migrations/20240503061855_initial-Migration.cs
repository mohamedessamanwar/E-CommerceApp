using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreateOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdateOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrentPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, computedColumnSql: "Price * (1 - (Discount / 10))"),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdateOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "Id", "Name", "UpdateOn" },
                values: new object[,]
                {
                    { 1, "Apple", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Dell", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "HP", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Lenovo", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "ASUS", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Acer", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Microsoft", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "MSI", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "Razer", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "Samsung", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryID", "Description", "Discount", "Model", "Name", "Price", "UpdateOn" },
                values: new object[,]
                {
                    { 1, 4, "Processor AMD Ryzen™ 7 5800H(8C / 16T, 3.2 / 4.4GHz, 4MB L2 / 16MB L3)\r\nGraphics\r\nNVIDIA® GeForce RTX™ 3060 6GB GDDR6, Boost Clock 1425 / 1702MHz, TGP 130W\r\nMemory\r\n2x 8GB SO-DIMM DDR4-3200\r\nUp to 32GB DDR4-3200 offering\r\nStorage\r\n1TB SSD M.2 2280 PCIe® 3.0x4 NVMe®\r\n", 0m, "82JQ00TQED", "LENOVO Legion 5 Pro", 46999m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 5, "Processor: Intel® Core™ i7-1260P 12th Generation 12C / 16T Processor 2.1 GHz (18M Cache, up to 4.7 GHz, 4P+8E cores)\r\nGraphics: \"Intel® Iris Xe Graphics\"\r\nMemory: 16GB LPDDR5 on board\r\nStorage: 1TB M.2 NVMe™ PCIe® 3.0 SSD\r\nDisplay: 14.0-inch, 2.8K (2880 x 1800) OLED 16:10 aspect ratio, 0.2ms response time, 90Hz refresh rate, 400nits, 600nits HDR peak brightness, 100% DCI-P3 /touch screen, (Screen-to-body ratio)90%", 0m, "UX3402ZA-OLED007W", "Asus ZenBook 14 UX3402ZA", 43499m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 4, "Processor\r\nAMD Ryzen 5 5600H (6C / 12T, 3.3 / 4.2GHz, 3MB L2 / 16MB L3)\r\nGraphics\r\nNVIDIA GeForce RTX 3050 Ti 4GB GDDR6, Boost Clock 1485 / 1597.5MHz, TGP 85W\r\nMemory\r\n1x 8GB SO-DIMM DDR4-3200\r\nStorage\r\n256GB SSD M.2 2242 PCIe 3.0x4 NVMe + 1TB HDD\r\nDisplay\r\n15.6\" FHD (1920x1080) IPS 250nits Anti-glare, 45% NTSC, 120Hz\r\nOperating System\r\nWindows 11 Home, English\r\nKeyboard\r\nWhite Backlit, English (US)", 0m, "LENOVO IdeaPad Gaming", "LENOVO IdeaPad Gaming", 27999m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 3, "AMD Ryzen™ 7 5700U (up to 4.3 GHz max boost clock, 8 MB L3 cache, 8 cores, 16 threads) 1 2 \r\nIntegrated,AMD Radeon™ Graphics .8 GB DDR4-3200 MHz RAM (1 x 8 GB) 512 GB PCIe® NVMe™ M.2 SSD\r\n39.6 cm (15.6\") diagonal, FHD (1920 x 1080), micro-edge, anti-glare, 250 nits, 45% NTSC 3\r\nFull-size, jet black keyboard with numeric keypad", 0m, "eq2009ne", "NOTEBOOK-HP-AMD-15s", 16666m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 1, "The Apple MacBook Air is a lightweight and portable laptop with excellent battery life.", 50m, "MacBook Air", "Apple MacBook Air", 40000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 1, "The Apple MacBook Pro is a high-performance laptop loved by professionals.", 20m, "MacBook Pro", "Apple MacBook Pro", 80000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 2, "The Dell XPS 13 is a sleek and powerful laptop with a stunning display.", 0m, "XPS 13", "Dell XPS 13", 40000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 2, "The Dell Inspiron 15 is a versatile laptop suitable for everyday use.", 13m, "Inspiron 15", "Dell Inspiron 15", 35000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 3, "The HP Spectre x360 is a stylish 2-in-1 laptop with powerful performance.", 15m, "Spectre x360", "HP Spectre x360", 25000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 3, "The HP Pavilion 14 is a budget-friendly laptop with decent specifications.", 60m, "Pavilion 14", "HP Pavilion 14", 15000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 1, "The Apple MacBook Air is a lightweight and portable laptop with excellent battery life.", 10m, "MacBook Air", "Apple MacBook Air", 28000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 1, "The Apple MacBook Pro is a high-performance laptop loved by professionals.", 12m, "MacBook Pro", "Apple MacBook Pro", 30000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, 1, "The Apple iMac is a sleek and powerful all-in-one desktop computer.", 0m, "iMac", "Apple iMac", 16000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, 2, "The Dell XPS 13 is a sleek and powerful laptop with a stunning display.", 90m, "XPS 13", "Dell XPS 13", 14000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, 2, "The Dell Inspiron 15 is a versatile laptop suitable for everyday use.", 18m, "Inspiron 15", "Dell Inspiron 15", 30000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, 2, "The Dell G5 Gaming Desktop is a powerful gaming machine with immersive graphics.", 20m, "G5 Gaming Desktop", "Dell G5 Gaming Desktop", 38000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, 3, "The HP Spectre x360 is a stylish 2-in-1 laptop with powerful performance.", 19m, "Spectre x360", "HP Spectre x360", 26000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, 3, "The HP Pavilion 14 is a budget-friendly laptop with decent specifications.", 0m, "Pavilion 14", "HP Pavilion 14", 6000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, 3, "The HP EliteBook 840 is a business laptop with top-notch security features.", 80m, "EliteBook 840", "HP EliteBook 840", 50000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, 1, "The Apple MacBook Air is a lightweight and portable laptop with excellent battery life.", 15m, "MacBook Air", "Apple MacBook Air", 18000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, 2, "The Dell XPS 13 is a sleek and powerful laptop with a stunning display.", 5m, "XPS 13", "Dell XPS 13", 13000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22, 3, "The HP Spectre x360 is a stylish 2-in-1 laptop with powerful performance.", 10m, "Spectre x360", "HP Spectre x360", 12000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23, 4, "The Lenovo ThinkCentre M720 is a compact and reliable desktop computer for business use.", 6m, "ThinkCentre M720", "Lenovo ThinkCentre M720", 15000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24, 5, "The ASUS ROG Strix G15 is a powerful gaming desktop with RGB lighting and high-performance components.", 60m, "ROG Strix G15", "ASUS ROG Strix G15", 80000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25, 6, "The Acer Aspire TC is a budget-friendly desktop computer with decent performance.", 15m, "Aspire TC", "Acer Aspire TC", 18000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 26, 2, "The Dell Inspiron 27 is an all-in-one desktop computer with a large display and powerful performance.", 10m, "Inspiron 27", "Dell Inspiron 27", 22000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 27, 5, "The ASUS ZenBook Pro is a premium laptop with a stunning 4K display and high-performance components.", 15m, "ZenBook Pro", "ASUS ZenBook Pro", 28000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 28, 3, "The HP Pavilion Gaming Desktop is a gaming powerhouse with advanced graphics and smooth gameplay.", 80m, "Pavilion Gaming Desktop", "HP Pavilion Gaming Desktop", 15000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 29, 4, "The Lenovo Legion Y540 is a gaming laptop with powerful hardware and immersive gaming experience.", 12m, "Legion Y540", "Lenovo Legion Y540", 20000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 30, 1, "The Apple iMac is a sleek all-in-one desktop computer with a stunning Retina display and powerful performance.", 20m, "iMac", "Apple iMac", 24000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 31, 2, "The Dell G5 is a gaming laptop with high-performance hardware and immersive gaming features.", 10m, "G5 Gaming Laptop", "Dell G5 Gaming Laptop", 18000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 32, 3, "The HP Envy 15 is a premium laptop with a sleek design and powerful performance for multimedia and productivity tasks.", 15m, "Envy 15", "HP Envy 15", 16000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 33, 4, "The Lenovo IdeaCentre 5 is a compact and versatile desktop computer suitable for home and office use.", 50m, "IdeaCentre 5", "Lenovo IdeaCentre 5", 8990m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 34, 5, "The ASUS VivoBook S15 is a stylish and lightweight laptop with a vibrant display and long battery life.", 0m, "VivoBook S15", "ASUS VivoBook S15", 9990m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 35, 10, "The Samsung Galaxy Book Pro is a thin and lightweight laptop with a stunning AMOLED display and powerful performance.", 10m, "Galaxy Book Pro", "Samsung Galaxy Book Pro", 14990m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 36, 2, "The Dell Alienware Aurora R10 is a high-performance gaming desktop with powerful hardware and customizable lighting.", 20m, "Alienware Aurora R10", "Dell Alienware Aurora R10", 28000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 37, 3, "The HP Omen 15 is a gaming laptop with a sleek design, high-refresh-rate display, and powerful performance for gaming enthusiasts.", 15m, "Omen 15", "HP Omen 15", 17999m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 38, 1, "The Apple MacBook Air is a lightweight and portable laptop with a stunning Retina display and all-day battery life.", 10m, "MacBook Air", "Apple MacBook Air", 12990m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 39, 9, "The Razer Blade 15 is a premium gaming laptop with a sleek design, high-refresh-rate display, and powerful performance.", 15m, "Blade 15", "Razer Blade 15", 23990m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 40, 4, "The Lenovo ThinkPad X1 Carbon is a premium business laptop with a durable build, long battery life, and top-notch performance.", 60m, "ThinkPad X1 Carbon", "Lenovo ThinkPad X1 Carbon", 18990m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 41, 5, "The ASUS ROG Zephyrus G14 is a powerful gaming laptop with an ultra-portable design and impressive performance.", 0m, "ROG Zephyrus G14", "ASUS ROG Zephyrus G14", 17000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 42, 8, "The MSI GS66 Stealth is a high-performance gaming laptop with a sleek design and powerful components.", 19m, "GS66 Stealth", "MSI GS66 Stealth", 23999m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 43, 8, "The MSI Prestige 14 is a stylish and powerful laptop designed for creative professionals.", 17m, "Prestige 14", "MSI Prestige 14", 15990m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 44, 7, "The Microsoft Surface Laptop 4 is a sleek and versatile laptop with a premium design and excellent performance.", 14m, "Surface Laptop 4", "Microsoft Surface Laptop 4", 23000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 45, 7, "The Microsoft Surface Pro 7 is a powerful 2-in-1 tablet-laptop hybrid with a detachable keyboard and versatile functionality.", 0m, "Surface Pro 7", "Microsoft Surface Pro 7", 20000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "categories");
        }
    }
}
