using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageEmployee.Migrations
{
    /// <inheritdoc />
    public partial class EditIssuedByProvinceToDiploma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IssuedByProvince",
                table: "Diploma");

            migrationBuilder.AddColumn<int>(
                name: "IssuedByProvinceId",
                table: "Diploma",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Diploma_IssuedByProvinceId",
                table: "Diploma",
                column: "IssuedByProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diploma_Province_IssuedByProvinceId",
                table: "Diploma",
                column: "IssuedByProvinceId",
                principalTable: "Province",
                principalColumn: "ProvinceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diploma_Province_IssuedByProvinceId",
                table: "Diploma");

            migrationBuilder.DropIndex(
                name: "IX_Diploma_IssuedByProvinceId",
                table: "Diploma");

            migrationBuilder.DropColumn(
                name: "IssuedByProvinceId",
                table: "Diploma");

            migrationBuilder.AddColumn<string>(
                name: "IssuedByProvince",
                table: "Diploma",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
