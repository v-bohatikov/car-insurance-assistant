using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PolicyProcessor.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "InsurancePlans",
                columns: new[] { "Id", "LifetimeInDays", "Name", "PolicyTemplateDocumentId", "Price", "PriceReasoning" },
                values: new object[,]
                {
                    { "01K33RQRZ3MA3CJFR6JMBM8HXF", 7, "Basic Plan", "01K33RW7YFCVHMWJM3BT9VSHF8", 25m, "This plan provides basic level of protection and services. NOTE: Price if fixed for all clients for this insurance plan." },
                    { "01K33RT9B4R5H40M0S501EWAYN", 7, "Premium Plan", "01K33RWMYKQTSCGBXMWZTQCCKK", 100m, "This plan provides premium level of protection and services. NOTE: Price if fixed for all clients for this insurance plan." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: "01K33RQRZ3MA3CJFR6JMBM8HXF");

            migrationBuilder.DeleteData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: "01K33RT9B4R5H40M0S501EWAYN");
        }
    }
}
