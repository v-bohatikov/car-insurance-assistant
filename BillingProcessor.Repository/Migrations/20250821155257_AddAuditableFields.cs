using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillingProcessor.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditableFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChangedOn",
                table: "BillingOperations",
                type: "nvarchar(48)",
                maxLength: 48,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                table: "BillingOperations",
                type: "nvarchar(48)",
                maxLength: 48,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangedOn",
                table: "BillingOperations");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "BillingOperations");
        }
    }
}
