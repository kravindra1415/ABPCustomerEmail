using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerRegister.Migrations
{
    public partial class chngedTemplates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTemplateInfo_AppCustomerInfo_CustomerInfoDataId",
                table: "AppTemplateInfo");

            migrationBuilder.DropIndex(
                name: "IX_AppTemplateInfo_CustomerInfoDataId",
                table: "AppTemplateInfo");

            migrationBuilder.DropColumn(
                name: "CustomerInfoDataId",
                table: "AppTemplateInfo");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerInfoDataId",
                table: "AppTemplateInfo",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppTemplateInfo_CustomerInfoDataId",
                table: "AppTemplateInfo",
                column: "CustomerInfoDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppTemplateInfo_AppCustomerInfo_CustomerInfoDataId",
                table: "AppTemplateInfo",
                column: "CustomerInfoDataId",
                principalTable: "AppCustomerInfo",
                principalColumn: "Id");
        }
    }
}
