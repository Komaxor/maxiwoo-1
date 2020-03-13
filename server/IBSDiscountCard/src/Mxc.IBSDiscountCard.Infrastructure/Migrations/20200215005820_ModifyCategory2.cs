using Microsoft.EntityFrameworkCore.Migrations;

namespace Mxc.IBSDiscountCard.Infrastructure.Migrations
{
    public partial class ModifyCategory2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Places");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Places",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Places");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Places",
                nullable: true);
        }
    }
}
