using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TestWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    UAN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CareatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CareatedDate", "Name", "Salary", "UAN" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 8, 21, 39, 30, 838, DateTimeKind.Local).AddTicks(7070), "Test1", 1000.0, "ABCD1" },
                    { 2, new DateTime(2024, 3, 8, 21, 39, 30, 838, DateTimeKind.Local).AddTicks(7087), "Test2", 2000.0, "ABCD2" },
                    { 3, new DateTime(2024, 3, 8, 21, 39, 30, 838, DateTimeKind.Local).AddTicks(7090), "Test3", 3000.0, "ABCD3" },
                    { 4, new DateTime(2024, 3, 8, 21, 39, 30, 838, DateTimeKind.Local).AddTicks(7092), "Test4", 4000.0, "ABCD4" },
                    { 5, new DateTime(2024, 3, 8, 21, 39, 30, 838, DateTimeKind.Local).AddTicks(7094), "Test5", 5000.0, "ABCD5" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
