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
                    Role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bt_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "bt_baggages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TagNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    BaggageStatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bt_baggages", x => x.Id);
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
                    { 1L, "Avery Thompson", "711f24d8676c4462bcb9b8d6ff12e524483afcff5ea9ba726fba772c296b214c", 0, "avery.thompson" },
                    { 2L, "Sebastian Morales", "46498d3d669434f320a45770a9b8ab8cbc16bd7dfeeb724c5503b2cb9d3d395e", 0, "sebastian.morales" },
                    { 3L, "Olivia Martinez", "0cc4bbe5ac4df909798c2ccd0844f15a86a694457758e42d9ce52e7d39e9e256", 0, "olivia.martinez" }
                });

            migrationBuilder.InsertData(
                table: "bt_baggages",
                columns: new[] { "Id", "BaggageStatus", "TagNumber", "UserId" },
                values: new object[,]
                {
                    { 1L, 0, "T436712", 1L },
                    { 2L, 0, "T377053", 1L },
                    { 3L, 0, "T205967", 1L },
                    { 4L, 0, "T519736", 2L },
                    { 5L, 0, "T724821", 3L },
                    { 6L, 0, "T541263", 3L }
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
