using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RRSCONTROLLER.Migrations
{
    /// <inheritdoc />
    public partial class migracion1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTS_Name",
                table: "PRODUCTS",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MENUS_Name",
                table: "MENUS",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FOODS_Name",
                table: "FOODS",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PRODUCTS_Name",
                table: "PRODUCTS");

            migrationBuilder.DropIndex(
                name: "IX_MENUS_Name",
                table: "MENUS");

            migrationBuilder.DropIndex(
                name: "IX_FOODS_Name",
                table: "FOODS");
        }
    }
}
