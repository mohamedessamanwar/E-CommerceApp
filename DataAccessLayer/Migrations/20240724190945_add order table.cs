using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class addordertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateAt",
                table: "shoppingCarts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 24, 22, 9, 45, 253, DateTimeKind.Local).AddTicks(272),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 14, 16, 33, 23, 365, DateTimeKind.Local).AddTicks(4141));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "shoppingCarts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 24, 22, 9, 45, 252, DateTimeKind.Local).AddTicks(9766),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 14, 16, 33, 23, 365, DateTimeKind.Local).AddTicks(3686));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "addresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 24, 22, 9, 45, 250, DateTimeKind.Local).AddTicks(8560),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 14, 16, 33, 23, 363, DateTimeKind.Local).AddTicks(9393));

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShippingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OrderPaymentStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OrderPaymentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    SessionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentIntentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orders_addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "orderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orderDetails_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orderDetails_OrderId",
                table: "orderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_orderDetails_ProductId",
                table: "orderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_AddressId",
                table: "orders",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_UserId",
                table: "orders",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderDetails");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateAt",
                table: "shoppingCarts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 14, 16, 33, 23, 365, DateTimeKind.Local).AddTicks(4141),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 24, 22, 9, 45, 253, DateTimeKind.Local).AddTicks(272));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "shoppingCarts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 14, 16, 33, 23, 365, DateTimeKind.Local).AddTicks(3686),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 24, 22, 9, 45, 252, DateTimeKind.Local).AddTicks(9766));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "addresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 14, 16, 33, 23, 363, DateTimeKind.Local).AddTicks(9393),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 24, 22, 9, 45, 250, DateTimeKind.Local).AddTicks(8560));
        }
    }
}
