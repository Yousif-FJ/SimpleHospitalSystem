using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleHospitalModel.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bed",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomNumber = table.Column<string>(maxLength: 32, nullable: true),
                    BedNumber = table.Column<string>(maxLength: 32, nullable: true),
                    DepartmentId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bed_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Age = table.Column<int>(nullable: false),
                    BedId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patient_Bed_BedId",
                        column: x => x.BedId,
                        principalTable: "Bed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ClinicalData",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(nullable: false),
                    ChiefComplaint = table.Column<string>(maxLength: 10000, nullable: true),
                    History = table.Column<string>(maxLength: 10000, nullable: true),
                    Diagnosis = table.Column<string>(maxLength: 10000, nullable: true),
                    PatientId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClinicalData_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bed_DepartmentId",
                table: "Bed",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalData_PatientId",
                table: "ClinicalData",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_BedId",
                table: "Patient",
                column: "BedId",
                unique: true,
                filter: "[BedId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClinicalData");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "Bed");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
