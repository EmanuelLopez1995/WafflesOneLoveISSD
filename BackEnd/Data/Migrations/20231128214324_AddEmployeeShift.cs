using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddEmployeeShift : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {



        migrationBuilder.CreateTable(
                name: "EmployeeShifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartTimeHours = table.Column<int>(type: "int", nullable: false),
                    StartTimeMinutes = table.Column<int>(type: "int", nullable: true),
                    EndTimeHours = table.Column<int>(type: "int", nullable: false),
                    EndTimeMinutes = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<String>(type: "string", nullable: true),
                    Cashier = table.Column<bool>(type: "bit", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeShifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeShifts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeShifts_EmployeeId",
                table: "EmployeeShifts",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeShifts");
        }
    }
}
