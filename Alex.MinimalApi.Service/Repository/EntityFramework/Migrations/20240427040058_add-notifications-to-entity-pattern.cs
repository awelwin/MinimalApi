using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alex.MinimalApi.Service.Repository.EntiryFramework.Migrations
{
    /// <inheritdoc />
    public partial class addnotificationstoentitypattern : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Notifications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Notifications",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Notifications");
        }
    }
}
