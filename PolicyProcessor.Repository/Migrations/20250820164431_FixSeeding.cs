using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolicyProcessor.Repository.Migrations
{
    /// <inheritdoc />
    public partial class FixSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: "01K33RQRZ3MA3CJFR6JMBM8HXF",
                column: "PriceReasoning",
                value: "This plan provides basic level of protection and services. NOTE: Price is fixed for all clients for this insurance plan.");

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: "01K33RT9B4R5H40M0S501EWAYN",
                column: "PriceReasoning",
                value: "This plan provides premium level of protection and services. NOTE: Price is fixed for all clients for this insurance plan.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: "01K33RQRZ3MA3CJFR6JMBM8HXF",
                column: "PriceReasoning",
                value: "This plan provides basic level of protection and services. NOTE: Price if fixed for all clients for this insurance plan.");

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: "01K33RT9B4R5H40M0S501EWAYN",
                column: "PriceReasoning",
                value: "This plan provides premium level of protection and services. NOTE: Price if fixed for all clients for this insurance plan.");
        }
    }
}
