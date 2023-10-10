using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TourAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixTourProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<string>(
                name: "DurationDays",
                table: "Tours",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "007a8386-eb65-4867-90ca-634feb5a1f6c", "1", "Admin", "Admin" },
                    { "22229c7e-fc4a-4a81-99fe-9b422fab800f", "3", "HR", "HumanResources" },
                    { "32c4924a-c63c-4f44-8790-a0659a8721b1", "2", "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "007a8386-eb65-4867-90ca-634feb5a1f6c");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "22229c7e-fc4a-4a81-99fe-9b422fab800f");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "32c4924a-c63c-4f44-8790-a0659a8721b1");

            migrationBuilder.AlterColumn<int>(
                name: "DurationDays",
                table: "Tours",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a126143f-5450-4013-92c5-8ad336c47b9b", "1", "Admin", "Admin" },
                    { "a4b68645-53c8-4b33-81db-77da58971d53", "2", "User", "User" },
                    { "d7a99e4e-8958-4899-b28d-6f1e41ac8fff", "3", "HR", "HumanResources" }
                });
        }
    }
}
