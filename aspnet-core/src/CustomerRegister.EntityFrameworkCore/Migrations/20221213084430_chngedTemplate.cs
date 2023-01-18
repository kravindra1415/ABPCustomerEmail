using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerRegister.Migrations
{
    public partial class chngedTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
