using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Break.Module.Core.Modules.Break.Module.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Breake : Migration
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
                    busyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrakeTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrakeTimes_BusyCheckers_busyId",
                        column: x => x.busyId,
                        principalTable: "BusyCheckers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DateTimeWorkSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    BrakeId = table.Column<int>(type: "int", nullable: true),
                    BrakeTimeStartId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateTimeWorkSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DateTimeWorkSchedules_BrakeTimes_BrakeId",
                        column: x => x.BrakeId,
                        principalTable: "BrakeTimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_DateTimeWorkSchedules_BrakeTimes_BrakeTimeStartId",
                        column: x => x.BrakeTimeStartId,
                        principalTable: "BrakeTimes",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BrakeTimes_busyId",
                table: "BrakeTimes",
                column: "busyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DateTimeWorkSchedules_BrakeId",
                table: "DateTimeWorkSchedules",
                column: "BrakeId");

            migrationBuilder.CreateIndex(
                name: "IX_DateTimeWorkSchedules_BrakeTimeStartId",
                table: "DateTimeWorkSchedules",
                column: "BrakeTimeStartId");
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
