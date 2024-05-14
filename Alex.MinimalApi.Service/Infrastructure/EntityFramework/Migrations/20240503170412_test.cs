using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alex.MinimalApi.Service.Repository.EntiryFramework.Migrations
{
    /// <inheritdoc />
#pragma warning disable CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.
    public partial class test : Migration
#pragma warning restore CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaxFile_Employees_EmployeeId",
                table: "TaxFile");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxFileRecord_TaxFile_TaxFileId",
                table: "TaxFileRecord");

            migrationBuilder.DropIndex(
                name: "IX_TaxFile_EmployeeId",
                table: "TaxFile");

            migrationBuilder.AlterColumn<int>(
                name: "TaxFileId",
                table: "TaxFileRecord",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "TaxFile",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaxFile_EmployeeId",
                table: "TaxFile",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TaxFile_Employees_EmployeeId",
                table: "TaxFile",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaxFileRecord_TaxFile_TaxFileId",
                table: "TaxFileRecord",
                column: "TaxFileId",
                principalTable: "TaxFile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaxFile_Employees_EmployeeId",
                table: "TaxFile");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxFileRecord_TaxFile_TaxFileId",
                table: "TaxFileRecord");

            migrationBuilder.DropIndex(
                name: "IX_TaxFile_EmployeeId",
                table: "TaxFile");

            migrationBuilder.AlterColumn<int>(
                name: "TaxFileId",
                table: "TaxFileRecord",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "TaxFile",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_TaxFile_EmployeeId",
                table: "TaxFile",
                column: "EmployeeId",
                unique: true,
                filter: "[EmployeeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_TaxFile_Employees_EmployeeId",
                table: "TaxFile",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaxFileRecord_TaxFile_TaxFileId",
                table: "TaxFileRecord",
                column: "TaxFileId",
                principalTable: "TaxFile",
                principalColumn: "Id");
        }
    }
}
