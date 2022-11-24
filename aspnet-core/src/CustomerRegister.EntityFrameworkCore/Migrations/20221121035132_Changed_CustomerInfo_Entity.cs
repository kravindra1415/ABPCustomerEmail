using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerRegister.Migrations
{
    public partial class Changed_CustomerInfo_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Courses",
                table: "AppCustomerInfo");

            migrationBuilder.AddColumn<string>(
                name: "CourseName",
                table: "AppCustomerInfo",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseName",
                table: "AppCustomerInfo");

            migrationBuilder.AddColumn<int>(
                name: "Courses",
                table: "AppCustomerInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
