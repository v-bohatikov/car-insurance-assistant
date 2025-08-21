using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolicyProcessor.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditableFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChangedOn",
                table: "InsurancePolicies",
                type: "nvarchar(48)",
                maxLength: 48,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                table: "InsurancePolicies",
                type: "nvarchar(48)",
                maxLength: 48,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ChangedOn",
                table: "InsurancePlans",
                type: "nvarchar(48)",
                maxLength: 48,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                table: "InsurancePlans",
                type: "nvarchar(48)",
                maxLength: 48,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: "01K33RQRZ3MA3CJFR6JMBM8HXF",
                columns: new[] { "ChangedOn", "CreatedOn" },
                values: new object[] { null, "2025-08-21 15:53:44.8502618" });

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: "01K33RT9B4R5H40M0S501EWAYN",
                columns: new[] { "ChangedOn", "CreatedOn", "PriceReasoning" },
                values: new object[] { null, "2025-08-21 15:53:44.8502892", "This plan provides premium level of protection and services. NOTE: Price isentity fixed for all clients for this insurance plan." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangedOn",
                table: "InsurancePolicies");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "InsurancePolicies");

            migrationBuilder.DropColumn(
                name: "ChangedOn",
                table: "InsurancePlans");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "InsurancePlans");

            migrationBuilder.UpdateData(
                table: "InsurancePlans",
                keyColumn: "Id",
                keyValue: "01K33RT9B4R5H40M0S501EWAYN",
                column: "PriceReasoning",
                value: "This plan provides premium level of protection and services. NOTE: Price is fixed for all clients for this insurance plan.");
        }
    }
}
