using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolicyProcessor.Repository.Migrations
{
    /// <inheritdoc />
    public partial class FixAuditableFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: "01K33RQRZ3MA3CJFR6JMBM8HXF",
                column: "CreatedOn",
                value: "2025-08-21 15:53:44");

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: "01K33RT9B4R5H40M0S501EWAYN",
                columns: new[] { "CreatedOn", "PriceReasoning" },
                values: new object[] { "2025-08-21 15:53:44", "This plan provides premium level of protection and services. NOTE: Price is entity fixed for all clients for this insurance plan." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: "01K33RQRZ3MA3CJFR6JMBM8HXF",
                column: "CreatedOn",
                value: "2025-08-21 15:53:44.8502618");

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: "01K33RT9B4R5H40M0S501EWAYN",
                columns: new[] { "CreatedOn", "PriceReasoning" },
                values: new object[] { "2025-08-21 15:53:44.8502892", "This plan provides premium level of protection and services. NOTE: Price isentity fixed for all clients for this insurance plan." });
        }
    }
}
