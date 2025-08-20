using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DocumentProcessor.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "Id", "DocumentDownloadUrl", "DocumentType", "FileId" },
                values: new object[,]
                {
                    { "01K33RW7YFCVHMWJM3BT9VSHF8", null, "InsurancePolicyTemplate", "01K33SHJFHVVW9AC1YEC8FJ129" },
                    { "01K33RWMYKQTSCGBXMWZTQCCKK", null, "InsurancePolicyTemplate", "01K33SKD4PKGDZCG5S73NDD4PP" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "01K33RW7YFCVHMWJM3BT9VSHF8");

            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: "01K33RWMYKQTSCGBXMWZTQCCKK");
        }
    }
}
