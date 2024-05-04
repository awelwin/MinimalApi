using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alex.MinimalApi.Service.Repository.EntiryFramework.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaxFileRecord_TaxFile_TaxFileId",
                table: "TaxFileRecord");

            migrationBuilder.AlterColumn<int>(
                name: "TaxFileId",
                table: "TaxFileRecord",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TaxFileRecord_TaxFile_TaxFileId",
                table: "TaxFileRecord",
                column: "TaxFileId",
                principalTable: "TaxFile",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaxFileRecord_TaxFile_TaxFileId",
                table: "TaxFileRecord");

            migrationBuilder.AlterColumn<int>(
                name: "TaxFileId",
                table: "TaxFileRecord",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TaxFileRecord_TaxFile_TaxFileId",
                table: "TaxFileRecord",
                column: "TaxFileId",
                principalTable: "TaxFile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
