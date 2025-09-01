using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitalDBCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FishInformation",
                columns: table => new
                {
                    FishId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FishName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FishInformation", x => x.FishId);
                });

            migrationBuilder.CreateTable(
                name: "SupplierInformation",
                columns: table => new
                {
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Middlename = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierInformation", x => x.SupplierId);
                });

            migrationBuilder.CreateTable(
                name: "UserDefinition",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Middlename = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDefinition", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "BoatInformation",
                columns: table => new
                {
                    BoatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoatName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    BoatType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoatInformation", x => x.BoatId);
                    table.ForeignKey(
                        name: "FK_BoatInformation_UserDefinition_UserId",
                        column: x => x.UserId,
                        principalTable: "UserDefinition",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CrewInformation",
                columns: table => new
                {
                    CrewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Middlename = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    AdharNo = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    BoatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrewInformation", x => x.CrewID);
                    table.ForeignKey(
                        name: "FK_CrewInformation_BoatInformation_BoatId",
                        column: x => x.BoatId,
                        principalTable: "BoatInformation",
                        principalColumn: "BoatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripInformation",
                columns: table => new
                {
                    TripId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TripStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TripEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TripDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BoatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripInformation", x => x.TripId);
                    table.ForeignKey(
                        name: "FK_TripInformation_BoatInformation_BoatId",
                        column: x => x.BoatId,
                        principalTable: "BoatInformation",
                        principalColumn: "BoatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaveInformation",
                columns: table => new
                {
                    LeaveId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalLeaves = table.Column<int>(type: "int", maxLength: 3, nullable: false),
                    ForYear = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CrewId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveInformation", x => x.LeaveId);
                    table.ForeignKey(
                        name: "FK_LeaveInformation_CrewInformation_CrewId",
                        column: x => x.CrewId,
                        principalTable: "CrewInformation",
                        principalColumn: "CrewID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalaryInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalSalary = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ForYear = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    startDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    endDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CrewId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalaryInformation_CrewInformation_CrewId",
                        column: x => x.CrewId,
                        principalTable: "CrewInformation",
                        principalColumn: "CrewID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TripExpenditures",
                columns: table => new
                {
                    TripExpenditureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TripId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripExpenditures", x => x.TripExpenditureId);
                    table.ForeignKey(
                        name: "FK_TripExpenditures_TripInformation_TripId",
                        column: x => x.TripId,
                        principalTable: "TripInformation",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripParticulars",
                columns: table => new
                {
                    TripParticularId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FishId = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    RatePerKg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TripId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripParticulars", x => x.TripParticularId);
                    table.ForeignKey(
                        name: "FK_TripParticulars_TripInformation_TripId",
                        column: x => x.TripId,
                        principalTable: "TripInformation",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaveSummary",
                columns: table => new
                {
                    LeaveSummaryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoOfDaysOff = table.Column<int>(type: "int", maxLength: 3, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CrewId = table.Column<int>(type: "int", nullable: false),
                    LeaveId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveSummary", x => x.LeaveSummaryId);
                    table.ForeignKey(
                        name: "FK_LeaveSummary_CrewInformation_CrewId",
                        column: x => x.CrewId,
                        principalTable: "CrewInformation",
                        principalColumn: "CrewID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveSummary_LeaveInformation_LeaveId",
                        column: x => x.LeaveId,
                        principalTable: "LeaveInformation",
                        principalColumn: "LeaveId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalarySummary",
                columns: table => new
                {
                    SalarySummaryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmountTaken = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CrewId = table.Column<int>(type: "int", nullable: false),
                    SalaryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalarySummary", x => x.SalarySummaryId);
                    table.ForeignKey(
                        name: "FK_SalarySummary_CrewInformation_CrewId",
                        column: x => x.CrewId,
                        principalTable: "CrewInformation",
                        principalColumn: "CrewID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalarySummary_SalaryInformation_SalaryId",
                        column: x => x.SalaryId,
                        principalTable: "SalaryInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "FishInformation",
                columns: new[] { "FishId", "FishName" },
                values: new object[,]
                {
                    { 1, "Promplet" },
                    { 2, "Halwa" },
                    { 3, "Surmai" },
                    { 4, "Tuna" },
                    { 5, "Pronze" },
                    { 6, "Ribben" },
                    { 7, "White" },
                    { 8, "Lobster" },
                    { 9, "Bangada" },
                    { 10, "Bombil" }
                });

            migrationBuilder.InsertData(
                table: "SupplierInformation",
                columns: new[] { "SupplierId", "Address", "Email", "Firstname", "Lastname", "Middlename", "Phone" },
                values: new object[] { 1, "Harnai", "abczyx@gmail.com", "ABC", "XYZ", "PQR", "8478523693" });

            migrationBuilder.InsertData(
                table: "UserDefinition",
                columns: new[] { "UserId", "Address", "Email", "Firstname", "Lastname", "Middlename", "Password", "Phone" },
                values: new object[,]
                {
                    { 1, "Pune", "Karan@gmail.com", "Karan", "Tabib", "Bhagwan", "1234", "123456789" },
                    { 2, "Pune", "Sagar@gmail.com", "Sagar", "Tabib", "Bhagwan", "4321", "123456789" },
                    { 3, "Pune", "Arvind@gmail.com", "Arvind", "Tabib", "Bhagwan", "7895", "123456789" }
                });

            migrationBuilder.InsertData(
                table: "BoatInformation",
                columns: new[] { "BoatId", "BoatName", "BoatType", "UserId" },
                values: new object[,]
                {
                    { 1, "Bhagwati", "Daldi", 3 },
                    { 2, "Dhanlaxmi", "Trawler", 3 },
                    { 3, "dwarkashish", "Trawler", 1 }
                });

            migrationBuilder.InsertData(
                table: "CrewInformation",
                columns: new[] { "CrewID", "Address", "AdharNo", "BoatId", "DOB", "Firstname", "Gender", "Lastname", "Middlename", "Phone" },
                values: new object[,]
                {
                    { 1, "Paj", "1234567899876543", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harishchandra", 0, "Pawase", "rama", "9082716352" },
                    { 2, "Paj", "1334567899876543", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manoj", 0, "Choagle", "shankar", "9082716353" },
                    { 3, "Paj", "1434567899876543", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vaman", 0, "Pawase", "shiva", "9082716354" },
                    { 4, "Paj", "1534567899876543", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kashinath", 0, "Tabib", "Maya", "9082716355" },
                    { 5, "Paj", "1634567899876543", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ganesh", 0, "Palekar", "Eknath", "9082716356" },
                    { 6, "Paj", "1734567899876543", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Darshan", 0, "Tabib", "Ramesh", "9082716357" },
                    { 7, "Paj", "1834567899876543", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Amol", 0, "Tabib", "Ramesh", "9082716358" },
                    { 8, "Paj", "1934567899876543", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Navnath", 0, "Tabib", "Ramesh", "9082716359" }
                });

            migrationBuilder.InsertData(
                table: "TripInformation",
                columns: new[] { "TripId", "BoatId", "CreatedDate", "TripDescription", "TripEndDate", "TripName", "TripStartDate", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 2, 4, 18, 50, 51, 649, DateTimeKind.Local).AddTicks(8678), "First Trip", new DateTime(2025, 2, 11, 18, 50, 51, 649, DateTimeKind.Local).AddTicks(8669), "First Trip", new DateTime(2025, 2, 4, 18, 50, 51, 649, DateTimeKind.Local).AddTicks(8663), new DateTime(2025, 2, 4, 18, 50, 51, 649, DateTimeKind.Local).AddTicks(8679) },
                    { 2, 1, new DateTime(2025, 2, 4, 18, 50, 51, 649, DateTimeKind.Local).AddTicks(8687), "Second Trip", new DateTime(2025, 2, 11, 18, 50, 51, 649, DateTimeKind.Local).AddTicks(8685), "Second Trip", new DateTime(2025, 2, 4, 18, 50, 51, 649, DateTimeKind.Local).AddTicks(8684), new DateTime(2025, 2, 4, 18, 50, 51, 649, DateTimeKind.Local).AddTicks(8687) },
                    { 3, 1, new DateTime(2025, 2, 4, 18, 50, 51, 649, DateTimeKind.Local).AddTicks(8691), "Third Trip", new DateTime(2025, 2, 11, 18, 50, 51, 649, DateTimeKind.Local).AddTicks(8689), "Third Trip", new DateTime(2025, 2, 4, 18, 50, 51, 649, DateTimeKind.Local).AddTicks(8689), new DateTime(2025, 2, 4, 18, 50, 51, 649, DateTimeKind.Local).AddTicks(8691) }
                });

            migrationBuilder.InsertData(
                table: "SalaryInformation",
                columns: new[] { "Id", "CrewId", "ForYear", "TotalSalary", "endDate", "startDate" },
                values: new object[,]
                {
                    { 1, 1, 2024, 200000m, new DateTime(2026, 2, 4, 18, 50, 51, 649, DateTimeKind.Local), new DateTime(2025, 2, 4, 18, 50, 51, 648, DateTimeKind.Local).AddTicks(9981) },
                    { 2, 2, 2024, 220000m, new DateTime(2026, 2, 4, 18, 50, 51, 649, DateTimeKind.Local).AddTicks(19), new DateTime(2025, 2, 4, 18, 50, 51, 649, DateTimeKind.Local).AddTicks(18) },
                    { 3, 3, 2024, 240000m, new DateTime(2026, 2, 4, 18, 50, 51, 649, DateTimeKind.Local).AddTicks(23), new DateTime(2025, 2, 4, 18, 50, 51, 649, DateTimeKind.Local).AddTicks(22) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoatInformation_UserId",
                table: "BoatInformation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CrewInformation_BoatId",
                table: "CrewInformation",
                column: "BoatId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveInformation_CrewId",
                table: "LeaveInformation",
                column: "CrewId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeaveSummary_CrewId",
                table: "LeaveSummary",
                column: "CrewId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveSummary_LeaveId",
                table: "LeaveSummary",
                column: "LeaveId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryInformation_CrewId",
                table: "SalaryInformation",
                column: "CrewId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalarySummary_CrewId",
                table: "SalarySummary",
                column: "CrewId");

            migrationBuilder.CreateIndex(
                name: "IX_SalarySummary_SalaryId",
                table: "SalarySummary",
                column: "SalaryId");

            migrationBuilder.CreateIndex(
                name: "IX_TripExpenditures_TripId",
                table: "TripExpenditures",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_TripInformation_BoatId",
                table: "TripInformation",
                column: "BoatId");

            migrationBuilder.CreateIndex(
                name: "IX_TripParticulars_TripId",
                table: "TripParticulars",
                column: "TripId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FishInformation");

            migrationBuilder.DropTable(
                name: "LeaveSummary");

            migrationBuilder.DropTable(
                name: "SalarySummary");

            migrationBuilder.DropTable(
                name: "SupplierInformation");

            migrationBuilder.DropTable(
                name: "TripExpenditures");

            migrationBuilder.DropTable(
                name: "TripParticulars");

            migrationBuilder.DropTable(
                name: "LeaveInformation");

            migrationBuilder.DropTable(
                name: "SalaryInformation");

            migrationBuilder.DropTable(
                name: "TripInformation");

            migrationBuilder.DropTable(
                name: "CrewInformation");

            migrationBuilder.DropTable(
                name: "BoatInformation");

            migrationBuilder.DropTable(
                name: "UserDefinition");
        }
    }
}
