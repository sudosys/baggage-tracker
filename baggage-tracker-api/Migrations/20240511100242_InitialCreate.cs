using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BaggageTrackerApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bt_users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FullName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Password = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bt_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "bt_baggages",
                columns: table => new
                {
                    BaggageId = table.Column<Guid>(type: "uuid", nullable: false),
                    BaggageName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    BaggageStatus = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bt_baggages", x => x.BaggageId);
                    table.ForeignKey(
                        name: "FK_bt_baggages_bt_users_UserId",
                        column: x => x.UserId,
                        principalTable: "bt_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bt_flights",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FlightNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bt_flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bt_flights_bt_users_UserId",
                        column: x => x.UserId,
                        principalTable: "bt_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "bt_users",
                columns: new[] { "Id", "FullName", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1L, "Avery Thompson", "711f24d8676c4462bcb9b8d6ff12e524483afcff5ea9ba726fba772c296b214c", "Passenger", "avery.thompson" },
                    { 2L, "Sebastian Morales", "46498d3d669434f320a45770a9b8ab8cbc16bd7dfeeb724c5503b2cb9d3d395e", "Passenger", "sebastian.morales" },
                    { 3L, "Olivia Martinez", "0cc4bbe5ac4df909798c2ccd0844f15a86a694457758e42d9ce52e7d39e9e256", "Passenger", "olivia.martinez" },
                    { 4L, "Lukas Cruz", "9a18e4407334e70436cc60b0c53e8f384fc4d0934d2bc3c7d70573b63fc72a64", "Personnel", "lukas.cruz" }
                });

            migrationBuilder.InsertData(
                table: "bt_baggages",
                columns: new[] { "BaggageId", "BaggageName", "BaggageStatus", "UserId" },
                values: new object[,]
                {
                    { new Guid("0543b03e-7449-4507-adf2-5d24f248c678"), "Samsonite Popsoda", "Undefined", 2L },
                    { new Guid("0d58ae42-ca7b-43f1-8502-3d5d9b2c4eae"), "Benetti Sports Bag", "Undefined", 1L },
                    { new Guid("55cc9206-a6c7-4f32-8a3f-5e5142ff400e"), "Canvas Explorer Holdall", "Undefined", 3L },
                    { new Guid("6bd8c8db-a5ba-4e29-a7ae-a3786a98ec01"), "Blue Samsonite Case", "Undefined", 1L },
                    { new Guid("8f161fdd-3549-49e6-aa59-1de5ca1383d5"), "Fantana Matrix PP Hard Shell", "Undefined", 3L },
                    { new Guid("b42c7dd7-8fcd-477f-9822-7bd14862e3b8"), "Lightweight PP Collection", "Undefined", 1L }
                });

            migrationBuilder.InsertData(
                table: "bt_flights",
                columns: new[] { "Id", "FlightNumber", "UserId" },
                values: new object[,]
                {
                    { 1L, "TK5094", 1L },
                    { 2L, "TK5094", 2L },
                    { 3L, "TK2745", 3L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_bt_baggages_UserId",
                table: "bt_baggages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_bt_flights_UserId",
                table: "bt_flights",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bt_baggages");

            migrationBuilder.DropTable(
                name: "bt_flights");

            migrationBuilder.DropTable(
                name: "bt_users");
        }
    }
}
