using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class RowVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateAt",
                table: "shoppingCarts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 20, 55, 10, 579, DateTimeKind.Local).AddTicks(183),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 30, 1, 7, 27, 401, DateTimeKind.Local).AddTicks(3807));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "shoppingCarts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 20, 55, 10, 578, DateTimeKind.Local).AddTicks(9613),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 30, 1, 7, 27, 401, DateTimeKind.Local).AddTicks(3305));

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Products",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "addresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 20, 55, 10, 576, DateTimeKind.Local).AddTicks(2262),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 30, 1, 7, 27, 399, DateTimeKind.Local).AddTicks(1137));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Products");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateAt",
                table: "shoppingCarts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 30, 1, 7, 27, 401, DateTimeKind.Local).AddTicks(3807),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 20, 55, 10, 579, DateTimeKind.Local).AddTicks(183));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "shoppingCarts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 30, 1, 7, 27, 401, DateTimeKind.Local).AddTicks(3305),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 20, 55, 10, 578, DateTimeKind.Local).AddTicks(9613));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "addresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 30, 1, 7, 27, 399, DateTimeKind.Local).AddTicks(1137),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 20, 55, 10, 576, DateTimeKind.Local).AddTicks(2262));
        }
    }
}
