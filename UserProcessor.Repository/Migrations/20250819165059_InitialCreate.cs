using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserProcessor.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UserTelegramId = table.Column<long>(type: "bigint", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPassports",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    DocumentId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    GivenNames = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PassportNumber = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DateOfBirth = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPassports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPassports_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DocumentId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    OwnerName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    VehicleName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    VehicleIdentificationNumber = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: false),
                    PlateNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPassports_PassportNumber",
                table: "UserPassports",
                column: "PassportNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPassports_UserId",
                table: "UserPassports",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber_UserTelegramId",
                table: "Users",
                columns: new[] { "PhoneNumber", "UserTelegramId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserTelegramId",
                table: "Users",
                column: "UserTelegramId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_PlateNumber_RegistrationNumber_VehicleIdentificationNumber",
                table: "Vehicles",
                columns: new[] { "PlateNumber", "RegistrationNumber", "VehicleIdentificationNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_RegistrationNumber_VehicleIdentificationNumber",
                table: "Vehicles",
                columns: new[] { "RegistrationNumber", "VehicleIdentificationNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_UserId",
                table: "Vehicles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleIdentificationNumber",
                table: "Vehicles",
                column: "VehicleIdentificationNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPassports");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
