using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class addreview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateAt",
                table: "shoppingCarts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 20, 1, 18, 355, DateTimeKind.Local).AddTicks(271),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 16, 15, 30, 59, 548, DateTimeKind.Local).AddTicks(7918));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "shoppingCarts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 20, 1, 18, 354, DateTimeKind.Local).AddTicks(9655),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 16, 15, 30, 59, 548, DateTimeKind.Local).AddTicks(7499));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "addresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 20, 1, 18, 352, DateTimeKind.Local).AddTicks(1281),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 16, 15, 30, 59, 546, DateTimeKind.Local).AddTicks(7433));

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Liked = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_reviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_reviews_ProductId",
                table: "reviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_UserId",
                table: "reviews",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateAt",
                table: "shoppingCarts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 16, 15, 30, 59, 548, DateTimeKind.Local).AddTicks(7918),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 20, 1, 18, 355, DateTimeKind.Local).AddTicks(271));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "shoppingCarts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 16, 15, 30, 59, 548, DateTimeKind.Local).AddTicks(7499),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 20, 1, 18, 354, DateTimeKind.Local).AddTicks(9655));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "addresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 16, 15, 30, 59, 546, DateTimeKind.Local).AddTicks(7433),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 20, 1, 18, 352, DateTimeKind.Local).AddTicks(1281));
        }
    }
}
