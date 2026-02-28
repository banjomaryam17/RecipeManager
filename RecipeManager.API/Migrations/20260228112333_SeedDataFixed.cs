using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeManager.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$N9qo8uLOickgx2ZMRZoMyeIjZAgcfl7p92ldGxad68LJZdL17lhWy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$dZCsHFde6xc0bSS63AGTa.2DP7Rnew3fSZrlzXbYG36wjlLUGpH2m");
        }
    }
}
