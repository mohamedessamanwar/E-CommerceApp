using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class addusertoken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateAt",
                table: "shoppingCarts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 29, 23, 57, 49, 182, DateTimeKind.Local).AddTicks(4616),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 21, 19, 43, 56, 727, DateTimeKind.Local).AddTicks(5786));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "shoppingCarts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 29, 23, 57, 49, 182, DateTimeKind.Local).AddTicks(3983),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 21, 19, 43, 56, 727, DateTimeKind.Local).AddTicks(5285));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "addresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 29, 23, 57, 49, 179, DateTimeKind.Local).AddTicks(4445),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 21, 19, 43, 56, 725, DateTimeKind.Local).AddTicks(4706));

            migrationBuilder.CreateTable(
                name: "userTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_userTokens_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_userTokens_ApplicationUserId",
                table: "userTokens",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userTokens");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateAt",
                table: "shoppingCarts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 21, 19, 43, 56, 727, DateTimeKind.Local).AddTicks(5786),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 29, 23, 57, 49, 182, DateTimeKind.Local).AddTicks(4616));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "shoppingCarts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 21, 19, 43, 56, 727, DateTimeKind.Local).AddTicks(5285),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 29, 23, 57, 49, 182, DateTimeKind.Local).AddTicks(3983));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "addresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 21, 19, 43, 56, 725, DateTimeKind.Local).AddTicks(4706),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 29, 23, 57, 49, 179, DateTimeKind.Local).AddTicks(4445));
        }
    }
}
