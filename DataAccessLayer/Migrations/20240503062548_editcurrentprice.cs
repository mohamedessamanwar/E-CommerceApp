using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class editcurrentprice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentPrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "Price * (1 - (Discount / 100))",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComputedColumnSql: "Price * (1 - (Discount / 10))");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentPrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "Price * (1 - (Discount / 10))",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComputedColumnSql: "Price * (1 - (Discount / 100))");
        }
    }
}
