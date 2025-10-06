using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserProcessor.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditableFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChangedOn",
                table: "Vehicles",
                type: "nvarchar(48)",
                maxLength: 48,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                table: "Vehicles",
                type: "nvarchar(48)",
                maxLength: 48,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ChangedOn",
                table: "Users",
                type: "nvarchar(48)",
                maxLength: 48,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                table: "Users",
                type: "nvarchar(48)",
                maxLength: 48,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ChangedOn",
                table: "UserPassports",
                type: "nvarchar(48)",
                maxLength: 48,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                table: "UserPassports",
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
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "ChangedOn",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ChangedOn",
                table: "UserPassports");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "UserPassports");
        }
    }
}
