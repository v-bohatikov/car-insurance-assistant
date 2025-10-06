using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillingProcessor.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillingOperations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    OperationStatus = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    FailureReasoning = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingOperations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillingOperations_OrderId",
                table: "BillingOperations",
                column: "OrderId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillingOperations");
        }
    }
}
