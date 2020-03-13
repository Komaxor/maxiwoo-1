using Microsoft.EntityFrameworkCore.Migrations;

namespace Mxc.IBSDiscountCard.Infrastructure.Migrations
{
    public partial class ModifyCategory1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Places",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Places");
        }
    }
}
