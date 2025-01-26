using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeInTimeOut.Module.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class TimeInTimeOut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "comingAndgoings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comingAndgoings", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DateTimeTimeInTimeOuts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TimeIn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TimeOut = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ComingAndgoingId = table.Column<int>(type: "int", nullable: true),
                    ComingAndgoingId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateTimeTimeInTimeOuts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DateTimeTimeInTimeOuts_comingAndgoings_ComingAndgoingId",
                        column: x => x.ComingAndgoingId,
                        principalTable: "comingAndgoings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_DateTimeTimeInTimeOuts_comingAndgoings_ComingAndgoingId1",
                        column: x => x.ComingAndgoingId1,
                        principalTable: "comingAndgoings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DateTimeTimeInTimeOuts_ComingAndgoingId",
                table: "DateTimeTimeInTimeOuts",
                column: "ComingAndgoingId");

            migrationBuilder.CreateIndex(
                name: "IX_DateTimeTimeInTimeOuts_ComingAndgoingId1",
                table: "DateTimeTimeInTimeOuts",
                column: "ComingAndgoingId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DateTimeTimeInTimeOuts");

            migrationBuilder.DropTable(
                name: "comingAndgoings");
        }
    }
}
