using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentProcessor.Repository.Migrations
{
    /// <inheritdoc />
    public partial class FixAuditableFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "01K33RW7YFCVHMWJM3BT9VSHF8",
                column: "CreatedOn",
                value: "2025-08-21 15:53:44");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "01K33RWMYKQTSCGBXMWZTQCCKK",
                column: "CreatedOn",
                value: "2025-08-21 15:53:44");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "01K33RW7YFCVHMWJM3BT9VSHF8",
                column: "CreatedOn",
                value: "2025-08-21 15:52:16.9885262");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "01K33RWMYKQTSCGBXMWZTQCCKK",
                column: "CreatedOn",
                value: "2025-08-21 15:52:16.9885448");
        }
    }
}
