using Microsoft.EntityFrameworkCore.Migrations;

namespace Mxc.IBSDiscountCard.Infrastructure.Migrations
{
    public partial class AddHiddenPropertyToPlace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHidden",
                table: "Places",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHidden",
                table: "Places");
        }
    }
}
