using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Break.Module.Core.Migrations
{
    /// <inheritdoc />
    public partial class @break : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BusyCheckers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    busy = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusyCheckers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BrakeTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    busyId = table.Column<int>(type: "int", nullable: true),
                    busyCheckerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrakeTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrakeTimes_BusyCheckers_busyCheckerId",
                        column: x => x.busyCheckerId,
                        principalTable: "BusyCheckers",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DateTimeWorkSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    dateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    BrakeTimeId = table.Column<int>(type: "int", nullable: true),
                    BrakeTimeId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateTimeWorkSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DateTimeWorkSchedules_BrakeTimes_BrakeTimeId",
                        column: x => x.BrakeTimeId,
                        principalTable: "BrakeTimes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DateTimeWorkSchedules_BrakeTimes_BrakeTimeId1",
                        column: x => x.BrakeTimeId1,
                        principalTable: "BrakeTimes",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BrakeTimes_busyCheckerId",
                table: "BrakeTimes",
                column: "busyCheckerId");

            migrationBuilder.CreateIndex(
                name: "IX_DateTimeWorkSchedules_BrakeTimeId",
                table: "DateTimeWorkSchedules",
                column: "BrakeTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_DateTimeWorkSchedules_BrakeTimeId1",
                table: "DateTimeWorkSchedules",
                column: "BrakeTimeId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DateTimeWorkSchedules");

            migrationBuilder.DropTable(
                name: "BrakeTimes");

            migrationBuilder.DropTable(
                name: "BusyCheckers");
        }
    }
}
