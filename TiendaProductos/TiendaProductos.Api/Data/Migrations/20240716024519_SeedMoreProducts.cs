using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaProductos.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoreProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[] { 3, "Tablet", 800.00m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
