using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class addshoppingCart2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateAt",
                table: "shoppingCarts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 14, 16, 33, 23, 365, DateTimeKind.Local).AddTicks(4141),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "shoppingCarts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 14, 16, 33, 23, 365, DateTimeKind.Local).AddTicks(3686),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "addresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 14, 16, 33, 23, 363, DateTimeKind.Local).AddTicks(9393),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 14, 15, 45, 39, 112, DateTimeKind.Local).AddTicks(9151));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateAt",
                table: "shoppingCarts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 14, 16, 33, 23, 365, DateTimeKind.Local).AddTicks(4141));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "shoppingCarts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 14, 16, 33, 23, 365, DateTimeKind.Local).AddTicks(3686));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "addresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 14, 15, 45, 39, 112, DateTimeKind.Local).AddTicks(9151),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 14, 16, 33, 23, 363, DateTimeKind.Local).AddTicks(9393));
        }
    }
}
