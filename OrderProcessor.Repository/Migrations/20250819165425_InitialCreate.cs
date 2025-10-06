using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderProcessor.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    VehicleId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    InsurancePlanId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    FailureReasoning = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId_VehicleId",
                table: "Orders",
                columns: new[] { "UserId", "VehicleId" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId_VehicleId_InsurancePlanId",
                table: "Orders",
                columns: new[] { "UserId", "VehicleId", "InsurancePlanId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
