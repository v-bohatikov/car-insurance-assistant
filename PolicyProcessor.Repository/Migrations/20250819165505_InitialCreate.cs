using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolicyProcessor.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InsurancePlans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    PriceReasoning = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    LifetimeInDays = table.Column<int>(type: "int", nullable: false),
                    PolicyTemplateDocumentId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurancePlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InsurancePolicies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    VehicleId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    InsurancePlanId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    PolicyDocumentId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: true),
                    IssuedOn = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ExpiredAt = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FailureReasoning = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurancePolicies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InsurancePolicies_InsurancePlans_InsurancePlanId",
                        column: x => x.InsurancePlanId,
                        principalTable: "InsurancePlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicies_InsurancePlanId",
                table: "InsurancePolicies",
                column: "InsurancePlanId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicies_UserId_VehicleId",
                table: "InsurancePolicies",
                columns: new[] { "UserId", "VehicleId" });

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicies_UserId_VehicleId_InsurancePlanId",
                table: "InsurancePolicies",
                columns: new[] { "UserId", "VehicleId", "InsurancePlanId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InsurancePolicies");

            migrationBuilder.DropTable(
                name: "InsurancePlans");
        }
    }
}
