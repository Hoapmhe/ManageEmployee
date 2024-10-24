using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageEmployee.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressToEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommuneId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProvinceId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CommuneId",
                table: "Employees",
                column: "CommuneId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DistrictId",
                table: "Employees",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ProvinceId",
                table: "Employees",
                column: "ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Commune_CommuneId",
                table: "Employees",
                column: "CommuneId",
                principalTable: "Commune",
                principalColumn: "CommuneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_District_DistrictId",
                table: "Employees",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Province_ProvinceId",
                table: "Employees",
                column: "ProvinceId",
                principalTable: "Province",
                principalColumn: "ProvinceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Commune_CommuneId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_District_DistrictId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Province_ProvinceId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_CommuneId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DistrictId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ProvinceId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CommuneId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "Employees");
        }
    }
}
