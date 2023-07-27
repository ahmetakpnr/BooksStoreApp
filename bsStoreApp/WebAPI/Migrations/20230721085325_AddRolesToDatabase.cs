using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "620548c2-8702-4581-ae38-1653b52210f6", "0181a014-823d-43ce-b206-fa667de9e5e9", "Editor", "EDITOR" },
                    { "64eefbac-1a04-4fae-ae6e-ea1a6551be21", "394ab7ae-9b11-47b8-ac21-12cce9767633", "User", "USER" },
                    { "7f32dcaf-dee6-4db5-bac9-0cb4301c8859", "720a4542-e140-466e-98b0-82438175cb09", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "620548c2-8702-4581-ae38-1653b52210f6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "64eefbac-1a04-4fae-ae6e-ea1a6551be21");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f32dcaf-dee6-4db5-bac9-0cb4301c8859");
        }
    }
}
