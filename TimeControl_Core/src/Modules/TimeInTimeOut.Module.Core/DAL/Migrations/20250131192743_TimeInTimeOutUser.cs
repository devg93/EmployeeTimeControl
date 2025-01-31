using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeInTimeOut.Module.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class TimeInTimeOutUser : Migration
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
                    TimeIn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    TimeOut = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ComingAndgoingId = table.Column<int>(type: "int", nullable: false),
                    OnlineComingAndgoingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateTimeTimeInTimeOuts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DateTimeTimeInTimeOuts_comingAndgoings_ComingAndgoingId",
                        column: x => x.ComingAndgoingId,
                        principalTable: "comingAndgoings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DateTimeTimeInTimeOuts_comingAndgoings_OnlineComingAndgoingId",
                        column: x => x.OnlineComingAndgoingId,
                        principalTable: "comingAndgoings",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DateTimeTimeInTimeOuts_ComingAndgoingId",
                table: "DateTimeTimeInTimeOuts",
                column: "ComingAndgoingId");

            migrationBuilder.CreateIndex(
                name: "IX_DateTimeTimeInTimeOuts_OnlineComingAndgoingId",
                table: "DateTimeTimeInTimeOuts",
                column: "OnlineComingAndgoingId");
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
