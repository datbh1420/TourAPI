using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TourAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddLocationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Location_LocationId",
                table: "Tours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Location");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "0be2dcc9-c884-4856-978c-08b24ea1cda5");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "bf7c86a1-9c4d-4718-82ed-b3bdf1b275f1");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d246e557-1f8e-4947-b6bb-5b2a46c10e08");

            migrationBuilder.RenameTable(
                name: "Location",
                newName: "Locations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a126143f-5450-4013-92c5-8ad336c47b9b", "1", "Admin", "Admin" },
                    { "a4b68645-53c8-4b33-81db-77da58971d53", "2", "User", "User" },
                    { "d7a99e4e-8958-4899-b28d-6f1e41ac8fff", "3", "HR", "HumanResources" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Locations_LocationId",
                table: "Tours",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Locations_LocationId",
                table: "Tours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a126143f-5450-4013-92c5-8ad336c47b9b");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a4b68645-53c8-4b33-81db-77da58971d53");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d7a99e4e-8958-4899-b28d-6f1e41ac8fff");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "Location");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Location",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0be2dcc9-c884-4856-978c-08b24ea1cda5", "1", "Admin", "Admin" },
                    { "bf7c86a1-9c4d-4718-82ed-b3bdf1b275f1", "2", "User", "User" },
                    { "d246e557-1f8e-4947-b6bb-5b2a46c10e08", "3", "HR", "HumanResources" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Location_LocationId",
                table: "Tours",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id");
        }
    }
}
