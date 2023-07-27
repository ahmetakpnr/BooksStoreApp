using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2918b81a-050e-49bc-b835-7e951566c7fe", "acff3e31-222a-406f-9744-bc7f41f5ae83", "Editor", "EDITOR" },
                    { "cb5555c5-63f8-4b47-b471-29f462e78d6c", "e32748d4-83d0-46e7-b8a1-61334152609a", "User", "USER" },
                    { "d52148de-c682-43ab-a7cb-da3bd1bf6763", "6c60f39d-dab5-46c6-a5b0-5193bb7a8162", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2918b81a-050e-49bc-b835-7e951566c7fe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb5555c5-63f8-4b47-b471-29f462e78d6c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d52148de-c682-43ab-a7cb-da3bd1bf6763");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

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
    }
}
