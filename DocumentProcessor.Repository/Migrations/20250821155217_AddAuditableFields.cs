using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentProcessor.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditableFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChangedOn",
                table: "Documents",
                type: "nvarchar(48)",
                maxLength: 48,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                table: "Documents",
                type: "nvarchar(48)",
                maxLength: 48,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "01K33RW7YFCVHMWJM3BT9VSHF8",
                columns: new[] { "ChangedOn", "CreatedOn" },
                values: new object[] { null, "2025-08-21 15:52:16.9885262" });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "01K33RWMYKQTSCGBXMWZTQCCKK",
                columns: new[] { "ChangedOn", "CreatedOn" },
                values: new object[] { null, "2025-08-21 15:52:16.9885448" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangedOn",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Documents");
        }
    }
}
