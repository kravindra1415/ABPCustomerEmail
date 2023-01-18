using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerRegister.Migrations
{
    public partial class tempTableChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseName",
                table: "AppTemplateInfo");

            migrationBuilder.DropColumn(
                name: "CustomerEmail",
                table: "AppTemplateInfo");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "AppTemplateInfo");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "AppTemplateInfo");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "AppTemplateInfo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CourseName",
                table: "AppTemplateInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerEmail",
                table: "AppTemplateInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "AppTemplateInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "AppTemplateInfo",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "AppTemplateInfo",
                type: "datetime2",
                nullable: true);
        }
    }
}
